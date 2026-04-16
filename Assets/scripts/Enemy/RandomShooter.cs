using UnityEngine;
using System.Collections;

/// <summary>
/// 真ん中あたりまで下りてきて、一定時間ランダムな方向に弾幕を放ち、画面外（下）に移動していく敵
/// StreamEmitterと同構造。停止Y座標に到達→弾幕→下へ退場
/// </summary>
public class RandomShooter : Enemy
{
    [Header("RandomShooter設定")]
    public GameObject bulletPrefab;
    public float stopY = 0f;           // 停止するY座標（画面中央あたり）
    public int burstCount = 8;         // 1セットの弾数
    public int burstSets = 3;          // 弾幕セット数
    public float fireRate = 0.08f;     // 弾と弾の間隔
    public float setBetweenDelay = 0.5f; // セット間の間隔

    [Header("移動の滑らかさ")]
    public float acceleration = 6f;
    private Vector2 targetVelocity;

    private bool isShooting = false;
    private bool hasShotOnce = false;  // 一度射撃したかフラグ

    void Start()
    {
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
        if (!isShooting && !hasShotOnce && transform.position.y > stopY)
        {
            // 停止位置まで下降
            targetVelocity = Vector2.down * moveSpeed;
        }
        else if (!isShooting && !hasShotOnce && transform.position.y <= stopY)
        {
            // 停止位置に到達、静止待ち
            targetVelocity = Vector2.zero;
            if (rb.linearVelocity.magnitude < 0.05f)
            {
                StartCoroutine(ShootSequence());
            }
        }
        else if (isShooting)
        {
            // 射撃中は静止
            targetVelocity = Vector2.zero;
        }
        else
        {
            // 射撃終了後、下へ退場
            targetVelocity = Vector2.down * moveSpeed;
        }
    }

    IEnumerator ShootSequence()
    {
        isShooting = true;

        for (int set = 0; set < burstSets; set++)
        {
            for (int i = 0; i < burstCount; i++)
            {
                // 完全ランダムな方向に発射
                float randomAngle = Random.Range(0f, 360f);
                Shoot(randomAngle);
                yield return new WaitForSeconds(fireRate);
            }
            if (set < burstSets - 1)
            {
                yield return new WaitForSeconds(setBetweenDelay);
            }
        }

        isShooting = false;
        hasShotOnce = true; // 射撃完了フラグを立てて退場フェーズへ
    }

    void Shoot(float angleDeg)
    {
        if (bulletPrefab == null) return;

        Quaternion rotation = Quaternion.Euler(0, 0, angleDeg - 90f);
        GameObject bolt = Instantiate(bulletPrefab, transform.position, rotation);

        EnemyBolt eb = bolt.GetComponent<EnemyBolt>();
        if (eb != null)
        {
            eb.speed = 3f;
            eb.damage = 2f;
        }
    }
}
