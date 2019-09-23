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

    private GameObject uIOverlayTextboxImageSmall, uIOverlayTextboxImageLarge;

    [SerializeField] public GameObject uIOverlayPointer;
    private bool activeUIOverlayPointer;
    private Vector3 tutorialArea;

    public void SummonUIOverlayPointer(string coordLabel)
    { 

        // New positions after having pointer offset for area that's actually clicked (i.e. the fingertip)
        // Queue Button: -350, 500, 0
        Vector3 coordOverlayPointer;

        switch (coordLabel)
        {
            case "ButtonQueue":
                coordOverlayPointer = new Vector3(200, 1450, 0);
                break;
            case "ButtonSell":
                coordOverlayPointer = new Vector3(530, 250, 0);
                break;
            case "ButtonRebate":
                coordOverlayPointer = new Vector3(250, 450, 0);
                break;
            case "ButtonUpcharge":
                coordOverlayPointer = new Vector3(825, 450, 0);
                break;
            case "ButtonSuggest":
                coordOverlayPointer = new Vector3(250, 650, 0);
                break;
            case "ButtonRefuse":
                coordOverlayPointer = new Vector3(825, 650, 0);
                break;

            case "ButtonCraft":
                coordOverlayPointer = new Vector3(-250, 850, 0);
                break;
            case "ButtonCraftWeapon":
                coordOverlayPointer = new Vector3(-350, 500, 0);
                break;
            case "ButtonCraftDaggers":
                coordOverlayPointer = new Vector3(-350, 500, 0);
                break;
            case "ButtonCraftDirk":
                coordOverlayPointer = new Vector3(-350, 500, 0);
                break;
            case "ButtonCraftFinish":
                coordOverlayPointer = new Vector3(-350, 500, 0);
                break;
            default:
                coordOverlayPointer = new Vector3(0, 0, 0);
                break;
        }

        Debug.Log("Calling SummonUIOverlayPointer(" + coordOverlayPointer.x + ", " + coordOverlayPointer.y + ", "+ coordOverlayPointer.z + ")");
        uIOverlayPointer.SetActive(true);

        // Disable clicking anywhere outside of immediate area of pointer
        CameraControl.instance.DisableSwipe();
        CameraControl.instance.EnableRestrictOnClick();
        int pixelRadius = 100;
        Vector3 unrestrictedOnClickAreaTopLeft = new Vector3(coordOverlayPointer.x - pixelRadius, coordOverlayPointer.y + pixelRadius, 0);
        Vector3 unrestrictedOnClickAreaBottomRight = new Vector3(coordOverlayPointer.x + pixelRadius, coordOverlayPointer.y - pixelRadius, 0);
        CameraControl.instance.SetUnrestrictedOnClickArea(unrestrictedOnClickAreaTopLeft, unrestrictedOnClickAreaBottomRight);


        RectTransform animationPointer = uIOverlayPointer.transform.GetChild(0).GetComponent<RectTransform>();
        animationPointer.localPosition = coordOverlayPointer;

        activeUIOverlayPointer = true;
    }
    public void RemoveUIOverlayPointer()
    { 
        if(activeUIOverlayPointer)
        { 
            // Enable clicking anywhere outside of immediate area of pointer
            CameraControl.instance.EnableSwipe();
            CameraControl.instance.DisableRestrictOnClick();

            uIOverlayPointer.SetActive(false);
            Debug.Log("Calling RemoveUIOverlayPointer()");
            activeUIOverlayPointer = false;
        }
    }

    // Moving all code dealing with tutorials here.

    public static Tutorial instance;
    
    // This ID is to progress specific shop events during tutorial
    private int ID_TUTORIAL_EVENT = 0; public int GetID_TUTORIAL_EVENT(){ return(ID_TUTORIAL_EVENT); } public void IncrementID_TUTORIAL_EVENT(){ ID_TUTORIAL_EVENT++; }

    // These bools are to keep track of specific UIOverlayTextBoxes not in Shop
    public bool SEEN_UIOVERLAYTEXTBOX_SHOP_1, SEEN_UIOVERLAYTEXTBOX_TOWN_1;

    void Awake()
    {
        instance = this;

        TMProUIOverlayTextboxTitle = uIOverlayTextbox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TMProUIOverlayTextboxText = uIOverlayTextbox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        uIOverlayTextboxImageSmall = uIOverlayTextbox.transform.GetChild(2).gameObject;
        uIOverlayTextboxImageSmall.SetActive(false);
        uIOverlayTextboxImageLarge = uIOverlayTextbox.transform.GetChild(3).gameObject;
        uIOverlayTextboxImageLarge.SetActive(false);

// child.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(kvp.Value.filepathImage);

        buttonUIOverlayTextboxClose = uIOverlayTextbox.transform.GetChild(4).GetComponent<Button>();
    }
    void Start()
    {
        activeUIOverlayPointer = false;
        SEEN_UIOVERLAYTEXTBOX_SHOP_1 = false;
        SEEN_UIOVERLAYTEXTBOX_TOWN_1 = false;

        SummonUIOverlayTextBoxWithEventImageLarge("Final Test", 
        "You are almost ready to take over my shop.\n- Bigly",
        1,
        "Images/Tutorial/bigly_sm");
    }
    void Update()
    {
        //RemoveUIOverlayPointer(); // Handled by CameraControl.CheckUnrestrictedOnClickArea() now.
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
    public void SummonUIOverlayTextBoxImageSmall(string title, string body, string filepathImageSmall)
    {
        CameraControl.instance.DisableSwipe();
        PanelUIOverlayStats.SetActive(false);
        PanelScreens.SetActive(false);
        uIOverlayTextbox.SetActive(true);

        TMProUIOverlayTextboxTitle.text = title;
        TMProUIOverlayTextboxText.text = body;

        uIOverlayTextboxImageSmall.SetActive(true);
        uIOverlayTextboxImageSmall.GetComponent<Image>().sprite = Resources.Load<Sprite>(filepathImageSmall);

        buttonUIOverlayTextboxClose.onClick.RemoveAllListeners();
        buttonUIOverlayTextboxClose.onClick.AddListener
        (
            delegate
            {
                PanelUIOverlayStats.SetActive(true);
                PanelScreens.SetActive(true);
                uIOverlayTextbox.SetActive(false);
                CameraControl.instance.EnableSwipe();

                uIOverlayTextboxImageSmall.SetActive(false);
            }
        );

    }
    public void SummonUIOverlayTextBoxImageLarge(string title, string body, string filepathImageLarge)
    {
        CameraControl.instance.DisableSwipe();
        PanelUIOverlayStats.SetActive(false);
        PanelScreens.SetActive(false);
        uIOverlayTextbox.SetActive(true);

        TMProUIOverlayTextboxTitle.text = title;
        TMProUIOverlayTextboxText.text = body;

        uIOverlayTextboxImageLarge.SetActive(true);
        uIOverlayTextboxImageLarge.GetComponent<Image>().sprite = Resources.Load<Sprite>(filepathImageLarge);

        buttonUIOverlayTextboxClose.onClick.RemoveAllListeners();
        buttonUIOverlayTextboxClose.onClick.AddListener
        (
            delegate
            {
                PanelUIOverlayStats.SetActive(true);
                PanelScreens.SetActive(true);
                uIOverlayTextbox.SetActive(false);
                CameraControl.instance.EnableSwipe();

                uIOverlayTextboxImageLarge.SetActive(false);
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
                        SummonUIOverlayPointer("ButtonQueue");
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
    public void SummonUIOverlayTextBoxWithEventImageSmall(string title, string body, int eventID, string filepathImageSmall)
    {
        CameraControl.instance.DisableSwipe();
        PanelUIOverlayStats.SetActive(false);
        PanelScreens.SetActive(false);
        uIOverlayTextbox.SetActive(true);

        TMProUIOverlayTextboxTitle.text = title;
        TMProUIOverlayTextboxText.text = body;

        uIOverlayTextboxImageSmall.SetActive(true);
        uIOverlayTextboxImageSmall.GetComponent<Image>().sprite = Resources.Load<Sprite>(filepathImageSmall);

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
                        SummonUIOverlayPointer("ButtonQueue");
                        break;
                    case 2:
                        // Console.WriteLine("someInt = 2");
                        break;
                    default:
                        // Console.WriteLine("someInt = something other than 1 or 2");
                        break;
                }

                uIOverlayTextboxImageSmall.SetActive(false);
            }
        );
    }
    public void SummonUIOverlayTextBoxWithEventImageLarge(string title, string body, int eventID, string filepathImageLarge)
    {
        CameraControl.instance.DisableSwipe();
        PanelUIOverlayStats.SetActive(false);
        PanelScreens.SetActive(false);
        uIOverlayTextbox.SetActive(true);

        TMProUIOverlayTextboxTitle.text = title;
        TMProUIOverlayTextboxText.text = body;

        uIOverlayTextboxImageLarge.SetActive(true);
        uIOverlayTextboxImageLarge.GetComponent<Image>().sprite = Resources.Load<Sprite>(filepathImageLarge);

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
                        SummonUIOverlayPointer("ButtonQueue");
                        break;
                    case 2:
                        // Console.WriteLine("someInt = 2");
                        break;
                    default:
                        // Console.WriteLine("someInt = something other than 1 or 2");
                        break;
                }

                uIOverlayTextboxImageLarge.SetActive(false);
            }
        );
    }
}
