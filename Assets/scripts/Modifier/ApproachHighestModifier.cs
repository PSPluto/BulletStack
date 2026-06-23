using UnityEngine;

[CreateAssetMenu]
public class ApproachHighestModifier : Modifier
{
    public enum TargetStat { Damage, Voltage, Speed }

    public override void Process(Signal signal)
    {
        // 最も高い値を特定
        float highestValue = Mathf.Max(signal.baseDamage, signal.voltage, signal.bulletSpeed);

        // ランダムで1つのステータスを選択
        TargetStat target = (TargetStat)Random.Range(0, 3);

        switch (target)
        {
            case TargetStat.Damage:
                signal.baseDamage = Mathf.MoveTowards(signal.baseDamage, highestValue, signal.baseDamage * signal.modMultiplier);
                break;
            case TargetStat.Voltage:
                signal.voltage = Mathf.MoveTowards(signal.voltage, highestValue, signal.voltage * signal.modMultiplier);
                break;
            case TargetStat.Speed:
                signal.bulletSpeed = Mathf.MoveTowards(signal.bulletSpeed, highestValue, signal.bulletSpeed * signal.modMultiplier);
                break;
        }
    }
}