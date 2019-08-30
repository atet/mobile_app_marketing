using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Thanks GenericEntity
// Adapted from: https://forum.unity.com/threads/how-to-insert-music-into-a-scene-and-have-it-play-on-the-click-of-a-button.328828/
    
[RequireComponent(typeof(AudioSource))] // This tag is not compulsory, but I like to use it because it makes sense. Basically, it will force any Game Object with this script attached to also have an AudioSource component. I want to do this because I will be referencing an AudioSource later, so I want to make sure that one exists.
public class SFX : MonoBehaviour
{
    public static SFX instance;
    private bool globalSFXMute = false;

    [SerializeField] public AudioClip sFXClick, sFXSale, sFXUpcharge, sFXRebate, sFXSuggest, sFXRefuse, sFXNoGo, sFXLevelUp;

    public void PlaySFXClick(){ if(!globalSFXMute){ PlayOneShot(sFXClick); } }
    public void PlaySFXSale(){ if(!globalSFXMute){ PlayOneShot(sFXSale); } }
    public void PlaySFXUpcharge(){ if(!globalSFXMute){ PlayOneShot(sFXUpcharge); } }
    public void PlaySFXRebate(){ if(!globalSFXMute){ PlayOneShot(sFXRebate); } }
    public void PlaySFXSuggest(){ if(!globalSFXMute){ PlayOneShot(sFXSuggest); } }
    public void PlaySFXRefuse(){ if(!globalSFXMute){ PlayOneShot(sFXRefuse); } }
    public void PlaySFXNoGo(){ if(!globalSFXMute){ PlayOneShot(sFXNoGo); } }
    public void PlaySFXLevelUp(){ if(!globalSFXMute){ PlayOneShot(sFXLevelUp); } }

    [SerializeField] public Button buttonSFXSetting;

    private AudioSource _audio; // The reference to my AudioSource (look in the Start() function for more details)

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
            PlaySFXSale();
        }
    }

    void Awake()
    {
        instance = this;
        buttonSFXSetting.onClick.AddListener( delegate{ OnClickSFXSetting(); } );
    }

    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



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
 
    public void PlayOneShot(AudioClip audioClip)
    {  
        _audio.PlayOneShot(audioClip);
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
