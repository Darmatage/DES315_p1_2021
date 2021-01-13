using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrollingGuardRS : MonoBehaviour
{
	public bool DebugDraw = true;                           // Display debug drawing
	[Range(1.0f, 10.0f)] public float Speed;                // Movement speed
	[Range(0.1f, 5.0f)] public float CornerningTime = 1.0f; // Time it takes to rotate around corners
	private float corneringTimer;
	
	[Range(1.0f, 20.0f)] public float ViewDistance;    // Length of vision cone
	[Range(1.0f, 10.0f)] public float VisionConeWidth; // Width of vision cone
	[Range(1, 10)]       public int RaysPerFrame;      // The number of vision rays cast per frame
	public LayerMask Visible; // All layers that are visible to this enemy
	
	// Counterclockwise order of nodes along the path this monster will follow
	public Vector2Int[] Path;
	private List<Vector3> path;
	private float PathingEpsilon = 0.01f; // Distance at which a new node is chosen
	private Vector3 target;
	private Vector3 oldTarget;
	private int targetIndex = 0;

	// Map containing nodes for monster pathing nodes
	public GridLayout NodeMap;

	private LineRenderer visionCone;
	private Transform player;

	private void Start()
	{
		visionCone = GetComponent<LineRenderer>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		// Initialize the path
		path = new List<Vector3>();
		foreach (Vector2Int node in Path)
			path.Add(GetNodePosition(node));
		target = path[0];
		
		// Initialize cornering timer
		corneringTimer = CornerningTime;
	}

	private void Update()
	{
		// Don't update if the player is dead
		if (player == null)
			return;
		
		// Rotate toward next node
		if (corneringTimer < CornerningTime)
		{
			corneringTimer += Time.deltaTime;

			// Rotate toward new target
			target = Vector2.Lerp(oldTarget, 
														path[(targetIndex + 1) % Path.Length], 
														corneringTimer / CornerningTime);
			
			// Update target
			if (corneringTimer >= CornerningTime)
			{
				targetIndex = (targetIndex + 1) % Path.Length;
				target = path[targetIndex];
			}
		}

		else
		{
			// Start rotating toward new target
			if (Vector3.Distance(target, transform.position) < PathingEpsilon)
			{
				corneringTimer = 0.0f;
				oldTarget = transform.position + (target - transform.position).normalized * ViewDistance;
			}

			// Move toward target
			transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
		}

		// Update vision cone
		visionCone.endWidth = VisionConeWidth;
		visionCone.SetPosition(0, transform.position);
		visionCone.SetPosition(1, transform.position + (target - transform.position).normalized * ViewDistance);

		// Cast rays at player
		for (int _ = 0; _ < RaysPerFrame; _++)
		{
			Vector3 ray = GetRandomVisionRay();
			// if (DebugDraw)
			// 	DrawRaycast(ray);
			
			RaycastHit2D hit = Physics2D.Raycast(transform.position, ray, ViewDistance, Visible);
			if (hit.collider != null && hit.transform.CompareTag("Player"))
			{
				//Debug.Log("Player caught!");
				Debug.Log("Object hit: " + hit.transform.name);
				break;
			}
		}

		// Draw all debugging data
		if (DebugDraw)
			DrawCurrentPath();
	}

	// Get the world coordinates of the center of the given 2D grid cell
	private Vector3 GetNodePosition(Vector2Int node)
	{
		Vector3 offset = NodeMap.cellSize / 2.0f;
		offset.z = 0.0f;
		return NodeMap.CellToWorld(new Vector3Int(node.x, node.y, 0)) + offset;
	}

	// Returns a random vector in the vision cone of this enemy
	private Vector3 GetRandomVisionRay()
	{
		Vector3 currentPath = (target - transform.position).normalized * ViewDistance;
		float angle = Mathf.Rad2Deg * Mathf.Atan2(VisionConeWidth / 2.0f, ViewDistance);
		return Quaternion.AngleAxis(Random.Range(-angle, angle), Vector3.forward) * currentPath;
	}
	
	// Draw a debug line from the enemy to the current target node
	private void DrawCurrentPath()
	{
		Debug.DrawLine(transform.position, target, Color.magenta);
	}

	// Draw a debug line for the given raycast
	private void DrawRaycast(Vector3 ray)
	{
		Debug.DrawLine(transform.position, transform.position + ray, Color.green, 0.1f);
	}
}