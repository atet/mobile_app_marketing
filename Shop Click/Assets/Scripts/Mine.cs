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

    [SerializeField] public GameObject panelMineCraftQueue;
    private void CraftingItem(string itemName){
        // Different from CraftItem(), this waits a certain amount of time before CraftItem()
        if(
            Global.instance.GetInventory()[itemName].CheckResource() && 
            CraftingItemSlotOpen()
          )
        {
            Debug.Log("CraftingItem(" + itemName + ")");

            // Subtract resources.
            Global.instance.GetInventory()[itemName].AcquireResources();

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
                    Global.instance.GetInventory()[itemName].CraftItem();
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
            Resources.Load<Sprite>(Global.instance.GetInventory()[itemName].filepathImage);
            currentSlot.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Image>().SetNativeSize();

            // Set countdown to Item's craft time.
            countdownQueue[currentIndex] = Global.instance.GetInventory()[itemName].timeCrafting;

            // Set text on slot to reflect countdown time.
            CraftingItemUpdateTime(currentIndex);

            // Add item to Recent (ly crafted items list)
            Debug.Log("Adding item to Recent");
            PopulateCraftRecentWindow(itemName);

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
                Button currentButton = panelsCraft[i].transform.GetChild(0).gameObject.GetComponent<Button>();
                Item currentItem = items[i];

                // Name of item
                currentButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentItem.name;

                // Value
                currentButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + currentItem.value.ToString("N0");

                // Level (TODO: Have a tier number somewhere and a level/count how many have been made over lifetime)
                currentButton.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = currentItem.tier.ToString();

                // Stock
                currentButton.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = currentItem.GetStock().ToString("N0");

                // Crafting time
                currentButton.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = Helper.TimeFormatter(currentItem.timeCrafting);

                // Image
                currentButton.transform.GetChild(9).GetComponent<Image>().sprite = Resources.Load<Sprite>(currentItem.filepathImage);
                currentButton.transform.GetChild(9).GetComponent<Image>().SetNativeSize();

                // Required resources
                GameObject currentPanelResource = currentButton.transform.GetChild(10).gameObject;
                // Inactivate all panels first
                for(int j = 0; j < 6; j++){ currentPanelResource.transform.GetChild(j).gameObject.SetActive(false); }

                SetPanelResources(currentItem, currentPanelResource);

                // Link button to function to craft said item.
                currentButton.onClick.RemoveAllListeners(); // Remove any previous listeners.
                currentButton.onClick.AddListener(delegate { CraftingItem(currentItem.name); });
                
                // Show the button
                panelsCraft[i].SetActive(true);
            }

            for(int i = items.Count; i < maxIndex; i++)
            {
                Debug.Log("Blank index =" + i);
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
    public void PopulateCraftRecentWindow(string itemNameRecent)
    {
        // This function will get called everytime an item is crafted

        // Check if item is already in the Dictionary
        if(itemsRecent.Count > 0)
        {
            for(int i = 0; i < itemsRecent.Count; i++)
            {
                if(itemsRecent[i].name == itemNameRecent)
                {
                    Debug.Log("Already have a " + itemNameRecent + " in Recent list.");
                    itemsRecent.RemoveAt(i);
                    break;
                }
            }
        }

        // Add Item to itemsRecent
        itemsRecent.Add(Global.instance.GetInventory()[itemNameRecent]);

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
    private Dictionary<string, Item> itemsFavorites = new Dictionary<string, Item>();




    // Start is called before the first frame update
    void Start()
    {
        countdownQueue = new float[9];
        for (int i = 0; i < countdownQueue.Length; i++){ countdownQueue[i] = -999f; }

        LinkButtonsCategory();

        LinkButtonCraftRecent();
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
