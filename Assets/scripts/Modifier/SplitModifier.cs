using UnityEngine;

[CreateAssetMenu]
public class SplitModifier : Modifier
{

    public override void Process(Signal signal)
    {
        signal.pelletCount *= 2;
        signal.bulletSpeed /= 1.25f;
        signal.baseDamage /= 1.5f;

    }
}
