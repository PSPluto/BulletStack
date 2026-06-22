using UnityEngine;

[CreateAssetMenu]
public class OverclockModifier : Modifier
{
    public override void Process(Signal signal)
    {
        signal.minFireinterval /= (1.5f * signal.modMultiplier);
    }
}