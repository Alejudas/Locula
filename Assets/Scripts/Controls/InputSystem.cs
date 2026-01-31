using UnityEngine;

public class InputSystem : MonoBehaviour
{
    InputSystem_Actions _controls;

    private void Awake()
    {
        _controls = new();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    public InputSystem_Actions ControlsGetter()
    {
        return _controls;
    }
}