using UnityEngine;

[CreateAssetMenu]
public class HardCodeModifier : Modifier
{
    public float skipProbabilityAdd = 0.2f; // 20%
    public float damageAddValue = 5f;
    public float voltageAddValue = 5f;
    public float speedAddValue = 5f;

    public override void Process(Signal signal)
    {
        signal.skipProbability += skipProbabilityAdd * signal.modMultiplier;
        signal.baseDamage += damageAddValue * signal.modMultiplier;
        signal.voltage += voltageAddValue * signal.modMultiplier;
        signal.bulletSpeed += speedAddValue * signal.modMultiplier;
    }
}