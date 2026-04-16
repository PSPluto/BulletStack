using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemys;

    public void SpawnEnemy()
    {
        GameObject obj = Instantiate((enemys[Random.Range(0,enemys.Length)]), new Vector3(Random.Range(-2, 3), 10, 0), Quaternion.identity);
    }
    Coroutine _activeLoop;
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
        while (true)
        {
            // タイトルのループ処理
            yield return null;
        }
    }

    IEnumerator InGameLoop()
    {
        int count = 0;
        while (true)
        {
            if(count >= 600)
            {
                count = 0;
                SpawnEnemy();
            }
            else
            {
                count += 1;
            }
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
}


