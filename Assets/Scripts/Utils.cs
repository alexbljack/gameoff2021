
using System;
using UnityEngine;

public static class Utils
{
    public static bool RaycastBox(Vector2 direction, Bounds bounds, LayerMask layer, 
        float scaleX = 1f, float scaleY = 1f)
    {
        Vector2 boxSize = new Vector2(bounds.size.x * scaleX, bounds.size.y * scaleY);
        RaycastHit2D hit = Physics2D.BoxCast(bounds.center, boxSize, 0f, 
            direction, 0.1f, layer);
        return hit.collider != null;
    }
    
    public static Vector2 MousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    public static void FlipTransform(Transform transform, float input)
    {
        Vector3 scale = transform.localScale;
        var xScale = Math.Abs(input) > 0 ? Math.Abs(scale.x) * Mathf.Sign(input) : scale.x;
        transform.localScale = new Vector3(xScale, scale.y, scale.z);
    }
    
    public static void GizmosDrawCircle(Vector2 center, float radius, int sectors, Color color)
    {
        var angle = 0f;
        var step = 360f / sectors;
        Vector2 thisPoint = Vector2.zero;
        Vector2 lastPoint = Vector2.zero;

        Gizmos.color = color;
        
        for (var i = 0; i < sectors + 1; i++)
        {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            thisPoint.y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            Gizmos.DrawLine(center + lastPoint, center + thisPoint);
            
            lastPoint = thisPoint;
            angle += step;
        }
    }
}