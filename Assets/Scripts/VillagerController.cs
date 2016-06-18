using UnityEngine;

public class VillagerController : MonoBehaviour {

    private float moveSpeed = 5;
    private Vector3 startPos;
    private int state = 0;

    // Use this for initialization
    void Start() {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        MovementHandlerTick();

        GroundHandlerTick();
    }

    // Change y axis depending on ground position
    void GroundHandlerTick() {
        Ray rayDown = new Ray(transform.position, Vector3.down);
        Ray rayUp = new Ray(transform.position, Vector3.up);
        RaycastHit hit;

        if (Physics.Raycast(rayDown, out hit, 1 << LayerMask.NameToLayer("Terrain")) ||
            Physics.Raycast(rayUp, out hit, 1 << LayerMask.NameToLayer("Terrain"))) {
            Vector3 pos = transform.position;
            pos.y = hit.point.y;

            transform.position = pos;
        }
    }

    void MovementHandlerTick() {
        Vector3 from = transform.position;
        Vector3 to;
        switch (state) {
            case 0:
                to = Vector3.zero;
                break;

            case 1:
            default:
                to = startPos;
                break;
        }

        // Disregard y position between the two points
        from.y = 0;
        to.y = 0;

        // Move
        transform.position = Vector3.MoveTowards(from, to, moveSpeed*Time.deltaTime);

        // Change destination when goal is reached
        if (from == to) {
            switch (state) {
                case 0:
                    state = 1;
                    break;

                case 1:
                    state = 0;
                    break;
            }
        }
    }
}
