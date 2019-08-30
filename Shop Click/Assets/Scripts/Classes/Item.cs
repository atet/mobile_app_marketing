using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Item
{
    public string id, name, category, filepathImage;
    public string prerequsiteUnlock;
    public ulong prerequsiteUnlockCostResearchScrolls;
    public ulong tier, value, timeCrafting, xPMerchant, xPWorker;
    public string prerequisiteWorker1;
    public ulong prerequisiteWorker1Level;
    public string prerequisiteWorker2;
    public ulong prerequisiteWorker2Level;
    public ulong costIron, costWood, costHide, costHerbs, costSteel, costTitanium, costElectricity, costOil;
    public string costComponent1Name, costComponent1Quality; 
    public ulong costComponent1; 
    public string costComponent2Name, costComponent2Quality; 
    public ulong costComponent2; 
    public ulong statATK, statDEF, statHP; 
    public string upgradeCrafting1, upgradeCrafting1Key, upgradeCrafting1Value;
    public string upgradeCrafting1Count; 
    public string upgradeCrafting2, upgradeCrafting2Key, upgradeCrafting2Value;
    public string upgradeCrafting2Count; 
    public string upgradeCrafting3, upgradeCrafting3Key, upgradeCrafting3Value; 
    public string upgradeCrafting3Count; 
    public string upgradeCrafting4, upgradeCrafting4Key, upgradeCrafting4Value; 
    public string upgradeCrafting4Count; 
    public string upgradeCrafting5, upgradeCrafting5Key, upgradeCrafting5Value;
    public string upgradeCrafting5Count; 
    public string upgradeAscension1, upgradeAscension1Key, upgradeAscension1Value; 
    public string upgradeAscension1Cost; 
    public string upgradeAscension2, upgradeAscension2Key, upgradeAscension2Value; 
    public string upgradeAscension2Cost; 
    public string upgradeAscension3, upgradeAscension3Key, upgradeAscension3Value; 
    public string upgradeAscension3Cost; 
    public ulong energyDiscount, energySurcharge, energySuggest, energySpeedUp;

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
