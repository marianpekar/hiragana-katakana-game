using UnityEngine;

public class Box : MonoBehaviour, IObserver
{
    [SerializeField]
    private GameManager gameManager = null;

    private const int BoxTexturesCount = 5;
    private readonly Material[] materials = new Material[BoxTexturesCount];
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        gameManager.RegisterObserver(this);
    }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        for (int i = 0; i < BoxTexturesCount; i++) {
            var texture = Resources.Load<Texture2D>($"Textures/Box_{i}");
            var material = new Material(meshRenderer.material);

            material.SetTexture("_MainTex", texture);
            materials[i] = material;
        }
    }

    public void OnNotify(ISubject subject, ActionType actionType = ActionType.Unspeficied)
    {
        if (actionType.Equals(ActionType.GameStarts)) {
            SetRandomMaterial();
        }
    }

    private void SetRandomMaterial() {
        meshRenderer.material = materials[Random.Range(0, BoxTexturesCount)];
    }
}
