using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class TankHero : MonoBehaviour
{
    /// <summary>
    /// Скрипт Танка. Данный скрипт отвечает за движение танка, жизнь, скорость 
    /// 
    /// </summary>
    public CameraController cameraController;

    // -->    Спайн!   <-- //
   
    public SkeletonAnimation skeletonAnimationTank2;
    

    // -->    Игрок - Персонаж! <-- //
    public float speed; // Скорость танка
    public int hpHero; // Жизнь танка
    public int hpHeroMax; // Максимальное Хп героя
    public Image heartsBar; // Отрисовка жизни
    public float fill;

    public EnemyAmmo enemyAmmo; //Ракета противника

    public WheelJoint2D wheelFront, wheelBack;
    private JointMotor2D backMotor, frontMotor;


    public float SpeedForward; // Скорость танка
    public float SpeedBackward; // Скорость танка
    public float Torque; // Скорость танка



    private void Awake()
    {
        skeletonAnimationTank2 = GetComponent<SkeletonAnimation>();
     
    }


    void Tank()
    {
        //skeletonAnimation.skeleton.SetAttachment("bone11", "bone1"); //Включаем нужный нам скин 
        //skeletonAnimationTank2.skeleton.SetAttachment("bone11", null); //Отключаем  не нужный нам скин 
    }

    void Start()
    {
        hpHeroMax = hpHero;
        fill = 1;
        speed = 3;
        Tank();
        MoveTank(200,400);
    }

    void Update()
    {
        heartsBar.fillAmount = fill;
        HpHero();     
    }

    public void MoveTank(int speedBack, int speedFront)

    {
      SpeedBackward = speedBack;
      SpeedForward = speedFront;

        backMotor.motorSpeed = SpeedBackward;
        frontMotor.motorSpeed = SpeedForward;

        backMotor.maxMotorTorque = Torque;
        frontMotor.maxMotorTorque = Torque;

        wheelFront.motor = frontMotor;
        wheelBack.motor = backMotor;
        //   Vector3 direction = transform.right;
        //    transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    // Игрок получает урон при коллизи
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AmmoEnemy"))
        {

            fill = ((float)hpHero / (float)hpHeroMax);
            hpHero = hpHero - 10;
            LifeHero();                  
        }
        if (collision.gameObject.CompareTag("EnemyTank"))
        {

            fill = ((float)hpHero / (float)hpHeroMax);
            hpHero = hpHero - 100000;
            LifeHero();                  
        }
    }

    void HpHero()
    {
        fill = ((float)hpHero / (float)hpHeroMax);
        if (hpHero > hpHeroMax)
        {
           // fill = hpHeroMax;
            hpHero = hpHeroMax;
           
        }

     

    }
    public void LifeHero()
    {

        if (hpHero <= 0)
        {
            fill = 0;
            Destroy(gameObject);
            cameraController.alive = false;
        }
    }

}
