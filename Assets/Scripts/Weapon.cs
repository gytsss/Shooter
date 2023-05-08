using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    private PlayerMovement input;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private float bulletSpeed = 600.0f;

    // Start is called before the first frame update
    void Start()
    {
        input = transform.root.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(input.fire)
        {
            Fire();
            input.fire = false;
        }

        void Fire()
        {
            Debug.Log("Fire");
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            Destroy(bullet, 1);
        }
    }
}
