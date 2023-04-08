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

    private int sellPrice = 1;//the price for selling electricity
    private int buyPrice = 2;// the price for buying electircity
    private int sellNum = 1;// the electricity num for selling
    private int buyNum = 1;// the electricity num for buying

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
        return ResourceManager.Instance.getTargetCardsNum(17) + bufferMoney;
    }

    public int getCurrentElectricity(){
        return ResourceManager.Instance.getElectricityCardNum() + bufferElectricity;
    }

    public bool checkedMoneyState(int price){
        if(ResourceManager.Instance.getTargetCardsNum(17) + bufferMoney  >= price){
            return true;
        }
        return false;
    }

    public bool checkedElectricityState(int price){
        if(ResourceManager.Instance.getTargetCardsNum(5) + bufferElectricity  >= price){
            return true;
        }
        return false;
    }

    public void buyMarketElectricity(){
        int currentElectricity = getCurrentElectricity();
        int currentMoneyNum = getCurrentMoneyNum();
        if(checkedMoneyState(buyPrice)){
            int remainMoney = bufferMoney - buyPrice;
            if(remainMoney > 0){
                bufferMoney = remainMoney;
            }else{
                bufferMoney = 0;
            }
            ResourceManager.Instance.consumeMoney(-remainMoney);
            bufferElectricity += buyNum;
            currentElectricity += buyNum;
            currentMoneyNum -= buyPrice;
            InformationManager.Instance.setMarketInfoBoxText(currentMoneyNum.ToString(),currentElectricity.ToString());
        }else{
            InformationManager.Instance.setMarketNotationInfo("You Do Not Have Enough Money To Buy Electricity.");
        }
    }

    public void sellMarketElectricity(){
        int currentElectricity = getCurrentElectricity();
        int currentMoneyNum = getCurrentMoneyNum();
        if(checkedElectricityState(sellNum)){
            int remainElectriciy = bufferElectricity - sellNum;
            if(remainElectriciy > 0){
                bufferElectricity = remainElectriciy;
            }else{
                bufferElectricity = 0;
            }
            ResourceManager.Instance.consumeElectricity(-remainElectriciy);
            bufferMoney += sellPrice;
            currentMoneyNum +=sellPrice;
            currentElectricity -=sellNum;
            InformationManager.Instance.setMarketInfoBoxText(currentMoneyNum.ToString(),currentElectricity.ToString());
        }else{
            InformationManager.Instance.setMarketNotationInfo("You Do Not Have Enough Electricity To Sell.");
        }
    }

    public void purchasingTheCard(CardData cardData, int price){
        purchasedCards.Add(cardData);
        int consumeMoney = price - bufferMoney;
        ResourceManager.Instance.consumeMoney(consumeMoney);
    }

    public void MarketManagerOnGameStateChange(GameState gameState){

    }


}
