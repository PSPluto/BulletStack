using NUnit.Framework;
using Shapes2D;
using System;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Shape = Shapes2D.Shape;

public class InventoryDisplay : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
    public AddModifier addModData;

    [SerializeField]private TMPro.TextMeshProUGUI modNameObj;
    [SerializeField]private TMPro.TextMeshProUGUI addToObj;
    [SerializeField]private SVGImage imageObj;
    [HideInInspector]private InventoryType inventoryType;
    [SerializeField]private Image img;
    public MODStatsDisplay modStatsDisplay;

    public bool isContent;


    public int thisIndex = 0;
    public InventoryType inType;

    private void Start()
    {
        inventoryType = inType;
        modStatsDisplay = FindAnyObjectByType<MODStatsDisplay>();
        playerScript = FindAnyObjectByType<MotherBoard>();
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        if(isContent == false) { 
            return;
        }
        img.enabled = false;
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
        if (!isContent) {  return; }
        //自分のindexIDをつかって情報を更新
        if (modifiers.Count >= thisIndex+1)
        {
            modData = modifiers[thisIndex];
            modNameObj.text = modData.chipName;
            imageObj.sprite = modData.icon;
            if (modData is AddModifier addModifier)
            {
                addToObj.text = addModifier.applicableTo.ToString();
                addModData = addModifier;
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
            if (!isContent) { return; }
            if (inType == InventoryType.Circuit)
            {
                Debug.Log("回路の右クリック");
                playerScript.UnEquipItem(thisIndex);
                //引数なしで初期化できる↓
                playerScript.listMovement();
            }
            else
            {
                playerInventory.EquipItem(thisIndex);
                playerInventory.listMovement();
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isContent) { return;}
            modStatsDisplay.UpdateUI(modData.name, modData.description, modData.resistance, modData.voltageCost, addModData?.applicableTo, addModData?.addValue);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isContent) { return; }
        modStatsDisplay.UpdateUI();
    }

}
