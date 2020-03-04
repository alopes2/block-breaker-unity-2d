using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] 
    private int _numberOfBreakableBlocks = 0;

    private SceneLoader _sceneLoader;

    void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        _numberOfBreakableBlocks++;
    }
    public void ReduceBreakableBlocksByOne()
    {
        _numberOfBreakableBlocks--;

        if (_numberOfBreakableBlocks == 0)
        {
            _sceneLoader.LoadNextScene();
        }
    }
}
