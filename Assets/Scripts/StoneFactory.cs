using UnityEngine;

public class StoneFactory : Singleton<StoneFactory>
{
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
        var StoneGO = Instantiate(stonePrefab);
        StoneGO.name = $"Stone_{alphabet}_{sign}";

        var Stone = StoneGO.AddComponent<Stone>();
        Stone.SetAlphabet(alphabet);
        Stone.SetSign(sign);

        var material = new Material(stoneMaterial);

        var texture = Resources.Load<Texture2D>($"Textures/{alphabet}_{sign}");
        if(texture) {
            material.SetTexture("_MainTex", texture);
        }

        StoneGO.GetComponent<MeshRenderer>().material = material;

        var particleMaterial = new Material(this.particleMaterial);
        var particleTexture = Resources.Load<Texture2D>($"Textures/Particle_{sign}");
        if (texture)
        {
            particleMaterial.SetTexture("_MainTex", particleTexture);
        }

        var particleRenderer = StoneGO.GetComponentInChildren<ParticleSystemRenderer>();
        particleRenderer.material = particleMaterial;

        var particleSystem = StoneGO.GetComponentInChildren<ParticleSystem>();
        var trails = particleSystem.trails;

        if (alphabet == Alphabet.Hiragana) {
            particleMaterial.color = hiraganaParticleColor;
            trails.colorOverLifetime = hiraganaParticleColor;
        } 
        else if (alphabet == Alphabet.Katakana) {
            particleMaterial.color = katakanaParticleColor;
            trails.colorOverLifetime = katakanaParticleColor;
        }

        StoneGO.SetActive(false);
        return StoneGO;
    }
}
