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
        GameManager.onWeatherStateChange += updateBoxPic;
    }

    private void updateBoxPic(WeatherState currentWeather){
        image.sprite = ResourceManager.Instance.weatherDictionary[currentWeather].cardPic;
    }

    private string getCurrentWeatherDescription(){
        string result = "";
        result += ResourceManager.Instance.weatherDictionary[GameManager.Instance.currentWeatherState].description;
        return result;
    }

    private string getCurrentWeatherDeckState(){
        List<WeatherState> weatherDeck = ResourceManager.Instance.WeatherCardDeck;
        int num;
        string result = "\n\n The weather deck contains ";
        if(weatherDeck.Contains(WeatherState.Sunny)) {
            num = weatherDeck.FindAll(x => x.Equals(WeatherState.Sunny)).Count;
            result += $"Sunny card * {num}, ";
            }
        if(weatherDeck.Contains(WeatherState.Rainy)) {
            num = weatherDeck.FindAll(x => x.Equals(WeatherState.Rainy)).Count;
            result += $"Rain card * {num}, ";
            }
        if(weatherDeck.Contains(WeatherState.Windy)) {
            num = weatherDeck.FindAll(x => x.Equals(WeatherState.Windy)).Count;
            result += $"Windy card * {num}, ";
            }    
        if(weatherDeck.Contains(WeatherState.AirPollution)) {
            num = weatherDeck.FindAll(x => x.Equals(WeatherState.AirPollution)).Count;
            result += $"AirPollution card * {num}, ";
            }
        if(weatherDeck.Contains(WeatherState.UrbanHeatIsland)) {
            num = weatherDeck.FindAll(x => x.Equals(WeatherState.UrbanHeatIsland)).Count;
            result += $"UrbanHeatIsland card * {num}, ";
            }
        if(weatherDeck.Contains(WeatherState.Rainstorms)) {
            num = weatherDeck.FindAll(x => x.Equals(WeatherState.Rainstorms)).Count;
            result += $"Rainstorms card * {num}, ";
            }
        if(weatherDeck.Contains(WeatherState.Snow)) {
            num = weatherDeck.FindAll(x => x.Equals(WeatherState.Snow)).Count;
            result += $"Snow card * {num}, ";
            }
        
        result += "Good luck!";
        
        

        return result;
    }



    private void OnMouseOver() {
        string message = BasicDescription + getCurrentWeatherDescription() + getCurrentWeatherDeckState();
        InformationManager.Instance.showInInformationBox(message,true);
    }

    private void OnDestroy() {
        GameManager.onWeatherStateChange -= updateBoxPic;
    }

}
