using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public MotherBoard player;
    [SerializeField] Image hpBarImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mappingHP = player.currentHP / player.maxHP;
        hpBarImage.fillAmount = Mathf.Lerp(0.14f, 0.93f, mappingHP);
    }
}
