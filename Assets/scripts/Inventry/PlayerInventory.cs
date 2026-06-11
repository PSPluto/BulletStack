using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public MotherBoard motherBoard;

    public List<Modifier> playerInventry;

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
            yield return null;
    }

    IEnumerator GameOverLoop()
    {
            yield return null;
    }
}

