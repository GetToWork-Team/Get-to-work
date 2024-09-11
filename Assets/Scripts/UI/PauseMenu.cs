using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _ReturnToMain;

    private void OnEnable()
    {
        _ReturnToMain.onClick.AddListener(OnReturnToMenu);
    }
    private void OnDisable()
    {
        _ReturnToMain.onClick.RemoveAllListeners();
    }

    private void OnReturnToMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    
}
