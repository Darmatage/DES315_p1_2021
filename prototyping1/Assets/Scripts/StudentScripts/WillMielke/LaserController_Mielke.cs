using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NOTE:: if laser does not hit anything then will throw null exception. Make sure laser is enclosed
public class LaserController_Mielke : MonoBehaviour
{
    public bool activated; // determines if laser box should be on or off
    public bool left, right, up, down; // determines which sides of box should be on when activated
    public int laserDamage;
    private GameHandler gameHandlerObj;

    // ignore these variables public so that it is easier to access them as they are on children
    public LineRenderer leftLaser, rightLaser, topLaser, botLaser;
    public Transform leftHit, rightHit, topHit, botHit;
    public Transform leftTrans, rightTrans, topTrans, botTrans;
    
    
    // Start is called before the first frame update
    void Start()
    {
        leftLaser.enabled = rightLaser.enabled = topLaser.enabled = botLaser.enabled = true;
        leftLaser.useWorldSpace = rightLaser.useWorldSpace = topLaser.useWorldSpace = botLaser.useWorldSpace = true;

        GameObject gameHandlerLocation = GameObject.FindWithTag("GameHandler");
        if (gameHandlerLocation != null)
        {
            gameHandlerObj = gameHandlerLocation.GetComponent<GameHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!activated) // if not activated do nothing
        {
            leftLaser.enabled = rightLaser.enabled = topLaser.enabled = botLaser.enabled = false;
            return;
        }

        RaycastHit2D hit; // point where laser hits

        if(left)
        {
            // find what it hits and draw it
            leftLaser.enabled = true;
            hit = Physics2D.Raycast(leftTrans.position, new Vector2(-1, 0));
            leftHit.position = hit.point;
            leftLaser.SetPosition(0, leftTrans.position);
            leftLaser.SetPosition(1, leftHit.position);
            // laser kills enemies and does constant damage to player, by a lot
            if(hit.transform.CompareTag("Player") && laserDamage > 0)
            {
                gameHandlerObj.TakeDamage(laserDamage);
            }
            else if(hit.transform.CompareTag("Enemy"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
        else
        {
            leftLaser.enabled = false;
        }


        if (right)
        {
            // find what it hits and draw it
            rightLaser.enabled = true;
            hit = Physics2D.Raycast(rightTrans.position, new Vector2(1, 0));
            Debug.DrawLine(rightTrans.position, hit.point);
            rightHit.position = hit.point;
            rightLaser.SetPosition(0, rightTrans.position);
            rightLaser.SetPosition(1, rightHit.position);
            // laser kills enemies and does constant damage to player, by a lot
            if (hit.transform.CompareTag("Player") && laserDamage > 0)
            {
                gameHandlerObj.TakeDamage(laserDamage);
            }
            else if (hit.transform.CompareTag("Enemy"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
        else
        {
            rightLaser.enabled = false;
        }

        if (up)
        {
            // find what it hits and draw it
            topLaser.enabled = true;
            hit = Physics2D.Raycast(topTrans.position, new Vector2(0, 1));
            Debug.DrawLine(topTrans.position, hit.point);
            topHit.position = hit.point;
            topLaser.SetPosition(0, topTrans.position);
            topLaser.SetPosition(1, topHit.position);
            // laser kills enemies and does constant damage to player, by a lot
            if (hit.transform.CompareTag("Player") && laserDamage > 0)
            {
                gameHandlerObj.TakeDamage(laserDamage);
            }
            else if (hit.transform.CompareTag("Enemy"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
        else
        {
            topLaser.enabled = false;
        }

        if (down)
        {
            // find what it hits and draw it
            botLaser.enabled = true;
            hit = Physics2D.Raycast(botTrans.position, new Vector2(0, -1));
            Debug.DrawLine(botTrans.position, hit.point);
            botHit.position = hit.point;
            botLaser.SetPosition(0, botTrans.position);
            botLaser.SetPosition(1, botHit.position);
            // laser kills enemies and does constant damage to player, by a lot
            if (hit.transform.CompareTag("Player") && laserDamage > 0)
            {
                gameHandlerObj.TakeDamage(laserDamage);
            }
            else if (hit.transform.CompareTag("Enemy"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
        else
        {
            botLaser.enabled = false;
        }
    }
}
