using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DrawMgr : MonoBehaviour
{
    private SteamVR_Action_Boolean trigger;
    private SteamVR_Input_Sources rightHand;
    private SteamVR_Action_Pose pose;

    public float lineWidth = 0.01f;
    private LineRenderer line;
    private int count = 0;

    void Start()
    {
        trigger = SteamVR_Actions.default_InteractUI;
        rightHand = SteamVR_Input_Sources.RightHand;        
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.GetStateDown(rightHand))
        {
            CreateLine();
        }
    }

    void CreateLine()
    {
        GameObject lineObject = new GameObject($"Line_{++count}");
        line = lineObject.AddComponent<LineRenderer>();

        Material mt = new Material(Shader.Find("Unlit/Color"));
        mt.color = Color.white;
        line.material = mt;

        line.useWorldSpace = false;
        line.receiveShadows = false;
        line.numCapVertices = 10;

        line.startWidth = lineWidth;
        line.endWidth   = lineWidth;
        line.positionCount = 1;

        line.SetPosition(0, pose.GetLocalPosition(rightHand));
    }
}
