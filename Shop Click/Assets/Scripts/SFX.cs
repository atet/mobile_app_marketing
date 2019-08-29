using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SFX : MonoBehaviour
{
    private bool globalSFXMute = false;
    [SerializeField] public Button buttonSFXSetting;

    public void OnClickSFXSetting()
    {
        if(!globalSFXMute)
        {
            globalSFXMute = true;
            buttonSFXSetting.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SFX: Off";
        }
        else
        {
            globalSFXMute = false;
            buttonSFXSetting.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SFX: On";
        }
    }

    void Awake()
    {
        buttonSFXSetting.onClick.AddListener( delegate{ OnClickSFXSetting(); } );
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
