using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global instance;
    private bool MODE_TUTORIAL = true; public bool GetMODE_TUTORIAL(){ return(MODE_TUTORIAL); } public void SetMODE_TUTORIAL(bool MODE_TUTORIAL){ this.MODE_TUTORIAL = MODE_TUTORIAL; }
    private const bool MODE_DEBUG = false; public bool GetMODE_DEBUG(){ return(MODE_DEBUG); }
    // 1 is original values, 2 would be a 2x speedup (each resource takes 50% less time to get, item crafting duration is 50%).
    private const float globalMultiplier = 100; public float GetGlobalMultiplier(){ return(globalMultiplier); }

    private const string filepathInventoryJSON = "Data/shop_click_values_vanilla_munged_20190925";
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
            stats.Add("Level",  new Resource("Level",  "Images/UI/level" , 1, 0, System.UInt64.MaxValue,   1));
            stats.Add("Coins",  new Resource("Coins",  "Images/UI/coins" , 1, 0, System.UInt64.MaxValue,   0));
            stats.Add("Chakra", new Resource("Chakra", "Images/UI/chakra", 1, 0,                    999, 100));
            stats.Add("Gems",   new Resource("Gems",   "Images/UI/gems"  , 1, 0, System.UInt64.MaxValue,   5));
            stats.Add("Stock",  new Resource("Stock",  "Images/UI/stock" , 1, 0,                    999,   0));
        }
        else
        {
            stats.Add("Level",  new Resource("Level",  "Images/UI/level" , 1, 0, System.UInt64.MaxValue,  1));
            //stats.Add("Coins",  new Resource("Coins",  "Images/UI/coins" , 1, 0, System.UInt64.MaxValue,  0));
            stats.Add("Coins",  new Resource("Coins",  "Images/UI/coins" , 1, 0, System.UInt64.MaxValue, 9999999));
            stats.Add("Chakra", new Resource("Chakra", "Images/UI/chakra", 1, 0,                    100, 46));
            stats.Add("Gems",   new Resource("Gems",   "Images/UI/gems"  , 1, 0, System.UInt64.MaxValue,  0));
            //stats.Add("Gems",   new Resource("Gems",   "Images/UI/gems"  , 1, 0, System.UInt64.MaxValue,  999));
            // stats.Add("Stock",  new Resource("Stock",  "Images/UI/stock" , 1, 0,                     15,  0));
            stats.Add("Stock",  new Resource("Stock",  "Images/UI/stock" , 1, 0,                    999,  0));
        }

        stats["Level"].SetThresholdBool(true);
        stats["Level"].SetThresholdKeys(new List<int>(){2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81});
        stats["Level"].SetThresholdValues(new List<ulong>(){1200, 1400, 1700, 2100, 3100, 4500, 6400, 9100, 14300, 20500, 27800, 36400, 46600, 58600, 72800, 89500, 109300, 132600, 160100, 192600, 231000, 276300, 329800, 392900, 467400, 555300, 659100, 781600, 926100, 1096700, 1298100, 1535800, 1816300, 2147400, 2538200, 2999500, 3544000, 4186700, 4945300, 5840700, 6897500, 8144900, 9617200, 11355000, 13406200, 15827200, 18684800, 22057700, 26038800, 30737700, 36284000, 42830400, 50557200, 59677300, 70442000, 83147700, 98144500, 115845600, 136738600, 161399000, 190506200, 224862000, 265412900, 313275900, 369769600, 436450300, 515154900, 608051500, 717699300, 847118900, 999875500, 1180177300, 1392991200, 1644179800, 1940662900, 2290608100, 2703655600, 3191184100, 3766624000});
        stats["Level"].SetThresholdRates(new List<float>(){8.89f, 8f, 7.27f, 6.67f, 6.15f, 5.71f, 5.33f, 5f, 4.71f, 4.44f, 4.21f, 4f, 3.81f, 3.64f, 3.48f, 3.33f, 3.16f, 3f, 2.73f});
        stats["Level"].SetThresholdTypes(new List<string>(){"level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level","level"});
        stats["Level"].SetThresholdDescriptions(new List<string>(){"Excitement is to be had, level 2!\n\nYou gain experience by selling items.\n\nSome upgrades can only be unlocked at certain levels.","Niceness, 3!!!","There's more to go level 4.","You have ascended to level 5.","You are on you way to the top, level 6.","Cool beans, level 7.","There's more to go level 8.","Sweet!, 9!","You are on you way to the top, level 10.","Sweet!, 11!","Congrats!, level 12.","You have ascended to level 13.","Merchant level 14.","Merchant level 15.","You made it to 16!","Here's a cake, level 17!","You made it to 18!","There's more to go level 19.","Excitement is to be had, 20!","Merchant level 21.","Merchant level 22.","Excitement is to be had, 23!","Excitement is to be had, 24!","Merchant level 25.","Merchant level 26.","You are on you way to the top, level 27.","You made it to 28!","Excitement is to be had, 29!","I had faith you'd make it to level 30.","You have ascended to level 31.","Niceness, 32!!!","Congrats!, level 33.","Here's a cake, level 34!","You made it to 35!","You made it to 36!","Here's a cake, level 37!","Niceness, 38!!!","I had faith you'd make it to level 39.","Niceness, 40!!!","You made it to 41!","I had faith you'd make it to level 42.","Excitement is to be had, 43!","There's more to go level 44.","Here's a cake, level 45!","Excitement is to be had, 46!","Look at you, you've made it to level 47!","I had faith you'd make it to level 48.","Excitement is to be had, 49!","There's more to go level 50.","Look at you, you've made it to level 51!","Here's a cake, level 52!","You made it to 53!","There's more to go level 54.","Congrats!, level 55.","You made it to 56!","Here's a cake, level 57!","Look at you, you've made it to level 58!","You are on you way to the top, level 59.","Cool beans, level 60.","You are on you way to the top, level 61.","Here's a cake, level 62!","Here's a cake, level 63!","You have ascended to level 64.","Congrats!, level 65.","Congrats!, level 66.","Look at you, you've made it to level 67!","Here's a cake, level 68!","There's more to go level 69.","I had faith you'd make it to level 70.","Here's a cake, level 71!","Look at you, you've made it to level 72!","There's more to go level 73.","Sweet!, 74!","You have ascended to level 75.","Niceness, 76!!!","Cool beans, level 77.","Look at you, you've made it to level 78!","Excitement is to be had, 79!","Excitement is to be had, 80!","There's more to go level 81."});
        stats["Level"].SetThresholdEventBools(new List<bool>(){true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true});

    }

    public void InitResources()
    {
        resources = new Dictionary<string, Resource>();
        ulong componentStartCap;
        ulong componentStartAmount;
        // resources
        // 1 iron
        // 2 wood
        // 3 hide
        // 4 herbs
        // 5 steel
        // 6 titanium
        // 7 electricity
        // 8 oil

        if(MODE_DEBUG)
        {
            resources.Add("resource_0", new Resource("Queue",       "Images/UI/queue",       1,  1, 999, 900));
            resources.Add("resource_1", new Resource("Iron",        "Images/UI/iron",        1,  1, 999, 900));
            resources.Add("resource_2", new Resource("Wood",        "Images/UI/wood",        1,  1, 999, 900));
            resources.Add("resource_3", new Resource("Hide",        "Images/UI/hide",        1,  1, 999, 900));
            resources.Add("resource_4", new Resource("Herbs",       "Images/UI/herbs",       1,  1, 999, 900));
            resources.Add("resource_5", new Resource("Steel",       "Images/UI/steel",       1,  1, 999, 900));
            resources.Add("resource_6", new Resource("Titanium",    "Images/UI/titanium",    1,  1, 999, 900));
            resources.Add("resource_7", new Resource("Electricity", "Images/UI/electricity", 1,  1, 999, 900));
            resources.Add("resource_8", new Resource("Oil",         "Images/UI/oil",         1,  1, 999, 900));
            componentStartCap = 999;
            componentStartAmount = 999;
        }
        else
        {
            // Start only with iron (and queue)
            resources.Add("resource_0", new Resource("Queue",       "Images/UI/queue",       1, 10, 10,  5));
            resources.Add("resource_1", new Resource("Iron",        "Images/UI/iron",        1, 10, 35,  5));
            resources.Add("resource_2", new Resource("Wood",        "Images/UI/wood",        1, 10,  0,  0));
            resources.Add("resource_3", new Resource("Hide",        "Images/UI/hide",        1, 10,  0,  0));
            resources.Add("resource_4", new Resource("Herbs",       "Images/UI/herbs",       1, 10,  0,  0));
            resources.Add("resource_5", new Resource("Steel",       "Images/UI/steel",       1, 45,  0,  0));
            resources.Add("resource_6", new Resource("Titanium",    "Images/UI/titanium",    1, 45,  0,  0));
            resources.Add("resource_7", new Resource("Electricity", "Images/UI/electricity", 1, 45,  0,  0));
            resources.Add("resource_8", new Resource("Oil",         "Images/UI/oil",         1, 45,  0,  0));
            // Making components Resource instead of Item (since there won't be buy/sell components)
            // componentStartCap = 0;
            // componentStartAmount = 0;
            componentStartCap = 999;
            componentStartAmount = 999;
        }

        resources.Add("component_1", new Resource("Fish Scale",     "Images/Component/component_2",   1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_2", new Resource("Egg",            "Images/Component/component_10",  1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_3", new Resource("Broken Bone",    "Images/Component/component_15",  1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_4", new Resource("Snake Fang",     "Images/Component/component_16",  1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_5", new Resource("Whole Bone",     "Images/Component/component_21",  1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_6", new Resource("Petrified Rock", "Images/Component/component_26",  1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_7", new Resource("Blue Tooth",     "Images/Component/component_29",  1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_8", new Resource("Red Eye",        "Images/Component/component_31",  1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_9", new Resource("Spiral Fruit",   "Images/Component/component_32",  1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_10", new Resource("Aquamarine",    "Images/Component/component_40",  1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_11", new Resource("Bat Wing",      "Images/Component/component_49",  1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_12", new Resource("Diamond Stud",  "Images/Component/component_51",  1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_13", new Resource("Tomacco Leaf",  "Images/Component/component_54",  1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_14", new Resource("Giant Claw",    "Images/Component/component_55",  1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_15", new Resource("Wyrm Skull",    "Images/Component/component_57",  1, 0, componentStartCap, componentStartAmount));
        resources.Add("component_16", new Resource("Starrock",      "Images/Component/component_100", 1, 0, componentStartCap, componentStartAmount));

        resources["resource_1"].SetThresholdBool(true);
        resources["resource_1"].SetThresholdKeys(new List<int>(){2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20});
        //resources["resource_1"].SetThresholdIncrementsCoins(new List<ulong>(){100, 200, 400, 1000, 2200, 2800, 8000, 12000, 50000, 150000, 300000, 500000, 1000000, 2000000, 4000000, 10000000, 30000000, 50000000, 100000000});
        resources["resource_1"].SetThresholdIncrementsCoins(new List<ulong>(){1000, 2000, 4000, 10000, 22000, 28000, 80000, 120000, 500000, 1500000, 3000000, 5000000, 10000000, 20000000, 40000000, 100000000, 300000000, 500000000, 1000000000});
        resources["resource_1"].SetThresholdIncrementsGems(new List<ulong>(){2, 4, 6, 10, 20, 23, 30, 35, 40, 50, 60, 70, 80, 90, 100, 100, 100, 100, 100});
        resources["resource_1"].SetThresholdValues(new List<ulong>(){1000, 4000, 12000, 60000, 198000, 300000, 1425000, 3200000, 19800000, 70000000, 220000000, 520000000, 1120000000, 3000000000, 6400000000, 17000000000, 54000000000, 95000000000, 200000000000});
        resources["resource_1"].SetThresholdRates(new List<float>(){10f, 8.89f, 8f, 7.27f, 6.67f, 6.15f, 5.71f, 5.33f, 5f, 4.71f, 4.44f, 4.21f, 4f, 3.81f, 3.64f, 3.48f, 3.33f, 3.16f, 3f, 2.73f});
        resources["resource_1"].SetThresholdTypes(new List<string>(){"level", "level", "level", "level", "level", "unlock_id01", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level"});
        resources["resource_1"].SetThresholdDescriptions(new List<string>(){"Excitement is to be had, level 2!","Niceness, 3!!!","There's more to go level 4.","You have ascended to level 5.","You are on you way to the top, level 6.","Cool beans, level 7.","There's more to go level 8.","Sweet!, 9!","You are on you way to the top, level 10.","Sweet!, 11!","Congrats!, level 12.","You have ascended to level 13.","Merchant level 14.","Merchant level 15.","You made it to 16!","Here's a cake, level 17!","You made it to 18!","There's more to go level 19.","Excitement is to be had, 20!"});
        resources["resource_1"].SetThresholdEventBools(new List<bool>(){true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true});
        resources["resource_1"].SetThresholdCaps(new List<ulong>(){35,47,58,70,81,93,104,116,127,139,150,162,173,185,196,208,219,231,242,254,265,277,288,300,311,323,334,346,357,369,380,392,403,415,426,438,449,461,472,484,495,507,518,530,541,553,564,576,587,599,610,622,633,645,656,668,679,691,702,714,725,737,748,760});
        resources["resource_1"].SetThresholdCapCostCoins(new List<ulong>(){1225,1475,1775,2137,2572,3096,3726,4484,5398,6497,7820,9412,11328,13635,16411,19753,23775,28616,34443,41456,49897,60057,72286,87006,104722,126045,151711,182602,219784,264536,318401,383235,461269,555193,668242,804311,968085,1165208,1402468,1688040,2031761,2445470,2943419,3542761,4264142,5132410,6177477,7435341,8949332,10771604,12964929,15604861,18782338,22606817,27210040,32750575,39419280,47445873,57106849,68735003,82730893,99576640,119852535});
        resources["resource_1"].SetThresholdCapCostGems(new List<ulong>(){2,2,2,2,3,3,3,4,4,5,5,6,7,8,9,10,12,13,15,17,19,22,25,28,32,36,41,47,53,60,69,78,88,100,114,129,147,166,189,214,243,276,313,356,404,458,520,590,670,761,863,980,1112,1263,1433,1627,1846,2096,2378,2700,3064,3478,3948});

        resources["resource_2"].SetThresholdBool(true);
        resources["resource_2"].SetThresholdKeys(new List<int>(){2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20});
        //resources["resource_2"].SetThresholdIncrementsCoins(new List<ulong>(){100, 200, 400, 1000, 2200, 2800, 8000, 12000, 50000, 150000, 300000, 500000, 1000000, 2000000, 4000000, 10000000, 30000000, 50000000, 100000000});
        resources["resource_2"].SetThresholdIncrementsCoins(new List<ulong>(){1000, 2000, 4000, 10000, 22000, 28000, 80000, 120000, 500000, 1500000, 3000000, 5000000, 10000000, 20000000, 40000000, 100000000, 300000000, 500000000, 1000000000});
        resources["resource_2"].SetThresholdIncrementsGems(new List<ulong>(){2, 4, 6, 10, 20, 23, 30, 35, 40, 50, 60, 70, 80, 90, 100, 100, 100, 100, 100});
        resources["resource_2"].SetThresholdValues(new List<ulong>(){1000, 4000, 12000, 60000, 198000, 300000, 1425000, 3200000, 19800000, 70000000, 220000000, 520000000, 1120000000, 3000000000, 6400000000, 17000000000, 54000000000, 95000000000, 200000000000});
        resources["resource_2"].SetThresholdRates(new List<float>(){10f, 8.89f, 8f, 7.27f, 6.67f, 6.15f, 5.71f, 5.33f, 5f, 4.71f, 4.44f, 4.21f, 4f, 3.81f, 3.64f, 3.48f, 3.33f, 3.16f, 3f, 2.73f});
        resources["resource_2"].SetThresholdTypes(new List<string>(){"level", "level", "level", "level", "level", "unlock_id02", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level"});
        resources["resource_2"].SetThresholdDescriptions(new List<string>(){"Excitement is to be had, level 2!","Niceness, 3!!!","There's more to go level 4.","You have ascended to level 5.","You are on you way to the top, level 6.","Cool beans, level 7.","There's more to go level 8.","Sweet!, 9!","You are on you way to the top, level 10.","Sweet!, 11!","Congrats!, level 12.","You have ascended to level 13.","Merchant level 14.","Merchant level 15.","You made it to 16!","Here's a cake, level 17!","You made it to 18!","There's more to go level 19.","Excitement is to be had, 20!"});
        resources["resource_2"].SetThresholdEventBools(new List<bool>(){true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true});
        resources["resource_2"].SetThresholdCaps(new List<ulong>(){0,35,47,58,70,81,93,104,116,127,139,150,162,173,185,196,208,219,231,242,254,265,277,288,300,311,323,334,346,357,369,380,392,403,415,426,438,449,461,472,484,495,507,518,530,541,553,564,576,587,599,610,622,633,645,656,668,679,691,702,714,725,737,748,760});
        resources["resource_2"].SetThresholdCapCostCoins(new List<ulong>(){6018,1225,1475,1775,2137,2572,3096,3726,4484,5398,6497,7820,9412,11328,13635,16411,19753,23775,28616,34443,41456,49897,60057,72286,87006,104722,126045,151711,182602,219784,264536,318401,383235,461269,555193,668242,804311,968085,1165208,1402468,1688040,2031761,2445470,2943419,3542761,4264142,5132410,6177477,7435341,8949332,10771604,12964929,15604861,18782338,22606817,27210040,32750575,39419280,47445873,57106849,68735003,82730893,99576640,119852535});
        resources["resource_2"].SetThresholdCapCostGems(new List<ulong>(){11,2,2,2,2,3,3,3,4,4,5,5,6,7,8,9,10,12,13,15,17,19,22,25,28,32,36,41,47,53,60,69,78,88,100,114,129,147,166,189,214,243,276,313,356,404,458,520,590,670,761,863,980,1112,1263,1433,1627,1846,2096,2378,2700,3064,3478,3948});

        resources["resource_3"].SetThresholdBool(true);
        resources["resource_3"].SetThresholdKeys(new List<int>(){2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20});
        //resources["resource_3"].SetThresholdIncrementsCoins(new List<ulong>(){100, 200, 400, 1000, 2200, 2800, 8000, 12000, 50000, 150000, 300000, 500000, 1000000, 2000000, 4000000, 10000000, 30000000, 50000000, 100000000});
        resources["resource_3"].SetThresholdIncrementsCoins(new List<ulong>(){1000, 2000, 4000, 10000, 22000, 28000, 80000, 120000, 500000, 1500000, 3000000, 5000000, 10000000, 20000000, 40000000, 100000000, 300000000, 500000000, 1000000000});
        resources["resource_3"].SetThresholdIncrementsGems(new List<ulong>(){2, 4, 6, 10, 20, 23, 30, 35, 40, 50, 60, 70, 80, 90, 100, 100, 100, 100, 100});
        resources["resource_3"].SetThresholdValues(new List<ulong>(){1000, 4000, 12000, 60000, 198000, 300000, 1425000, 3200000, 19800000, 70000000, 220000000, 520000000, 1120000000, 3000000000, 6400000000, 17000000000, 54000000000, 95000000000, 200000000000});
        resources["resource_3"].SetThresholdRates(new List<float>(){10f, 8.89f, 8f, 7.27f, 6.67f, 6.15f, 5.71f, 5.33f, 5f, 4.71f, 4.44f, 4.21f, 4f, 3.81f, 3.64f, 3.48f, 3.33f, 3.16f, 3f, 2.73f});
        resources["resource_3"].SetThresholdTypes(new List<string>(){"level", "level", "level", "level", "level", "unlock_id03", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level"});
        resources["resource_3"].SetThresholdDescriptions(new List<string>(){"Excitement is to be had, level 2!","Niceness, 3!!!","There's more to go level 4.","You have ascended to level 5.","You are on you way to the top, level 6.","Cool beans, level 7.","There's more to go level 8.","Sweet!, 9!","You are on you way to the top, level 10.","Sweet!, 11!","Congrats!, level 12.","You have ascended to level 13.","Merchant level 14.","Merchant level 15.","You made it to 16!","Here's a cake, level 17!","You made it to 18!","There's more to go level 19.","Excitement is to be had, 20!"});
        resources["resource_3"].SetThresholdEventBools(new List<bool>(){true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true});
        resources["resource_3"].SetThresholdCaps(new List<ulong>(){0,35,47,58,70,81,93,104,116,127,139,150,162,173,185,196,208,219,231,242,254,265,277,288,300,311,323,334,346,357,369,380,392,403,415,426,438,449,461,472,484,495,507,518,530,541,553,564,576,587,599,610,622,633,645,656,668,679,691,702,714,725,737,748,760});
        resources["resource_3"].SetThresholdCapCostCoins(new List<ulong>(){6018,1225,1475,1775,2137,2572,3096,3726,4484,5398,6497,7820,9412,11328,13635,16411,19753,23775,28616,34443,41456,49897,60057,72286,87006,104722,126045,151711,182602,219784,264536,318401,383235,461269,555193,668242,804311,968085,1165208,1402468,1688040,2031761,2445470,2943419,3542761,4264142,5132410,6177477,7435341,8949332,10771604,12964929,15604861,18782338,22606817,27210040,32750575,39419280,47445873,57106849,68735003,82730893,99576640,119852535});
        resources["resource_3"].SetThresholdCapCostGems(new List<ulong>(){11,2,2,2,2,3,3,3,4,4,5,5,6,7,8,9,10,12,13,15,17,19,22,25,28,32,36,41,47,53,60,69,78,88,100,114,129,147,166,189,214,243,276,313,356,404,458,520,590,670,761,863,980,1112,1263,1433,1627,1846,2096,2378,2700,3064,3478,3948});

        resources["resource_4"].SetThresholdBool(true);
        resources["resource_4"].SetThresholdKeys(new List<int>(){2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20});
        //resources["resource_4"].SetThresholdIncrementsCoins(new List<ulong>(){100, 200, 400, 1000, 2200, 2800, 8000, 12000, 50000, 150000, 300000, 500000, 1000000, 2000000, 4000000, 10000000, 30000000, 50000000, 100000000});
        resources["resource_4"].SetThresholdIncrementsCoins(new List<ulong>(){1000, 2000, 4000, 10000, 22000, 28000, 80000, 120000, 500000, 1500000, 3000000, 5000000, 10000000, 20000000, 40000000, 100000000, 300000000, 500000000, 1000000000});
        resources["resource_4"].SetThresholdIncrementsGems(new List<ulong>(){2, 4, 6, 10, 20, 23, 30, 35, 40, 50, 60, 70, 80, 90, 100, 100, 100, 100, 100});
        resources["resource_4"].SetThresholdValues(new List<ulong>(){1000, 4000, 12000, 60000, 198000, 300000, 1425000, 3200000, 19800000, 70000000, 220000000, 520000000, 1120000000, 3000000000, 6400000000, 17000000000, 54000000000, 95000000000, 200000000000});
        resources["resource_4"].SetThresholdRates(new List<float>(){10f, 8.89f, 8f, 7.27f, 6.67f, 6.15f, 5.71f, 5.33f, 5f, 4.71f, 4.44f, 4.21f, 4f, 3.81f, 3.64f, 3.48f, 3.33f, 3.16f, 3f, 2.73f});
        resources["resource_4"].SetThresholdTypes(new List<string>(){"level", "level", "level", "level", "level", "unlock_id04", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level"});
        resources["resource_4"].SetThresholdDescriptions(new List<string>(){"Excitement is to be had, level 2!","Niceness, 3!!!","There's more to go level 4.","You have ascended to level 5.","You are on you way to the top, level 6.","Cool beans, level 7.","There's more to go level 8.","Sweet!, 9!","You are on you way to the top, level 10.","Sweet!, 11!","Congrats!, level 12.","You have ascended to level 13.","Merchant level 14.","Merchant level 15.","You made it to 16!","Here's a cake, level 17!","You made it to 18!","There's more to go level 19.","Excitement is to be had, 20!"});
        resources["resource_4"].SetThresholdEventBools(new List<bool>(){true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true});
        resources["resource_4"].SetThresholdCaps(new List<ulong>(){0,35,47,58,70,81,93,104,116,127,139,150,162,173,185,196,208,219,231,242,254,265,277,288,300,311,323,334,346,357,369,380,392,403,415,426,438,449,461,472,484,495,507,518,530,541,553,564,576,587,599,610,622,633,645,656,668,679,691,702,714,725,737,748,760});
        resources["resource_4"].SetThresholdCapCostCoins(new List<ulong>(){6018,1225,1475,1775,2137,2572,3096,3726,4484,5398,6497,7820,9412,11328,13635,16411,19753,23775,28616,34443,41456,49897,60057,72286,87006,104722,126045,151711,182602,219784,264536,318401,383235,461269,555193,668242,804311,968085,1165208,1402468,1688040,2031761,2445470,2943419,3542761,4264142,5132410,6177477,7435341,8949332,10771604,12964929,15604861,18782338,22606817,27210040,32750575,39419280,47445873,57106849,68735003,82730893,99576640,119852535});
        resources["resource_4"].SetThresholdCapCostGems(new List<ulong>(){11,2,2,2,2,3,3,3,4,4,5,5,6,7,8,9,10,12,13,15,17,19,22,25,28,32,36,41,47,53,60,69,78,88,100,114,129,147,166,189,214,243,276,313,356,404,458,520,590,670,761,863,980,1112,1263,1433,1627,1846,2096,2378,2700,3064,3478,3948});

        resources["resource_5"].SetThresholdBool(true);
        resources["resource_5"].SetThresholdKeys(new List<int>(){2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20});
        //resources["resource_5"].SetThresholdIncrementsCoins(new List<ulong>(){300, 600, 1200, 3000, 6600, 8400, 24000, 36000, 150000, 450000, 900000, 1500000, 3000000, 6000000, 12000000, 30000000, 90000000, 150000000, 300000000});
        resources["resource_5"].SetThresholdIncrementsCoins(new List<ulong>(){3000, 6000, 12000, 30000, 66000, 84000, 240000, 360000, 1500000, 4500000, 9000000, 15000000, 30000000, 60000000, 120000000, 300000000, 900000000, 1500000000, 3000000000});
        resources["resource_5"].SetThresholdIncrementsGems(new List<ulong>(){6, 12, 18, 30, 60, 69, 90, 105, 120, 150, 180, 210, 240, 270, 300, 300, 300, 300, 300});
        resources["resource_5"].SetThresholdValues(new List<ulong>(){3000, 12000, 36000, 179000, 600000, 920000, 4200000, 9550000, 59500000, 210000000, 660000000, 1560000000, 3360000000, 9000000000, 19200000000, 51000000000, 162000000000, 285000000000, 600000000000});
        resources["resource_5"].SetThresholdRates(new List<float>(){80f, 60f, 48f, 40f, 34.29f, 30f, 26.67f, 24f, 21.82f, 20f, 18.46f, 17.14f, 16f, 15f, 14.12f, 13.33f, 12.63f, 12f, 11.43f, 10.91f});
        resources["resource_5"].SetThresholdTypes(new List<string>(){"level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level"});
        resources["resource_5"].SetThresholdDescriptions(new List<string>(){"Excitement is to be had, level 2!","Niceness, 3!!!","There's more to go level 4.","You have ascended to level 5.","You are on you way to the top, level 6.","Cool beans, level 7.","There's more to go level 8.","Sweet!, 9!","You are on you way to the top, level 10.","Sweet!, 11!","Congrats!, level 12.","You have ascended to level 13.","Merchant level 14.","Merchant level 15.","You made it to 16!","Here's a cake, level 17!","You made it to 18!","There's more to go level 19.","Excitement is to be had, 20!"});
        resources["resource_5"].SetThresholdEventBools(new List<bool>(){true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true});
        resources["resource_5"].SetThresholdCaps(new List<ulong>(){0,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,96,101,106,111,116,121,126,131,136,141,146,151,156,161,166,171,176,181,186,191,196,201,206,211,216,221,226,231,236,241,246,251,257,262,267,272,277,282,287,292,297,302,307,312,317,322,327,332});
        resources["resource_5"].SetThresholdCapCostCoins(new List<ulong>(){59070,10715,12659,14955,17668,20873,24659,29132,34417,40660,48036,56749,67044,79205,93573,110547,130600,154290,182278,215343,254406,300555,355075,419484,495578,585475,691679,817148,965377,1140495,1347378,1591790,1880537,2221663,2624668,3100777,3663252,4327759,5112806,6040258,7135949,8430396,9959653,11766314,13900700,16422259,19401224,22920568,27078313,31990265,37793235,44648852,52748064,62316458,73620538,86975157,102752276,121391334,143411479,169426034,200159578,236468125,279362970,330038854});
        resources["resource_5"].SetThresholdCapCostGems(new List<ulong>(){221,23,25,28,31,34,37,41,45,50,55,61,67,74,81,90,99,109,121,133,147,162,179,197,217,240,264,292,322,355,392,432,477,526,580,640,706,778,859,947,1045,1153,1271,1402,1547,1707,1883,2077,2291,2527,2788,3075,3392,3742,4128,4553,5023,5541,6112,6742,7437,8204,9050,9983});

        resources["resource_6"].SetThresholdBool(true);
        resources["resource_6"].SetThresholdKeys(new List<int>(){2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20});
        //resources["resource_6"].SetThresholdIncrementsCoins(new List<ulong>(){300, 600, 1200, 3000, 6600, 8400, 24000, 36000, 150000, 450000, 900000, 1500000, 3000000, 6000000, 12000000, 30000000, 90000000, 150000000, 300000000});
        resources["resource_6"].SetThresholdIncrementsCoins(new List<ulong>(){3000, 6000, 12000, 30000, 66000, 84000, 240000, 360000, 1500000, 4500000, 9000000, 15000000, 30000000, 60000000, 120000000, 300000000, 900000000, 1500000000, 3000000000});
        resources["resource_6"].SetThresholdIncrementsGems(new List<ulong>(){6, 12, 18, 30, 60, 69, 90, 105, 120, 150, 180, 210, 240, 270, 300, 300, 300, 300, 300});
        resources["resource_6"].SetThresholdValues(new List<ulong>(){3000, 12000, 36000, 179000, 600000, 920000, 4200000, 9550000, 59500000, 210000000, 660000000, 1560000000, 3360000000, 9000000000, 19200000000, 51000000000, 162000000000, 285000000000, 600000000000});
        resources["resource_6"].SetThresholdRates(new List<float>(){80f, 60f, 48f, 40f, 34.29f, 30f, 26.67f, 24f, 21.82f, 20f, 18.46f, 17.14f, 16f, 15f, 14.12f, 13.33f, 12.63f, 12f, 11.43f, 10.91f});
        resources["resource_6"].SetThresholdTypes(new List<string>(){"level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level"});
        resources["resource_6"].SetThresholdDescriptions(new List<string>(){"Excitement is to be had, level 2!","Niceness, 3!!!","There's more to go level 4.","You have ascended to level 5.","You are on you way to the top, level 6.","Cool beans, level 7.","There's more to go level 8.","Sweet!, 9!","You are on you way to the top, level 10.","Sweet!, 11!","Congrats!, level 12.","You have ascended to level 13.","Merchant level 14.","Merchant level 15.","You made it to 16!","Here's a cake, level 17!","You made it to 18!","There's more to go level 19.","Excitement is to be had, 20!"});
        resources["resource_6"].SetThresholdEventBools(new List<bool>(){true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true});
        resources["resource_6"].SetThresholdCaps(new List<ulong>(){0,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,96,101,106,111,116,121,126,131,136,141,146,151,156,161,166,171,176,181,186,191,196,201,206,211,216,221,226,231,236,241,246,251,257,262,267,272,277,282,287,292,297,302,307,312,317,322,327,332});
        resources["resource_6"].SetThresholdCapCostCoins(new List<ulong>(){59070,10715,12659,14955,17668,20873,24659,29132,34417,40660,48036,56749,67044,79205,93573,110547,130600,154290,182278,215343,254406,300555,355075,419484,495578,585475,691679,817148,965377,1140495,1347378,1591790,1880537,2221663,2624668,3100777,3663252,4327759,5112806,6040258,7135949,8430396,9959653,11766314,13900700,16422259,19401224,22920568,27078313,31990265,37793235,44648852,52748064,62316458,73620538,86975157,102752276,121391334,143411479,169426034,200159578,236468125,279362970,330038854});
        resources["resource_6"].SetThresholdCapCostGems(new List<ulong>(){221,23,25,28,31,34,37,41,45,50,55,61,67,74,81,90,99,109,121,133,147,162,179,197,217,240,264,292,322,355,392,432,477,526,580,640,706,778,859,947,1045,1153,1271,1402,1547,1707,1883,2077,2291,2527,2788,3075,3392,3742,4128,4553,5023,5541,6112,6742,7437,8204,9050,9983});

        resources["resource_7"].SetThresholdBool(true);
        resources["resource_7"].SetThresholdKeys(new List<int>(){2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20});
        //resources["resource_7"].SetThresholdIncrementsCoins(new List<ulong>(){300, 600, 1200, 3000, 6600, 8400, 24000, 36000, 150000, 450000, 900000, 1500000, 3000000, 6000000, 12000000, 30000000, 90000000, 150000000, 300000000});
        resources["resource_7"].SetThresholdIncrementsCoins(new List<ulong>(){3000, 6000, 12000, 30000, 66000, 84000, 240000, 360000, 1500000, 4500000, 9000000, 15000000, 30000000, 60000000, 120000000, 300000000, 900000000, 1500000000, 3000000000});
        resources["resource_7"].SetThresholdIncrementsGems(new List<ulong>(){6, 12, 18, 30, 60, 69, 90, 105, 120, 150, 180, 210, 240, 270, 300, 300, 300, 300, 300});
        resources["resource_7"].SetThresholdValues(new List<ulong>(){3000, 12000, 36000, 179000, 600000, 920000, 4200000, 9550000, 59500000, 210000000, 660000000, 1560000000, 3360000000, 9000000000, 19200000000, 51000000000, 162000000000, 285000000000, 600000000000});
        resources["resource_7"].SetThresholdRates(new List<float>(){80f, 60f, 48f, 40f, 34.29f, 30f, 26.67f, 24f, 21.82f, 20f, 18.46f, 17.14f, 16f, 15f, 14.12f, 13.33f, 12.63f, 12f, 11.43f, 10.91f});
        resources["resource_7"].SetThresholdTypes(new List<string>(){"level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level"});
        resources["resource_7"].SetThresholdDescriptions(new List<string>(){"Excitement is to be had, level 2!","Niceness, 3!!!","There's more to go level 4.","You have ascended to level 5.","You are on you way to the top, level 6.","Cool beans, level 7.","There's more to go level 8.","Sweet!, 9!","You are on you way to the top, level 10.","Sweet!, 11!","Congrats!, level 12.","You have ascended to level 13.","Merchant level 14.","Merchant level 15.","You made it to 16!","Here's a cake, level 17!","You made it to 18!","There's more to go level 19.","Excitement is to be had, 20!"});
        resources["resource_7"].SetThresholdEventBools(new List<bool>(){true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true});
        resources["resource_7"].SetThresholdCaps(new List<ulong>(){0,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,96,101,106,111,116,121,126,131,136,141,146,151,156,161,166,171,176,181,186,191,196,201,206,211,216,221,226,231,236,241,246,251,257,262,267,272,277,282,287,292,297,302,307,312,317,322,327,332});
        resources["resource_7"].SetThresholdCapCostCoins(new List<ulong>(){59070,10715,12659,14955,17668,20873,24659,29132,34417,40660,48036,56749,67044,79205,93573,110547,130600,154290,182278,215343,254406,300555,355075,419484,495578,585475,691679,817148,965377,1140495,1347378,1591790,1880537,2221663,2624668,3100777,3663252,4327759,5112806,6040258,7135949,8430396,9959653,11766314,13900700,16422259,19401224,22920568,27078313,31990265,37793235,44648852,52748064,62316458,73620538,86975157,102752276,121391334,143411479,169426034,200159578,236468125,279362970,330038854});
        resources["resource_7"].SetThresholdCapCostGems(new List<ulong>(){221,23,25,28,31,34,37,41,45,50,55,61,67,74,81,90,99,109,121,133,147,162,179,197,217,240,264,292,322,355,392,432,477,526,580,640,706,778,859,947,1045,1153,1271,1402,1547,1707,1883,2077,2291,2527,2788,3075,3392,3742,4128,4553,5023,5541,6112,6742,7437,8204,9050,9983});

        resources["resource_8"].SetThresholdBool(true);
        resources["resource_8"].SetThresholdKeys(new List<int>(){2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20});
        //resources["resource_8"].SetThresholdIncrementsCoins(new List<ulong>(){300, 600, 1200, 3000, 6600, 8400, 24000, 36000, 150000, 450000, 900000, 1500000, 3000000, 6000000, 12000000, 30000000, 90000000, 150000000, 300000000});
        resources["resource_8"].SetThresholdIncrementsCoins(new List<ulong>(){3000, 6000, 12000, 30000, 66000, 84000, 240000, 360000, 1500000, 4500000, 9000000, 15000000, 30000000, 60000000, 120000000, 300000000, 900000000, 1500000000, 3000000000});
        resources["resource_8"].SetThresholdIncrementsGems(new List<ulong>(){6, 12, 18, 30, 60, 69, 90, 105, 120, 150, 180, 210, 240, 270, 300, 300, 300, 300, 300});
        resources["resource_8"].SetThresholdValues(new List<ulong>(){3000, 12000, 36000, 179000, 600000, 920000, 4200000, 9550000, 59500000, 210000000, 660000000, 1560000000, 3360000000, 9000000000, 19200000000, 51000000000, 162000000000, 285000000000, 600000000000});
        resources["resource_8"].SetThresholdRates(new List<float>(){80f, 60f, 48f, 40f, 34.29f, 30f, 26.67f, 24f, 21.82f, 20f, 18.46f, 17.14f, 16f, 15f, 14.12f, 13.33f, 12.63f, 12f, 11.43f, 10.91f});
        resources["resource_8"].SetThresholdTypes(new List<string>(){"level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level", "level"});
        resources["resource_8"].SetThresholdDescriptions(new List<string>(){"Excitement is to be had, level 2!","Niceness, 3!!!","There's more to go level 4.","You have ascended to level 5.","You are on you way to the top, level 6.","Cool beans, level 7.","There's more to go level 8.","Sweet!, 9!","You are on you way to the top, level 10.","Sweet!, 11!","Congrats!, level 12.","You have ascended to level 13.","Merchant level 14.","Merchant level 15.","You made it to 16!","Here's a cake, level 17!","You made it to 18!","There's more to go level 19.","Excitement is to be had, 20!"});
        resources["resource_8"].SetThresholdEventBools(new List<bool>(){true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true});
        resources["resource_8"].SetThresholdCaps(new List<ulong>(){0,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,96,101,106,111,116,121,126,131,136,141,146,151,156,161,166,171,176,181,186,191,196,201,206,211,216,221,226,231,236,241,246,251,257,262,267,272,277,282,287,292,297,302,307,312,317,322,327,332});
        resources["resource_8"].SetThresholdCapCostCoins(new List<ulong>(){59070,10715,12659,14955,17668,20873,24659,29132,34417,40660,48036,56749,67044,79205,93573,110547,130600,154290,182278,215343,254406,300555,355075,419484,495578,585475,691679,817148,965377,1140495,1347378,1591790,1880537,2221663,2624668,3100777,3663252,4327759,5112806,6040258,7135949,8430396,9959653,11766314,13900700,16422259,19401224,22920568,27078313,31990265,37793235,44648852,52748064,62316458,73620538,86975157,102752276,121391334,143411479,169426034,200159578,236468125,279362970,330038854});
        resources["resource_8"].SetThresholdCapCostGems(new List<ulong>(){221,23,25,28,31,34,37,41,45,50,55,61,67,74,81,90,99,109,121,133,147,162,179,197,217,240,264,292,322,355,392,432,477,526,580,640,706,778,859,947,1045,1153,1271,1402,1547,1707,1883,2077,2291,2527,2788,3075,3392,3742,4128,4553,5023,5541,6112,6742,7437,8204,9050,9983});
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
            // Everything is available to craft and only takes 2 seconds.
            foreach(var kvp in inventory)
            {
                kvp.Value.SetIsAvailable(true);
                kvp.Value.timeCrafting = 2;
            }
        }
        else
        {
            // Start off with only basic iron items
            //inventory["axe_1"].SetIsAvailable(true); inventory["axe_1"].SetStock(0);
            inventory["dagger_1"].SetIsAvailable(true); inventory["dagger_1"].SetStock(0);
            //inventory["spear_1"].SetIsAvailable(true); inventory["spear_1"].SetStock(1);
            //inventory["bow_1"].SetIsAvailable(true); inventory["bow_1"].SetStock(1);
            inventory["harmor_1"].SetIsAvailable(true); inventory["harmor_1"].SetStock(0);
            //inventory["larmor_1"].SetIsAvailable(true); inventory["larmor_1"].SetStock(1);
            inventory["helmet_1"].SetIsAvailable(true); inventory["helmet_1"].SetStock(0);
            //inventory["headgear_1"].SetIsAvailable(true); inventory["headgear_1"].SetStock(0);
            //inventory["hat_1"].SetIsAvailable(true); inventory["hat_1"].SetStock(1);
            inventory["gauntlets_1"].SetIsAvailable(true); inventory["gauntlets_1"].SetStock(0);
            inventory["boots_1"].SetIsAvailable(true); inventory["boots_1"].SetStock(0);
            //inventory["shield_1"].SetIsAvailable(true); inventory["shield_1"].SetStock(0);
            //inventory["potion_1"].SetIsAvailable(true); inventory["potion_1"].SetStock(0);
        }

        // For the tutorial
        inventory["spear_1"].SetStock(1);
        inventory["bow_1"].SetStock(1);
        inventory["larmor_1"].SetStock(1);
        inventory["helmet_1"].SetStock(1);

        // Make every item craft time 10s to test upgrade time reduction
        foreach(var kvp in inventory)
        {
            kvp.Value.timeCrafting = 1;
        }

    }

    public Item RandomItem(bool isStocked)
    {
        // Returns a random item from the inventory.
        // Will make this more elaborate later (only return things in stock or mixed, etc.).

        List<string> keyList = new List<string>(); // This list contains Items that will be sampled from

        // Iterate through dictionary
        foreach(KeyValuePair<string, Item> entry in inventory)
        {
            if(isStocked) // Could've purchased an item that you still can't make yet
            {
                if(entry.Value.GetStock() > 0){ keyList.Add(entry.Key); }
            }
            else
            {
                if(entry.Value.GetIsAvailable()){ keyList.Add(entry.Key); }
            }
        }
        if(keyList.Count == 0) // If there's nothing in stock, then randomly pick from what can be made
        {
            foreach(KeyValuePair<string, Item> entry in inventory)
            {
                if(entry.Value.GetIsAvailable()){ keyList.Add(entry.Key); }
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
            if(entry.Value.GetRate() > 0) // 0 will mean that the resource does not regenerate (e.g. components)
            {
                entry.Value.CheckTimeRemaining();
            }
            
        }
    }

    public List<Item> CheckItemsAvailable(string itemCategory)
    {
        // Returns a vector of all items available for a specific item class
        List<Item> itemsAvailable = new List<Item>();

        foreach(KeyValuePair<string, Item> entry in inventory)
        {
            if((entry.Value.category2 == itemCategory) && (entry.Value.GetIsAvailable())){
                //Debug.Log("Available " + itemCategory + ": " + entry.Value.name);
                itemsAvailable.Add(entry.Value);
            }
        }
        return(itemsAvailable);
    }

}


