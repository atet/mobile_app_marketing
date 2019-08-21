using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global instance;

    private Resource level; public Resource GetLevel(){ return(level); }
    private Resource coins; public Resource GetCoins(){ return(coins); }
    private Resource chakra; public Resource GetChakra(){ return(chakra); }
    private Resource gems; public Resource GetGems(){ return(gems); }

    private Resource iron; public Resource GetIron(){ return(iron); }
    private Resource hide; public Resource GetHide(){ return(hide); }
    private Resource wood; public Resource GetWood(){ return(wood); }
    private Resource herbs; public Resource GetHerbs(){ return(herbs); }
    private Resource steel; public Resource GetSteel(){ return(steel); }
    private Resource oil; public Resource GetOil(){ return(oil); }
    private Resource electricity; public Resource GetElectricity(){ return(electricity); }
    private Resource titanium; public Resource GetTitanium(){ return(titanium); }

    void Start()
    {
        instance = this;

        level = new Resource("Level", 1, 0, System.UInt64.MaxValue, 1);
        coins = new Resource("Coins", 1, 0, System.UInt64.MaxValue, 0);
        chakra = new Resource("Chakra", 1, 0, 100, 39);
        gems = new Resource("Gems", 1, 0, System.UInt64.MaxValue, 5);
        
        iron = new Resource("Iron", 1, 1, 20, 10);
        hide = new Resource("Hide", 1, 1,  0,  0);
        wood = new Resource("Wood", 1, 1,  0,  0);
        herbs = new Resource("Herbs", 1, 1,  0,  0);
        steel = new Resource("Steel", 1, 1,  0,  0);
        oil = new Resource("Oil", 1, 1,  0,  0);
        electricity = new Resource("Electricity", 1, 1,  0,  0);
        titanium = new Resource("Titanium", 1, 1,  0,  0);
    }

}


