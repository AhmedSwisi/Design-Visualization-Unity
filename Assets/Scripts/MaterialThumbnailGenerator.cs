using UnityEngine;
using UnityEngine.UI;

public class MaterialThumbnailGenerator : MonoBehaviour
{
    public Material[] materials;
    public int thumbnailWidth = 128;
    public int thumbnailHeight = 128;
    public RawImage thumbnailImagePrefab; // Reference to the RawImage prefab in the UI Canvas

    private void Start()
    {
        GenerateMaterialThumbnails();
    }

    private void GenerateMaterialThumbnails()
    {
        foreach (Material material in materials)
        {
            Texture2D thumbnail = GenerateMaterialThumbnail(material);
            DisplayThumbnail(thumbnail);
        }
    }

    private Texture2D GenerateMaterialThumbnail(Material material)
    {
        RenderTexture thumbnailRenderTexture = new RenderTexture(thumbnailWidth, thumbnailHeight, 0);
        RenderTexture.active = thumbnailRenderTexture;

        // Create a temporary game object and assign the material to its renderer
        GameObject tempObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
        tempObject.GetComponent<Renderer>().material = material;

        // Render the material to the render texture
        Graphics.Blit(null, thumbnailRenderTexture, tempObject.GetComponent<Renderer>().material);

        // Create a new texture to store the thumbnail
        Texture2D thumbnailTexture = new Texture2D(thumbnailWidth, thumbnailHeight);
        thumbnailTexture.ReadPixels(new Rect(0, 0, thumbnailWidth, thumbnailHeight), 0, 0);
        thumbnailTexture.Apply();

        // Clean up temporary objects and render textures
        Destroy(tempObject);
        RenderTexture.active = null;
        thumbnailRenderTexture.Release();
        Destroy(thumbnailRenderTexture);

        return thumbnailTexture;
    }

    private void DisplayThumbnail(Texture2D thumbnailTexture)
    {
        // Instantiate a new RawImage from the prefab
        RawImage thumbnailImage = Instantiate(thumbnailImagePrefab, transform);

        // Set the thumbnail texture to the RawImage
        thumbnailImage.texture = thumbnailTexture;

        // Set the size of the thumbnail image (optional)
        thumbnailImage.rectTransform.sizeDelta = new Vector2(thumbnailWidth, thumbnailHeight);
    }
}

