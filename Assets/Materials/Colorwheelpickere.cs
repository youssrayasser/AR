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

        // Convert screen point to local UI space
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localCursor))
            return;

        Texture2D tex = rawImage.texture as Texture2D;

        // Normalize coordinates (0 to 1)
        Rect rect = rectTransform.rect;
        float x = (localCursor.x - rect.x) / rect.width;
        float y = (localCursor.y - rect.y) / rect.height;

        // Convert to pixel position
        int texX = Mathf.Clamp((int)(x * tex.width), 0, tex.width - 1);
        int texY = Mathf.Clamp((int)(y * tex.height), 0, tex.height - 1);

        Color selectedColor = tex.GetPixel(texX, texY);

        // Only apply if pixel is not fully transparent
        if (selectedColor.a > 0.1f)
        {
            carManager.ChangeCarColor(selectedColor);
        }
    }
}
