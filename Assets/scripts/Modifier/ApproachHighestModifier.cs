using UnityEngine;
using static AddModifier;

[CreateAssetMenu]
public class ApproachHighestModifier : Modifier
{
    public enum TargetStat { Damage, Voltage, Speed }
    public ApplicableTo applicableTo;

    public override void Process(Signal signal)
    {
        // 最も高い値を特定
        float highestValue = Mathf.Max(signal.baseDamage, signal.voltage, signal.bulletSpeed);

        switch (applicableTo)
        {
            case ApplicableTo.Damage:
                signal.baseDamage = Mathf.MoveTowards(signal.baseDamage, highestValue, signal.baseDamage * signal.modMultiplier);
                break;
            case ApplicableTo.Voltage:
                signal.voltage = Mathf.MoveTowards(signal.voltage, highestValue, signal.voltage * signal.modMultiplier);
                break;
            case ApplicableTo.Speed:
                signal.bulletSpeed = Mathf.MoveTowards(signal.bulletSpeed, highestValue, signal.bulletSpeed * signal.modMultiplier);
                break;
        }
    }
    public void RollRandomStats()
    {
        //適用先(applicableTo)をDamage, Voltage, Speedの中からランダムに選んでる
        applicableTo = (ApplicableTo)RNGManager.Reward.Next(0, System.Enum.GetValues(typeof(ApplicableTo)).Length);
    }
}