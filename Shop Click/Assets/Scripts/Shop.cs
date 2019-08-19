using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{

    [SerializeField] Button buttonRebate;
    [SerializeField] Button buttonUpcharge;
    [SerializeField] Button buttonSuggest;



    private const int chakraSuggestCost = 30; private const int chakraRebateGain = 20; private const int chakraUpchargeCost = 40; 
    [SerializeField] TextMeshProUGUI tMProSuggest, tMProRebate, tMProUpcharge;
    public void UpdateTMProSuggest(){ tMProSuggest.text = "-" + chakraSuggestCost.ToString() + "%"; }
    public void UpdateTMProRebate(){ tMProRebate.text = "+" + chakraRebateGain.ToString() + "%"; }
    public void UpdateTMProUpcharge(){ tMProUpcharge.text = "-" + chakraUpchargeCost.ToString() + "%"; }
    //public int GetChakraSuggestCost(){ return(chakraSuggestCost); }
    //public int GetChakraRebateGain(){ return(chakraRebateGain); }
    //public int GetChakraUpchargeCost(){ return(chakraUpchargeCost); }

    [SerializeField] TextMeshProUGUI tMProRefuse;
    public void UpdateTMProRefuse(){ if(chakraRefuse >= 0){ tMProRefuse.text = "+" + chakraRefuse.ToString() + "%"; }else{ tMProRefuse.text = chakraRefuse.ToString() + "%"; } }
    private int chakraRefuse;
    public int GetChakraRefuse(){ return(chakraRefuse); }
    public void AddChakraRefuse(int addChakraRefuse){ chakraRefuse += addChakraRefuse; UpdateTMProRefuse(); }
    public void ResetChakraRefuse(){ chakraRefuse = 0; UpdateTMProRefuse(); }

    [SerializeField] TextMeshProUGUI tMProSellCoins;
    public void UpdateTMProSellCoins(){ tMProSellCoins.text = "+" + sellGainCoins.ToString("N0"); }
    private ulong sellGainCoins;
    public ulong GetSellGainCoins(){ return(sellGainCoins); }
    public void SetSellGainCoins(ulong setSellGainCoins){ sellGainCoins = setSellGainCoins; UpdateTMProSellCoins(); }
    public void RebateSellGainCoins(){ SetSellGainCoins( (ulong)Mathf.RoundToInt(GetSellGainCoins() / 2) ); }
    public void UpchargeSellGainCoins(){ SetSellGainCoins( GetSellGainCoins() * 2 ); }


    [SerializeField] TextMeshProUGUI tMProSellChakra;
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


    // Start is called before the first frame update
    void Start()
    {
        // New game.
        SetCountSales(0);
        SetCountRefusals(0);



        UpdateTMProSellChakra();
        UpdateTMProSuggest();
        UpdateTMProRebate();
        UpdateTMProUpcharge();
        AddChakraRefuse(0);
        
        // First customer.
        NextCustomer();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPressSuggest()
    {
        Debug.Log("Pressed Suggest...");
        if(Global.instance.CheckChakra(chakraSuggestCost)){
            Global.instance.RemoveChakra(chakraSuggestCost);
            

            // Disable buttons.
            ButtonDisable(buttonSuggest);

            // Change chakra cost for refusing
            AddChakraRefuse(chakraSuggestCost);
            
            
            // Go to different item, same person
        }
    }
    public void OnPressRebate()
    {
        Debug.Log("Pressed Rebate...");
        Global.instance.AddChakra(chakraRebateGain);
        RebateSellGainCoins();

        // Disable buttons.
        ButtonDisable(buttonRebate);
        ButtonDisable(buttonUpcharge);
        ButtonDisable(buttonSuggest);

        // Change chakra cost for refusing
        AddChakraRefuse(-chakraRebateGain);
    }
    public void OnPressUpcharge()
    {
        Debug.Log("Pressed Upcharge...");
        if(Global.instance.CheckChakra(chakraUpchargeCost)){
            Global.instance.RemoveChakra(chakraUpchargeCost);
            UpchargeSellGainCoins();

            // Disable buttons.
            ButtonDisable(buttonRebate);
            ButtonDisable(buttonUpcharge);
            ButtonDisable(buttonSuggest);


            // Change chakra cost for refusing
            AddChakraRefuse(chakraUpchargeCost);
        }
    }
    public void OnPressSell()
    {
        Debug.Log("Pressed Sell...");
        Global.instance.AddCoins(GetSellGainCoins());

        // Gain a bit of chakra from sale
        Global.instance.AddChakra(sellGainChakra);

        // Reset Chakra for refuse to 0.
        ResetChakraRefuse();

        // Increment sales count.
        IncrementCountSales();


        NextCustomer();
    }
    public void OnPressRefuse(){
        Debug.Log("Pressed Refuse...");
        // chakraRefuse will be a positive or negative number depending.
        Global.instance.AddChakra(chakraRefuse);
        // Reset Chakra for refuse to 0.
        ResetChakraRefuse();

        // Increment refusal count.
        IncrementCountRefusals();

        NextCustomer();
    }


    public void NextCustomer(){
        Debug.Log("Next customer...");


        // Reset buttons.
        ButtonEnable(buttonRebate);
        ButtonEnable(buttonUpcharge);
        ButtonEnable(buttonSuggest);

        // Go to next person (simulate).
        SetSellGainCoins(1000);
    }



    public void ButtonEnable(Button button)
    {
        button.interactable = true;
    }
    public void ButtonDisable(Button button)
    {
        button.interactable = false;
    }
}
