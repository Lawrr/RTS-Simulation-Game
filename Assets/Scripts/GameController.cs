using UnityEngine;

public class GameController : MonoBehaviour {

    private GameObject selectedBuilding;
    private bool buildingSelected = false;
    private bool buildingSpawned = false;
    private bool guiPressed = false;

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
    void Update() {
        BuildingCreationHandler();
    }

    void OnGUI() {
        GUI.Box(new Rect(Screen.width / 2, Screen.height - 110, 100, 100), "Buildings");
        if (GUI.Button(new Rect(Screen.width / 2 + 10, Screen.height - 85, 80, 70), "House") && !buildingSelected) {
            guiPressed = true;
            buildingSelected = true;
            buildingSpawned = false;
        }
    }

    void BuildingCreationHandler() {
        if (buildingSelected) {
            if (!buildingSpawned) {
                buildingSpawned = true;
                selectedBuilding = (GameObject) Instantiate(Resources.Load("Baker House/Prefabs/Baker_house"),
                                               Input.mousePosition,
                                               Quaternion.Euler(270, 0, 0));

                Renderer renderer = selectedBuilding.GetComponent<Renderer>();
                renderer.material.shader = Shader.Find("Standard");
                Color color = renderer.material.color;
                color.a = 0.5f;
                renderer.material.color = color;
            }

            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if (hit.collider.tag == "Terrain") {
                    selectedBuilding.transform.position = hit.point;
                }
            }

            if (Input.GetMouseButtonUp(0)) {
                if (guiPressed) {
                    guiPressed = false;
                } else {
                    Renderer renderer = selectedBuilding.GetComponent<Renderer>();
                    renderer.material.shader = Shader.Find("Diffuse");
                    Color color = renderer.material.color;
                    color.a = 1;
                    renderer.material.color = color;

                    buildingSelected = false;
                    selectedBuilding = null;
                }
            }
        }
    }
}
