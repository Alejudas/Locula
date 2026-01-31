using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header ("Movement")]
    public float frenesiSpeed = 2;
    public float normalSpeed;
    public float detectionRange;


    [Header("Attack")]
    public float attackRange;
    public float attackDamage;

    [Header ("References")]
    public Transform player;
    public NavMeshAgent agent;
    private BaseStates currentState;

    public PlayerMovement pm;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        ChangeState(new FollowState(this));

        pm = FindAnyObjectByType<PlayerMovement>();

        normalSpeed = agent.speed;
    }

    private void Update()
    {
        if(player == null) return;
        currentState?.Update();
    }
    public void ChangeState(BaseStates newstate)
    {
        currentState?.Exit();
        currentState = newstate;
        currentState.Enter();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
