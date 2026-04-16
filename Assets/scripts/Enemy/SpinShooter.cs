using UnityEngine;

/// <summary>
/// ゆっくり落ちていく。回転しながら一定間隔で8方向に弾幕を張る敵
/// Updateで回転と射撃タイマーを管理
/// </summary>
public class SpinShooter : Enemy
{
    [Header("SpinShooter設定")]
    public GameObject bulletPrefab;
    public float rotateSpeed = 90f;    // 1秒あたりの回転角度
    public float shootInterval = 1.5f; // 射撃間隔（秒）
    public int directions = 8;         // 弾の方向数（8方向）
    public float bulletSpeed = 4f;
    public float bulletDamage = 2f;

    [Header("移動設定")]
    public float acceleration = 4f;

    private float shootTimer = 0f;
    private float currentAngle = 0f;   // 現在の回転角度（弾の向き基準にも使う）

    void Start()
    {
        Initialize();
        // 開始直後に1回射撃するため、タイマーを満タンにしておく
        shootTimer = shootInterval;
    }

    void Update()
    {
        removeOffscreen();

        // 回転
        currentAngle += rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);

        // 射撃タイマー
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            shootTimer = 0f;
            ShootAllDirections();
        }
    }

    void FixedUpdate()
    {
        // ゆっくり一定速度で落下（加速なし・シンプル）
        rb.linearVelocity = Vector2.MoveTowards(
            rb.linearVelocity,
            Vector2.down * moveSpeed,
            acceleration * Time.fixedDeltaTime
        );
    }

    void ShootAllDirections()
    {
        if (bulletPrefab == null) return;

        float angleStep = 360f / directions;

        for (int i = 0; i < directions; i++)
        {
            // currentAngleを基準にすることで、回転に合わせて弾の向きが変わる
            float angle = currentAngle + angleStep * i;
            Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);
            GameObject bolt = Instantiate(bulletPrefab, transform.position, rotation);

            EnemyBolt eb = bolt.GetComponent<EnemyBolt>();
            if (eb != null)
            {
                eb.speed = bulletSpeed;
                eb.damage = bulletDamage;
            }
        }
    }
}
