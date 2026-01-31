using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Inject] DiContainer container;
    [Inject] InputSystem inputSystem;

    Rigidbody rb;

    StateMachine stateMachine;

    IdleState idle;
    WalkState walk;

    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float crouchSpeed;

    private void Awake()
    {
        rb = gameObject.TryGetComponent(out rb) ? rb : gameObject.AddComponent<Rigidbody>();
        rb.freezeRotation = true;

        stateMachine = gameObject.TryGetComponent(out stateMachine) ? stateMachine : gameObject.AddComponent<StateMachine>();

        CreateStates();

        stateMachine.SetState(idle);
        stateMachine.AddTransition(Transitions(), AnyTransitions());
    }

    void CreateStates()
    {
        idle = container.Instantiate<IdleState>(new object[] { gameObject });
        walk = container.Instantiate<WalkState>(new object[] { gameObject, walkSpeed });
    }

    Dictionary<ICharacterState, List<StateTransition>> Transitions()
    {
        return new Dictionary<ICharacterState, List<StateTransition>>
        {
            {
                idle,
                new()
                {
                    new(walk, ()=> inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>() != Vector2.zero),
                }
            },
            {
                walk,
                new()
                {
                    new(walk, ()=> inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>() == Vector2.zero),
                }
            }
        };

    }

    List<StateTransition> AnyTransitions()
    {
        return new List<StateTransition>
        {

        };
    }
}
