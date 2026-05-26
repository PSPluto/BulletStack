using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public MotherBoard motherBoard;

    public List<Modifier> playerInventry;


    public void EquipItem(int index)
    {
        if (index < 0 || index >= playerInventry.Count || playerInventry[index] == null)
        {
            Debug.Log($"ポインタが配列の範囲外か、値がnullでした");
            return;
        }
        motherBoard.circuit.Add(playerInventry[index]);
        playerInventry.RemoveAt(index);
        //playerInventry.Remove(playerInventry[index]);
    }
}