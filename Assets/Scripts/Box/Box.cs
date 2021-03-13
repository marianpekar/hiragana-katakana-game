using UnityEngine;

public class Box : MonoBehaviour, IObserver
{
    [SerializeField]
    private GameManager gameManager = null;

    private PersistenceManager persistenceManager;

    private const int BoxTexturesCount = 10;
    private readonly Material[] materials = new Material[BoxTexturesCount];
    private MeshRenderer meshRenderer;

    private int currentBoxTextureIndex;

    private void Awake()
    {
        persistenceManager = PersistenceManager.Instance;
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

        currentBoxTextureIndex = persistenceManager.LastBoxTextureIndex;
    }

    public void OnNotify(ISubject subject, ActionType actionType = ActionType.Unspeficied)
    {
        if (actionType.Equals(ActionType.GameStarts)) {
            SetNextMaterial();
        }
    }

    private void SetNextMaterial() {
        currentBoxTextureIndex++;

        if (currentBoxTextureIndex >= BoxTexturesCount) {
            currentBoxTextureIndex = 0;
        }

        meshRenderer.material = materials[currentBoxTextureIndex];
        persistenceManager.LastBoxTextureIndex = currentBoxTextureIndex;
    }
}
