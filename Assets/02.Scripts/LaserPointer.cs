using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    private SteamVR_Behaviour_Pose pose;
    private SteamVR_Input_Sources hand;
    private SteamVR_Action_Boolean trigger;
    private SteamVR_Action_Boolean teleport;

    private LineRenderer line;
    public float distance = 10.0f;

    private Ray ray;
    private RaycastHit hit;
    private Transform tr;

    private GameObject prevButton;
    private GameObject currButton;

    [SerializeField]
    private Transform pointerTr;

    public float fadeTime = 0.2f;

    void Start()
    {
        teleport = SteamVR_Actions.default_Teleport;
        pointerTr = GameObject.Find("Pointer").GetComponent<Transform>();
        tr = GetComponent<Transform>();
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        hand = pose.inputSource;
        trigger = SteamVR_Actions.default_InteractUI;

        CreateLine();
    }

    private void Update()
    {
        ray = new Ray(tr.position, tr.forward);
        if (Physics.Raycast(ray, out hit, distance))
        {
            line.SetPosition(1, new Vector3(0, 0, hit.distance));

            pointerTr.position = hit.point + (hit.normal * 0.01f);
            pointerTr.rotation = Quaternion.LookRotation(hit.normal);

            currButton = hit.collider.gameObject;
            if (currButton != prevButton)
            {
                ExecuteEvents.Execute(currButton
                                    , new PointerEventData(EventSystem.current)
                                    , ExecuteEvents.pointerEnterHandler);

                ExecuteEvents.Execute(prevButton
                                    , new PointerEventData(EventSystem.current)
                                    , ExecuteEvents.pointerExitHandler);

                prevButton = currButton;
            }

            if (trigger.GetStateDown(hand))
            {
                ExecuteEvents.Execute(currButton
                                      , new PointerEventData(EventSystem.current)
                                      , ExecuteEvents.pointerClickHandler);
            }

        }

        if (teleport.GetLastStateUp(hand))
        {
            SteamVR_Fade.Start(Color.black, 0.0f);
            StartCoroutine(this.Teleporting(hit.point));
        }
    }

    IEnumerator Teleporting(Vector3 pos)
    {
        tr.parent.transform.position = pos;
        yield return new WaitForSeconds(fadeTime);
        SteamVR_Fade.Start(Color.clear, 0.2f);
      
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
