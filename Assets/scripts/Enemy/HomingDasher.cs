using UnityEngine;

/// <summary>
/// 名前: ホーミングダッシャー (HomingDasher)
/// HP: 中 (8) ・ XP: 中 (15) ・ ScoreValue: 150
/// 下降中はプレイヤーのX座標を追尾しながらゆっくり接近し、
/// 一定のY座標に到達するとプレイヤーへ向けて高速ダッシュする敵。
/// </summary>
public class HomingDasher : Enemy
{
    [Header("HomingDasher設定")]
    public float homingSpeed = 2.5f;
    public float dashSpeed = 12f;
    public float dashTriggerY = 2.5f;
    public float homingAcceleration = 4f;
    public float dashAcceleration = 20f;

    private bool isDashing = false;
    private Vector2 targetVelocity;

    void Start()
    {
        maxHP = 8f;
        XPValue = 15f;
        ScoreValue = 150;

        Initialize();
    }

    void Update()
    {
        removeOffscreen();
        UpdateTargetVelocity();
    }

    void FixedUpdate()
    {
        float accel = isDashing ? dashAcceleration : homingAcceleration;
        rb.linearVelocity = Vector2.MoveTowards(
            rb.linearVelocity,
            targetVelocity,
            accel * Time.fixedDeltaTime
        );
    }

    void UpdateTargetVelocity()
    {
        if (isDashing) return;

        if (transform.position.y <= dashTriggerY)
        {
            StartDash();
            return;
        }

        float dirX = 0f;
        if (player != null)
        {
            dirX = Mathf.Sign(player.transform.position.x - transform.position.x);
        }
        targetVelocity = new Vector2(dirX * homingSpeed, -moveSpeed);
    }

    void StartDash()
    {
        isDashing = true;
        Vector2 dir = Vector2.down;
        if (player != null)
        {
            dir = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        }
        targetVelocity = dir * dashSpeed;
    }
}
