using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] public GameObject PanelUIOverlayStats;
    [SerializeField] public GameObject PanelScreens;
    [SerializeField] public GameObject uIOverlayTextbox;
    private Button buttonUIOverlayTextboxClose;
    private TextMeshProUGUI TMProUIOverlayTextboxTitle, TMProUIOverlayTextboxText;


    [SerializeField] public GameObject uIOverlayPointer;
    private bool activeUIOverlayPointer = false;

    public void SummonUIOverlayPointer(float x, float y, float z)
    { 
        // Positions in UI for the following
        // Queue Button: -170, 400, 0
        // Sell Button: 85, -800, 0
        // Rebate Button: -25, -600, 0
        // Upcharge Button: 375, -600, 0
        // Suggest Button: -25, -395, 0
        // Refuse Button: 375, -395, 0
        // Craft Button: -150, -900, 0
        // Craft Weapon Button: -290, -750, 0
        // Craft Weapon Daggers Button: 220, -800, 0
        // Craft Dirk Button: 400, -750, 0
        // Finish Craft Button: -170, 140, 0

        Debug.Log("Calling SummonUIOverlayPointer(" + x + ", " + y + ", "+ z + ")");
        uIOverlayPointer.SetActive(true);
        
        RectTransform animationPointer = uIOverlayPointer.transform.GetChild(0).GetComponent<RectTransform>();
        animationPointer.localPosition = new Vector3(x, y, z);

        activeUIOverlayPointer = true;
    }
    public void RemoveUIOverlayPointer()
    { 
        if(activeUIOverlayPointer & Input.GetMouseButtonDown(0))
        { 
            uIOverlayPointer.SetActive(false);
            Debug.Log("Calling RemoveUIOverlayPointer()");
            activeUIOverlayPointer = false;
        }
    }

    // Moving all code dealing with tutorials here.

    public static Tutorial instance;
    private int ID_TUTORIAL_EVENT = 0; public int GetID_TUTORIAL_EVENT(){ return(ID_TUTORIAL_EVENT); } public void IncrementID_TUTORIAL_EVENT(){ ID_TUTORIAL_EVENT++; }

    void Awake()
    {
        instance = this;

        TMProUIOverlayTextboxTitle = uIOverlayTextbox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TMProUIOverlayTextboxText = uIOverlayTextbox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        buttonUIOverlayTextboxClose = uIOverlayTextbox.transform.GetChild(2).GetComponent<Button>();
    }
    void Start()
    {
        SummonUIOverlayTextBoxWithEvent("Almost...", "After years of being my apprentice, you're almost ready to take over my shop.\n\nToday will be your final test.\n\n- Biggs", 1);
    }
    void Update()
    {
        RemoveUIOverlayPointer();
    }

    public void SummonUIOverlayTextBox(string title, string body)
    {
        CameraControl.instance.DisableSwipe();
        PanelUIOverlayStats.SetActive(false);
        PanelScreens.SetActive(false);
        uIOverlayTextbox.SetActive(true);

        TMProUIOverlayTextboxTitle.text = title;
        TMProUIOverlayTextboxText.text = body;

        buttonUIOverlayTextboxClose.onClick.RemoveAllListeners();
        buttonUIOverlayTextboxClose.onClick.AddListener
        (
            delegate
            {
                PanelUIOverlayStats.SetActive(true);
                PanelScreens.SetActive(true);
                uIOverlayTextbox.SetActive(false);
                CameraControl.instance.EnableSwipe();
            }
        );
    }
    public void SummonUIOverlayTextBoxWithEvent(string title, string body, int eventID)
    {
        CameraControl.instance.DisableSwipe();
        PanelUIOverlayStats.SetActive(false);
        PanelScreens.SetActive(false);
        uIOverlayTextbox.SetActive(true);

        TMProUIOverlayTextboxTitle.text = title;
        TMProUIOverlayTextboxText.text = body;

        buttonUIOverlayTextboxClose.onClick.RemoveAllListeners();
        buttonUIOverlayTextboxClose.onClick.AddListener
        (
            delegate
            {
                PanelUIOverlayStats.SetActive(true);
                PanelScreens.SetActive(true);
                uIOverlayTextbox.SetActive(false);
                CameraControl.instance.EnableSwipe();

                switch(eventID)
                {
                    case 1:
                        // Console.WriteLine("someInt = 1");
                        SummonUIOverlayPointer(-170f, 400f, 0f);
                        break;
                    case 2:
                        // Console.WriteLine("someInt = 2");
                        break;
                    default:
                        // Console.WriteLine("someInt = something other than 1 or 2");
                        break;
                }

            }
        );
    }
}
