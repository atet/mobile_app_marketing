using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mine : MonoBehaviour
{
    [SerializeField] public Button buttonStock;
    public void UpdateButtonStock()
    {
        buttonStock.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Global.instance.GetStats()["Stock"].GetAmount().ToString();
        buttonStock.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Global.instance.GetStats()["Stock"].GetCap().ToString();
    }
    [SerializeField] public GameObject panelStockDetailVLG;
    [SerializeField] public GameObject prefabStockDetailVLGElement;

    public void LinkButtonStock()
    {
        // Setup onClick events through code
        buttonStock.onClick.AddListener(delegate { PopulateStockDetail(); });
    }
    public void PopulateStockDetail()
    {
        DepopulateStockDetail();

        foreach(var kvp in Global.instance.GetInventory())
        {
            if(kvp.Value.GetStock() > 0)
            {
                UnityEngine.Object prefab = Resources.Load("PreFabs/Panel Mine Stock Detail VLG Element"); // Don't add file extension
                GameObject child = (GameObject)Instantiate(prefab, panelStockDetailVLG.transform);
                // Now to access that child
                child.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = kvp.Value.name;
                child.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(kvp.Value.filepathImage);
                child.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "x" + kvp.Value.GetStock().ToString();

                // Trash item
                child.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate
                {
                    SFX.instance.PlaySFXTrash();
                    kvp.Value.RemoveStock(1);
                    PopulateStockDetail();
                });
            }    
        }
    }
    public void DepopulateStockDetail()
    {
        foreach (Transform child in panelStockDetailVLG.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }


    [SerializeField] public GameObject panelMineCraftQueue;
    private void CraftingItem(string itemID){
        // Different from CraftItem(), this waits a certain amount of time before CraftItem()
        if(
            Global.instance.GetInventory()[itemID].CheckResource() && 
            CraftingItemSlotOpen()
          )
        {
            Debug.Log("CraftingItem(" + itemID + ")");

            // Subtract resources.
            Global.instance.GetInventory()[itemID].AcquireResources();

            // Close panelCraftWindow.
            panelCraftWindow.SetActive(false);

            // Find the lowest index open slot.
            int currentIndex = CraftingItemSelectSlot();
            GameObject currentSlot = panelMineCraftQueue.transform.GetChild(currentIndex).gameObject;

            // Disable the panel's button for now, do this before making the panel active.
            currentSlot.transform.GetChild(0).GetComponent<Button>().interactable = false;

            // Link the slot/button to increment the item when time is over and button is clicked
            currentSlot.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            currentSlot.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
            { 
                if(Global.instance.GetStats()["Stock"].CheckCapacity())
                {
                    Global.instance.GetInventory()[itemID].CraftItem();
                    currentSlot.SetActive(false);

                    SFX.instance.PlaySFXStockItem();
                }
                else
                {
                    SFX.instance.PlaySFXNoGo();
                    Debug.Log("Stock at capacity!");
                }
            });

            // Make slot's panel active.
            currentSlot.SetActive(true);

            // Set image
            currentSlot.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = 
            Resources.Load<Sprite>(Global.instance.GetInventory()[itemID].filepathImage);
            currentSlot.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Image>().SetNativeSize();

            // Set countdown to Item's craft time.
            countdownQueue[currentIndex] = Global.instance.GetInventory()[itemID].timeCrafting;

            // Set text on slot to reflect countdown time.
            CraftingItemUpdateTime(currentIndex);

            // Add item to Recent (ly crafted items list)
            Debug.Log("Adding item to Recent");
            PopulateCraftRecentWindow(itemID);

            // Panel Craft Window is not directly related to Panel Craft Recent and Favorites.
            // When you close Panel Craft Window, the invisible Recent and Favorites close button still exists.
            panelCraftRecent.SetActive(false);
            panelCraftFavorites.SetActive(false);

            SFX.instance.PlaySFXCraftItem();
        }
        else
        {
            SFX.instance.PlaySFXNoGo();
        }
    }
    private bool CraftingItemSlotOpen()
    {
        if(CraftingItemSelectSlot() != -1)
        {
            return(true);
        }
        else
        {
            Debug.Log("No crafting slots open!");
            return(false);
        }
    }
    private int CraftingItemSelectSlot()
    {
        // TODO: Rearrage slots in descending time remaining order
        // Finds out which slots are free
        for(int i = 0; i < panelMineCraftQueue.transform.childCount; i++)
        {
            if(!panelMineCraftQueue.transform.GetChild(i).gameObject.activeSelf)
            {
                return(i);
            }
        }
        // else
        return(-1);
    }
    
    private float[] countdownQueue;
    private List<TextMeshProUGUI> tMProMineCraftQueue;
    private List<Button> buttonMineCraftQueue;
    public void LinkCraftQueue(){
        tMProMineCraftQueue = new List<TextMeshProUGUI>
        {
            panelMineCraftQueue.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>(),
            panelMineCraftQueue.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>(),
            panelMineCraftQueue.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>(),
            panelMineCraftQueue.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>(),
            panelMineCraftQueue.transform.GetChild(4).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>(),
            panelMineCraftQueue.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>(),
            panelMineCraftQueue.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>(),
            panelMineCraftQueue.transform.GetChild(7).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>(),
            panelMineCraftQueue.transform.GetChild(8).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>()
        };
        buttonMineCraftQueue = new List<Button>
        {
            panelMineCraftQueue.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Button>(),
            panelMineCraftQueue.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Button>(),
            panelMineCraftQueue.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Button>(),
            panelMineCraftQueue.transform.GetChild(3).gameObject.transform.GetChild(0).GetComponent<Button>(),
            panelMineCraftQueue.transform.GetChild(4).gameObject.transform.GetChild(0).GetComponent<Button>(),
            panelMineCraftQueue.transform.GetChild(5).gameObject.transform.GetChild(0).GetComponent<Button>(),
            panelMineCraftQueue.transform.GetChild(6).gameObject.transform.GetChild(0).GetComponent<Button>(),
            panelMineCraftQueue.transform.GetChild(7).gameObject.transform.GetChild(0).GetComponent<Button>(),
            panelMineCraftQueue.transform.GetChild(8).gameObject.transform.GetChild(0).GetComponent<Button>()
        };
    }
    private void CraftingItemUpdateTimeAll()
    {
        // There's only a finite amount of slots:
        // Will update time on all slots.
        for(int i = 0; i < panelMineCraftQueue.transform.childCount; i++)
        {
            CraftingItemUpdateTime(i);
        }
    }
    private void CraftingItemUpdateTime(int index)
    {
        if(countdownQueue[index] != -999)
        {
            if(countdownQueue[index] > 0)
            {
                countdownQueue[index] -= Time.deltaTime;
                //panelMineCraftQueue.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Helper.TimeFormatter(countdownQueue[index]);
                tMProMineCraftQueue[index].text = Helper.TimeFormatter(countdownQueue[index]);
            }
            else
            {
                // Countdown done.
                // Enable button.
                //panelMineCraftQueue.transform.GetChild(index).gameObject.transform.GetChild(0).GetComponent<Button>().interactable = true;
                buttonMineCraftQueue[index].interactable = true;
                // Set countdown to -999 to denote this slot is no longer being used
                countdownQueue[index] = -999;

                // Change button text to "Finished"
                //panelMineCraftQueue.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Finished";
                tMProMineCraftQueue[index].text = "Finished";
                SFX.instance.PlaySFXDone();
            }

        }
    }

    [SerializeField] public GameObject panelCraftWindow, panelCraftRecent, panelCraftFavorites;
    [SerializeField] public List<GameObject> panelsCraft;
    public void OnClickCraftWindow(List<Item> items)
    {
        int maxIndex = panelsCraft.Count; // Currently max of seven menu items
        // Only open menu if there's actually available items
        if(items.Count > 0)
        {
            for(int i = 0; i < items.Count; i++)
            {
                Button currentButtonCraft = panelsCraft[i].transform.GetChild(0).gameObject.GetComponent<Button>();
                Button currentButtonFavorites = panelsCraft[i].transform.GetChild(1).gameObject.GetComponent<Button>();
                Item currentItem = items[i];

                // Name of item
                currentButtonCraft.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentItem.name;

                // Value
                currentButtonCraft.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + currentItem.value.ToString("N0");

                // Level (TODO: Have a tier number somewhere and a level/count how many have been made over lifetime)
                currentButtonCraft.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = currentItem.tier.ToString();

                // Stock
                currentButtonCraft.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = currentItem.GetStock().ToString("N0");

                // Crafting time
                currentButtonCraft.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = Helper.TimeFormatter(currentItem.timeCrafting);

                // Image
                currentButtonCraft.transform.GetChild(9).GetComponent<Image>().sprite = Resources.Load<Sprite>(currentItem.filepathImage);
                currentButtonCraft.transform.GetChild(9).GetComponent<Image>().SetNativeSize();

                // Favorites
                SetImageCraftFavorites(currentButtonFavorites, currentItem); // Default appropriate image on creation.
                currentButtonFavorites.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                currentButtonFavorites.gameObject.GetComponent<Button>().onClick.AddListener(
                delegate
                {
                    OnClickButtonCraftFavorites(currentButtonFavorites, currentItem);
                });

                // Required resources
                GameObject currentPanelResource = currentButtonCraft.transform.GetChild(10).gameObject;
                // Inactivate all panels first
                for(int j = 0; j < 6; j++){ currentPanelResource.transform.GetChild(j).gameObject.SetActive(false); }

                SetPanelResources(currentItem, currentPanelResource);

                // Link button to function to craft said item.
                currentButtonCraft.onClick.RemoveAllListeners(); // Remove any previous listeners.
                currentButtonCraft.onClick.AddListener(delegate { CraftingItem(currentItem.id); });
                
                // Show the button
                panelsCraft[i].SetActive(true);
            }

            for(int i = items.Count; i < maxIndex; i++)
            {
                //Debug.Log("Blank index =" + i);
                // Deactivate panel/button after clicked, important when selecting a menu that has less items after a many that had more items
                panelsCraft[i].SetActive(false); 
            }
            // Show panel
            panelCraftWindow.SetActive(true);
        }
        else
        {
            panelCraftWindow.SetActive(false);
        }
    }


    [SerializeField] public Button buttonCraftMenu;
    [SerializeField] public Button buttonWeapon, buttonArmor, buttonAccessory;
    [SerializeField] public Button buttonDagger, buttonStaff, buttonBow, buttonMace, buttonAxe, buttonSpear, buttonSword, buttonWand, buttonCrossbow, buttonGun;
    [SerializeField] public Button buttonShield, buttonShoes, buttonBoots, buttonGloves, buttonGauntlets, buttonHeadgear, buttonHat, buttonHelmet, buttonClothing, buttonLightArmor, buttonHeavyArmor;
    [SerializeField] public Button buttonRing, buttonAmulet, buttonRunestone, buttonEnchantment, buttonSpirit, buttonMedicine, buttonPotion, buttonMagic;
    public void LinkButtonsCategory()
    {
        // Setup onClick events through code
        buttonCraftMenu.onClick.AddListener(delegate { OnClickButtonCraftMenu(); });

        buttonDagger.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Dagger")); });
        buttonStaff.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Staff")); });
        buttonBow.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Bow")); });
        buttonMace.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Mace")); });
        buttonAxe.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Axe")); });
        buttonSpear.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Spear")); });
        buttonSword.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Sword")); });
        buttonWand.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Wand")); });
        buttonCrossbow.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Crossbow")); });
        buttonGun.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Gun")); });

        buttonShield.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Shield")); });
        buttonShoes.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Shoes")); });
        buttonBoots.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Boots")); });
        buttonGloves.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Gloves")); });
        buttonGauntlets.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Gauntlets")); });
        buttonHeadgear.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Headgear")); });
        buttonHat.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Hat")); });
        buttonHelmet.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Helmet")); });
        buttonClothing.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Clothing")); });
        buttonLightArmor.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Light Armor")); });
        buttonHeavyArmor.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Heavy Armor")); });

        buttonRing.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Ring")); });
        buttonAmulet.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Amulet")); });
        buttonRunestone.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Runestone")); });
        buttonEnchantment.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Enchantment")); });
        buttonSpirit.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Spirit")); });
        buttonMedicine.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Medicine")); });
        buttonPotion.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Potion")); });
        buttonMagic.onClick.AddListener(delegate {OnClickCraftWindow(Global.instance.CheckItemsAvailable("Magic")); });
    
        // Start off all disabled
        buttonWeapon.interactable = buttonArmor.interactable = buttonAccessory.interactable =
        buttonDagger.interactable = buttonStaff.interactable = buttonBow.interactable = buttonMace.interactable = buttonAxe.interactable = buttonSpear.interactable = buttonSword.interactable = buttonWand.interactable = buttonCrossbow.interactable = buttonGun.interactable = 
        buttonShield.interactable = buttonShoes.interactable = buttonBoots.interactable = buttonGloves.interactable = buttonGauntlets.interactable = buttonHeadgear.interactable = buttonHat.interactable = buttonHelmet.interactable = buttonClothing.interactable = buttonLightArmor.interactable = buttonHeavyArmor.interactable = 
        buttonRing.interactable = buttonAmulet.interactable = buttonRunestone.interactable = buttonEnchantment.interactable = buttonSpirit.interactable = buttonMedicine.interactable = buttonPotion.interactable = buttonMagic.interactable =
        false;
    }

    public void OnClickButtonCraftMenu()
    {
        // Go through inventory and see what can be made
        foreach(var kvp in Global.instance.GetInventory())
        {
            CheckCategoryAvailable(buttonWeapon, kvp, "Weapon");
            CheckCategoryAvailable(buttonArmor, kvp, "Armor");
            CheckCategoryAvailable(buttonAccessory, kvp, "Accessory");
            CheckCategoryAvailable(buttonDagger, kvp, "Dagger");
            CheckCategoryAvailable(buttonStaff, kvp, "Staff");
            CheckCategoryAvailable(buttonBow, kvp, "Bow");
            CheckCategoryAvailable(buttonMace, kvp, "Mace");
            CheckCategoryAvailable(buttonAxe, kvp, "Axe");
            CheckCategoryAvailable(buttonSpear, kvp, "Spear");
            CheckCategoryAvailable(buttonSword, kvp, "Sword");
            CheckCategoryAvailable(buttonWand, kvp, "Wand");
            CheckCategoryAvailable(buttonCrossbow, kvp, "Crossbow");
            CheckCategoryAvailable(buttonGun, kvp, "Gun");
            CheckCategoryAvailable(buttonShield, kvp, "Shield");
            CheckCategoryAvailable(buttonShoes, kvp, "Shoes");
            CheckCategoryAvailable(buttonBoots, kvp, "Boots");
            CheckCategoryAvailable(buttonGloves, kvp, "Gloves");
            CheckCategoryAvailable(buttonGauntlets, kvp, "Gauntlets");
            CheckCategoryAvailable(buttonHeadgear, kvp, "Headgear");
            CheckCategoryAvailable(buttonHat, kvp, "Hat");
            CheckCategoryAvailable(buttonHelmet, kvp, "Helmet");
            CheckCategoryAvailable(buttonClothing, kvp, "Clothing");
            CheckCategoryAvailable(buttonLightArmor, kvp, "LightArmor");
            CheckCategoryAvailable(buttonHeavyArmor, kvp, "HeavyArmor");
            CheckCategoryAvailable(buttonRing, kvp, "Ring");
            CheckCategoryAvailable(buttonAmulet, kvp, "Amulet");
            CheckCategoryAvailable(buttonRunestone, kvp, "Runestone");
            CheckCategoryAvailable(buttonEnchantment, kvp, "Enchantment");
            CheckCategoryAvailable(buttonSpirit, kvp, "Spirit");
            CheckCategoryAvailable(buttonMedicine, kvp, "Medicine");
            CheckCategoryAvailable(buttonPotion, kvp, "Potion");
            CheckCategoryAvailable(buttonMagic, kvp, "Magic");
        }
    }
    public void CheckCategoryAvailable(Button button, KeyValuePair<string, Item> kvp, string category)
    {
        if(!button.IsInteractable()){
            if(kvp.Value.GetIsAvailable() && ((kvp.Value.category1 == category) || (kvp.Value.category2 == category)))
            { 
                button.interactable = true;
            }
        }
    }

    [SerializeField] public Button buttonCraftRecent;
    private List<Item> itemsRecent = new List<Item>();
    public void PopulateCraftRecentWindow(string itemIDRecent)
    {
        // This function will get called everytime an item is crafted

        // Check if item is already in the List
        if(itemsRecent.Count > 0)
        {
            for(int i = 0; i < itemsRecent.Count; i++)
            {
                if(itemsRecent[i].id == itemIDRecent)
                {
                    Debug.Log("Already have a " + itemIDRecent + " in Recent list.");
                    itemsRecent.RemoveAt(i);
                    break;
                }
            }
        }

        // Add Item to itemsRecent
        itemsRecent.Add(Global.instance.GetInventory()[itemIDRecent]);

        // Remove oldest item if list gets over panelsCraft.Count, currently max of 7 slots
        if(itemsRecent.Count > panelsCraft.Count)
        {
            Debug.Log("Removing the oldest item.");
            itemsRecent.RemoveAt(0);
        }
    }
    public void LinkButtonCraftRecent()
    {
        // Setup onClick events through code
        buttonCraftRecent.onClick.AddListener(delegate { OnClickCraftWindow(itemsRecent); });
    }


    [SerializeField] public Button buttonCraftFavorites;
    private List<Item> itemsFavorites = new List<Item>();
    private void OnClickButtonCraftFavorites(Button currentButton, Item currentItem){
        string imageFilepath;
        //Debug.Log("Currently favorite bool = " + currentItem.GetIsFavorite());
        if(currentItem.GetIsFavorite())
        {
            //Debug.Log("Setting back to black");
            currentItem.SetIsFavorite(false);
            imageFilepath = "Images/UI/heart_black_transparent";
            DepopulateCraftFavoritesWindow(currentItem);
            SFX.instance.PlaySFXUnfavorite();
        }
        else
        {
            //Debug.Log("Setting back to red");
            currentItem.SetIsFavorite(true);
            imageFilepath = "Images/UI/heart_red_transparent";
            PopulateCraftFavoritesWindow(currentItem);
            SFX.instance.PlaySFXFavorite();
        }
        SetImageCraftFavorites(currentButton, imageFilepath);
    }
    public void SetImageCraftFavorites(Button currentButton, string imageFilepath)
    {
        currentButton.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(imageFilepath);
        //currentButton.transform.GetChild(0).GetComponent<Image>().SetNativeSize(); // Don't do this
    }
    public void SetImageCraftFavorites(Button currentButton, Item currentItem)
    {
        string imageFilepath;
        if(currentItem.GetIsFavorite())
        {
            imageFilepath = "Images/UI/heart_red_transparent";
        }
        else
        {
            imageFilepath = "Images/UI/heart_black_transparent";
        }
        SetImageCraftFavorites(currentButton, imageFilepath);
    }
    public void PopulateCraftFavoritesWindow(Item itemFavorite)
    {
        // This function will get called everytime an item is added to favorites

        // Add Item to itemsFavorites
        itemsFavorites.Add(itemFavorite);

        // Remove oldest item if list gets over panelsCraft.Count, currently max of 7 slots
        if(itemsFavorites.Count > panelsCraft.Count)
        {
            Debug.Log("Removing the oldest item.");
            itemsFavorites[0].SetIsFavorite(false);
            itemsFavorites.RemoveAt(0);
        }
    }
    public void DepopulateCraftFavoritesWindow(Item itemFavorite)
    {
        string itemNameFavorite = itemFavorite.name;
        // This function will get called everytime an item is removed from favorites
        // Check if item is already in the List
        if(itemsFavorites.Count > 0)
        {
            for(int i = 0; i < itemsFavorites.Count; i++)
            {
                if(itemsFavorites[i].name == itemNameFavorite)
                {
                    itemsFavorites.RemoveAt(i);
                    break;
                }
            }
        }
    }
    public void LinkButtonCraftFavorites()
    {
        // Setup onClick events through code
        buttonCraftFavorites.onClick.AddListener(delegate { OnClickCraftWindow(itemsFavorites); });
    }




    // Start is called before the first frame update
    void Start()
    {
        countdownQueue = new float[9];
        for (int i = 0; i < countdownQueue.Length; i++){ countdownQueue[i] = -999f; }

        LinkButtonsCategory();

        LinkButtonStock();
        LinkButtonCraftRecent();
        LinkButtonCraftFavorites();
        LinkCraftQueue();

        //TestInstantiate();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateButtonStock();
        CraftingItemUpdateTimeAll();
    }


    // Helpers
    public void SetPanelResources(Item item, GameObject panelResource)
    {
        int currentIndexPanelResources = 0;
        if(item.costResource1 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_1"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costResource1.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costResource2 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_2"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costResource2.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costResource3 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_3"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costResource3.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costResource4 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_4"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costResource4.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costResource5 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_5"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costResource5.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costResource6 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_6"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costResource6.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costResource7 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_7"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costResource7.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costResource8 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_8"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costResource8.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent1 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_1"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent1.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent2 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_2"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent2.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent3 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_3"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent3.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent4 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_4"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent4.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent5 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_5"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent5.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent6 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_6"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent6.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent7 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_7"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent7.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent8 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_8"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent8.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent9 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_9"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent9.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent10 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_10"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent10.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent11 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_11"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent11.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent12 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_12"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent12.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent13 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_13"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent13.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent14 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_14"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent14.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent15 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_15"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent15.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costComponent16 > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_16"].GetFilepathImage());
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costComponent16.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
    }
}
