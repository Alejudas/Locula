using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class MaskController : MonoBehaviour
{
    [Inject] InputSystem inputSystem;

    List<BaseMask> masks = new();

    Mask1 mask1 = new();
    Mask2 mask2;
    Mask3 mask3 = new();

    int currentMaskIndex;

    [Header("Mask2 utilities")]
    [SerializeField] Vector3 detectorSize;
    [SerializeField] Transform detectorPivot;
    [SerializeField] LayerMask allowedLayers;

    private void Awake()
    {
        mask2 = new(detectorSize, detectorPivot, allowedLayers);

        masks.Add(mask1);
        masks.Add(mask2);
        masks.Add(mask3);

        masks[currentMaskIndex].MaskSelected();
    }

    private void OnEnable()
    {
        inputSystem.ControlsGetter().Player.Previous.started += ChangeToPreviousMask;
        inputSystem.ControlsGetter().Player.Next.started += ChangeToNextMask;
        inputSystem.ControlsGetter().Player.Attack.started += UseMask;
    }

    private void OnDisable()
    {
        inputSystem.ControlsGetter().Player.Previous.started -= ChangeToPreviousMask;
        inputSystem.ControlsGetter().Player.Next.started -= ChangeToNextMask;
        inputSystem.ControlsGetter().Player.Attack.started -= UseMask;
    }

    public void ChangeToNextMask(InputAction.CallbackContext context)
    {
        masks[currentMaskIndex].MaskDeselected();
        currentMaskIndex = currentMaskIndex++ < masks.Count - 1 ? currentMaskIndex++ : 0;
        masks[currentMaskIndex].MaskSelected();
    }

    public void ChangeToPreviousMask(InputAction.CallbackContext context)
    {
        masks[currentMaskIndex].MaskDeselected();
        currentMaskIndex = currentMaskIndex-- > 0 ? currentMaskIndex-- : masks.Count - 1;
        masks[currentMaskIndex].MaskSelected();
    }

    public void UseMask(InputAction.CallbackContext context) => masks[currentMaskIndex].MaskActionPerformed();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(detectorPivot.position, detectorSize);
    }
}
