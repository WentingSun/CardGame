using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : Singleton<MarketManager>
{
    public GameObject testObj;
    public GameObject supplyArea;
    public List<CardData> purchasedCards;
    public int bufferMoney = 0;
    public int bufferElectricity = 0;

    protected override void Awake() {
        base.Awake();
        bufferMoney = 2;
        bufferElectricity = 3;//Testing code
        GameManager.OnGameStateChange += MarketManagerOnGameStateChange;
        purchasedCards.Clear();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.OnGameStateChange -= MarketManagerOnGameStateChange;
    }


    public void testButton(){
        Card.createCard(ResourceManager.Instance.cardDatas[1], testObj.transform);
    }
    
    public void provideCard(CardData cardData){
        Card.createCard(cardData, supplyArea.transform);
    }

    public int getCurrentMoneyNum(){
        return ResourceManager.Instance.getTargetCardsNum(17)+MarketManager.Instance.bufferMoney;
    }

    public int getCurrentElectricity(){
        return ResourceManager.Instance.getElectricityCardNum()+MarketManager.Instance.bufferElectricity;
    }

    public bool checkedMoneyState(int price){
        if(ResourceManager.Instance.getTargetCardsNum(17) + bufferMoney  >= price){
            return true;
        }
        return false;
    }

    public void purchasingTheCard(CardData cardData, int price){
        purchasedCards.Add(cardData);
        int consumeMoney = price - bufferMoney;
        ResourceManager.Instance.consumeMoney(consumeMoney);
    }

    public void MarketManagerOnGameStateChange(GameState gameState){

    }


}
