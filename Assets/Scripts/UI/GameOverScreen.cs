using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button ReturnToMenuButton;

    private void Start()
    {
        SoundManager.instance.PlayOneShootSound(SoundReference.instance.sfx_Lose, new Vector2(0, 0));
        ReturnToMenuButton.onClick.AddListener(OnReturnButton);
    }

    private void OnReturnButton()
    {
        gameObject.SetActive(false);
        SceneManager.LoadSceneAsync("TitleScreen");
    }
}
