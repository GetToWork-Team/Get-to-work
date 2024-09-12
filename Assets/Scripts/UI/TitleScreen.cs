using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Button _PlayButton, _CreditButton, _QuitButton, _SoundButton,_SFXButton;

    private bool _SoundButtonActive = false;
    private bool _SFXButtonActive = false;
    [SerializeField] private GameObject _SoundSliders,_SFXSlider;
    [SerializeField] private Transition _Transition;

    private void Start()
    {
        _PlayButton.onClick.AddListener(OnPlay);
        _CreditButton.onClick.AddListener(OnCredit);
        _QuitButton.onClick.AddListener(OnQuit);
        _SoundButton.onClick.AddListener(OnSoundButton);
        _SFXButton.onClick.AddListener(OnSFXButton);
    }

    private void OnPlay()
    {
        Debug.Log("LoadScene :  GameScene");
        _Transition.gameObject.SetActive(true);
        _Transition.FadeIn();
        StartCoroutine(WaitForTransition());
        
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

    private void OnSFXButton()
    {
        _SFXButtonActive = !_SFXButtonActive;
        if (_SFXButtonActive) {  _SFXSlider.SetActive(true); }
        else { _SFXSlider.SetActive(false);}
    }

    private void OnDisable()
    {
        _PlayButton.onClick.RemoveAllListeners();
        _CreditButton.onClick.RemoveAllListeners();
        _QuitButton.onClick.RemoveAllListeners();
        _SoundButton.onClick.RemoveAllListeners();
        _SFXButton.onClick.RemoveAllListeners();
    }

    private IEnumerator WaitForTransition()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameScene");
    }

    private void OnDestroy()
    {
        StopCoroutine(WaitForTransition());
    }
}
