using Unity.VisualScripting;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float damage = 0f;
    public float speed = 0f;

    void Update()
    {
        if (Mathf.Abs(transform.position.y)>7 || Mathf.Abs(transform.position.x)>5 )
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.up * speed * 4 * Time.deltaTime);
    }
}
