using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
public class Nomal : Enemy
{
    private void Update()
    {
        rb.linearVelocity = new Vector2(0, - moveSpeed);
    }
    //public override void TakeDamage(float damage)
    //{
    //    // ダメージを受けたとき
    //    if (isInvincible) return;
    //    currentHP -= damage;
    //    if (currentHP <= 0)
    //    {
    //        Die();
    //    }
    //}
    public virtual void Die()
    {
        Destroy(gameObject);
    }
    public override void OnContactWithPlayer()
    {
        //プレイヤーに触れたときの処理
    }
}
