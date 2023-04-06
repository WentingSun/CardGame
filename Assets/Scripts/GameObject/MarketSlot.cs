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

    private void Awake() {
        Product = GetComponentInChildren<Card>();
        Product.activateItCollider(false);
        priceText = transform.Find("Price").GetComponent<TextMeshProUGUI>();
        // resetTheProduct();
        // resetThePrice();
        
    }

    private void Start() {
        resetTheProduct();
        resetThePrice();
    }

    public void purchaseTheCard(){

        isSold = true;

    }

    public void resetTheProduct(){
        Debug.Log("resetTheProduct()");
        isSold = false;
        CardData newCardData = GameHelperFunction.RandomSelect(ResourceManager.Instance.productList);
        Product.loadCardDataInthisCard(newCardData);



    }

    private void resetThePrice(){
        System.Random random = new System.Random();
        int newPrice = random.Next(Product.cardData.basicPrice-3,Product.cardData.basicPrice+3);
        newPrice = Math.Max(1,newPrice);
        price = Math.Min(newPrice,Product.cardData.basicPrice*3);
        priceText.text = $"$ {price}";
    }

    
    private Card getProduct(){
        return Product;
    }

    private void getProductPrice(Card card){

    }


}
