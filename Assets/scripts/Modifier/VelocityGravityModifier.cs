using UnityEngine;

[CreateAssetMenu]
public class VelocityModifier : Modifier
{
    public override void Process(Signal signal)
    {
        float threshold = 15f;
        if (signal.bulletSpeed > threshold)
        {
            float excess = signal.bulletSpeed - threshold;
            signal.baseDamage += excess * 1.5f * signal.modMultiplier;
            signal.bulletSpeed = threshold;
        }
    }
}