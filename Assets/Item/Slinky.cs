using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slinky : MonoBehaviour
{
    private Transform baseSegment;
    private Transform[] midSegments;
    private Transform looseSegment;

    void Start()
    {
        baseSegment = StaticData.findDeepChild(transform, "Base");
        looseSegment = StaticData.findDeepChild(transform, "Left");

        List<Transform> segs = new List<Transform>();
        int counter = 1;
        Transform seg = StaticData.findDeepChild(transform, $"Mid{counter}");
        while (seg != null)
        {
            segs.Add(seg);
            counter++;
            seg = StaticData.findDeepChild(transform, $"Mid{counter}");
        }
        midSegments = segs.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = looseSegment.localPosition - baseSegment.localPosition;
        Vector3 rotation = looseSegment.rotation.eulerAngles - baseSegment.rotation.eulerAngles;
        for (int q = 0; q < midSegments.Length; q++)
        {
            Transform seg = midSegments[q];
            Vector3 pos = direction * (q + 1) / (midSegments.Length + 1);
            Vector3 rot = rotation * (q + 1) / (midSegments.Length + 1);
            seg.SetLocalPositionAndRotation(pos, baseSegment.rotation);
            seg.Rotate(rot);
        }
    }
}
