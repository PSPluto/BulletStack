using UnityEngine;

[CreateAssetMenu]
public class DiffusionModifier : Modifier
{
    public override void Process(Signal signal)
    {
        signal.maxSpreadAngle += 5f * signal.modMultiplier;
    }
}
