using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    ICharacterState _currentState;
    public ICharacterState CurrentState => _currentState;

    Dictionary<ICharacterState, List<StateTransition>> transitions = new();
    List<StateTransition> anyTransitions = new();

    public void AddTransition(Dictionary<ICharacterState, List<StateTransition>> transitions, List<StateTransition> anyTransitions = null)
    {
        this.transitions = transitions;
        this.anyTransitions = anyTransitions;
    }

    public void SetState(ICharacterState newState)
    {
        if (_currentState != newState)
        {
            _currentState?.OnStateExit();
            _currentState = newState;
            _currentState.OnStateEnter();
        }
    }

    public void CheckAnyTransitions()
    {
        if (Time.timeScale == 0) return;

        foreach (var transition in anyTransitions)
        {
            if (_currentState == null)
                return;

            if (transition.Condition())
            {
                SetState(transition.NewState);
                return;
            }
        }
    }

    public void CheckStateTransitions()
    {
        if (Time.timeScale == 0) return;

        if (_currentState == null)
            return;

        if (!transitions.ContainsKey(_currentState))
            return;

        foreach (var transition in transitions[_currentState])
        {
            if (transition.Condition())
            {
                SetState(transition.NewState);
                return;
            }
        }
    }

    private void Update()
    {
        CheckStateTransitions();
        CheckAnyTransitions();
        _currentState.OnStateUpdate();
    }

    private void FixedUpdate()
    {
        _currentState.OnStateFixedUpdate();
    }
}