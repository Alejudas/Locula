using UnityEngine;

public class JumpState : BaseCharacterState
{
    protected Rigidbody rb;
    protected float speed;
    protected float jumpForce;

    protected Vector3 moveDirection;
    protected Vector3 rotationDirection;

    public JumpState(GameObject playerObj, float speed, float jumpForce) : base(playerObj)
    {
        this.speed = speed;
        this.jumpForce = jumpForce;
    }

    public override void OnStateEnter()
    {
        rb = PlayerObj.GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        moveDirection = PlayerObj.transform.right * inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>().x + PlayerObj.transform.forward * inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>().y;

    }

    public override void OnStateFixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        rb.linearVelocity = Vector3.zero;
    }
}
