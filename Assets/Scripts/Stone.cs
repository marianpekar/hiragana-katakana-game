using System;
using System.Collections;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public bool IsDisolving { get; private set; }
    private float dissolveAmount = 0f;

    public AudioManager AudioManager { get; set; }

    private StoneProperties stoneProperties = new StoneProperties();

    public new ParticleSystem particleSystem;
    public new Light light;
    public MeshRenderer meshRenderer;

    [SerializeField]
    private float DissolveAmountPerStep = 0.01f;

    [SerializeField]
    private float DissolveSpeed = 0.01f;

    [SerializeField]
    private float LightGlowSpeed = 0.01f;

    [SerializeField]
    private float LightGlowIntensityPerStep = 0.01f;

    [SerializeField]
    private float LightGlowHighCap = 1.3f;

    private void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        light = GetComponentInChildren<Light>();
    }

    public void SetSign(Sign sign) => stoneProperties.Sign = sign;
    public Sign GetSign() => stoneProperties.Sign;

    public void SetAlphabet(Alphabet type) => stoneProperties.Alphabet = type;
    public Alphabet GetAlphabet() => stoneProperties.Alphabet;

    public void Dissolve() {
        AudioManager.ReadSign(stoneProperties.Sign);

        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        meshRenderer.receiveShadows = false;

        IsDisolving = true;
        particleSystem.Play();
        InvokeRepeating(nameof(DisolveCoroutine), DissolveSpeed, DissolveSpeed);
        InvokeRepeating(nameof(GlowStartCoroutine), LightGlowSpeed, LightGlowSpeed);
        Invoke(nameof(Disable), 10f);
    }

    private void DisolveCoroutine()
    {
        dissolveAmount += DissolveAmountPerStep;
        meshRenderer.material.SetFloat("_Amount", dissolveAmount);

        if (dissolveAmount >= 1.0f) {
            CancelInvoke(nameof(DisolveCoroutine));
            GetComponent<Collider>().enabled = false;
        }
    }
    public void GlowStartCoroutine()
    {
        light.intensity += LightGlowIntensityPerStep;

        if (light.intensity >= LightGlowHighCap)
        {
            CancelInvoke(nameof(GlowStartCoroutine));
            InvokeRepeating(nameof(GlowEndCoroutine), LightGlowSpeed, LightGlowSpeed / 2f);
        }
    }

    public void GlowEndCoroutine()
    {
        light.intensity -= LightGlowIntensityPerStep;

        if (light.intensity <= 0.0f)
        {
            CancelInvoke(nameof(GlowEndCoroutine));
        }
    }
    private void Disable() {
        this.gameObject.SetActive(false);
    }
}
