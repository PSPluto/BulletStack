using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public MotherBoard player;

    public float maxHP = 6;
    public float currentHP;
    public float moveSpeed = 4;

    public int ScoreValue = 100;
    public float XPValue = 10;

    //無敵フラグ
    public bool isInvincible = false;

    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        player = Object.FindAnyObjectByType<MotherBoard>();
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float damage)
    {
        // ダメージを受けたとき
        if (isInvincible == true) return;
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }
    public virtual void AddValue(int addScore , float addXP)
    {
        player.currentXP += addXP;
        player.score += addScore;
    }
    public virtual void Die()
    {
        AddValue(ScoreValue, XPValue);
        Destroy(gameObject);
    }
    public virtual void OnContactWithPlayer()
    {
        //プレイヤーに触れたときの処理
    }

    public virtual void removeOffscreen()
    {
        if (transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
    }
}
