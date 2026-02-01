using UnityEngine;

public class Mask2 : BaseMask
{
    bool grabbing = false;
    Vector3 detectorSize;
    Transform detectorPivot;
    LayerMask allowedLayers;

    Collider obj;

    public Mask2(Vector3 detectorSize, Transform detectorPivot, LayerMask allowedLayers)
    {
        this.detectorSize = detectorSize;
        this.detectorPivot = detectorPivot;
        this.allowedLayers = allowedLayers;
    }

    public override void MaskActionPerformed()
    {
        Collider[] objs = Physics.OverlapBox(detectorPivot.position, detectorSize, detectorPivot.rotation, allowedLayers);
        obj = objs[0];

        grabbing = !grabbing;

        if (grabbing == false)
            Grab();
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
        if (obj.TryGetComponent(out Rigidbody rb))
            rb.isKinematic = true;

        obj.transform.parent = detectorPivot;
        obj.transform.position = Vector3.zero;
        obj.enabled = false;
        grabbing = true;
    }

    void Drop()
    {
        if (obj.TryGetComponent(out Rigidbody rb))
            rb.isKinematic = false;

        obj.transform.parent = null;
        obj.enabled = true;
        grabbing = false;

        obj = null;
    }
}
