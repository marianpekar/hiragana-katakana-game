using System;
using System.Collections;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public bool IsDisolving { get; private set; }
    private float dissolveAmount = 0f;

    private StoneProperties stoneProperties = new StoneProperties();

    public void SetSign(Sign sign) => stoneProperties.Sign = sign;
    public Sign GetSign() => stoneProperties.Sign;

    public void SetAlphabet(Alphabet type) => stoneProperties.Alphabet = type;
    public Alphabet GetAlphabet() => stoneProperties.Alphabet;

    public void Dissolve() {
        IsDisolving = true;
        InvokeRepeating(nameof(DisolveCoroutine), 0.01f, 0.01f);
    }

    private void DisolveCoroutine()
    {
        var meshRenderer = gameObject.GetComponent<MeshRenderer>();

        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        meshRenderer.receiveShadows = false;

        dissolveAmount += 0.01f;
        meshRenderer.material.SetFloat("_Amount", dissolveAmount);

        if(dissolveAmount >= 1.0f) {
            CancelInvoke(nameof(DisolveCoroutine));
            Destroy(gameObject);
        }
    }
}
