using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Global : MonoBehaviour
{
    public static Global instance;

    [SerializeField] TextMeshProUGUI tMProLevel;
    public void UpdateTMProLevel(){ tMProLevel.text = level.ToString(); }
    private int level;
    public int GetLevel(){ return(level); }
    public void SetLevel(int setLevel){ level = setLevel; UpdateTMProLevel(); }
    public void IncrementLevel(){ level += 1; UpdateTMProLevel(); }
    private ulong experience;
    public ulong GetExperience(){ return(experience); }
    public void SetExperience(ulong setExperience){ experience = setExperience; }
    private ulong[] experienceThresholds = {600, 900, 1400, 2100, 3100, 4500, 6400, 9100, 14300, 20500, 27800, 36400, 46600, 58600, 72800, 89500, 109300, 132600, 160100, 192600, 231000, 276300, 329800, 392900, 467400, 555300, 659100, 781600, 926100, 1096700, 1298100, 1535800, 1816300, 2147400, 2538200, 2999500, 3544000, 4186700, 4945300, 5840700, 6897500, 8144900, 9617200, 11355000, 13406200, 15827200, 18684800, 22057700, 26038800, 30737700, 36284000, 42830400, 50557200, 59677300, 70442000, 83147700, 98144500, 115845600, 136738600, 161399000, 190506200, 224862000, 265412900, 313275900, 369769600, 436450300, 515154900, 608051500, 717699300, 847118900, 999875500, 1180177300, 1392991200, 1644179800, 1940662900, 2290608100, 2703655600, 3191184100, 3766624000};
    // TODO: If experience added will increment level, increment level based on above.
    public void AddExperience(ulong addExperience){ experience += addExperience; Debug.Log("AddExperience(" + addExperience.ToString() + ") = " + experience.ToString()); }
    
    [SerializeField] TextMeshProUGUI tMProCoins;
    public void UpdateTMProCoins(){ tMProCoins.text = coins.ToString("N0"); }
    private ulong coins; private ulong coinsLifetime; private ulong coinsSpent;
    public ulong GetCoins(){ return(coins); }
    public void SetCoins(ulong setCoins){ coins = setCoins; UpdateTMProCoins(); }
    public void AddCoins(ulong addCoins){ coins += addCoins; coinsLifetime += addCoins; UpdateTMProCoins(); Debug.Log("AddCoins(" + addCoins.ToString() + ") = " + coins.ToString()); }
    public bool CheckCoins(ulong removeCoins){ if(removeCoins <= coins){ return(true); } else { return(false); } }
    // Each remove gold based on a customer sale will add to merchant experience
    public void RemoveCoins(ulong removeCoins){ coins -= removeCoins; coinsSpent -= removeCoins; UpdateTMProCoins(); Debug.Log("RemoveCoins(" + removeCoins.ToString() + ") = " + coins.ToString()); }
    
    [SerializeField] TextMeshProUGUI tMProChakra;
    public void UpdateTMProChakra(){ tMProChakra.text = chakra.ToString() + "%"; }
    public int GetChakra(){ return(chakra); }
    private int chakra;
    public void SetChakra(int setChakra){ chakra = setChakra; UpdateTMProChakra(); }
    public void AddChakra(int addChakra){ if((chakra + addChakra) >= 100 ){ chakra = 100; }else{ chakra += addChakra; } UpdateTMProChakra(); Debug.Log("AddChakra(" + addChakra.ToString() + ") = " + chakra.ToString()); }
    public bool CheckChakra(int removeChakra)
    { 
        Debug.Log("Checking Chakra amount...");
        if(removeChakra <= chakra)
        { 
            Debug.Log("...enough chakra.");
            return(true); 
        } 
        else 
        { 
            Debug.Log("...not enough chakra.");
            return(false); 
        } 
    }
    public void RemoveChakra(int removeChakra){ chakra -= removeChakra; UpdateTMProChakra(); Debug.Log("RemoveChakra(" + removeChakra.ToString() + ") = " + chakra.ToString()); }
    

    private int gemsCurrent;
    private int gemsLifetime;
    private int gemsSpent;


    private int inventoryTotalCount;
    public int GetInventoryTotalCount(){ return(inventoryTotalCount); }
    public void SetInventoryTotalCount(int addInventoryTotalCount){ inventoryTotalCount += addInventoryTotalCount; }


    void Start()
    {
        instance = this;
        SetLevel(1);
        SetExperience(0);
        SetCoins(0);
        SetChakra(40);
    }
}
