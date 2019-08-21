using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Item
{
    public string name;
    public string category;
    public string filepathImage;
    public string prerequsiteUnlock;
    public string prerequsiteUnlockCostResearchScrolls;
    public string tier;
    public int value;
    public string timeCrafting;
    public string xPMerchant;
    public string xPWorker;
    public string prerequisiteWorker1;
    public string prerequisiteWorker1Level;
    public string prerequisiteWorker2;
    public string prerequisiteWorker2Level;
    public string costIron;
    public string costWood;
    public string costHide;
    public string costHerbs;
    public string costSteel;
    public string costTitanium; 
    public string costElectricity;
    public string costOil;
    public string costComponent1Name; 
    public string costComponent1Quality; 
    public string costComponent1; 
    public string costComponent2Name; 
    public string costComponent2Quality; 
    public string costComponent2; 
    public string statATK; 
    public string statDEF; 
    public string statHP; 
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
    public string energyDiscount;
    public string energySurcharge; 
    public string energySuggest;
    public string energySpeedUp;

    private int stock; public int GetStock(){ return(stock); }

    public Item()
    {
        stock = 0;
    }
}
