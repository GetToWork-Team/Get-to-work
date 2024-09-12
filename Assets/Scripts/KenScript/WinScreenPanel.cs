using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinScreenPanel : MonoBehaviour
{
    public MoneySystem money;

    public TextMeshProUGUI saving;
    public TextMeshProUGUI wage;
    public TextMeshProUGUI intoxRep;
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

        if (money.intoxPercentage < 0f)
        {
            money.intoxPercentage = 0f;
        }

        saving.SetText("Economies : " + money.saving.ToString());
        wage.SetText("Argent gagné : " + money.currentMoney.ToString());
        intoxRep.SetText("Intox Réputation : " + money.intoxPercentage.ToString() + " %");
        money.saving += money.currentMoney;
        money.currentMoney = 0f;
        total.SetText("Total : " + money.saving.ToString());

        if (money.intoxPercentage >= 100f)
        {
            // TO DO : GAME OVER
        }

    }

    private void OnDisable()
    {
        _NextDayButton.onClick.RemoveAllListeners();
    }
}
