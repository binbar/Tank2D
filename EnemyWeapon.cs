using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class EnemyWeapon : MonoBehaviour
{
    private float timeShootEnemy;
    private float damageEnemy;


    [Tooltip("Ссылка на сам скелет и обьект Спайна")]
    public SkeletonAnimation skeletonAnimationTank2;
    [Tooltip("Анимации Танка 2")]
    public AnimationReferenceAsset idleTank2Enemy, shootTank2Enemy, drivingTank2Enemy;


    [Tooltip("Кость нашего танка")]
    public SkeletonUtilityBone skeletonUtilityBone;
    public string currentStateTank2Enemy;

    private Spine.Bone bone5; //обращаемся к костью 5 
    public EnemyAmmo enemyAmmo;
    public EnemyTank enemyTank;
    public Transform shotDir;
    private void Start()
    {
        currentStateTank2Enemy = "driving";
        SetStateTank2Enemy(currentStateTank2Enemy);
        skeletonUtilityBone.overrideAlpha = 0.15f;
        bone5 = skeletonAnimationTank2.Skeleton.FindBone("bone5");
        StartCoroutine(AmmoEnemy());


    }
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        skeletonAnimationTank2.state.SetAnimation(0, animation, loop).TimeScale = timeScale;


    }
    public void SetStateTank2Enemy(string state)
    {
        if (state.Equals("shoot"))
        {
            SetAnimation(shootTank2Enemy, true, 1f);
        }
        if (state.Equals("driving"))
        {
            SetAnimation(drivingTank2Enemy, true, 1f);
        }
        if (state.Equals("Idle"))
        {
            SetAnimation(idleTank2Enemy, true, 1f);
        }
    }
    private void Ammo()
    {
        if (enemyTank.hpEnemy >= 0)
        {
            EnemyAmmo ammo = Instantiate(enemyAmmo, shotDir.position, Quaternion.identity);
            ammo.Setup(shotDir.up);

            SetAnimation(drivingTank2Enemy, true, 1f);
        }

    }

    private IEnumerator AmmoEnemy()
    {
        yield return new WaitForSeconds(4f);
        Ammo();
        StartCoroutine(AmmoEnemy());

    }

}
