using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings instance;

    public Slider musicSlider;
    public Slider sfxSlider;

    public float musicValue;
    public float sfxValue;

    void Start()
    {
        Load();
    }

    public void OnMusicSliderChange()
    {
        SoundManager.instance.musicVolume = musicValue = musicSlider.value;
        PlayerPrefs.SetFloat("musicValue", musicValue);
        PlayerPrefs.Save();
    }

    public void OnSfxSliderChange()
    {
        SoundManager.instance.sfxVolume = sfxValue = sfxSlider.value;
        PlayerPrefs.SetFloat("sfxValue", sfxValue);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        SoundManager.instance.musicVolume = musicValue = PlayerPrefs.GetFloat("musicValue", 1f);
        SoundManager.instance.sfxVolume = sfxValue = PlayerPrefs.GetFloat("sfxValue", 1f);

        if(musicSlider)
            musicSlider.value = musicValue;
        if (sfxSlider)
            sfxSlider.value = sfxValue;
    }
}