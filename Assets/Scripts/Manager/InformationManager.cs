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

    public void updateSmartMeterInfo(){
        setCurrentRlectricityInfo();
        setElectricityRequireInfo();
        setResidentNumInfo();
        setResidentCapacityInfo();
    }

}
