using UnityEngine;
// 1. 新しいInput Systemのネームスペースを追加
using UnityEngine.InputSystem;

public class MODStatsDisplay : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;

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
}
