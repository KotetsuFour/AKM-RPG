using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationHandler : MonoBehaviour
{
    public virtual GameNotification.Permission allowNotification(GameNotification note)
    {
        return new GameNotification.Permission(this, true);
    }
    public virtual GameNotification.Permission allowPermission(GameNotification.Permission note)
    {
        return new GameNotification.Permission(this, true);
    }
    public virtual List<GameNotification> getResponse(GameNotification note)
    {
        return null;
    }
}
