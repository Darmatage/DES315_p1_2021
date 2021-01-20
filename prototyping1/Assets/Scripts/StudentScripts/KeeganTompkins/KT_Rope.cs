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
        public Transform RopePoint;
        public GameObject UI_Object;
        public GameObject AttachedPost;
    };

    public float GrabRadius = 1.0f;
    public float MaxLength = 3.0f;

    public GameObject UI_Prefab;

    public Color HoverColor;
    public Color UIColor;

    public GameObject PostTop;
    public GameObject PostBot;

    public GameObject HighlightedPost = null;

    public KT_PlayerRope Player = null;

    public bool Slideable = true;

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

    bool InPlayerRange(Vector3 Pos, float range = 0.0f)
    {
        return Vector2.Distance(Pos, Player.transform.position) <= Player.PlayerRange + range;
    }
    bool InPlayerRange(int grabPoint, float range = 0.0f)
    {
        return Vector2.Distance(grabPoints[grabPoint].AttachedPost.transform.position, Player.transform.position) <= Player.PlayerRange + range;
    }

    int CurrentGrabbed()
    {
        if (grabPoints[KT_BOT].IsGrabbed) return KT_BOT;
        else if (grabPoints[KT_TOP].IsGrabbed) return KT_TOP;
        else return -1;
    }

    int NotGrabbed()
    {
        if (grabPoints[KT_BOT].IsGrabbed) return KT_TOP;
        else if (grabPoints[KT_TOP].IsGrabbed) return KT_BOT;
        else return -1;
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

        grabPoints[KT_TOP].UI_Object = Instantiate(UI_Prefab);
        grabPoints[KT_BOT].UI_Object = Instantiate(UI_Prefab);

        Vector3 Scale = new Vector3(2.0f * GrabRadius, 2.0f * GrabRadius, 1.0f);

        grabPoints[KT_TOP].UI_Object.transform.localScale = Scale;
        grabPoints[KT_BOT].UI_Object.transform.localScale = Scale;

        grabPoints[KT_TOP].AttachedPost = PostTop;
        grabPoints[KT_BOT].AttachedPost = PostBot;
        PostTop.GetComponent<KT_Post>().IsAttached = true;
        PostBot.GetComponent<KT_Post>().IsAttached = true;
        PostTop.GetComponent<KT_Post>().AttachedRope = this;
        PostBot.GetComponent<KT_Post>().AttachedRope = this;

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<KT_PlayerRope>();
        }
    }
    void UpdateUI()
    {
        if (HighlightedPost && HighlightedPost != ClosestPost())
        {
            HighlightedPost.GetComponent<SpriteRenderer>().color = HighlightedPost.GetComponent<KT_Post>().NormalColor;
        }

        for (int i = 0; i < grabPoints.Length; ++i)
        {
            GameObject UI = grabPoints[i].UI_Object;

            if (!Input.GetMouseButtonDown(0) && GetMouseDistances()[i] < GrabRadius && InPlayerRange(i, GrabRadius))
            {
                UI.GetComponent<SpriteRenderer>().color = HoverColor;
            }
            else
            {
                UI.GetComponent<SpriteRenderer>().color = UIColor;
            }

            Vector3 Position = grabPoints[i].RopePoint.position;
            Position.z = -1.0f;
            UI.transform.position = Position;

            if (grabPoints[i].IsGrabbed)
            {
                if (ClosestPost())
                {
                    HighlightedPost = ClosestPost();
                    HighlightedPost.GetComponent<SpriteRenderer>().color = HighlightedPost.GetComponent<KT_Post>().HoverColor;
                }
            }

        }
    }

    void UpdatePosition()
    {
        Vector3 top = grabPoints[KT_TOP].AttachedPost.transform.position;
        Vector3 bot = grabPoints[KT_BOT].AttachedPost.transform.position;

        if (grabPoints[KT_TOP].IsGrabbed) top = GetMousePos();
        if (grabPoints[KT_BOT].IsGrabbed) bot = GetMousePos();

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

    int MouseClosestTo()
    {
        float[] dists = GetMouseDistances();

        if (InPlayerRange(GetMousePos(), GrabRadius) && (dists[KT_BOT] <= GrabRadius || dists[KT_TOP] <= GrabRadius))
        {
            if (dists[KT_BOT] <= dists[KT_TOP])
            {
                return KT_BOT;
            }
            else
            {
                return KT_TOP;
            }
        }

        return -1;
    }

    GameObject ClosestPost()
    {
        Vector3 MousePos = GetMousePos();

        var Posts = FindObjectsOfType<KT_Post>();

        float MinDist = -1.0f;
        GameObject MinPost = null;

        foreach (var post in Posts)
        {
            float dist = Vector2.Distance(post.transform.position, MousePos);
            if (dist <= GrabRadius &&
                InPlayerRange(post.transform.position, GrabRadius) &&
                (dist <= MinDist || MinDist < 0.0f) &&
                !post.IsAttached)
            {
                MinDist = dist;
                MinPost = post.gameObject;
            }
        }

        return MinPost;
    }

    void UpdateGrabPoints()
    {
        Vector3 MousePos = GetMousePos();

        float[] Distances = GetMouseDistances();

        var closest = ClosestPost();

        // Release mouse
        if (Input.GetMouseButtonUp(0))
        {
            if (grabPoints[KT_BOT].IsGrabbed)
            {
                grabPoints[KT_BOT].IsGrabbed = false;

                if (closest != null)
                {
                    grabPoints[KT_BOT].AttachedPost.GetComponent<KT_Post>().IsAttached = false;
                    grabPoints[KT_BOT].AttachedPost.GetComponent<KT_Post>().AttachedRope = null;
                    grabPoints[KT_BOT].AttachedPost = ClosestPost();
                    closest.GetComponent<KT_Post>().IsAttached = true;
                    closest.GetComponent<KT_Post>().AttachedRope = this;
                }
            }
            if (grabPoints[KT_TOP].IsGrabbed)
            {
                grabPoints[KT_TOP].IsGrabbed = false;

                if (closest != null)
                {
                    grabPoints[KT_TOP].AttachedPost.GetComponent<KT_Post>().IsAttached = false;
                    grabPoints[KT_TOP].AttachedPost.GetComponent<KT_Post>().AttachedRope = null;
                    grabPoints[KT_TOP].AttachedPost = ClosestPost();
                    closest.GetComponent<KT_Post>().IsAttached = true;
                    closest.GetComponent<KT_Post>().AttachedRope = this;
                }
            }
        }

        // Mouse has been pressed
        else if (Input.GetMouseButtonDown(0))
        {
            if (MouseClosestTo() == KT_BOT)
            {
                grabPoints[KT_BOT].IsGrabbed = true;
            }
            else if (MouseClosestTo() == KT_TOP)
            {
                grabPoints[KT_TOP].IsGrabbed = true;
            }
        }

        // Mouse is held
        else if (Input.GetMouseButton(0))
        {
        }
    }

    public Vector3 PointOnRope(float DistanceAlongRope, KT_Post startingPost)
    {
        Vector3 Start = Vector3.zero;
        Vector3 End = Vector3.zero;

        if (grabPoints[KT_BOT].AttachedPost == startingPost.gameObject)
        {
            Start = grabPoints[KT_BOT].AttachedPost.transform.position;
            End = grabPoints[KT_TOP].AttachedPost.transform.position;
        }
        else
        {
            Start = grabPoints[KT_TOP].AttachedPost.transform.position;
            End = grabPoints[KT_BOT].AttachedPost.transform.position;
        }

        // Return the interpolation from start to end.
        return (1.0f - DistanceAlongRope) * Start + DistanceAlongRope * End;
    }

    public float RopeLength()
    {
        return Vector2.Distance(grabPoints[KT_BOT].AttachedPost.transform.position,
                                grabPoints[KT_TOP].AttachedPost.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGrabPoints();
        UpdatePosition();
        UpdateUI();

        if (GetComponent<BoxCollider2D>() != null)
        {
            if (CurrentGrabbed() != -1)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
