using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button ReturnToMenuButton;

    private void Start()
    {
        ReturnToMenuButton.onClick.AddListener(OnReturnButton);
    }

    private void OnReturnButton()
    {
        SceneManager.LoadSceneAsync("TitleScreen");
    }
}
