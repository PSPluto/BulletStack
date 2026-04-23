using UnityEngine;

public class ModifierGenerator : MonoBehaviour
{
    public static ModifierGenerator Instance;
    public Modifier[] baseAssets; // 全モディファイアのリスト

    private void Awake()
    {
        Instance = this;
    }
    public Modifier GenerateRandomModifier()
    {
        Modifier seed = baseAssets[Random.Range(0, baseAssets.Length)];
        Modifier instance = Instantiate(seed);

        if (instance is AddModifier addMod) addMod.RollRandomStats();

        return instance;
    }
    //メソッド呼ぶとScriptableObjectを返す
}