using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI level;
    public void UpdateLevel(){ level.text = Global.instance.GetLevel().GetAmount().ToString("N0"); }
    [SerializeField] public TextMeshProUGUI coins;
    public void UpdateCoins(){ coins.text = Global.instance.GetCoins().GetAmount().ToString("N0"); }
    [SerializeField] public TextMeshProUGUI chakra;
    public void UpdateChakra(){ chakra.text = Global.instance.GetChakra().GetAmount().ToString("N0") + "%"; }
    [SerializeField] public TextMeshProUGUI gems;
    public void UpdateGems(){ gems.text = Global.instance.GetGems().GetAmount().ToString("N0"); }

    [SerializeField] public TextMeshProUGUI iron;
    public void UpdateIron(){ iron.text = Global.instance.GetIron().GetAmount().ToString("N0"); }
    [SerializeField] public TextMeshProUGUI hide;
    public void UpdateHide(){ hide.text = Global.instance.GetHide().GetAmount().ToString("N0"); }
    [SerializeField] public TextMeshProUGUI wood;
    public void UpdateWood(){ wood.text = Global.instance.GetWood().GetAmount().ToString("N0"); }
    [SerializeField] public TextMeshProUGUI herbs;
    public void UpdateHerbs(){ herbs.text = Global.instance.GetHerbs().GetAmount().ToString("N0"); }
    [SerializeField] public TextMeshProUGUI steel;
    public void UpdateSteel(){ steel.text = Global.instance.GetSteel().GetAmount().ToString("N0"); }
    [SerializeField] public TextMeshProUGUI oil;
    public void UpdateOil(){ oil.text = Global.instance.GetOil().GetAmount().ToString("N0"); }
    [SerializeField] public TextMeshProUGUI electricity;
    public void UpdateElectricity(){ electricity.text = Global.instance.GetElectricity().GetAmount().ToString("N0"); }
    [SerializeField] public TextMeshProUGUI titanium;
    public void UpdateTitanium(){ titanium.text = Global.instance.GetTitanium().GetAmount().ToString("N0"); }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
