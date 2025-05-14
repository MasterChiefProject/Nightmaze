using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonCursor : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("Drag your hand‐cursor texture here")]
    public Texture2D hoverCursor;

    private Texture2D defaultCursor = null;

    public Vector2 hotspot = Vector2.zero;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Switch to the hover cursor
        Cursor.SetCursor(hoverCursor, hotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Revert back to default
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
