using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gameboard : MonoBehaviour
{
    [SerializeField] private PositionState[] party1States;
    [SerializeField] private PositionState[] party2States;
    [SerializeField] private PositionState[] party3States;
    public List<PositionState> allPositions;

    public CameraRig cam;

    private Location location;
    private Person[] party1;
    private Person[] party2;
    private Person[] party3;

    private List<Person> turnQueue;
    private int round;
    private int turn;
    private int moveTo;
    private int category;
    private int moveIdx;
    private int target;
    private bool donePlaying;
    private int winner;
    private List<List<Move>> allMoveCategories;
    private Move selectedMove;
    private List<Move> activeMoves;

    private LinkedList<GameNotification> actionQueue;
    private GameArchive archive;

    private SelectionMode selectionMode;
    [SerializeField] private LayerMask positionLayer;
    [SerializeField] private LayerMask personLayer;
    [SerializeField] private Transform menu;

    public List<NotificationHandler> getAllReactors()
    {
        List<NotificationHandler> ret = new List<NotificationHandler>();
        foreach (Person p in party1)
        {
            ret.Add(p);
        }
        foreach (Person p in party2)
        {
            ret.Add(p);
        }
        foreach (Person p in party3)
        {
            ret.Add(p);
        }
        foreach (Move move in activeMoves)
        {
            if (move.isActive())
            {
                ret.Add(move);
            }
            else
            {
                activeMoves.Remove(move);
                Destroy(move.gameObject);
            }
        }
        ret.Add(location);

        return ret;
    }

    // Start is called before the first frame update
    void Start()
    {
        StaticData.board = this;

        allPositions = new List<PositionState>();
        allPositions.AddRange(party1States);
        allPositions.AddRange(party2States);
        allPositions.AddRange(party3States);

        actionQueue = new LinkedList<GameNotification>();

        archive = new GameArchive();
    }

    // Update is called once per frame
    void Update()
    {
        if (actionQueue.Count > 0)
        {
            GameNotification currentAction = actionQueue.First.Value;

            if (currentAction.getStage() == GameNotification.Stage.PERMISSION)
            {
                currentAction.allow();
            }
            else if (currentAction.getStage() == GameNotification.Stage.ACTING)
            {
                currentAction.act();
            }
            else if (currentAction.getStage() == GameNotification.Stage.DENIED)
            {
                actionQueue.RemoveFirst();
            }
            else if (currentAction.getStage() == GameNotification.Stage.COMPLETED)
            {
                if (currentAction.getNature() == GameNotification.Nature.LOCATION_EFFECT)
                {
                    List<GameNotification> responses = currentAction.getCause().getResponse(currentAction);
                    LinkedListNode<GameNotification> after = actionQueue.First;
                    foreach (GameNotification note in responses)
                    {
                        actionQueue.AddAfter(after, note);
                        after = after.Next;
                    }
                    actionQueue.RemoveFirst();
                }
                else
                {
                    List<GameNotification> responses = new List<GameNotification>();
                    List<NotificationHandler> responders = getAllReactors();
                    foreach (NotificationHandler card in responders)
                    {
                        List<GameNotification> response = card.getResponse(currentAction);
                        if (response != null)
                        {
                            responses.AddRange(response);
                        }
                    }
                    LinkedListNode<GameNotification> after = actionQueue.First;
                    foreach (GameNotification note in responses)
                    {
                        actionQueue.AddAfter(after, note);
                        after = after.Next;
                    }
                    processNotification();
                    actionQueue.RemoveFirst();
                }
            }
        }

        if (selectionMode == SelectionMode.STANDBY || selectionMode == SelectionMode.MOVE)
        {
            //TODO move camera
            if (Input.GetKey(KeyCode.W))
            {

            }
            else if (Input.GetKey(KeyCode.S))
            {

            }
            if (Input.GetKey(KeyCode.A))
            {

            }
            else if (Input.GetKey(KeyCode.D))
            {

            }
        }

        if (selectionMode == SelectionMode.MOVE)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //TODO you can only move to adjacent spots
                RaycastHit hit;
                if (Physics.Raycast(cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue, positionLayer))
                {
                    moveTo = allPositions.IndexOf(hit.collider.GetComponent<PositionState>());
                    //TODO visualize tentative move
                }
                //TODO bring up the action menu
                selectionMode = SelectionMode.STANDBY;
            }
        }
        else if (selectionMode == SelectionMode.SELECT_TARGET)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue, personLayer))
            {
                target = turnQueue.IndexOf(hit.collider.GetComponent<Person>());
                actOnDecision();
            }
            //TODO bring up the action menu
            selectionMode = SelectionMode.STANDBY;
        }
        else if (selectionMode == SelectionMode.HITGAME)
        {
            if (selectedMove.donePerformingHitGame())
            {

            }
        }
    }

    public void standby()
    {
        category = -1;
        moveIdx = -1;
        target = -1;
        actOnDecision();
    }

    public void selectCategory(int cat)
    {
        category = cat;
        //TODO go to moves menu
    }

    public void backFromCategoryMenu()
    {
        //TODO remove category menu
        selectionMode = SelectionMode.MOVE;
    }
    public void selectMove(int mv)
    {
        moveIdx = mv;
        selectedMove = Instantiate(allMoveCategories[category][mv]);
        //TODO remove menu
        if (selectedMove.targeting == Move.MoveTargetType.ONE_ENEMY
            || selectedMove.targeting == Move.MoveTargetType.ONE_ALLY)
        {
            selectionMode = SelectionMode.SELECT_TARGET;
        }
        else if (selectedMove.targeting == Move.MoveTargetType.MENU_SELECTION)
        {
            //TODO setup extra menu
        }
        else
        {
            //TODO setup confirm button
        }
    }
    public void extraMoveMenuSelection(int sel)
    {
        target = sel;
        //TODO remove menu
        actOnDecision();
    }
    public void confirmMove()
    {
        target = -1;
        actOnDecision();
    }
    public void actOnDecision()
    {
        activeMoves.Add(selectedMove);
        if (selectedMove.hasHitGame)
        {
            //TODO setup Hitgame
            selectionMode = SelectionMode.HITGAME;
        }
        else
        {
            finalizeMove();
        }
    }

    public void finalizeMove()
    {
        GameNotification note = new GameNotification(GameNotification.Nature.PLAYER_ACTION, false, currentActor());
        note.setInts(new int[] { round, turn, moveTo, category, moveIdx, target, selectedMove.getHitGameScore() });
        addNotification(note);
        if (StaticData.numPlayers > 1)
        {
            //TODO send data to other players
        }
    }

    public void backFromMovesMenu()
    {
        //TODO return to category menu
    }

    public void backFromSelectTarget()
    {
        //TODO return to moves menu
        selectionMode = SelectionMode.STANDBY;
    }

    private void processNotification()
    {
        GameNotification note = actionQueue.First.Value;
        if (note.getNature() == GameNotification.Nature.GAME_START)
        {
            /*TODO place all characters in their starting locations*/
            actionQueue.AddLast(new GameNotification(GameNotification.Nature.TURN_START, false, null));
        }
        else if (note.getNature() == GameNotification.Nature.ROUND_START)
        {
            actionQueue.AddLast(new GameNotification(GameNotification.Nature.TURN_START, false, null));
        }
        else if (note.getNature() == GameNotification.Nature.TURN_START)
        {
            allMoveCategories = currentActor().getAllCategories();
            if (currentActor().myPlayer == StaticData.playerName)
            {
                donePlaying = false;
                //TODO setup the game for play
                actionQueue.AddLast(new GameNotification(GameNotification.Nature.PLAY_PHASE, false, null));
            }
            else
            {
                actionQueue.AddLast(new GameNotification(GameNotification.Nature.WAIT, false, null));
            }
        }
        else if (note.getNature() == GameNotification.Nature.PLAY_PHASE)
        {
            //TODO send hitGame data and play animation
        }
        else if (note.getNature() == GameNotification.Nature.WAIT)
        {
            //TODO play animation from received data
        }
        else if (note.getNature() == GameNotification.Nature.TURN_END)
        {
            winner = gameEnded();
            if (winner == 0)
            {
                turn = (turn + 1) % turnQueue.Count;
                if (turn == 0)
                {
                    actionQueue.AddLast(new GameNotification(GameNotification.Nature.ROUND_END, false, null));
                }
                else
                {
                    actionQueue.AddLast(new GameNotification(GameNotification.Nature.TURN_START, false, null));
                }
            }
            else
            {
                actionQueue.AddLast(new GameNotification(GameNotification.Nature.GAME_END, false, null));
            }
        }
        else if (note.getNature() == GameNotification.Nature.ROUND_END)
        {
            round++;
            GameNotification end = new GameNotification(GameNotification.Nature.ROUND_START, false, null);
        }
        else if (note.getNature() == GameNotification.Nature.GAME_END)
        {
            actionQueue.AddLast(new GameNotification(GameNotification.Nature.FINISH, false, null));
        }
        else if (note.getNature() == GameNotification.Nature.FINISH)
        {
            //TODO Setup end screen
            actionQueue.AddLast(new GameNotification(GameNotification.Nature.STANDBY, false, null));
        }
    }
    public Person currentActor()
    {
        return turnQueue[turn];
    }

    private int gameEnded()
    {
        //TODO return 0 for game not over, 1-3 for winner if game over
        return 0;
    }

    public void addNotification(GameNotification note)
    {
        actionQueue.AddLast(note);
    }

    public enum SelectionMode
    {
        STANDBY, MOVE, SELECT_TARGET, HITGAME
    }

}
