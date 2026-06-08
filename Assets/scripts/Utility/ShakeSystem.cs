using System.Collections;
using UnityEngine;

public class ShakeSystem : MonoBehaviour
{
    private float currentShakeVal;
    private Vector3 startPos;
    private float resultPosX;
    private float resultPosY;
    private bool isShake;
    private int shakeColutionCount;

    //public static ShakeSystem instance { get; private set; }
    //private void Awake()
    //{
    //   instance = this;
    //}

    public void Shake(float maxVal, float decreaseAmount)
    {
        StartCoroutine(ShakeColution(maxVal, decreaseAmount));
        shakeColutionCount += 1;
    }
    IEnumerator ShakeColution(float maxVal_C, float decreaseAmount_C)
    {
        currentShakeVal = maxVal_C;
        if (isShake == false)
        {
            startPos = transform.position;
        }
        isShake = true;
        while (currentShakeVal > 0.1f) {
            resultPosX = currentShakeVal + Random.Range(currentShakeVal, currentShakeVal * -1);
            resultPosY = currentShakeVal + Random.Range(currentShakeVal, currentShakeVal * -1);
            transform.position = startPos + new Vector3(resultPosX,resultPosY);
            currentShakeVal = Mathf.Lerp(currentShakeVal, 0, decreaseAmount_C);
            yield return null;
        }
        shakeColutionCount -= 1;
        if (shakeColutionCount == 0)
        {
            transform.position = startPos;
            isShake = false;
        }
        yield return null;
    }

}
