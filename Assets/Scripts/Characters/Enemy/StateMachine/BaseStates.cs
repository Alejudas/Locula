using UnityEngine;

public abstract class BaseStates 
{
    protected EnemyController enemy;
    public BaseStates(EnemyController enemy) => this.enemy = enemy;

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
