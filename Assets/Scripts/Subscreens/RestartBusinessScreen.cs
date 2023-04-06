using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class RestartBusinessScreen : MonoBehaviour
{
    [SerializeField] private Coins _coins;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _resetScreen;

    public event Action OnConfirmReset;

    void OnEnable()
    {
        _text.text = _coins.SessionFairies.ToString();
    }

    public void HandleClaimButton()
    {
        if (_coins.SessionFairies >= 1)
        {
            _resetScreen.SetActive(true);
        }
    }

    public void HandleConfirmButton()
    {
        OnConfirmReset?.Invoke();
        _coins.Dollar = 0;
        _coins.AllTimeDollar = 0;
        _coins.Fairies += _coins.SessionFairies;
        _coins.SessionFairies = 0;
        _coins.AllTimeDollar = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
