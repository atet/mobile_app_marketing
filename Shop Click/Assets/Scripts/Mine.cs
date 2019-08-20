using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mine : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI tMProIron;
    [SerializeField] TextMeshProUGUI tMProHide;
    [SerializeField] TextMeshProUGUI tMProWood;
    [SerializeField] TextMeshProUGUI tMProHerbs;
    [SerializeField] TextMeshProUGUI tMProSteel;
    [SerializeField] TextMeshProUGUI tMProOil;
    [SerializeField] TextMeshProUGUI tMProElectricity;
    [SerializeField] TextMeshProUGUI tMProTitanium;
            


    // Start is called before the first frame update
    void Start()
    {
        Resource iron        = new Resource("Iron",        1, 1, 20, 10, tMProIron);
        Resource hide        = new Resource("Hide",        1, 1,  0,  0, tMProHide);
        Resource wood        = new Resource("Wood",        1, 1,  0,  0, tMProWood);
        Resource herbs       = new Resource("Herbs",       1, 1,  0,  0, tMProHerbs);
        Resource steel       = new Resource("Steel",       1, 1,  0,  0, tMProSteel);
        Resource oil         = new Resource("Oil",         1, 1,  0,  0, tMProOil);
        Resource electricity = new Resource("Electricity", 1, 1,  0,  0, tMProElectricity);
        Resource titanium    = new Resource("Titanium",    1, 1,  0,  0, tMProTitanium);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
