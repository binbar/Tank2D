using Spine;
using Spine.Unity;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Зажатая кнопка
    public bool isRacePressed;
    public bool isbrakePressed;
    //Старт Спайн
    [Tooltip("Ссылка на сам скелет и обьект Спайна")]
    public SkeletonAnimation skeletonAnimationTank2;
    [Tooltip("Анимации Танка 2")]
    public AnimationReferenceAsset idleTank2, shootTank2, drivingTank2;
    [Tooltip("Кость нашего танка")]
    public SkeletonUtilityBone skeletonUtilityBoneTank2;
    
    public string currentStateTank2;
    public CharacterControllerAnim character;
    
    private Bone bone5; //обращаемся к костью 5 

    public Transform shotDir;
    
    [Tooltip("Ракеты")]//Наши ракеты
    

    public Ammo1 _ammo1;
    public Ammo2 _ammo2;
    public Ammo3 _ammo3;
    public Ammo4 _ammo4;

    
    private bool shootOn;
    private bool onAmmo;
    private bool up;
    public TankHero tankHero;

    private float _delayShootTime = 0.5f;
    
    public GameObject point1;
    public GameObject point2;
    public GameObject flashDulo;
    public GameObject thoughCloudGO;
    public ThoughCloud thoughCloud;
    public LoadItem loadItem;



    private void Start()
    {
        up = true;
        point1.SetActive(false);
        point2.SetActive(false);
        currentStateTank2 = "driving";
        SetStateTank2(currentStateTank2);
        skeletonUtilityBoneTank2.overrideAlpha = 0;
        bone5 = skeletonAnimationTank2.Skeleton.FindBone("bone5");

    }
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        skeletonAnimationTank2.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
    }
    public void SetStateTank2(string state)
    {
        if (state.Equals("shoot"))
        {
            SetAnimation(shootTank2, true, 1f);
        }
        if (state.Equals("driving"))
        {
            SetAnimation(drivingTank2, true, 1f);
        }
        if (state.Equals("Idle"))
        {
            SetAnimation(idleTank2, true, 1f);
        }
    }




    private void Update()
    {
        // if (Input.touchCount > 0) Swipe();
        MouseStatusChanger();
        // Выставляем силу выстрела снаряда (скорость)
        ForseDulo();
    }
    private void Ammo()
    {
        point1.SetActive(false);
        point2.SetActive(false);
        flashDulo.SetActive(true);
        
        if (loadItem.shootIndex1 == 1)
        {
            Ammo1 ammo1 = Instantiate(_ammo1, shotDir.position, Quaternion.identity);
            ammo1.Setup(shotDir.up);
            Debug.Log("1 кнопка");

        }
        if (loadItem.shootIndex2 == 2)
        {
            Ammo2 ammo2 = Instantiate(_ammo2, shotDir.position, Quaternion.identity);
            ammo2.Setup(shotDir.up); // желательно через gameobject создавать
           
        }
        if (loadItem.shootIndex3 == 3)
        {
            Ammo3 ammo3 = Instantiate(_ammo3, shotDir.position, Quaternion.identity);
            ammo3.Setup(shotDir.up);
            Debug.Log("3 кнопка");

        }
        if (loadItem.shootIndex4 == 4)
        {
            Ammo4 ammo4 = Instantiate(_ammo4, shotDir.position, Quaternion.identity);
            ammo4.Setup(shotDir.up);
            
            Debug.Log("4 кнопка");

        }
        
        character.DrivingHero();
        tankHero.MoveTank(400,300);
        SetAnimation(drivingTank2, true, 1f);
        Invoke(nameof(DuloStartPois), 0.3f);
        thoughCloudGO.SetActive(false);
        thoughCloud.NoActiveText();
    }
    void DuloStartPois()
    { 
        point1.SetActive(false);
        point2.SetActive(false);
        skeletonUtilityBoneTank2.overrideAlpha = 0.01f;
        tankHero.speed = 2;
        shootOn = false;
        onAmmo = false;
        flashDulo.SetActive(false);
    }

    public void MouseStatusChanger()
    {
        if (isRacePressed && shootOn == false && onAmmo) //Три проверки. 
        {
            thoughCloudGO.SetActive(true);   
        tankHero.MoveTank(0,0); // Остановливаем танк
        character.IdleHero();  // Включаем анимацию козла Idle 
        SetAnimation(idleTank2, true, 1f); // Включаем анимацию танка 

        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.95f && up)
        {
          //  Debug.Log("Поднимаем " + skeletonUtilityBoneTank2.overrideAlpha );
            skeletonUtilityBoneTank2.overrideAlpha += 0.1f * Time.deltaTime / 0.1f; // Поднимаем дуло танка 
            if (skeletonUtilityBoneTank2.overrideAlpha >= 0.95)
            {
                Debug.Log("АП= " + up);
                up = false;
            }
        }

        if (up == false)
        {
            up = false;
            skeletonUtilityBoneTank2.overrideAlpha -= 0.1f * Time.deltaTime / 0.1f; // Поднимаем дуло танка 
        //    Debug.Log("Тут" + skeletonUtilityBoneTank2.overrideAlpha);
            if (skeletonUtilityBoneTank2.overrideAlpha <= 0.1)
            {
                up = true;
            }
        }
        }
    }

    public void SwipeUP() //Если проводим пальцем вверх
    {
       
        tankHero.MoveTank(0,0);
        character.IdleHero();
        SetAnimation(idleTank2, true, 1f);
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.95f && up)
        {
            //  Debug.Log("Поднимаем " + skeletonUtilityBoneTank2.overrideAlpha );
            skeletonUtilityBoneTank2.overrideAlpha += 0.1f * Time.deltaTime / 0.1f; // Поднимаем дуло танка 
            if (skeletonUtilityBoneTank2.overrideAlpha >= 0.95)
            {
                Debug.Log("АП= " + up);
                up = false;
            }
        }

        if (up == false)
        {
            up = false;
            skeletonUtilityBoneTank2.overrideAlpha -= 0.1f * Time.deltaTime / 0.1f; // Поднимаем дуло танка 
            //    Debug.Log("Тут" + skeletonUtilityBoneTank2.overrideAlpha);
            if (skeletonUtilityBoneTank2.overrideAlpha <= 0.1)
            {
                up = true;
            }
        }
       //skeletonUtilityBoneTank2.overrideAlpha += 0.1f * Time.deltaTime / 0.1f;
      //  Debug.Log("Палец Вверх");

    }

    public void SwipeDown() //Если проводим пальцем вниз
    {
        tankHero.MoveTank(0,0);
        character.IdleHero();
        SetAnimation(idleTank2, true, 1f);
      //  skeletonUtilityBoneTank2.overrideAlpha += -0.1f * Time.deltaTime / 0.1f;
//        Debug.Log("Палец Внизу");

    }

    public void SwipeGet() //Отпускаем палец
    {
        
        character.ShootHero();
        SetAnimation(shootTank2, true, 1f);
        Invoke(nameof(Ammo), _delayShootTime);
       // Debug.Log("Отпускаем!");
    }
    
    
  


    public void onPointerDownRaceButton()
    {
       
        isRacePressed = true;
       onAmmo = true;
    }

    public void onPointerUpRaceButton()
    {

        if (shootOn == false && isRacePressed)
        {
            isRacePressed = false;
            shootOn = true;

            character.ShootHero();
            SetAnimation(shootTank2, true, 1f);
            Invoke(nameof(Ammo), _delayShootTime);
        }
     
    }
    
    public void ForseDulo() //Даем силу для выстрела. 
    {
      
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.1f && skeletonUtilityBoneTank2.overrideAlpha >= 0f)
        {
            _ammo1._force = 800;
            _ammo2._force = 800;
            _ammo3._force = 800;
            _ammo4._force = 800;
           
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.15f && skeletonUtilityBoneTank2.overrideAlpha >= 0.1f)
        {
            _ammo1._force = 1000;
            _ammo2._force = 1000;
            _ammo3._force = 1000;
            _ammo4._force = 1000;
            point1.SetActive(true);
        }
        
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.2f && skeletonUtilityBoneTank2.overrideAlpha >= 0.15f)
        {
            _ammo1._force = 1200;
            _ammo2._force = 1200;
            _ammo3._force = 1200;
            _ammo4._force = 1200;
            point1.SetActive(true);
            
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.25f && skeletonUtilityBoneTank2.overrideAlpha >= 0.2f)
        {
            _ammo1._force = 1400;
            _ammo2._force = 1400;
            _ammo3._force = 1400;
            _ammo4._force = 1400;

            point1.SetActive(true);
            point2.SetActive(true);
         
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.3f && skeletonUtilityBoneTank2.overrideAlpha >= 0.25f)
        {
            _ammo1._force = 1600;
            _ammo2._force = 1600;
            _ammo3._force = 1600;
            _ammo4._force = 1600;

            point1.SetActive(true);
            point2.SetActive(true);
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.35f && skeletonUtilityBoneTank2.overrideAlpha >= 0.3f)
        {
            _ammo1._force = 1800;
            _ammo2._force = 1800;
            _ammo3._force = 1800;
            _ammo4._force = 1800;
            point1.SetActive(true);
            point2.SetActive(true);
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.4f && skeletonUtilityBoneTank2.overrideAlpha >= 0.35f)
        {
            _ammo1._force = 2000;
            _ammo2._force = 2000;
            _ammo3._force = 2000;
            _ammo4._force = 2000;
            point1.SetActive(true);
            point2.SetActive(true);
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.45f && skeletonUtilityBoneTank2.overrideAlpha >= 0.4f)
        {
            
            _ammo1._force = 2200;
            _ammo2._force = 2200;
            _ammo3._force = 2200;
            _ammo4._force = 2200;

            
            point1.SetActive(true);
            point2.SetActive(true);
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.5f && skeletonUtilityBoneTank2.overrideAlpha >= 0.45f)
        {
            _ammo1._force = 2400;
            _ammo2._force = 2400;
            _ammo3._force = 2400;
            _ammo4._force = 2400;
            point1.SetActive(true);
            point2.SetActive(true);
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.55f && skeletonUtilityBoneTank2.overrideAlpha >= 0.5f)
        {
            _ammo1._force = 2600;
            _ammo2._force = 2600;
            _ammo3._force = 2600;
            _ammo4._force = 2600;

            
            point1.SetActive(true);
            point2.SetActive(true);
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.6f && skeletonUtilityBoneTank2.overrideAlpha >= 0.55f)
        {
            _ammo1._force = 2800;
            _ammo2._force = 2800;
            _ammo3._force = 2800;
            _ammo4._force = 2800;

            point1.SetActive(true);
            point2.SetActive(true);
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.65f && skeletonUtilityBoneTank2.overrideAlpha >= 0.6f)
        {
            _ammo1._force = 3000;
            _ammo2._force = 3000;
            _ammo3._force = 3000;
            _ammo4._force = 3000;

            point1.SetActive(true);
            point2.SetActive(true);
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.7f && skeletonUtilityBoneTank2.overrideAlpha >= 0.65f)
        {
            _ammo1._force = 3200;
            _ammo2._force = 3200;
            _ammo3._force = 3200;
            _ammo4._force = 3200;

            point1.SetActive(true);
            point2.SetActive(true);
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.75f && skeletonUtilityBoneTank2.overrideAlpha >= 0.7f)
        {
            _ammo1._force = 3500;
            _ammo2._force = 3500;
            _ammo3._force = 3500;
            _ammo4._force = 3500;

            point1.SetActive(true);
            point2.SetActive(true);
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.85f && skeletonUtilityBoneTank2.overrideAlpha >= 0.75f)
        {
            _ammo1._force = 3850;
            _ammo2._force = 3850;
            _ammo3._force = 3850;
            _ammo4._force = 3850;

            point1.SetActive(true);
            point2.SetActive(true);
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.9f && skeletonUtilityBoneTank2.overrideAlpha >= 0.8f)
        {
            _ammo1._force = 4000;
            _ammo2._force = 4000;
            _ammo3._force = 4000;
            _ammo4._force = 4000;

            point1.SetActive(true);
            point2.SetActive(true);
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 0.95f && skeletonUtilityBoneTank2.overrideAlpha >= 0.85f)
        {
            _ammo1._force = 4150;
            _ammo2._force = 4150;
            _ammo3._force = 4150;
            _ammo4._force = 4150;

            point1.SetActive(true);
            point2.SetActive(true);
        }
        if (skeletonUtilityBoneTank2.overrideAlpha <= 1f && skeletonUtilityBoneTank2.overrideAlpha >= 0.9f)
        {
            _ammo1._force = 4250;
            _ammo2._force = 4250;
            _ammo3._force = 4250;
            _ammo4._force = 4250;

            point1.SetActive(true);
            point2.SetActive(true);
        }
    }
}
    

