using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Thanks GenericEntity
// Adapted from: https://forum.unity.com/threads/how-to-insert-music-into-a-scene-and-have-it-play-on-the-click-of-a-button.328828/
    
[RequireComponent(typeof(AudioSource))] // This tag is not compulsory, but I like to use it because it makes sense. Basically, it will force any Game Object with this script attached to also have an AudioSource component. I want to do this because I will be referencing an AudioSource later, so I want to make sure that one exists.
public class BGM : MonoBehaviour
{
    private bool globalBGMMute = false;
    [SerializeField] public AudioClip bGM1, bGM2, bGM3, bGM4; // The music clip you want to play. The [SerializeField] tag specifies that this variable is viewable in Unity's inspector. I prefer not to use public variables if I can get away with using private ones.
    [SerializeField] public Button buttonBGM1, buttonBGM2, buttonBGM3, buttonBGM4, buttonBGMSetting;
    private AudioSource _audio; // The reference to my AudioSource (look in the Start() function for more details)
 
    public void OnClickBGM1()
    {
        buttonBGM1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold | FontStyles.Underline;
        buttonBGM1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(255, 255, 255, 255);
        buttonBGM2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        buttonBGM2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);
        buttonBGM3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        buttonBGM3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);
        buttonBGM4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        buttonBGM4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);

        _audio.clip = bGM1;

        if(!globalBGMMute){ PlayMusic(); }

    }
    public void OnClickBGM2()
    {
        buttonBGM1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        buttonBGM1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);
        buttonBGM2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold | FontStyles.Underline;
        buttonBGM2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(255, 255, 255, 255);
        buttonBGM3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        buttonBGM3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);
        buttonBGM4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        buttonBGM4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);

        _audio.clip = bGM2;

        if(!globalBGMMute){ PlayMusic(); }
    }
    public void OnClickBGM3()
    {
        buttonBGM1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        buttonBGM1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);
        buttonBGM2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        buttonBGM2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);
        buttonBGM3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold | FontStyles.Underline;
        buttonBGM3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(255, 255, 255, 255);
        buttonBGM4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        buttonBGM4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);

        _audio.clip = bGM3;

        if(!globalBGMMute){ PlayMusic(); }
    }
    public void OnClickBGM4()
    {
        buttonBGM1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        buttonBGM1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);
        buttonBGM2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        buttonBGM2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);
        buttonBGM3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        buttonBGM3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);
        buttonBGM4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold | FontStyles.Underline;
        buttonBGM4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(255, 255, 255, 255);

        _audio.clip = bGM4;

        if(!globalBGMMute){ PlayMusic(); }
    }

    public void OnClickBGMSetting()
    {
        if(_audio.isPlaying)
        {
            globalBGMMute = true;
            _audio.Stop();
            buttonBGMSetting.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Music: Off";
        }
        else
        {
            globalBGMMute = false;
            _audio.Play();
            buttonBGMSetting.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Music: On";
        }
    }

    /*********************/
    /* Protected Mono Methods */
    /*********************/
    void Awake()
    {
        // Link buttons to funtions
        buttonBGM1.onClick.AddListener( delegate{ OnClickBGM1(); } );
        buttonBGM2.onClick.AddListener( delegate{ OnClickBGM2(); } );
        buttonBGM3.onClick.AddListener( delegate{ OnClickBGM3(); } );
        buttonBGM4.onClick.AddListener( delegate{ OnClickBGM4(); } );
        buttonBGMSetting.onClick.AddListener( delegate{ OnClickBGMSetting(); } );
    }
    protected void Start()
    {
        // Get my AudioSource component and store a reference to it in _audio
        // The point of doing this is because GetComponent() is expensive for computer resources
        // So if we can get away with only calling it one time at the start, then let's do that.
        // From this point on, we can refer to our AudioSource through _audio, which makes the computer happier than GetComponent.
        _audio = GetComponent<AudioSource>();
 
        // We set the audio clip to play as your background music.
        _audio.clip = bGM1;
        buttonBGM1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold | FontStyles.Underline;
        buttonBGM2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);
        buttonBGM3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);
        buttonBGM4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().faceColor = new Color32(128, 128, 128, 255);
        PlayMusic();
    }
 
    /*********************/
    /* Public Interface */
    /*********************/
 
    public void PlayPauseMusic()
    {
        // Check if the music is currently playing.
        if(_audio.isPlaying)
            _audio.Pause(); // Pause if it is
        else
            _audio.Play(); // Play if it isn't
    }
 
    public void PlayStop()
    {
        if(_audio.isPlaying)
            _audio.Stop();
        else
            _audio.Play();
    }
 
    public void PlayMusic()
    {  
        _audio.Play();
    }
 
    public void StopMusic()
    {
        _audio.Stop();
    }
 
    public void PauseMusic()
    {
        _audio.Pause();
    }
}