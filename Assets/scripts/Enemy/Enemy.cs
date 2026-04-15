using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;


    public float maxHP = 3;
    public float currentHP;
    public float moveSpeed = 4;

    public float ScoreValue = 100;
    public float XPValue = 10;

    //無敵フラグ
    public bool isInvincible = false;

    void Start()
    {
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
    public virtual void AddValue(float addScore , float addXP)
    {
        
    }
    public virtual void Die()
    {

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
