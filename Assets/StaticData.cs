using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    public static Gameboard board;
    public static int numPlayers;
    public static string playerName;
    public static string playerLoginId;

    public static Transform findDeepChild(Transform parent, string childName)
    {
        LinkedList<Transform> kids = new LinkedList<Transform>();
        for (int q = 0; q < parent.childCount; q++)
        {
            kids.AddLast(parent.GetChild(q));
        }
        while (kids.Count > 0)
        {
            Transform current = kids.First.Value;
            kids.RemoveFirst();
            if (current.name == childName || current.name + "(Clone)" == childName)
            {
                return current;
            }
            for (int q = 0; q < current.childCount; q++)
            {
                kids.AddLast(current.GetChild(q));
            }
        }
        return null;
    }

    public static void setActiveCamera(int camNum)
    {
        //TODO
    }

    public static bool areAllies(string player1, string player2)
    {
        //TODO
        return false;
    }

}
