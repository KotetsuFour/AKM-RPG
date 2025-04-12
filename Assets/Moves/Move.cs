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
    [SerializeField] private float[] hitGameTimes;
    [SerializeField] private KeyCode[] hitGameValues;

    [SerializeField] private string[] moveAnimations;
    [SerializeField] private float[] timesOfMoveAnimations;
    [SerializeField] private int[] camerasToDisplay;
    [SerializeField] private float[] timesForCameraDisplays;
    [SerializeField] private float totalMoveTime;

    public float moveTimer;
    private int camIdx;
    private int animIdx;
    private int hitGameIdx;

    public Person user;

    public void basicMovePreparation()
    {
        moveTimer = 0;
        camIdx = 0;
        animIdx = 0;
        hitGameIdx = 0;
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
            StaticData.setActiveCamera(camerasToDisplay[camIdx]);
            camIdx++;
        }
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

}
