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

    void Start()
    {
        equip();
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
                item.transform.localPosition = right_hand.localPosition;
                item.transform.rotation = right_hand.rotation;
                item.transform.SetParent(right_hand);
            }
            else if (item.equipType == Item.EquipType.LEFT_DOMINANT)
            {
                secondHandItem = StaticData.findDeepChild(item.transform, "Right");
                secondHandIsLeft = false;
                Transform left_hand = StaticData.findDeepChild(transform, "LeftHand");
                item.transform.localPosition = left_hand.localPosition;
                item.transform.rotation = left_hand.rotation;
                item.transform.SetParent(left_hand);
            }
            else if (item.equipType == Item.EquipType.RIGHT_HAND)
            {
                Transform right_hand = StaticData.findDeepChild(transform, "RightHand");
                item.transform.localPosition = right_hand.localPosition;
                item.transform.rotation = right_hand.rotation;
                item.transform.SetParent(right_hand);
            }
            else if (item.equipType == Item.EquipType.LEFT_HAND)
            {
                Transform left_hand = StaticData.findDeepChild(transform, "LeftHand");
                item.transform.localPosition = left_hand.localPosition;
                item.transform.rotation = left_hand.rotation;
                item.transform.SetParent(left_hand);
            }
            else if (item.equipType == Item.EquipType.HEAD)
            {
                Transform head = StaticData.findDeepChild(transform, "Head");
                item.transform.localPosition = head.localPosition;
                item.transform.rotation = head.rotation;
                item.transform.SetParent(head);
            }
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
