using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public MotherBoard player;

    public float maxHP = 6;
    public float currentHP;
    public float moveSpeed = 4;

    public int ScoreValue = 100;
    public float XPValue = 10;

    public float damage;
    [SerializeField]private AudioClip dieSE;
    [SerializeField]private AudioClip shotSE;
    [SerializeField]private GameObject dieParticlePrefab;
    [SerializeField]private ShakeSystem shakeSystem;

    //無敵フラグ
    public bool isInvincible = false;

    void Start()
    {
        Initialize();
        //shakeSystem = GetComponentInChildren<ShakeSystem>();
    }
    private void Update()
    {
        removeOffscreen();
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
        //shakeSystem.Shake(0.2f, 0.05f);
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public virtual void AddValue(int addScore , float addXP)
    {
        player.currentXP += addXP;
        player.score += addScore;
        player.LevelUpCheck();
    }
    public virtual void Die()
    {
        AddValue(ScoreValue, XPValue);
        gameObject.tag = "Untagged";
        ParticleManager.Instance.CreateParticle(dieParticlePrefab, this.transform.position, transform.rotation);
        Destroy(gameObject);
        PhaseManager.Instance.PhaseEndCheck();
        AudioManager.Instance.Playsound(dieSE);

    }
    public virtual void OnContactWithPlayer()
    {
        //プレイヤーに触れたときの処理
    }

    public virtual void removeOffscreen()
    {
        if (transform.position.y <= -6)
        {
            gameObject.tag = "Untagged";
            Destroy(gameObject);
            PhaseManager.Instance.PhaseEndCheck();
        }
    }
    public virtual void ShotSound()
    {
        AudioManager.Instance.Playsound(shotSE);
    }


    /// <summary>
    /// コルーチン
    /// </summary>
    Coroutine _activeLoop;
    void OnEnable()
    {
        StateManager.OnStateChanged += StateChanged;
    }
    void OnDisable()
    {
        StateManager.OnStateChanged -= StateChanged;
    }

    public void StateChanged(int newState)
    {
        if (_activeLoop != null)
        {
            StopCoroutine(_activeLoop);
            _activeLoop = null;
        }
        switch (newState)
        {
            case 0: _activeLoop = StartCoroutine(TitleLoop()); break;
            case 1: _activeLoop = StartCoroutine(InGameLoop()); break;
            case 2: _activeLoop = StartCoroutine(GameOverLoop()); break;
            default: break;
        }
    }
    IEnumerator TitleLoop()
    {
        Destroy(gameObject);
        yield break;
    }

    IEnumerator InGameLoop()
    {
            yield return null;
    }

    IEnumerator GameOverLoop()
    {
        while (true)
        {
            // ゲームオーバーのループ処理
            yield return null;
        }
    }
}
