using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController_Mielke : MonoBehaviour
{
    public bool activated;
    public bool left, right, up, down;

    public LineRenderer leftLaser, rightLaser, topLaser, botLaser;
    public Transform leftHit, rightHit, topHit, botHit;
    public Transform leftTrans, rightTrans, topTrans, botTrans;
    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        leftLaser.enabled = rightLaser.enabled = topLaser.enabled = botLaser.enabled = true;
        leftLaser.useWorldSpace = rightLaser.useWorldSpace = topLaser.useWorldSpace = botLaser.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(leftTrans.position, new Vector2(-1, 0));
        Debug.DrawLine(leftTrans.position, hit.point);
        leftHit.position = hit.point;
        leftLaser.SetPosition(0, leftTrans.position);
        leftLaser.SetPosition(1, leftHit.position);

        hit = Physics2D.Raycast(rightTrans.position, new Vector2(1, 0));
        Debug.DrawLine(rightTrans.position, hit.point);
        rightHit.position = hit.point;
        rightLaser.SetPosition(0, rightTrans.position);
        rightLaser.SetPosition(1, rightHit.position);

        hit = Physics2D.Raycast(topTrans.position, new Vector2(0, 1));
        Debug.DrawLine(topTrans.position, hit.point);
        topHit.position = hit.point;
        topLaser.SetPosition(0, topTrans.position);
        topLaser.SetPosition(1, topHit.position);

        hit = Physics2D.Raycast(botTrans.position, new Vector2(0, -1));
        Debug.DrawLine(botTrans.position, hit.point);
        botHit.position = hit.point;
        botLaser.SetPosition(0, botTrans.position);
        botLaser.SetPosition(1, botHit.position);

    }
}
