using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        // Z²‚ğ’†S‚ÉA1•bŠÔ‚É90“x‚Ì‘¬‚³‚Å‰ñ“]‚³‚¹‚é—á
        transform.Rotate(0, 0, 90 * Time.deltaTime);
    }
}
