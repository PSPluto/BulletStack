using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RewordGUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RewordInventrySystem ris;
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
        // 必要に応じて処理を追加
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 必要に応じて処理を追加
    }
}
