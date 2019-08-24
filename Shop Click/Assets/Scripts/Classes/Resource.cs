using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{   
    private string label; public void SetLabel(string label){ this.label = label; } public string GetLabel(){ return(label); }
    private int level; public void SetLevel(int level){ this.level = level; } public int GetLevel(){ return(level); }
    private float rate; public void SetRate(float rate){ this.rate = rate; } public float GetRate(){ return(rate); }
    private ulong cap; public void SetCap(ulong cap){ this.cap = cap; } public ulong GetCap(){ return(cap); }

    private ulong amount;
    public void SetAmount(ulong amount){ this.amount = amount; amountLifetimeGain = amount; }
    public ulong GetAmount(){ return(amount); } 
    public void AddAmount(ulong amount)
    { 
        if((this.amount + amount) > cap){
            amountLifetimeGain += (cap - this.amount);
            this.amount = cap;
        }
        else
        {
            this.amount += amount; amountLifetimeGain += amount;
        }
        
    }
    public void IncrementAmount(){ amount += 1; amountLifetimeGain += 1; }
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


    // Not implemented yet, placeholder.
    // private ulong[] levelThresholds; public void SetLevelThresholds(ulong[] setLevelThresholds){ levelThresholds = setLevelThresholds; } public ulong[] GetLevelThresholds(){ return(levelThresholds); }

    // Use this for initialization
    public Resource(string label, int level, float rate, ulong cap, ulong amount)
    {
        this.label = label;
        this.level = level;
        this.rate = rate;
        this.cap = cap;
        this.amount = amount;
        // DebugLog();
    }

}
