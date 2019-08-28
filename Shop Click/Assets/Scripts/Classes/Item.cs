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

    private bool isFavorite; public bool GetIsFavorite(){ return(isFavorite); } public void SetIsFavorite(bool isFavorite){ this.isFavorite = isFavorite; }
    private bool isAvailable; public bool GetIsAvailable(){ return(isAvailable); } public void SetIsAvailable(bool isAvailable){ this.isAvailable = isAvailable; }
    private int stock;
    public int GetStock(){ return(stock); }
    public void SetStock(int stock){ this.stock = stock; Global.instance.GetStats()["Stock"].AddAmount((ulong)stock); }
    public bool CheckStock(int stock){ if(stock <= this.stock){ return(true); } else { return(false); }}
    public void AddStock(int stock){ this.stock += stock; Global.instance.GetStats()["Stock"].AddAmount(System.Convert.ToUInt64(stock)); }
    public int lifetimeCrafted;

    public void AcquireResources()
    {
        //Debug.Log("Acquiring resources for " + name + ".");
        Global.instance.GetResources()["Iron"].RemoveAmount(costIron);
        Global.instance.GetResources()["Wood"].RemoveAmount(costWood);
        Global.instance.GetResources()["Hide"].RemoveAmount(costHide);
        Global.instance.GetResources()["Herbs"].RemoveAmount(costHerbs);
        Global.instance.GetResources()["Steel"].RemoveAmount(costSteel);
        Global.instance.GetResources()["Titanium"].RemoveAmount(costTitanium);
        Global.instance.GetResources()["Electricity"].RemoveAmount(costElectricity);
        Global.instance.GetResources()["Oil"].RemoveAmount(costOil);
    }
    public void CraftItem()
    {
        Debug.Log(name + " has been crafted.");
        stock += 1; lifetimeCrafted += 1;
        // Now increment Global stock
        Global.instance.GetStats()["Stock"].IncrementAmount();
    }
    public int lifetimeSold;
    public void SoldItem(){ stock -= 1; lifetimeSold += 1; }

    public bool CheckResource()
    {
            //Debug.Log("Iron: " + Global.instance.GetResources()["Iron"].ToStringAmount() + " > cost " + costIron + " = " + Global.instance.GetResources()["Iron"].CheckAmount(costIron));
            //Debug.Log("Wood: " + Global.instance.GetResources()["Wood"].ToStringAmount() + " > cost " + costWood + " = " + Global.instance.GetResources()["Wood"].CheckAmount(costWood));
            //Debug.Log("Hide: " + Global.instance.GetResources()["Hide"].ToStringAmount() + " > cost " + costHide + " = " + Global.instance.GetResources()["Hide"].CheckAmount(costHide));
            //Debug.Log("Herbs: " + Global.instance.GetResources()["Herbs"].ToStringAmount() + " > cost " + costHerbs + " = " + Global.instance.GetResources()["Herbs"].CheckAmount(costHerbs));
            //Debug.Log("Steel: " + Global.instance.GetResources()["Steel"].ToStringAmount() + " > cost " + costSteel + " = " + Global.instance.GetResources()["Steel"].CheckAmount(costSteel));
            //Debug.Log("Titanium: " + Global.instance.GetResources()["Titanium"].ToStringAmount() + " > cost " + costTitanium + " = " + Global.instance.GetResources()["Titanium"].CheckAmount(costTitanium));
            //Debug.Log("Electricity: " + Global.instance.GetResources()["Electricity"].ToStringAmount() + " > cost " + costElectricity + " = " + Global.instance.GetResources()["Electricity"].CheckAmount(costElectricity));
            //Debug.Log("Oil: " + Global.instance.GetResources()["Oil"].ToStringAmount() + " > cost " + costOil + " = " + Global.instance.GetResources()["Oil"].CheckAmount(costOil));


        if(
            (Global.instance.GetResources()["Iron"].CheckAmount(costIron)) &&
            (Global.instance.GetResources()["Wood"].CheckAmount(costWood)) &&
            (Global.instance.GetResources()["Hide"].CheckAmount(costHide)) &&
            (Global.instance.GetResources()["Herbs"].CheckAmount(costHerbs)) &&
            (Global.instance.GetResources()["Steel"].CheckAmount(costSteel)) &&
            (Global.instance.GetResources()["Titanium"].CheckAmount(costTitanium)) &&
            (Global.instance.GetResources()["Electricity"].CheckAmount(costElectricity)) &&
            (Global.instance.GetResources()["Oil"].CheckAmount(costOil))
            )
        {
            //Debug.Log("CheckResource() = true");
            return(true);
        }
        else
        {
            //Debug.Log("CheckResource() = false");
            return(false);
        }
        
    }


    public Item()
    {
        isFavorite = false;
        isAvailable = false;
        stock = 0;
        lifetimeCrafted = 0;
        lifetimeSold = 0;
    }
}
