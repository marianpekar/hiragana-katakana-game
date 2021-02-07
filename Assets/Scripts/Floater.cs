using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] 
    float scale = 0.01f;

    [SerializeField]
    float speed = 5f;

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * speed) * scale, transform.position.z);
    }
}
