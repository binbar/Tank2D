using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class EnemyTank : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;


    private float speedEnemy;
    public float hpEnemy;
    public TankHero tankHero;
    public Score score;
    public int scoreEnemy;
    public GameObject floatingDamage;
    private int scoreText;
    public EnemyTank enemyTank;
    public GameObject boom;
    public GameObject weapon;
    public GameObject coin;
    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();

    }


    void Tank()
    {
        // skeletonAnimation.skeleton.SetAttachment("bone11", "bone1"); //Включаем нужный нам скин 
        //skeletonAnimation.skeleton.SetAttachment("bone11", null); //Отключаем  не нужный нам скин 
    }

    void Start()
    {
        hpEnemy = 100;
        speedEnemy = 2;
        scoreEnemy = 150;
        Tank();
    }

    void Update()
    {
        MoveTank();
       
    }

    void MoveTank()
    {

        Vector3 direction = -transform.right;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speedEnemy * Time.deltaTime);

    }

    void LifeEnemy()
    {
       
        if (hpEnemy <= 0)
        {
            GetComponent<MeshRenderer>().enabled = (false); // Отключаем компонент MeshRenderer с нашего обьекта
            GetComponent<BoxCollider2D>().enabled = (false); // Отключаем компонент MeshRenderer с нашего обьекта
            speedEnemy = 0;
            weapon.SetActive(false);
            boom.SetActive(true);
            score.Hit();
            Debug.Log(score.scoreHero + "очки");
            Invoke(nameof(DestroyTankEnemy), 1.30f);
           // Instantiate(coin, transform.position, Quaternion.identity);
            Vector2 coinPos = new Vector2(transform.position.x -1f, transform.position.y + 2.5f);
            var go = Instantiate(coin, coinPos, Quaternion.identity);
            
            GetComponent<EnemyTank>().enabled = (false);
        }
        if (hpEnemy > 0)
        {
            score.Hit();
        }
    }

    public void DestroyTankEnemy()
    {
        Destroy(gameObject);
    }
    //Вылетающий текс после смерти танка врага!
    public void FloatingDamageText()
    {
    
     //   scoreText = scoreEnemy * score.hitAmmo;
        Vector2 damagePos = new Vector2(transform.position.x -1f, transform.position.y + 7.0f);
        var go = Instantiate(floatingDamage, damagePos, Quaternion.identity);
        go.GetComponent<TextMesh>().text = score.scoreHero.ToString();
       // floatingDamage.GetComponentInChildren<FloatingDamage>().damage = scoreText;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ammo1"))
        {
            hpEnemy = hpEnemy - 50;
            score.ScoreEnemy();
            FloatingDamageText();
            LifeEnemy();
        }
        if (collision.gameObject.CompareTag("Ammo2"))
        {
            hpEnemy = hpEnemy - 50;
            score.ScoreEnemy();
            FloatingDamageText();
            LifeEnemy();
        }
        if (collision.gameObject.CompareTag("Ammo3"))
        {
            hpEnemy = hpEnemy - 50;
            score.ScoreEnemy();
            FloatingDamageText();
            LifeEnemy();
        }
        if (collision.gameObject.CompareTag("Ammo4"))
        {
            hpEnemy = hpEnemy - 50;
            score.ScoreEnemy();
            FloatingDamageText();
            LifeEnemy();
        }
        if (collision.gameObject.CompareTag("HeroTank"))
        {
            hpEnemy = hpEnemy - 5000;
            score.ScoreEnemy();
            FloatingDamageText();
            LifeEnemy();
        }
    }


}
