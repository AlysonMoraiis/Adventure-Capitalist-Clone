using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagersScreen : MonoBehaviour
{
    [Header("Buy buttons")]
    [SerializeField] private Button _almsBuyButton;
    [SerializeField] private Button _hotdogBuyButton;
    [SerializeField] private Button _storeBuyButton;
    [SerializeField] private Button _gymBuyButton;

    [Header("Text buttons")]
    [SerializeField] private TMP_Text _almsText;
    [SerializeField] private TMP_Text _hotdogText;
    [SerializeField] private TMP_Text _storeText;
    [SerializeField] private TMP_Text _gymText;

    [Header("Others")]
    [SerializeField] private Coins _coins;

    [SerializeField] private TMP_Text _fairiesText;
    [SerializeField] private Business _almsBusiness;
    [SerializeField] private Business _hotdogBusiness;
    [SerializeField] private Business _storeBusiness;
    [SerializeField] private Business _gymBusiness;

    void OnEnable()
    {
        UpdateFairiesText();
        CheckIfHasPurchased();
    }

    public void HandleAlmsBusinessButton()
    {
        if (_coins.Fairies >= 1)
        {
            _coins.Fairies -= 1;
            _almsBusiness.HasPurchasedManager = true;
            CheckIfHasPurchased();
            UpdateFairiesText();
        }
    }

    public void HandleHotdogBusinessButton()
    {
        if (_coins.Fairies >= 2)
        {
            _coins.Fairies -= 2;
            _hotdogBusiness.HasPurchasedManager = true;
            CheckIfHasPurchased();
            UpdateFairiesText();
        }
    }

    public void HandleStoreBusinessButton()
    {
        if (_coins.Fairies >= 4)
        {
            _coins.Fairies -= 4;
            _storeBusiness.HasPurchasedManager = true;
            CheckIfHasPurchased();
            UpdateFairiesText();
        }
    }

    public void HandleGymBusinessButton()
    {
        if (_coins.Fairies >= 6)
        {
            _coins.Fairies -= 6;
            _gymBusiness.HasPurchasedManager = true;
            CheckIfHasPurchased();
            UpdateFairiesText();
        }
    }

    private void UpdateFairiesText()
    {
        _fairiesText.text = _coins.Fairies.ToString();
    }

    private void CheckIfHasPurchased()
    {
        if (_almsBusiness.HasPurchasedManager == true)
        {
            _almsText.text = "already purchased";
            _almsBuyButton.interactable = false;
        }

        if (_hotdogBusiness.HasPurchasedManager == true)
        {
            _hotdogText.text = "already purchased";
            _hotdogBuyButton.interactable = false;
        }

        if (_storeBusiness.HasPurchasedManager == true)
        {
            _storeText.text = "already purchased";
            _storeBuyButton.interactable = false;
        }

        if (_gymBusiness.HasPurchasedManager == true)
        {
            _gymText.text = "already purchased";
            _gymBuyButton.interactable = false;
        }
    }
}
