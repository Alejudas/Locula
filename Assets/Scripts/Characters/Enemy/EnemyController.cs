using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header ("Movement")]
    public float speed;
    public float currentSpeed;
    public float detectionRange;

    [Header("Attack")]
    public float attackRange;
    public float attackDamage;

    [Header ("References")]
    public Transform player;
    public NavMeshAgent agent;
    private BaseStates currentState;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        ChangeState(new PatrolState(this));
    }

    private void Update()
    {
        if(player == null) return;
        currentState?.Updtate();
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
