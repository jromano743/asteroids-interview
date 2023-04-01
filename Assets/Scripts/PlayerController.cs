using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    bool thrusting;
    float turnDirection;

    [Header("Settings")]
    [SerializeField] float thrustSpeed = 1.0f;
    [SerializeField] float turnSpeed = 1.0f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] AudioClip shootSound, deadSound, respawnSound;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        thrusting = Input.GetAxis("Vertical") > 0;

        turnDirection = Input.GetAxis("Horizontal") * -1;
        
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if(thrusting)
        {
            _rigidbody.AddForce(transform.up * thrustSpeed);
        }

        if (turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(turnDirection * turnSpeed);
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().Project(transform.up);

        AudioManager.instance.PlaySound(shootSound);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Asteroid"))
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;

            gameObject.SetActive(false);

            AudioManager.instance.PlaySound(deadSound);

            GameManager.instance.PlayerDied();
        }
    }

    public void Respawn()
    {
        AudioManager.instance.PlaySound(respawnSound);
    }
}
