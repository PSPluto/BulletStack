using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class SmoothXpBar : MonoBehaviour
{
    [SerializeField]private XPUI xpUI;
    [SerializeField] private UnityEngine.UI.Image xpbarImage;
    [SerializeField]private float smoothValue;
    // Update is called once per frame
    void Update()
    {
        xpbarImage.fillAmount = Mathf.Lerp(xpbarImage.fillAmount, xpUI.fAmount, smoothValue * Time.deltaTime);
    }
}