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
    public ulong costResource1, costResource2, costResource3, costResource4, costResource5, costResource6, costResource7, costResource8;
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
    public void RemoveStock(int stock){ this.stock -= stock; Global.instance.GetStats()["Stock"].RemoveAmount(System.Convert.ToUInt64(stock)); }
    
    public int lifetimeCrafted;

    public void AcquireResources()
    {
        // resources
        // 1 iron
        // 2 wood
        // 3 hide
        // 4 herbs
        // 5 steel
        // 6 titanium
        // 7 electricity
        // 8 oil
        //Debug.Log("Acquiring resources for " + name + ".");
        Global.instance.GetResources()["resource_1"].RemoveAmount(costResource1);
        Global.instance.GetResources()["resource_2"].RemoveAmount(costResource2);
        Global.instance.GetResources()["resource_3"].RemoveAmount(costResource3);
        Global.instance.GetResources()["resource_4"].RemoveAmount(costResource4);
        Global.instance.GetResources()["resource_5"].RemoveAmount(costResource5);
        Global.instance.GetResources()["resource_6"].RemoveAmount(costResource6);
        Global.instance.GetResources()["resource_7"].RemoveAmount(costResource7);
        Global.instance.GetResources()["resource_8"].RemoveAmount(costResource8);
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
            (Global.instance.GetResources()["resource_1"].CheckAmount(costResource1)) &&
            (Global.instance.GetResources()["resource_2"].CheckAmount(costResource2)) &&
            (Global.instance.GetResources()["resource_3"].CheckAmount(costResource3)) &&
            (Global.instance.GetResources()["resource_4"].CheckAmount(costResource4)) &&
            (Global.instance.GetResources()["resource_5"].CheckAmount(costResource5)) &&
            (Global.instance.GetResources()["resource_6"].CheckAmount(costResource6)) &&
            (Global.instance.GetResources()["resource_7"].CheckAmount(costResource7)) &&
            (Global.instance.GetResources()["resource_8"].CheckAmount(costResource8))
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
