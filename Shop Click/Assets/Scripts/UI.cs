using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public static UI instance;
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

        // Disable advanced resources at start, need to upgrade basic resources to level 7 to unlock
        panelMineResource.transform.GetChild(4).gameObject.SetActive(false);
        panelMineResource.transform.GetChild(5).gameObject.SetActive(false);
        panelMineResource.transform.GetChild(6).gameObject.SetActive(false);
        panelMineResource.transform.GetChild(7).gameObject.SetActive(false);

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
    private List<Button> buttonsMineResourceDetails = new List<Button>();
    private List<TextMeshProUGUI> tMProMineResourcesDetailsLabels = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> tMProMineResourcesDetailsLevels = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> tMProMineResourcesDetailsRates = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> tMProMineResourcesDetailsCaps = new List<TextMeshProUGUI>();

    private List<TextMeshProUGUI> tMProMineComponentsDetailsLabels = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> tMProMineComponentsDetailsCaps = new List<TextMeshProUGUI>();


    public void InitPanelMineResourcesDetails()
    {
        // Resources
        panelMineResourceDetail.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_1"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_2"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_3"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(3).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_4"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(4).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_5"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(5).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_6"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(6).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_7"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(7).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["resource_8"].GetFilepathImage());
        for(int i = 0; i < panelMineResourceDetail.transform.childCount - 2; i++)
        {
            tMProMineResourcesDetailsLabels.Add( panelMineResourceDetail.transform.GetChild(i).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>() );
            tMProMineResourcesDetailsLevels.Add( panelMineResourceDetail.transform.GetChild(i).transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>() );
            tMProMineResourcesDetailsRates.Add( panelMineResourceDetail.transform.GetChild(i).transform.GetChild(1).transform.GetChild(2).GetComponent<TextMeshProUGUI>() );
            tMProMineResourcesDetailsCaps.Add( panelMineResourceDetail.transform.GetChild(i).transform.GetChild(1).transform.GetChild(3).GetComponent<TextMeshProUGUI>() );
        }

        // Resources button, link to Town.instance.FocusTownDetailWindow()
        panelMineResourceDetail.transform.GetChild(0).GetComponent<Button>().onClick.AddListener( delegate{ Town.instance.FocusTownDetailWindow("resource_1"); } );
        panelMineResourceDetail.transform.GetChild(1).GetComponent<Button>().onClick.AddListener( delegate{ Town.instance.FocusTownDetailWindow("resource_2"); } );
        panelMineResourceDetail.transform.GetChild(2).GetComponent<Button>().onClick.AddListener( delegate{ Town.instance.FocusTownDetailWindow("resource_3"); } );
        panelMineResourceDetail.transform.GetChild(3).GetComponent<Button>().onClick.AddListener( delegate{ Town.instance.FocusTownDetailWindow("resource_4"); } );
        panelMineResourceDetail.transform.GetChild(4).GetComponent<Button>().onClick.AddListener( delegate{ Town.instance.FocusTownDetailWindow("resource_5"); } );
        panelMineResourceDetail.transform.GetChild(5).GetComponent<Button>().onClick.AddListener( delegate{ Town.instance.FocusTownDetailWindow("resource_6"); } );
        panelMineResourceDetail.transform.GetChild(6).GetComponent<Button>().onClick.AddListener( delegate{ Town.instance.FocusTownDetailWindow("resource_7"); } );
        panelMineResourceDetail.transform.GetChild(7).GetComponent<Button>().onClick.AddListener( delegate{ Town.instance.FocusTownDetailWindow("resource_8"); } );

        // Components
        panelMineResourceDetail.transform.GetChild(8).GetComponent<Button>().onClick.AddListener( delegate{ Tutorial.instance.SummonUIOverlayTextBoxImageSmall("Components", "Components are special resources that currently generate very slowly and cannot be upgraded.\n\nSwipe left twice to check out the Colosseum to get more components.", "Images/UI/hand_swipe_static_left"); } );

        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_1"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_2"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_3"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_4"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_5"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(5).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_6"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(6).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_7"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(7).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_8"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(8).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_9"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(9).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_10"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(10).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_11"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(11).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_12"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(12).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_13"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(13).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_14"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(14).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_15"].GetFilepathImage());
        panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(15).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()["component_16"].GetFilepathImage());
    
        for(int i = 0; i < 16; i++)
        {
            tMProMineComponentsDetailsLabels.Add( panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(i).transform.GetChild(1).GetComponent<TextMeshProUGUI>() );
            tMProMineComponentsDetailsCaps.Add( panelMineResourceDetail.transform.GetChild(8).transform.GetChild(0).transform.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>() );
        }

        // Make basic resource buttons non-interactable at start (make interactable at tutorial end)
        panelMineResourceDetail.transform.GetChild(0).GetComponent<Button>().interactable = false;
        panelMineResourceDetail.transform.GetChild(1).GetComponent<Button>().interactable = false;
        panelMineResourceDetail.transform.GetChild(2).GetComponent<Button>().interactable = false;
        panelMineResourceDetail.transform.GetChild(3).GetComponent<Button>().interactable = false;
        panelMineResourceDetail.transform.GetChild(8).GetComponent<Button>().interactable = false;

        // Disable advanced resources at start, need to upgrade basic resources to level 7 to unlock
        panelMineResourceDetail.transform.GetChild(4).gameObject.SetActive(false);
        panelMineResourceDetail.transform.GetChild(5).gameObject.SetActive(false);
        panelMineResourceDetail.transform.GetChild(6).gameObject.SetActive(false);
        panelMineResourceDetail.transform.GetChild(7).gameObject.SetActive(false);
    }
    public void UpdatePanelMineResourcesDetail(int index, string resource)
    {
        tMProMineResourcesDetailsLabels[index].text = Global.instance.GetResources()[resource].GetLabel();
        tMProMineResourcesDetailsLevels[index].text = "Lvl. " + Global.instance.GetResources()[resource].GetLevel().ToString();
        tMProMineResourcesDetailsRates[index].text = Global.instance.GetResources()[resource].ToStringRate();
        tMProMineResourcesDetailsCaps[index].text = Global.instance.GetResources()[resource].ToStringAmount() + "/" + Global.instance.GetResources()[resource].GetCap().ToString();
    }
    public void UpdatePanelMineComponentsDetail(int index, string resource)
    {
        tMProMineComponentsDetailsLabels[index].text = Global.instance.GetResources()[resource].GetLabel();
        tMProMineComponentsDetailsCaps[index].text = Global.instance.GetResources()[resource].ToStringAmount() + "/" + Global.instance.GetResources()[resource].GetCap().ToString();
        //Debug.Log(resource + " = " + Global.instance.GetResources()[resource].ToStringAmount());
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

        UpdatePanelMineComponentsDetail(0, "component_1");
        UpdatePanelMineComponentsDetail(1, "component_2");
        UpdatePanelMineComponentsDetail(2, "component_3");
        UpdatePanelMineComponentsDetail(3, "component_4");
        UpdatePanelMineComponentsDetail(4, "component_5");
        UpdatePanelMineComponentsDetail(5, "component_6");
        UpdatePanelMineComponentsDetail(6, "component_7");
        UpdatePanelMineComponentsDetail(7, "component_8");
        UpdatePanelMineComponentsDetail(8, "component_9");
        UpdatePanelMineComponentsDetail(9, "component_10");
        UpdatePanelMineComponentsDetail(10, "component_11");
        UpdatePanelMineComponentsDetail(11, "component_12");
        UpdatePanelMineComponentsDetail(12, "component_13");
        UpdatePanelMineComponentsDetail(13, "component_14");
        UpdatePanelMineComponentsDetail(14, "component_15");
        UpdatePanelMineComponentsDetail(15, "component_16");
    }

    void Awake()
    {
        instance = this;
    }

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
