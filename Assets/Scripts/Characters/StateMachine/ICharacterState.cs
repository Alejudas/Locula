using UnityEngine;

public interface ICharacterState
{
    public GameObject PlayerObj { get; }

    public float StateTimer { get; }

    public void OnStateEnter();
    public void OnStateUpdate();
    public void OnStateFixedUpdate();
    public void OnStateExit();
}
