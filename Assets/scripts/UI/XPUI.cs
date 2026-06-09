using UnityEngine;
using UnityEngine.UI;

public class XPUI : MonoBehaviour
{
    [HideInInspector]public float fAmount;
    public MotherBoard player;
    [SerializeField] Image xpBarImage;
    public float lastXP = -1f;

    void Update()
    {
        if (player.currentXP == lastXP) return;
        if (player == null || player.levelUpXpValue <= 0f)
        {
            fAmount = 0f;
            if (xpBarImage != null) xpBarImage.fillAmount = 0f;
            return;
        }
        float mappingXP = player.currentXP / player.levelUpXpValue;
        fAmount = Mathf.Lerp(0f, 1f, mappingXP);
        xpBarImage.fillAmount = fAmount;
        lastXP = player.currentXP;
    }
}
