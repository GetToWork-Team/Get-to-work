using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ditacticiel : MonoBehaviour
{
    [SerializeField] private Button _CloseButton;
    private Animator _DitacticielAnimator;


    private void Start()
    {
        _DitacticielAnimator = GetComponent<Animator>();
        _DitacticielAnimator.SetTrigger("SetMove");
        _CloseButton.onClick.AddListener(OnCloseButton);
    }

    private void OnCloseButton()
    {
        gameObject.SetActive(false);
        GameManager.startGameEvent?.Invoke();
        Destroy(gameObject);
    }
}