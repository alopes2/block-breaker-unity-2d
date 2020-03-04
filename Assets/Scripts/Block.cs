using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private AudioClip _breakSound;

    [SerializeField] 
    private GameObject _blockSparklesVFX;

    //[SerializeField] 
    //private int _maxHits;

    [SerializeField] 
    private Sprite[] _damagedSprites;

    private Level _level;
    private GameSession _gameSession;

    // state variables
    private int _numberOfHitsTaken;

    void Start()
    {
        _level = FindObjectOfType<Level>();
        _gameSession = FindObjectOfType<GameSession>();

        CountBreakableBlock();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(tag);
        if (tag.Equals("Breakable"))
        {
            HandleHit();
        }
    }

    private void CountBreakableBlock()
    {
        if (tag.Equals("Breakable"))
        {
            _level.CountBlocks();
        }
    }

    private void HandleHit()
    {
        _numberOfHitsTaken++;
        if (_numberOfHitsTaken > _damagedSprites.Length)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var spriteIndex = _numberOfHitsTaken - 1;

        spriteRenderer.sprite = _damagedSprites[spriteIndex];
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(_breakSound, Camera.main.transform.position);
        _gameSession.AddToScore();
        _level.ReduceBreakableBlocksByOne();
        TriggerSparklesVFX();
        Destroy(gameObject);
    }

    private void TriggerSparklesVFX()
    {
        var sparkles = Instantiate(_blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
