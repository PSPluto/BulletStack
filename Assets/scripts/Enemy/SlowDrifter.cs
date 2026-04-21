using System.Collections;
using UnityEngine;

/// <summary>
/// ゆっくり落ちていく。プレイヤーに触れるとダメージを与える敵
/// OnContactWithPlayer をオーバーライドしてダメージ処理を実装
/// </summary>
public class SlowDrifter : Enemy
{
    [Header("SlowDrifter設定")]
    public float contactDamage = 5f;  
    public float damageCooldown = 1f;

    [Header("移動設定")]
    public float acceleration = 2f;    

    private float damageTimer = 0f;
    private bool canDamage = true;

    public Transform spriteTransform;
    private float transformDiray = 0.5f;


    IEnumerator SpriteAnim()
    {
        while (true)
        {
            spriteTransform.Rotate(0, 0, 45);
            yield return new WaitForSeconds(transformDiray);
        }

    }
    void Start()
    {
        Initialize();
        StartCoroutine(SpriteAnim());
    }
        void Update()
    {
        removeOffscreen();

        if (!canDamage)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageCooldown)
            {
                canDamage = true;
                damageTimer = 0f;
            }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DealContactDamage(other);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DealContactDamage(other);
        }
    }

    private void DealContactDamage(Collider2D other)
    {
        if (!canDamage) return;
        MotherBoard mb = other.GetComponent<MotherBoard>();
        if (mb != null)
        {
            mb.TakeDamage(damage);
        }

        canDamage = false;
        OnContactWithPlayer();
    }

    public override void OnContactWithPlayer()
    {
        base.OnContactWithPlayer();
    }
}
