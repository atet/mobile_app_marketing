﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    private Item currentItem;
    private Character currentCharacter;

    [SerializeField] public TextMeshProUGUI tMProQueue;
    public void UpdateTMProQueue() { tMProQueue.text = Global.instance.GetResources()["resource_0"].ToStringAmount(); }

    [SerializeField] public GameObject panelShopDialog;
    
    [SerializeField] public Button buttonQueue;

    public void OnPressQueue()
    {
        if(
            (panelShopDialog.activeSelf == false) &
            (Global.instance.GetResources()["resource_0"].GetAmount() > 0)
        )
        {
            panelShopDialog.SetActive(true);
            
            if(!Global.instance.GetMODE_TUTORIAL())
            {
                NextCustomer("Random"); //
            }
            else
            {
                // First customer.
                TutorialFirstCustomer();
            }
        }else{
            //panelShopDialog.SetActive(false);
        }
    }


    [SerializeField] public TextMeshProUGUI tMProStock;
    public void UpdateTMProStock(){ tMProStock.text = currentItem.GetStock().ToString(); }

    // Need to bring buttons in to disable them.
    [SerializeField] public GameObject panelRefuse;
    [SerializeField] public GameObject panelRebate;
    [SerializeField] public GameObject PanelUpcharge;
    [SerializeField] public GameObject panelSuggest;

    // Bonus stuff.
    [SerializeField] public GameObject panelBonusButton;
    [SerializeField] public GameObject panelBonusDetail;
    

    private int chakraSuggestCost; private int chakraRebateGain; private int chakraUpchargeCost; 
    [SerializeField] public TextMeshProUGUI tMProSuggest, tMProRebate, tMProUpcharge;
    public void UpdateTMProSuggest(){ tMProSuggest.text = "-" + chakraSuggestCost.ToString() + "%"; }
    public void UpdateTMProRebate(){ tMProRebate.text = "+" + chakraRebateGain.ToString() + "%"; }
    public void UpdateTMProUpcharge(){ tMProUpcharge.text = "-" + chakraUpchargeCost.ToString() + "%"; }

    [SerializeField] public TextMeshProUGUI tMProRefuse;
    public void UpdateTMProRefuse(){ if(chakraRefuse >= 0){ tMProRefuse.text = "+" + chakraRefuse.ToString() + "%"; }else{ tMProRefuse.text = chakraRefuse.ToString() + "%"; } }
    private int chakraRefuse; // Can be a negative number!
    public int GetChakraRefuse(){ return(chakraRefuse); }
    public void AddChakraRefuse(int addChakraRefuse){ chakraRefuse += addChakraRefuse; UpdateTMProRefuse(); }
    public void RemoveChakraRefuse(int removeChakraRefuse){ chakraRefuse -= removeChakraRefuse; UpdateTMProRefuse(); }
    public void ResetChakraRefuse(){ chakraRefuse = 0; UpdateTMProRefuse(); }

    [SerializeField] public TextMeshProUGUI tMProSellCoins;
    public void UpdateTMProSellCoins(){ tMProSellCoins.text = "+" + sellGainCoins.ToString("N0"); }
    private ulong sellGainCoins; // Can be a negative number when buying is implemented!
    public ulong GetSellGainCoins(){ return(sellGainCoins); }
    public void SetSellGainCoins(ulong setSellGainCoins){ sellGainCoins = setSellGainCoins; UpdateTMProSellCoins(); }
    public void RebateSellGainCoins(){ SetSellGainCoins( (ulong)Mathf.RoundToInt(GetSellGainCoins() / 2) ); }
    public void UpchargeSellGainCoins(){ SetSellGainCoins( GetSellGainCoins() * 2 ); }

    [SerializeField] public TextMeshProUGUI tMProSellChakra;
    public void UpdateTMProSellChakra(){ tMProSellChakra.text = "+" + sellGainChakra.ToString() + "%"; }
    private int sellGainChakra = 1;
    public void AddSellGainChakra(int addSellGainChakra){ sellGainChakra += addSellGainChakra; UpdateTMProSellChakra(); }

    private ulong countSales;
    public ulong GetCountSales(){ return(countSales); }
    public void SetCountSales(ulong setCountSales){ countSales = setCountSales; }
    public void IncrementCountSales(){ countSales += 1; }

    private ulong countRefusals;
    public ulong GetCountRefusals(){ return(countRefusals); }
    public void SetCountRefusals(ulong setCountRefusals){ countRefusals = setCountRefusals; }
    public void IncrementCountRefusals(){ countRefusals += 1; }

    [SerializeField] public Image imageItem;
    public void UpdateSpriteItem(){ 
        imageItem.sprite = Resources.Load<Sprite>(currentItem.filepathImage); 
        imageItem.SetNativeSize();

    }

    [SerializeField] public Image imageCharacter;
    public void UpdateSpriteCharacter(){ 
        imageCharacter.sprite = Resources.Load<Sprite>(currentCharacter.filepathImage); 
        imageCharacter.SetNativeSize();
    }


    [SerializeField] public TextMeshProUGUI tMProDialog;
    public void UpdateTMProDialog(){ tMProDialog.text = currentCharacter.name + ": I would like to buy a " + currentItem.name + "."; }
    //public void UpdateTMProDialog(string dialog){ tMProDialog.text = ":" }
    void Awake()
    {
        // Lifetime stats, should be put somewhere else when implementing save game.
        SetCountSales(0);
        SetCountRefusals(0);

        chakraSuggestCost = 30;
        chakraRebateGain = 20;
        chakraUpchargeCost = 40;

        currentItem = Global.instance.RandomItem(false); // Temporarily have an item at the beginning.
    }
    // Start is called before the first frame update
    void Start()
    {
        // Close shop window if opened during dev.
        panelShopDialog.SetActive(false);

        panelBonusButton.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        // TODO: This is a temporary work around since UI wouldn't know what Item's stock to update.
        // :.. Consider making this a static class, so that the instance's currentItem can be accessed elsewhere.
        UpdateTMProStock();

    }
    public void UpdateAllButtons()
    {
            // Update values on all buttons.
            UpdateTMProSellChakra();
            UpdateTMProSuggest();
            UpdateTMProRebate();
            UpdateTMProUpcharge();
            ResetChakraRefuse();

            // Check Stock.
            UpdateTMProStock();

            // Update customer dialog.
            UpdateTMProDialog();

            // Reset buttons.
            Helper.ButtonEnable(panelRebate.transform.GetChild(0).gameObject.GetComponent<Button>());
            Helper.ButtonEnable(PanelUpcharge.transform.GetChild(0).gameObject.GetComponent<Button>());
            Helper.ButtonEnable(panelSuggest.transform.GetChild(0).gameObject.GetComponent<Button>());
    }

    public void NextCustomer(string itemName){

        if(Global.instance.GetResources()["resource_0"].GetAmount() > 0)
        {
            
            if(itemName == "Random")
            {
                //Debug.Log("Next random customer...");
                // Pick random item from inventory that is in stock, if nothing in stock will be something that can be made
                currentItem = Global.instance.RandomItem(true);
            } 
            else
            {
                //Debug.Log("Next customer specifically wanting: " + itemName);
                // Specific item to sell (e.g. Use for tutorial)
                currentItem = Global.instance.GetInventory()[itemName];
            }

            //Debug.Log(currentItem.name);
            // Update Item Sprite.
            UpdateSpriteItem();

            currentCharacter = Global.instance.RandomCharacter();
            UpdateSpriteCharacter();

            UpdateAllButtons();

            // Set value for this transaction.
            SetSellGainCoins(currentItem.value);
        }
        else
        {
            panelShopDialog.SetActive(false);
        }

    }
    public void OnPressRebate()
    {
        //Debug.Log("Pressed Rebate...");
        Global.instance.GetStats()["Chakra"].AddAmount((ulong)chakraRebateGain);

        RebateSellGainCoins();

        // Disable buttons.
        Helper.ButtonDisable(panelRebate.transform.GetChild(0).gameObject.GetComponent<Button>());
        Helper.ButtonDisable(PanelUpcharge.transform.GetChild(0).gameObject.GetComponent<Button>());
        Helper.ButtonDisable(panelSuggest.transform.GetChild(0).gameObject.GetComponent<Button>());

        // Change chakra cost for refusing
        RemoveChakraRefuse(chakraRebateGain);

        // Play SFX
        SFX.instance.PlaySFXRebate();
    }
    public void OnPressUpcharge()
    {
        //Debug.Log("Pressed Upcharge...");
        if(Global.instance.GetStats()["Chakra"].CheckAmount((ulong)chakraUpchargeCost)){
            Global.instance.GetStats()["Chakra"].RemoveAmount((ulong)chakraUpchargeCost);
            UpchargeSellGainCoins();

            // Disable buttons.
            Helper.ButtonDisable(panelRebate.transform.GetChild(0).gameObject.GetComponent<Button>());
            Helper.ButtonDisable(PanelUpcharge.transform.GetChild(0).gameObject.GetComponent<Button>());
            Helper.ButtonDisable(panelSuggest.transform.GetChild(0).gameObject.GetComponent<Button>());

            // Change chakra cost for refusing
            AddChakraRefuse(chakraUpchargeCost);

            // Play SFX
            SFX.instance.PlaySFXUpcharge();
        }
        else
        {
            // Play SFX
            SFX.instance.PlaySFXNoGo();
        }
    }
    public void OnPressSuggest()
    {
        //Debug.Log("Pressed Suggest...");
        if(
            Global.instance.GetStats()["Chakra"].CheckAmount((ulong)chakraSuggestCost) &&
            Global.instance.GetStats()["Stock"].CheckAmount(1) // Have at least something else in stock
          )
        {
            Global.instance.GetStats()["Chakra"].RemoveAmount((ulong)chakraSuggestCost);
            
            // Disable buttons.
            Helper.ButtonDisable(panelSuggest.transform.GetChild(0).gameObject.GetComponent<Button>());

            // Change chakra cost for refusing
            AddChakraRefuse(chakraSuggestCost);
            
            // Go to different item, same person
            // TODO: No window selection, just go to random new item in stock
            currentItem = Global.instance.RandomItem(true);
            //Debug.Log("Suggested item is: " + currentItem.name);
            UpdateSpriteItem();
            //UpdateSpriteCharacter();
            SetSellGainCoins(currentItem.value);

            // Play SFX
            SFX.instance.PlaySFXSuggest();
        }
        else
        {
            // Play SFX
            SFX.instance.PlaySFXNoGo();
        }
    }
    public void OnPressSell()
    {
        //Debug.Log("Pressed Sell...");
        if(currentItem.CheckStock(1)){
            // Get coins from sale.
            Global.instance.GetStats()["Coins"].AddAmount(sellGainCoins);

            // Get experience from sale.
            Global.instance.GetStats()["Level"].AddAmount(currentItem.xPMerchant);
            //Debug.Log("XP from sale: " + currentItem.xPMerchant + "(Lifetime " + currentItem.lifetimeCrafted + ")");

            // Remove stock.
            currentItem.SoldItem();

            // Gain a bit of chakra from sale
            Global.instance.GetStats()["Chakra"].AddAmount((ulong)sellGainChakra);

            // Reset Chakra for refuse to 0.
            ResetChakraRefuse();

            // Increment sales count.
            IncrementCountSales();

            // Decrement Global stock count.
            Global.instance.GetStats()["Stock"].DecrementAmount();

            Global.instance.GetResources()["resource_0"].DecrementAmount();
            UpdateTMProQueue();

            NextTransaction();

            // Play SFX
            SFX.instance.PlaySFXSale();
        }
        else
        {
             // Play SFX
            SFX.instance.PlaySFXNoGo();           
        }
    }
    public void OnPressRefuse(){
        //Debug.Log("Pressed Refuse...");
        // chakraRefuse will be a positive or negative number depending.

        if(chakraRefuse >= 0)
        {
            Global.instance.GetStats()["Chakra"].AddAmount((ulong)chakraRefuse);
        }
        else
        {
            Global.instance.GetStats()["Chakra"].RemoveAmount((ulong)chakraRefuse);
        }

        // Reset Chakra for refuse to 0.
        ResetChakraRefuse();

        // Increment refusal count.
        IncrementCountRefusals();

        Global.instance.GetResources()["resource_0"].DecrementAmount();
        UpdateTMProQueue();

        NextTransaction();

        // Play SFX
        SFX.instance.PlaySFXRefuse();
    }


    public void NextTransaction()
    {
        if(!Global.instance.GetMODE_TUTORIAL())
        {
            NextCustomer("Random");
        }
        else
        {
            switch(Tutorial.instance.GetID_TUTORIAL_EVENT())
            {
                case 1:
                    TutorialSecondCustomer();
                    break;
                case 2:
                    TutorialThirdCustomer();
                    break;
                case 3:
                    TutorialFourthCustomer();
                    break;
                case 4:
                    TutorialFifthCustomer();
                    break;
                case 5:
                    TutorialSixthCustomer();
                    break;
                default:
                    TutorialDone();
                    NextCustomer("Random");
                    break;
            }
        }
    }




    // TUTORIAL EVENTS
    // Before anything starts, point out the customer queue.
    public void TutorialDone(){
        //Debug.Log("Tutorial done.");
        Global.instance.SetMODE_TUTORIAL(false);
        panelRefuse.SetActive(true);
        panelRebate.SetActive(true);
        PanelUpcharge.SetActive(true);
        panelSuggest.SetActive(true);
        CameraControl.instance.EnableSwipe();
        CameraControl.instance.EnableSwipeMine();
        CameraControl.instance.EnableSwipeTown();
        CameraControl.instance.EnableSwipeColosseum();

        panelBonusButton.SetActive(true);

        Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Congrats!", "You're ready to take over my shop.\n\nSwipe up to go to town.\n\nGood luck!\n\n- Bigly", "Images/UI/hand_swipe_static_up");
    }
    public void TutorialFirstCustomer()
    {
        //Debug.Log("First tutorial customer: Selling an item.");
        currentItem = Global.instance.GetInventory()["sword_1"];

        // Update Item Sprite.
        UpdateSpriteItem();

        currentCharacter = Global.instance.RandomCharacter();
        UpdateSpriteCharacter();

        UpdateAllButtons();

        // Hide these for now
        panelRefuse.SetActive(false);
        panelRebate.SetActive(false);
        PanelUpcharge.SetActive(false);
        panelSuggest.SetActive(false);

        // Set value for this transaction.
        SetSellGainCoins(currentItem.value);

        // Custom character picture and text.
        imageCharacter.sprite = Resources.Load<Sprite>("Images/Characters/20");
        tMProDialog.text = "Bigly: Sell me this item for some coins.";

        Tutorial.instance.IncrementID_TUTORIAL_EVENT();

        Tutorial.instance.SummonUIOverlayPointer("ButtonSell");
    }
    public void TutorialSecondCustomer()
    {
        //Debug.Log("Second tutorial customer: Rebating an item to get energy.");
        currentItem = Global.instance.GetInventory()["staff_1"];

        // Update Item Sprite.
        UpdateSpriteItem();

        currentCharacter = Global.instance.RandomCharacter();
        UpdateSpriteCharacter();

        UpdateAllButtons();

        // Hide these for now
        panelRefuse.SetActive(false);
        panelRebate.SetActive(true);
        PanelUpcharge.SetActive(false);
        panelSuggest.SetActive(false);

        // Set value for this transaction.
        SetSellGainCoins(currentItem.value);

        // Custom character picture and text.
        imageCharacter.sprite = Resources.Load<Sprite>("Images/Characters/20");
        tMProDialog.text = "Bigly: Save up energy by rebating.";

        Tutorial.instance.IncrementID_TUTORIAL_EVENT();

        Tutorial.instance.SummonUIOverlayPointer("ButtonRebate");
    }

    public void TutorialThirdCustomer()
    {
        //Debug.Log("Third tutorial customer: Spending energy to upcharge");
        currentItem = Global.instance.GetInventory()["shoes_1"];

        // Update Item Sprite.
        UpdateSpriteItem();

        currentCharacter = Global.instance.RandomCharacter();
        UpdateSpriteCharacter();

        UpdateAllButtons();

        // Hide these for now
        panelRefuse.SetActive(false);
        panelRebate.SetActive(false);
        PanelUpcharge.SetActive(true);
        panelSuggest.SetActive(false);

        // Set value for this transaction.
        SetSellGainCoins(currentItem.value);

        // Custom character picture and text.
        imageCharacter.sprite = Resources.Load<Sprite>("Images/Characters/20");
        tMProDialog.text = "Bigly: Spend that energy to upcharge!";

        Tutorial.instance.IncrementID_TUTORIAL_EVENT();

        Tutorial.instance.SummonUIOverlayPointer("ButtonUpcharge");
    }

    public void TutorialFourthCustomer()
    {
        //Debug.Log("Fourth tutorial customer: We don't know how to even make this item... yet. Refuse.");
        currentItem = Global.instance.GetInventory()["ring_7"];

        // Update Item Sprite.
        UpdateSpriteItem();

        currentCharacter = Global.instance.RandomCharacter();
        UpdateSpriteCharacter();

        UpdateAllButtons();

        // Hide these for now
        panelRefuse.SetActive(true);
        panelRebate.SetActive(false);
        PanelUpcharge.SetActive(false);
        panelSuggest.SetActive(false);

        // Set value for this transaction.
        SetSellGainCoins(currentItem.value);

        // Custom character picture and text.
        imageCharacter.sprite = Resources.Load<Sprite>("Images/Characters/20");
        tMProDialog.text = "Bigly: Can't make this yet, Refuse sale.";


        Tutorial.instance.IncrementID_TUTORIAL_EVENT();

        Tutorial.instance.SummonUIOverlayPointer("ButtonRefuse");
    }

    public void TutorialFifthCustomer()
    {
        //Debug.Log("Fifth tutorial customer: We don't have but we can make, go to Mine.");
        currentItem = Global.instance.GetInventory()["sword_1"];

        // Update Item Sprite.
        UpdateSpriteItem();

        currentCharacter = Global.instance.RandomCharacter();
        UpdateSpriteCharacter();

        UpdateAllButtons();

        // Hide these for now
        panelRefuse.SetActive(false);
        panelRebate.SetActive(false);
        PanelUpcharge.SetActive(false);
        panelSuggest.SetActive(false);

        // Set value for this transaction.
        SetSellGainCoins(currentItem.value);

        // Custom character picture and text.
        imageCharacter.sprite = Resources.Load<Sprite>("Images/Characters/20");
        tMProDialog.text = "Bigly: Not in stock, but we can make it.";

        CameraControl.instance.EnableSwipeMine();

        Tutorial.instance.IncrementID_TUTORIAL_EVENT();

        // Instructions to swipe to go left
        Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Crafting", "Squire Swords are out of stock.\n\nSwipe right to go to the Mine to craft another one.\n\n- Bigly", "Images/UI/hand_swipe_static_right");
    }

    public void TutorialSixthCustomer()
    {
        //Debug.Log("Sixth tutorial customer: Suggest something else.");
        currentItem = Global.instance.GetInventory()["harmor_7"];

        // Update Item Sprite.
        UpdateSpriteItem();

        currentCharacter = Global.instance.RandomCharacter();
        UpdateSpriteCharacter();

        UpdateAllButtons();

        // Hide these for now
        panelRefuse.SetActive(false);
        panelRebate.SetActive(false);
        PanelUpcharge.SetActive(false);
        panelSuggest.SetActive(true);

        // Set value for this transaction.
        SetSellGainCoins(currentItem.value);

        // Custom character picture and text.
        imageCharacter.sprite = Resources.Load<Sprite>("Images/Characters/20");
        tMProDialog.text = "Bigly: Suggest something in stock.";

        Tutorial.instance.IncrementID_TUTORIAL_EVENT();

        Tutorial.instance.SummonUIOverlayPointer("ButtonSuggest");
    }

}
