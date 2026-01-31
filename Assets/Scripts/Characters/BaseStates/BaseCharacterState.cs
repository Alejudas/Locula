using UnityEngine;
using Zenject;

public abstract class BaseCharacterState : ICharacterState
{
    [Inject] protected InputSystem inputSystem;
    protected AudioSource localSFXAudioSourceScaled;

    readonly GameObject _playerObj;
    float _stateTimer;

    public GameObject PlayerObj => _playerObj;
    public float StateTimer => _stateTimer;

    public BaseCharacterState(GameObject _playerObj)
    {
        this._playerObj = _playerObj;
    }

    public virtual void OnStateEnter()
    {

    }

    public virtual void OnStateUpdate()
    {
        AddToTimer(Time.deltaTime);
    }

    public virtual void OnStateFixedUpdate()
    {

    }

    public virtual void OnStateExit()
    {
        _stateTimer = 0;
    }

    public void AddToTimer(float valueToAdd) => _stateTimer += valueToAdd;

    public void SetTimerValue(float value) => _stateTimer += value;
}
