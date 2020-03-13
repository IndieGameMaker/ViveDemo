using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    private SteamVR_Behaviour_Pose pose;
    private SteamVR_Input_Sources hand;
    private LineRenderer line;
    public float distance = 10.0f;

    private Ray ray;
    private RaycastHit hit;
    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        hand = pose.inputSource;

        CreateLine();
    }

    private void Update()
    {
        ray = new Ray(tr.position, tr.forward);
        if (Physics.Raycast(ray, out hit, distance))
        {
            line.SetPosition(1, new Vector3(0, 0, hit.distance));
        }
    }

    void CreateLine()
    {
        line = this.gameObject.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.positionCount = 2;
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, new Vector3(0, 0, distance));
        line.startWidth = 0.05f;
        line.endWidth = 0.005f;

        line.material = new Material(Shader.Find("Unlit/Color"));
        line.material.color = Color.blue;
    }
}
