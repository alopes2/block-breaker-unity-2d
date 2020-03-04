using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 10f)]
    private float _gameSpeed = 1f;

    [SerializeField] 
    private int _pointsPerBlockDestroyed = 42;

    [SerializeField] 
    private TextMeshProUGUI _scoreText;

    [SerializeField] 
    private bool _isAutoPlayEnabled = false;

    // State
    [SerializeField] 
    private int _currentScore = 0;

    public bool IsAutoPlayEnabled => _isAutoPlayEnabled;

    void Awake()
    {
        var gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        SetScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = _gameSpeed;
    }

    public void AddToScore()
    {
        _currentScore += _pointsPerBlockDestroyed;
        SetScoreText();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    private void SetScoreText()
    {
        _scoreText.text = _currentScore.ToString();
    }
}
