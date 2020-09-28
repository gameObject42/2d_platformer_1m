using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Warning: Multiple " + this +" in Scene");
            Destroy(this);
        }
    }

    public enum GameState
    {
        MainMenu,
        Playing,
        PauseMenu,
        PlayerDead,
        GameOver,
        ShopMenu
    }
    public GameState _gameState;
}
