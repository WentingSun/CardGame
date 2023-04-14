using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherMenuButtonControl : MonoBehaviour
{
    
    public void GrabWeatherCard(){
    ResourceManager.Instance.updateWeatherDeck();
    WeatherState selectedWeather = GameHelperFunction.RandomSelect(ResourceManager.Instance.WeatherCardDeck);
    InformationManager.Instance.setWeatherNotationInfor(selectedWeather);
    GameManager.Instance.UpdateWeatherState(selectedWeather);
    MenuManager.Instance.loadMenu();
    }

    public void confirmButton(){
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
        InformationManager.Instance.EmptyWeatherNotationInfor();
    }



}
