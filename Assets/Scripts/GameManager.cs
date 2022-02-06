using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event Action GameEnded;


    private void Awake()
    {
        if(FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void EndGame()
    {
        Debug.Log("Ending game from game manager");
        GameEnded?.Invoke();
    }

}
