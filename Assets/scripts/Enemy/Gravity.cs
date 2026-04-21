using UnityEngine;
using System.Collections;

/// <summary>
/// 真ん中あたりまで下りてきて、一定時間ランダムな方向に弾幕を放ち、画面外（下）に移動していく敵
/// StreamEmitterと同構造。停止Y座標に到達→弾幕→下へ退場
/// </summary>
public class Gravity : Enemy
{
    [Header("静止位置設定")]
    public float stopY = 4f;

    [Header("移動の滑らかさ")]
    public float acceleration = 6f;
    private Vector2 targetVelocity;
    private bool isStay = false;


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
        if (!isStay && transform.position.y > stopY)
        {
            // 停止位置まで下降
            targetVelocity = Vector2.down * moveSpeed;
        }
        else if (!isStay && transform.position.y <= stopY)
        {
            // 停止位置に到達、静止待ち
            targetVelocity = Vector2.zero;
            if (rb.linearVelocity.magnitude < 0.05f)
            {
                StartCoroutine(ShootSequence());
            }
        }
        else if (isStay)
        {
            targetVelocity = Vector2.zero;
        }
        else
        {
            targetVelocity = Vector2.down * moveSpeed;
        }
    }

    IEnumerator ShootSequence()
    {
        isStay = true;

        yield return new WaitForSeconds(4);
        isStay = false;
    }
}
