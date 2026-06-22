using UnityEngine;

[CreateAssetMenu]
public class NarrowDownModifier: Modifier
{
    public override void Process(Signal signal)
    {
        signal.maxSpreadAngle = signal.maxSpreadAngle / 3 * signal.modMultiplier;
    }
}
