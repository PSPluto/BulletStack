using UnityEngine;

[CreateAssetMenu]
public class OverloadModifier : Modifier
{
    //ËŒ‚ŠÔŠu‚ğ’x‚­‚·‚é‘ã‚í‚è‚ÉAmodMultiplier‚ğ0.5 * modMultiplier‘‰Á‚³‚¹‚é
    public override void Process(Signal signal)
    {
        signal.minFireinterval += 0.1f * signal.modMultiplier;
        signal.modMultiplier += 0.5f * signal.modMultiplier;
    }
}