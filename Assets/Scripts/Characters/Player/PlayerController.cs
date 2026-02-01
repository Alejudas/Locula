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
    CrouchingState crouch;
    TakeDamageState takeDamage;
    JumpState jump;
    RunState run;

    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    [Header("Crounch")]
    [SerializeField] float crouchSpeed;
    [SerializeField] float crouchHeight = 1f;
    [SerializeField] Transform cameraHolder;
    bool isHidden = false;

    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.3f;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    [Header("Jump")]
    [SerializeField] float jumpForce;

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
        crouch = container.Instantiate<CrouchingState>(new object[] { gameObject, crouchSpeed, crouchHeight, cameraHolder });
        takeDamage = container.Instantiate<TakeDamageState>(new object[] { gameObject });
        jump = container.Instantiate<JumpState>(new object[] { gameObject, crouchSpeed, jumpForce });
        run = container.Instantiate<RunState>(new object[] { gameObject, runSpeed });
    }

    Dictionary<ICharacterState, List<StateTransition>> Transitions()
    {
        return new Dictionary<ICharacterState, List<StateTransition>>
        {
            {
                idle,
                new()
                {
                    new(walk, ()=> inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>() != Vector2.zero && !inputSystem.ControlsGetter().Player.Run.IsPressed()),
                    new(run, ()=> inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>() != Vector2.zero && inputSystem.ControlsGetter().Player.Run.IsPressed()),
                    new(crouch, ()=> inputSystem.ControlsGetter().Player.Crouch.IsPressed()),
                    new(jump, ()=> inputSystem.ControlsGetter().Player.Jump.WasPressedThisFrame() && CheckGround())

                }
            },
            {
                walk,
                new()
                {
                    new(idle, ()=> inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>() == Vector2.zero),
                    new(crouch, ()=> inputSystem.ControlsGetter().Player.Crouch.IsPressed()),
                    new(jump, ()=> inputSystem.ControlsGetter().Player.Jump.WasPressedThisFrame() && isGrounded)
                }
            },
            {
                crouch,
                new()
                {
                    new(idle, ()=> inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>() == Vector2.zero && isHidden == false && !inputSystem.ControlsGetter().Player.Crouch.IsPressed()),
                    new(walk, ()=> inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>() != Vector2.zero && isHidden == false && !inputSystem.ControlsGetter().Player.Crouch.IsPressed() && !inputSystem.ControlsGetter().Player.Run.IsPressed()),
                    new(run, ()=> inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>() != Vector2.zero && isHidden == false && !inputSystem.ControlsGetter().Player.Crouch.IsPressed() && inputSystem.ControlsGetter().Player.Run.IsPressed()),
                    new(jump, ()=> inputSystem.ControlsGetter().Player.Jump.WasPressedThisFrame() && isGrounded)
                }
            },
            {
                jump,
                new()
                {
                    new(idle, ()=> inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>() == Vector2.zero && CheckGround() && rb.linearVelocity.y < 0.25f),
                    new(walk, ()=> inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>() != Vector2.zero && CheckGround() && rb.linearVelocity.y < 0.25f && !inputSystem.ControlsGetter().Player.Run.IsPressed()),
                    new(run, ()=> inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>() != Vector2.zero && CheckGround() && rb.linearVelocity.y < 0.25f && inputSystem.ControlsGetter().Player.Run.IsPressed()),
                    new(crouch, ()=> inputSystem.ControlsGetter().Player.Crouch.IsPressed() && CheckGround() && rb.linearVelocity.y < 0.25f),
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

    bool CheckGround()
    {
        return isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void OnTriggerEnter(Collider other)
    {
        isHidden = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isHidden = false;
    }
}
