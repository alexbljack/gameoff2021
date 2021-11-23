using UnityEngine;

public class CursorController : MonoBehaviour
{
    private SpriteRenderer cursor;
    void Start()
    {
        cursor = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
    }

    void Update()
    {
        var currentCursor = Input.mousePosition;
        cursor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(currentCursor.x, currentCursor.y, Camera.main.nearClipPlane));
    }
}
