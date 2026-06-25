using System.Collections;
using UnityEngine;

/// <summary>
/// 名前: レーザースナイパー (LaserSniper)
/// HP: 中 (10) ・ XP: 多め (25) ・ ScoreValue: 220
/// 停止位置まで降りてきて、プレイヤーを狙ってチャージ後に
/// 高速・高威力の狙撃弾を発射する敵。指定回数撃ったら退場する。
/// </summary>
public class LaserSniper : Enemy
{
    [Header("LaserSniper設定")]
    public GameObject bulletPrefab;
    public float stopY = 3f;
    public float chargeTime = 0.8f;
    public int shotCount = 3;
    public float shotInterval = 1.2f;
    public float bulletSpeed = 9f;     // 通常弾より速い
    public float bulletDamage = 6f;    // 通常弾より高威力

    [Header("移動設定")]
    public float acceleration = 6f;
    private Vector2 targetVelocity;

    private bool isFiring = false;

    void Start()
    {
        maxHP = 10f;
        XPValue = 25f;
        ScoreValue = 220;

        Initialize();
    }

    void Update()
    {
        removeOffscreen();
        UpdateTargetVelocity();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = Vector2.MoveTowards(
            rb.linearVelocity,
            targetVelocity,
            acceleration * Time.fixedDeltaTime
        );
    }

    void UpdateTargetVelocity()
    {
        if (isFiring)
        {
            targetVelocity = Vector2.zero;
            return;
        }

        if (transform.position.y > stopY)
        {
            targetVelocity = Vector2.down * moveSpeed;
            return;
        }

        targetVelocity = Vector2.zero;
        if (rb.linearVelocity.magnitude < 0.05f)
        {
            StartCoroutine(SniperSequence());
        }
    }

    IEnumerator SniperSequence()
    {
        isFiring = true;

        for (int i = 0; i < shotCount; i++)
        {
            // チャージ（プレイヤーに照準を合わせて待機＝攻撃の予兆）
            yield return new WaitForSeconds(chargeTime);
            FireAtPlayer();
            yield return new WaitForSeconds(shotInterval);
        }

        // 射撃終了後、画面外へ退場
        isFiring = false;
        stopY = -10f;
    }

    void FireAtPlayer()
    {
        if (bulletPrefab == null || player == null) return;

        Vector2 toPlayer = (Vector2)player.transform.position - (Vector2)transform.position;
        float angleDeg = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angleDeg - 90f);
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
