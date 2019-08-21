using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private Item[] items;

    private int stock;
    public int GetStock(){ return(stock); }
    public void SetStock(int setStock){ stock = setStock; }
    public void AddStock(int addStock){ stock += addStock; }
    public void RemoveStock(int removeStock){ stock -= removeStock; }
    public bool CheckStock(int removeStock){ if(removeStock <= stock){ Debug.Log("Item in stock..."); return(true); } else { return(false); } }

    private ulong value;
    public ulong GetValue(){ return(value); }
    public void SetValue(ulong setValue){ value = setValue; }


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        SetStock(10);
        SetValue(100);

        string filePath = "Data/shop_click_values_SHORT";
        readValues(filePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void readValues(string filePath)
    {
        Debug.Log("Before JSON read.");
        TextAsset jsonTextFile = Resources.Load<TextAsset>(filePath);
        items = Helper.FromJson<Item>(jsonTextFile.ToString());
        Debug.Log("After JSON read.");
        Debug.Log("items[0].GetStock() = " + items[0].GetStock());
    }

}
