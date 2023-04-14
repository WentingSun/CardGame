using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class Card : MonoBehaviour
{

    [SerializeField] int cardId;
    public LoadingBarControl loadingBar;
    public TextMeshProUGUI nameText;
    public Image cardImage;

    public CardData cardData;//TODO load cardData

    [SerializeField] Transform parentDuringMove;
    [SerializeField] Stack TargetStack;
    [SerializeField] Collider2D touchedCollider;


    public int resourceNum;

    public Stack currentStack;
    [SerializeField] List<Transform> upperCards;

    private Vector3 dragOffset;

    public bool isMove = false;

    public bool isInStore;


    public int getCardId(){
        return this.cardId;
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // GameManager.OnGameStateChange += CardOnGameStateChanged;
        if(!isInStore){
            ResourceManager.Instance.addingCardList(this);
        }
        
        
    }

    private void CardOnGameStateChanged(GameState gameState){
        isMove = (gameState == GameState.PlayerTurn);
    }

    public void activateMove(bool activity){
        isMove = activity;
    }

    public void activateItCollider(bool activity){
        this.gameObject.GetComponent<BoxCollider2D>().enabled = activity;
    }

    private void getLoadingBar(){
        // Debug.Log(this.transform.GetChild(3).gameObject.GetComponent<Slider>());
        loadingBar = this.transform.GetChild(this.transform.childCount-1).GetComponent<LoadingBarControl>();
    }

    private void AddingUpperCards(){
        upperCards = new List<Transform>();
        for(int i = transform.GetSiblingIndex(); i < currentStack.transform.childCount; i++ ){
            upperCards.Add(currentStack.transform.GetChild(i));
        }
        // Debug.Log(transform.GetSiblingIndex());
        // Debug.Log(currentStack.transform.childCount);
    }
    
    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    private void OnMouseDown()
    {
        if (isMove){
            dragOffset = this.transform.position - getMousePosition();
            Stack stack = currentStack;

            AddingUpperCards();
            Transform canvas = ResourceManager.Instance.canvasTransform;

            if (this.transform == currentStack.transform.GetChild(0)){
                foreach(Transform card in upperCards){
                    card.SetParent(canvas);
                    card.gameObject.GetComponent<Collider2D>().enabled = false;
                }
                    this.gameObject.GetComponent<Collider2D>().enabled = true;
                // Debug.Log("D");
                currentStack.destroyStack();
            }else{
                // Debug.Log("H");
                foreach(Transform card in upperCards){
                    card.SetParent(canvas);
                    card.gameObject.GetComponent<Collider2D>().enabled = false;
                }
                this.gameObject.GetComponent<Collider2D>().enabled = true;
            }
            stack.checkStackState();
            stack.enableCollider();
        }else{
            Debug.Log("You cant move this card now");
        }



    }

    public static void createCard(CardData cardData, Transform targetPlace){
        GameObject newCardObj = Instantiate(ResourceManager.Instance.cardTemple, targetPlace);
        Card newCard = newCardObj.GetComponent<Card>();
        newCard.cardData = cardData;
        newCard.loadCardData();
        newCard.transform.position = targetPlace.position;
        
        if(targetPlace.childCount == 1){
            newCard.createStack();
        }else{
            Stack targetStack = targetPlace.GetComponentInChildren<Card>().currentStack;
            newCard.currentStack = targetStack;
            newCard.transform.SetParent(targetStack.transform);
            newCard.transform.position = targetPlace.position + (targetStack.transform.childCount -1)* new Vector3(0,-0.22f,0);
        }
        
    }

    public void changeCardData(int Id){
        CardData newData = ResourceManager.Instance.cardDatas[Id];
        loadCardDataInthisCard(newData);
    }


    
    
    private void OnMouseDrag()
   {
    if(isMove){
        if(upperCards != null){
        upperCards[0].position = getMousePosition() + dragOffset;

            for(int i = 1; i < upperCards.Count; i++){
                upperCards[i].position = upperCards[i-1].position + new Vector3(0,-0.22f,0);
            }
        
        }else{
        transform.position = getMousePosition() + dragOffset;
        }
    }
   }
   
   /// <summary>
   /// OnMouseDown is called when the user has pressed the mouse button while
   /// over the GUIElement or Collider.
   /// </summary>
   private void OnMouseUp() //TODO adding animation of following
   {
    if(isMove){
        if(touchedCollider != null && touchedCollider.GetComponent<Card>() ){
            this.transform.position = getLowestPosition(TargetStack.transform) + new Vector3(0,-0.22f,0);
            this.transform.SetParent(touchedCollider.transform.parent);
            currentStack = TargetStack;
        }else {
            // Debug.Log("Should create a stack");
            createStack();
        }
        for(int i = 1; i< upperCards.Count; i++){
            // upperCards[i].gameObject.GetComponent<Collider2D>().enabled = true;
            upperCards[i].SetParent(currentStack.transform);
            upperCards[i].GetComponent<Card>().currentStack =currentStack;
            upperCards[i].position = upperCards[i-1].position + new Vector3(0,-0.22f,0);
        }
        // foreach(Transform card in upperCards){
        //     card.gameObject.GetComponent<Collider2D>().enabled = true;
        //     card.SetParent(currentStack.transform);
        //     card.GetComponent<Card>().currentStack =currentStack;
        // }
        currentStack.checkStackState();
        currentStack.enableCollider();
        upperCards = null;
        }
    }

   Vector3 getMousePosition()
   {
    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mousePosition.z = 0;
    return mousePosition;
   }

   /// <summary>
   /// Sent when another object enters a trigger collider attached to this
   /// object (2D physics only).
   /// </summary>
   /// <param name="other">The other Collider2D involved in this collision.</param>
   private void OnTriggerStay2D(Collider2D other)
   {
    touchedCollider = other;
    TargetStack = other.gameObject.transform.parent.GetComponent<Stack>();
   }

   /// <summary>
   /// Sent when another object leaves a trigger collider attached to
   /// this object (2D physics only).
   /// </summary>
   /// <param name="other">The other Collider2D involved in this collision.</param>
   private void OnTriggerExit2D(Collider2D other)
   {
    // Debug.Log("Leaveing");
    touchedCollider = null;
    TargetStack = null;
   }

   private void OnMouseOver(){
     InformationManager.Instance.showInInformationBox(this.cardData.description,this.cardData,this.resourceNum, true);    
   }

   void OnMouseEnter() {
   }

   void onMouseExit(){
    Debug.Log("Exit");
   }



    // Start is called before the first frame update
    void Start()
    {
        // getLoadingBar();
        loadCardData();
        if (currentStack == null && GameManager.Instance.State != GameState.Market){ // test code todo remove it in future
            createStack();      
        }
        
        
        
        if(GameManager.Instance.State == GameState.PlayerTurn) isMove = true;

    }

    public void loadCardData(){
        if(cardData != null){
            cardImage.sprite = cardData.cardPic;
            nameText.text = cardData.cardName;
            cardId = cardData.cardId;
            resourceNum = cardData.resourceNum;
        }
    }

    public void loadCardDataInthisCard(CardData _cardData){

        cardImage.sprite = _cardData.cardPic;
        nameText.text = _cardData.cardName;
        cardId = _cardData.cardId;
        cardData = _cardData;
        resourceNum = _cardData.resourceNum;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void createStack(){
        GameObject newStack = Instantiate(ResourceManager.Instance.stackTemple,this.transform.parent);
        this.transform.SetParent(newStack.transform);
        currentStack = newStack.GetComponent<Stack>();
        currentStack.checkStackState();
        ResourceManager.Instance.addingStackList(currentStack);
    }

    private Vector3 getLowestPosition(Transform targetStack)
    {
        // Debug.Log(targetStack.childCount);
        var result = targetStack.GetChild(targetStack.childCount -1 ).position;
        return result;
    }

    public void followAnimation(Transform target, Transform follower){
        

    }

    public void consumeThisCard(){
        if(this.transform == currentStack.transform.GetChild(0) && currentStack.transform.childCount == 1){
            currentStack.destroyStack();
        }else{
            Destroy(this.gameObject);
        }
    }

    public void OnDestroy(){
        // GameManager.OnGameStateChange -= CardOnGameStateChanged;
        if(!isInStore){
            ResourceManager.Instance.removingCardList(this);
        }
    }


}