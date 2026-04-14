using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnim : MonoBehaviour
{
    Coroutine _activeLoop;

    public bool TitleShow;
    public bool InGameShow;
    public bool GameOverShow;

    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
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
        if (TitleShow == true)
        {
            animator.SetBool("Show", true);
        }
        else
        {
            animator.SetBool("Show", false);
        }
        while (true)
        {
            // タイトルのループ処理
            yield return null;
        }
    }

    IEnumerator InGameLoop()
    {
        if (InGameShow == true)
        {
            animator.SetBool("Show", true);
        }
        else
        {
            animator.SetBool("Show", false);
        }
        while (true)
        {
            // インゲームのループ処理
            yield return null;
        }
    }

    IEnumerator GameOverLoop()
    {
        if (GameOverShow == true)
        {
            animator.SetBool("Show", true);
        }
        else
        {
            animator.SetBool("Show", false);
        }
        while (true)
        {
            // ゲームオーバーのループ処理
            yield return null;
        }
    }
}