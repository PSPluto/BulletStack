using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemys;
    [SerializeField] GameObject shieldPrefab;

    public void SpawnEnemy(GameObject prefab,int xPos)
    {
        GameObject obj = Instantiate(prefab, new Vector3(xPos, 10, 0), Quaternion.identity);
        if (RNGManager.Gameplay.Next(0, 6) == 0)
        {
            Instantiate(shieldPrefab, obj.transform);
        }
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
}


