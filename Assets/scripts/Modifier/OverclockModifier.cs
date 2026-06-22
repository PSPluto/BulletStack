using UnityEngine;

[CreateAssetMenu]
public class OverclockModifier : Modifier
{
    // Å¬”­ËŠÔŠu‚ğ1.5 * modMultiplier‚ÅŠ„‚éB
    public override void Process(Signal signal)
    {
        signal.minFireinterval /= (1.5f * signal.modMultiplier);
    }
}