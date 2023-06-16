using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private float zoomSpeed = 1.0f;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float panSpeed = 1.0f;

    private Camera camera;

    private Vector3 lastMousePos;
    private Vector3 mousePosChange;

    private void Awake() {
        camera = GetComponent<Camera>();
    }
    
    private void Update() {
        var newMousePos = Input.mousePosition;
        mousePosChange = lastMousePos - newMousePos;
        
        UpdateZoom();
        UpdateRotation();
        UpdatePan();

        lastMousePos = newMousePos;
    }

    private void UpdateZoom() {
        var scrollChange = Input.mouseScrollDelta.y;
        if (scrollChange == 0) return;

        var direction = scrollChange > 0 ? Vector3.forward : Vector3.back;
        transform.Translate(direction * zoomSpeed);
    }

    private void UpdateRotation() {
        if (!Input.GetMouseButton(2)) return;

        var rotationChange = new Vector3(-mousePosChange.y, mousePosChange.x, 0);
        transform.eulerAngles -= rotationChange * rotationSpeed;
    }

    private void UpdatePan() {
        if (!Input.GetMouseButton(1)) return;

        transform.position += mousePosChange * panSpeed;
    }
}