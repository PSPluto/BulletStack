using UnityEngine;

/// <summary>
/// ゆっくり落ちていく。プレイヤーに触れるとダメージを与える敵
/// OnContactWithPlayer をオーバーライドしてダメージ処理を実装
/// </summary>
public class SlowDrifter : Enemy
{
    [Header("SlowDrifter設定")]
    public float contactDamage = 5f;   // 接触ダメージ量
    public float damageCooldown = 1f;  // 連続ダメージの間隔（秒）

    [Header("移動設定")]
    public float acceleration = 2f;    // 小さいほどゆっくり動き出す

    private float damageTimer = 0f;
    private bool canDamage = true;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        removeOffscreen();

        // ダメージクールダウン管理
        if (!canDamage)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageCooldown)
            {
                canDamage = true;
                damageTimer = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        // ゆっくり加速しながら落下
        rb.linearVelocity = Vector2.MoveTowards(
            rb.linearVelocity,
            Vector2.down * moveSpeed,
            acceleration * Time.fixedDeltaTime
        );
    }

    // プレイヤーと接触したとき Enemy.cs の OnTriggerEnter2D 等から呼ぶ想定
    // または OnTriggerEnter2D をここで定義してプレイヤーを直接参照する
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DealContactDamage(other);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DealContactDamage(other);
        }
    }

    private void DealContactDamage(Collider2D other)
    {
        if (!canDamage) return;

        // MotherBoard（プレイヤー本体）がある場合はそちらへダメージ
        // プレイヤー側にTakeDamageメソッドがあればそちらを呼ぶ
        // ※MotherBoardの実装に合わせて変更してください
        MotherBoard mb = other.GetComponent<MotherBoard>();
        if (mb != null)
        {
            // mb.TakeDamage(contactDamage); // MotherBoardにTakeDamageがあれば
            // 暫定：ScoreやXPには影響なく純粋にダメージのみ
        }

        canDamage = false;

        // Enemy基底クラスのOnContactWithPlayerも呼ぶ
        OnContactWithPlayer();
    }

    public override void OnContactWithPlayer()
    {
        // 追加の接触エフェクトが必要なときはここに書く
        base.OnContactWithPlayer();
    }
}
