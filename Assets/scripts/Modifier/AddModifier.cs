using UnityEngine;

[CreateAssetMenu]
public class AddModifier : Modifier
{
    public enum ApplicableTo { Damage, Voltage, Speed }
    public ApplicableTo applicableTo;

    public float addValue = 1f;
    public override void Process(Signal signal)
    {
        switch (applicableTo)
        {
            case ApplicableTo.Damage:
                // 適用先(applicableTo)がダメージならbaseDamageに加算する値(addValue)を加算
                signal.baseDamage += addValue * signal.modMultiplier;
                break;
            case ApplicableTo.Voltage:
                // 適用先(applicableTo)が電圧ならvoltageに加算する値(addValue)を加算
                signal.voltage += addValue * signal.modMultiplier;
                break;
            case ApplicableTo.Speed:
                // 適用先(applicableTo)が速度ならbulletSpeedに加算する値(addValue)を加算
                signal.bulletSpeed += addValue * signal.modMultiplier;
                break;
        }

    }

    public void RollRandomStats()
    {
        //適用先(applicableTo)をDamage, Voltage, Speedの中からランダムに選んでる
        applicableTo = (ApplicableTo)RNGManager.Reward.Next(0, System.Enum.GetValues(typeof(ApplicableTo)).Length);
        switch (applicableTo)
        {
            case ApplicableTo.Damage:
                addValue = (float)(RNGManager.Reward.NextDouble() * (6f - 2f) + 2f);
                break;
            case ApplicableTo.Voltage:
                addValue = (float)(RNGManager.Reward.NextDouble() * (8f - 4f) + 4f);
                break;
            case ApplicableTo.Speed:
                addValue = (float)(RNGManager.Reward.NextDouble() * (6f - 1f) + 1f);
                break;
        }
    }
}
