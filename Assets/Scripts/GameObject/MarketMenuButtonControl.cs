using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketMenuButtonControl : MonoBehaviour
{
    public List<MarketSlot> marketSlots;

    public void ExitButton(){
        MenuManager.Instance.activiteMarketMenu(false);
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }
}
