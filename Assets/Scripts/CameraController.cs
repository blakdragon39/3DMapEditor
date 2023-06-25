using UnityEngine;
using UnityEngine.InputSystem;
using Pointer = UnityEngine.InputSystem.Pointer;

public class CameraController : MonoBehaviour {

    [SerializeField] private new Camera camera;
    
    [SerializeField] private float zoomSpeed = 1.0f;
    [SerializeField] private float rotationSpeed = 1.0f;

    private Vector3 mouseWorldPosStart;
    private Vector3 lastMousePos;

    private bool panning;
    private bool rotating;

    private void Update() {
    }

    public void UpdateZoom() {
        var scrollUp = Mouse.current.scroll.up.ReadValue();
        
        var direction = scrollUp > 0 ? Vector3.forward : Vector3.back;
        camera.transform.Translate(direction * zoomSpeed);
    }

    public void StartRotation(InputAction.CallbackContext context) {
        if (context.started) {
            lastMousePos = InputUtils.GetMousePos();
            rotating = true;
        } else if (context.canceled) {
            rotating = false;
        }
    }

    public void UpdateRotation() {
        if (!rotating) return;

        var newMousePos = InputUtils.GetMousePos();
        var mouseXAxis = newMousePos.x - lastMousePos.x;
        var mouseYAxis = newMousePos.y - lastMousePos.y;

        mouseXAxis = mouseXAxis switch {
            > 0 => 1,
            < 0 => -1,
            _ => 0
        };

        mouseYAxis = mouseYAxis switch {
            > 0 => 1,
            < 0 => -1,
            _ => 0
        };
        
        transform.Rotate(Vector3.right, -mouseYAxis * rotationSpeed);
        transform.Rotate(Vector3.up, mouseXAxis * rotationSpeed, Space.World);

        lastMousePos = newMousePos;
    }

    public void StartPan(InputAction.CallbackContext context) {
        if (context.started) {
            panning = true;
            mouseWorldPosStart = GetPerspectivePos();
        } else if (context.canceled) {
            panning = false;
        }
    }
    
    public void UpdatePan() {
        if (!panning) return;
        
        Vector3 mouseWorldPosDiff = mouseWorldPosStart - GetPerspectivePos();
        transform.position += mouseWorldPosDiff;
    }

    private Vector3 GetPerspectivePos() {
        var ray = camera.ScreenPointToRay(Pointer.current.position.ReadValue());
        var plane = new Plane(transform.forward, 0.0f);
        plane.Raycast(ray, out float distance);
        return ray.GetPoint(distance);
    }
}