using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// В этом скрипте мы активировать купленные ракеты. 
public class LoadItem : MonoBehaviour
{
   
    [HideInInspector] public int quantGame1; //Ракета 1
    [HideInInspector] public int quantGame2;
    [HideInInspector] public int quantGame3;
    [HideInInspector] public int quantGame4;
    [Tooltip("Кнопки ракет")]
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    [Tooltip("Текст количество ракет")]
    public Text textQuantity1;
    public Text textQuantity2;
    public Text textQuantity3;
    public Text textQuantity4;
    
    public GameObject ShootButton1;
    public GameObject ShootButton2;
    public GameObject ShootButton3;
    public GameObject ShootButton4;

    public int shootIndex1;
    public int shootIndex2;
    public int shootIndex3;
    public int shootIndex4;

    

    public enum ActiveAmmo
    {
        Ammo1,
        Ammo2,
        Ammo3,
        Ammo4,
    }
    private void SetState(ActiveAmmo activeAmmo)
    {
        ShootButton1.SetActive(activeAmmo == ActiveAmmo.Ammo1);
        ShootButton2.SetActive(activeAmmo == ActiveAmmo.Ammo2);
        ShootButton3.SetActive(activeAmmo == ActiveAmmo.Ammo3);
        ShootButton4.SetActive(activeAmmo == ActiveAmmo.Ammo4);
    }
    public void Ammo1()
    {
        SetState(ActiveAmmo.Ammo1);
        shootIndex1 = 1;
        shootIndex2 = 0;
        shootIndex3 = 0;
        shootIndex4 = 0;
    }
    public void Ammo2()
    {
        SetState(ActiveAmmo.Ammo2);
        shootIndex1 = 0;
        shootIndex2 = 2;
        shootIndex3 = 0;
        shootIndex4 = 0;
    }
    public void Ammo3()
    {
        SetState(ActiveAmmo.Ammo3);
        shootIndex1 = 0;
        shootIndex2 = 0;
        shootIndex3 = 3;
        shootIndex4 = 0;
    }
    public void Ammo4()
    {
        shootIndex1 = 0;
        shootIndex2 = 0;
        shootIndex3 = 0;
        shootIndex4 = 4;
        SetState(ActiveAmmo.Ammo4);
        
    }
    void Start()
    {
        Ammo1();
        LoadQuantity();
    }

  
    
    public void LoadQuantity() //Загружаем данные из магазина и выводим их в игровую сцену! 
    {
        //Узнгаем сколько ракет у нас есть. 
        quantGame1 = PlayerPrefs.GetInt("keyQuantity1", 0);
        quantGame2 = PlayerPrefs.GetInt("keyQuantity2", 0);
        quantGame3 = PlayerPrefs.GetInt("keyQuantity3", 0);
        quantGame4 = PlayerPrefs.GetInt("keyQuantity4", 0);
        //Обновляем цифры количество ракет в самом начале. 
        textQuantity1.text = quantGame1.ToString();
        textQuantity2.text = quantGame2.ToString();
        textQuantity3.text = quantGame3.ToString();
        textQuantity4.text = quantGame4.ToString();
        //Проверяем ракеты больше 0, если да то активируем кнопку. 
        if (quantGame1 > 0) button1.SetActive(true);
        if (quantGame2 > 0) button2.SetActive(true);
        if (quantGame3 > 0) button3.SetActive(true);
        if (quantGame4 > 0) button4.SetActive(true);
    }
}

