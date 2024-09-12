using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    [SerializeField] private Button MenuButton;
    [SerializeField] private Transition _Transition;

    private void Start()
    {
        MenuButton.onClick.AddListener(OnMenu);
    }

    private void OnMenu()
    {
        _Transition.gameObject.SetActive(true);
        _Transition.FadeIn();
        StartCoroutine(WaitForTransition());
    }

    private void OnDisable()
    {
        MenuButton?.onClick.RemoveListener(OnMenu);
    }

    private IEnumerator WaitForTransition()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("TitleScreen");
    }

    private void OnDestroy()
    {
        StopCoroutine(WaitForTransition());
    }
}
