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
}
