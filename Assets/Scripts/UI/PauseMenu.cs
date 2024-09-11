using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _ReturnToMain;

    private void OnEnable()
    {
        _ReturnToMain.onClick.AddListener(OnReturnToGame);
    }
    private void OnDisable()
    {
        _ReturnToMain.onClick.RemoveAllListeners();
    }

    private void OnReturnToGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    
}
