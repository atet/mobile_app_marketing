using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

    public static string TimeFormatter(float seconds){

        // Thanks: https://stackoverflow.com/a/5398499
        TimeSpan t = TimeSpan.FromSeconds(seconds);
        //return(time.ToString(@"hh\h\:mm\m\:ss\s"));

    {
        string shortForm = "";
        if (t.Hours > 0)
        {
            shortForm += string.Format("{0}h ", t.Hours.ToString());
        }
        if (t.Minutes > 0)
        {
            shortForm += string.Format("{0}m ", t.Minutes.ToString());
        }
        if (t.Seconds > 0)
        {
            shortForm += string.Format("{0}s", t.Seconds.ToString());
        }
        return shortForm;
    } 
    }


    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }
    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
    [System.Serializable] private class Wrapper<T>
    {
        public T[] Items;
    }
}
