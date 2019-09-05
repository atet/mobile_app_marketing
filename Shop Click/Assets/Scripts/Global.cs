﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global instance;
    private bool MODE_TUTORIAL = true; public bool GetMODE_TUTORIAL(){ return(MODE_TUTORIAL); } public void SetMODE_TUTORIAL(bool MODE_TUTORIAL){ this.MODE_TUTORIAL = MODE_TUTORIAL; }
    private const bool MODE_DEBUG = false; public bool GetMODE_DEBUG(){ return(MODE_DEBUG); }
    // 1 is original values, 2 would be a 2x speedup (each resource takes 50% less time to get, item crafting duration is 50%).
    private const float globalMultiplier = 1; public float GetGlobalMultiplier(){ return(globalMultiplier); }

    private const string filepathInventoryJSON = "Data/shop_click_values_vanilla_munged_20190829";
    private const string filepathCharactersJSON = "Data/shop_click_characters";
    
    private const string filepathSecretJSON = "Data/AdMob"; private Secret secret; public Secret GetSecret(){ return(secret); }
    public void InitSecret()
    {
        TextAsset jsonTextFile = Resources.Load<TextAsset>(filepathSecretJSON);
        //Debug.Log("jsonTextFile.ToString(): " + jsonTextFile.ToString());
        secret = Helper.FromJson<Secret>(jsonTextFile.ToString())[0];
        Debug.Log("Secrets read: " + secret.iDAdMobApp + ", " + secret.iDAdMobAdBanner + ", " + secret.iDAdMobAdInterstitial);
    }

    public Random rnd = new Random();

    Dictionary<string, Resource>  stats; public Dictionary<string, Resource> GetStats(){ return(stats); }
    Dictionary<string, Resource>  resources; public Dictionary<string, Resource> GetResources(){ return(resources); }
    Dictionary<string, Item>      inventory; public Dictionary<string, Item> GetInventory(){ return(inventory); }
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
        InitSecret();        
    }


    void Start()
    {
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
            stats.Add("Chakra", new Resource("Chakra", 1, 0,                    999, 100));
            stats.Add("Gems",   new Resource("Gems",   1, 0, System.UInt64.MaxValue,   5));
            stats.Add("Stock",  new Resource("Stock",  1, 0,                    999,   0));
        }
        else
        {
            stats.Add("Level",  new Resource("Level",  1, 0, System.UInt64.MaxValue,  1));
            stats.Add("Coins",  new Resource("Coins",  1, 0, System.UInt64.MaxValue,  0));
            stats.Add("Chakra", new Resource("Chakra", 1, 0,                    100, 46));
            stats.Add("Gems",   new Resource("Gems",   1, 0, System.UInt64.MaxValue,  5));
            stats.Add("Stock",  new Resource("Stock",  1, 0,                     15,  0));
        }

//{600, 900, 1400, 2100, 3100, 4500, 6400, 9100, 14300, 20500, 27800, 36400, 46600, 58600, 72800, 89500, 109300, 132600, 160100, 192600, 231000, 276300, 329800, 392900, 467400, 555300, 659100, 781600, 926100, 1096700, 1298100, 1535800, 1816300, 2147400, 2538200, 2999500, 3544000, 4186700, 4945300, 5840700, 6897500, 8144900, 9617200, 11355000, 13406200, 15827200, 18684800, 22057700, 26038800, 30737700, 36284000, 42830400, 50557200, 59677300, 70442000, 83147700, 98144500, 115845600, 136738600, 161399000, 190506200, 224862000, 265412900, 313275900, 369769600, 436450300, 515154900, 608051500, 717699300, 847118900, 999875500, 1180177300, 1392991200, 1644179800, 1940662900, 2290608100, 2703655600, 3191184100, 3766624000}


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
            inventory.Add(items[i].id, items[i]);
            inventory[items[i].id].timeCrafting = (ulong)(inventory[items[i].id].timeCrafting * (1 / Global.instance.GetGlobalMultiplier()));
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
            inventory["spear_1"].SetStock(1);
            inventory["bow_1"].SetStock(1);
            inventory["larmor_1"].SetStock(1);
            inventory["hat_1"].SetStock(1);
        }
        else
        {
            inventory["axe_1"].SetIsAvailable(true); inventory["axe_1"].SetStock(0);
            inventory["dagger_1"].SetIsAvailable(true); inventory["dagger_1"].SetStock(0);
            inventory["spear_1"].SetIsAvailable(true); inventory["spear_1"].SetStock(1);
            inventory["bow_1"].SetIsAvailable(true); inventory["bow_1"].SetStock(1);
            inventory["harmor_1"].SetIsAvailable(true); inventory["harmor_1"].SetStock(0);
            inventory["larmor_1"].SetIsAvailable(true); inventory["larmor_1"].SetStock(1);
            inventory["helmet_1"].SetIsAvailable(true); inventory["helmet_1"].SetStock(0);
            inventory["headgear_1"].SetIsAvailable(true); inventory["headgear_1"].SetStock(0);
            inventory["hat_1"].SetIsAvailable(true); inventory["hat_1"].SetStock(1);
            inventory["gauntlets_1"].SetIsAvailable(true); inventory["gauntlets_1"].SetStock(0);
            inventory["boots_1"].SetIsAvailable(true); inventory["boots_1"].SetStock(0);
            inventory["shield_1"].SetIsAvailable(true); inventory["shield_1"].SetStock(0);
            inventory["potion_1"].SetIsAvailable(true); inventory["potion_1"].SetStock(0);
        }
    }

    public Item RandomItem(bool isStocked)
    {
        // Returns a random item from the inventory.
        // Will make this more elaborate later (only return things in stock or mixed, etc.).

        List<string> keyList = new List<string>();

        // Iterate through dictionary
        foreach(KeyValuePair<string, Item> entry in inventory)
        {
            if(isStocked) // Could've purchased an item that you still can't make yet
            {
                if(entry.Value.GetStock() > 0){
                    keyList.Add(entry.Key);
                }
            }
            else
            {
                if(entry.Value.GetIsAvailable()){
                    keyList.Add(entry.Key);
                }
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


