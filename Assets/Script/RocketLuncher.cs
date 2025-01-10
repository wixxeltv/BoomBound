using UnityEngine;
using UnityEngine.InputSystem;

public class RocketLuncher : Shooter
{
    private float leftRight;
    private float topDown;
    private bool wantToFire;
    void Start()
    {
        wantToFire = false;
    }
    
    void FixedUpdate()
    {
        if (canFire && wantToFire)
        {
            GetComponent<Animator>().SetTrigger("Shoot");
            StartCoroutine(fire());
        }
        
        if (leftRight != 0 || topDown != 0)
        {
            float angle = Mathf.Atan2(topDown, leftRight) * Mathf.Rad2Deg;
            float rotationX = 0;
            transform.rotation = Quaternion.Euler(rotationX, 0, angle);
        }
    }
    void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            wantToFire = true;
        }
        else
        {
            wantToFire = false;
        }
    }
    void OnRocketLeftRight(InputValue value)
    {
        leftRight = value.Get<float>();
    }
    void OnRocketTopDown(InputValue value)
    {
        topDown = value.Get<float>();
    }
}
