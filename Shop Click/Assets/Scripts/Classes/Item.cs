using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Item
{
    public string name;
    public string category;
    public string filepathImage;
    public string prerequsiteUnlock;
    public ulong prerequsiteUnlockCostResearchScrolls;
    public ulong tier;
    public ulong value;
    public ulong timeCrafting;
    public ulong xPMerchant;
    public ulong xPWorker;
    public string prerequisiteWorker1;
    public string prerequisiteWorker1Level;
    public string prerequisiteWorker2;
    public string prerequisiteWorker2Level;
    public ulong costIron;
    public ulong costWood;
    public ulong costHide;
    public ulong costHerbs;
    public ulong costSteel;
    public ulong costTitanium; 
    public ulong costElectricity;
    public ulong costOil;
    public string costComponent1Name; 
    public string costComponent1Quality; 
    public ulong costComponent1; 
    public string costComponent2Name; 
    public string costComponent2Quality; 
    public ulong costComponent2; 
    public ulong statATK; 
    public ulong statDEF; 
    public ulong statHP; 
    public string upgradeCrafting1;
    public string upgradeCrafting1Count; 
    public string upgradeCrafting2;
    public string upgradeCrafting2Count; 
    public string upgradeCrafting3; 
    public string upgradeCrafting3Count; 
    public string upgradeCrafting4; 
    public string upgradeCrafting4Count; 
    public string upgradeCrafting5;
    public string upgradeCrafting5Count; 
    public string upgradeAscension1; 
    public string upgradeAscension1Cost; 
    public string upgradeAscension2; 
    public string upgradeAscension2Cost; 
    public string upgradeAscension3; 
    public string upgradeAscension3Cost; 
    public ulong energyDiscount;
    public ulong energySurcharge; 
    public ulong energySuggest;
    public ulong energySpeedUp;

    private bool isAvailable;
    public bool GetIsAvailable(){ return(isAvailable); }
    public void SetIsAvailable(bool isAvailable){ this.isAvailable = isAvailable; }
    private int stock;
    public int GetStock(){ return(stock); }
    public void SetStock(int stock){ this.stock = stock; }
    public bool CheckStock(int stock){ if(stock <= this.stock){ return(true); } else { return(false); }}
    public void AddStock(int stock){ this.stock += stock; }
    public int lifetimeCrafted;
    public void CraftItem(){ stock += 1; lifetimeCrafted += 1; }
    public int lifetimeSold;
    public void SoldItem(){ stock -= 1; lifetimeSold += 1; }

    public Item()
    {
        isAvailable = false;
        stock = 0;
        lifetimeCrafted = 0;
        lifetimeSold = 0;
    }
}
