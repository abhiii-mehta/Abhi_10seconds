using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public AudioSource bgmSource;
    public float startingPitch = 1f;
    public float maxPitch = 1.5f;

    private void Start()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1)) * 20);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1)) * 20);
    }

   

    

}
