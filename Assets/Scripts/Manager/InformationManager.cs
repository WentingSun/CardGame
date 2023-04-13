using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformationManager : Singleton<InformationManager>
{
    public TextMeshProUGUI CurrentElectricityInfo;
    public TextMeshProUGUI ElectricityRequireInfo;
    public TextMeshProUGUI ResidentNumInfo;
    public TextMeshProUGUI ResidentCapacityInfo;
    public TextMeshProUGUI WeatherNotaion;
    public TextMeshProUGUI MarketNotationInfo;

    public GameObject MarketInfoBox;


    private TextMeshProUGUI InformationBoxText;
    private GameObject InformationBox;

    


    private bool inforBoxActivity = false;

    private bool isShowing = false;

    protected override void Awake()
    {
        base.Awake();
        GameManager.OnGameStateChange += informationManagerOnGameStateChange;
        InformationBox = GameObject.Find("Information box");
        InformationBoxText = InformationBox.GetComponentInChildren<TextMeshProUGUI>();
        InformationBox.SetActive(false);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.OnGameStateChange -= informationManagerOnGameStateChange;
    }

    private void Start() {
        updateSmartMeterInfo();
    }

    public void setCurrentelectricityInfo(){
        int CurrentElectricityNum = ResourceManager.Instance.getElectricityCardNum();
        CurrentElectricityInfo.text = CurrentElectricityNum.ToString();
    }

    public void setElectricityRequireInfo(){
        int ElectricityRequireNum = ResourceManager.Instance.getElectricityCardRequire();
        ElectricityRequireInfo.text = ElectricityRequireNum.ToString();
    }

    public void setResidentNumInfo(){
        int currentHumanNum = ResourceManager.Instance.getTargetCardsNum(1);//1 means human card id
        ResidentNumInfo.text = currentHumanNum.ToString();
    }

    public void setResidentCapacityInfo(){
        int currentResidentCapacity = ResourceManager.Instance.getResidentCapacityNum();
        ResidentCapacityInfo.text = currentResidentCapacity.ToString();
    }

    public void setMarketNotationInfo(string contents){
        MarketNotationInfo.text = contents;
    }

    public void setWeatherNotationInfor(WeatherState weather){
        WeatherNotaion.text = ResourceManager.Instance.weatherDictionary[weather].description;
    }

    public void EmptyWeatherNotationInfor(){
        WeatherNotaion.text = "";
    }

    public void setMarketInfoBoxText(string moneyNum, string electricityNum){
        MarketInfoBox.transform.Find("MoneyNum").GetComponent<TextMeshProUGUI>().text = moneyNum;
        MarketInfoBox.transform.Find("ElectricityNum").GetComponent<TextMeshProUGUI>().text = electricityNum;
    }

    public void updateMarketInfoBoxText(){
        setMarketInfoBoxText(MarketManager.Instance.getCurrentMoneyNum().ToString(),
                             MarketManager.Instance.getCurrentElectricity().ToString());
    }


    public void updateSmartMeterInfo(){
        setCurrentelectricityInfo();
        setElectricityRequireInfo();
        setResidentNumInfo();
        setResidentCapacityInfo();
    }

    private void Update() {
        if(isShowing){
            isShowing = false;
        }else{
            dismissInformationBox();
        }
    }

    public void showInInformationBox(string contents, bool auto = false){
        if(inforBoxActivity){
            InformationBox.SetActive(true);
            InformationBoxText.text = contents;
            InformationBoxText.enableAutoSizing = auto;
            isShowing = true;
        }
        
    }

    public void showInInformationBox(string contents,CardData cardData,int resourceNum, bool auto = false){
        if(inforBoxActivity){
            InformationBox.SetActive(true);
            InformationBoxText.text = contents;
            InformationBoxText.enableAutoSizing = auto;
            isShowing = true;
        }
            InformationBoxText.text += getExtraInformation(cardData,resourceNum);
    }

    private string getExtraInformation(CardData cardData, int resourceNum){
        string result = "\n\n";
        switch(cardData.cardId){
            case 3:
            result += $"The remaining number of excavation attempts is {resourceNum}.";
            break;
            case 6:
            result += $"Remaining lifespan is {resourceNum}.";
            break;
            case 7:
            result += $"Remaining lifespan is {resourceNum}.";
            break;
            case 10:
            result += $"The remaining number of excavation attempts is {resourceNum}.";
            break;
            case 11:
            result += $"The stored amount of electricity is {resourceNum}.";
            break;
            case 12:
            result += $"The stored amount of electricity is {resourceNum}.";
            break;
            case 13:
            result += $"The stored amount of electricity is {resourceNum}.";
            break;
            default:
            
            break;
        }
        return result;
    }

    

    private void informationManagerOnGameStateChange(GameState gameState){
        if(gameState == GameState.Market){
            inforBoxActivity =false;
        }else{
            inforBoxActivity =true;
        }
    }

    public void dismissInformationBox(){
        InformationBox.SetActive(false);
    }

}
