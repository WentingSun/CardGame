using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : Singleton<GameManager>
{
    

    public GameState State;
    public WeatherState currentWeatherState;
    public SeasonState currentSeasonState;
    public  int TurnNum = 1;

    public int strikeRoundNum = 0;

    public float turnScale = 60;
    private int consecutiveTurn = 0;

    public static event Action<GameState> OnGameStateChange;
    public static event Action<WeatherState> onWeatherStateChange;
    
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
            case GameState.Market:
            HandleMarKet();
            break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState , null);

        }

        OnGameStateChange?.Invoke(State);

    }

    public void UpdateWeatherState(WeatherState newWeatherState){
        currentWeatherState = newWeatherState;


        onWeatherStateChange?.Invoke(currentWeatherState);
    }

    private void HandleStart(){
        // ResourceManager.Instance.resetAllStackPos();
        // InformationManager.Instance.updateSmartMeterInfo();
        resetTurnBar();
        consumeElectricityInStorage();
        supplyTheProduct();
        if(TurnNum % 4 == 3){
            addingNewHuman();
        }
        if(consecutiveTurn != 0){
            rewardHumanitierBar();
        }
        ResourceManager.Instance.changeNaturalBarValue(5);
        resetTheMarket();
        playerGrabTheCurrentWeather();
        
        Debug.Log("Start");
    }



    private void  HandlePlayerTurn(){
    
    }

    private void HandleSettlement(){
        Debug.Log("Settlement");
        if(checkCountinue()){
            Debug.Log("Continue");
            consumeElectricityAtSettlement();
            consumeHumanAtSettlement();
            consumeLifeSpanOfSomeCard();
            TurnNum += 1;
            if(currentWeatherState != WeatherState.AirPollution){
                consecutiveTurn += 1;
            }
            if(strikeRoundNum > 0){
                strikeRoundNum -= 1;
            }
            UpdateGameState(GameState.Start);
        }else{
            UpdateGameState(GameState.GameOver);
        }
    }

    private void  HandleMarKet(){

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

    private void consumeHumanAtSettlement(){
        int currentHumanNum = ResourceManager.Instance.getTargetCardsNum(1);
        int currentResidentCapacity = ResourceManager.Instance.getResidentCapacityNum();
        if(currentHumanNum > currentResidentCapacity){
            ResourceManager.Instance.consumeHuman(currentHumanNum - currentResidentCapacity);
        }
    }

    private void addingNewHuman(){
        MarketManager.Instance.provideCard(ResourceManager.Instance.cardDatas[1]);
    }

    private void consumeLifeSpanOfSomeCard(){
        ResourceManager.Instance.consumeLifeSpanOfSomeCard();
    }

    private void consumeElectricityInStorage()
    {
        ResourceManager.Instance.consumeStorageElectricity();
    }

    private void resetTheMarket(){
        MarketManager.Instance.resetAllMarketProduct();
    }

    private void supplyTheProduct(){
        MarketManager.Instance.supplyCardAtStart();
    }

    private void playerGrabTheCurrentWeather(){
        MenuManager.Instance.activiteWeatherMenu(true);
        
    }

    private void rewardHumanitierBar(){
        ResourceManager.Instance.changeHumanitiesBarValue(consecutiveTurn+3);
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
