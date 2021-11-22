using System;
using UnityEngine;

[Serializable]
public struct AxisBounds
{
    public float min;
    public float max;
}

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform toFollow;
    [SerializeField] float smoothTime;
    [SerializeField] bool freezeX;
    [SerializeField] bool freezeY;

    [SerializeField] AxisBounds xBounds;
    [SerializeField] AxisBounds yBounds;

    Vector3 _velocity = Vector3.zero;

    void LateUpdate()
    {
        if (toFollow != null)
        {
            CenterCamera();
        }
    }
    
    void CenterCamera()
    {
        Vector3 targetPos = toFollow.position;
        Vector3 currentPos = transform.position;
        var x = freezeX ? currentPos.x : targetPos.x;
        var y = freezeY ? currentPos.y : targetPos.y;
        x = Mathf.Clamp(x, xBounds.min, xBounds.max);
        y = Mathf.Clamp(y, yBounds.min, yBounds.max);
        var finalPos = new Vector3(x, y, currentPos.z);
        transform.position = Vector3.SmoothDamp(currentPos, finalPos, ref _velocity, smoothTime);
    }
}