using UnityEngine;
using UnityEngine.InputSystem;

public class RocketLuncher : Shooter
{
    private float leftRight;
    private float topDown;
    private bool wantToFire;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wantToFire = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canFire && wantToFire)
        {
            Debug.Log("Fire");
            GetComponent<Animator>().SetTrigger("Shoot");
            StartCoroutine(fire());
        }
        
        if (leftRight != 0 || topDown != 0)
        {
                

            // Calcul de l'angle
            float angle = Mathf.Atan2(topDown, leftRight) * Mathf.Rad2Deg; // angle en degrés

// Récupérer la rotation actuelle sur l'axe X
            float rotationX = 0;

// Vérifier si l'angle est entre 90 et 270 degrés
          
            
            transform.rotation = Quaternion.Euler(rotationX, 0, angle);

        }
    }
    void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Want to fire");
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
