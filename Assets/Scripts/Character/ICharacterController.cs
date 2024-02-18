using UnityEngine.InputSystem;

public interface ICharacterController
{
    public void OnMove(InputAction.CallbackContext context);
    public void OnMouseLook(InputAction.CallbackContext context);
    public void OnJoystickLook(InputAction.CallbackContext context);
}
