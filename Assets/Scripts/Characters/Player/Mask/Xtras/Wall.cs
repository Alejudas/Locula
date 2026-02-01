using UnityEngine;

[DefaultExecutionOrder(-9999)]
public class Wall : MonoBehaviour
{
    [SerializeField] Material[] transparentMaterials;
    Material[] startedMaterials;

    MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        startedMaterials = meshRenderer.materials;
    }

    public void MakeTransparent()
    {
        meshRenderer.materials = transparentMaterials;
        if (gameObject.TryGetComponent(out Collider collider))
            collider.enabled = false;
    }

    public void MakeSolid()
    {
        meshRenderer.materials = startedMaterials;
        if (gameObject.TryGetComponent(out Collider collider))
            collider.enabled = true;
    }
}
