using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Town : MonoBehaviour
{

[SerializeField] public GameObject panelTownWorker;
private List<GameObject> panelTownWorkers = new List<GameObject>(); // To activate/inactivate workers.
// private List<TextMeshProUGUI> uITownResourcesDetailsLevels = new List<TextMeshProUGUI>(); // Update UI.
// private List<TextMeshProUGUI> uITownResourcesDetailsRates = new List<TextMeshProUGUI>(); // Update UI.
[SerializeField] public GameObject panelTownDetailWindow; // This is the window with the details.

// Only first eight workers for now
string[] filepathImageCharacter = new string[]{"Images/Characters/8", "Images/Characters/17", "Images/Characters/21", "Images/Characters/22","Images/Characters/19", "Images/Characters/15", "Images/Characters/5", "Images/Characters/3"};
string[] idResource = new string[]{"resource_1", "resource_2", "resource_3", "resource_4", "resource_5", "resource_6", "resource_7", "resource_8"};
string[] idLabel = new string[]{"Workshop", "Saw Mill", "Guild", "Dispensary","Forge", "University", "Labortory", "Restaurant"};


private List<TextMeshProUGUI> tMProResourceLevels = new List<TextMeshProUGUI>();
private List<TextMeshProUGUI> tMProResourceRates = new List<TextMeshProUGUI>();

    public void InitPanelTownWorkers()
    {
        // Only first eight workers for now
        // panelTownWorker.transform.childCount
        for(int i = 0; i < 8; i++)
        {
            panelTownWorkers.Add(panelTownWorker.transform.GetChild(i).gameObject);
            panelTownWorkers[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(filepathImageCharacter[i]);
            panelTownWorkers[i].transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(Global.instance.GetResources()[idResource[i]].GetFilepathImage());
            panelTownWorkers[i].transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = idLabel[i];
        
            // TODO: First eight are the workers that deal with resources (for now)
            tMProResourceLevels.Add( panelTownWorker.transform.GetChild(i).transform.GetChild(0).transform.GetChild(3).GetComponent<TextMeshProUGUI>() );
            tMProResourceRates.Add( panelTownWorker.transform.GetChild(i).transform.GetChild(0).transform.GetChild(4).GetComponent<TextMeshProUGUI>() );
        }
        
    }

    public void UpdatePanelTownWorker()
    {
        for(int i = 0; i < 8; i++)
        {
            tMProResourceLevels[i].text = "Level " + Global.instance.GetResources()[idResource[i]].GetLevel().ToString();
            tMProResourceRates[i].text = Global.instance.GetResources()[idResource[i]].ToStringRate();
        }

    }

    public void LinkButtonWorkerDetails()
    {
        // Only first eight workers for now
        // panelTownWorker.transform.childCount

        // panelTownWorkers[1].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate{OnClickTownDetailWindow(
        //     Global.instance.GetResources()[idResource[1]],
        //     idLabel[1],
        //     Global.instance.GetResources()[idResource[1]].GetFilepathImage(),
        //     filepathImageCharacter[1]);});

        // panelTownWorkers[0].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate{OnClickTownDetailWindow(
        //     Global.instance.GetResources()[idResource[0]],
        //     idLabel[0],
        //     Global.instance.GetResources()[idResource[0]].GetFilepathImage(),
        //     filepathImageCharacter[0]);});

        // panelTownWorkers[2].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate{OnClickTownDetailWindow(
        //     Global.instance.GetResources()[idResource[2]],
        //     idLabel[2],
        //     Global.instance.GetResources()[idResource[2]].GetFilepathImage(),
        //     filepathImageCharacter[2]);});
        // panelTownWorkers[3].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate{OnClickTownDetailWindow(
        //     Global.instance.GetResources()[idResource[3]],
        //     idLabel[3],
        //     Global.instance.GetResources()[idResource[3]].GetFilepathImage(),
        //     filepathImageCharacter[3]);});
        // panelTownWorkers[4].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate{OnClickTownDetailWindow(
        //     Global.instance.GetResources()[idResource[4]],
        //     idLabel[4],
        //     Global.instance.GetResources()[idResource[4]].GetFilepathImage(),
        //     filepathImageCharacter[4]);});
        // panelTownWorkers[5].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate{OnClickTownDetailWindow(
        //     Global.instance.GetResources()[idResource[5]],
        //     idLabel[5],
        //     Global.instance.GetResources()[idResource[5]].GetFilepathImage(),
        //     filepathImageCharacter[5]);});
        // panelTownWorkers[6].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate{OnClickTownDetailWindow(
        //     Global.instance.GetResources()[idResource[6]],
        //     idLabel[6],
        //     Global.instance.GetResources()[idResource[6]].GetFilepathImage(),
        //     filepathImageCharacter[6]);});
        // panelTownWorkers[7].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate{OnClickTownDetailWindow(
        //     Global.instance.GetResources()[idResource[7]],
        //     idLabel[7],
        //     Global.instance.GetResources()[idResource[7]].GetFilepathImage(),
        //     filepathImageCharacter[7]);});


        LinkButtonWorkerDetail(0);
        LinkButtonWorkerDetail(1);
        LinkButtonWorkerDetail(2);
        LinkButtonWorkerDetail(3);
        LinkButtonWorkerDetail(4);
        LinkButtonWorkerDetail(5);
        LinkButtonWorkerDetail(6);
        LinkButtonWorkerDetail(7);
    }

    public void LinkButtonWorkerDetail(int i)
    {
        //Debug.Log("OnClickTownDetailWindow(Resource " + Global.instance.GetResources()[idResource[i]].GetLabel() + ", " + idLabel[i] + ", " + Global.instance.GetResources()[idResource[i]].GetFilepathImage() + ", " + filepathImageCharacter[i]);
        panelTownWorkers[i].transform.GetChild(0).GetComponent<Button>().onClick.AddListener
        (
            delegate
            {
                OnClickTownDetailWindow
                (
                    Global.instance.GetResources()[idResource[i]],
                    idLabel[i],
                    Global.instance.GetResources()[idResource[i]].GetFilepathImage(),
                    filepathImageCharacter[i]
                );
            }
        );
    }


    public void OnClickTownDetailWindow(Resource currentResource, string idLabel, string filepathImageResource, string filepathImageCharacter)
    {
        // Panel Town Worker Detail is enabled
        // Panel Town Worker disabled
        // UIControl.DisableUIOverlayButtons
        // CameraControl.DisableSwipe
        panelTownDetailWindow.SetActive(true);
        panelTownWorker.SetActive(false);
        UIControl.instance.DisableUIOverlayButtons();
        CameraControl.instance.DisableSwipe();

        //Debug.Log("OnClickTownDetailWindow(Resource " + currentResource.GetLabel() + ", " + idLabel + ", " + filepathImageResource + ", " + filepathImageCharacter);
        // Image character.
        panelTownDetailWindow.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(filepathImageCharacter);

        // Text title.
        panelTownDetailWindow.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = idLabel;

        // Text body.
        panelTownDetailWindow.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Donate a bit of money and after enough I'll level up and work faster!";

        // Text level, updated when leveled up.
        panelTownDetailWindow.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "  Level " + currentResource.GetLevel().ToString();

        // Image resource
        panelTownDetailWindow.transform.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>(currentResource.GetFilepathImage());

        // Text progress, updated when money is added or level up.
        panelTownDetailWindow.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Next Level:\n" + currentResource.GetCurrentInvestmentValue().ToString("N0") + "/" + currentResource.GetCurrentThresholdValue();

        // Text rate, updated when leveled up.
        panelTownDetailWindow.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "Current Rate: " + currentResource.ToStringRate();
        panelTownDetailWindow.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "Next Rate: " + currentResource.ToStringNextRate();

        // Button coins text
        panelTownDetailWindow.transform.GetChild(8).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentResource.GetCurrentThresholdIncrement().ToString("N0");

        // Button coins on click
        panelTownDetailWindow.transform.GetChild(8).GetComponent<Button>().onClick.RemoveAllListeners();
        panelTownDetailWindow.transform.GetChild(8).GetComponent<Button>().onClick.AddListener
        (
            delegate
            {
                // Check if enough coins
                if(Global.instance.GetStats()["Coins"].CheckAmount(currentResource.GetCurrentThresholdIncrement())){
                    // Remove amount of coins
                    Global.instance.GetStats()["Coins"].RemoveAmount(currentResource.GetCurrentThresholdIncrement());

                    // Increase amount of investment value
                    currentResource.AddCurrentInvestmentValue(currentResource.GetCurrentThresholdIncrement());

                    // Update stuff:
                        // Text progress, updated when money is added or level up.
                        panelTownDetailWindow.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Next Level:\n" + currentResource.GetCurrentInvestmentValue().ToString("N0") + "/" + currentResource.GetCurrentThresholdValue();
                        // Text level, updated when leveled up.
                        panelTownDetailWindow.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "  Level " + currentResource.GetLevel().ToString();
                        // Text rate, updated when leveled up.
                        panelTownDetailWindow.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "Current Rate: " + currentResource.ToStringRate();
                        panelTownDetailWindow.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "Next Rate: " + currentResource.ToStringNextRate();
                        // Button coins text
                        panelTownDetailWindow.transform.GetChild(8).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentResource.GetCurrentThresholdIncrement().ToString("N0");

                    // Play SFX
                    SFX.instance.PlaySFXSale();

                }
                else
                {
                    // Play SFX
                    SFX.instance.PlaySFXNoGo();  
                }
            }
        );

        // Button close.
        panelTownDetailWindow.transform.GetChild(10).GetComponent<Button>().onClick.RemoveAllListeners();
        panelTownDetailWindow.transform.GetChild(10).GetComponent<Button>().onClick.AddListener
        (
            delegate
            {
                panelTownDetailWindow.SetActive(false);
                panelTownWorker.SetActive(true);
                UIControl.instance.EnableUIOverlayButtons();
                CameraControl.instance.EnableSwipe();
            }
        );
    }

    void Awake()
    {
        
    }

    void Start()
    {
        InitPanelTownWorkers(); // Init in Start(), will crash in Awake() since other things needed to load first.
        LinkButtonWorkerDetails();
        panelTownDetailWindow.SetActive(false); // If open during debugging, close.
    }
    void Update()
    {
        UpdatePanelTownWorker();
    }



}
