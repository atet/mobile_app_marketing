using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    // [SerializeField] public TextMeshProUGUI level, coins, chakra, gems, iron, hide, wood, herbs, steel, oil, electricity, titanium;

    // UI Overlay: Shown on all screens.
    [SerializeField] public GameObject panelUIOverlayStats;
    // Order is coins, level, chakra, gems
    private List<TextMeshProUGUI> tMProUIOverlays = new List<TextMeshProUGUI>();
    public void InitUIOverlay()
    {
        for(int i = 0; i < panelUIOverlayStats.transform.childCount; i++)
        {
            tMProUIOverlays.Add( panelUIOverlayStats.transform.GetChild(i).transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>() );
        }
    }
    public void UpdateUIOverlay()
    {
        tMProUIOverlays[0].text = Global.instance.GetStats()["Coins"].ToStringAmount();
        tMProUIOverlays[1].text = Global.instance.GetStats()["Level"].GetLevel().ToString();
        tMProUIOverlays[2].text = Global.instance.GetStats()["Chakra"].ToStringAmount();
        tMProUIOverlays[3].text = Global.instance.GetStats()["Gems"].ToStringAmount();
    }

    [SerializeField] public GameObject panelShop;
    private TextMeshProUGUI tMProShopQueue;
    public void InitPanelShop()
    {
        tMProShopQueue = panelShop.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    public void UpdatePanelShop()
    { 
        //Debug.Log("UpdatePanelShop(): " + Global.instance.GetResources()["resource_0"].ToStringAmount());
        tMProShopQueue.text = Global.instance.GetResources()["resource_0"].ToStringAmount();
    }

    [SerializeField] public GameObject panelMineResource;
    private List<TextMeshProUGUI> tMProMineResources = new List<TextMeshProUGUI>();

    public void InitPanelMineResources()
    {
        panelMineResource.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_1"].GetFilepathImage());
        panelMineResource.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_2"].GetFilepathImage());
        panelMineResource.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_3"].GetFilepathImage());
        panelMineResource.transform.GetChild(3).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_4"].GetFilepathImage());
        panelMineResource.transform.GetChild(4).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_5"].GetFilepathImage());
        panelMineResource.transform.GetChild(5).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_6"].GetFilepathImage());
        panelMineResource.transform.GetChild(6).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_7"].GetFilepathImage());
        panelMineResource.transform.GetChild(7).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_8"].GetFilepathImage());
        
        for(int i = 0; i < panelMineResource.transform.childCount; i++)
        {
            tMProMineResources.Add( panelMineResource.transform.GetChild(i).transform.GetChild(1).GetComponent<TextMeshProUGUI>() );
        }
    }
    public void UpdatePanelMineResources()
    {
        tMProMineResources[0].text = Global.instance.GetResources()["resource_1"].ToStringAmount();
        tMProMineResources[1].text = Global.instance.GetResources()["resource_2"].ToStringAmount();
        tMProMineResources[2].text = Global.instance.GetResources()["resource_3"].ToStringAmount();
        tMProMineResources[3].text = Global.instance.GetResources()["resource_4"].ToStringAmount();
        tMProMineResources[4].text = Global.instance.GetResources()["resource_5"].ToStringAmount();
        tMProMineResources[5].text = Global.instance.GetResources()["resource_6"].ToStringAmount();
        tMProMineResources[6].text = Global.instance.GetResources()["resource_7"].ToStringAmount();
        tMProMineResources[7].text = Global.instance.GetResources()["resource_8"].ToStringAmount();
    }

    [SerializeField] public GameObject panelMineResourceDetail;


    private List<TextMeshProUGUI> tMProMineResourcesDetailsLabels = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> tMProMineResourcesDetailsLevels = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> tMProMineResourcesDetailsRates = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> tMProMineResourcesDetailsCaps = new List<TextMeshProUGUI>();
    public void InitPanelMineResourcesDetails()
    {
        panelMineResourceDetail.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_1"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_2"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_3"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(3).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_4"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(4).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_5"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(5).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_6"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(6).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_7"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(7).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_8"].GetFilepathImage());
        for(int i = 0; i < panelMineResourceDetail.transform.childCount - 2; i++) // TODO: Components and close are the last indices, handle components later
        {
            tMProMineResourcesDetailsLabels.Add( panelMineResourceDetail.transform.GetChild(i).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>() );
            tMProMineResourcesDetailsLevels.Add( panelMineResourceDetail.transform.GetChild(i).transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>() );
            tMProMineResourcesDetailsRates.Add( panelMineResourceDetail.transform.GetChild(i).transform.GetChild(1).transform.GetChild(2).GetComponent<TextMeshProUGUI>() );
            tMProMineResourcesDetailsCaps.Add( panelMineResourceDetail.transform.GetChild(i).transform.GetChild(1).transform.GetChild(3).GetComponent<TextMeshProUGUI>() );
        }
    }
    public void UpdatePanelMineResourcesDetail(int index, string resource)
    {
        tMProMineResourcesDetailsLabels[index].text = Global.instance.GetResources()[resource].GetLabel();
        tMProMineResourcesDetailsLevels[index].text = "Lvl. " + Global.instance.GetResources()[resource].GetLevel().ToString();
        tMProMineResourcesDetailsRates[index].text = Global.instance.GetResources()[resource].ToStringRate();
        tMProMineResourcesDetailsCaps[index].text = Global.instance.GetResources()[resource].ToStringAmount() + "/" + Global.instance.GetResources()[resource].GetCap().ToString();
    }
    public void UpdatePanelMineResourcesDetails()
    {
        UpdatePanelMineResourcesDetail(0, "resource_1");
        UpdatePanelMineResourcesDetail(1, "resource_2");
        UpdatePanelMineResourcesDetail(2, "resource_3");
        UpdatePanelMineResourcesDetail(3, "resource_4");
        UpdatePanelMineResourcesDetail(4, "resource_5");
        UpdatePanelMineResourcesDetail(5, "resource_6");
        UpdatePanelMineResourcesDetail(6, "resource_7");
        UpdatePanelMineResourcesDetail(7, "resource_8");
    }



    // [SerializeField] public GameObject panelTownWorkers;
    // private List<TextMeshProUGUI> uITownResourcesDetailsLevels = new List<TextMeshProUGUI>();
    // private List<TextMeshProUGUI> uITownResourcesDetailsRates = new List<TextMeshProUGUI>();
    // public void InitPanelTownWorkers()
    // {
    //     panelTownWorkers.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Characters/8");
    //     panelTownWorkers.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_1"].GetFilepathImage());
    //     panelTownWorkers.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Workshop";

    //     panelTownWorkers.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Characters/17");
    //     panelTownWorkers.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_2"].GetFilepathImage());
    //     panelTownWorkers.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Saw Mill";

    //     panelTownWorkers.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Characters/21");
    //     panelTownWorkers.transform.GetChild(2).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_3"].GetFilepathImage());
    //     panelTownWorkers.transform.GetChild(2).transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Guild";

    //     panelTownWorkers.transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Characters/22");
    //     panelTownWorkers.transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_4"].GetFilepathImage());
    //     panelTownWorkers.transform.GetChild(3).transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Dispensary";

    //     panelTownWorkers.transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Characters/19");
    //     panelTownWorkers.transform.GetChild(4).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_5"].GetFilepathImage());
    //     panelTownWorkers.transform.GetChild(4).transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Forge";

    //     panelTownWorkers.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Characters/15");
    //     panelTownWorkers.transform.GetChild(5).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_6"].GetFilepathImage());
    //     panelTownWorkers.transform.GetChild(5).transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "University";

    //     panelTownWorkers.transform.GetChild(6).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Characters/5");
    //     panelTownWorkers.transform.GetChild(6).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_7"].GetFilepathImage());
    //     panelTownWorkers.transform.GetChild(6).transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Laboratory";

    //     panelTownWorkers.transform.GetChild(7).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Characters/3");
    //     panelTownWorkers.transform.GetChild(7).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_8"].GetFilepathImage());
    //     panelTownWorkers.transform.GetChild(7).transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Restaurant";

    //     for(int i = 0; i < 8; i++) // TODO: First eight are the workers that deal with resources (for now)
    //     {
    //         uITownResourcesDetailsLevels.Add( panelTownWorkers.transform.GetChild(i).transform.GetChild(0).transform.GetChild(3).GetComponent<TextMeshProUGUI>() );
    //         uITownResourcesDetailsRates.Add( panelTownWorkers.transform.GetChild(i).transform.GetChild(0).transform.GetChild(4).GetComponent<TextMeshProUGUI>() );
    //     }
    // }
    // public void UpdatePanelTownWorker(int index, string resource)
    // {
    //     uITownResourcesDetailsLevels[index].text = "Level " + Global.instance.GetResources()[resource].GetLevel().ToString();
    //     uITownResourcesDetailsRates[index].text = Global.instance.GetResources()[resource].ToStringRate();
    // }
    // public void UpdatePanelTownWorkers()
    // {   
    //     UpdatePanelTownWorker(0, "resource_1");
    //     UpdatePanelTownWorker(1, "resource_2");
    //     UpdatePanelTownWorker(2, "resource_3");
    //     UpdatePanelTownWorker(3, "resource_4");
    //     UpdatePanelTownWorker(4, "resource_5");
    //     UpdatePanelTownWorker(5, "resource_6");
    //     UpdatePanelTownWorker(6, "resource_7");
    //     UpdatePanelTownWorker(7, "resource_8");
    // }


    // Start is called before the first frame update
    void Start()
    {
        InitUIOverlay();
        InitPanelShop();
        InitPanelMineResources();
        InitPanelMineResourcesDetails();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUIOverlay();
        UpdatePanelShop();
        UpdatePanelMineResources();
        UpdatePanelMineResourcesDetails();

    }



}
