using UnityEngine;

[CreateAssetMenu]
public class SplitModifier : Modifier
{

    public override void Process(Signal signal)
    {
        signal.pelletCount = (int)(signal.pelletCount * 2 * signal.modMultiplier);
        signal.bulletSpeed /= (2f * signal.modMultiplier);
        signal.baseDamage /= (2f * signal.modMultiplier);
    }
}
