using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkLogic : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {

    }

    public struct TurnAction : INetworkSerializable
    {
        public int round;
        public int turn;
        public int moveTo;
        public int category;
        public int moveIdx;
        public int target;
        public int hitGameResults;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref round);
            serializer.SerializeValue(ref turn);
            serializer.SerializeValue(ref moveTo);
            serializer.SerializeValue(ref category);
            serializer.SerializeValue(ref moveIdx);
            serializer.SerializeValue(ref target);
            serializer.SerializeValue(ref hitGameResults);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
