using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    Coroutine _activeLoop;
    public RewordInventrySystem rewordInventrySystem;
    public EnemySpawner enemySpawner;

    public static PhaseManager Instance { get; private set; }

    public int wave = 0;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {


    }
    void OnEnable()
    {
        StateManager.OnStateChanged += StateChanged;
    }
    void OnDisable()
    {
        StateManager.OnStateChanged -= StateChanged;
    }

    // state更新のイベントで呼び出される
    public void StateChanged(int newState)
    {
        // 今のループを止める
        if (_activeLoop != null)
        {
            StopCoroutine(_activeLoop);
            _activeLoop = null;
        }
        // 次のループを始める
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
        wave = 0;
        while (true)
        {
            yield return null;
        }
    }

    IEnumerator InGameLoop()
    {
        wave = 0;
        SpawnNewEnemy();
        wave = wave + 1;


        while (true)
        {
            yield return null;
        }
    }

    IEnumerator GameOverLoop()
    {
        while (true)
        {
            // ゲームオーバーのループ処理
            yield return null;
        }
    }

    public void PhaseEndCheck()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount - 0 <= 0)
        {
            Debug.Log("フェーズ終了！");
            Debug.Log("コルーチン呼び出し");
            StartCoroutine(NextPhaseRoutine());
        }
    }

    IEnumerator NextPhaseRoutine()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("新しいフェーズの始まり");
        SpawnNewEnemy();
        wave = wave + 1;
        Debug.Log(wave);
    }

    IEnumerator Weit(int sec)
    {
        yield return new WaitForSeconds(sec);
    }

    private void SpawnNewEnemy()
    {
        for (int i = 0; i < 2 + wave; i++)
        {
            enemySpawner.SpawnEnemy();
        }
    }

}