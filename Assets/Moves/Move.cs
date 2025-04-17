using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move : MonoBehaviour
{
    public static float HITGAME_ERROR_MARGIN = 0.1f;

    public string moveName;
    public int minRange;
    public int maxRange;
    public bool disabled;
    public bool hasHitGame;
    public int numTargets;
    [SerializeField] private float[] hitGameTimes;
    [SerializeField] private KeyCode[] hitGameValues;

    [SerializeField] private string[] moveAnimations;
    [SerializeField] private float[] timesOfMoveAnimations;
    [SerializeField] private CameraTargetSetting[] cameraTargets;
    [SerializeField] private CameraRig.CameraPositionSetting[] cameraPositions;
    [SerializeField] private float[] cameraDistances;
    [SerializeField] private CameraRig.CameraMoveSetting[] cameraMoveSettings;
    [SerializeField] private float[] cameraSpeeds;
    [SerializeField] private float[] timesForCameraDisplays;
    [SerializeField] private float totalMoveTime;

    public float moveTimer;
    private int camIdx;
    private int animIdx;
    private int hitGameIdx;
    private int hitGameScore;
    private bool hitGameStarted;

    public Person user;
    public MoveTargetType targeting;

    protected SelectionMode selectionMode;

    public void basicMovePreparation()
    {
        moveTimer = 0;
        camIdx = 0;
        animIdx = 0;
        hitGameIdx = 0;
        hitGameScore = 0;
    }
    public virtual void extraSetup()
    {
        //NOTHING
    }
    public bool donePerformingHitGame()
    {
        if (!hasHitGame)
        {
            return true;
        }
        if (!hitGameStarted)
        {
            hitGameStarted = true;
            basicMovePreparation();
            extraSetup();
            selectionMode = SelectionMode.HITGAME;
        }
        if (gradeHit())
        {
            hitGameScore++;
        }
        return hitGameIdx < hitGameTimes.Length;
    }
    public int getHitGameScore()
    {
        return hitGameScore;
    }
    public void updateAnimationAndCamera()
    {
        if(animIdx < timesOfMoveAnimations.Length && moveTimer >= timesOfMoveAnimations[animIdx])
        {
            user.playAnimation(moveAnimations[animIdx]);
            animIdx++;
        }
        if (camIdx < timesForCameraDisplays.Length && moveTimer >= timesForCameraDisplays[camIdx])
        {
            StaticData.board.cam.setCameraSettings(getTarget(cameraTargets[camIdx]), cameraPositions[camIdx],
                cameraDistances[camIdx], cameraMoveSettings[camIdx], cameraSpeeds[camIdx]);
            camIdx++;
        }
    }

    private Transform getTarget(CameraTargetSetting targ)
    {
        if (targ == CameraTargetSetting.ACTOR)
        {
            return user.transform;
        }
        else if (targ == CameraTargetSetting.TARGET)
        {

        }
        else if (targ == CameraTargetSetting.BOARD)
        {
            return StaticData.board.transform;
        }
        else if (targ == CameraTargetSetting.PROJECTILE)
        {

        }
        return null;
    }

    public bool gradeHit()
    {
        if (hitGameIdx < hitGameValues.Length)
        {
            if (Input.GetKeyDown(hitGameValues[hitGameIdx])
                && Mathf.Abs(hitGameTimes[hitGameIdx] - moveTimer) <= HITGAME_ERROR_MARGIN)
            {
                hitGameIdx++;
                return true;
            }

            if (moveTimer - hitGameTimes[hitGameIdx] > HITGAME_ERROR_MARGIN)
            {
                hitGameIdx++;
            }
        }
        return false;
    }

    public enum CameraTargetSetting
    {
        NONE, ACTOR, TARGET, BOARD, PROJECTILE
    }
    public enum MoveTargetType
    {
        NONE, ONE_ENEMY, ONE_ALLY, AOE, ALL, ALLIES, ENEMIES, MENU_SELECTION
    }
    public enum SelectionMode
    {
        STANDBY, HITGAME, ACTING
    }
}
