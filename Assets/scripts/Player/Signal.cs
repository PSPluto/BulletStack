using UnityEngine;

public class Signal
{
    public float voltage;
    public float baseDamage;
    public int   pelletCount;
    public float bulletSpeed;
    public float skipProbability;
    public float modMultiplier;
    public float maxSpreadAngle;
    public float minFireinterval;
    public Signal(float voltage, float baseDamage, int pelletCount, float bulletSpeed, float modMultiplier, float skipProbability, float maxSpreadAngle, float minFireinterval)
    {
        this.voltage = voltage;//電圧：modifierを通貨するたびに減少する。電圧が0になると、信号が消滅する。
        this.baseDamage = baseDamage;//基本ダメージ：基本のダメージ。Modifierによって増減する。
        this.pelletCount = pelletCount;//散弾数：1回で何発の弾を撃つか。Modifierによって増減する。
        this.bulletSpeed = bulletSpeed;//弾速：弾の速度。Modifierによって増減する。
        this.modMultiplier = modMultiplier;//モディファイア適用倍率：Modifierの効果をどれだけ強くするかの倍率。Modifierによって増減する。
        this.skipProbability = skipProbability;//パケットロス確率：Modifierをスキップする確率。Modifierによって増減する。
        this.maxSpreadAngle = maxSpreadAngle;//最大拡散角度：散弾の拡散角度の最大値。Modifierによって増減する。
        this.minFireinterval = minFireinterval;//最小発射間隔：信号が次に発射できるようになるまでの最小時間。Modifierによって増減する。
    }
}
