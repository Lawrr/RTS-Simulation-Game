using System.Collections.Generic;
using UnityEngine;

public class VillagerController : MonoBehaviour {

    private float moveSpeed = 5;
    private int state = 0;
    private List<Destination> destinations = new List<Destination>();

    private void Start() {
        destinations.Add(new Destination(transform.position));
        destinations.Add(new Destination(Vector3.zero));
        destinations.Add(new Destination(new Vector3(10, 0, 10)));
        destinations.Add(new Destination(Vector3.zero));
    }

    private void Update() {
        MovementHandlerTick();
        GroundHandlerTick();
    }

    // Change y axis depending on ground position
    private void GroundHandlerTick() {
        Ray rayDown = new Ray(transform.position, Vector3.down);
        Ray rayUp = new Ray(transform.position, Vector3.up);
        RaycastHit hit;

        if (Physics.Raycast(rayDown, out hit, 1 << LayerMask.NameToLayer("Terrain")) ||
            Physics.Raycast(rayUp, out hit, 1 << LayerMask.NameToLayer("Terrain"))) {
            Vector3 position = transform.position;
            position.y = hit.point.y;

            transform.position = position;
        }
    }

    private void MovementHandlerTick() {
        Vector3 from = transform.position;
        Vector3 to = destinations[state].position;

        // Disregard y position between the two points
        from.y = 0;
        to.y = 0;

        // Move
        transform.position = Vector3.MoveTowards(from, to, moveSpeed*Time.deltaTime);

        // Change destination when goal is reached
        if (from == to) {
            state = (state + 1) % destinations.Count;
        }
    }
}
