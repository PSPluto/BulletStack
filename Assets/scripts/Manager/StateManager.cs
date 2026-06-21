using System.Collections;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // イベントを定義
    public static System.Action<int> OnStateChanged;
    public static int CurrentState { get; private set; }

    public GameObject resultDisplay;
    public static void ChangeState(int newState)
    {
        CurrentState = newState;
        OnStateChanged?.Invoke(newState);
        //Debug.Log($"Stateが{newState}になりました");
    }
    private void OnDestroy()
    {
        OnStateChanged = null;
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
        resultDisplay.transform.localScale = Vector3.zero;
        yield return null;
    }

    IEnumerator InGameLoop()
    {
        resultDisplay.transform.localScale = Vector3.zero;
        yield return null;
    }

    IEnumerator GameOverLoop()
    {
        resultDisplay.transform.localScale = Vector3.one;
        yield return null;
    }

}
