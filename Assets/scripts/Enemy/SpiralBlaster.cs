using UnityEngine;

/// <summary>
/// 名前: スパイラルブラスター (SpiralBlaster)
/// HP: 高め (12) ・ XP: 多め (20) ・ ScoreValue: 200
/// ゆっくり降下しながら、回転する1方向から連続して弾を発射し、
/// 螺旋状の弾幕を形成する敵。
/// </summary>
public class SpiralBlaster : Enemy
{
    [Header("SpiralBlaster設定")]
    public GameObject bulletPrefab;
    public float rotateSpeed = 140f;    // 螺旋の回転速度
    public float fireInterval = 0.12f;  // 発射間隔（短いほど密な螺旋）
    public float bulletSpeed = 3.5f;
    public float bulletDamage = 2f;

    [Header("移動設定")]
    public float acceleration = 3f;

    private float currentAngle = 0f;
    private float fireTimer = 0f;

    void Start()
    {
        maxHP = 12f;
        XPValue = 20f;
        ScoreValue = 200;

        Initialize();
    }

    void Update()
    {
        removeOffscreen();

        currentAngle += rotateSpeed * Time.deltaTime;

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            fireTimer = 0f;
            FireSpiralBullet();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = Vector2.MoveTowards(
            rb.linearVelocity,
            Vector2.down * moveSpeed,
            acceleration * Time.fixedDeltaTime
        );
    }

    void FireSpiralBullet()
    {
        if (bulletPrefab == null) return;

        Quaternion rotation = Quaternion.Euler(0, 0, currentAngle - 90f);
        ShotSound();
        GameObject bolt = Instantiate(bulletPrefab, transform.position, rotation);

        EnemyBolt eb = bolt.GetComponent<EnemyBolt>();
        if (eb != null)
        {
            eb.speed = bulletSpeed;
            eb.damage = bulletDamage;
        }
    }
}
