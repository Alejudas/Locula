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
    [SerializeField] float rayDistance;
    [SerializeField] Transform detectorPivot;
    [SerializeField] LayerMask allowedLayers;

<<<<<<< HEAD
    [SerializeField] float timeMask3;
    float timerMask3 = 0;

    [SerializeField] GameObject deadPanel;

=======
    float timerMask3 = 0;

>>>>>>> develop
    private void Awake()
    {
        mask2 = new(rayDistance, detectorPivot, allowedLayers);

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

<<<<<<< HEAD
    private void Update()
    {
        if(mask3.Used == true)
        {
            timerMask3 += Time.deltaTime;
            if(timerMask3 >= timeMask3)
            {
                mask3.Used = false;
            }
        }
    }

=======
>>>>>>> develop
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

    private void OnCollisionEnter(Collision collision)
    {
<<<<<<< HEAD
        if(collision.gameObject.CompareTag("Enemy") )
        {
            if(currentMaskIndex == 2 && mask3.Used == false)
            {
                mask3.Used = true;
                
            }
            else
            {
                Time.timeScale = 0;
                deadPanel.SetActive(true);
            }
=======
        if(collision.gameObject.CompareTag("Enemy") && currentMaskIndex == 2 && mask3.Used == false)
        {

>>>>>>> develop
        }
    }

    public void UseMask(InputAction.CallbackContext context) => masks[currentMaskIndex].MaskActionPerformed();
}
