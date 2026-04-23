using Unity.VisualScripting;
using UnityEngine;

public class EnemyBolt : MonoBehaviour
{
    public float damage;
    public float speed;
    public bool isActive;

    // Attractメソッドを追加
    public void Attract(Vector2 attractPosition, float force)
    {
        // Rigidbody2Dがアタッチされている場合のみ処理
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

