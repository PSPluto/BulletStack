using UnityEngine;

public class ModifierGenerator : MonoBehaviour
{
    public Modifier[] baseAssets; // 全モディファイアのリスト
    public Modifier GenerateRandomModifier()
    {
        Modifier seed = baseAssets[Random.Range(0, baseAssets.Length)];
        Modifier instance = Instantiate(seed);

        if (instance is AddModifier addMod) addMod.RollRandomStats();

        return instance;
    }
}