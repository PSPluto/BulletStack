using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float damage = 0f;
    public float speed = 0f;

    private Rigidbody2D rb;
    [SerializeField]private GameObject hitParticlePrefab;
    [SerializeField]private AudioClip hitSE;
    [SerializeField]private AudioClip noHitSE;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed * 4f;
    }

    void Update()
    {
        if (IsOutOfBounds()) Destroy(gameObject);
    }

    bool IsOutOfBounds()
    {
        return Mathf.Abs(transform.position.y) > 7f
            || Mathf.Abs(transform.position.x) > 5f;
    }

    public void Attract(Vector2 attractPosition, float force)
    {
        Vector2 direction = (attractPosition - rb.position).normalized;
        rb.AddForce(direction * force, ForceMode2D.Force);


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy == null) return;

        enemy.TakeDamage(damage);
        if (enemy.isInvincible)
        {
            AudioManager.Instance.Playsound(noHitSE);
        }
        else {
        AudioManager.Instance.Playsound(hitSE);
        ParticleManager.Instance.CreateParticle(hitParticlePrefab, transform.position, (transform.rotation));

        }

        Destroy(gameObject);
    }
}