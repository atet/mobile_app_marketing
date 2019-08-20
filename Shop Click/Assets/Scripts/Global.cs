using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global instance;


    private Resource level; public Resource GetLevel(){ return(level); }

    private Resource coins; public Resource GetCoins(){ return(coins); }

    private Resource chakra; public Resource GetChakra(){ return(chakra); }

    private Resource gems; public Resource GetGems(){ return(gems); }


    private Resource iron; public Resource GetIron(){ return(iron); }

    private Resource hide; public Resource GetHide(){ return(hide); }

    private Resource wood; public Resource GetWood(){ return(wood); }

    private Resource herbs; public Resource GetHerbs(){ return(herbs); }

    private Resource steel; public Resource GetSteel(){ return(steel); }

    private Resource oil; public Resource GetOil(){ return(oil); }

    private Resource electricity; public Resource GetElectricity(){ return(electricity); }

    private Resource titanium; public Resource GetTitanium(){ return(titanium); }


    void Start()
    {
        instance = this;

        level = new Resource("Level", 1, 0, System.UInt64.MaxValue, 1);
        coins = new Resource("Coins", 1, 0, System.UInt64.MaxValue, 0);
        chakra = new Resource("Chakra", 1, 0, 100, 39);
        gems = new Resource("Gems", 1, 0, System.UInt64.MaxValue, 5);

        Debug.Log("Resource level.label = " + level.GetLabel());
        Debug.Log("Resource level.amount = " + level.GetAmount().ToString());

        iron = new Resource("Iron", 1, 1, 20, 10);
        hide = new Resource("Hide", 1, 1,  0,  0);
        wood = new Resource("Wood", 1, 1,  0,  0);
        herbs = new Resource("Herbs", 1, 1,  0,  0);
        steel = new Resource("Steel", 1, 1,  0,  0);
        oil = new Resource("Oil", 1, 1,  0,  0);
        electricity = new Resource("Electricity", 1, 1,  0,  0);
        titanium = new Resource("Titanium", 1, 1,  0,  0);

    }



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
        public void RemoveAmount(ulong amount){ this.amount -= amount; amountLifetimeSpend += amount; }

        private ulong amountLifetimeGain;
        public void SetAmountLifetimeGain(ulong amountLifetimeGain){ this.amountLifetimeGain = amountLifetimeGain; }
        public ulong GetAmountLifetimeGain(){ return(amountLifetimeGain); }

        private ulong amountLifetimeSpend;
        public void SetAmountLifetimeSpend(ulong amountLifetimeSpend){ this.amountLifetimeSpend = amountLifetimeSpend; }
        public ulong GetAmountLifetimeSpend(){ return(amountLifetimeSpend); }

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
        }

    }

}


