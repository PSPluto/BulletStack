using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryDisplay : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector]public MotherBoard playerScript;
    public Modifier modData;

    [SerializeField]private TMPro.TextMeshProUGUI modNameObj;
    [SerializeField]private TMPro.TextMeshProUGUI addToObj;
    [SerializeField]private SVGImage imageObj;

    public int thisIndex = 0;

    private void Start()
    {
        playerScript = FindAnyObjectByType<MotherBoard>();
    }
    public void InventoryHUDUpdate()
    {
        if (playerScript.circuit.Count >= thisIndex+1)
        {
            modData = playerScript.circuit[thisIndex];
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
    void Update()
    {
        InventoryHUDUpdate();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            playerScript.UnEquipItem(thisIndex);
        }
    }

}
