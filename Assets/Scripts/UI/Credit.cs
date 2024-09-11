using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    [SerializeField] private Button MenuButton;

    private void Start()
    {
        MenuButton.onClick.AddListener(OnMenu);
    }

    private void OnMenu()
    {
        SceneManager.LoadSceneAsync("TitleScreen");
    }

    private void OnDisable()
    {
        MenuButton?.onClick.RemoveListener(OnMenu);
    }
}
