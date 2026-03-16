using UnityEngine;

public class VisualEquipmentComponent : MonoBehaviour
{
    [SerializeField] MeshRenderer render;
    [SerializeField] MeshFilter filter;

    public void SetValues(Item _item)
    {
        filter.mesh = _item.mesh;
        render.materials = _item.materials.ToArray();
        transform.localScale = Vector3.one * _item.scale;
    }
}
