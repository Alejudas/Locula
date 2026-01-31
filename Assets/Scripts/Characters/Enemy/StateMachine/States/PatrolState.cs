using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseStates
{
    public Transform[] patrolPoints;

    public PatrolState(EnemyController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        enemy.agent.isStopped = true;

    }
    public override void Update()
    {
        if (patrolPoints == null || patrolPoints.Length < 0)
        {

        }
    }
    public override void Exit()
    {
    }

    
}
