using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : Singleton<MenuManager>
{

    public GameObject WeatherMenu;
    public GameObject MarketMenu;
    public GameObject GameOverMenu;
    public void loadMenu(){

        WeatherMenu = ResourceManager.Instance.canvasTransform.Find("WeatherMenu").gameObject;
        MarketMenu = ResourceManager.Instance.canvasTransform.Find("MarketMenu").gameObject;
        GameOverMenu = ResourceManager.Instance.canvasTransform.Find("GameOverMenu").gameObject;

    }

    public void activiteWeatherMenu(bool active){
        WeatherMenu.SetActive(active);
        WeatherMenu.transform.SetSiblingIndex(ResourceManager.Instance.canvasTransform.childCount + 1);
    }

    public void activiteMarketMenu(bool active){
        MarketMenu.SetActive(active);
        MarketMenu.transform.SetSiblingIndex(ResourceManager.Instance.canvasTransform.childCount + 1);
    }

    protected override void Awake() {
        base.Awake();
        GameManager.OnGameStateChange += MenuManagerOnGameStateChanged;
        
    }

    private void Update() {
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.OnGameStateChange -= MenuManagerOnGameStateChanged;
    }

    private void MenuManagerOnGameStateChanged(GameState newState){
        if(WeatherMenu == null 
        || MarketMenu == null
        || GameOverMenu == null){
            loadMenu();
        }
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
