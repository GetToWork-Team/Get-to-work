using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinScreenPanel : MonoBehaviour
{
    public MoneySystem money;

    public TextMeshProUGUI saving;
    public TextMeshProUGUI wage;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI total;

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
        saving.SetText("Economies : " + money.saving.ToString());
        wage.SetText("Argent gagné : " + money.currentMoney.ToString());
        cost.SetText("Argent dépensé : " + money.cost.ToString());
        money.saving += money.currentMoney;
        money.currentMoney = 0f;
        money.saving -= money.cost;
        total.SetText("Total : " + money.saving.ToString());
    }

    private void OnDisable()
    {
        _NextDayButton.onClick.RemoveAllListeners();
    }
}
