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
    public SteamVR_Action_Boolean grab;  //
    public SteamVR_Action_Vibration haptic;

    void Awake()
    {
        any = SteamVR_Input_Sources.Any;
        leftHand = SteamVR_Input_Sources.LeftHand;
        trigger = SteamVR_Actions.default_InteractUI;
        trackPadTouch = SteamVR_Actions.default_TrackPadTouch;
        trackPadPosition = SteamVR_Actions.default_TrackPadPosition;
        grab = SteamVR_Actions.default_GrabGrip;
        haptic = SteamVR_Actions.default_Haptic;
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

        if (grab.GetStateDown(any))
        {
            Debug.Log("Grab Grip button");
            SteamVR_Actions.default_Haptic.Execute(0.1f, 0.2f, 50.0f, 0.5f, any);
        }
    }

    public void OnButtonClick(string msg)
    {
        Debug.Log($"{msg} button clicked !!");
    }
}
