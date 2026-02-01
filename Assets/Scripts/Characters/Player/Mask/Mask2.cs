using UnityEngine;

public class Mask2 : BaseMask
{
    bool grabbing = false;
    float rayDistance;
    Transform detectorPivot;
    LayerMask allowedLayers;

    GameObject obj;

    public Mask2(float rayDistance, Transform detectorPivot, LayerMask allowedLayers)
    {
        this.rayDistance = rayDistance;
        this.detectorPivot = detectorPivot;
        this.allowedLayers = allowedLayers;
    }

    public override void MaskActionPerformed()
    {
        if (grabbing == false)
        {
            UnityEngine.Camera cam = UnityEngine.Camera.main;
            if (cam == null) return;

            Vector3 origin = cam.transform.position;
            Vector3 direction = cam.transform.forward;

            // DEBUG del rayo
            Debug.DrawRay(origin, direction * rayDistance, Color.red, 1f);

            if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance, allowedLayers))
            {
                obj = hit.collider.gameObject;
            }
            else
            {
                obj = null;
            }

            Grab();
        }
        else
            Drop();
    }

    public override void MaskDeselected()
    {
        if (grabbing == true)
            Drop();
    }

    void Grab()
    {
        if (obj == null) return;

        if (obj.TryGetComponent(out Rigidbody rb))
            rb.isKinematic = true;

        obj.transform.SetParent(detectorPivot);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        grabbing = true;
    }

    void Drop()
    {
        if (obj != null && obj.TryGetComponent(out Rigidbody rb))
            rb.isKinematic = false;

        if (obj != null)
            obj.transform.SetParent(null);

        grabbing = false;
        obj = null;
    }
}