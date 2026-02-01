
using UnityEngine;

public class FollowState : BaseStates
{
    public FollowState(EnemyController enemy) : base(enemy) { }
    public override void Enter()
    {

    }

    public override void Update()
    {
        if (enemy.player == null) return;

        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);


        if (enemy.pm.isHidden == true)
        {
            enemy.ChangeState(new PatrolState(enemy));
        }
        else
        {
            enemy.agent.SetDestination(enemy.player.position);
        }

        if (distance <= enemy.detectionRange)
        {
            enemy.ChangeState(new FrenesiState(enemy));
        }

    }
    public override void Exit()
    {
    }
}
