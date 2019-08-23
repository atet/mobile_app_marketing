using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    // [SerializeField] public TextMeshProUGUI level, coins, chakra, gems, iron, hide, wood, herbs, steel, oil, electricity, titanium;


    // UI Overlay: Shown on all screens.
    [SerializeField] public TextMeshProUGUI uIOverlayLevel;
    public void UpdateLevel(){ uIOverlayLevel.text = Global.instance.GetStats()["Level"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIOverlayCoins;
    public void UpdateCoins(){ uIOverlayCoins.text = Global.instance.GetStats()["Coins"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIOverlayChakra;
    public void UpdateChakra(){ uIOverlayChakra.text = Global.instance.GetStats()["Chakra"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIOverlayGems;
    public void UpdateGems(){ uIOverlayGems.text = Global.instance.GetStats()["Gems"].ToStringAmount(); }

    [SerializeField] public TextMeshProUGUI uIMineIron;
    public void UpdateIron(){ uIMineIron.text = Global.instance.GetResources()["Iron"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIMineHide;
    public void UpdateHide(){ uIMineHide.text = Global.instance.GetResources()["Hide"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIMineWood;
    public void UpdateWood(){ uIMineWood.text = Global.instance.GetResources()["Wood"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIMineHerbs;
    public void UpdateHerbs(){ uIMineHerbs.text = Global.instance.GetResources()["Herbs"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIMineSteel;
    public void UpdateSteel(){ uIMineSteel.text = Global.instance.GetResources()["Steel"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIMineOil;
    public void UpdateOil(){ uIMineOil.text = Global.instance.GetResources()["Oil"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIMineElectricity;
    public void UpdateElectricity(){ uIMineElectricity.text = Global.instance.GetResources()["Electricity"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI uIMineTitanium;
    public void UpdateTitanium(){ uIMineTitanium.text = Global.instance.GetResources()["Titanium"].ToStringAmount(); }
    
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
        UpdateLevel();
        UpdateCoins();
        UpdateChakra();
        UpdateGems();
        UpdateIron();
        UpdateHide();
        UpdateWood();
        UpdateHerbs();
        UpdateSteel();
        UpdateOil();
        UpdateElectricity();
        UpdateTitanium();
    }


}
