using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 2.0F;

    [SerializeField] private Transform target;
  
    public bool alive = true; //Состояние Танка,true если жива
   

    private void Awake()
    {
        if (!target) target = FindObjectOfType<TankHero>().transform;
      
    }

  

    private void Update()
    {
        if (alive == true)
        {
            Vector3 position = new Vector3(target.position.x + 25f, 0.0f, -5.0F);
            transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
        }

    }

 

}