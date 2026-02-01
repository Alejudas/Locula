using UnityEngine;

public class CrouchingState : BaseCharacterState
{
    protected Rigidbody rb;
    protected float speed;

    protected Vector3 moveDirection;
    protected Vector3 rotationDirection;

    float crouchHeight;
    Transform cameraHolder;

    public CrouchingState(GameObject playerObj, float speed, float crouchHeight, Transform cameraHolder) : base(playerObj)
    {
        this.speed = speed;
        this.crouchHeight = crouchHeight;
        this.cameraHolder = cameraHolder;
    }

    public override void OnStateEnter()
    {
        rb = PlayerObj.GetComponent<Rigidbody>();

        //PlayerObj.transform.localScale = new Vector3(1, crouchHeight, 1);
        //PlayerObj.transform.position = new Vector3(PlayerObj.transform.position.x, PlayerObj.transform.position.y - crouchHeight, PlayerObj.transform.position.z);
        cameraHolder.localPosition = new Vector3(0, 0.5f, 0);
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();

        if(PlayerObj.transform.localScale.y > crouchHeight)
        {
            PlayerObj.transform.localScale = new Vector3(1, Mathf.Lerp(PlayerObj.transform.localScale.y, crouchHeight - 0.1f, 0.1f), 1);
            PlayerObj.transform.position = new Vector3(PlayerObj.transform.position.x, Mathf.Lerp(PlayerObj.transform.position.y, PlayerObj.transform.position.y - crouchHeight, 0.1f), PlayerObj.transform.position.z);
        }

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
        
        PlayerObj.transform.localScale = Vector3.one;
        cameraHolder.localPosition = new Vector3(0, 0.9f, 0);
    }
}
