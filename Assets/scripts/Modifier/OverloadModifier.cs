using UnityEngine;

[CreateAssetMenu]
public class OverloadModifier : Modifier
{
    public override void Process(Signal signal)
    {
        signal.skipProbability += 0.15f * signal.modMultiplier;
        signal.minFireinterval += 0.1f * signal.modMultiplier;
        signal.modMultiplier += 0.5f * signal.modMultiplier;
    }
}