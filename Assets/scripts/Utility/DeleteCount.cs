using System.Collections;
using UnityEngine;

public class DeleteCount : MonoBehaviour
{
    [SerializeField]private float count = 0;
    void Start()
    {
        StartCoroutine(DeleteTimer());
    }
    IEnumerator DeleteTimer()
    {
        yield return new WaitForSeconds(count);
        Destroy(gameObject);
    }
}
