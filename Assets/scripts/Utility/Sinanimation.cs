using UnityEngine;

public class SinScale : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Update()
    {
        this.transform.localScale = Vector3.one * (1f + 0.05f * Mathf.Sin(Time.time * 4f));
        this.transform.rotation = Quaternion.Euler(0f, 0f, 2 * Mathf.Sin(Time.time * 2f));
    }
}
