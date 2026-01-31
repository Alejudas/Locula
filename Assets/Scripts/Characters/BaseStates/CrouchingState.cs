using UnityEngine;
using UnityEngine.Rendering;

public class CrouchingState : BaseCharacterState
{
    float speed;

    public CrouchingState(GameObject playerObj, float speed) : base(playerObj)
    {
        this.speed = speed;
    }


}
