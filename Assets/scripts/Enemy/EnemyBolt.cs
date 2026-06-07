using Unity.VisualScripting;
using UnityEngine;

public class EnemyBolt : MonoBehaviour
{
    public float damage;
    public float speed;
    public bool isActive;
    [SerializeField]private AudioClip hitSE;
    [SerializeField]private GameObject hitParticlePrefab;

    public void Attract(Vector2 attractPosition, float force)
    {
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (attractPosition - rb.position).normalized;
            rb.AddForce(direction * force, ForceMode2D.Force);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                MotherBoard enemy = other.gameObject.GetComponent<MotherBoard>();
                if (isActive == false) { break; }
                enemy.TakeDamage(damage);
                ParticleManager.Instance.CreateParticle(hitParticlePrefab, transform.position, Quaternion.Inverse(transform.rotation));
                AudioManager.Instance.Playsound(hitSE);
                isActive = false;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (isActive == true)
        {
            if (Mathf.Abs(transform.position.y) > 7 || Mathf.Abs(transform.position.x) > 5)
            {
                Destroy(gameObject);
            }
            transform.Translate(Vector3.up * speed * 4 * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}

