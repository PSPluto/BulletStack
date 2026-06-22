using UnityEngine;

[CreateAssetMenu]
public class JointBulleModifier : Modifier
{
    public override void Process(Signal signal)
    {
        signal.bulletSpeed *= Mathf.Sqrt(signal.pelletCount);
        signal.baseDamage *= signal.pelletCount * signal.modMultiplier;
        signal.pelletCount = 1;
    }
}
