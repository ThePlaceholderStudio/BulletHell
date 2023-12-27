using UnityEngine.InputSystem;
using UnityEngine;

public class CharacterController : CharacterAimController, ICharacterController
{
    [SerializeField] float _Speed;
    [SerializeField] bool _IsPc;

    public Vector3 Movement;

    RaycastHit hit;

    Rigidbody rb;

    Character character;

    [Header("Dash Variables")]
    public Transform orientation;
    public Transform playerCam;
    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCdTimer;

    public bool resetVel = true;

    private Vector3 delayedForceToApply;

    public void OnMove(InputAction.CallbackContext context)
    {
        _Move = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        _MouseLook = context.ReadValue<Vector2>();
    }

    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        _JoystickLook = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            Dash();
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
    }

    private void FixedUpdate()
    {
        if (!PauseControl.isPaused)
        {
            hit = GetRaycastHitFromMouseLook();

            if (dashCdTimer > 0)
                dashCdTimer -= Time.deltaTime;

            if (_IsPc)
            {
                _RotationTarget = hit.point;

                MovePlayerWithAim();
            }
            else
            {
                if (_JoystickLook.x == 0 && _JoystickLook.y == 0)
                {
                    MovePlayer();
                }
                else
                {
                    MovePlayerWithAim();
                }
            }
        }
    }
    private void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = character.DashCoolDown.Value;

        isDashing = true;

        Movement = new Vector3(_Move.x, 0f, _Move.y).normalized;

        Transform forwardT;

        if (Movement != Vector3.zero)
        {
            forwardT = playerCam; /// where you're facing (no up or down)
        }
        else
        {
            forwardT = orientation;
        }

        Vector3 direction = GetDirection(forwardT);

        Vector3 forceToApply = direction * character.DashRange.Value;

        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        rb.drag = 0;
        Invoke(nameof(ResetDash), dashDuration);
    }

    private void DelayedDashForce()
    {
        if (resetVel)
            rb.velocity = Vector3.zero;

        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }
    private void ResetDash()
    {
        isDashing = false;
    }

    private void MovePlayer()
    {
        Movement = new(_Move.x, 0f, _Move.y);
        if (Movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Movement), 0.15f);
        }
        transform.Translate(character.MoveSpeed.Value * Time.deltaTime * Movement , Space.World);
    }

    public void MovePlayerWithAim()
    {
        Movement = new(_Move.x, 0f, _Move.y);
        if (_IsPc)
        {
            if (GetAimDirection(_IsPc) != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, GetRotation(), 0.15f);
            }
        }
        else
        {
            if (GetAimDirection(_IsPc) != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(GetAimDirection(_IsPc)), 0.15f);
            }
        }
        transform.Translate(character.MoveSpeed.Value * Time.deltaTime * Movement, Space.World);
    }
}
