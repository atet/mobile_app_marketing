using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global instance;

    Dictionary<string, Resource> stats; public Dictionary<string, Resource> GetStats(){ return(stats); }
    Dictionary<string, Resource> resources; public Dictionary<string, Resource> GetResources(){ return(resources); }

    void Awake()
    {
        instance = this;
        // TODO: Load saved game state;
        InitStats();
        InitResources();
    }


    void Start()
    {

    }

    public void InitStats()
    {
        stats = new Dictionary<string, Resource>();

        stats.Add("Level", new Resource("Level", 1, 0, System.UInt64.MaxValue, 1));
        stats.Add("Coins", new Resource("Coins", 1, 0, System.UInt64.MaxValue, 0));
        stats.Add("Chakra", new Resource("Chakra", 1, 0, 100, 39));
        stats.Add("Gems", new Resource("Gems", 1, 0, System.UInt64.MaxValue, 5));
    }

    public void InitResources()
    {
        resources = new Dictionary<string, Resource>();

        resources.Add("Iron",        new Resource("Iron", 1, 1, 20, 10));
        resources.Add("Hide",        new Resource("Hide", 1, 1,  0,  0));
        resources.Add("Wood",        new Resource("Wood", 1, 1,  0,  0));
        resources.Add("Herbs",       new Resource("Herbs", 1, 1,  0,  0));
        resources.Add("Steel",       new Resource("Steel", 1, 1,  0,  0));
        resources.Add("Oil",         new Resource("Oil", 1, 1,  0,  0));
        resources.Add("Electricity", new Resource("Electricity", 1, 1,  0,  0));
        resources.Add("Titanium",    new Resource("Titanium", 1, 1,  0,  0));
    }


}


