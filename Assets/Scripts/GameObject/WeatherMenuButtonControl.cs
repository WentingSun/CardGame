using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherMenuButtonControl : MonoBehaviour
{
    
    public void GrabWeatherCard(){
    WeatherState selectedWeather = GameHelperFunction.RandomSelect(ResourceManager.Instance.WeatherCardDeck);
    GameManager.Instance.UpdateWeatherState(selectedWeather);
    MenuManager.Instance.loadMenu();
    }

    public void confirmButton(){
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }



}