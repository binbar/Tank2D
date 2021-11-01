using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
 //   public const string Hited = "Hited";
//public const string Misseds = "Misseds";

    [HideInInspector] public int hitAmmo;
    [HideInInspector] public int scoreHero;
    [HideInInspector] public int sumScoreHero;
    [HideInInspector] public int scoreTopHero;

    [SerializeField] private Transform transformEnemy;

    [SerializeField] private EnemyTank enemyTank;
    [SerializeField] private GameObject floatingDamage;

    [Header("Работа с Текстом")]
    [SerializeField] private Text textHit; // Точное попадание в противника 
    [SerializeField] private Text textScore; // Счетчик очков
    [SerializeField] private Text textScoreTopHero; // Максимальное количество очков набранного за один раунд
    [SerializeField] private Text textSumScoreHero; // Максимальное количество очков набранного за один раунд



    private void Awake()
    {
      //  Messenger.AddListener(Score.Hited, Hit);
       // Messenger.AddListener(Score.Misseds, Missed);
        hitAmmo = 1;
        TopScore();
        //scoreHero = enemyTank.scoreEnemy;

    }

    public void ScoreEnemy()
    {
        scoreHero = enemyTank.scoreEnemy * hitAmmo;
        sumScoreHero += scoreHero;

        textSumScoreHero.text = sumScoreHero.ToString();
        TopScore();
//        Debug.Log(scoreHero + "Скрипт Score");
    }
    public void Hit()
    {
        if (hitAmmo >= 1) hitAmmo++;
        textHit.text = hitAmmo.ToString();
        TopScore();

    }
    public void Missed()
    {
        if (hitAmmo > 1) hitAmmo--;
        textHit.text = hitAmmo.ToString();
        TopScore();
    }

    public void TopScore()
    {
    //    Debug.Log("ScoreHero " + scoreHero);
     //   Debug.Log("ScoreTOP   " + scoreTopHero);

        scoreTopHero = PlayerPrefs.GetInt("keyScoreTopHero", scoreTopHero); // Узнаем сохраненные данные. 
        textScoreTopHero.text = scoreTopHero.ToString();
        if (sumScoreHero > scoreTopHero)
        {
            scoreTopHero = sumScoreHero;
            PlayerPrefs.SetInt("keyScoreTopHero", scoreTopHero); // Сохраняем значение
            textScoreTopHero.text = scoreTopHero.ToString();
        }
    }
   


}
