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
                signal.baseDamage += addValue;
                break;
            case ApplicableTo.Voltage:
                // 適用先(applicableTo)が電圧ならvoltageに加算する値(addValue)を加算
                signal.voltage += addValue;
                break;
            case ApplicableTo.Speed:
                // 適用先(applicableTo)が速度ならbulletSpeedに加算する値(addValue)を加算
                signal.bulletSpeed += addValue;
                break;
        }

    }

    public void RollRandomStats()
    {
        //適用先(applicableTo)をDamage, Voltage, Speedの中からランダムに選んでる
        applicableTo = (ApplicableTo)Random.Range(0, System.Enum.GetValues(typeof(ApplicableTo)).Length);
        switch (applicableTo)
        {
            case ApplicableTo.Damage:
                addValue = Random.Range(2f, 6f);
                break;
            case ApplicableTo.Voltage:
                addValue = Random.Range(4f, 8f);
                break;
            case ApplicableTo.Speed:
                addValue = Random.Range(1f, 6f);
                break;
        }
    }
}
