using UnityEngine;

[CreateAssetMenu]
public class SaverModifier : Modifier
{
    public override void Process(Signal signal)
    {
        if (signal.modMultiplier > 0.5f)
        {
            signal.voltage += 18f * signal.modMultiplier;
            signal.modMultiplier -= 0.3f * signal.modMultiplier ;
        }
    }
}