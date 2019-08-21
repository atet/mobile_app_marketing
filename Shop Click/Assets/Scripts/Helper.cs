using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class Helper
{
    public static void ButtonEnable(Button button)
    {
        button.interactable = true;
    }
    public static void ButtonDisable(Button button)
    {
        button.interactable = false;
    }
}
