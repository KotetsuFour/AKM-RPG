using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionState : MonoBehaviour
{
    [SerializeField] private List<PositionState> connectedPositions;
    [SerializeField] private Transform stage;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public List<PositionState> getConnectedPositions()
    {
        return connectedPositions;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
