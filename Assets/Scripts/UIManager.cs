using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text _coinText;
    [SerializeField]
    TMP_Text _lifeText;
    [SerializeField]
    GameObject _gameOverMenu;

    public void UpdateCoinText(int num) 
    {
        _coinText.text ="Coin: " + num.ToString();
    }
    
    public void UpdateLifeText(int num)
    {
        _lifeText.text ="Life: " + num.ToString();
    }

    public void ShowGameOverMenu() 
    {
        _gameOverMenu.SetActive(true);
    }

}
