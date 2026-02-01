using Unity.VisualScripting;
using UnityEngine;

public class FrenesiState : BaseStates
{
    public FrenesiState(EnemyController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        enemy.agent.speed = enemy.frenesiSpeed;
    }
    public override void Update()
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);
        enemy.agent.SetDestination(enemy.player.position);

        if (distance > enemy.detectionRange)
        {
            enemy.ChangeState(new FollowState(enemy));
        }

        if (enemy.pm.isHidden == true)
        {
            enemy.ChangeState(new PatrolState(enemy));
        }
    }
    public override void Exit()
    {
        enemy.agent.speed = enemy.normalSpeed;
    }


}
