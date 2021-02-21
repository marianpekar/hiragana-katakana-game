using UnityEngine;

public class StoneFactory : MonoBehaviour
{
    [SerializeField]
    private AudioManager audioManager = null;

    [SerializeField]
    private MarkerController markerController = null;

    [SerializeField]
    private GameObject stonePrefab = null;

    [SerializeField]
    private Material stoneMaterial = null;

    [SerializeField]
    private Material particleMaterial = null;

    [SerializeField]
    private Color hiraganaParticleColor = Color.white;

    [SerializeField]
    private Color katakanaParticleColor = Color.black;

    public GameObject CreateStone(Alphabet alphabet, Sign sign)
    {
        var stoneGO = Instantiate(stonePrefab);
        stoneGO.name = $"Stone_{alphabet}_{sign}";

        AddStoneComponent(stoneGO, alphabet, sign);

        Texture2D stoneTexture = SetStoneMaterial(stoneGO, alphabet, sign);
        SetParticleEffect(stoneGO, stoneTexture, alphabet, sign);

        stoneGO.SetActive(false);
        return stoneGO;
    }

    private Texture2D SetStoneMaterial(GameObject stoneGO, Alphabet alphabet, Sign sign)
    {
        var material = new Material(stoneMaterial);
        var texture = Resources.Load<Texture2D>($"Textures/{alphabet}_{sign}");
        if (texture)
        {
            material.SetTexture("_MainTex", texture);
        }
        stoneGO.GetComponent<MeshRenderer>().material = material;

        return texture;
    }

    private void SetParticleEffect(GameObject stoneGO, Texture2D texture, Alphabet alphabet, Sign sign)
    {
        var particleMaterial = new Material(this.particleMaterial);
        var particleTexture = Resources.Load<Texture2D>($"Textures/Particle_{sign}");
        if (texture)
        {
            particleMaterial.SetTexture("_MainTex", particleTexture);
        }

        var particleRenderer = stoneGO.GetComponentInChildren<ParticleSystemRenderer>();
        particleRenderer.material = particleMaterial;

        var particleSystem = stoneGO.GetComponentInChildren<ParticleSystem>();
        var trails = particleSystem.trails;

        switch (alphabet)
        {
            case Alphabet.Hiragana:
                particleMaterial.color = hiraganaParticleColor;
                trails.colorOverLifetime = hiraganaParticleColor;
                break;
            case Alphabet.Katakana:
                particleMaterial.color = katakanaParticleColor;
                trails.colorOverLifetime = katakanaParticleColor;
                break;
        }
    }

    private void AddStoneComponent(GameObject stoneGO, Alphabet alphabet, Sign sign) {
        var stone = stoneGO.AddComponent<Stone>();
        stone.SetAlphabet(alphabet);
        stone.SetSign(sign);

        stone.RegisterObserver(audioManager);
        stone.RegisterObserver(markerController);
    }
}
