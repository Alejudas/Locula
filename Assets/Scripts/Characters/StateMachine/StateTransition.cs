using System;

public class StateTransition
{
    ICharacterState _newState;
    Func<bool> _conditions;

    public ICharacterState NewState => _newState;
    public Func<bool> Condition => _conditions;

    public StateTransition(ICharacterState newState, Func<bool> conditions)
    {
        _newState = newState;
        _conditions = conditions;
    }
}
