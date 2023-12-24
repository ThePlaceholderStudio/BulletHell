using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    public Transform _Target;
    public float _Damping = 0.3f;
    public Vector3 _Offset = Vector3.zero;
    private Vector3 _Velocity = Vector3.zero;

    void FixedUpdate()
    {
        if (_Target != null)
        {
            Vector3 targetPosition = _Target.position + _Offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _Velocity, _Damping);
        }
    }

}