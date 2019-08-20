using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resource : MonoBehaviour
{
    TextMeshProUGUI tMPro;
    public void SetTMPro(TextMeshProUGUI setTMPro){ tMPro = setTMPro; }
    public void UpdateTMPro(string updateTMPro){ tMPro.text = updateTMPro; }
    
    private string resourceName;
    public string GetResourceName(){ return(resourceName); }
    public void SetResourceName(string setResourceName){ resourceName = setResourceName; }

    private int resourceLevel;
    public int GetResourceLevel(){ return(resourceLevel); }
    public void SetResourceLevel(int setResourceLevel){ resourceLevel = setResourceLevel; }

    private int resourceRate;
    public int GetResourceRate(){ return(resourceRate); }
    public void SetResourceRate(int setResourceRate){ resourceRate = setResourceRate; }

    private int resourceCap;
    public int GetResourceCap(){ return(resourceCap); }
    public void SetResourceCap(int setResourceCap){ resourceCap = setResourceCap; }



    
    private int resourceAmount;
    public int GetResourceAmount(){ return(resourceAmount); }
    public void SetResourceAmount(int setResourceAmount){ resourceAmount = setResourceAmount; }
    public void AddResourceAmount(int addResourceAmount){ resourceAmount += addResourceAmount; }
    public bool CheckResourceAmount(int removeResourceAmount){ Debug.Log("Checking " + resourceName + " amount..."); if(removeResourceAmount <= resourceAmount){ Debug.Log("...enough " + resourceName + "."); return(true); }else{ Debug.Log("...not enough " + resourceName + "."); return(false); } }
    public void RemoveResourceAmount(int removeResourceAmount){ resourceAmount -= removeResourceAmount; }
    public void IncrementResourceAmount(int setResourceAmount){ resourceAmount = setResourceAmount; }



    // Use this for initialization
    public Resource(string resourceName, int resourceLevel, int resourceRate, int resourceCap, int resourceAmount, TextMeshProUGUI tMPro)
    {
        this.resourceName = resourceName;
        this.resourceLevel = resourceLevel;
        this.resourceRate = resourceRate;
        this.resourceCap = resourceCap;
        this.resourceAmount = resourceAmount;
        this.tMPro = tMPro;
        UpdateTMPro(resourceAmount.ToString());
    }
}
