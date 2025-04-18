using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : NotificationHandler
{
    [SerializeField] private string personName;
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHP;
    private int absorptionHP;
    [SerializeField] private int physAttack;
    private int tempPhysAttack;
    [SerializeField] private int specAttack;
    private int tempSpecAttack;
    [SerializeField] private int skill;
    private int tempSkill;
    [SerializeField] private int speed;
    private int tempSpeed;
    [SerializeField] private int physDefense;
    private int tempPhysDefense;
    [SerializeField] private int specDefense;
    private int tempSpecDefense;
    [SerializeField] private List<EffectType> types;
    private List<EffectType> tempTypes;

    public List<string> moveCategories;
    public List<Move> movesCat1;
    public List<Move> movesCat2;
    public List<Move> movesCat3;
    public List<Move> movesCat4;

    public Item item;

    private Transform secondHandItem;
    private bool secondHandIsLeft;

    public string myPlayer;

    void Start()
    {
        equip();
    }
    public List<List<Move>> getAllCategories()
    {
        List<List<Move>> ret = new List<List<Move>>();
        if (movesCat1 != null && movesCat1.Count > 0)
        {
            ret.Add(movesCat1);
        }
        if (movesCat2 != null && movesCat2.Count > 0)
        {
            ret.Add(movesCat2);
        }
        if (movesCat3 != null && movesCat3.Count > 0)
        {
            ret.Add(movesCat3);
        }
        if (movesCat4 != null && movesCat4.Count > 0)
        {
            ret.Add(movesCat4);
        }
        if (item != null)
        {
            List<Move> itemMoves = item.getMoves();
            if (itemMoves != null && itemMoves.Count > 0)
            {
                ret.Add(itemMoves);
            }
        }
        return ret;
    }
    public void playAnimation(string anim)
    {
        StaticData.findDeepChild(transform, "model").GetComponent<Animator>().Play(anim);
    }

    public enum EffectType
    {
        FIRE, WATER, EARTH, AIR, LIGHTNING, ICE, LIFE, LIGHT, DARK, LEARNED_MAGIC, INNATE_MAGIC,
        MARTIAL_ARTS, NOVELTY_WEAPON, FIREARMS, TRADITIONAL_WEAPONS, HIGH_DIMENSION, DREAM_DIMENSION,
        TECHNOLOGY, SPIRIT
    }

    public bool isAlive()
    {
        return currentHP > 0;
    }

    public void equip()
    {
        if (item == null)
        {
            secondHandItem = null;
        }
        else
        {
            if (item.equipType == Item.EquipType.RIGHT_DOMINANT)
            {
                secondHandItem = StaticData.findDeepChild(item.transform, "Left");
                secondHandIsLeft = true;
                Transform right_hand = StaticData.findDeepChild(transform, "RightHand");
                item.transform.rotation = right_hand.rotation;
                item.transform.SetParent(right_hand);
            }
            else if (item.equipType == Item.EquipType.LEFT_DOMINANT)
            {
                secondHandItem = StaticData.findDeepChild(item.transform, "Right");
                secondHandIsLeft = false;
                Transform left_hand = StaticData.findDeepChild(transform, "LeftHand");
                item.transform.rotation = left_hand.rotation;
                item.transform.SetParent(left_hand);
            }
            else if (item.equipType == Item.EquipType.RIGHT_HAND)
            {
                Transform right_hand = StaticData.findDeepChild(transform, "RightHand");
                item.transform.rotation = right_hand.rotation;
                item.transform.SetParent(right_hand);
            }
            else if (item.equipType == Item.EquipType.LEFT_HAND)
            {
                Transform left_hand = StaticData.findDeepChild(transform, "LeftHand");
                item.transform.rotation = left_hand.rotation;
                item.transform.SetParent(left_hand);
            }
            else if (item.equipType == Item.EquipType.HEAD)
            {
                Transform head = StaticData.findDeepChild(transform, "Head");
                item.transform.rotation = head.rotation;
                item.transform.SetParent(head);
            }
            item.transform.localPosition = Vector3.zero;
        }
    }
    void Update()
    {
        if (secondHandItem != null)
        {
            Vector3 pos;
            Quaternion rot;
            if (secondHandIsLeft)
            {
                StaticData.findDeepChild(transform, "LeftHand").GetPositionAndRotation(out pos, out rot);
            }
            else
            {
                StaticData.findDeepChild(transform, "RightHand").GetPositionAndRotation(out pos, out rot);
            }
            secondHandItem.SetPositionAndRotation(pos, rot);
        }
    }
}
