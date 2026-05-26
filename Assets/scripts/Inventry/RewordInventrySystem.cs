using Unity.VisualScripting;
using UnityEngine;

public class RewordInventrySystem : MonoBehaviour
{
    public Modifier[] rewardInventory = new Modifier[3];

    public PlayerInventory pInventry;


    public void NewRewardCreate()
    {
        CleanTable();
        for (int i = 0; i < 3; i++)
        {
            rewardInventory[i] = ModifierGenerator.Instance.GenerateRandomModifier();
        }

        Debug.Log("リワードの3枠を更新しました！");
    }

    public void ClaimReward(int index)
    {
        if (rewardInventory[index] == null)
        {
            Debug.Log("[index]がnullでした");
            return;
        }
        pInventry.playerInventry.Add(rewardInventory[index]);
        rewardInventory[index] = null;
        CleanTable();
    }

    public void CleanTable()
    {
        for (int i = 0; i < rewardInventory.Length; i++)
        {
            if (rewardInventory[i] != null) Destroy(rewardInventory[i]);
        }

    }
}