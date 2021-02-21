using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(StoneEffectsController))]
public class Stone : MonoBehaviour
{
    public AudioManager AudioManager { get; set; }

    public MarkerController MarkerController { get; set; }

    private StoneProperties stoneProperties = new StoneProperties();

    private StoneEffectsController stoneEffectsController;

    [SerializeField]
    float DelayBeforeDisableAfterDissolve = 10.0f;

    [SerializeField]
    private float DelayBeforeReadSign = 0.33f;

    public bool IsDisolving { get; set; }

    public void SetSign(Sign sign) => stoneProperties.Sign = sign;
    public Sign GetSign() => stoneProperties.Sign;

    public void SetAlphabet(Alphabet type) => stoneProperties.Alphabet = type;
    public Alphabet GetAlphabet() => stoneProperties.Alphabet;

    private void Awake()
    {
        stoneEffectsController = GetComponent<StoneEffectsController>();
    }

    private void OnEnable()
    {
        IsDisolving = false;
    }

    public void Dissolve() {
        IsDisolving = true;

        MarkerController.Shrink();
        stoneEffectsController.Dissolve();
        Invoke(nameof(ReadSign), DelayBeforeReadSign);
        Invoke(nameof(Disable), DelayBeforeDisableAfterDissolve);
    }

    private void ReadSign() {
        AudioManager.ReadSign(stoneProperties.Sign);
    }


    private void Disable() {
        this.gameObject.SetActive(false);
    }
}
