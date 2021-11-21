using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject toFollow;
    [SerializeField] float smoothTime;
    [SerializeField] bool freezeX;
    [SerializeField] bool freezeY;

    [SerializeField] float minX;
    [SerializeField] float maxX;
    
    Vector3 _velocity = Vector3.zero;
    Camera _camera;

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        CenterCamera();
    }
    
    void CenterCamera()
    {
        Vector3 targetPos = toFollow.transform.position;
        Vector3 currentPos = transform.position;
        var x = freezeX ? currentPos.x : targetPos.x;
        var y = freezeY ? currentPos.y : targetPos.y;
        x = Mathf.Clamp(x, minX, maxX);
        var finalPos = new Vector3(x, y, currentPos.z);
        transform.position = Vector3.SmoothDamp(currentPos, finalPos, ref _velocity, smoothTime);
    }
}