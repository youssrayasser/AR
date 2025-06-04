using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorPicker : MonoBehaviour, IPointerDownHandler
{
    public CarSelectionManager carManager;
    private RawImage rawImage;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransform rectTransform = rawImage.rectTransform;
        Vector2 localCursor;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localCursor))
            return;

        Texture2D tex = rawImage.texture as Texture2D;
        if (tex == null)
        {
            Debug.LogWarning("Texture is not Texture2D or not assigned.");
            return;
        }

        Rect rect = rectTransform.rect;
        float x = (localCursor.x - rect.x) / rect.width;
        float y = (localCursor.y - rect.y) / rect.height;

        int texX = Mathf.Clamp((int)(x * tex.width), 0, tex.width - 1);
        int texY = Mathf.Clamp((int)(y * tex.height), 0, tex.height - 1);

        Color selectedColor = tex.GetPixel(texX, texY);

        Debug.Log($"Picked color at ({texX},{texY}): {selectedColor}");

        if (selectedColor.a > 0.1f)
        {
            carManager.ChangeCarColor(selectedColor);
        }
        else
        {
            Debug.Log("Selected color is mostly transparent, ignoring.");
        }
    }
}
