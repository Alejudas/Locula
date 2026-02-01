using UnityEngine;

public class Mask1 : BaseMask
{
    public override void MaskSelected()
    {
        Wall[] walls = Transform.FindObjectsByType<Wall>(FindObjectsSortMode.None);
        
        foreach (Wall wall in walls)
        {
            wall.MakeTransparent();
        }
    }

    public override void MaskDeselected()
    {
        Wall[] walls = Transform.FindObjectsByType<Wall>(FindObjectsSortMode.None);

        foreach (Wall wall in walls)
        {
            wall.MakeSolid();
        }
    }
}
