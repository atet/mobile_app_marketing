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

    [SerializeField] public TextMeshProUGUI uIMineIron;
    public void UpdateUIMineIron(){ uIMineIron.text = Global.instance.GetResources()["Iron"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIMineHide;
    public void UpdateUIMineHide(){ uIMineHide.text = Global.instance.GetResources()["Hide"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIMineWood;
    public void UpdateUIMineWood(){ uIMineWood.text = Global.instance.GetResources()["Wood"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIMineHerbs;
    public void UpdateUIMineHerbs(){ uIMineHerbs.text = Global.instance.GetResources()["Herbs"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIMineSteel;
    public void UpdateUIMineSteel(){ uIMineSteel.text = Global.instance.GetResources()["Steel"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIMineOil;
    public void UpdateUIMineOil(){ uIMineOil.text = Global.instance.GetResources()["Oil"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIMineElectricity;
    public void UpdateUIMineElectricity(){ uIMineElectricity.text = Global.instance.GetResources()["Electricity"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIMineTitanium;
    public void UpdateUIMineTitanium(){ uIMineTitanium.text = Global.instance.GetResources()["Titanium"].ToStringAmount(); }
    

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
