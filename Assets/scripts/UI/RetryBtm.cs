using UnityEngine;

public class RetryBtm : MonoBehaviour
{
    public void EnterGame(bool isInit)
    {
        RNGManager.Init(0);
    }
}
