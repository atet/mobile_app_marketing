using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{   
    private string label; public void SetLabel(string label){ this.label = label; } public string GetLabel(){ return(label); }
    private string filepathImage; public void SetFilepathImage(string filepathImage){ this.filepathImage = filepathImage; } public string GetFilepathImage(){ return(filepathImage); }
    private int level; public void SetLevel(int level){ this.level = level; } public int GetLevel(){ return(level); }
    private float rate; public void SetRate(float rate){ this.rate = rate; } public float GetRate(){ return(rate); }


    private ulong currentInvestmentValue; // Coins invested in resource generation
    public ulong GetCurrentInvestmentValue(){ return(currentInvestmentValue); } 
    public void AddCurrentInvestmentValue(ulong currentInvestmentValue)
    { 
        this.currentInvestmentValue += currentInvestmentValue;
        CheckLevelUp2(); // Check if enough to level up.
    }

    // These values work for upgrading merchant experience OR incremental resource rate upgrades
    private bool thresholdBool = false; public bool GetThresholdBool(){ return(thresholdBool); } public void SetThresholdBool(bool thresholdBool){ this.thresholdBool = thresholdBool; }
    private int thresholdIndex;
    private List<int> thresholdKeys = new List<int>(); public List<int> GetThresholdKeys(){ return(thresholdKeys); } public void SetThresholdKeys(List<int> thresholdKeys){ this.thresholdKeys = thresholdKeys; }
    private ulong currentThresholdValue; public ulong GetCurrentThresholdValue(){ return(thresholdValues[thresholdIndex]); } 
    private List<ulong> thresholdValues = new List<ulong>(); public List<ulong> GetThresholdValues(){ return(thresholdValues); } public void SetThresholdValues(List<ulong> thresholdValues){ this.thresholdValues = thresholdValues; currentThresholdValue = this.thresholdValues[thresholdIndex]; }
    private List<float> thresholdRates = new List<float>(); public List<float> GetThresholdRates(){ return(thresholdRates); } public void SetThresholdRates(List<float> thresholdRates){ this.thresholdRates = thresholdRates; }
    private ulong currentThresholdIncrementCoins; public ulong GetCurrentThresholdIncrementCoins(){ return(thresholdIncrementsCoins[thresholdIndex]); }
    private List<ulong> thresholdIncrementsCoins = new List<ulong>(); public List<ulong> GetThresholdIncrementsCoins(){ return(thresholdIncrementsCoins); } public void SetThresholdIncrementsCoins(List<ulong> thresholdIncrementsCoins){ this.thresholdIncrementsCoins = thresholdIncrementsCoins; currentThresholdIncrementCoins = this.thresholdIncrementsCoins[thresholdIndex];  }
    private ulong currentThresholdIncrementGems; public ulong GetCurrentThresholdIncrementGems(){ return(thresholdIncrementsGems[thresholdIndex]); }
    private List<ulong> thresholdIncrementsGems = new List<ulong>(); public List<ulong> GetThresholdIncrementsGems(){ return(thresholdIncrementsGems); } public void SetThresholdIncrementsGems(List<ulong> thresholdIncrementsGems){ this.thresholdIncrementsGems = thresholdIncrementsGems; currentThresholdIncrementGems = this.thresholdIncrementsGems[thresholdIndex];  }    
    private List<string> thresholdTypes = new List<string>(); public List<string> GetThresholdTypes(){ return(thresholdTypes); } public void SetThresholdTypes(List<string> thresholdTypes){ this.thresholdTypes = thresholdTypes; }
    private List<string> thresholdDescriptions = new List<string>(); public List<string> GetThresholdDescriptions(){ return(thresholdDescriptions); } public void SetThresholdDescriptions(List<string> thresholdDescriptions){ this.thresholdDescriptions = thresholdDescriptions; }
    private List<bool> thresholdEventBools = new List<bool>();  public List<bool> GetThresholdEventBools(){ return(thresholdEventBools); } public void SetThresholdEventBools(List<bool> thresholdEventBools){ this.thresholdEventBools = thresholdEventBools; }


    // For upgrading cap
    private int thresholdCapIndex;
    private List<ulong> thresholdCaps = new List<ulong>(); 
    public string ToStringCap(){ return(cap.ToString("N0")); } public string ToStringNextCap(){ return(thresholdCaps[thresholdCapIndex + 1].ToString("N0")); }
    public List<ulong> GetThresholdCaps(){ return(thresholdCaps); } 
    public void SetThresholdCaps(List<ulong> thresholdCaps){ this.thresholdCaps = thresholdCaps; }
    private List<ulong> thresholdCapCostsCoins = new List<ulong>(); 
    public List<ulong> GetThresholdCapCostsCoins(){ return(thresholdCapCostsCoins); } 
    public void SetThresholdCapCostCoins(List<ulong> thresholdCapCostsCoins)
    {
        this.thresholdCapCostsCoins = thresholdCapCostsCoins;
        currentThresholdCapCostCoins = this.thresholdCapCostsCoins[thresholdCapIndex];
    }
    private ulong currentThresholdCapCostCoins; public ulong GetCurrentThresholdCapCostCoins(){ return(currentThresholdCapCostCoins); } 
    private List<ulong> thresholdCapCostsGems = new List<ulong>(); 
    public List<ulong> GetThresholdCapCostsGems(){ return(thresholdCapCostsGems); } 
    public void SetThresholdCapCostGems(List<ulong> thresholdCapCostsGems)
    {
        this.thresholdCapCostsGems = thresholdCapCostsGems;
        currentThresholdCapCostGems = this.thresholdCapCostsGems[thresholdCapIndex];
    }
    private ulong currentThresholdCapCostGems; public ulong GetCurrentThresholdCapCostGems(){ return(currentThresholdCapCostGems); } 



    private float timeRemaining;
    public void CheckTimeRemaining()
    {
        timeRemaining -= Time.deltaTime;
        if(timeRemaining < 0){
            //Debug.Log("Hello from " + label);
            IncrementAmount();
            // Reset time remianing
            timeRemaining = rate;
        }
    }

    private ulong cap; public void SetCap(ulong cap){ this.cap = cap; } public ulong GetCap(){ return(cap); }

    private ulong amount;
    public void SetAmount(ulong amount){ this.amount = amount; amountLifetimeGain = amount; }
    public ulong GetAmount(){ return(amount); } 
    public void AddAmount(ulong amount)
    { 
        if((this.amount + amount) <= cap)
        {
            this.amount += amount;
            AddAmountLifetimeGain(amount);
        }
        else
        {
            AddAmountLifetimeGain(cap - this.amount);
            this.amount = cap;   
        }
    }
    public void IncrementAmount()
    { 
        if((amount + 1) <= cap)
        {
            amount += 1;
            AddAmountLifetimeGain(1); 
        }
    }
    public bool CheckAmount(ulong amount){ if(amount <= this.amount){ return(true); }else{ return(false); } }
    public void DecrementAmount(){ amount -= 1; amountLifetimeSpend += 1; }
    public void RemoveAmount(ulong amount){ this.amount -= amount; amountLifetimeSpend += amount; }

    private ulong amountLifetimeGain;
    public void SetAmountLifetimeGain(ulong amountLifetimeGain)
    { this.amountLifetimeGain = amountLifetimeGain; }
    public ulong GetAmountLifetimeGain(){ return(amountLifetimeGain); }
    public void AddAmountLifetimeGain(ulong amountLifetimeGain)
    {
        // Must use this function when adding to amountLifetimeGain to also check for leveling up.
        this.amountLifetimeGain += amountLifetimeGain;
        if(thresholdBool)
        {
            CheckLevelUp();
        }
    }

    public void CheckLevelUp() // This is for merchant experience, nothing but level goes up
    {
        //Debug.Log("Called CheckLevelUp()"); 
        // TODO: Check where current amountLifetimeGain value is between to handle multiple levelups in a single call.
        if(amountLifetimeGain >= thresholdValues[thresholdIndex])
        {
            //Debug.Log("Level up triggered!");            
            LevelUp(thresholdIndex);
            thresholdIndex++;
        }
    }
    public void CheckLevelUp2() // This is for incremental resource rates, resource rates goes up
    {
        //Debug.Log("Called CheckLevelUp()2"); 
        // TODO: Check where current amountLifetimeGain value is between to handle multiple levelups in a single call.
        if(currentInvestmentValue >= currentThresholdValue)
        {
            //Debug.Log("CheckLevelUp2(): " + thresholdIndex);            
            LevelUp(thresholdIndex);
            thresholdIndex++;
            //Debug.Log("CheckLevelUp2(): " + thresholdIndex);   
            rate = thresholdRates[thresholdIndex] * (1 / Global.instance.GetGlobalMultiplier()); // New rate.
            currentThresholdValue = this.thresholdValues[thresholdIndex]; // New threshold
            currentThresholdIncrementCoins = this.thresholdIncrementsCoins[thresholdIndex]; // New increment amount
        }
    }
    public void CheckLevelUp3() // This is for resource caps, resource caps goes up
    {
        // No check here, check done in Town.cs
        //Debug.Log("Called CheckLevelUp3()");    
        LevelUp3(thresholdCapIndex);
        thresholdCapIndex++;
        cap = thresholdCaps[thresholdCapIndex]; // New cap.
        currentThresholdCapCostCoins = this.thresholdCapCostsCoins[thresholdCapIndex]; // New threshold
        currentThresholdCapCostGems = this.thresholdCapCostsGems[thresholdCapIndex];
    }
    public void LevelUp(int index)
    {
        //Debug.Log("Called LevelUp()");
        level = thresholdKeys[index];
        if(thresholdEventBools[index])
        {
            Tutorial.instance.SummonUIOverlayTextBox("Level Up!", thresholdDescriptions[index]);
        }
        // Make SFX sound
        SFX.instance.PlaySFXLevelUp();
    }
    public void LevelUp3(int index)
    {
        SFX.instance.PlaySFXLevelUp();
    }
    public ulong GetToNextLevelValue() // To show amount of thresholdValues[thresholdIndex] needed for nest level
    {
        return(thresholdValues[thresholdIndex] - amountLifetimeGain);
    }



    private ulong amountLifetimeSpend;
    public void SetAmountLifetimeSpend(ulong amountLifetimeSpend){ this.amountLifetimeSpend = amountLifetimeSpend; }
    public ulong GetAmountLifetimeSpend(){ return(amountLifetimeSpend); }


    public void DebugLog(){ Debug.Log("label: " + label + ", level: " + level.ToString() + ", rate: " + rate.ToString() + ", cap: " + cap.ToString() + ", amount: " + amount.ToString()); }

    public string ToStringAmount(){ return(amount.ToString("N0")); }

    public string ToStringRate(){ return(System.Math.Round(60 / rate, 1).ToString() + "/min."); }
    public string ToStringNextRate(){ return(System.Math.Round(60 / (thresholdRates[thresholdIndex + 1] * (1 / Global.instance.GetGlobalMultiplier()) ), 1).ToString() + "/min."); }

    public bool CheckCapacity()
    {
        if(amount < cap)
        {
            return(true);
        }
        else
        {
            return(false);
        }
    }


    // Not implemented yet, placeholder.
    // private ulong[] levelThresholds; public void SetLevelThresholds(ulong[] setLevelThresholds){ levelThresholds = setLevelThresholds; } public ulong[] GetLevelThresholds(){ return(levelThresholds); }

    // Use this for initialization
    public Resource(string label, string filepathImage, int level, float rate, ulong cap, ulong amount)
    {
        amountLifetimeGain = 0;
        amountLifetimeSpend = 0;
        
        thresholdIndex = 0;
        currentInvestmentValue = 0;
        //currentThresholdValue = thresholdValues[thresholdIndex];

        thresholdCapIndex = 0;


        this.label = label;
        this.filepathImage = filepathImage;
        this.level = level;
        this.rate = rate * (1 / Global.instance.GetGlobalMultiplier());
        timeRemaining = this.rate; // Initial set
        this.cap = cap;
        this.amount = amount;
        //Debug.Log(label + " = " + amount);
    }

}
