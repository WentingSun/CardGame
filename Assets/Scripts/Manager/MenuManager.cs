using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : Singleton<MenuManager>
{

    public GameObject WeatherMenu;

    public void activiteWeatherMenu(){

    }

    protected override void Awake() {
        base.Awake();
        GameManager.OnGameStateChange += MenuManagerOnGameStateChanged;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.OnGameStateChange -= MenuManagerOnGameStateChanged;
    }

    void MenuManagerOnGameStateChanged(GameState newState){

    }

    public void GrabWeatherCard(){
        WeatherState selectedWeather = RandomSelect(ResourceManager.Instance.WeatherCardDeck);
        GameManager.Instance.UpdateWeatherState(selectedWeather);
    }

    public void confirmButton(){
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }

    public static T RandomSelect<T>(List<T> beSelectedList){
        if(beSelectedList == null || beSelectedList.Count == 0){
            return default (T);
        }
        System.Random random = new System.Random();
        int index = random.Next(0,beSelectedList.Count);

        return beSelectedList[index];
        
    }
        

}
