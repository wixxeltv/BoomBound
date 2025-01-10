using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Rocket rocketPrefab;
    public Transform shootPoint;
    public float bulletSpeed;
    [SerializeField] public float fireRate;
    public Rigidbody2D objectRigidBody = null;
    public AudioSource shotSound;
    protected bool canFire = true;

    private void Start()
    {
        //canFire = true;
    }

    protected IEnumerator fire()
    {
        Debug.Log("Trying to fire");
        canFire = false;
        Shoot();
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    void Shoot()
    {
        Debug.Log("Audio go");
        shotSound.PlayOneShot(shotSound.clip);
        Rocket bullet = Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);
        Vector2 direction = shootPoint.right;
        Vector2 initialForce = direction * bulletSpeed;
        bullet.rb.linearVelocity = initialForce;
    }

    
}