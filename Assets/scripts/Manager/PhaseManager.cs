using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    Coroutine _activeLoop;
    public MotherBoard player;
    public RewordInventrySystem rewordInventrySystem;
    public EnemySpawner enemySpawner;
    public TMP_Text wavetext;
    public Animator OKButton;
    private int lastLv;

    public static PhaseManager Instance { get; private set; }

    public int wave = 0;
    public List<int> costWheight = new List<int>();
    [Header("敵の出現陣形を入れる。")]
    public List<BaseEnemyPattern> EnemyPatterns;
    public bool isSpawning;
    public bool isIngame = false;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        OKButton.gameObject.SetActive(false);

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
        isIngame = false;
        wave = 0;
        while (true)
        {
            yield return null;
        }
    }

    IEnumerator InGameLoop()
    {
        lastLv = 1;
        isIngame = true;
        isSpawning = false;
        wave = 0;
        //StartCoroutine(SpawnEnemys());
        wave = wave + 1;
        SpawnNewEnemy();
        wavetext.text = ($"Wave:{wave}");


        while (true)
        {
            yield return null;
        }
    }

    IEnumerator GameOverLoop()
    {
        isIngame = false;
        while (true)
        {
            // ゲームオーバーのループ処理
            yield return null;
        }
    }


    IEnumerator SpawnEnemys()
    {
        foreach(int cost in costWheight)
        {
            isSpawning = true;
            //cost：コストウェイトの値
            List<BaseEnemyPattern> resultEnemyTable = FilterTable(cost);
            if (resultEnemyTable.Count < 1)
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }
            SpawnPattern(resultEnemyTable[RNGManager.Reward.Next(0, resultEnemyTable.Count)].prefabs);
            yield return new WaitForSeconds(1);
        }
        isSpawning = false;
        yield break;
    }
    public void SpawnPattern(GameObject[] spawnPrefab)
    {
        for (int i = 0; i < spawnPrefab.Length; i++)
        {
            if (spawnPrefab[i] != null)
            {
                if (isIngame)
                {
                    enemySpawner.SpawnEnemy(spawnPrefab[i], i - 2);
                }

            }
        }
    }
    public List<BaseEnemyPattern> FilterTable(int filterCost)
    {
        List<BaseEnemyPattern> resultTable = new List<BaseEnemyPattern>();
        foreach(BaseEnemyPattern pattern in EnemyPatterns)
        {
            if(pattern.cost == filterCost)
            {
                resultTable.Add(pattern);
            }
        }
        return resultTable;
    }

    public void PhaseEndCheck()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if ((enemyCount <= 0) && isSpawning == false)
        {
            if (player.currentLevel != lastLv)
            {
                wavetext.text = ($"~設計タイム~");
                OKButton.gameObject.SetActive(true);
                OKButton.SetBool("Show", true);
            }
            else
            {
                NextWave();
            }
            lastLv = player.currentLevel;
        }
    }
    public void CloseBtn()
    {
        OKButton.SetBool("Show",false);
    }
    public void NextWave()
    {
        StartCoroutine(NextPhaseRoutine());
    }
    IEnumerator NextPhaseRoutine()
    {
        yield return new WaitForSeconds(0f);
        //Debug.Log("新しいフェーズの始まり");
        wave = wave + 1;
        SpawnNewEnemy();
        wavetext.text = ($"Wave:{wave}");
        //Debug.Log(wave);
    }

    private void SpawnNewEnemy()
    {
        // 敵パターンのテーブルを更新
        // コストの計算
        CostInit((wave * 2) + 4,RNGManager.Gameplay.Next(wave/2+1, wave+1));
        Debug.Log($"計算結果：[{string.Join(", ", costWheight)}]");

        StartCoroutine(SpawnEnemys());
        // 仮
        //for (int i = 0; i < 2 + wave; i++)
        //{
        //    enemySpawner.SpawnEnemy();
        //}

    }
    private void CostInit(int totalCost, int listLength = 3)
    {
        costWheight = new List<int>();
        List<int> temporaryCostList = new List<int>();
        for (int i = 0; i != listLength; i++ )
        {
            // コストを等分
            temporaryCostList.Add(totalCost / listLength);
        }
        // コストのランダム化
        for (int i = 0; i != listLength; i++)
        {
            if (i == listLength - 1)
            {
                costWheight.Add(temporaryCostList[i]);
                return;
            }
            int indexCost = RNGManager.Gameplay.Next(1, temporaryCostList[i]);
            if (indexCost > 10)
            {
                indexCost = 10;
            }
            costWheight.Add(indexCost);
            temporaryCostList[i] -= indexCost;
            temporaryCostList[i + 1] += temporaryCostList[i];
        }
    }

}
