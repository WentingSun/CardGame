using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherBox : MonoBehaviour
{
    public Image image;
    private string BasicDescription = "This box shows the current weather and effect some cards.\n\n";

    private void Awake() {
        image = GetComponentInChildren<Image>();
    }

    private string getCurrentWeatherDescription(){
        string result = "";
        switch(GameManager.Instance.currentWeatherState){
            case WeatherState.Sunny:
            result += ResourceManager.Instance.weatherCardDatas[1].description;
            break;
            case WeatherState.Rainy:
            result += ResourceManager.Instance.weatherCardDatas[2].description;
            break;
            case WeatherState.Windy:
            result += ResourceManager.Instance.weatherCardDatas[3].description;
            break;
            case WeatherState.AirPollution:
            result += ResourceManager.Instance.weatherCardDatas[4].description;
            break;
            
        }
        return result;
    }



    private void OnMouseOver() {
        string message = BasicDescription + getCurrentWeatherDescription();
        InformationManager.Instance.showInInformationBox(message,true);
    }
}
