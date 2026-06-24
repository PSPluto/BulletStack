using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RewordGUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RewordInventrySystem ris;
    [SerializeField] private MODStatsDisplay modDisplay;
    [SerializeField] private int index;
    [SerializeField] private SVGImage image;
    private void Start()
    {
        
    }
    void Update()
    {
        if (ris.rewardInventory[index] != null)
        {
            image.sprite = ris.rewardInventory[index].icon;
            image.color = ris.rewardInventory[index].Color;
        }
        else
        {
            image.sprite = null;
            image.color = Color.white;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var rewordIndex = ris.rewardInventory[index];
        if (ris.rewardInventory[index] != null)
        {
            var addMod = rewordIndex as AddModifier;
            if (addMod != null)
            {
                modDisplay.UpdateUI(rewordIndex.name, rewordIndex.description, rewordIndex.resistance, rewordIndex.voltageCost, addMod?.applicableTo, addMod?.addValue);
                return;

            }
            var AppMod = rewordIndex as ApproachHighestModifier;
            if (AppMod != null)
            {
                modDisplay.UpdateUI(rewordIndex.name, rewordIndex.description, rewordIndex.resistance, rewordIndex.voltageCost, AppMod?.applicableTo);
                return;
            }
            else
            {
                modDisplay.UpdateUI(rewordIndex.name, rewordIndex.description, rewordIndex.resistance, rewordIndex.voltageCost);
            }

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        modDisplay.UpdateUI();
    }

    
}
