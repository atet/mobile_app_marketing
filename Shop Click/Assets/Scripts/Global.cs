using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global instance;
    private bool MODE_TUTORIAL = true; public bool GetMODE_TUTORIAL(){ return(MODE_TUTORIAL); } public void SetMODE_TUTORIAL(bool MODE_TUTORIAL){ this.MODE_TUTORIAL = MODE_TUTORIAL; }
    private int ID_TUTORIAL_EVENT = 0; public int GetID_TUTORIAL_EVENT(){ return(ID_TUTORIAL_EVENT); } public void IncrementID_TUTORIAL_EVENT(){ ID_TUTORIAL_EVENT++; }
    private const bool MODE_DEBUG = true; public bool GetMODE_DEBUG(){ return(MODE_DEBUG); }
    // 1 is original values, 2 would be a 2x speedup (each resource takes 50% less time to get, item crafting duration is 50%).
    private const float globalMultiplier = 20; public float GetGlobalMultiplier(){ return(globalMultiplier); }

    private const string filepathInventoryJSON = "Data/shop_click_values_20190827";
    private const string filepathCharactersJSON = "Data/shop_click_characters";

    public Random rnd = new Random();

    Dictionary<string, Resource> stats; public Dictionary<string, Resource> GetStats(){ return(stats); }
    Dictionary<string, Resource> resources; public Dictionary<string, Resource> GetResources(){ return(resources); }
    Dictionary<string, Item> inventory; public Dictionary<string, Item> GetInventory(){ return(inventory); }
    Dictionary<string, Character> characters; public Dictionary<string, Character> GetCharacters(){ return(characters); }

    void Awake()
    {
        instance = this;
        // TODO: Load saved game state;
        InitStats();
        InitResources();
        InitCharacters(); // This has to be run before inventory?
        InitInventory();
        InitInventoryStarting();





        
    }


    void Start()
    {
        //Debug.Log(Helper.TimeFormatter(101));

        
    }

    void Update()
    {
        CheckTimeRemaining();
    }


    public void InitStats()
    {
        stats = new Dictionary<string, Resource>();
        if(MODE_DEBUG)
        {
            stats.Add("Level",  new Resource("Level",  1, 0, System.UInt64.MaxValue,   1));
            stats.Add("Coins",  new Resource("Coins",  1, 0, System.UInt64.MaxValue,   0));
            stats.Add("Chakra", new Resource("Chakra", 1, 0,                    999, 999));
            stats.Add("Gems",   new Resource("Gems",   1, 0, System.UInt64.MaxValue,   5));
            stats.Add("Stock",  new Resource("Stock",  1, 0,                    999,   0));
        }
        else
        {
            stats.Add("Level",  new Resource("Level",  1, 0, System.UInt64.MaxValue,  1));
            stats.Add("Coins",  new Resource("Coins",  1, 0, System.UInt64.MaxValue,  0));
            stats.Add("Chakra", new Resource("Chakra", 1, 0,                    100, 18));
            stats.Add("Gems",   new Resource("Gems",   1, 0, System.UInt64.MaxValue,  5));
            stats.Add("Stock",  new Resource("Stock",  1, 0,                     15,  0));
        }

    }

    public void InitResources()
    {
        resources = new Dictionary<string, Resource>();

        if(MODE_DEBUG)
        {
            // Customers coming in the store.
            resources.Add("Queue",       new Resource("Queue",       1,  1, 999, 900));

            resources.Add("Iron",        new Resource("Iron",        1,  1, 999, 900));
            resources.Add("Wood",        new Resource("Wood",        1,  1, 999, 900));
            resources.Add("Hide",        new Resource("Hide",        1,  1, 999, 900));
            resources.Add("Herbs",       new Resource("Herbs",       1,  1, 999, 900));
            resources.Add("Steel",       new Resource("Steel",       1,  1, 999, 900));
            resources.Add("Oil",         new Resource("Oil",         1,  1, 999, 900));
            resources.Add("Electricity", new Resource("Electricity", 1,  1, 999, 900));
            resources.Add("Titanium",    new Resource("Titanium",    1,  1, 999, 900));
        }
        else
        {
            // Customers coming in the store.
            resources.Add("Queue",       new Resource("Queue",       1, 10,  5, 10));

            resources.Add("Iron",        new Resource("Iron",        1, 10, 35,  5));
            resources.Add("Wood",        new Resource("Wood",        1, 10, 35,  0));
            resources.Add("Hide",        new Resource("Hide",        1, 10, 35,  0));
            resources.Add("Herbs",       new Resource("Herbs",       1, 10, 35,  0));
            resources.Add("Steel",       new Resource("Steel",       1, 45, 15,  0));
            resources.Add("Oil",         new Resource("Oil",         1, 45, 15,  0));
            resources.Add("Electricity", new Resource("Electricity", 1, 45, 15,  0));
            resources.Add("Titanium",    new Resource("Titanium",    1, 45, 15,  0));
        }


    }

    public void InitCharacters()
    {
        //Debug.Log("InitCharacters()");
        // Read in JSON data
        TextAsset jsonTextFile = Resources.Load<TextAsset>(filepathCharactersJSON);
        Character[] items = Helper.FromJson<Character>(jsonTextFile.ToString());
        // Debug.Log("JSON inventory read, contains " + items.Length.ToString() + " items.");

        // Populate dictionary with JSON values
        characters = new Dictionary<string, Character>();

        for (int i = 0; i < items.Length; i++)
        {
            characters.Add(items[i].name, items[i]);
            //Debug.Log(items[i].name);
        }
    }
    public Character RandomCharacter()
    {
        // Returns a random character from the characters.
        List<string> keyList = new List<string>(characters.Keys);
        string randomCharacter = keyList[Random.Range(0, characters.Count)];
        //Debug.Log("CHECK: " + randomCharacter);
        return(characters[randomCharacter]);
    }

    public void InitInventory()
    {
        // Read in JSON data
        TextAsset jsonTextFile = Resources.Load<TextAsset>(filepathInventoryJSON);
        Item[] items = Helper.FromJson<Item>(jsonTextFile.ToString());
        // Debug.Log("JSON inventory read, contains " + items.Length.ToString() + " items.");

        // Populate dictionary with JSON values
        inventory = new Dictionary<string, Item>();

        for (int i = 0; i < items.Length; i++)
        {
            inventory.Add(items[i].name, items[i]);
            inventory[items[i].name].timeCrafting = (ulong)(inventory[items[i].name].timeCrafting * (1 / Global.instance.GetGlobalMultiplier()));
            // Debug.Log(items[i].name);
        }
    }
    public void InitInventoryStarting()
    {
        if(MODE_DEBUG){
            // Everything is available to craft and only takes 10 seconds.
            foreach (var kvp in inventory)
            {
                kvp.Value.SetIsAvailable(true);
                kvp.Value.timeCrafting = 2;
            }

            // FOr the tutorial
            inventory["Javelin"].SetStock(1);
            inventory["Long Bow"].SetStock(1);
            inventory["Leather Armor"].SetStock(1);
        }
        else
        {
            inventory["Wood Axe"].SetIsAvailable(true); inventory["Wood Axe"].SetStock(0);
            inventory["Dirk"].SetIsAvailable(true); inventory["Dirk"].SetStock(0);
            inventory["Javelin"].SetIsAvailable(true); inventory["Javelin"].SetStock(1);
            inventory["Long Bow"].SetIsAvailable(true); inventory["Long Bow"].SetStock(1);
            inventory["Breastplate"].SetIsAvailable(true); inventory["Breastplate"].SetStock(0);
            inventory["Leather Armor"].SetIsAvailable(true); inventory["Leather Armor"].SetStock(1);
            inventory["Wooden Dome"].SetIsAvailable(true); inventory["Wooden Dome"].SetStock(0);
            inventory["Leather Cap"].SetIsAvailable(true); inventory["Leather Cap"].SetStock(0);
            inventory["Stitched Cone"].SetIsAvailable(true); inventory["Stitched Cone"].SetStock(0);
            inventory["Iron Armguards"].SetIsAvailable(true); inventory["Iron Armguards"].SetStock(0);
            inventory["Shin Guards"].SetIsAvailable(true); inventory["Shin Guards"].SetStock(0);
            inventory["Escutcheon"].SetIsAvailable(true); inventory["Escutcheon"].SetStock(0);
            inventory["Warm Tea"].SetIsAvailable(true); inventory["Warm Tea"].SetStock(0);
        }
    }

    public Item RandomItem()
    {
        // Returns a random item from the inventory.
        // Will make this more elaborate later (only return things in stock or mixed, etc.).

        List<string> keyList = new List<string>();

        // Iterate through dictionary
        foreach(KeyValuePair<string, Item> entry in inventory)
        {
            if(entry.Value.GetIsAvailable()){
                keyList.Add(entry.Key);
            }
        }

        int randomInt = Random.Range(0, keyList.Count);
        //Debug.Log("Random Int: " + randomInt);
        string randomKey = keyList[randomInt];

        return(inventory[randomKey]);

        // // Returns a random item from the inventory.
        // // https://stackoverflow.com/a/1028238
        // List<string> keyList = new List<string>(inventory.Keys);

        // int randomInt = Random.Range(0, inventory.Count);
        // //Debug.Log("Random Int: " + randomInt);
        // string randomKey = keyList[randomInt];

        // return(inventory[randomKey]);
    }

    public void CheckTimeRemaining()
    {
        // Iterate through dictionary
        foreach(KeyValuePair<string, Resource> entry in resources)
        {
            entry.Value.CheckTimeRemaining();
        }
    }

    public List<Item> CheckItemsAvailable(string itemCategory)
    {
        // Returns a vector of all items available for a specific item class
        List<Item> itemsAvailable = new List<Item>();

        foreach(KeyValuePair<string, Item> entry in inventory)
        {
            if((entry.Value.category == itemCategory) && (entry.Value.GetIsAvailable())){
                //Debug.Log("Available " + itemCategory + ": " + entry.Value.name);
                itemsAvailable.Add(entry.Value);
            }
        }
        return(itemsAvailable);
    }

}


