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

        // Check distance disregarding y position
        Vector3 movePos = Vector3.MoveTowards(transform.position, to, moveSpeed*Time.deltaTime);
        Vector3 checkPos = movePos;
        checkPos.y = 0;
        to.y = 0;
        if (Vector3.Distance(checkPos, to) < 0.1) {
            // Goal reached
            switch (state) {
                case 0:
                    state = 1;
                    break;

                case 1:
                    state = 0;
                    break;
            }
        }

        transform.position = movePos;
    }
}
