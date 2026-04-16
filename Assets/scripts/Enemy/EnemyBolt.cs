using Unity.VisualScripting;
using UnityEngine;

public class EnemyBolt : MonoBehaviour
{
    public float damage = 0f;
    public float speed = 0f;

    public bool isActive = true;

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

