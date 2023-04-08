using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketMenuButtonControl : MonoBehaviour
{
    public List<MarketSlot> marketSlots;

    public void ExitButton(){
        MenuManager.Instance.activiteMarketMenu(false);
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
        provideEleAndMoney();
    }

    public void MarketButton(){
        if(GameManager.Instance.State == GameState.PlayerTurn){
            GameManager.Instance.UpdateGameState(GameState.Market);
            MenuManager.Instance.activiteMarketMenu(true);
            InformationManager.Instance.setMarketInfoBoxText(MarketManager.Instance.getCurrentMoneyNum().ToString(),
                                                             MarketManager.Instance.getCurrentElectricity().ToString());
            
        }

        
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
