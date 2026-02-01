using UnityEngine;

public class StunedState : BaseStates
{
    public StunedState(EnemyController enemy) : base(enemy)
    {
      
    }

    public override void Enter()
    { 
        enemy.agent.isStopped = true;
    }

   

    public override void Update()
    {
        if(enemy.time >= enemy.coolTime)
        {
            enemy.time = 0;
            enemy.stuned = false;
            enemy.ChangeState(new FollowState(enemy));
        }
    } 
    public override void Exit()
    {
        enemy.agent.isStopped = false;

    }
}
