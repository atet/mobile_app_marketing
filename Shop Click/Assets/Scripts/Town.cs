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
[SerializeField] public GameObject panelTownDetailWindow;

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
            Debug.Log(idResource[i]);
            Debug.Log(Global.instance.GetResources()["resource_1"].GetFilepathImage());
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

    void Awake()
    {
        
    }

    void Start()
    {
        InitPanelTownWorkers(); // Init in Start(), will crash in Awak() since other things needed to load first.
    }
    void Update()
    {
        UpdatePanelTownWorker();
    }



}
