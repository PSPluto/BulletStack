using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class MotherBoard : MonoBehaviour
{
    Coroutine _activeLoop;
    public GameObject boltPrefab;
    public PlayerInventory pInventory;
    public bool canFire = true;
    public List<Modifier> circuit = new List<Modifier>();
    public float mindiray = 0.05f;

    public float resultVoltage;
    public float resultBaseDamage;
    public int   resultPelletCount;
    public float resultBulletSpeed;
    public float resultSkipProbability;
    public float resultModMultiplier;
    public float resultMaxSpreadAngle;

    public float maxHP = 40;
    public float currentHP;

    public float currentXP;
    public float levelUpXpValue;
    public int   currentLevel;
    public int   score;


    public float TimeToFire = 0f;
    public float resultTimeToFire = 0f;

    public bool isInGameLoop = false;

    public List<StatsDisplay> displayList = new List<StatsDisplay>();

    public AudioClip shotSound;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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
        isInGameLoop = false;
        while (true)
        {
            yield return null;
        }
    }

    IEnumerator InGameLoop()
    {
        Initialize();
        isInGameLoop = true;
        yield break;
    }

    IEnumerator GameOverLoop()
    {
        isInGameLoop = false;
        while (true)
        {
            // ゲームオーバーのループ処理
            yield return null;
        }
    }

    void Update()
    {
        if (isInGameLoop == true)
        {
            Fire();
        }    
    }

    public void Initialize()
    {
        currentHP = maxHP;
        currentXP = 0f;
        currentLevel = 1;
        levelUpXpValue = 200f;
        score = 0;
        circuit.Clear();

    }


    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Gameover();
        }
    }
    public void Gameover()
    {
        StateManager.ChangeState(2);
    }






    public void Fire()
    {
        if (canFire == true)
        {
            StartCoroutine(ExecuteCircuit());
        }
    }

    public Quaternion SetRotateOffset(float maxAngle)
    {
        float myAngle = transform.rotation.eulerAngles.y;
        float rundomOffset = Random.Range(-maxAngle, maxAngle);
        Quaternion bulletRotation = Quaternion.Euler(0, 0, myAngle + rundomOffset);
        return bulletRotation;
    }

    IEnumerator ExecuteCircuit()
    {
        TimeToFire = 0f;
        Signal s = new Signal(
            voltage: 14f,
            baseDamage: 4f,
            pelletCount: 1,
            bulletSpeed: 6f,
            modMultiplier: 1f,
            skipProbability: 0f,
            maxSpreadAngle: 5f
        );

        List<Modifier> circuitSnapshot = new List<Modifier>(circuit);

        canFire = false;

        for (int i = 0; i < circuitSnapshot.Count; i++)
        {
            if (circuitSnapshot[i] == null)
            {
                continue;
            }

            TimeToFire += (circuitSnapshot[i].resistance / 5f)/s.voltage;
            yield return new WaitForSeconds((circuitSnapshot[i].resistance / 5f)/s.voltage);

            s.voltage -= circuitSnapshot[i].voltageCost;
            circuitSnapshot[i].Process(s);

            if (s.voltage <= 0)
            {
                yield return new WaitForSeconds(mindiray);
                SetBreakValues();
                UpdateDisplays();
                canFire = true;
                yield break;
            }
        }
        TimeToFire +=(mindiray);
        yield return new WaitForSeconds(mindiray);
        
        SetFinalResults(s);
        UpdateDisplays();

        SpawnBolts();

        //Debug.Log($"最終：ダメージ={resultBaseDamage}, 電圧={resultVoltage}, 散弾数={resultPelletCount}, 弾速={resultBulletSpeed}, モディファイア適用倍率={resultModMultiplier}, パケットロス確率={resultSkipProbability}, 最大拡散角度={resultMaxSpreadAngle}");

        canFire = true;
        yield break;
    }


    private void SpawnBolts()
    {
        for (int i = 1; i <= resultPelletCount; i++)
        {
            GameObject Bulet = Instantiate(boltPrefab, transform.position, SetRotateOffset(resultMaxSpreadAngle));
            Bolt boltScript = Bulet.GetComponent<Bolt>();
            boltScript.damage = resultBaseDamage;
            boltScript.speed = resultBulletSpeed;
        }
        _audioSource.pitch = 1f + Random.Range(-0.1f, 0.1f);
        _audioSource.PlayOneShot(shotSound);
    }
    private void SetFinalResults(Signal s)
    {
        resultBaseDamage = s.baseDamage;
        resultVoltage = s.voltage;
        resultPelletCount = s.pelletCount;
        resultBulletSpeed = s.bulletSpeed;
        resultSkipProbability = s.skipProbability;
        resultModMultiplier = s.modMultiplier;
        resultMaxSpreadAngle = s.maxSpreadAngle;
        resultTimeToFire = TimeToFire;
    }
    private void SetBreakValues()
    {
        resultBaseDamage = 0;
        resultVoltage = 0;
        resultPelletCount = 0;
        resultBulletSpeed = 0;
        resultSkipProbability = 0;
        resultModMultiplier = 0;
        resultMaxSpreadAngle = 0;
        resultTimeToFire = TimeToFire;
    }
    public void UpdateDisplays()
    {
        foreach (StatsDisplay display in displayList)
        {
            display.UpdateDisplay();
        }
    }

    public void UnEquipItem(int index)
    {
        this.circuit.Remove(circuit[index]);
        pInventory.playerInventry.Add(circuit[index]);
    }

    //装備順の入れ替え
    public void SwapItem(int index1 ,int index2)
    {

    }
}
