using UnityEngine;

[CreateAssetMenu]
public class ConverterModifier : Modifier
{
    public override void Process(Signal signal)
    {
        float n = signal.skipProbability;
        signal.skipProbability = 0f;

        // 減算対象候補のリスト（値がN以上のステータスのみ）
        System.Collections.Generic.List<System.Action> targets = new System.Collections.Generic.List<System.Action>();

        if (signal.voltage >= n) targets.Add(() => signal.voltage -= n);
        if (signal.baseDamage >= n) targets.Add(() => signal.baseDamage -= n);
        if (signal.bulletSpeed >= n) targets.Add(() => signal.bulletSpeed -= n);
        if (signal.modMultiplier >= n) targets.Add(() => signal.modMultiplier -= n);
        if (signal.maxSpreadAngle >= n) targets.Add(() => signal.maxSpreadAngle -= n);
        if (signal.minFireinterval >= n) targets.Add(() => signal.minFireinterval -= n);

        if (targets.Count > 0)
        {
            int randomIndex = RNGManager.Reward.Next(0, targets.Count);
            targets[randomIndex].Invoke();
        }
    }
}