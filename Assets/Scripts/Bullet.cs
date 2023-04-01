using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    [Header("Settings")]
    [SerializeField] float moveSpeed = 500.0f;
    [SerializeField] float maxLifeTime = 10.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * moveSpeed);

        Destroy(gameObject, maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
