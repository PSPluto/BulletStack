using UnityEngine;

public class StateManager : MonoBehaviour
{
    // イベントを定義
    public static System.Action<int> OnStateChanged;
    public static int CurrentState { get; private set; }

    public static void ChangeState(int newState)
    {
        CurrentState = newState;
        OnStateChanged?.Invoke(newState);
        Debug.Log($"Stateが{newState}になりました");
    }
    private void OnDestroy()
    {
        OnStateChanged = null;
    }
}
