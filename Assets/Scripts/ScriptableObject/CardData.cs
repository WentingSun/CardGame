using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CardData")]
public class CardData : ScriptableObject
{
    public int cardId;
    public string cardName;
    public string description;
    public Sprite cardPic;
    public int resourceNum;

    public int basicPrice;

}
