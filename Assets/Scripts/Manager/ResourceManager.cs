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

    public CardData[] cardDatas;
    public WeatherCardData[] weatherCardDatas;

    override protected void Awake() {
        base.Awake();
        cardDatas = Resources.LoadAll<CardData>("CardData");
        weatherCardDatas = Resources.LoadAll<WeatherCardData>("WeatherCardData");
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

    




}
