using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static InventoryDisplay;

public class EquippedInventoryMnager : MonoBehaviour
{
    [SerializeField] private int indexBox;
    [SerializeField] private GameObject hudObj;
    private HashSet<GameObject> objects = new HashSet<GameObject>();
    private GameObject obj;
    private int? findex;
    private bool sisContent;

    public void UpdateList( List<Modifier> list, InventoryType invType)
    {
        foreach(GameObject objs in objects)
        {
            Destroy(objs);
        }
        objects.Clear();

        if (list == null)
        {
            return;
        }

        // 要素生成とHashListへの登録
        for (indexBox = 0; indexBox < list.Count; indexBox++)
        {
            obj = Instantiate(hudObj, transform.Find("Scroll View/Viewport/Content"));
            //obj.transform.localPosition = new Vector3(0, -indexBox * 40, 0);
            InventoryDisplay inventoryDisplay = obj.GetComponent<InventoryDisplay>();
            inventoryDisplay.thisIndex = indexBox;
            inventoryDisplay.inType = invType;
            inventoryDisplay.equippedInventoryMnager = this; 
            //インデックスと担当するインベントリ、このスクリプトへの参照
            //が渡される
            objects.Add(obj);
        }


    }
    public void listMovement(int? thisIndex, bool isContent, List<Modifier> modifiers)
    {
        if (thisIndex == null)
        {
            if (isContent == false)
            {
                return;
            }
            findex = thisIndex;
            Modifier = findex;
            
        }
        else
        {
            sisContent = isContent;
            if (isContent)
            {
                // 入れ替え
            }
            else
            {
                // 差し込み
            }
        }
        

    }
}
