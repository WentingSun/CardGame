using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    // public static event Action<GameState> OnGameStateChange;
    
    public NormalBarControl NaturalBar;




    void Awake(){
        Instance = this;
    }
    void Start()
    {
        // OnGameStateChange?.Invoke(State);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

public enum GameState {
    Start,
    PlayerTurn,
    Settlement,
    Lose,

}
