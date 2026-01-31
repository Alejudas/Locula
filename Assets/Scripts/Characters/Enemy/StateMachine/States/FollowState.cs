using UnityEngine;

public class FollowState : BaseStates
{
    public FollowState(EnemyController enemy) : base(enemy) { }
    public override void Enter()
    {

    }

    public override void Updtate()
    {
        if (enemy.player == null) return;

        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distance <= enemy.detectionRange)
        {
            enemy.agent.SetDestination(enemy.player.position);
        }
        else
        {
            enemy.agent.ResetPath();
            enemy.ChangeState(new PatrolState(enemy));
        }

    }
    public override void Exit()
    {
    }
}
