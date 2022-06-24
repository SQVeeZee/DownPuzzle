using UnityEngine;

public interface ICameraItem
{
    Camera Camera { get; }
    ECameraType CameraType { get; }
    Vector2 ConvertScreenToWorldPoint(Vector2 screenPoint);
}
