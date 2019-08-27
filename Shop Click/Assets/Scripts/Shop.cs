using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    private Item currentItem;
    private Character currentCharacter;

    [SerializeField] public TextMeshProUGUI tMProQueue;
    public void UpdateTMProQueue() { tMProQueue.text = Global.instance.GetResources()["Queue"].ToStringAmount(); }

    [SerializeField] public GameObject panelShopDialog;
    
    [SerializeField] public Button buttonQueue;

    public void OnPressQueue()
    {
        if(Global.instance.GetResources()["Queue"].GetAmount() > 0){
            panelShopDialog.SetActive(true);
        }else{
            panelShopDialog.SetActive(false);
        }
    }


    [SerializeField] public TextMeshProUGUI tMProStock;
    public void UpdateTMProStock(){ tMProStock.text = currentItem.GetStock().ToString(); }

    // Need to bring buttons in to disable them.
    [SerializeField] public Button buttonRebate;
    [SerializeField] public Button buttonUpcharge;
    [SerializeField] public Button buttonSuggest;

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

    void Awake()
    {
        // Lifetime stats, should be put somewhere else when implementing save game.
        SetCountSales(0);
        SetCountRefusals(0);

        chakraSuggestCost = 30;
        chakraRebateGain = 20;
        chakraUpchargeCost = 40;
    }
    // Start is called before the first frame update
    void Start()
    {
        // First customer.
        NextCustomer();
    }
    // Update is called once per frame
    void Update()
    {
        // TODO: This is a temporary work around since UI wouldn't know what Item's stock to update.
        // :.. Consider making this a static class, so that the instance's currentItem can be accessed elsewhere.
        UpdateTMProStock();

    }
    public void NextCustomer(){

        if(Global.instance.GetResources()["Queue"].GetAmount() > 0)
        {
            Debug.Log("Next customer...");

            // Pick random item from inventory.
            currentItem = Global.instance.RandomItem();
            //Debug.Log(currentItem.name);
            // Update Item Sprite.
            UpdateSpriteItem();

            currentCharacter = Global.instance.RandomCharacter();
            UpdateSpriteCharacter();


            // Update values on all buttons.
            UpdateTMProSellChakra();
            UpdateTMProSuggest();
            UpdateTMProRebate();
            UpdateTMProUpcharge();
            ResetChakraRefuse();


            // Reset buttons.
            Helper.ButtonEnable(buttonRebate);
            Helper.ButtonEnable(buttonUpcharge);
            Helper.ButtonEnable(buttonSuggest);

            // Check Stock.
            UpdateTMProStock();

            // Update customer dialog.
            UpdateTMProDialog();

            // Set value for this transaction.
            SetSellGainCoins(currentItem.value);
        }
        else
        {
            panelShopDialog.SetActive(false);
        }


        // TODO: Need to think about where to add queue check/close panel, etc. when there are currently no customers.

        
    }
    public void OnPressSuggest()
    {
        //Debug.Log("Pressed Suggest...");
        if(Global.instance.GetStats()["Chakra"].CheckAmount((ulong)chakraSuggestCost)){
            Global.instance.GetStats()["Chakra"].RemoveAmount((ulong)chakraSuggestCost);
            
            // Disable buttons.
            Helper.ButtonDisable(buttonSuggest);

            // Change chakra cost for refusing
            AddChakraRefuse(chakraSuggestCost);
            
            // Go to different item, same person
            // TODO
        }
    }
    public void OnPressRebate()
    {
        //Debug.Log("Pressed Rebate...");
        Global.instance.GetStats()["Chakra"].AddAmount((ulong)chakraRebateGain);

        RebateSellGainCoins();

        // Disable buttons.
        Helper.ButtonDisable(buttonRebate);
        Helper.ButtonDisable(buttonUpcharge);
        Helper.ButtonDisable(buttonSuggest);

        // Change chakra cost for refusing
        RemoveChakraRefuse(chakraRebateGain);
    }
    public void OnPressUpcharge()
    {
        //Debug.Log("Pressed Upcharge...");
        if(Global.instance.GetStats()["Chakra"].CheckAmount((ulong)chakraUpchargeCost)){
            Global.instance.GetStats()["Chakra"].RemoveAmount((ulong)chakraUpchargeCost);
            UpchargeSellGainCoins();

            // Disable buttons.
            Helper.ButtonDisable(buttonRebate);
            Helper.ButtonDisable(buttonUpcharge);
            Helper.ButtonDisable(buttonSuggest);

            // Change chakra cost for refusing
            AddChakraRefuse(chakraUpchargeCost);
        }
    }
    public void OnPressSell()
    {
        //Debug.Log("Pressed Sell...");
        if(currentItem.CheckStock(1)){
            // Get coins from sale.
            Global.instance.GetStats()["Coins"].AddAmount(sellGainCoins);

            // Remove stock.
            currentItem.SoldItem();

            // Gain a bit of chakra from sale
            Global.instance.GetStats()["Chakra"].AddAmount((ulong)sellGainChakra);

            // Reset Chakra for refuse to 0.
            ResetChakraRefuse();

            // Increment sales count.
            IncrementCountSales();

            Global.instance.GetResources()["Queue"].DecrementAmount();
            UpdateTMProQueue();
            NextCustomer();
        }
    }
    public void OnPressRefuse(){
        Debug.Log("Pressed Refuse...");
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

        Global.instance.GetResources()["Queue"].DecrementAmount();
        UpdateTMProQueue();
        NextCustomer();
    }

}
