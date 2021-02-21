using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneEffectsController : MonoBehaviour
{
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

    private float dissolveAmount = 0f;

    private new ParticleSystem particleSystem;
    private new Light light;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        light = GetComponentInChildren<Light>();
    }

    public void Dissolve()
    {
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        meshRenderer.receiveShadows = false;

        particleSystem.Play();

        InvokeRepeating(nameof(DisolveCoroutine), DissolveSpeed, DissolveSpeed);
        InvokeRepeating(nameof(GlowStartCoroutine), LightGlowSpeed, LightGlowSpeed);
    }

    private void DisolveCoroutine()
    {
        dissolveAmount += DissolveAmountPerStep;
        meshRenderer.material.SetFloat("_Amount", dissolveAmount);

        if (dissolveAmount >= 1.0f)
        {
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
}