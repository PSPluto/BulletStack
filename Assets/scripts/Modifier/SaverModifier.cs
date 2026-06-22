using UnityEngine;

[CreateAssetMenu]
public class SaverModifier : Modifier
{
    // modMultiplierが0.5以上の場合、voltageを18 * modMultiplier増加させ、modMultiplierを0.3 * modMultiplier減少させる。
    // バッテリーセーバー
    public override void Process(Signal signal)
    {
        if (signal.modMultiplier > 0.5f)
        {
            signal.voltage += 18f * signal.modMultiplier;
            signal.modMultiplier -= 0.3f * signal.modMultiplier ;
        }
    }
}