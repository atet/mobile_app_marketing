using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public static UIControl instance;
    [SerializeField] public Button buttonCoinsDetail;
    [SerializeField] public Button buttonLevelDetail;
    [SerializeField] public Button buttonChakraDetail;
    [SerializeField] public Button buttonGemsDetail;
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
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
