using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public MotherBoard motherBoard;

    public List<Modifier> playerInventry;


    public void EquipItem(int index)
    {
        motherBoard.circuit.Add(playerInventry[index]);
        playerInventry.RemoveAt(index);
        //playerInventry.Remove(playerInventry[index]);
    }
}