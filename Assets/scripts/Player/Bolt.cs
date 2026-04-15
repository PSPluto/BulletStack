using Unity.VisualScripting;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float damage = 0f;
    public float speed = 0f;

    public bool canGiveDamage = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Enemy":
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                if (canGiveDamage == false) { break; }
                enemy.TakeDamage(damage);
                canGiveDamage = false;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (canGiveDamage == true)
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

