using UnityEngine;

[RequireComponent(typeof(StoneEffectsController))]
public class Stone : MonoBehaviourSubject
{
    private StoneProperties stoneProperties = new StoneProperties();

    private StoneEffectsController stoneEffectsController;

    [SerializeField]
    float DelayBeforeDisableAfterDissolve = 10.0f;

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
        stoneEffectsController.Undissolve();
    }

    public void Dissolve() {
        IsDisolving = true;

        NotifyObservers();

        stoneEffectsController.Dissolve();
        Invoke(nameof(Disable), DelayBeforeDisableAfterDissolve);
    }

    private void Disable() {
        gameObject.SetActive(false);
    }

    public void NotifyObservers()
    {
        foreach(var observer in observers) {
            observer.OnNotify(this);
        }
    }
}
