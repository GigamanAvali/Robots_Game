using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : BaseEntity
{
	private NavMeshAgent agent;
	//private Transform player;
	[SerializeField] private Transform billboardHealthBar;

	//Патруль
	private Vector3 walkPoint;
	private bool walkPointSet = false;
	[SerializeField] private float walkPointRange = 10;
	private float moveTime = 0;

	[SerializeField] private LayerMask groundMask;
	//Стейты поведения
	private bool playerInSightRange, playerInAttackRange = false;
	[SerializeField] private float sightRange = 15;
	[SerializeField] private float attackRange = 7;
	[SerializeField] private LayerMask playerMask;


	public override void Awake()
	{
		base.Awake();
		agent = this.GetComponent<NavMeshAgent>();
		//player = GameObject.Find("ThirdPersonPlayer").transform;
	}

	void Update()
	{
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
		playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
		Patrolling();
		
		//if (!playerInSightRange && !playerInAttackRange) Patrolling();
		//if (playerInSightRange && !playerInAttackRange) ChasePlayer();
		//if (playerInSightRange && playerInAttackRange) Attack();
		//if (AppData.Player)
		//{
		//	//billboardHealthBar?.LookAt(AppData.Player);
		//	//billboardHealthBar.GetComponent<BillboardHealthBar>().UpdateHP(Health, MaxHealth);
		//}
	}

	private void Patrolling()
	{
		if (!walkPointSet)
			SearchWalkPoint();
		else
		{
			moveTime += Time.deltaTime;
			//Debug.Log($"moveTime = {moveTime}");
			agent.SetDestination(walkPoint);
			Vector3 reachDistanceVector = transform.position - walkPoint;
			reachDistanceVector.y = 0;
			//Debug.Log($"reachDistance = {reachDistanceVector.magnitude}");
			//if (reachDistanceVector.magnitude < 1)
			//	walkPointSet = false;
			if (reachDistanceVector.magnitude < 1 || moveTime > 5)
			{
				walkPointSet = false;
				moveTime = 0;
			}

		}
	}

	private void SearchWalkPoint()
	{
		float randomX = Random.Range(-walkPointRange, walkPointRange);
		float randomZ = Random.Range(-walkPointRange, walkPointRange);
		Vector3 curPosition = transform.position;
		walkPoint = new Vector3(curPosition.x + randomX, curPosition.y, curPosition.z + randomZ);
		//walkPointSet = true;
		//Debug.Log($"walkPoint = {walkPoint}");
		bool rayUp = Physics.Raycast(walkPoint, transform.up, out RaycastHit upHitInfo, 100f, groundMask);
		bool rayDown = rayDown = Physics.Raycast(walkPoint, -transform.up, out RaycastHit downHitInfo, 100f, groundMask);

		if (rayUp)
			walkPoint = upHitInfo.point;
		else if (rayDown)
			walkPoint = downHitInfo.point;

		if (rayUp || rayDown)
			walkPointSet = true;
	}
	private void ChasePlayer()
	{
		if (AppData.Player == null)
			return;
		agent.SetDestination(AppData.Player.position);
		transform.LookAt(AppData.Player);
	}
	public override void Attack()
	{
		if (AppData.Player == null)
			return;
		agent.SetDestination(transform.position);
		transform.LookAt(AppData.Player);

		base.Attack();
	}

}