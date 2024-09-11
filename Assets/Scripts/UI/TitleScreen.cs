using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] Button PlayButton, CreditButton, QuitButton, SoundButton;

    private void Start()
    {
        PlayButton.onClick.AddListener(OnPlay);
        CreditButton.onClick.AddListener(OnCredit);
        QuitButton.onClick.AddListener(OnQuit);
        SoundButton.onClick.AddListener(OnSoundButton);
    }

    private void OnPlay()
    {
        Debug.Log("LoadScene :  GamePlay");
        SceneManager.LoadSceneAsync("GamePlay");
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

    }

    private void OnDisable()
    {
        PlayButton.onClick.RemoveAllListeners();
        CreditButton.onClick.RemoveAllListeners();
        QuitButton.onClick.RemoveAllListeners();
        SoundButton.onClick.RemoveAllListeners();
    }
}
