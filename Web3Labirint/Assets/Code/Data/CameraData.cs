using UnityEngine;

[CreateAssetMenu(fileName = "CameraData", menuName = "Data/Game/CameraData", order = 0)]
public class CameraData : ScriptableObject
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothedSpeed;

    public Vector3 Offset => offset;

    public float SmoothedSpeed => smoothedSpeed;
}