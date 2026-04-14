using UnityEngine;

public class BG_obj : MonoBehaviour
{
    public float speed = 2f;
    void Update()
    {
        if ((transform.position.x < -9f) || (transform.position.y < -5f))
        {
            Destroy(gameObject);
        }
        transform.position += -transform.right * speed * Time.deltaTime;
    }
}
