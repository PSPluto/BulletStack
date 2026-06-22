using UnityEngine;  [CreateAssetMenu] public class ConservationModifier : Modifier {
    //ダメージと速度の平均値を計算し、それぞれにmodMultiplierを掛けて更新する
    public override void Process(Signal signal)     {         float average = (signal.baseDamage + signal.bulletSpeed) / 2f;         signal.baseDamage = average * signal.modMultiplier;         signal.bulletSpeed = average * signal.modMultiplier;     } }