using UnityEngine;

public class VillagerController : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5;

    private Vector3 startPos;
    private int state = 0;

    // Use this for initialization
    void Start() {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        MovementHandler();

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            if (hit.collider.tag == "Terrain") {
                Vector3 pos = transform.position;
                pos.z = hit.point.z;

                transform.position = pos;
            }
        }
    }

    void MovementHandler() {
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

        Vector3 movePos = Vector3.MoveTowards(transform.position, to, moveSpeed*Time.deltaTime);
        if (movePos == to) {
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
