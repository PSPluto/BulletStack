using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventoryDisplay : MonoBehaviour, IPointerClickHandler
{
    public enum InventoryType
    {
        Circuit,
        Inventory
    }
    [HideInInspector]public MotherBoard playerScript;
    [HideInInspector]public PlayerInventory playerInventory;
    public Modifier modData;

    [SerializeField]private TMPro.TextMeshProUGUI modNameObj;
    [SerializeField]private TMPro.TextMeshProUGUI addToObj;
    [SerializeField]private SVGImage imageObj;

    public int thisIndex = 0;
    public InventoryType inType;

    private void Start()
    {
        playerScript = FindAnyObjectByType<MotherBoard>();
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        SetList(inType);
    }
    public void SetList(InventoryType type)
    {
        if (type == InventoryType.Circuit)
        {
            InventoryHUDUpdate(playerScript.circuit);
        }
        else if (type == InventoryType.Inventory)
        {
            InventoryHUDUpdate(playerInventory.playerInventry);
        }
    }
    public void InventoryHUDUpdate(List<Modifier> modifiers)
    {
        if (modifiers.Count >= thisIndex+1)
        {
            modData = modifiers[thisIndex];
            modNameObj.text = modData.chipName;
            imageObj.sprite = modData.icon;
            if (modData is AddModifier addModifier)
            {
                addToObj.text = addModifier.applicableTo.ToString();
            }
            else
            {
                addToObj.text = "-----";
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (inType == InventoryType.Circuit)
            {
                playerScript.UnEquipItem(thisIndex);
            }
            else
            {
                playerInventory.EquipItem(thisIndex);
            }
        }
    }

}
