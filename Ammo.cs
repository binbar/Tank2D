using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    public float _force = 50f;
    [SerializeField]
    private float _destroyTime = 2f;

    private float speed = 3f;
    private Rigidbody2D _rigidbody2D;

   
    public GameObject exploison;
    public GameObject raketa;
   

    //Очки героя
    public int hitAmmo = 0;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
      

    }
 
    public void Setup(Vector2 direction)
    {
        SetRoatation(direction);
        _rigidbody2D.AddForce(direction.normalized * _force);
    }

    private void SetRoatation(Vector2 direction)
    {
        float z = Vector2.SignedAngle(Vector2.up, direction);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, z));
    }



    private void FixedUpdate()
    {
        SetRoatation(_rigidbody2D.velocity.normalized);
       //     Debug.Log("Позиция ракеты:" + transform.position);
            if (this._rigidbody2D.velocity.y < 0)
            {
                Debug.Log("Падаем вниз");
            }
    }
    public void DestroyAmmo()
    {
        Destroy(gameObject);
    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            _rigidbody2D.velocity = Vector2.zero;
            raketa.SetActive(false);
            exploison.SetActive(true);
            Invoke(nameof(DestroyAmmo), 0.60f);
        }

        if ( collision.gameObject.CompareTag("EnemyTank") || collision.gameObject.CompareTag("AmmoEnemy"))
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            _rigidbody2D.velocity = Vector2.zero;
            raketa.SetActive(false);
            exploison.SetActive(true);
            collision.otherCollider.enabled = false; 
            Invoke(nameof(DestroyAmmo), 0.60f);
        }

    }


}