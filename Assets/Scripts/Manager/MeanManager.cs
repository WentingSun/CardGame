using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeanManager : Singleton<MeanManager>
{

    public void activiteMenu(){

    }

    protected override void Awake() {
        base.Awake();
        GameManager.OnGameStateChange += MeanManagerOnGameStateChanged;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.OnGameStateChange -= MeanManagerOnGameStateChanged;
    }

    void MeanManagerOnGameStateChanged(GameState newState){

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
