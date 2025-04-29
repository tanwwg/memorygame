using UnityEngine;

public class SetMaterialTexture : MonoBehaviour
{
    public Renderer target;
    public int materialIndex;
    public string textureName;

    public void Invoke(Texture2D texture)
    {
        target.materials[materialIndex].SetTexture(textureName, texture);
    }
}
