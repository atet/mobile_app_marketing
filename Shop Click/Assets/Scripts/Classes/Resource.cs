using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{   
    private string label; public void SetLabel(string label){ this.label = label; } public string GetLabel(){ return(label); }
    private int level; public void SetLevel(int level){ this.level = level; } public int GetLevel(){ return(level); }
    private float rate; public void SetRate(float rate){ this.rate = rate; } public float GetRate(){ return(rate); }

    //private ulong[] thresholds; public ulong[] GetThresholds{ return(thresholds); } public void SetThresholds(){ this.thresholds = thresholds; }
    //private ulong[] thresholds_value; public ulong[] GetThresholdsValue{ return(thresholds_value); } public void SetThresholdsValues(){ this.thresholds_value = thresholds_value; }
    //private string[] thresholds_type; public ulong[] GetThresholdsType{ return(thresholds_type); } public void SetThresholdsType(){ this.thresholds_type = thresholds_type; }

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
        if((this.amount + amount) <= cap){
            this.amount += amount; amountLifetimeGain += amount;
            
        }
        else
        {
            amountLifetimeGain += (cap - this.amount);
            this.amount = cap;
        }
        
    }
    public void IncrementAmount()
    { 
        if((amount + 1) <= cap){
        amount += 1;
        amountLifetimeGain += 1; 
        }
    }
    public bool CheckAmount(ulong amount){ if(amount <= this.amount){ return(true); }else{ return(false); } }
    public void DecrementAmount(){ amount -= 1; amountLifetimeSpend += 1; }
    public void RemoveAmount(ulong amount){ this.amount -= amount; amountLifetimeSpend += amount; }

    private ulong amountLifetimeGain;
    public void SetAmountLifetimeGain(ulong amountLifetimeGain){ this.amountLifetimeGain = amountLifetimeGain; }
    public ulong GetAmountLifetimeGain(){ return(amountLifetimeGain); }

    private ulong amountLifetimeSpend;
    public void SetAmountLifetimeSpend(ulong amountLifetimeSpend){ this.amountLifetimeSpend = amountLifetimeSpend; }
    public ulong GetAmountLifetimeSpend(){ return(amountLifetimeSpend); }


    public void DebugLog(){ Debug.Log("label: " + label + ", level: " + level.ToString() + ", rate: " + rate.ToString() + ", cap: " + cap.ToString() + ", amount: " + amount.ToString()); }

    public string ToStringAmount(){ return(amount.ToString("N0")); }

    public string ToStringRate(){ return(System.Math.Round(60 / rate, 1).ToString() + "/min."); }

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
    public Resource(string label, int level, float rate, ulong cap, ulong amount)
    {
        this.label = label;
        this.level = level;
        this.rate = rate * (1 / Global.instance.GetGlobalMultiplier());
        timeRemaining = this.rate; // Initial set
        this.cap = cap;
        this.amount = amount;
        // DebugLog();
    }

}
