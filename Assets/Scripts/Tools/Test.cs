using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Image image;

    private void Awake() {
        image = GetComponentInChildren<Image>();
    }
    
    public void testButton(){
        Debug.Log("testButton()");
    }

    // private void Update() {
    //     switch(GameManager.Instance.currentWeatherState){
    //         case WeatherState.Sunny:
    //         image.sprite = ResourceManager.Instance.weatherCardPic[1];
    //         break;
    //         case WeatherState.Windy:
    //         image.sprite = ResourceManager.Instance.weatherCardPic[3];
    //         break;
    //         case WeatherState.Rainy:
    //         image.sprite = ResourceManager.Instance.weatherCardPic[2];
    //         break;
    //         case WeatherState.AirPollution:
    //         image.sprite = ResourceManager.Instance.weatherCardPic[4];
    //         break;
    //     }
    // }
}
