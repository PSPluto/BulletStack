using System.Collections;
using UnityEngine;

/// <summary>
/// 名前: ウェイトドロッパー (WeightDropper)
/// HP: 高め (15) ・ XP: 多め (30) ・ ScoreValue: 250
/// 画面上部で一定時間待機(ホバリング)した後、急速に落下し、
/// 当たったプレイヤーに大ダメージを与える「重り」タイプの敵。
/// 落下中のみ当たり判定が有効になり、接触すると大ダメージを与えて自身も消滅する。
/// </summary>
public class WeightDropper : Enemy
{
    [Header("WeightDropper設定")]
    public float waitTimeAtTop = 1.2f;   // 上部で待機する時間（予兆）
    public float fallSpeed = 18f;        // 落下速度（非常に速い）
    public float dropAcceleration = 40f; // 落下開始時の加速度
    public float impactDamage = 15f;     // 接触ダメージ（大）

    private enum Phase { Waiting, Falling }
    private Phase phase = Phase.Waiting;
    private Vector2 targetVelocity;

    void Start()
    {
        // ステータス設定
        maxHP = 15f;
        XPValue = 30f;
        ScoreValue = 250;
        damage = impactDamage;

        Initialize();
        StartCoroutine(WaitThenDrop());
    }

    void Update()
    {
        removeOffscreen();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = Vector2.MoveTowards(
            rb.linearVelocity,
            targetVelocity,
            dropAcceleration * Time.fixedDeltaTime
        );
    }

    IEnumerator WaitThenDrop()
    {
        // 待機中は完全停止（プレイヤーに警戒させる）
        targetVelocity = Vector2.zero;
        phase = Phase.Waiting;

        yield return new WaitForSeconds(waitTimeAtTop);

        // 落下開始
        phase = Phase.Falling;
        targetVelocity = Vector2.down * fallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryImpact(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        TryImpact(other);
    }

    private void TryImpact(Collider2D other)
    {
        if (phase != Phase.Falling) return; // 落下中のみダメージ判定
        if (other.CompareTag("Player"))
        {
            MotherBoard mb = other.GetComponent<MotherBoard>();
            if (mb != null)
            {
                mb.TakeDamage(impactDamage);
            }
            OnContactWithPlayer();

            // 着弾後は自壊する（重りが激突したイメージ）
            gameObject.tag = "Untagged";
            Destroy(gameObject);
            PhaseManager.Instance.PhaseEndCheck();
        }
    }

    public override void OnContactWithPlayer()
    {
        base.OnContactWithPlayer();
    }
}
