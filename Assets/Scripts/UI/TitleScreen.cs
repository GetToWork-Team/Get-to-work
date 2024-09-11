using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] Button PlayButton, CreditButton, QuitButton, SoundButton;

    private bool _SoundButtonActive = false;
    [SerializeField] private GameObject _SoundSliders;

    private void Start()
    {
        PlayButton.onClick.AddListener(OnPlay);
        CreditButton.onClick.AddListener(OnCredit);
        QuitButton.onClick.AddListener(OnQuit);
        SoundButton.onClick.AddListener(OnSoundButton);
    }

    private void OnPlay()
    {
        Debug.Log("LoadScene :  GameScene");
        SceneManager.LoadSceneAsync("GameScene");
    }

    private void OnCredit()
    {
        Debug.Log("LoadScene :  Credit");
        SceneManager.LoadSceneAsync("CreditScene");
    }

    private void OnQuit()
    {
        Application.Quit();
    }

    private void OnSoundButton()
    {
        _SoundButtonActive = !_SoundButtonActive;

        if (_SoundButtonActive ) { _SoundSliders.SetActive(true); }
        else { _SoundSliders.SetActive(false);}
    }

    private void OnDisable()
    {
        PlayButton.onClick.RemoveAllListeners();
        CreditButton.onClick.RemoveAllListeners();
        QuitButton.onClick.RemoveAllListeners();
        SoundButton.onClick.RemoveAllListeners();
    }
}
