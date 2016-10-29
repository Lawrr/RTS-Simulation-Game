using UnityEngine;

public class BuildingController : MonoBehaviour {

    public bool canPlace { get; private set; }
    public bool placed { get; set; }

    // Number of other buildings this is triggered on
    private int triggers = 0;

    private void Start() {
        canPlace = true;
        placed = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (!placed && other.tag == "Building") {
            triggers++;
            canPlace = false;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color = new Color(1, 0.5f, 0.5f);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!placed && other.tag == "Building") {
            // Make sure we're no longer triggered on anything
            if (--triggers == 0) {
                canPlace = true;
                Renderer renderer = GetComponent<Renderer>();
                renderer.material.color = new Color(0.5f, 1, 0.5f);
            }
        }
    }
}
