using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatChanger : Move
{
    [SerializeField] private int hpChange;
    [SerializeField] private int physAttChange;
    [SerializeField] private int specAttChange;
    [SerializeField] private int speedChange;
    [SerializeField] private int skillChange;
    [SerializeField] private int physDefChange;
    [SerializeField] private int specDefChange;
    [SerializeField] private List<Person.EffectType> givenTypes;
    [SerializeField] private int duration;
    [SerializeField] private bool stackable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
