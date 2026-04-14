using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;

    //2. 共通の「動き」のルール（メソッド）
    //TakeDamage(float damage) : ダメージを受ける処理。HPを減らす,死んだか判定する

    //Die() : 死ぬ時の処理。「爆発エフェクトを出す」「オブジェクトを消す」など。

    //OnContactWithPlayer() : プレイヤーにぶつかった時の処理。
    public float maxHP = 3;
    public float currentHP;
    public float moveSpeed = 4;

    public float ScoreValue = 100;
    public float XPValue = 10;

    public bool isInvincible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float damage)
    {
        if (isInvincible) return;
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
    public virtual void OnContactWithPlayer()
    {
        //プレイヤーに触れたときの処理
    }
}
