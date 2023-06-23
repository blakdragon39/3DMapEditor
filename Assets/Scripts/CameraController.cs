using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private GameObject modelContainer;
    [SerializeField] private new Camera camera;
    
    [SerializeField] private float zoomSpeed = 1.0f;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float panSpeed = 1.0f;

    private Vector3 mouseWorldPosStart;
    private float mouseX;
    private float mouseY;

    private void Update() {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        UpdateZoom();
        UpdateRotation();
        UpdatePan();
    }

    private void UpdateZoom() {
        var scrollChange = Input.mouseScrollDelta.y;
        if (scrollChange == 0) return;

        var direction = scrollChange > 0 ? Vector3.forward : Vector3.back;
        transform.Translate(direction * zoomSpeed);
    }

    private void UpdateRotation() {
        if (!Input.GetMouseButton(2)) return;
        
        transform.Rotate(Vector3.right, -mouseY * rotationSpeed);
        transform.Rotate(Vector3.up, mouseX * rotationSpeed, Space.World);
    }

    private void UpdatePan() {
        if (Input.GetMouseButtonDown(1)) {
            mouseWorldPosStart = GetPerspectivePos();
            return;
        }
        
        if (!Input.GetMouseButton(1)) return;
        
        if (mouseX != 0 || mouseY != 0) {
            Vector3 mouseWorldPosDiff = mouseWorldPosStart - GetPerspectivePos();
            transform.position += mouseWorldPosDiff;
        }
    }

    private Vector3 GetPerspectivePos() {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(transform.forward, 0.0f);
        plane.Raycast(ray, out float distance);
        return ray.GetPoint(distance);
    }
}