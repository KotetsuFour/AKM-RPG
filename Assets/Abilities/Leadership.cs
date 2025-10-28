using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leadership : Ability
{
    private List<Person> affected;
    public override List<GameNotification> getResponse(GameNotification note)
    {
        List<GameNotification> ret = new List<GameNotification>();
        PositionState dest = null;
        if (note.getNature() == GameNotification.Nature.SET_POSITION)
        {
            dest = note.getPositions()[0];
        }
        else if (note.getNature() == GameNotification.Nature.MOVE_TO_POSITION)
        {
            dest = note.getPositions()[1];
        }
        if (dest != null)
        {
            if (note.getPeople()[0] == transform.parent.GetComponent<Person>())
            {
                return ret;
            }
            bool isAlly = StaticData.areAllies(note.getPeople()[0].myPlayer, transform.parent.GetComponent<Person>().myPlayer);
            bool connected = transform.parent.GetComponent<Person>().getPosition().getConnectedPositions().Contains(dest);
            if (isAlly)
            {
                if (connected && !affected.Contains(note.getPeople()[0]))
                {
                    GameNotification support = new GameNotification(GameNotification.Nature.ALTER_SPEC_ATT, true, this);
                    support.setInts(new int[] { 30 });
                    affected.Add(note.getPeople()[0]);
                    ret.Add(support);
                }
                else if (!connected && affected.Contains(note.getPeople()[0]))
                {
                    GameNotification unsupport = new GameNotification(GameNotification.Nature.ALTER_SPEC_ATT, true, this);
                    unsupport.setInts(new int[] { -30 });
                    affected.Remove(note.getPeople()[0]);
                    ret.Add(unsupport);
                }
            }
        }
        return ret;
    }
}
