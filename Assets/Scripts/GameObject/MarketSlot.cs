using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MarketSlot : MonoBehaviour
{
    // Start is called before the first frame update
    public Card Product;
    public int price;
    public bool isSold = false;

    public TextMeshProUGUI priceText;
    private string notaionContents;

    public GameObject SoldoutPic;

    private void Awake() {
        Product = GetComponentInChildren<Card>();
        Product.activateItCollider(false);
        priceText = transform.Find("Price").GetComponent<TextMeshProUGUI>();
        SoldoutPic = transform.Find("SoldoutPic").gameObject;
        // resetTheProduct();
        // resetThePrice();
        
    }

    private void Start() {
        resetTheProduct();
        resetThePrice();
    }

    public void purchaseTheCard(){ 
        notaionContents = "You Can't Buy It Now.";
        if(MarketManager.Instance.checkedMoneyState(price)){
            if(!isSold){
                isSold = true;
                SoldoutPic.SetActive(true);
                MarketManager.Instance.purchasingTheCard(Product.cardData,price);
                //todo
            }else{
                notaionContents += "\n It Is Sold Out!";
                InformationManager.Instance.setMarketNotationInfo(notaionContents);
            }
        }else{
            notaionContents += "\n You Don't Have Enough Money!";
            InformationManager.Instance.setMarketNotationInfo(notaionContents);
        }
        

    }


   private void resetTheProduct(){
        isSold = false;
        CardData newCardData = GameHelperFunction.RandomSelect(ResourceManager.Instance.productList);
        Product.loadCardDataInthisCard(newCardData);
        SoldoutPic.SetActive(false);


    }

    private void resetThePrice(){
        System.Random random = new System.Random();
        int newPrice = random.Next(Product.cardData.basicPrice,Product.cardData.basicPrice+3);
        newPrice = Math.Max(1,newPrice);
        price = Math.Min(newPrice,Product.cardData.basicPrice*3);
        priceText.text = $"$ {price}";
    }

    public void resetMarketSlot(){
        resetTheProduct();
        resetThePrice();
    }
    
    private Card getProduct(){
        return Product;
    }

    private void getProductPrice(Card card){

    }


}
