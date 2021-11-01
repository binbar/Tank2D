using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class EnemyAmmo : MonoBehaviour
{
    [SerializeField]
    public float _force = 600;
    [SerializeField]
    private float _destroyTime = 2f;

    private Rigidbody2D _rigidbody2D;
    public GameObject exploison;
    public GameObject raketa;
   
    public void Setup( Vector2 direction)
    { 
        SetRoatation(direction);
        _rigidbody2D.AddForce(direction.normalized * _force);
    }

    private void SetRoatation(Vector2 direction)
    {
        float z = Vector2.SignedAngle(Vector2.up, direction);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, z));
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    


    private void FixedUpdate()
    {
        SetRoatation(_rigidbody2D.velocity.normalized);
    }
    public void DestroyAmmo()
    {
       
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "HeroTank")
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
