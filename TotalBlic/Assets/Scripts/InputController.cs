using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    [SerializeField] private Camera cameraMain;
    [SerializeField] private float moveCameraSpeed;
    private Vector2 cameraDirection;
    [SerializeField] public Vector2 mausePosition { get; private set; }

    public void OnMoveCamera(InputAction.CallbackContext context)
    {
        cameraDirection = context.ReadValue<Vector2>();
    }

    public void OnMausePosition(InputAction.CallbackContext context)
    {
        mausePosition = cameraMain.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void OnClickRightButton(InputAction.CallbackContext context)
    {
        MyEventManager.SendSetTargetForSelectedunits(mausePosition);
    }

    private void OnClickLeftButton(InputAction.CallbackContext context)
    {

    }

    void Update()
    {
        cameraMain.transform.position += (Vector3)cameraDirection * moveCameraSpeed * Time.deltaTime;
    }
}
