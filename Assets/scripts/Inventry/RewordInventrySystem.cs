using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RewordInventrySystem : MonoBehaviour
{
    public Modifier[] rewardInventory = new Modifier[3];

    public PlayerInventory pInventry;
    public MotherBoard motherBoard;
    public int stack = 0;


    public void NewRewardCreate()
    {
        if (rewardInventory[0] != null)
        {
            stack += 1;
            Debug.Log("リワードが取得されていなかったため、リワード待機変数に+1しました");
        }
        else
        {
            CleanTable();
            for (int i = 0; i < 3; i++)
            {
                rewardInventory[i] = ModifierGenerator.Instance.GenerateRandomModifier();
            }

            Debug.Log("リワードの3枠を更新しました！");
        }
    }

    public void ClaimReward(int index ,bool shouldEquip=false)
    {
        if (rewardInventory[index] == null)
        {
            Debug.Log("[index]がnullでした");
            return;
        }
        if (shouldEquip)
        {
            motherBoard.circuit.Add(rewardInventory[index]);
            motherBoard.EquippedInventoryUpdate();
        }
        else
        {
            pInventry.playerInventry.Add(rewardInventory[index]);
            pInventry.EquippedInventoryUpdate();
        }
        rewardInventory[index] = null;
        CleanTable();
        if (stack > 0)
        {
            stack -= 1;
            NewRewardCreate();
        }
    }

    public void CleanTable()
    {
        for (int i = 0; i < rewardInventory.Length; i++)
        {
            if (rewardInventory[i] != null)
            {
                Destroy(rewardInventory[i]);
                rewardInventory[i] = null;
            }
        }
    }
    Coroutine _activeLoop;
    void Start()
    {

    }
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
        stack = 0;
        CleanTable();
        yield break;
    }

    IEnumerator InGameLoop()
    {
        while (true)
        {
            // インゲームのループ処理
            yield return null;
        }
    }

    IEnumerator GameOverLoop()
    {
        while (true)
        {
            // ゲームオーバーのループ処理
            yield return null;
        }
    }
}