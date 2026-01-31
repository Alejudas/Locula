using UnityEngine;
public class WalkState : BaseCharacterState
{
    protected Rigidbody rb;
    protected float speed;

    protected Vector3 moveDirection;
    protected Vector3 rotationDirection;

    public WalkState(GameObject playerObj, float speed) : base(playerObj)
    {
        this.speed = speed;
    }

    public override void OnStateEnter()
    {
        rb = PlayerObj.GetComponent<Rigidbody>();
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        moveDirection = PlayerObj.transform.right * inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>().x + PlayerObj.transform.forward * inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>().y;

        //moveDirection = inputSystem.ControlsGetter().Player.Move.ReadValue<Vector2>() * PlayerObj.transform.forward.normalized;
    }

    public override void OnStateFixedUpdate()
    {
        //rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.y * speed);
        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
    }
}
