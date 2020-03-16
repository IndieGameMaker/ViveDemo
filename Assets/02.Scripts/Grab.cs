using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grab : MonoBehaviour
{
    private Transform tr;
    private GameObject grabObject; //잡은 물체
    private bool isTouched = false;    

    private SteamVR_Action_Boolean trigger;
    private SteamVR_Input_Sources rightHand;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        trigger = SteamVR_Actions.default_InteractUI;
        rightHand = SteamVR_Input_Sources.RightHand;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabObject == null) return;

        if (isTouched && trigger.GetStateDown(rightHand))
        {
            grabObject.transform.SetParent(tr);
            grabObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        if (trigger.GetStateUp(rightHand))
        {
            grabObject.transform.SetParent(null);
            //Vector3 _velocity = SteamVR_Actions.default_Pose.velocity;
            Vector3 _velocity = SteamVR_Actions.default_Pose.GetVelocity(rightHand);
            var rb = grabObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.velocity = _velocity;

            grabObject = null;
            isTouched = false;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("BALL"))
        {
            grabObject = coll.gameObject;
            isTouched = true;
        }
    }
}
