using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrollingGuardRS : MonoBehaviour
{
	public bool DebugDraw = true;                      // Display debug drawing
	public float Speed = 2f;                           // Movement speed
	[Range(1.0f, 20.0f)] public float ViewDistance;    // Length of vision cone
	[Range(1.0f, 10.0f)] public float VisionConeWidth; // Width of vision cone

	// Counterclockwise order of nodes along the path this monster will follow
	public Vector2Int[] Path;
	private List<Vector3> path;
	private float PathingEpsilon = 0.01f; // Distance at which a new node is chosen
	private Vector3 target;
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
	}

	private void Update()
	{
		// Don't update if the player is dead
		if (player == null)
			return;

		// Draw all debugging data
		if (DebugDraw)
		{
			DrawCurrentPath();
			DrawRaycasts();
		}

		// Update target
		if (Vector3.Distance(target, transform.position) < PathingEpsilon)
		{
			targetIndex = (targetIndex + 1) % Path.Length;
			target = path[targetIndex];
		}
		
		// Move toward target
		transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);

		visionCone.endWidth = VisionConeWidth;
		visionCone.SetPosition(0, transform.position);
		visionCone.SetPosition(1, transform.position + (target - transform.position).normalized * ViewDistance);
	}

	// Get the world coordinates of the center of the given 2D grid cell
	private Vector3 GetNodePosition(Vector2Int node)
	{
		Vector3 offset = NodeMap.cellSize / 2.0f;
		offset.z = 0.0f;
		return NodeMap.CellToWorld(new Vector3Int(node.x, node.y, 0)) + offset;
	}

	// Draw a debug line from the enemy to the current target node
	private void DrawCurrentPath()
	{
		Vector3 currentPath = (target - transform.position).normalized * PathingEpsilon;
		Debug.DrawLine(transform.position, transform.position + currentPath, Color.magenta);
	}

	// Draw debug lines for each raycast
	private void DrawRaycasts()
	{
		Vector3 currentPath = (target - transform.position).normalized * ViewDistance;
		float angle = Mathf.Rad2Deg * Mathf.Atan2(VisionConeWidth / 2.0f, ViewDistance);
		Vector3 rayCast = Quaternion.AngleAxis(Random.Range(-angle, angle), Vector3.forward) * currentPath;
		Debug.DrawLine(transform.position, transform.position + rayCast, Color.green, 0.1f);
	}
}