using UnityEngine;

public class CamController : MonoBehaviour {

    private Vector3 focus = Vector3.zero;
    private Vector3 lastPos;
    
    // Update is called once per frame
    private void Update() {
        MouseHandler();
    }

    private void OnGUI() {
        GUI.Box(new Rect(Screen.width - 210, Screen.height - 100, 200, 90), "Camera Operations");
        GUI.Label(new Rect(Screen.width - 200, Screen.height - 80, 200, 30), "RMB / Alt+LMB: Tumble");
        GUI.Label(new Rect(Screen.width - 200, Screen.height - 60, 200, 30), "MMB / Alt+Cmd+LMB: Track");
        GUI.Label(new Rect(Screen.width - 200, Screen.height - 40, 200, 30), "Wheel / 2 Fingers Swipe: Dolly");
    }

    private void MouseHandler() {
        MouseScrollHandler();
    }

    private void MouseScrollHandler() {
        float delta = Input.GetAxis("Mouse ScrollWheel");

        Vector3 focusPos = transform.position - focus;
        Vector3 pos = focusPos * (1.0f + delta);

        if (pos.magnitude > 0.01) {
            transform.position = focus + pos;
        }
    }
}
