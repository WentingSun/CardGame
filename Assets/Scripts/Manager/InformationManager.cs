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
    public TextMeshProUGUI MarketNotationInfo;


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

    public void setCurrentRlectricityInfo(){
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


    public void updateSmartMeterInfo(){
        setCurrentRlectricityInfo();
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
