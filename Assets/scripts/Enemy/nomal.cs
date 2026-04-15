using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
public class Nomal : Enemy
{
    private void Update()
    {
        rb.linearVelocity = new Vector2(0, - moveSpeed);
        removeOffscreen();
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
    public override void OnContactWithPlayer()
    {
        //プレイヤーに触れたときの処理
    }
}
