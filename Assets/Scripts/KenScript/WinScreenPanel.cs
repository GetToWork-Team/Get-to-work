using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinScreenPanel : MonoBehaviour
{
    [SerializeField] private Button _NextDayButton;
    public static UnityEvent onNextDayButton = new UnityEvent();

    private void NextDay()
    {
        onNextDayButton?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _NextDayButton.onClick.AddListener(NextDay);
    }

    private void OnDisable()
    {
        _NextDayButton.onClick.RemoveAllListeners();
    }
}
