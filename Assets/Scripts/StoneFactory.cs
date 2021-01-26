using UnityEngine;

public class StoneFactory : Singleton<StoneFactory>
{
    [SerializeField]
    private GameObject stonePrefab = null;

    [SerializeField]
    private Material stoneMaterial = null;

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

        StoneGO.SetActive(false);
        return StoneGO;
    }
}
