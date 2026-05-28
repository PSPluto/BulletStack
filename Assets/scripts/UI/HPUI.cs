using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public MotherBoard player;
    [SerializeField] Image hpBarImage;
    public float lastHP = -1f;

    void Update()
    {
        if (player.currentHP == lastHP) return;
        float mappingHP = player.currentHP / player.maxHP;
        hpBarImage.fillAmount = Mathf.Lerp(0.14f, 0.93f, mappingHP);
        lastHP = player.currentHP;
    }
}
