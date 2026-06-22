using UnityEngine;

[CreateAssetMenu]
public class VelocityModifier : Modifier
{
    // bulletSpeed‚ª10‚ð’´‚¦‚Ä‚¢‚éê‡A’´‰ß•ª‚ÌbulletSpeed‚É‰ž‚¶‚ÄbaseDamage‚ð‘‰Á‚³‚¹‚é
    public override void Process(Signal signal)
    {
        float threshold = 10f;
        if (signal.bulletSpeed > threshold)
        {
            float excess = signal.bulletSpeed - threshold;
            signal.baseDamage += excess * 1.5f * signal.modMultiplier;
            signal.bulletSpeed = threshold;
        }
    }
}