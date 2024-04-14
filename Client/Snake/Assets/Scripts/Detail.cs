using UnityEngine;

public class Detail : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    public void SetColor(float hue)
    {
        Color.RGBToHSV(_mesh.material.color, out float h,out float s, out float v);
        Color newColor = Color.HSVToRGB(hue, s, v);
        _mesh.material.color = newColor;
    }
}
