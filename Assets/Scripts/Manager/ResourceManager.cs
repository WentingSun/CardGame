using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField] NormalBarControl NaturalBar;
    [SerializeField] NormalBarControl HumanitiesBar;

    public GameObject cardTemple;

    public GameObject stackTemple;

    [SerializeField] List<Card> WholeCardsList;
    [SerializeField] List<Stack> WholeStacksList;

    public CardData[] cardDatas;
    public WeatherCardData[] weatherCardDatas;

    override protected void Awake() {
        base.Awake();
        cardDatas = Resources.LoadAll<CardData>("CardData");
        weatherCardDatas = Resources.LoadAll<WeatherCardData>("WeatherCardData");
        GameManager.OnGameStateChange += ResourceManagerOnGameStateChanged;
    }

    private void ResourceManagerOnGameStateChanged(GameState gameState){
    if(gameState == GameState.PlayerTurn){
        foreach(Card cards in WholeCardsList){
            cards.activateMove(true);
          }
        }
    }
    
    public void changeNaturalBarValue(float changeValue){
        NaturalBar.changeCurrentBarValue(changeValue);
    }
    public void changeHumanitiesBarValue(float changeValue){
        HumanitiesBar.changeCurrentBarValue(changeValue);
    }

    public void addingCardList(Card card){
        WholeCardsList.Add(card);
    }

    public void removingCardList(Card card){
        WholeCardsList.Remove(card);
    }

    public void addingStackList(Stack stack ){
        WholeStacksList.Add(stack);
    }

    public void removingStackList(Stack stack){
        WholeStacksList.Remove(stack);
    }



    override protected void OnDestroy() {
        base.OnDestroy();
        GameManager.OnGameStateChange -= ResourceManagerOnGameStateChanged;
    }




}
