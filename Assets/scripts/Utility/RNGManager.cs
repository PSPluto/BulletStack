using UnityEngine;

public static class RNGManager
{
    public static System.Random Reward;
    public static System.Random Gameplay;
    public static System.Random Player;

    public static void Init(int seed)
    {
        Reward = new System.Random(seed);
        Gameplay = new System.Random(seed + 1);
        Player = new System.Random(seed + 2);
    }
}