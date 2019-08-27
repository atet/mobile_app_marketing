using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mine : MonoBehaviour
{

    [SerializeField] GameObject panelMineCraftQueue;
    


    private float[] countdownQueue = new float[9];

    private void CraftingItem(string itemName){
        // Different from CraftItem(), this waits a certain amount of time before CraftItem()
        if(CraftingItemSlotOpen()){
            // Close panelCraftWindow.
            panelCraftWindow.SetActive(false);

            // Find the lowest index open slot.
            int currentIndex = CraftingItemSelectSlot();
            GameObject currentSlot = panelMineCraftQueue.transform.GetChild(currentIndex).gameObject;

            // Make slot's panel active.
            currentSlot.SetActive(true);

            // Set image
            currentSlot.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = 
            Resources.Load<Sprite>(Global.instance.GetInventory()[itemName].filepathImage);
            currentSlot.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Image>().SetNativeSize();

            // Set countdown to Item's craft time.
            //countdownQueue[currentIndex] = Global.instance.GetInventory()[itemName].timeCrafting;

            // Set text on slot to reflect countdown time.
            //currentSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.text = "";


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

    [SerializeField] GameObject panelCraftWindow;
    

    [SerializeField] Button buttonDagger;

    [SerializeField] Button buttonDaggerWoodDagger;

    public void OnPressButtonDaggerWoodDagger(){

        // TODO: Make this function generic for any Item that is passed to this.
        // Perform a resource check
        if(Global.instance.GetInventory()["Wood Dagger"].CheckResource())
        {
            // TEST: Instant crafting
            //Global.instance.GetInventory()["Wood Dagger"].CraftItem();
            //panelCraftWindow.SetActive(false);

            CraftingItem("Wood Dagger");
            

        }

        
        
        // TODO: Open Craft Queue and wait for countdown before item is finished being crafted
        //panelCraftQueue.SetActive(true);
    }






    public void OnPressButtonDagger()
    {
        panelCraftWindow.SetActive(true);
    }            


    // Specific hard coded example



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
