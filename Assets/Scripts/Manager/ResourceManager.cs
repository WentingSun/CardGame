using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField] NormalBarControl NaturalBar;
    [SerializeField] NormalBarControl HumanitiesBar;
    [SerializeField] TurnBarControl TurnBar;

    public GameObject cardTemple;

    public GameObject stackTemple;

    [SerializeField] List<Card> WholeCardsList;
    [SerializeField] List<Stack> WholeStacksList;
    public List<WeatherState> WeatherCardDeck;
    

    private int rewardResisdentCapacity = 0;


    
    public Transform canvasTransform;
    public CardData[] cardDatas;
    public WeatherCardData[] weatherCardDatas;
    public Dictionary<WeatherState,WeatherCardData>  weatherDictionary;
    public Sprite[] cardPic;
    public Sprite[] weatherCardPic;
    public List<CardData> productList;

    override protected void Awake() {
        base.Awake();
        cardDatas = Resources.LoadAll<CardData>("CardData");
        weatherCardDatas = Resources.LoadAll<WeatherCardData>("WeatherCardData");
        cardPic = Resources.LoadAll<Sprite>("Image/CardPic");
        weatherCardPic = Resources.LoadAll<Sprite>("Image/WeatherCardPic");
        GameManager.OnGameStateChange += ResourceManagerOnGameStateChanged;
        weatherDictionary = weatherCardDatas.ToDictionary(x => x.weatherState, x =>x);
        WeatherCardDeck.AddRange(WeatherStateConstants.SpringWeatherStates);
        canvasTransform = GameObject.Find("Canvas").transform;
        productList = cardDatas.ToList().FindAll(x => x.basicPrice >= 0);
    }

    public void loadGameResource(){
        NaturalBar = GameObject.Find("NatureBar").GetComponent<NormalBarControl>();
        HumanitiesBar = GameObject.Find("HumanitiesBar").GetComponent<NormalBarControl>();
        TurnBar = GameObject.Find("TurnBar").GetComponent<TurnBarControl>();
        canvasTransform = GameObject.Find("Canvas").transform;
    }

    private void ResourceManagerOnGameStateChanged(GameState gameState){
    if(gameState == GameState.PlayerTurn){
        setAllCardActivity(true);
        }else{
        setAllCardActivity(false);
        }
    }

    private void setAllCardActivity(bool isActivity){
        foreach(Card cards in WholeCardsList){
            cards.activateMove(isActivity);
            cards.activateItCollider(isActivity);
          }
    }
    
    public void resetAllStackPos(){
        foreach(Stack stacks in WholeStacksList){
            stacks.resetStackedCardsPosition();
        }
    }

    public int getElectricityCardNum(){
        int result = 0;
        if(WholeCardsList != null){
            foreach(Card cards in WholeCardsList){
                if(cards.cardData != null && cards.cardData.cardId == 5){
                    result ++;
                }//todo else if(cards.cardData.cardId == ){result += cards.resuorceNum}
            }
          }
        return result;
    }

    public int getElectricityCardRequire(){//todo
        int result =0 ;
        foreach(Card cards in WholeCardsList){
            if(cards.cardData != null && cards.cardData.cardId == 8){
                result += 4;
            }
        }
        return result;
    }

    public int getTargetCardsNum(int TargetCardDataId){
        int result = 0;
        foreach(Card cards in WholeCardsList){
            if(cards.cardData != null && cards.cardData.cardId == TargetCardDataId){
                result ++;
            }
        }

        return result;
    }

    public int getResidentCapacityNum(){
        int result = rewardResisdentCapacity;
        result += getTargetCardsNum(8)*3;
        return result;
    }

    public int getRewardResisdentCapacity(){
        return this.rewardResisdentCapacity;
    }

    public void consumeElectricity(int num){
        for(int i =0 ; i <WholeCardsList.Count; i++){
            if(WholeCardsList[i].cardData.cardId == 5 && num > 0){
                WholeCardsList[i].consumeThisCard();
                num --;
            }
        }
    }

    public void consumeMoney(int num){
        if(num < 0) return ;
        for(int i =0 ; i <WholeCardsList.Count; i++){
             if(WholeCardsList[i].cardData.cardId == 17 && num > 0){
                WholeCardsList[i].consumeThisCard();
                num --;
            }
        }
    }


    public void consumeAllElectricity(){
        foreach(Card cards in WholeCardsList){
            if (cards.cardData.cardId == 4){
                cards.consumeThisCard();
            }
        }
    }

    public void resetTurnBar(float turnScale){
        TurnBar.setTheTurnTime(turnScale);
    }

    
    
    public void changeNaturalBarValue(float changeValue){
        NaturalBar.changeCurrentBarValue(changeValue);
    }
    public void changeHumanitiesBarValue(float changeValue){
        HumanitiesBar.changeCurrentBarValue(changeValue);
    }

    public void addingCardList(Card card){
        WholeCardsList.Add(card);
        InformationManager.Instance.updateSmartMeterInfo();
    }

    public void removingCardList(Card card){
        WholeCardsList.Remove(card);
        InformationManager.Instance.updateSmartMeterInfo();
    }

    public void addingStackList(Stack stack ){
        WholeStacksList.Add(stack);
        InformationManager.Instance.updateSmartMeterInfo();
    }

    public void removingStackList(Stack stack){
        WholeStacksList.Remove(stack);
        InformationManager.Instance.updateSmartMeterInfo();
    }



    override protected void OnDestroy() {
        base.OnDestroy();
        GameManager.OnGameStateChange -= ResourceManagerOnGameStateChanged;
    }




}

