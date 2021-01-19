using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KT_Rope : MonoBehaviour
{
    const int KT_TOP = 0;
    const int KT_BOT = 1;
    public struct GrabPoint
    {
        public bool IsGrabbed;
        public Vector3 PrevPost;
        public Vector3 CurrPost;
        public Transform RopePoint;
    };

    public float GrabRadius = 1.0f;
    public float MaxLength = 3.0f;


    GrabPoint[] grabPoints = new GrabPoint[2];

    Vector3 GetMousePos()
    {
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = -1.0f;
        return MousePos;
    }

    Transform[] GetAttachPoints()
    {
        Transform[] retVal = new Transform[2];
        foreach (Transform t in GetComponentInChildren<Transform>())
        {
            if (t.name == "Top")
            {
                retVal[KT_TOP] = t;
            }
            else if (t.name == "Bottom")
            {
                retVal[KT_BOT] = t;
            }
        }
        return retVal;
    }

    // Start is called before the first frame update
    void Start()
    {
        grabPoints = new GrabPoint[2];

        grabPoints[KT_TOP].IsGrabbed = false;
        grabPoints[KT_BOT].IsGrabbed = false;

        var Points = GetAttachPoints();

        grabPoints[KT_TOP].RopePoint = Points[KT_TOP];
        grabPoints[KT_BOT].RopePoint = Points[KT_BOT];

        grabPoints[KT_TOP].CurrPost = Points[KT_TOP].position;
        grabPoints[KT_BOT].CurrPost = Points[KT_BOT].position;
    }

    void UpdatePosition()
    {
        Vector3 top = grabPoints[KT_TOP].CurrPost;
        Vector3 bot = grabPoints[KT_BOT].CurrPost;
        Vector3 topMbot = top - bot;

        transform.position = (top + bot) / 2.0f;

        transform.position = new Vector3(transform.position.x, transform.position.y, -1.0f);

        //Scale to fill the gap between the points.
        transform.localScale = new Vector3(0.2f, Vector3.Distance(top, bot), 1.0f);

        //Rotate to match the points.
        float angle = Vector2.SignedAngle(Vector2.up, new Vector2(topMbot.x, topMbot.y));

        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle));
    }

    float[] GetMouseDistances()
    {
        float[] retVal = new float[2];
        Vector2 MousePos = GetMousePos();

        retVal[KT_BOT] = Vector2.Distance(MousePos, grabPoints[KT_BOT].RopePoint.position);
        retVal[KT_TOP] = Vector2.Distance(MousePos, grabPoints[KT_TOP].RopePoint.position);

        return retVal;
    }

    void UpdateGrabPoints()
    {
        Vector3 MousePos = GetMousePos();

        float[] Distances = GetMouseDistances();

        if (Input.GetMouseButtonUp(0))
        {
            if (grabPoints[KT_BOT].IsGrabbed)
            {
                grabPoints[KT_BOT].IsGrabbed = false;
                grabPoints[KT_BOT].CurrPost = MousePos;
            }
            if (grabPoints[KT_TOP].IsGrabbed)
            {
                grabPoints[KT_TOP].IsGrabbed = false;
                grabPoints[KT_TOP].CurrPost = MousePos;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            float[] dists = GetMouseDistances();

            if (dists[KT_BOT] <= GrabRadius || dists[KT_TOP] <= GrabRadius)
            {
                if (dists[KT_BOT] <= dists[KT_TOP])
                {
                    grabPoints[KT_BOT].IsGrabbed = true;
                    grabPoints[KT_BOT].CurrPost = GetMousePos();
                }
                else
                {
                    grabPoints[KT_TOP].IsGrabbed = true;
                    grabPoints[KT_TOP].CurrPost = GetMousePos();
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (grabPoints[KT_BOT].IsGrabbed)
            {
                grabPoints[KT_BOT].CurrPost = GetMousePos();
            }
            if (grabPoints[KT_TOP].IsGrabbed)
            {
                grabPoints[KT_TOP].CurrPost = GetMousePos();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGrabPoints();
        UpdatePosition();
        Debug.Log("IsGrabbed: " + grabPoints[KT_BOT].IsGrabbed + "," + grabPoints[KT_TOP].IsGrabbed);
    }
}
