
using UnityEngine;
public class CameraController : IFixedExecute
{
    private Camera _camera;
    private Transform _target;

    private Vector3 _offset;
    private float _smoothSpeed;
 

    public CameraController( CameraData cameraData)
    {
        _camera = Camera.main;

        _offset = cameraData.Offset;
        _smoothSpeed = cameraData.SmoothedSpeed;
    }

    public void SetTarget(GameObject target)
    {
        _target = target.transform;
        _camera.transform.position = _target.position + _offset;
        _camera.transform.LookAt(_target.transform);

    }

    public void FixedExecute()
    {
        if (_target == null)
            return;
        Vector3 desiredPosition = _target.position + _offset;
        Vector3 smothedPosition =
            Vector3.Lerp(_camera.transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        _camera.transform.position = smothedPosition;


    }
}