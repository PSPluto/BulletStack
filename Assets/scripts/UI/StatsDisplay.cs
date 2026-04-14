using UnityEngine;
using TMPro;

public class StatsDisplay : MonoBehaviour
{

    public enum DisplayType { Damage, Voltage, PelletCount, Speed, SpreadAngle, FireRate}

    [Header("Ç«ÇÃïœêîÇï\é¶Ç∑ÇÈÇ©")]
    public DisplayType type;

    [Header("éQè∆êÊÇ∆ï\é¶ê›íË")]
    public MotherBoard motherBoardScript;
    public TextMeshProUGUI targetText;
    public string prefix = "Val: ";

    public void UpdateDisplay()
    {
        switch (type)
        {
            case DisplayType.Damage:
                targetText.text = $"{prefix}{motherBoardScript.resultBaseDamage:F1}";
                break;
            case DisplayType.Voltage:
                targetText.text = $"{prefix}{motherBoardScript.resultVoltage:F1}v";
                break;
            case DisplayType.PelletCount:
                targetText.text = $"{prefix}{motherBoardScript.resultPelletCount}î≠";
                break;
            case DisplayType.Speed:
                targetText.text = $"{prefix}{motherBoardScript.resultBulletSpeed:F1}";
                break;
            case DisplayType.SpreadAngle:
                targetText.text = $"{prefix}{motherBoardScript.resultMaxSpreadAngle:F0}Åã";
                break;
            case DisplayType.FireRate:
                targetText.text = $"{prefix}{motherBoardScript.resultTimeToFire:F2}s";
                break;
        }
    }
}