using UnityEngine;
using System.Collections;

public class StreamEmitter : Enemy
{
    [Header("StreamEmitter固有設定")]
    public GameObject bulletPrefab;
    public float stopY = 5.0f;
    public int burstCount = 5;
    public float fireRate = 0.1f;

    [Header("移動の滑らかさ設定")]
    public float acceleration = 8f; // 加速度（小さいほどゆっくり動き出す）
    private Vector2 targetVelocity; // 目標となる速度

    private bool isShooting = false;
    private float aimAngle;
    private Transform playerPos;

    void Start()
    {
        // プレイヤーの存在チェックを追加（エラー防止）
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerPos = player.transform;

        Initialize();
    }

    void Update()
    {
        removeOffscreen();

        // Updateでは「目標速度」を決定するだけにする
        UpdateTargetVelocity();
    }

    void FixedUpdate()
    {
        // 実際の速度変更は物理演算のタイミングで行う
        // 現在の速度から目標速度へ、accelerationの分だけじわっと近づける
        rb.linearVelocity = Vector2.MoveTowards(
            rb.linearVelocity,
            targetVelocity,
            acceleration * Time.fixedDeltaTime
        );
    }

    void UpdateTargetVelocity()
    {
        // 射撃中でない、かつ停止位置より上なら下へ移動
        if (!isShooting && transform.position.y > stopY)
        {
            targetVelocity = Vector2.down * moveSpeed;
        }
        else if (!isShooting && transform.position.y <= stopY)
        {
            // 停止位置に到達
            targetVelocity = Vector2.zero;

            // 速度がほぼゼロになったら射撃開始
            if (rb.linearVelocity.magnitude < 0.05f)
            {
                StartCoroutine(ShootSequence());
            }
        }
        else if (isShooting)
        {
            // 射撃中は停止
            targetVelocity = Vector2.zero;
        }
        else
        {
            // 射撃が終わって再始動（stopYが-10fになっている状態）
            targetVelocity = Vector2.down * moveSpeed;
        }
    }

    IEnumerator ShootSequence()
    {
        isShooting = true;

        if (playerPos != null)
        {
            Vector2 toPlayer = (Vector2)playerPos.position - (Vector2)transform.position;
            aimAngle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;
        }

        for (int set = 0; set < 3; set++)
        {
            for (int i = 0; i < burstCount; i++)
            {
                float spread = Random.Range(-5f, 5f);
                Shoot(aimAngle + spread);
                yield return new WaitForSeconds(fireRate);
            }
            yield return new WaitForSeconds(0.7f);
        }

        isShooting = false;
        stopY = -10f; // 目標停止位置を画面外に更新して再始動させる
    }

    void Shoot(float angleDeg)
    {
        if (bulletPrefab == null || playerPos == null) return;

        // transform.up が前進方向のとき（弾の向き調整）
        Quaternion rotation = Quaternion.Euler(0, 0, angleDeg - 90f);
        GameObject bolt = Instantiate(bulletPrefab, transform.position, rotation);

        EnemyBolt eb = bolt.GetComponent<EnemyBolt>();
        if (eb != null)
        {
            eb.speed = 3;
            eb.damage = 3;
        }
    }
}