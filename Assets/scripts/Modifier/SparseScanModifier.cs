using UnityEngine;

[CreateAssetMenu]
public class SparseScanModifier : Modifier
{
    //モディファイアスキップ倍率を15％ * modMultiplier加算する代わりに、modMultiplierを0.5 * modMultiplier増加させる
    public override void Process(Signal signal)
    {
        signal.skipProbability += 15f * signal.modMultiplier;
        signal.modMultiplier += 0.5f * signal.modMultiplier;
    }
}