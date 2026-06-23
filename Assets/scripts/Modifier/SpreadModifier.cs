using UnityEngine;

[CreateAssetMenu]
public class SpreadModifier : Modifier
{
    public float thresholdAngle = 10f;
    public bool checkAbove = true; // trueなら一定以上、falseなら一定以下
    public float damageAddValue = 5f;
    public float speedAddValue = 2f;

    public override void Process(Signal signal)
    {
        bool conditionMet = checkAbove 
            ? signal.maxSpreadAngle >= thresholdAngle 
            : signal.maxSpreadAngle <= thresholdAngle;

        if (conditionMet)
        {
            signal.baseDamage += damageAddValue * signal.modMultiplier;
            signal.bulletSpeed += speedAddValue * signal.modMultiplier;
        }
    }
}