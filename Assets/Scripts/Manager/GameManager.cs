using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : Singleton<GameManager>
{
    

    public GameState State;

    public static event Action<GameState> OnGameStateChange;
    
    public void UpdateGameState(GameState newState){
        State = newState;

        switch(newState) {
            case GameState.Start:
            HandleStart();
            break;
            case GameState.PlayerTurn:
            HandlePlayerTurn();
            break;
            case GameState.Settlement:
            HandleSettlement();
            break;
            case GameState.GameOver:
            HandleGamerOver();
            break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState , null);

        }

        OnGameStateChange?.Invoke(State);

    }

    private void HandleStart(){

    }

    private void  HandlePlayerTurn(){

    }

    private void HandleSettlement(){

    }

    private void HandleGamerOver(){

    }






}

public enum GameState {
    Start,
    PlayerTurn,
    Settlement,
    GameOver,

}
