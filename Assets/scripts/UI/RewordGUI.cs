using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;

public class RewordGUI : MonoBehaviour
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
}
