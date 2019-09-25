using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Item
{
    public string id, name, category1, category2, filepathImage;
    public string prerequsiteUnlock;
    public ulong prerequsiteUnlockCostResearchScrolls;
    public ulong tier, value, timeCrafting, xPMerchant, xPWorker;
    public string prerequisiteWorker1;
    public ulong prerequisiteWorker1Level;
    public string prerequisiteWorker2;
    public ulong prerequisiteWorker2Level;
    public ulong costResource1, costResource2, costResource3, costResource4, costResource5, costResource6, costResource7, costResource8;
    // public string costComponent1Name, costComponent1Quality; 
    // public ulong costComponent1; 
    // public string costComponent2Name, costComponent2Quality; 
    // public ulong costComponent2; 
    public ulong costComponent1, costComponent2, costComponent3, costComponent4, costComponent5, costComponent6, costComponent7, costComponent8, costComponent9, costComponent10, costComponent11, costComponent12, costComponent13, costComponent14, costComponent15, costComponent16;
    public ulong costAmulet_1, costAxe_2, costBoots_3, costBow_2, costClothing_2, costCrossbow_3, costDagger_2, costDagger_3, costGauntlets_2, costHarmor_1, costHarmor_2, costHarmor_3, costHeadgear_1, costHelmet_1, costHelmet_2, costMace_1, costMace_4, costMagic_1, costMagic_3, costMedicine_1, costRing_1, costRing_3, costRunestone_1, costRunestone_2, costShoes_2, costSpear_2, costStaff_2, costSword_1;
    public ulong statATK, statDEF, statHP; 
    public string upgradeCrafting1, upgradeCrafting1Key, upgradeCrafting1Value;
    public int upgradeCrafting1Count; 
    public string upgradeCrafting2, upgradeCrafting2Key, upgradeCrafting2Value;
    public int upgradeCrafting2Count; 
    public string upgradeCrafting3, upgradeCrafting3Key, upgradeCrafting3Value; 
    public int upgradeCrafting3Count; 
    public string upgradeCrafting4, upgradeCrafting4Key, upgradeCrafting4Value; 
    public int upgradeCrafting4Count; 
    public string upgradeCrafting5, upgradeCrafting5Key, upgradeCrafting5Value;
    public int upgradeCrafting5Count; 
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
        Global.instance.GetResources()["component_1"].RemoveAmount(costComponent1);
        Global.instance.GetResources()["component_2"].RemoveAmount(costComponent2);
        Global.instance.GetResources()["component_3"].RemoveAmount(costComponent3);
        Global.instance.GetResources()["component_4"].RemoveAmount(costComponent4);
        Global.instance.GetResources()["component_5"].RemoveAmount(costComponent5);
        Global.instance.GetResources()["component_6"].RemoveAmount(costComponent6);
        Global.instance.GetResources()["component_7"].RemoveAmount(costComponent7);
        Global.instance.GetResources()["component_8"].RemoveAmount(costComponent8);
        Global.instance.GetResources()["component_9"].RemoveAmount(costComponent9);
        Global.instance.GetResources()["component_10"].RemoveAmount(costComponent10);
        Global.instance.GetResources()["component_11"].RemoveAmount(costComponent11);
        Global.instance.GetResources()["component_12"].RemoveAmount(costComponent12);
        Global.instance.GetResources()["component_13"].RemoveAmount(costComponent13);
        Global.instance.GetResources()["component_14"].RemoveAmount(costComponent14);
        Global.instance.GetResources()["component_15"].RemoveAmount(costComponent15);
        Global.instance.GetResources()["component_16"].RemoveAmount(costComponent16);
    }
    public void CraftItem()
    {
        Debug.Log(name + " has been crafted.");
        stock += 1; lifetimeCrafted += 1;
        // Now increment Global stock
        Global.instance.GetStats()["Stock"].IncrementAmount();

        // This occurs when you confirm the item crafted by clicking "Finished"
        // if(lifetimeCrafted == 5)
        // {
        //     Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Unlocked!", "You're pretty good at " + name + "crafting!\n\nYou've unlocked the <ITEM_NAME>.", "Images/Items/null");
        // }

        if(lifetimeCrafted == upgradeCrafting1Count)
        {
            UpgradeCrafting(upgradeCrafting1, upgradeCrafting1Key, upgradeCrafting1Value);
        }
        else if(lifetimeCrafted == upgradeCrafting2Count)
        {
            UpgradeCrafting(upgradeCrafting2, upgradeCrafting2Key, upgradeCrafting2Value);
        }
        else if(lifetimeCrafted == upgradeCrafting3Count)
        {
            UpgradeCrafting(upgradeCrafting3, upgradeCrafting3Key, upgradeCrafting3Value);
        }
        else if(lifetimeCrafted == upgradeCrafting4Count)
        {
            UpgradeCrafting(upgradeCrafting4, upgradeCrafting4Key, upgradeCrafting4Value);
        }
        else if(lifetimeCrafted == upgradeCrafting5Count)
        {
            UpgradeCrafting(upgradeCrafting5, upgradeCrafting5Key, upgradeCrafting5Value);
        }

    }
    public void UpgradeCrafting(string upgradeCrafting, string upgradeCraftingKey, string upgradeCraftingValue)
    {
        // upgradeCrafting is the text of what's going on
        //Tutorial.instance.SummonUIOverlayTextBoxImageSmall("TEST", name + "\n\n" + upgradeCrafting, "Images/Items/null");
    
        SFX.instance.PlaySFXLevelUp();

        switch(upgradeCraftingKey)
        {
            case "Unlock":
                // Tested
                Global.instance.GetInventory()[upgradeCraftingValue].SetIsAvailable(true);
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Unlock", Global.instance.GetInventory()[upgradeCraftingValue].name + " is now unlocked!", Global.instance.GetInventory()[upgradeCraftingValue].filepathImage);
                break;
            case "Time":
                // Tested
                timeCrafting = (ulong)Mathf.RoundToInt((float)timeCrafting * float.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCrafting + "\n\n" + name + " now takes " + Helper.TimeFormatter(timeCrafting) + " to craft!", "Images/UI/icon_time");
                break;
            case "Value":
                // Tested
                value = (ulong)Mathf.RoundToInt((float)value * float.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCrafting + "\n\n" + name + " now sells for " + value.ToString("N0") + " coins!", "Images/UI/coins");
                break;
            case "Quality":
                // Tested
                // Item quality is not implemented yet
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCrafting, "Images/UI/icon_craft");
                break;
            case "resource_1":
                // Tested
                costResource1 = (ulong)((int)costResource1 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "resource_2":
                //
                costResource2 = (ulong)((int)costResource2 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "resource_3":
                //
                costResource3 = (ulong)((int)costResource3 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "resource_4":
                //
                costResource4 = (ulong)((int)costResource4 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "resource_5":
                //
                costResource5 = (ulong)((int)costResource5 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "resource_6":
                //
                costResource6 = (ulong)((int)costResource6 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "resource_7":
                //
                costResource7 = (ulong)((int)costResource7 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "resource_8":
                //
                costResource8 = (ulong)((int)costResource8 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "component_1":
                //
                costComponent1 = (ulong)((int)costComponent1 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "component_2":
                //
                costComponent2 = (ulong)((int)costComponent2 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "component_4":
                //
                costComponent4 = (ulong)((int)costComponent4 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "component_5":
                //
                costComponent5 = (ulong)((int)costComponent5 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "component_6":
                //
                costComponent6 = (ulong)((int)costComponent6 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "component_7":
                //
                costComponent7 = (ulong)((int)costComponent7 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "component_8":
                //
                costComponent8 = (ulong)((int)costComponent8 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "component_9":
                //
                costComponent9 = (ulong)((int)costComponent9 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "component_10":
                //
                costComponent10 = (ulong)((int)costComponent10 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "component_11":
                //
                costComponent11 = (ulong)((int)costComponent11 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "component_12":
                //
                costComponent12 = (ulong)((int)costComponent12 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "component_13":
                //
                costComponent13 = (ulong)((int)costComponent13 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "component_14":
                //
                costComponent14 = (ulong)((int)costComponent14 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            case "component_15":
                //
                costComponent15 = (ulong)((int)costComponent15 + int.Parse(upgradeCraftingValue));
                Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Upgrade", upgradeCraftingValue + " of " + Global.instance.GetResources()[upgradeCraftingKey].GetLabel() + " required for crafting!", Global.instance.GetResources()[upgradeCraftingKey].GetFilepathImage());
                break;
            default:
                break;

        }
    
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
            (Global.instance.GetResources()["resource_8"].CheckAmount(costResource8)) &&
            (Global.instance.GetResources()["component_1"].CheckAmount(costComponent1)) &&
            (Global.instance.GetResources()["component_2"].CheckAmount(costComponent2)) &&
            (Global.instance.GetResources()["component_3"].CheckAmount(costComponent3)) &&
            (Global.instance.GetResources()["component_4"].CheckAmount(costComponent4)) &&
            (Global.instance.GetResources()["component_5"].CheckAmount(costComponent5)) &&
            (Global.instance.GetResources()["component_6"].CheckAmount(costComponent6)) &&
            (Global.instance.GetResources()["component_7"].CheckAmount(costComponent7)) &&
            (Global.instance.GetResources()["component_8"].CheckAmount(costComponent8)) &&
            (Global.instance.GetResources()["component_9"].CheckAmount(costComponent9)) &&
            (Global.instance.GetResources()["component_10"].CheckAmount(costComponent10)) &&
            (Global.instance.GetResources()["component_11"].CheckAmount(costComponent11)) &&
            (Global.instance.GetResources()["component_12"].CheckAmount(costComponent12)) &&
            (Global.instance.GetResources()["component_13"].CheckAmount(costComponent13)) &&
            (Global.instance.GetResources()["component_14"].CheckAmount(costComponent14)) &&
            (Global.instance.GetResources()["component_15"].CheckAmount(costComponent15)) &&
            (Global.instance.GetResources()["component_16"].CheckAmount(costComponent16))
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
