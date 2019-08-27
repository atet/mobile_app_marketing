using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mine : MonoBehaviour
{

    [SerializeField] public GameObject panelMineCraftQueue;
    
    private float[] countdownQueue;

    private void CraftingItem(string itemName){
        // Different from CraftItem(), this waits a certain amount of time before CraftItem()
        if(CraftingItemSlotOpen()){
            // Subtract resources.
            Global.instance.GetInventory()[itemName].AcquireResources();

            // Close panelCraftWindow.
            panelCraftWindow.SetActive(false);

            // Find the lowest index open slot.
            int currentIndex = CraftingItemSelectSlot();
            GameObject currentSlot = panelMineCraftQueue.transform.GetChild(currentIndex).gameObject;

            // Disable the panel's button for now, do this before making the panel active.
            currentSlot.transform.GetChild(0).GetComponent<Button>().interactable = false;

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
        }
    }

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

                countdownQueue[index] = -999;
                panelMineCraftQueue.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Done!";
            }

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

    [SerializeField] public GameObject panelCraftWindow;
    

    [SerializeField] public Button buttonDagger, buttonStaff, buttonBow, buttonMace, buttonAxe, buttonSpear, buttonSword, buttonWand, buttonCrossbow, buttonGun;
    [SerializeField] public Button buttonShield, buttonShoes, buttonBoots, buttonGloves, buttonGauntlets, buttonHeadgear, buttonHat, buttonHelmet, buttonClothing, buttonLightArmor, buttonHeavyArmor;
    [SerializeField] public Button buttonRing, buttonAmulet, buttonRunestone, buttonEnchantment, buttonSpirit, buttonMedicine, buttonPotion, buttonMagic;

    [SerializeField] public List<GameObject> panelsCraft;
    
    public void OnPressButtonItemClass(string itemCategory){
        Debug.Log("itemCategory = " + itemCategory);

        // Grab all Items with available blueprints
        List<Item> itemsAvailable = Global.instance.CheckItemsAvailable(itemCategory);

        // Only open menu if there's actually available items
        if(itemsAvailable.Count > 0)
        {
            for(int i = 0; i < itemsAvailable.Count; i++)
            {
                GameObject currentButton = panelsCraft[i].transform.GetChild(0).gameObject;

                // Name of item
                currentButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemsAvailable[i].name;

                // Value
                currentButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + itemsAvailable[i].value.ToString("N0");

                // Show the button
                panelsCraft[i].SetActive(true);
            }

            // Show panel
            panelCraftWindow.SetActive(true);
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        countdownQueue = new float[9];
        for (int i = 0; i < countdownQueue.Length; i++){ countdownQueue[i] = -999f; }

        LinkButtonsCategory();

    }

    // Update is called once per frame
    void Update()
    {
        CraftingItemUpdateTimeAll();
    }

    public void LinkButtonsCategory()
    {
        // Setup onClick events through code
        buttonDagger.onClick.AddListener(delegate {OnPressButtonItemClass("Dagger"); });
        buttonStaff.onClick.AddListener(delegate {OnPressButtonItemClass("Staff"); });
        buttonBow.onClick.AddListener(delegate {OnPressButtonItemClass("Bow"); });
        buttonMace.onClick.AddListener(delegate {OnPressButtonItemClass("Mace"); });
        buttonAxe.onClick.AddListener(delegate {OnPressButtonItemClass("Axe"); });
        buttonSpear.onClick.AddListener(delegate {OnPressButtonItemClass("Spear"); });
        buttonSword.onClick.AddListener(delegate {OnPressButtonItemClass("Sword"); });
        buttonWand.onClick.AddListener(delegate {OnPressButtonItemClass("Wand"); });
        buttonCrossbow.onClick.AddListener(delegate {OnPressButtonItemClass("Crossbow"); });
        buttonGun.onClick.AddListener(delegate {OnPressButtonItemClass("Gun"); });

        buttonShield.onClick.AddListener(delegate {OnPressButtonItemClass("Shield"); });
        buttonShoes.onClick.AddListener(delegate {OnPressButtonItemClass("Shoes"); });
        buttonBoots.onClick.AddListener(delegate {OnPressButtonItemClass("Boots"); });
        buttonGloves.onClick.AddListener(delegate {OnPressButtonItemClass("Gloves"); });
        buttonGauntlets.onClick.AddListener(delegate {OnPressButtonItemClass("Gauntlets"); });
        buttonHeadgear.onClick.AddListener(delegate {OnPressButtonItemClass("Headgear"); });
        buttonHat.onClick.AddListener(delegate {OnPressButtonItemClass("Hat"); });
        buttonHelmet.onClick.AddListener(delegate {OnPressButtonItemClass("Helmet"); });
        buttonClothing.onClick.AddListener(delegate {OnPressButtonItemClass("Clothing"); });
        buttonLightArmor.onClick.AddListener(delegate {OnPressButtonItemClass("Light Armor"); });
        buttonHeavyArmor.onClick.AddListener(delegate {OnPressButtonItemClass("Heavy Armor"); });

        buttonRing.onClick.AddListener(delegate {OnPressButtonItemClass("Ring"); });
        buttonAmulet.onClick.AddListener(delegate {OnPressButtonItemClass("Amulet"); });
        buttonRunestone.onClick.AddListener(delegate {OnPressButtonItemClass("Runestone"); });
        buttonEnchantment.onClick.AddListener(delegate {OnPressButtonItemClass("Enchantment"); });
        buttonSpirit.onClick.AddListener(delegate {OnPressButtonItemClass("Spirit"); });
        buttonMedicine.onClick.AddListener(delegate {OnPressButtonItemClass("Medicine"); });
        buttonPotion.onClick.AddListener(delegate {OnPressButtonItemClass("Potion"); });
        buttonMagic.onClick.AddListener(delegate {OnPressButtonItemClass("Magic"); });

    }
}
