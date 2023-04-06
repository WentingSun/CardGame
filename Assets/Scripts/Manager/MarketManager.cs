using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : Singleton<MarketManager>
{
    public GameObject testObj;

    protected override void Awake() {
        base.Awake();
        GameManager.OnGameStateChange += MarketManagerOnGameStateChange;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.OnGameStateChange -= MarketManagerOnGameStateChange;
    }


    public void testButton(){
        Card.createCard(ResourceManager.Instance.cardDatas[1], testObj.transform);
    }

    public void MarketButton(){
        if(GameManager.Instance.State == GameState.PlayerTurn){
            GameManager.Instance.UpdateGameState(GameState.Market);
        }
        
    }

    public void MarketManagerOnGameStateChange(GameState gameState){

    }


}
