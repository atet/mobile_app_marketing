using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
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

    [SerializeField] TextMeshProUGUI tMProSell;
    public void UpdateTMProSell(){ tMProSell.text = sellGain.ToString("N0"); }
    private ulong sellGain;
    public ulong GetSellGain(){ return(sellGain); }
    public void SetSellGain(ulong setSellGain){ sellGain = setSellGain; UpdateTMProSell(); }
    public void RebateSellGain(){ SetSellGain( (ulong)Mathf.RoundToInt(GetSellGain() / 2) ); }
    public void UpchargeSellGain(){ SetSellGain( GetSellGain() * 2 ); }

    // Start is called before the first frame update
    void Start()
    {
        UpdateTMProSuggest();
        UpdateTMProRebate();
        UpdateTMProUpcharge();
        AddChakraRefuse(0);
        SetSellGain(1000);
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
            
            // Change chakra cost for refusing
            AddChakraRefuse(chakraSuggestCost);
            
            
            // Go to different item, same person
        }
    }
    public void OnPressRebate()
    {
        Debug.Log("Pressed Rebate...");
        Global.instance.AddChakra(chakraRebateGain);
        RebateSellGain();

        // Change chakra cost for refusing
        AddChakraRefuse(-chakraRebateGain);
    }
    public void OnPressUpcharge()
    {
        Debug.Log("Pressed Upcharge...");
        if(Global.instance.CheckChakra(chakraUpchargeCost)){
            Global.instance.RemoveChakra(chakraUpchargeCost);
            UpchargeSellGain();

            // Change chakra cost for refusing
            AddChakraRefuse(chakraUpchargeCost);
        }
    }
    public void OnPressSell()
    {
        Debug.Log("Pressed Sell...");
        Global.instance.AddCoins(GetSellGain());

        // Reset Chakra for refuse to 0.
        ResetChakraRefuse();


        NextCustomer();
    }
    public void OnPressRefuse(){
        Debug.Log("Pressed Refuse...");
        // chakraRefuse will be a positive or negative number depending.
        Global.instance.AddChakra(chakraRefuse);
        // Reset Chakra for refuse to 0.
        ResetChakraRefuse();

        NextCustomer();
    }


    public void NextCustomer(){
        Debug.Log("Next customer...");


        // Go to next person (simulate).
        SetSellGain(1000);
    }
}
