using UnityEngine;

public abstract class CharacterAimController : MonoBehaviour
{
    protected Vector3 _RotationTarget;
    protected Vector2 _Move, _MouseLook, _JoystickLook;

    public RaycastHit GetRaycastHitFromMouseLook()
    {
        Ray ray = Camera.main.ScreenPointToRay(_MouseLook);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit;
        }

        return default; // Return a default RaycastHit if no hit was found
    }

    public Quaternion GetRotation()
    {
        var _LookPos = _RotationTarget - transform.position;
        _LookPos.y = 0;
        return Quaternion.LookRotation(_LookPos);
    }

    public Vector3 GetAimDirection(bool isPc)
    {
        if (isPc)
        {
            var _LookPos = _RotationTarget - transform.position;
            _LookPos.y = 0;

            return new(_RotationTarget.x, 0f, _RotationTarget.z);
        }

        return new Vector3(_JoystickLook.x, 0f, _JoystickLook.y);
    }
}


