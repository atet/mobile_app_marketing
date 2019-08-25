using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mine : MonoBehaviour
{

    [SerializeField] GameObject panelCraftQueue;

    [SerializeField] Button buttonCraftQueue1, buttonCraftQueue2, buttonCraftQueue3, buttonCraftQueue4, buttonCraftQueue5, buttonCraftQueue6, buttonCraftQueue7, buttonCraftQueue8, buttonCraftQueue9;
    

    [SerializeField] GameObject panelCraftWindow;
    

    [SerializeField] Button buttonDagger;

    [SerializeField] Button buttonDaggerWoodDagger;

    public void OnPressButtonDaggerWoodDagger(){

        // TODO: Make this function generic for any Item that is passed to this.
        // Perform a resource check
        if(Global.instance.GetInventory()["Wood Dagger"].CheckResource())
        {
            // TEST: Instant crafting
            Global.instance.GetInventory()["Wood Dagger"].CraftItem();


            panelCraftWindow.SetActive(false);


            // Shop inventory TMPro not actively updated!

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
