using UnityEngine;

public class MarkerController : MonoBehaviour, IObserver
{
    [SerializeField]
    private Vector3 offset = new Vector3(0, 1.2f, 0);

    [SerializeField]
    float growSpeed = 0.0025f;

    [SerializeField]
    float shrinkSpeed = 0.01f;

    [SerializeField] 
    float scale = 0.01f;

    [SerializeField]
    float speed = 5f;

    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool isShrinking;

    private void Start()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * speed) * scale, transform.position.z);
    }

    public void SetPosition(Vector3 position) {
        if (isShrinking)
        {
            CancelInvoke(nameof(SmoothScale));
        }

        transform.position = position + offset;

        Grow();
    }

    public void Move(Vector3 position, float speed)
    {
        transform.position = Vector3.Lerp(transform.position, position + offset, Time.deltaTime * speed);
    }

    private void Grow() {
        transform.localScale = Vector3.zero;
        targetScale = originalScale;
        InvokeRepeating(nameof(SmoothScale), growSpeed, growSpeed);
    }

    public void Shrink() {
        targetScale = Vector3.zero;
        InvokeRepeating(nameof(SmoothScale), shrinkSpeed, shrinkSpeed);
        isShrinking = true;
    }

    private void SmoothScale()
    {
        transform.localScale += targetScale.sqrMagnitude > transform.localScale.sqrMagnitude ? new Vector3(1f, 1f, 1f) : -new Vector3(1f, 1f, 1f);

        if (Mathf.Abs(transform.localScale.sqrMagnitude - targetScale.sqrMagnitude) <= 0.01f)
        {
            CancelInvoke(nameof(SmoothScale));
        }
    }

    public void OnNotify(ISubject subject)
    {
        Stone stone = subject as Stone;
        if (stone)
        {
            Shrink();
        }
    }
}
