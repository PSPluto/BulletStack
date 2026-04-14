using System.Collections;
using UnityEngine;

public class Background_Animation : MonoBehaviour
{
    public GameObject BG_LinePrefab;
    
    private IEnumerator Start()
    {
        Instantiate(BG_LinePrefab, new Vector3(0, 5, 1), Quaternion.Euler(0f, 0f, 90f));
        Instantiate(BG_LinePrefab, new Vector3(0, 1, 1), Quaternion.Euler(0f, 0f, 90f));
        Instantiate(BG_LinePrefab, new Vector3(0, -3, 1), Quaternion.Euler(0f, 0f, 90f));
        Instantiate(BG_LinePrefab, new Vector3(0, -7, 1), Quaternion.Euler(0f, 0f, 90f));
        Instantiate(BG_LinePrefab, new Vector3(0, -10, 1), Quaternion.Euler(0f, 0f, 90f));

        Instantiate(BG_LinePrefab, new Vector3(9, 0, 1), Quaternion.Euler(0f, 0f, 0f));
        Instantiate(BG_LinePrefab, new Vector3(5, 0, 1), Quaternion.Euler(0f, 0f, 0f));
        Instantiate(BG_LinePrefab, new Vector3(1, 0, 1), Quaternion.Euler(0f, 0f, 0f));
        Instantiate(BG_LinePrefab, new Vector3(-3, 0, 1), Quaternion.Euler(0f, 0f, 0f));
        Instantiate(BG_LinePrefab, new Vector3(-7, 0, 1), Quaternion.Euler(0f, 0f, 0f));
        Instantiate(BG_LinePrefab, new Vector3(-10, 0, 1), Quaternion.Euler(0f, 0f, 0f));

        while (true)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(BG_LinePrefab, new Vector3(9, 0, 1),Quaternion.Euler(0f, 0f, 0f));
            Instantiate(BG_LinePrefab, new Vector3(0, 5, 1), Quaternion.Euler(0f, 0f, 90f));
        }
    } 
}
