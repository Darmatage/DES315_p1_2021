using System.Collections.Generic;
using UnityEngine;

public class PatrolRS : MonoBehaviour
{
	public bool DebugDraw = true; // Display debug drawing
	[Range(1, 100)] public int ChompDamage = 100;     // Damage done by chomping
	[Range(1.0f, 10.0f)] public float TargetingSpeed; // Movement speed while patroling
	[Range(1.0f, 10.0f)] public float WalkingSpeed;   // Movement speed while patroling
	[Range(1.0f, 10.0f)] public float ChasingSpeed;   // Movement speed while chasing the player
	[Range(0.1f, 5.0f)] public float CornerningTime = 1.0f; // Time it takes to rotate around corners
	private float corneringTimer;

	[Range(1.0f, 20.0f)] public float ViewDistance; // Length of vision cone
	private float currentViewDistance;
	[Range(1.0f, 10.0f)] public float VisionConeWidth; // Width of vision cone
	public Gradient VisionColorNormal; // Color of the vision cone when normally pathing
	public Gradient VisionColorSpotted; // Color of the vision cone when the player was spotted

	[Range(1, 10)] public int RaysPerFrame; // The number of vision rays cast per frame
	public LayerMask VisionLayers; // All layers that are visible to this enemy

	// Aggro switch
	private bool detectedPlayer = false;

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
	private Animator anim;
	public Animator Anim => anim;
	public SpriteRenderer sensingColor;
	public SpriteRenderer skullSprite;
	public SpriteRenderer shadowSprite;

	// Alert sound effect player
	public GameObject AlertHandlerPrefab;
	
	// Alert pop up image
	public GameObject AlertPopUpPrefab;

	// Chomp script reference
	private PatrolChompRS chompScript;
	
	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;

		// Initialize the path
		path = new List<Vector3>();
		foreach (Vector2Int node in Path)
			path.Add(GetNodePosition(node));
		target = path[0];

		// Initialize cornering timer
		corneringTimer = CornerningTime;

		// Initialize vision cone
		visionCone = GetComponent<LineRenderer>();
		visionCone.colorGradient = VisionColorNormal;
		currentViewDistance = ViewDistance;
		
		// Grab animator component
		anim = GetComponentInChildren<Animator>();
		
