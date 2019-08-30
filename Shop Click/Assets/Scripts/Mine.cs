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
            currentSlot.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { 
                if(Global.instance.GetStats()["Stock"].CheckCapacity())
                {
                    Global.instance.GetInventory()[itemID].CraftItem();
                    currentSlot.SetActive(false);
                }
                else
                {
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
    private void CraftingItemUpdateTimeAll()
    {
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
                panelMineCraftQueue.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Helper.TimeFormatter(countdownQueue[index]);
            }
            else
            {
                // Countdown done.
                // Enable button.
                panelMineCraftQueue.transform.GetChild(index).gameObject.transform.GetChild(0).GetComponent<Button>().interactable = true;

                // Set countdown to -999 to denote this slot is no longer being used
                countdownQueue[index] = -999;

                // Change button text to "Finished"
                panelMineCraftQueue.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Finished";
            }

        }
    }

    [SerializeField] public GameObject panelCraftWindow;
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


    [SerializeField] public Button buttonDagger, buttonStaff, buttonBow, buttonMace, buttonAxe, buttonSpear, buttonSword, buttonWand, buttonCrossbow, buttonGun;
    [SerializeField] public Button buttonShield, buttonShoes, buttonBoots, buttonGloves, buttonGauntlets, buttonHeadgear, buttonHat, buttonHelmet, buttonClothing, buttonLightArmor, buttonHeavyArmor;
    [SerializeField] public Button buttonRing, buttonAmulet, buttonRunestone, buttonEnchantment, buttonSpirit, buttonMedicine, buttonPotion, buttonMagic;
    public void LinkButtonsCategory()
    {
        // Setup onClick events through code
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
                if(itemsRecent[i].name == itemIDRecent)
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
        }
        else
        {
            //Debug.Log("Setting back to red");
            currentItem.SetIsFavorite(true);
            imageFilepath = "Images/UI/heart_red_transparent";
            PopulateCraftFavoritesWindow(currentItem);
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
        if(item.costIron > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UI/iron");
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costIron.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costHide > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UI/hide");
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costHide.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costWood > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UI/wood");
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costWood.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costHerbs > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UI/herbs");
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costHerbs.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costSteel > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UI/steel");
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costSteel.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costOil > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UI/oil");
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costOil.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costElectricity > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UI/electricity");
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costElectricity.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
        if(item.costTitanium > 0)
        {
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UI/titanium");
            panelResource.transform.GetChild(currentIndexPanelResources).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.costTitanium.ToString();
            // Set panel active
            panelResource.transform.GetChild(currentIndexPanelResources).gameObject.SetActive(true);
            currentIndexPanelResources++;
        }
    }
}
