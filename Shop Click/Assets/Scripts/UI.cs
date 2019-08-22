using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    // [SerializeField] public TextMeshProUGUI level, coins, chakra, gems, iron, hide, wood, herbs, steel, oil, electricity, titanium;


    [SerializeField] public TextMeshProUGUI level;
    public void UpdateLevel(){ level.text = Global.instance.GetStats()["Level"].ToStringAmount(); }



    [SerializeField] public TextMeshProUGUI coins;
    public void UpdateCoins(){ coins.text = Global.instance.GetStats()["Coins"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI chakra;
    public void UpdateChakra(){ chakra.text = Global.instance.GetStats()["Chakra"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI gems;
    public void UpdateGems(){ gems.text = Global.instance.GetStats()["Gems"].ToStringAmount(); }

    [SerializeField] public TextMeshProUGUI iron;
    public void UpdateIron(){ iron.text = Global.instance.GetResources()["Iron"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI hide;
    public void UpdateHide(){ hide.text = Global.instance.GetResources()["Hide"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI wood;
    public void UpdateWood(){ wood.text = Global.instance.GetResources()["Wood"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI herbs;
    public void UpdateHerbs(){ herbs.text = Global.instance.GetResources()["Herbs"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI steel;
    public void UpdateSteel(){ steel.text = Global.instance.GetResources()["Steel"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI oil;
    public void UpdateOil(){ oil.text = Global.instance.GetResources()["Oil"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI electricity;
    public void UpdateElectricity(){ electricity.text = Global.instance.GetResources()["Electricity"].ToStringAmount(); }
    [SerializeField] public TextMeshProUGUI titanium;
    public void UpdateTitanium(){ titanium.text = Global.instance.GetResources()["Titanium"].ToStringAmount(); }
    
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
