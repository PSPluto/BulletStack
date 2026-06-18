using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPattern", menuName = "Scriptable Objects/EnemyPattern")]
public class BaseEnemyPattern : ScriptableObject
{
    public int cost = 1;
    [Header("èoåªÇ∑ÇÈprefab")]
    public GameObject[] prefabs = new GameObject[5];
}
