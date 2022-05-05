using UnityEngine;

namespace UI
{
  public class LookAtCamera : MonoBehaviour
  {
    private Transform _transform;
    private Transform _transformCamera;

    private void Awake()
    {
      _transform = transform;
      _transformCamera = Camera.main.transform;
    }

    private void LateUpdate()
    {
      Quaternion rotation = _transformCamera.rotation;
      _transform.LookAt(_transform.position + rotation * Vector3.back, rotation * Vector3.up);
    }
  }
}