		// Grab chomp script
		chompScript = GetComponentInChildren<PatrolChompRS>();
	}

	private void Update()
	{
		// Don't update if the player is dead
		if (player == null)
			return;
		
		// Force chase
		if (path.Count == 1)
			detectedPlayer = true;
		
		// Update all vision cone data
		UpdateVisionCone();
		
		// Movement logic
		if (detectedPlayer)
			ChasePlayer();
		else
		{
			LookForPlayer();
			Patrol();
		}
		
		// Draw all debugging data
		if (DebugDraw)
			DrawCurrentPath();
	}

	#region Behavior

	// Cast out rays to search for the player
	private void LookForPlayer()
	{
		for (int _ = 0; _ < RaysPerFrame; _++)
		{
			Vector3 ray = GetRandomVisionRay();
			if (DebugDraw)
				DrawRaycast(ray);

			RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, ray, currentViewDistance, VisionLayers);
			foreach (RaycastHit2D currentHit in hits)
			{
				if (currentHit.collider.isTrigger)
					continue;
				
				if (currentHit.transform.CompareTag("Player"))
				{
					detectedPlayer = true;
					anim.Play("monsterSkull_walk");
					GameObject popUp = Instantiate(AlertPopUpPrefab, transform);
					popUp.transform.position += transform.lossyScale.y * Vector3.up;
					Instantiate(AlertHandlerPrefab, transform.position, Quaternion.identity);
				}
				
				break;
			}
		}
	}
	
	// Cycle through patrol path nodes
	private void Patrol()
	{
		// Turning around a corner
		if (corneringTimer < CornerningTime)
		{
			corneringTimer += Time.deltaTime;

			// Todo: Deprecated: couldn't solve bug where it instantly reaches the target, which hurt gameplay
			// Rotate toward new target
			target = Vector3.Slerp(oldTarget, path[(targetIndex + 1) % Path.Length], Mathf.Pow(corneringTimer / CornerningTime, 2.0f));

			// Update target
			if (corneringTimer >= CornerningTime)
			{
				targetIndex = (targetIndex + 1) % Path.Length;
				target = path[targetIndex];
			}
		}

		// Walking to a node
		else
		{
			// Start rotating toward new target
			if (Vector3.Distance(target, transform.position) < PathingEpsilon * 2.0f)
			{
				corneringTimer = 0.0f;
				oldTarget = transform.position + (target - transform.position).normalized * ViewDistance;
				target = oldTarget;
			}

			// Move toward target
			else
			{
				target = path[targetIndex];
				transform.position = Vector2.MoveTowards(transform.position, target, WalkingSpeed * Time.deltaTime);
			}
		}
	}

	// Chase the player
	private void ChasePlayer()
	{
		if (Vector3.Distance(target, player.transform.position) > 0.1f)
			target = Vector3.Lerp(target, player.transform.position, TargetingSpeed * Time.deltaTime);
		else
			target = player.transform.position;
		transform.position = Vector2.MoveTowards(transform.position, target, ChasingSpeed * Time.deltaTime);
	}

	// Update all vision cone data
	private void UpdateVisionCone()
	{
		if (!chompScript.Chomped)
		{
			if (visionCone.GetPosition(1).x > visionCone.GetPosition(0).x)
			{
				skullSprite.flipX = true;
				shadowSprite.flipX = true;
			}
			else
			{
				skullSprite.flipX = false;
				shadowSprite.flipX = false;
			}
		}

		float highLerpScale = 0.3f;
		float lowLerpScale  = 0.05f;
		
		// Update cone and ring color depending on if the player was seen
		if (detectedPlayer)
		{
			visionCone.colorGradient = VisionColorSpotted;
			Color startConeColor = visionCone.startColor;
			startConeColor.a = highLerpScale * (1.0f + 0.5f * Mathf.Sin(20.0f * Time.time));
			visionCone.startColor = startConeColor;

			Color ringColor = VisionColorSpotted.colorKeys[0].color;
			ringColor.a = lowLerpScale * (1.0f + 0.5f * Mathf.Sin(20.0f * Time.time));
			sensingColor.color = ringColor;
		}
		else
		{
			visionCone.colorGradient = VisionColorNormal;
			Color startConeColor = visionCone.startColor;
			startConeColor.a = highLerpScale * (1.0f + Mathf.Sin(5.0f * Time.time));
			visionCone.startColor = startConeColor;

			Color ringColor = VisionColorNormal.colorKeys[0].color;
			ringColor.a = lowLerpScale * (1.0f + Mathf.Sin(5.0f * Time.time));
			sensingColor.color = ringColor;
		}

		// Update cone endpoints
		visionCone.endWidth = VisionConeWidth;
		visionCone.SetPosition(0, transform.position);
		currentViewDistance = ViewDistance;
		if (corneringTimer < CornerningTime)
			currentViewDistance = Mathf.Lerp(ViewDistance / 2.0f, ViewDistance, Mathf.Pow(corneringTimer / CornerningTime, 2.0f));
		visionCone.SetPosition(1, transform.position + (target - transform.position).normalized * currentViewDistance);
	}

	#endregion

	#region Helper Functions

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
		Vector3 currentPath = (target - transform.position).normalized * currentViewDistance;
		float angle = Mathf.Rad2Deg * Mathf.Atan2(VisionConeWidth / 2.0f, currentViewDistance);
		return Quaternion.AngleAxis(Random.Range(-angle, angle), Vector3.forward) * currentPath;
	}

	#endregion

	#region Debug

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

	#endregion
}