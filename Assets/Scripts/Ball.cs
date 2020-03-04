using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // config params
    [SerializeField] 
    private Paddle _paddle;

    [SerializeField]
    private float _initialXVelocity = 15f;

    [SerializeField]
    private float _initialYVelocity = 15f;

    [SerializeField] 
    private AudioClip[] _ballSounds;

    private bool _hasStarted = false;

    // state
    private Vector2 _paddleToBallVector2;

    // Cached component references
    private AudioSource _audioSource;
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _paddleToBallVector2 = transform.position - _paddle.transform.position;
        _audioSource = GetComponent<AudioSource>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            _rigidbody2D.velocity = new Vector2(_initialXVelocity, _initialYVelocity);
            _hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        // Needs to be this because the paddle has a Vector3 position
        var paddlePosition = new Vector2(_paddle.transform.position.x, _paddle.transform.position.y);
        transform.position = _paddleToBallVector2 + paddlePosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasStarted)
        {
            PlayRandomAudioClip();
            PreventStuckMovementState();
        }
    }

    private void PlayRandomAudioClip()
    {
        var audioClip = _ballSounds[Random.Range(0, _ballSounds.Length)];
        _audioSource.PlayOneShot(audioClip);
    }

    private void PreventStuckMovementState()
    {
        if (_rigidbody2D.velocity.x == 0f)
        {
            _rigidbody2D.velocity = new Vector2(0.5f, _rigidbody2D.velocity.y);
        }

        if (_rigidbody2D.velocity.y == 0f)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0.5f);
        }
    }
}
