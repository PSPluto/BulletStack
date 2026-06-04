using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Modifier : ScriptableObject
{
    [Header("Œ©‚½–Ú‚Ìİ’è")]
    public string chipName;
    [TextArea]
    public string description;
    public Sprite icon;
    public Color Color = Color.white;
    [Header("Œø‰Ê‚Ìİ’è")]
    public float resistance = 0.5f;
    public float voltageCost = 2f;

    public abstract void Process(Signal signal);
}
