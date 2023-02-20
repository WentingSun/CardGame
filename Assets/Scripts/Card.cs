using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class Card : MonoBehaviour
{

    public Card Instance;
    [SerializeField] int cardId;
    public LoadingBarControl loadingBar;
    public TextMeshProUGUI nameText;
    public Image cardImage;

    public CardData cardData;//TODO load cardData

    [SerializeField] Transform parentDuringMove;
    [SerializeField] Stack TargetStack;
    [SerializeField] Collider2D touchedCollider;
    [SerializeField] GameObject stack_Prefabs;

    [SerializeField] int resourceNum;

    public Stack currentStack;
    [SerializeField] List<Transform> upperCards;

    private Vector3 dragOffset;

    public int getCardId(){
        return this.cardId;
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Instance = this;
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
        dragOffset = this.transform.position - getMousePosition();
        Stack stack = currentStack;

        AddingUpperCards();
        Transform canvas = currentStack.transform.parent;

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
        stack.updateCardList();
        stack.enableCollider();



    }

    
    
    private void OnMouseDrag()
   {
    if(upperCards != null){
    upperCards[0].position = getMousePosition() + dragOffset;

        for(int i = 1; i < upperCards.Count; i++){
            upperCards[i].position = upperCards[i-1].position + new Vector3(0,-0.22f,0);
        }
    
    }else{
    transform.position = getMousePosition() + dragOffset;
    }
   }
   
   /// <summary>
   /// OnMouseDown is called when the user has pressed the mouse button while
   /// over the GUIElement or Collider.
   /// </summary>
   private void OnMouseUp() //TODO adding animation of following
   {
    if(touchedCollider != null){
        Instance.transform.position = getLowestPosition(TargetStack.transform) + new Vector3(0,-0.22f,0);
        Instance.transform.SetParent(touchedCollider.transform.parent);
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
    currentStack.updateCardList();
    currentStack.enableCollider();
    upperCards = null;
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
    // string text = this.transform.gameObject.name;
    // Debug.Log(text);
   }

    // Start is called before the first frame update
    void Start()
    {
        // getLoadingBar();
        loadCardData();
        if (currentStack == null){
            createStack();      
        }
        
        
        

    }

    public void loadCardData(){
        if(cardData != null){
            cardImage.sprite = cardData.cardPic;
            nameText.text = cardData.cardName;
            cardId = cardData.cardId;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void createStack(){
        GameObject newStack = Instantiate(stack_Prefabs,this.transform.parent);
        this.transform.SetParent(newStack.transform);
        currentStack = newStack.GetComponent<Stack>();
        currentStack.updateCardList();
    }

    private Vector3 getLowestPosition(Transform targetStack)
    {
        // Debug.Log(targetStack.childCount);
        var result = targetStack.GetChild(targetStack.childCount -1 ).position;
        return result;
    }

    public void followAnimation(Transform target, Transform follower){
        

    }


}