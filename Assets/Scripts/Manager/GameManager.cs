using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : Singleton<GameManager>
{
    

    public GameState State;
    public WeatherState currentWeatherState;
    public float turnScale = 60;

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
        // ResourceManager.Instance.resetAllStackPos();
        // InformationManager.Instance.updateSmartMeterInfo();
        resetTurnBar();
        consumeElectricityInStorage();//todo
        supplyTheProduct();//todo
        resetTheMarket();//todo
        playerDrawTheCurrentWeather();//todo
        
        Debug.Log("Start");
    }



    private void  HandlePlayerTurn(){
    
    }

    private void HandleSettlement(){
        Debug.Log("Settlement");
        if(checkCountinue()){
            Debug.Log("Continue");
            consumeElectricityAtSettlement();
            consumeLifeSpanOfSomeCard();//todo
            UpdateGameState(GameState.Start);
        }else{
            UpdateGameState(GameState.GameOver);
        }
    }



    private void HandleGamerOver(){
        Debug.Log("game over");
    }

    private bool checkCountinue(){
        if(ResourceManager.Instance.getElectricityCardNum() < ResourceManager.Instance.getElectricityCardRequire()){
            return false;
        }
        return true;
    }

    private void resetTurnBar(){
        ResourceManager.Instance.resetTurnBar(turnScale);
    }

    private void consumeElectricityAtSettlement(){
        ResourceManager.Instance.consumeElectricity(ResourceManager.Instance.getElectricityCardRequire());
        // InformationManager.Instance.updateSmartMeterInfo();
    }

    private void consumeLifeSpanOfSomeCard(){
        
    }

    private void consumeElectricityInStorage()
    {
        
    }

    private void resetTheMarket(){

    }

    private void supplyTheProduct(){

    }

    private void playerDrawTheCurrentWeather(){
        MeanManager.Instance.activiteMenu();
        
    }

    private void initTurn(){

    }






}

public enum GameState {
    Start,
    PlayerTurn,
    Market,
    Pause,
    Settlement,
    GameOver,

}
