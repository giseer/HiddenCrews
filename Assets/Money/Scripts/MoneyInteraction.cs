using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyInteraction : MonoBehaviour
{
    public Text moneyText;
    private int money = 0;
    public int cantidadDeSubidaBajada = 10;
    public KeyCode teclaAumento = KeyCode.E;
    public KeyCode teclaDisminuir = KeyCode.Q;

    public static MoneyInteraction Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        money = PlayerPrefs.GetInt("money");
        UpdateText();
    }

 

    void Update()
    {
        if (Input.GetKeyDown(teclaAumento))
        {           
            money += cantidadDeSubidaBajada;
            AumentarDinero();                       
        }

        if (Input.GetKeyDown(teclaDisminuir))
        {           
            money -= cantidadDeSubidaBajada;
            DisminuirDinero();                       
        }


    }

    void AumentarDinero()
    {        
        money = Mathf.Max(0, money);
        UpdateText();
        PlayerPrefs.SetInt("money", money);
    }

    void DisminuirDinero()
    {       
        money = Mathf.Max(0, money);
        UpdateText();
        PlayerPrefs.SetInt("money", money);       
    }

    private void UpdateText()
    {
        moneyText.text = "Money: " + money + "€";
    }

    [ContextMenu(nameof(BorrarPlayerPrefs))]
    void BorrarPlayerPrefs()
    {
        PlayerPrefs.SetInt("money", 0);
    }
}
