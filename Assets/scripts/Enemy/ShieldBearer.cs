using System.Collections;
using UnityEngine;

/// <summary>
/// 名前: シールドベアラー (ShieldBearer)
/// HP: 高め (20) ・ XP: 多め (28) ・ ScoreValue: 300
/// 一定間隔でシールド(無敵状態)を展開・解除する敵。
/// シールド解除中のみダメージを受け、その間に全方位弾を放つ。
/// </summary>
public class ShieldBearer : Enemy
{
    [Header("ShieldBearer設定")]
    public GameObject bulletPrefab;
    public float shieldUpDuration = 2.5f;
    public float shieldDownDuration = 2.0f;
    public int directions = 8;
    public float bulletSpeed = 3.5f;
    public float bulletDamage = 2.5f;

    [Header("移動設定")]
    public float acceleration = 3f;

    void Start()
    {
        maxHP = 20f;
        XPValue = 28f;
        ScoreValue = 300;

        Initialize();
        StartCoroutine(ShieldCycle());
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

    IEnumerator ShieldCycle()
    {
        while (true)
        {
            // シールド展開中（無敵）
            isInvincible = true;
            yield return new WaitForSeconds(shieldUpDuration);

            // シールド解除（被弾可能）＆攻撃
            isInvincible = false;
            ShootAllDirections();
            yield return new WaitForSeconds(shieldDownDuration);
        }
    }

    void ShootAllDirections()
    {
        if (bulletPrefab == null) return;

        float angleStep = 360f / directions;
        ShotSound();
        for (int i = 0; i < directions; i++)
        {
            float angle = angleStep * i;
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
