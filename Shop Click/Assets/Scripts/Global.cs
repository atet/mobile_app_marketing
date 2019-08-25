using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global instance;

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
        InitInventory();
        inventory["Wood Bow"].SetIsAvailable(true);
        inventory["Wood Bow"].SetStock(5);
        inventory["Wood Dagger"].SetIsAvailable(true);
        inventory["Wood Dagger"].SetStock(5);
        InitCharacters();
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

        stats.Add("Level", new Resource("Level", 1, 0, System.UInt64.MaxValue, 1));
        stats.Add("Coins", new Resource("Coins", 1, 0, System.UInt64.MaxValue, 0));
        stats.Add("Chakra", new Resource("Chakra", 1, 0, 100, 39));
        stats.Add("Gems", new Resource("Gems", 1, 0, System.UInt64.MaxValue, 5));
    }

    public void InitResources()
    {
        resources = new Dictionary<string, Resource>();

        // Customers coming in the store.
        resources.Add("Queue",       new Resource("Queue",       1, 10, 10,  5));

        resources.Add("Iron",        new Resource("Iron",        1, 10, 20, 10));
        resources.Add("Wood",        new Resource("Wood",        1, 10, 20, 10));
        resources.Add("Hide",        new Resource("Hide",        1, 10,  0,  0));
        resources.Add("Herbs",       new Resource("Herbs",       1, 10,  0,  0));
        resources.Add("Steel",       new Resource("Steel",       1, 10,  0,  0));
        resources.Add("Oil",         new Resource("Oil",         1, 10,  0,  0));
        resources.Add("Electricity", new Resource("Electricity", 1, 10,  0,  0));
        resources.Add("Titanium",    new Resource("Titanium",    1, 10,  0,  0));
    }

    public void InitCharacters()
    {
        // Read in JSON data
        string filepathJSON = "Data/shop_click_characters";
        TextAsset jsonTextFile = Resources.Load<TextAsset>(filepathJSON);
        Character[] items = Helper.FromJson<Character>(jsonTextFile.ToString());
        // Debug.Log("JSON inventory read, contains " + items.Length.ToString() + " items.");

        // Populate dictionary with JSON values
        characters = new Dictionary<string, Character>();

        for (int i = 0; i < items.Length; i++)
        {
            characters.Add(items[i].name, items[i]);
            // Debug.Log(items[i].name);
        }
    }
    public Character RandomCharacter()
    {
        // Returns a random character from the characters.
        List<string> keyList = new List<string>(characters.Keys);
        return(characters[keyList[Random.Range(0, characters.Count)]]);
    }

    public void InitInventory()
    {
        // Read in JSON data
        string filepathJSON = "Data/shop_click_values_SHORT";
        TextAsset jsonTextFile = Resources.Load<TextAsset>(filepathJSON);
        Item[] items = Helper.FromJson<Item>(jsonTextFile.ToString());
        // Debug.Log("JSON inventory read, contains " + items.Length.ToString() + " items.");

        // Populate dictionary with JSON values
        inventory = new Dictionary<string, Item>();

        for (int i = 0; i < items.Length; i++)
        {
            inventory.Add(items[i].name, items[i]);
            // Debug.Log(items[i].name);
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


}


