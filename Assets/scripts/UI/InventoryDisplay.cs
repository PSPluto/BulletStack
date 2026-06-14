using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using Shapes2D;
using Shape = Shapes2D.Shape;

public class InventoryDisplay : MonoBehaviour, IPointerClickHandler
{
    public enum InventoryType
    {
        Circuit,
        Inventory
    }
    [HideInInspector]public MotherBoard playerScript;
    [HideInInspector]public PlayerInventory playerInventory;
    [HideInInspector]public EquippedInventoryMnager equippedInventoryMnager;

    
    public Modifier modData;

    [SerializeField]private TMPro.TextMeshProUGUI modNameObj;
    [SerializeField]private TMPro.TextMeshProUGUI addToObj;
    [SerializeField]private SVGImage imageObj;
    [HideInInspector]private InventoryType inventoryType;
    [SerializeField]private Image img;

    public bool isContent;


    public int thisIndex = 0;
    public InventoryType inType;

    private void Start()
    {
        img.enabled = false;
        playerScript = FindAnyObjectByType<MotherBoard>();
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        if(isContent == false) { 
            return;
        }
        SetList(inType);
    }
    public void SetList(InventoryType type)
    {
        inventoryType = type;
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
        //自分のindexIDをつかって情報を更新
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
            //左
            Debug.Log("左クリック");
            if (inventoryType == InventoryType.Circuit)
            {
                playerScript.listMovement(thisIndex, isContent, img);
            }
            else if (inventoryType == InventoryType.Inventory)
            {
                playerInventory.listMovement(thisIndex, isContent, img);
            }
            
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //右
            if (inType == InventoryType.Circuit)
            {
                Debug.Log("回路の右クリック");
                playerScript.UnEquipItem(thisIndex);
            }
            else
            {
                playerInventory.EquipItem(thisIndex);
            }
        }
    }

}
