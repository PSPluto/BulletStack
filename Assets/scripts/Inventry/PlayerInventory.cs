using NUnit.Framework;
using Shapes2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InventoryDisplay;

public class PlayerInventory : MonoBehaviour
{
    public MotherBoard motherBoard;
    public EquippedInventoryMnager equippedInventoryMnager;

    public List<Modifier> playerInventry;
    private Image img;
    private int lastIndex = -1;
    private void Update()
    {
    }
    public void EquipItem(int index)
    {
        if (index < 0 || index >= playerInventry.Count || playerInventry[index] == null)
        {
            //Debug.Log($"ポインタが配列の範囲外か、値がnullでした");
            return;
        }
        motherBoard.circuit.Add(playerInventry[index]);
        playerInventry.RemoveAt(index);
        motherBoard.EquippedInventoryUpdate();
        EquippedInventoryUpdate();
        //playerInventry.Remove(playerInventry[index]);
    }

    Coroutine _activeLoop;
    void OnEnable()
    {
        StateManager.OnStateChanged += StateChanged;
    }
    void OnDisable()
    {
        StateManager.OnStateChanged -= StateChanged;
    }

    // state更新のイベントで呼び出される
    public void StateChanged(int newState)
    {
        // 今のループを止める
        if (_activeLoop != null)
        {
            StopCoroutine(_activeLoop);
            _activeLoop = null;
        }
        // 次のループを始める
        switch (newState)
        {
            case 0: _activeLoop = StartCoroutine(TitleLoop()); break;
            case 1: _activeLoop = StartCoroutine(InGameLoop()); break;
            case 2: _activeLoop = StartCoroutine(GameOverLoop()); break;
            default: break;
        }
    }
    IEnumerator TitleLoop()
    {
        playerInventry.Clear();
        yield return null;
    }

    IEnumerator InGameLoop()
    {   
        lastIndex = -1;
        yield return null;
    }

    IEnumerator GameOverLoop()
    {
            yield return null;
    }
    public void EquippedInventoryUpdate()
    {
        equippedInventoryMnager.UpdateList(playerInventry, InventoryType.Inventory);
    }
    public void listMovement(int thisIndex = -1, bool isContent = false, Image thisImg = null)
    {
        if (lastIndex == -1)
        {
            Debug.Log("一度目のクリック。");
            
            //一回目
            if (isContent == false)
            {
                return;
                
            }
            lastIndex = thisIndex;
            img = thisImg;
            img.enabled = true;

        }
        else
        {
            Debug.Log("二度目のクリック。");
            if (img != null)
            {
                img.enabled = false;
            }
            if (isContent)
            {
                (playerInventry[lastIndex], playerInventry[thisIndex]) = (playerInventry[thisIndex], playerInventry[lastIndex]);
                // 入れ替え
                lastIndex = -1;
                EquippedInventoryUpdate();
            }
            else
            {
                // 差し込み
                //未完成
                lastIndex = -1;
                EquippedInventoryUpdate();
            }
            img.enabled = false;
        }
    }
}

