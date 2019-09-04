using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    // [SerializeField] public TextMeshProUGUI level, coins, chakra, gems, iron, hide, wood, herbs, steel, oil, electricity, titanium;

    // UI Overlay: Shown on all screens.
    [SerializeField] public TextMeshProUGUI uIOverlayLevel;
    public void UpdateUIOverlayLevel(){ uIOverlayLevel.text = Global.instance.GetStats()["Level"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIOverlayCoins;
    public void UpdateUIOverlayCoins(){ uIOverlayCoins.text = Global.instance.GetStats()["Coins"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIOverlayChakra;
    public void UpdateUIOverlayChakra(){ uIOverlayChakra.text = Global.instance.GetStats()["Chakra"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIOverlayGems;
    public void UpdateUIOverlayGems(){ uIOverlayGems.text = Global.instance.GetStats()["Gems"].ToStringAmount(); }

    [SerializeField] public TextMeshProUGUI uIShopQueue;
    public void UpdateUIShopQueue() { uIShopQueue.text = Global.instance.GetResources()["Queue"].ToStringAmount(); }


    [SerializeField] public TextMeshProUGUI uIMineIron, uIMineIronDetailLevel, uIMineIronDetailRate, uIMineIronDetailCap;
    [SerializeField] public TextMeshProUGUI uITownIronDetailLevel, uITownIronDetailRate;
    [SerializeField] public TextMeshProUGUI uIMineHide, uIMineHideDetailLevel, uIMineHideDetailRate, uIMineHideDetailCap;
    [SerializeField] public TextMeshProUGUI uITownHideDetailLevel, uITownHideDetailRate;
    [SerializeField] public TextMeshProUGUI uIMineWood, uIMineWoodDetailLevel, uIMineWoodDetailRate, uIMineWoodDetailCap;
    [SerializeField] public TextMeshProUGUI uITownWoodDetailLevel, uITownWoodDetailRate;
    [SerializeField] public TextMeshProUGUI uIMineHerbs, uIMineHerbsDetailLevel, uIMineHerbsDetailRate, uIMineHerbsDetailCap;
    [SerializeField] public TextMeshProUGUI uITownHerbsDetailLevel, uITownHerbsDetailRate;
    [SerializeField] public TextMeshProUGUI uIMineSteel, uIMineSteelDetailLevel, uIMineSteelDetailRate, uIMineSteelDetailCap;
    [SerializeField] public TextMeshProUGUI uITownSteelDetailLevel, uITownSteelDetailRate;
    [SerializeField] public TextMeshProUGUI uIMineOil, uIMineOilDetailLevel, uIMineOilDetailRate, uIMineOilDetailCap;
    [SerializeField] public TextMeshProUGUI uITownOilDetailLevel, uITownOilDetailRate;
    [SerializeField] public TextMeshProUGUI uIMineElectricity, uIMineElectricityDetailLevel, uIMineElectricityDetailRate, uIMineElectricityDetailCap;
    [SerializeField] public TextMeshProUGUI uITownElectricityDetailLevel, uITownElectricityDetailRate;
    [SerializeField] public TextMeshProUGUI uIMineTitanium, uIMineTitaniumDetailLevel, uIMineTitaniumDetailRate, uIMineTitaniumDetailCap;
    [SerializeField] public TextMeshProUGUI uITownTitaniumDetailLevel, uITownTitaniumDetailRate;

    public void UpdateUIResource(
        string resource,
        TextMeshProUGUI uIMineAmount,
        TextMeshProUGUI uIMineLevel,
        TextMeshProUGUI uIMineRate,
        TextMeshProUGUI uIMineCap,
        TextMeshProUGUI uITownLevel,
        TextMeshProUGUI uITownRate
    )
    {
        uIMineAmount.text = Global.instance.GetResources()[resource].ToStringAmount();
        uIMineLevel.text = "Lvl. " + Global.instance.GetResources()[resource].GetLevel().ToString();
        uIMineRate.text = Global.instance.GetResources()[resource].ToStringRate();
        uIMineCap.text = Global.instance.GetResources()[resource].ToStringAmount() + "/" + Global.instance.GetResources()[resource].GetCap().ToString();

        uITownLevel.text = "Level " + Global.instance.GetResources()[resource].GetLevel().ToString();
        uITownRate.text = Global.instance.GetResources()[resource].ToStringRate();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTMP();
    }


    public void UpdateTMP()
    {
        UpdateUIOverlayLevel();
        UpdateUIOverlayCoins();
        UpdateUIOverlayChakra();
        UpdateUIOverlayGems();

        UpdateUIResource("Iron", uIMineIron, uIMineIronDetailLevel, uIMineIronDetailRate, uIMineIronDetailCap, uITownIronDetailLevel, uITownIronDetailRate);
        UpdateUIResource("Hide", uIMineHide, uIMineHideDetailLevel, uIMineHideDetailRate, uIMineHideDetailCap, uITownHideDetailLevel, uITownHideDetailRate);
        UpdateUIResource("Wood", uIMineWood, uIMineWoodDetailLevel, uIMineWoodDetailRate, uIMineWoodDetailCap, uITownWoodDetailLevel, uITownWoodDetailRate);
        UpdateUIResource("Herbs", uIMineHerbs, uIMineHerbsDetailLevel, uIMineHerbsDetailRate, uIMineHerbsDetailCap, uITownHerbsDetailLevel, uITownHerbsDetailRate);
        UpdateUIResource("Steel", uIMineSteel, uIMineSteelDetailLevel, uIMineSteelDetailRate, uIMineSteelDetailCap, uITownSteelDetailLevel, uITownSteelDetailRate);
        UpdateUIResource("Oil", uIMineOil, uIMineOilDetailLevel, uIMineOilDetailRate, uIMineOilDetailCap, uITownOilDetailLevel, uITownOilDetailRate);
        UpdateUIResource("Electricity", uIMineElectricity, uIMineElectricityDetailLevel, uIMineElectricityDetailRate, uIMineElectricityDetailCap, uITownElectricityDetailLevel, uITownElectricityDetailRate);
        UpdateUIResource("Titanium", uIMineTitanium, uIMineTitaniumDetailLevel, uIMineTitaniumDetailRate, uIMineTitaniumDetailCap, uITownTitaniumDetailLevel, uITownTitaniumDetailRate);

        UpdateUIShopQueue();
    }


}
