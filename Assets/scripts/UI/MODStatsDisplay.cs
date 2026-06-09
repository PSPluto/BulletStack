using TMPro;
using Unity.VisualScripting;
using UnityEngine;
// 1. 新しいInput Systemのネームスペースを追加
using UnityEngine.InputSystem;
using static AddModifier;

public class MODStatsDisplay : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private TextMeshProUGUI resistanceText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI applicableTo;
    [SerializeField] private TextMeshProUGUI valueText;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // マウスが存在することを確認
        if (Mouse.current == null) return;

        Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        // 2. Input.mousePosition から Mouse.current.position.ReadValue() に変更
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            mousePos,
            cam,
            out localPoint))
        {
            rectTransform.anchoredPosition = localPoint + (new Vector2(10,-10));
        }
    }
    public void UpdateUI(string name = null, string desc = null, float? resistance = null, float? cost = null, ApplicableTo? to = null, float? value = null) {
        if (name == null) {
            rectTransform.localScale = Vector3.zero;
        return;
        }
        rectTransform.localScale = new Vector3(1, 1, 1);
        descText.text = desc;
        resistanceText.text = $"抵抗：{resistance}";
        costText.text = $"使用電力：{cost}";
        if (to != null)
        {
            applicableTo.text = $"適用先：{to}";
            valueText.text = $"加算量：{value}";
        }
        else
        {
            applicableTo.text = "---";
            valueText.text = "---";
        }
    
    }
}
