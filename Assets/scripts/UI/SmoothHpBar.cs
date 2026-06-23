
using UnityEngine;


public class SmoothHpBar : MonoBehaviour
{
    [SerializeField]private HPUI hpUI;
    [SerializeField] private UnityEngine.UI.Image hpbarImage;
    [SerializeField]private float smoothValue;
    // Update is called once per frame
    void Update()
    {
        hpbarImage.fillAmount = Mathf.Lerp(hpbarImage.fillAmount, hpUI.fAmount, smoothValue * Time.deltaTime);
    }
}