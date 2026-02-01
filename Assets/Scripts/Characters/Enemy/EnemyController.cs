using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float frenesiSpeed = 2;
    public float normalSpeed;
    public float detectionRange;


    [Header("Attack")]
    public float attackRange;
    public float attackDamage;

    [Header("References")]
    public Transform player;
    public NavMeshAgent agent;
    private BaseStates currentState;

    public Transform[] patrolPoints;
    [HideInInspector] public int patrolIndex = 0;

    public float coolTime;
    public float time;

    public bool stuned = false;
    public PlayerController pm;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        ChangeState(new FollowState(this));

        pm = FindAnyObjectByType<PlayerController>();

        normalSpeed = agent.speed;
    }

    private void Update()
    {
        if (player == null) return;
        currentState?.Update();

        if (Input.GetKeyDown(KeyCode.L))
        {
            stuned = true;
        }

        if (time < coolTime && stuned == true)
        {
            time += Time.deltaTime;
        }

        if (stuned == true)
        {
            ChangeState(new StunedState(this));
        }
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
