using UnityEngine;
using System.Collections;

/// <summary>
/// 画面中央付近まで下降 → 一定時間停止 → 画面下へ
/// </summary>
public class Gravity : Enemy
{
    [Header("停止Y座標")]
    public float stopY = 4f;

    [Header("速度補間の加速度")]
    public float acceleration = 6f;

    public float gravityRadius = 5;
    public LayerMask bulletLayer;

    // attractForce フィールドを追加
    [Header("重力吸引力")]
    public float attractForce = 10f;

    private Vector2 targetVelocity;

    private enum Phase { Descending, Staying, Leaving }
    private Phase phase = Phase.Descending;

    void Start() => Initialize();

    void Update()
    {
        removeOffscreen();
        UpdateTargetVelocity();
        AddGravityForce();
        
    }

    void AddGravityForce()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, gravityRadius, bulletLayer);

        foreach (var hit in hitColliders)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);

            float forceFactor = 1.0f - (dist / gravityRadius);

            Bolt bolt = hit.GetComponent<Bolt>();
            if (bolt != null)
            {
                // 距離に応じた力を渡す
                bolt.Attract(transform.position, attractForce * forceFactor);
            }
        }
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
        switch (phase)
        {
            case Phase.Descending:
                if (transform.position.y > stopY)
                {
                    targetVelocity = Vector2.down * moveSpeed;
                }
                else
                {
                    // 停止位置に到達
                    targetVelocity = Vector2.zero;
                    if (rb.linearVelocity.magnitude < 0.05f)
                    {
                        phase = Phase.Staying;
                        StartCoroutine(ShootSequence());
                    }
                }
                break;

            case Phase.Staying:
                targetVelocity = Vector2.zero;
                break;

            case Phase.Leaving:
                targetVelocity = Vector2.down * moveSpeed;
                break;
        }
    }

    IEnumerator ShootSequence()
    {
        yield return new WaitForSeconds(10f);

        

        phase = Phase.Leaving;
    }


}