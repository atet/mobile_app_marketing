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
        SummonUIOverlayTextBox("Almost...", "After years of being my apprentice, you're almost ready to take over my shop.\n\nToday will be your final test.\n\n- Biggs");
    }
    public void SummonUIOverlayTextBox(string title, string body)
    {
        CameraControl.instance.DisableSwipe();
        PanelUIOverlayStats.SetActive(false);
        PanelScreens.SetActive(false);
        uIOverlayTextbox.SetActive(true);

        TMProUIOverlayTextboxTitle.text = title;
        TMProUIOverlayTextboxText.text = body;

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
}
