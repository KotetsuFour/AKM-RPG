using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNotification
{
    private bool disputable;
    private Nature nature;
    private Stage stage;
    private NotificationHandler cause;

    private int[] intVals;

    private List<Permission> permissionsQueue;
    private bool denied;

    public GameNotification(Nature nature, bool disputable, NotificationHandler cause)
    {
        this.nature = nature;
        this.disputable = disputable;
        this.stage = Stage.PERMISSION;
        this.cause = cause;
    }
    public NotificationHandler getCause()
    {
        return cause;
    }
    public void setInts(int[] vals)
    {
        intVals = vals;
    }
    public void allow()
    {
        if (!disputable)
        {
            stage = Stage.ACTING;
        }
        permissionsQueue = new List<Permission>();
        List<NotificationHandler> handlers = StaticData.board.getAllReactors();
        foreach (NotificationHandler handler in handlers)
        {
            Permission permit = handler.allowNotification(this);
            if (!permit.permitted)
            {
                permit.notificationSubject = this;
                permissionsQueue.Add(permit);
            }
        }
        for (int q = 0; q < permissionsQueue.Count; q++)
        {
            foreach (NotificationHandler handler in handlers)
            {
                Permission permit = handler.allowPermission(permissionsQueue[q]);
                if (!permit.permitted)
                {
                    permit.permissionSubject = permissionsQueue[q];
                    permissionsQueue.Add(permit);
                }
            }
        }
        for (int q = permissionsQueue.Count - 1; q >= 0; q--)
        {
            permissionsQueue[q].deny();
        }
        if (!denied)
        {
            stage = Stage.ACTING;
        }
        stage = Stage.DENIED;
    }
    public void act()
    {
        stage = Stage.COMPLETED;
    }

    public bool isDisputable()
    {
        return disputable;
    }
    public Nature getNature()
    {
        return nature;
    }
    public Stage getStage()
    {
        return stage;
    }
    public int[] getInts()
    {
        return intVals;
    }


    public enum Nature
    {
        GAME_START, ROUND_START, TURN_START, PLAY_PHASE, WAIT, TURN_END, ROUND_END, GAME_END, FINISH, STANDBY,
        PLAYER_ACTION, LOCATION_EFFECT,
        PERM_ALTER_POWER, TEMP_ALTER_POWER, ALTER_COST,
        CREATE_CARD, RELOCATE_CARD, CHANGE_LOCATION, ALTER_ONGOING, TRANSFORM_CARD
    }
    public enum Stage
    {
        PERMISSION, ACTING, COMPLETED, DENIED
    }

    public class Permission
    {
        public NotificationHandler actor;
        public bool permitted;
        public int effectType; //0 for general, 1 for onReveal, 2 for ongoing
        public GameNotification notificationSubject;
        public Permission permissionSubject;

        public bool denied;
        public Permission(NotificationHandler actor, bool permitted, int effectType)
        {
            this.actor = actor;
            this.permitted = permitted;
            this.effectType = effectType;
        }
        public Permission(NotificationHandler actor, bool permitted)
        {
            this.actor = actor;
            this.permitted = permitted;
        }
        public void deny()
        {
            if (!denied && !permitted)
            {
                if (notificationSubject != null)
                {
                    notificationSubject.denied = true;
                }
                else if (permissionSubject != null)
                {
                    permissionSubject.denied = true;
                }
            }
        }
    }

}
