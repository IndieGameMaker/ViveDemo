using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveController : MonoBehaviour
{
    public SteamVR_Input_Sources leftHand;
    public SteamVR_Input_Sources any;
    public SteamVR_Action_Boolean trigger;
    public SteamVR_Action_Boolean trackPadTouch;
    public SteamVR_Action_Vector2 trackPadPosition;

    void Awake()
    {
        any = SteamVR_Input_Sources.Any;
        leftHand = SteamVR_Input_Sources.LeftHand;
        trigger = SteamVR_Actions.default_InteractUI;
        trackPadTouch = SteamVR_Actions.default_TrackPadTouch;
        trackPadPosition = SteamVR_Actions.default_TrackPadPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.GetStateDown(leftHand))
        {
            Debug.Log("Trigger Button Clicked");
        }

        if (trackPadTouch.GetState(any))
        {
            Vector2 pos = trackPadPosition.GetAxis(any);
            //Debug.LogFormat("TrackPad Position x={0}/y={1}", pos.x, pos.y);


            Debug.Log($"Position x={pos.x} / y={pos.y}"); 
        }
    }
}
