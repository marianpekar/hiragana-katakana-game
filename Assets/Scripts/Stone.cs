using System.Collections;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public bool IsDisolving { get; private set; }

    private StoneProperties stoneProperties = new StoneProperties();

    public void SetSign(Sign sign) => stoneProperties.Sign = sign;
    public Sign GetSign() => stoneProperties.Sign;

    public void SetAlphabet(Alphabet type) => stoneProperties.Alphabet = type;
    public Alphabet GetAlphabet() => stoneProperties.Alphabet;

    public void Dissolve() {
        IsDisolving = true;
        StartCoroutine(DissolveCoroutine());
    }

    private IEnumerator DissolveCoroutine() {
        var material = gameObject.GetComponent<MeshRenderer>().material;

        float newAmount = 0f;
        while(newAmount <= 1f) {
            newAmount += 0.01f;
            material.SetFloat("_Amount", newAmount);
            yield return new WaitForSeconds(.005f);
        }

        Destroy(gameObject);
    }
}
