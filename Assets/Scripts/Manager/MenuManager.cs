using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : Singleton<MenuManager>
{

    public GameObject WeatherMenu;
    public GameObject MarketMenu;

    public void loadMenu(){
        // Debug.Log("Loadmenu");
        WeatherMenu = ResourceManager.Instance.canvasTransform.Find("WeatherMenu").gameObject;
        MarketMenu = ResourceManager.Instance.canvasTransform.Find("MarketMenu").gameObject;

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
        if(WeatherMenu == null 
        || MarketMenu == null){
            loadMenu();
        }
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
