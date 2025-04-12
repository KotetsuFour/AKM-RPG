using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int absorptionHP;
    [SerializeField] private int tempPhysAttack;
    [SerializeField] private int tempSpecAttack;
    [SerializeField] private int tempSkill;
    [SerializeField] private int tempSpeed;
    [SerializeField] private int tempPhysDefense;
    [SerializeField] private int tempSpecDefense;
    [SerializeField] private List<Move> itemMoves;
    [SerializeField] private List<Person.EffectType> itemTypes;
    public EquipType equipType;
    [SerializeField] private string idleAnimation;

    public enum EquipType
    {
        RIGHT_HAND, LEFT_HAND, HEAD, RIGHT_DOMINANT, LEFT_DOMINANT
    }
}
