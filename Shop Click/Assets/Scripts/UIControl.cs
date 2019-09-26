using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControl : MonoBehaviour
{
    public static UIControl instance;
    [SerializeField] public Button buttonCoinsDetail, buttonLevelDetail, buttonChakraDetail, buttonGemsDetail;
    private Button buttonCoinsDetailClose, buttonLevelDetailClose, buttonChakraDetailClose, buttonGemsDetailClose;
    
    // See Tutorial.SummonUIOverlayTextBox()
    // [SerializeField] public GameObject uIOverlayWindowFullScreen;
    // private TextMeshProUGUI tMProOverlayWindowFullScreenTitle, tMProOverlayWindowFullScreenText;
    // private Button buttonOverlayWindowFullScreenClose;

    public void OnClickButtonCoinsDetail()
    {
        DisableUIOverlayButtons();
        panelCoinsDetail.SetActive(true);

        panelCoinsDetail.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Coins";
        panelCoinsDetail.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = 
            "Current Coins:\n" + Global.instance.GetStats()["Coins"].ToStringAmount() + "\n\n" +
            "Lifetime Gain:\n" + Global.instance.GetStats()["Coins"].GetAmountLifetimeGain().ToString("N0") + "\n\n" +
            "Lifetime Spend:\n" + Global.instance.GetStats()["Coins"].GetAmountLifetimeSpend().ToString("N0");
    }
    public void OnClickButtonLevelDetail()
    {
        DisableUIOverlayButtons();
        panelLevelDetail.SetActive(true);

        panelLevelDetail.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Level";
        panelLevelDetail.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = 
            "Current Level:\n" + Global.instance.GetStats()["Level"].GetLevel().ToString() + "\n\n" +
            "Current Experience:\n" + Global.instance.GetStats()["Level"].GetAmountLifetimeGain().ToString("N0") + "\n\n" +
            "Experience to Next Level:\n" + Global.instance.GetStats()["Level"].GetToNextLevelValue();
    }
    public void OnClickButtonChakraDetail()
    {
        DisableUIOverlayButtons();
        panelChakraDetail.SetActive(true);

        panelChakraDetail.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Chakra";
        panelChakraDetail.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = 
            "Current Chakra:\n" + Global.instance.GetStats()["Chakra"].ToStringAmount() + "\n\n" +
            "Lifetime Gain:\n" + Global.instance.GetStats()["Chakra"].GetAmountLifetimeGain().ToString("N0") + "\n\n" +
            "Lifetime Spend:\n" + Global.instance.GetStats()["Chakra"].GetAmountLifetimeSpend().ToString("N0");
    }
    public void OnClickButtonGemsDetail()
    {
        DisableUIOverlayButtons();
        panelGemsDetail.SetActive(true);

        panelGemsDetail.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Gems";
        panelGemsDetail.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = 
            "Current Gems:\n" + Global.instance.GetStats()["Gems"].ToStringAmount() + "\n\n" +
            "Lifetime Gain:\n" + Global.instance.GetStats()["Gems"].GetAmountLifetimeGain().ToString("N0") + "\n\n" +
            "Lifetime Spend:\n" + Global.instance.GetStats()["Gems"].GetAmountLifetimeSpend().ToString("N0");
    }
    [SerializeField] public GameObject panelCoinsDetail, panelLevelDetail, panelChakraDetail, panelGemsDetail;

    public void OnClickButtonClose()
    {
        EnableUIOverlayButtons();
        panelCoinsDetail.SetActive(false);
        panelLevelDetail.SetActive(false);
        panelChakraDetail.SetActive(false);
        panelGemsDetail.SetActive(false);
    }

    // Start is called before the first frame update

    public void EnableUIOverlayButtons()
    {
        buttonCoinsDetail.interactable = true;
        buttonLevelDetail.interactable = true;
        buttonChakraDetail.interactable = true;
        buttonGemsDetail.interactable = true;
    }
    public void DisableUIOverlayButtons()
    {
        buttonCoinsDetail.interactable = false;
        buttonLevelDetail.interactable = false;
        buttonChakraDetail.interactable = false;
        buttonGemsDetail.interactable = false;
    }
    void Awake()
    {
        instance = this;

        buttonCoinsDetail.onClick.AddListener(delegate{ OnClickButtonCoinsDetail(); } );
        buttonLevelDetail.onClick.AddListener(delegate{ OnClickButtonLevelDetail(); } );
        buttonChakraDetail.onClick.AddListener(delegate{ OnClickButtonChakraDetail(); } );
        buttonGemsDetail.onClick.AddListener(delegate{ OnClickButtonGemsDetail(); } );

        buttonCoinsDetailClose = panelCoinsDetail.transform.GetChild(2).gameObject.GetComponent<Button>();
        buttonLevelDetailClose = panelLevelDetail.transform.GetChild(2).gameObject.GetComponent<Button>();
        buttonChakraDetailClose = panelChakraDetail.transform.GetChild(2).gameObject.GetComponent<Button>();
        buttonGemsDetailClose = panelGemsDetail.transform.GetChild(2).gameObject.GetComponent<Button>();

        buttonCoinsDetailClose.onClick.AddListener(delegate{ OnClickButtonClose(); } );
        buttonLevelDetailClose.onClick.AddListener(delegate{ OnClickButtonClose(); } );
        buttonChakraDetailClose.onClick.AddListener(delegate{ OnClickButtonClose(); } );
        buttonGemsDetailClose.onClick.AddListener(delegate{ OnClickButtonClose(); } );
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
    }
}
