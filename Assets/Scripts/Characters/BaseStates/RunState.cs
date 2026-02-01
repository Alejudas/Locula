using UnityEngine;

public class RunState : BaseCharacterState
{
    protected Rigidbody rb;
    protected float speed;

    protected Vector3 moveDirection;
    protected Vector3 rotationDirection;

    public RunState(GameObject playerObj, float speed) : base(playerObj)
    {
        this.speed = speed;
    }

    public override void OnStateEnter()
    {
        rb = PlayerObj.GetComponent<Rigidbody>();
        Debug.Log("Corriendose");
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
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
    }
}
