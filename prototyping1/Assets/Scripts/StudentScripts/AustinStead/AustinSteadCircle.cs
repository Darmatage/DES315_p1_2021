using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AustinSteadCircle : MonoBehaviour
{
    public int vertexCount = 40;
    public float lineWidth = .2f;
    public float radius;

    public float z = 0;

    private LineRenderer lineRenderer;

    public Color circleColor;


    public void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DrawCircle();

    }

    public void Update()
    {
        DrawCircle();
    }
    private void DrawCircle()
    {

        lineRenderer.widthMultiplier = lineWidth;

        float deltaTheta = (2f * Mathf.PI) / (float)vertexCount;
        float theta = 0;

        lineRenderer.positionCount = vertexCount;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta) + transform.position.x, radius * Mathf.Sin(theta) + transform.position.y, z);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }





#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        float deltaTheta = (2f * Mathf.PI) / (float)(vertexCount);
        float theta = 0;

        Vector2 oldPos = Vector2.zero;
        for (int i = 0; i < vertexCount + 1; i++)
        {
            Vector2 pos = new Vector2(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta));
            Gizmos.DrawLine(oldPos, (Vector2)transform.position + pos);
            oldPos = (Vector2)transform.position + pos;

            theta += deltaTheta;
        }
    }

#endif


}
