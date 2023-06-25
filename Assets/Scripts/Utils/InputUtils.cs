using UnityEngine;
using UnityEngine.InputSystem;

public class InputUtils {

    public static Vector3 GetMousePos() {
        return new Vector3(
            Pointer.current.position.x.ReadValue(),
            Pointer.current.position.y.ReadValue(),
            0
        );
    }
}