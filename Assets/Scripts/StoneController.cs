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

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {

            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out var hit, Mathf.Infinity, layerMask: stoneLayerMask)) {
                selectedStone = hit.collider.gameObject;

                Vector3 selectedStonePosition = new Vector3(selectedStone.transform.position.x, selectedStone.transform.position.y, selectedStone.transform.position.z);
                circle.transform.position = selectedStonePosition + circleOffset;
            }
        }


    }
}
