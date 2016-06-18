using UnityEngine;

public class BuildingController : MonoBehaviour {

    public bool placed = false;
    public bool canPlace;

    // Number of other buildings this is triggered on
    private int triggers = 0;

    // Use this for initialization
	void Start () {
	    canPlace = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider other) {
        if (!placed && other.tag == "Building") {
            triggers++;
            canPlace = false;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color = new Color(1, 0.5f, 0.5f);
        }
    }

    void OnTriggerExit(Collider other) {
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
