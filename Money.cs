using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{

    //Игровая валюта
    public static int Coins;
    public static int Carrot;
    public Text textCoins;
    public Text textCube;

    private void Start()
    {
        Coins += 100000;
        Carrot += 50000;

        Coins = PlayerPrefs.GetInt("keyCoin", Coins);
        Carrot = PlayerPrefs.GetInt("keyCarrot", Carrot);
        textCoins.text = Coins.ToString();
        textCube.text = Carrot.ToString();


    }

   public void TestBuy()
    {
        Coins += 10;
        Carrot += 5;
        PlayerPrefs.SetInt("keyCoin", Coins);
        PlayerPrefs.SetInt("keyCarrot", Carrot);
        textCoins.text = Coins.ToString();
        textCube.text = Carrot.ToString();
    

    }

}
