using UnityEngine;

public class StoneController : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedStoneGO;

    [SerializeField]
    private GameObject circle;

    [SerializeField]
    private Vector3 circleOffset = new Vector3(0, 1.2f, 0);

    int stoneLayerMask = ~3; // = Stone, see Layers

    private Camera mainCamera;

    [SerializeField]
    private float moveSpeed = 12f;

    private bool isMoving;
    private Vector3 desiredPosition;

    private Sign selectedStoneSign;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if(isMoving) {
            Move();
            return;
        }

        SelectStone();

        if (!selectedStoneGO) return;

        if (Input.GetKey(KeyCode.W)) {
            StartMove(Vector3.forward);
        } 
        else if (Input.GetKey(KeyCode.S)) {
            StartMove(Vector3.back);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            StartMove(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartMove(Vector3.right);
        }
    }

    private void SelectStone() {
        if (Input.GetKey(KeyCode.Mouse0))
        {

            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out var hit, Mathf.Infinity, layerMask: stoneLayerMask))
            {
                if (hit.collider.GetComponent<Stone>().IsDisolving) return;

                selectedStoneGO = hit.collider.gameObject;

                selectedStoneSign = hit.collider.gameObject.GetComponent<Stone>().GetSign();

                Vector3 selectedStonePosition = new Vector3(selectedStoneGO.transform.position.x, selectedStoneGO.transform.position.y, selectedStoneGO.transform.position.z);
                circle.transform.position = selectedStonePosition + circleOffset;
            }
        }
    }

    private void StartMove(Vector3 direction) {
        Debug.DrawLine(selectedStoneGO.transform.position, selectedStoneGO.transform.position + direction * 2, Color.green, 10f);

        if (Physics.Raycast(selectedStoneGO.transform.position, direction, out var hit, 2f)) {
            if (hit.collider.gameObject.GetComponent<Stone>()) {
                var otherStone = hit.collider.gameObject.GetComponent<Stone>();
                if(otherStone.GetSign() == selectedStoneSign) {
                    selectedStoneGO.GetComponent<Stone>().Dissolve();
                    otherStone.Dissolve();
                    selectedStoneGO = null;
                }
            }

            return;
        }

        desiredPosition = selectedStoneGO.transform.position + direction * 2f;
        isMoving = true;
    }

    private void Move()
    {
        selectedStoneGO.transform.position = Vector3.Lerp(selectedStoneGO.transform.position, desiredPosition, Time.deltaTime * moveSpeed);
        circle.transform.position = Vector3.Lerp(circle.transform.position, desiredPosition + circleOffset, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(selectedStoneGO.transform.position, desiredPosition) < 0.01f) {
            selectedStoneGO.transform.position = desiredPosition;
            isMoving = false;
        }
    }
}
