using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{

    //[SerializeField] public Button buttonSettingsReadMe;
    //[SerializeField] public GameObject panelSettingsReadMe;
    [SerializeField] public Button buttonSettingsQuit;

    public void OnClickButtonSettingsReadMe()
    {
        
    }
    public void OnClickButtonSettingsQuit()
    {
        Application.Quit();
    }

    void Awake()
    {
        // Link buttons to funtions
        //buttonSettingsReadMe.onClick.AddListener( delegate{ OnClickButtonSettingsReadMe(); } );
        buttonSettingsQuit.onClick.AddListener( delegate{ OnClickButtonSettingsQuit(); } );
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
