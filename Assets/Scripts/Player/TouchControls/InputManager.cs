using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{

    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event StartTouch OnEndTouch;
    #endregion

    private TouchControls touchControls;
    // private Camera mainCamera;

    private void Awake()
    {
        touchControls = new TouchControls();
        // mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        touchControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    void StartTouchPrimary( InputAction.CallbackContext ctx )
    {
        if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(Camera.main, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.startTime);
    }

    void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(Camera.main, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.time);
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(Camera.main, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
