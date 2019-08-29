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

    [SerializeField] public TextMeshProUGUI uIMineIron, uIMineIronDetailLevel, uIMineIronDetailRate, uIMineIronDetailCap;
    public void UpdateUIMineIron()
    {
        uIMineIron.text = Global.instance.GetResources()["Iron"].ToStringAmount();
        uIMineIronDetailLevel.text = "Lvl. " + Global.instance.GetResources()["Iron"].GetLevel().ToString();
        uIMineIronDetailRate.text = System.Math.Round(60 / Global.instance.GetResources()["Iron"].GetRate(), 1).ToString() + "/min.";
        uIMineIronDetailCap.text = Global.instance.GetResources()["Iron"].ToStringAmount() + "/" + Global.instance.GetResources()["Iron"].GetCap().ToString();
    }
    [SerializeField] public TextMeshProUGUI uIMineHide, uIMineHideDetailLevel, uIMineHideDetailRate, uIMineHideDetailCap;
    public void UpdateUIMineHide()
    {
        uIMineHide.text = Global.instance.GetResources()["Hide"].ToStringAmount();
        uIMineHideDetailLevel.text = "Lvl. " + Global.instance.GetResources()["Hide"].GetLevel().ToString();
        uIMineHideDetailRate.text = System.Math.Round(60 / Global.instance.GetResources()["Hide"].GetRate(), 1).ToString() + "/min.";
        uIMineHideDetailCap.text = Global.instance.GetResources()["Hide"].ToStringAmount() + "/" + Global.instance.GetResources()["Hide"].GetCap().ToString();
    }
    [SerializeField] public TextMeshProUGUI uIMineWood, uIMineWoodDetailLevel, uIMineWoodDetailRate, uIMineWoodDetailCap;
    public void UpdateUIMineWood()
    {
        uIMineWood.text = Global.instance.GetResources()["Wood"].ToStringAmount();
        uIMineWoodDetailLevel.text = "Lvl. " + Global.instance.GetResources()["Wood"].GetLevel().ToString();
        uIMineWoodDetailRate.text = System.Math.Round(60 / Global.instance.GetResources()["Wood"].GetRate(), 1).ToString() + "/min.";
        uIMineWoodDetailCap.text = Global.instance.GetResources()["Wood"].ToStringAmount() + "/" + Global.instance.GetResources()["Wood"].GetCap().ToString();
    }
    [SerializeField] public TextMeshProUGUI uIMineHerbs, uIMineHerbsDetailLevel, uIMineHerbsDetailRate, uIMineHerbsDetailCap;
    public void UpdateUIMineHerbs()
    {
        uIMineHerbs.text = Global.instance.GetResources()["Herbs"].ToStringAmount();
        uIMineHerbsDetailLevel.text = "Lvl. " + Global.instance.GetResources()["Herbs"].GetLevel().ToString();
        uIMineHerbsDetailRate.text = System.Math.Round(60 / Global.instance.GetResources()["Herbs"].GetRate(), 1).ToString() + "/min.";
        uIMineHerbsDetailCap.text = Global.instance.GetResources()["Herbs"].ToStringAmount() + "/" + Global.instance.GetResources()["Herbs"].GetCap().ToString();
    }
    [SerializeField] public TextMeshProUGUI uIMineSteel, uIMineSteelDetailLevel, uIMineSteelDetailRate, uIMineSteelDetailCap;
    public void UpdateUIMineSteel()
    {
        uIMineSteel.text = Global.instance.GetResources()["Steel"].ToStringAmount();
        uIMineSteelDetailLevel.text = "Lvl. " + Global.instance.GetResources()["Steel"].GetLevel().ToString();
        uIMineSteelDetailRate.text = System.Math.Round(60 / Global.instance.GetResources()["Steel"].GetRate(), 1).ToString() + "/min.";
        uIMineSteelDetailCap.text = Global.instance.GetResources()["Steel"].ToStringAmount() + "/" + Global.instance.GetResources()["Steel"].GetCap().ToString();
    }
    [SerializeField] public TextMeshProUGUI uIMineOil, uIMineOilDetailLevel, uIMineOilDetailRate, uIMineOilDetailCap;
    public void UpdateUIMineOil()
    {
        uIMineOil.text = Global.instance.GetResources()["Oil"].ToStringAmount();
        uIMineOilDetailLevel.text = "Lvl. " + Global.instance.GetResources()["Oil"].GetLevel().ToString();
        uIMineOilDetailRate.text = System.Math.Round(60 / Global.instance.GetResources()["Oil"].GetRate(), 1).ToString() + "/min.";
        uIMineOilDetailCap.text = Global.instance.GetResources()["Oil"].ToStringAmount() + "/" + Global.instance.GetResources()["Oil"].GetCap().ToString();
    }
    [SerializeField] public TextMeshProUGUI uIMineElectricity, uIMineElectricityDetailLevel, uIMineElectricityDetailRate, uIMineElectricityDetailCap;
    public void UpdateUIMineElectricity()
    {
        uIMineElectricity.text = Global.instance.GetResources()["Electricity"].ToStringAmount();
        uIMineElectricityDetailLevel.text = "Lvl. " + Global.instance.GetResources()["Electricity"].GetLevel().ToString();
        uIMineElectricityDetailRate.text = System.Math.Round(60 / Global.instance.GetResources()["Electricity"].GetRate(), 1).ToString() + "/min.";
        uIMineElectricityDetailCap.text = Global.instance.GetResources()["Electricity"].ToStringAmount() + "/" + Global.instance.GetResources()["Electricity"].GetCap().ToString();
    }
    [SerializeField] public TextMeshProUGUI uIMineTitanium, uIMineTitaniumDetailLevel, uIMineTitaniumDetailRate, uIMineTitaniumDetailCap;
    public void UpdateUIMineTitanium()
    {
        uIMineTitanium.text = Global.instance.GetResources()["Titanium"].ToStringAmount();
        uIMineTitaniumDetailLevel.text = "Lvl. " + Global.instance.GetResources()["Titanium"].GetLevel().ToString();
        uIMineTitaniumDetailRate.text = System.Math.Round(60 / Global.instance.GetResources()["Titanium"].GetRate(), 1).ToString() + "/min.";
        uIMineTitaniumDetailCap.text = Global.instance.GetResources()["Titanium"].ToStringAmount() + "/" + Global.instance.GetResources()["Titanium"].GetCap().ToString();
    }
    

    [SerializeField] public TextMeshProUGUI uIShopQueue;
    public void UpdateUIShopQueue() { uIShopQueue.text = Global.instance.GetResources()["Queue"].ToStringAmount(); }


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
        UpdateUIMineIron();
        UpdateUIMineHide();
        UpdateUIMineWood();
        UpdateUIMineHerbs();
        UpdateUIMineSteel();
        UpdateUIMineOil();
        UpdateUIMineElectricity();
        UpdateUIMineTitanium();

        UpdateUIShopQueue();
    }


}
