using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private GameSession _gameSession;
    private Ball _ball;

    void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();
        _ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouseXPosition();
    }

    private void FollowMouseXPosition()
    {
        var cameraWidthSize = GetCameraWidthSize();

        var paddlePosition = new Vector2(transform.position.y, transform.position.y);
        
        var (minX, maxX) = CalculateMinAndMaxXValues(cameraWidthSize);
        
        paddlePosition.x = Mathf.Clamp(GetXPosition(cameraWidthSize), minX, maxX);

        transform.position = paddlePosition;
    }

    private float GetXPosition(float cameraWidthSize)
    {
        if (_gameSession.IsAutoPlayEnabled)
        {
            return _ball.transform.position.x;
        }

        var mousePositionX = (Input.mousePosition.x / Screen.width) * cameraWidthSize;
        return mousePositionX;
    }

    private (float minX, float maxX) CalculateMinAndMaxXValues(float cameraWidthSize)
    {
        var paddleHalfWidthSize = GetPaddleHalfWidthSize();
        var maxX = cameraWidthSize - paddleHalfWidthSize;

        return (paddleHalfWidthSize, maxX);
    }

    private float GetPaddleHalfWidthSize()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var paddleHalfWidthSize = spriteRenderer.sprite.bounds.size.x / 2f;
        return paddleHalfWidthSize;
    }

    private static float GetCameraWidthSize()
    {
        var cameraHeightSize = 2f * Camera.main.orthographicSize;
        var cameraWidthSize = cameraHeightSize * Camera.main.aspect;
        return cameraWidthSize;
    }
}
