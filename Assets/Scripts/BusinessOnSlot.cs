using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class BusinessOnSlot : MonoBehaviour
{
    [SerializeField] private Business _business;
    [SerializeField] private Coins _coins;
    [SerializeField] private Image _businessImage;
    [SerializeField] private Slider _slider;
    [SerializeField] private RestartBusinessScreen _restartBusinessScreen;

    private float _returnTime;
    private float _timer;
    private bool _canProfit = false;

    [Header("Texts")]
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _incomeText;
    [SerializeField] private TMP_Text _upgradeCostText;
    [SerializeField] private TMP_Text _levelText;

    private void OnEnable()
    {
        _restartBusinessScreen.OnConfirmReset += RestartAllBusinesses;
    }

    private void OnDisable()
    {
        _restartBusinessScreen.OnConfirmReset -= RestartAllBusinesses;
    }

    void Start()
    {
        SetBusinessOnSlot();
    }

    void FixedUpdate()
    {
        if (_business.Level >= 1)
        {
            if (_canProfit || _business.HasPurchasedManager)
            {
                TimeToProfit();
            }
        }
        else
        {
            _canProfit = false;
        }

        UpdateMoneyText();
    }

    private void SetBusinessOnSlot()
    {
        _businessImage.sprite = _business.Sprite;
        _timer = _business.ReturnTime;

        if (_business.Income == 0)
        {
            _business.Income = _business.InitialIncome;
        }
    }

    private void TimeToProfit()
    {
        _slider.maxValue = _business.ReturnTime;
        _returnTime += 1 * Time.deltaTime;
        _timer -= 1 * Time.deltaTime;
        _slider.value = _returnTime;

        if (_returnTime >= _slider.maxValue)
        {
            _returnTime = 0;
            _timer = _business.ReturnTime;
            _coins.Dollar += _business.Income;
            _coins.AllTimeDollar += _business.Income;

            UpdateMoneyText();

            _canProfit = false;
            _slider.value = _slider.minValue;

            CheckAllTimeDollar();
        }

        StringTimerNumbers();
    }

    private void StringTimerNumbers()
    {
        int hours = (int)_timer / 3600;
        int minutes = ((int)_timer / 60) % 60;
        int seconds = (int)_timer % 60;

        _timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    private void CheckAllTimeDollar()
    {
        _coins.SessionFairies = _coins.AllTimeDollar >= 20000000 ? 6 :
        _coins.AllTimeDollar >= 12000000 ? 4 :
        _coins.AllTimeDollar >= 7000000 ? 3 :
        _coins.AllTimeDollar >= 3000000 ? 2 :
        _coins.AllTimeDollar >= 1000000 ? 1 : 0;
    }

    private void UpdateMoneyText()
    {
        _incomeText.text = _business.Income.ToString("C2", CultureInfo.CreateSpecificCulture("en-US"));
        _levelText.text = _business.Level.ToString();

        UpdateCostOfUpgrade();

        if (_coins.Dollar < 100000000)
        {
            _moneyText.text = _coins.Dollar.ToString("C2", CultureInfo.CreateSpecificCulture("en-US"));
        }
        else if (_coins.Dollar >= 100000000 && _coins.Dollar < 100000000000)
        {
            double moneyMillion = _coins.Dollar / 1000000f;
            _moneyText.text = moneyMillion.ToString("C2", CultureInfo.CreateSpecificCulture("en-US")) + " Millions";
        }
        else if (_coins.Dollar >= 100000000000)
        {
            double moneyBillion = _coins.Dollar / 1000000000f;
            _moneyText.text = moneyBillion.ToString("C2", CultureInfo.CreateSpecificCulture("en-US")) + " Billion";
        }
    }

    private void UpdateCostOfUpgrade()
    {
        if (_business.UpgradeCost < 1000000)
        {
            _upgradeCostText.text = _business.UpgradeCost.ToString("C2", CultureInfo.CreateSpecificCulture("en-US"));
        }
        else if (_business.UpgradeCost >= 1000000 && _business.UpgradeCost < 1000000000)
        {
            double moneyMillion = _business.UpgradeCost / 1000000f;
            _upgradeCostText.text = moneyMillion.ToString("C2", CultureInfo.CreateSpecificCulture("en-US")) + " M";
        }
        else if (_business.UpgradeCost >= 1000000000)
        {
            double moneyBillion = _business.UpgradeCost / 1000000000f;
            _upgradeCostText.text = moneyBillion.ToString("C2", CultureInfo.CreateSpecificCulture("en-US")) + " B";
        }
    }

    public void Upgrade()
    {
        if (_coins.Dollar >= _business.UpgradeCost && _business.Level >= 1)
        {
            _coins.Dollar -= _business.UpgradeCost;
            _business.UpgradeCost *= 1.12f;
            _business.Level += 1;
            _business.Income += _business.InitialIncome;

            if (_business.Level >= _business.LevelToGoal)
            {
                NextFloor();
            }
            
            UpdateMoneyText();

            return;
        }
        else if (_coins.Dollar >= _business.UpgradeCost && _business.Level == 0)
        {
            _business.Level += 1;
        }
    }

    private void NextFloor()
    {
        _business.LevelToGoal += 25;
        _business.ReturnTime *= 0.8f;
        _business.Income *= 1.25f;
    }

    private void RestartAllBusinesses()
    {
        _business.Level = _business.InitialLevel;
        _business.Income = _business.InitialIncome;
        _business.ReturnTime = _business.InitialReturnTime;
        _business.UpgradeCost = _business.InitialUpgradeCost;
        _business.LevelToGoal = 25;
    }

    public void HandleProfitButton()
    {
        _canProfit = true;
    }
}
