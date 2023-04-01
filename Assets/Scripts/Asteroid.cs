using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField] Sprite[] sprites;
    [SerializeField] float speed = 10.0f;
    [SerializeField] float size = 1.0f;
    [SerializeField] float minSize = 0.5f;
    [SerializeField] float maxSize = 1.5f;
    [SerializeField] AudioClip destroySound;

    SpriteRenderer spriteRenderer;
    Rigidbody2D _rigidbody;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        size = Random.Range(minSize, maxSize);

        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * size;

        _rigidbody.mass = size;

    }

    public void SetTrayectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            if(size * 0.5 >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            GameManager.instance.AsteroidDestroyed(transform, size);

            AudioManager.instance.PlaySound(destroySound);

            Destroy(gameObject, 0.05f);
        }
    }

    void CreateSplit()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, transform.rotation);
        half.SetSize(size * 0.5f);
        half.SetTrayectory(Random.insideUnitCircle.normalized * speed);
    }

    public void SetSize(float newSize)
    {
        size = newSize;
    }
}
