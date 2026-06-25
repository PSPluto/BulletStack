using UnityEngine;

/// <summary>
/// 名前: スプリッター (SplitterEnemy)
/// HP: 低め (7) ・ XP: 少なめ (8、ただし分裂体を含めると合計取得量は多い) ・ ScoreValue: 80
/// ゆっくり落下するだけのシンプルな敵だが、撃破すると周囲に
/// 小型の分裂体（同スクリプトをミニ化したもの）を2体放出する。
/// 分裂体(isMini = true)はさらに分裂しない。
/// ※ splitPrefab には本スクリプトを付けたミニサイズのプレハブを割り当ててください。
/// </summary>
public class SplitterEnemy : Enemy
{
    [Header("Splitter設定")]
    public GameObject splitPrefab;     // 分裂時に生成するプレハブ（このスクリプト付き）
    public int splitCount = 2;
    public float splitSpeedBoost = 1.5f;
    public bool isMini = false;        // true の場合は分裂しない（ミニ個体）

    [Header("移動設定")]
    public float acceleration = 3f;

    void Start()
    {
        if (isMini)
        {
            maxHP = 3f;
            XPValue = 4f;
            ScoreValue = 40;
            moveSpeed *= splitSpeedBoost;
        }
        else
        {
            maxHP = 7f;
            XPValue = 8f;
            ScoreValue = 80;
        }

        Initialize();
    }

    void Update()
    {
        removeOffscreen();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = Vector2.MoveTowards(
            rb.linearVelocity,
            Vector2.down * moveSpeed,
            acceleration * Time.fixedDeltaTime
        );
    }

    public override void Die()
    {
        if (!isMini && splitPrefab != null)
        {
            float[] angles = { -35f, 35f };
            for (int i = 0; i < splitCount; i++)
            {
                float angleOffset = i < angles.Length ? angles[i] : 0f;
                GameObject obj = Instantiate(splitPrefab, transform.position, Quaternion.identity);

                SplitterEnemy mini = obj.GetComponent<SplitterEnemy>();
                if (mini != null)
                {
                    mini.isMini = true;
                }

                Rigidbody2D miniRb = obj.GetComponent<Rigidbody2D>();
                if (miniRb != null)
                {
                    Vector2 dir = Quaternion.Euler(0, 0, angleOffset) * Vector2.down;
                    miniRb.linearVelocity = dir * moveSpeed * splitSpeedBoost;
                }
            }
        }

        base.Die();
    }
}
