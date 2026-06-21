using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultDisplay : MonoBehaviour
{
    public TMPro.TMP_Text waveText;
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text newRecordText;
    public TMPro.TMP_Text SeedText;
    public MotherBoard player;
    public Animator anim;
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
        anim.SetBool("IsShow", false);
        while (true)
        {
            yield return null;
        }
    }

    IEnumerator InGameLoop()
    {

        anim.SetBool("IsShow", false);
        while (true)
        {
            // インゲームのループ処理
            yield return null;
        }
    }

    IEnumerator GameOverLoop()
    {
        anim.SetBool("IsShow", true);
        waveText.text = PhaseManager.Instance.wave.ToString();
        scoreText.text = player.score.ToString();
        SeedText.text = "| " + player.currentSeed.ToString();
        while (true)
        {
            // ゲームオーバーのループ処理
            yield return null;
        }
    }
}