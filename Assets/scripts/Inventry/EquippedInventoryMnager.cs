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

        // óvëfê∂ê¨Ç∆HashListÇ÷ÇÃìoò^
        for (indexBox = 0; indexBox < list.Count; indexBox++)
        {
            obj = Instantiate(hudObj, transform.Find("Scroll View/Viewport/Content"));
            //obj.transform.localPosition = new Vector3(0, -indexBox * 40, 0);
            InventoryDisplay inventoryDisplay = obj.GetComponent<InventoryDisplay>();
            inventoryDisplay.thisIndex = indexBox;
            inventoryDisplay.inType = invType;
            objects.Add(obj);
        }
    }
}
