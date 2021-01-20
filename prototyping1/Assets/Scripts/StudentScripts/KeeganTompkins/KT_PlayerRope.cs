using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KT_PlayerRope : MonoBehaviour
{
    public float PlayerRange = 5.0f;
    public Transform UI_Range = null;

    public float E_PromptRange = 1.5f;
    private GameObject E_Prompt = null;
    public GameObject E_Prompt_Prefab = null;
    private KT_Post ClosestPost = null;
    private KT_Post SlidePost = null;
    public Vector3 E_Offset;

    //Sliding variables
    public float Speed;
    public Vector3 SlideOffset;
    public ParticleSystem slideParticles;
    public Animator animator;
    float TimeAcross;
    float TotalTime;

    bool Sliding;


    // Start is called before the first frame update
    void Start()
    {
        if (UI_Range)
        {
            UI_Range.localScale = new Vector3(PlayerRange, PlayerRange, 1.0f) * 2;
        }
    }

    KT_Post ClosestPostToSelf()
    { 
        var Posts = FindObjectsOfType<KT_Post>();

        float MinDist = -1.0f;
        KT_Post MinPost = null;

        foreach (var post in Posts)
        {
            if (!post.IsAttached) continue;

            float dist = Vector2.Distance(post.transform.position, transform.position);

            if (dist <= E_PromptRange
                && (dist < MinDist || MinDist < 0.0f))
            {
                MinDist = dist;
                MinPost = post;
            }
        }
        return MinPost;
    }

    void UpdateUI()
    {
        var newClosest = ClosestPostToSelf();
        if (newClosest != ClosestPost)
        {
            if (ClosestPost != null)
            {
                Destroy(E_Prompt);
            }
            if (newClosest != null)
            {
                E_Prompt = Instantiate(E_Prompt_Prefab, newClosest.transform.position + E_Offset, Quaternion.identity);
            }

            ClosestPost = newClosest;
        }
    }

    void UpdateSlide()
    {
        TimeAcross = Mathf.Clamp(TimeAcross + Time.deltaTime * Speed, 0.0f, TotalTime);
        transform.position = SlidePost.AttachedRope.PointOnRope(TimeAcross / TotalTime, SlidePost) + SlideOffset;

        if (TimeAcross >= TotalTime)
        {
            Sliding = false;
            GetComponent<PlayerMove>().enabled = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<BoxCollider2D>().enabled = true;
            transform.position -= SlideOffset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Sliding)
        {
            UpdateUI();
        }

        if (E_Prompt != null && Input.GetKeyDown(KeyCode.E))
        {
            if (E_Prompt) Destroy(E_Prompt);

            TimeAcross = 0.0f;
            SlidePost = ClosestPost;
            Sliding = true;
            GetComponent<PlayerMove>().enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<BoxCollider2D>().enabled = false;
            TotalTime = Mathf.Log(SlidePost.AttachedRope.RopeLength(), 2.0f);
        }

        if (Sliding == true)
        {
            if (slideParticles.isStopped)
                slideParticles.Play();
            UpdateSlide();
            animator.enabled = false;
        }
        else
        {
            slideParticles.Stop();
            slideParticles.Clear();
            animator.enabled = true;
        }
    }
}
