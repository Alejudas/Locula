using UnityEngine;

public abstract class BaseMask
{
    public virtual void MaskSelected() { }
    public virtual void MaskActionPerformed() { }
    public virtual void MaskDeselected() { }
}
