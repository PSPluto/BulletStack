using UnityEngine;
using UnityEngine.EventSystems;

public class BtnClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]private RewordInventrySystem ris;
    [SerializeField]private int index;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ris.ClaimReward(index, false);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            ris.ClaimReward(index, true);
        }
    }

}
