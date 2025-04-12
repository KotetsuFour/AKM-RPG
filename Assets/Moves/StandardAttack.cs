using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardAttack : Move
{
    [SerializeField] private int fullDamage;
    [SerializeField] private bool usingPhysAttack;
    [SerializeField] private bool usingSpecAttack;

    [SerializeField] private float timeOfTargetReact;
    [SerializeField] private string preferredTargetReactAnimation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
