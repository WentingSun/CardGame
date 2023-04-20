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
    public static event Action<SeasonState> onSeasonStateChange;
    
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

    public void UpdateSeasonState(SeasonState seasonState){
        onSeasonStateChange?.Invoke(seasonState);
    }

    public void UpdateWeatherState(WeatherState newWeatherState){
        currentWeatherState = newWeatherState;


        onWeatherStateChange?.Invoke(currentWeatherState);
    }

    private void HandleStart(){
        // ResourceManager.Instance.resetAllStackPos();
        // InformationManager.Instance.updateSmartMeterInfo();
        if(TurnNum == 1){
            Debug.Log("First Turn");
            initTheFirstTurn();
        }
        resetTurnBar();
        consumeElectricityInStorage();
        supplyTheProduct();
        if(TurnNum % 4 == 3){
            addingNewHuman();
        }
        if(TurnNum%4 == 0){
            changeSeason();
        }
        if(consecutiveTurn != 0){
            rewardHumanitierBar();
        }
        rewardNatureBar();
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
            }else{
                consecutiveTurn = 0;
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
        MenuManager.Instance.GameOverMenu.SetActive(true);
        MenuManager.Instance.GameOverMenu.transform.SetSiblingIndex(10000);
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
            ResourceManager.Instance.changeHumanitiesBarValue(-40*(currentHumanNum - currentResidentCapacity));
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
        ResourceManager.Instance.consumeAllElectricity();
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

    private void changeSeason(){
        switch(currentSeasonState){
            case SeasonState.Spring:
            UpdateSeasonState(SeasonState.Summer);
            break;
            case SeasonState.Summer:
            UpdateSeasonState(SeasonState.Autumm);
            break;
            case SeasonState.Autumm:
            UpdateSeasonState(SeasonState.Winter);
            break;
            case SeasonState.Winter:
            UpdateSeasonState(SeasonState.Spring);
            break;

        }
    }

    private void rewardNatureBar(){
        ResourceManager.Instance.changeNaturalBarValue(15);
    }

    public void initTheGame(){
        Debug.Log("Testing");
        TurnNum = 1;
        consecutiveTurn = 0;
        initTheFirstTurn();
    }

    public void initTheFirstTurn(){
        if(ResourceManager.Instance == null){
            Debug.Log("NULL");
        }else{
            ResourceManager.Instance.GoodWeatherCardDeck = WeatherStateConstants.SpringWeatherStates;
            ResourceManager.Instance.BadWeatherCardDeck.Clear();
            ResourceManager.Instance.resetWholeCard();
            MarketManager.Instance.provideCard(ResourceManager.Instance.cardDatas[1]);
            MarketManager.Instance.provideCard(ResourceManager.Instance.cardDatas[3]);
            MarketManager.Instance.provideCard(ResourceManager.Instance.cardDatas[4]);
            MarketManager.Instance.provideCard(ResourceManager.Instance.cardDatas[8]);
            MarketManager.Instance.provideCard(ResourceManager.Instance.cardDatas[6]);
            MarketManager.Instance.provideCard(ResourceManager.Instance.cardDatas[7]);
            ResourceManager.Instance.resetHumanitierBarValue();
            ResourceManager.Instance.resetHumanitierBarValue();
            ResourceManager.Instance.resetTurnBar(60);
        }

    }


    public void QuitTheGame(){
        Debug.Log("Quit");
        Application.Quit();
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(State == GameState.PlayerTurn){
                UpdateGameState(GameState.Pause);
            }else if(State == GameState.Pause){
                UpdateGameState(GameState.PlayerTurn);
            }
            
        }
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
