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
    public float acceleration = 8f;   // 加速度（小さいほどゆっくり動き出す）
    private Vector2 targetVelocity;   // 目標となる速度

    private bool isShooting = false;
    private float aimAngle;
    private Transform playerPos;

    void Start()
    {
        InitializePlayerPosition();
        Initialize();
    }

    void Update()
    {
        removeOffscreen();
        UpdateTargetVelocity();
    }

    void FixedUpdate()
    {
        ApplySmoothMovement();
    }

    /// <summary>
    /// プレイヤーの初期位置を設定・取得
    /// </summary>
    private void InitializePlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerPos = player.transform;
        }
    }

    private void UpdateTargetVelocity()
    {
        // 射撃中は移動を停止
        if (isShooting)
        {
            targetVelocity = Vector2.zero;
            return;
        }

        // 停止位置より上にいるなら下へ移動
        if (transform.position.y > stopY)
        {
            targetVelocity = Vector2.down * moveSpeed;
            return;
        }

        // 停止位置に到達し、まだ射撃が始まっていない場合
        targetVelocity = Vector2.zero;

        // 速度が十分に落ちたら射撃シーケンスを開始
        if (rb.linearVelocity.magnitude < 0.05f)
        {
            StartCoroutine(ShootSequence());
        }
    }

    /// <summary>
    /// 目標速度にむけて滑らかに補間する
    /// </summary>
    private void ApplySmoothMovement()
    {
        rb.linearVelocity = Vector2.MoveTowards(
            rb.linearVelocity,
            targetVelocity,
            acceleration * Time.fixedDeltaTime
        );
    }

    /// <summary>
    /// 一連の射撃ループ処理
    /// </summary>
    IEnumerator ShootSequence()
    {
        isShooting = true;

        // 3セットのバースト射撃
        for (int set = 0; set < 3; set++)
        {
            LockOnPlayer();
            yield return StartCoroutine(BurstShootRoutine());
            yield return new WaitForSeconds(0.7f); // セット間のインターバル
        }

        // 射撃終了後、画面外へ退避させるための設定
        isShooting = false;
        stopY = -10f;
    }

    /// <summary>
    /// プレイヤーへの角度を計算してロックオンする
    /// </summary>
    private void LockOnPlayer()
    {
        if (playerPos != null)
        {
            Vector2 toPlayer = (Vector2)playerPos.position - (Vector2)transform.position;
            aimAngle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;
        }
    }

    /// <summary>
    /// 1セット分の連射（バースト）を実行するコルーチン
    /// </summary>
    IEnumerator BurstShootRoutine()
    {
        for (int i = 0; i < burstCount; i++)
        {
            float spread = ((float)RNGManager.Player.NextDouble() * 10f) - 5f;
            EmitBullet(aimAngle + spread);
            yield return new WaitForSeconds(fireRate);
        }
    }

    /// <summary>
    /// 指定された角度に弾を1発生成する
    /// </summary>
    private void EmitBullet(float angleDeg)
    {
        if (bulletPrefab == null || playerPos == null) return;

        Quaternion rotation = Quaternion.Euler(0, 0, angleDeg - 90f);
        ShotSound();
        GameObject bolt = Instantiate(bulletPrefab, transform.position, rotation);

        EnemyBolt eb = bolt.GetComponent<EnemyBolt>();
        if (eb != null)
        {
            eb.speed = 3;
            eb.damage = 3;
        }
    }
}