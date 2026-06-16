using UnityEngine;

public class GameDebug : MonoBehaviour
{
    [SerializeField] private MotherBoard motherBoard;
    [SerializeField] private RewordInventrySystem rewordInventrySystem;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            motherBoard.currentHP = 10000;
        } 
        if (Input.GetKeyDown(KeyCode.F2))
        {
            motherBoard.currentXP += 1000;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            rewordInventrySystem.NewRewardCreate();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            Debug.Log(FindObjectsByType<PlayerInventory>(sortMode: FindObjectsSortMode.None).Length);
        }
    }
}
