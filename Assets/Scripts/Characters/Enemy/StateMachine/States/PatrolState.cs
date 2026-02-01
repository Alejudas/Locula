using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PatrolState : BaseStates
{
    

    public PatrolState(EnemyController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        GoToCurrentPoint();

    }
    public override void Update()
    {
        if (enemy.patrolPoints == null || enemy.patrolPoints.Length == 0)
            return;

        // Si ya llegó al punto
        if (!enemy.agent.pathPending && enemy.agent.remainingDistance <= 0.3f)
        {
           
                // Cambiar al siguiente punto
                enemy.patrolIndex++;
                if (enemy.patrolIndex >= enemy.patrolPoints.Length)
                    enemy.patrolIndex = 0;

                GoToCurrentPoint();
        }

        if (enemy.pm.isHidden == false)
        {
            enemy.ChangeState(new FollowState(enemy));
        }
    }
    public override void Exit()
    {
    }

    private void GoToCurrentPoint()
    {
        Transform target = enemy.patrolPoints[enemy.patrolIndex];
        
        enemy.agent.SetDestination(target.position);

        // Opcional: rotación natural hacia el punto
       
    }
}
