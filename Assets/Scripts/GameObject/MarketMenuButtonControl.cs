using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketMenuButtonControl : MonoBehaviour
{

    public void ExitButton(){
        MenuManager.Instance.activiteMarketMenu(false);
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
        provideEleAndMoney();
        InformationManager.Instance.setMarketNotationInfo("");
    }

    public void MarketButton(){
        if(GameManager.Instance.State == GameState.PlayerTurn || GameManager.Instance.State == GameState.Pause){
            GameManager.Instance.UpdateGameState(GameState.Market);
            MenuManager.Instance.activiteMarketMenu(true);
            InformationManager.Instance.updateMarketInfoBoxText();
            
        }

        
    }

    public void SellElectriciyButton(){
        MarketManager.Instance.sellMarketElectricity();
    }

    public void BuyElectricityButton(){
        MarketManager.Instance.buyMarketElectricity();
    }

    public void provideEleAndMoney(){
        while(MarketManager.Instance.bufferElectricity > 0){
            MarketManager.Instance.provideCard(ResourceManager.Instance.cardDatas[5]);
            MarketManager.Instance.bufferElectricity --;
        }
        while(MarketManager.Instance.bufferMoney > 0){
            MarketManager.Instance.provideCard(ResourceManager.Instance.cardDatas[17]);
            MarketManager.Instance.bufferMoney --;
        }
    }






}
