using UnityEngine;

public class GameController : MonoBehaviour {

    private GameObject selectedBuilding = null;
    
    private void Update() {
        BuildingCreationHandlerTick();
    }

    private void OnGUI() {
        GUI.Box(new Rect(Screen.width / 2, Screen.height - 110, 100, 100), "Buildings");

        // House
        if (GUI.Button(new Rect(Screen.width / 2 + 10, Screen.height - 85, 80, 70), "House") && !selectedBuilding) {
            CreateBuildingBlueprint(Resources.Load("Prefabs/House"),
                Input.mousePosition,
                Quaternion.Euler(270, 0, 0));
        }
    }

    // Show blueprint of building
    private void CreateBuildingBlueprint(Object obj, Vector3 pos, Quaternion angle) {
        selectedBuilding = (GameObject) Instantiate(obj, pos, angle);

        Renderer renderer = selectedBuilding.GetComponent<Renderer>();
        renderer.material.shader = Shader.Find("Standard");
        renderer.material.color = new Color(0.5f, 1, 0.5f, 0.7f);
    }

    private void BuildingCreationHandlerTick() {
        if (selectedBuilding) {
            // Get terrain hit
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1 << LayerMask.NameToLayer("Terrain"))) {
                selectedBuilding.transform.position = hit.point;
            }

            // Place building on click
            BuildingController bc = selectedBuilding.GetComponent<BuildingController>();
            if (Input.GetMouseButtonUp(0) && bc.canPlace) {
                // Place down building
                Renderer renderer = selectedBuilding.GetComponent<Renderer>();
                renderer.material.shader = Shader.Find("Diffuse");
                renderer.material.color = new Color(1, 1, 1, 1);

                // Spawn villager
                Instantiate(Resources.Load("Prefabs/Villager"),
                    selectedBuilding.transform.position,
                    Quaternion.Euler(270, 0, 0));

                bc.placed = true;
                selectedBuilding = null;
            }
        }
    }
}
