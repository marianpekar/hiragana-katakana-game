using UnityEngine;

public class StoneController : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedStone;

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

        if (!selectedStone) return;

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
                selectedStone = hit.collider.gameObject;

                Vector3 selectedStonePosition = new Vector3(selectedStone.transform.position.x, selectedStone.transform.position.y, selectedStone.transform.position.z);
                circle.transform.position = selectedStonePosition + circleOffset;
            }
        }
    }

    private void StartMove(Vector3 direction) {
        Debug.DrawLine(selectedStone.transform.position, selectedStone.transform.position + direction * 2, Color.green, 10f);

        if (Physics.Raycast(selectedStone.transform.position, direction, out var hit, 2f)) return;

        desiredPosition = selectedStone.transform.position + direction * 2f;
        isMoving = true;
    }

    private void Move()
    {
        selectedStone.transform.position = Vector3.Lerp(selectedStone.transform.position, desiredPosition, Time.deltaTime * moveSpeed);
        circle.transform.position = Vector3.Lerp(circle.transform.position, desiredPosition + circleOffset, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(selectedStone.transform.position, desiredPosition) < 0.01f) {
            selectedStone.transform.position = desiredPosition;
            isMoving = false;
        }
    }
}
