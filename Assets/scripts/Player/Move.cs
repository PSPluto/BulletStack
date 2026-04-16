using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb;
    public float xMoveSpeed = 4f;
    public float yMoveSpeed = 4f;
    private int moveX;
    private int moveY;
    Coroutine _activeLoop;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.linearVelocity = new Vector2(0, 0);
        moveX = 0;
        moveY = 0;
        Vector3 targetPos = new Vector3(0, -4, 0);

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.05f);

            yield return null;
        }
        transform.position = targetPos;
    }

    IEnumerator InGameLoop()
    {
        while (true)
        {
            moveX = 0;
            moveY = 0;
            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                if (keyboard.wKey.isPressed) moveY += 1;
                if (keyboard.sKey.isPressed) moveY -= 1;
                if (keyboard.dKey.isPressed) moveX += 1;
                if (keyboard.aKey.isPressed) moveX -= 1;
            }
            Vector2 inputDirection = new Vector2(moveX, moveY).normalized;
            rb.linearVelocity = new Vector2(inputDirection.x * xMoveSpeed, inputDirection.y * yMoveSpeed);
            // インゲームのループ処理
            yield return null;
        }
    }

    IEnumerator GameOverLoop()
    {
        rb.linearVelocity = new Vector2(0, 0);
        moveX = 0;
        moveY = 0;
        Vector3 targetPos = new Vector3(0, -4, 0);

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.05f);

            yield return null;
        }
        transform.position = targetPos;
    }
}
