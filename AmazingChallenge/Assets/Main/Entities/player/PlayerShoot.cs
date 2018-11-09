using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform shootPosition;

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();
	}

    void Shoot()
    {
        float speed = 40f;
        GameObject bulletInstance= Instantiate(bulletPrefab,shootPosition.position,GetComponent<Rigidbody>().rotation);
        var force = bulletInstance.AddComponent<ConstantForce>();
        force.relativeForce = Vector3.right * speed;
        bulletInstance.tag = "PlayerBullet";
        MusicManager.instance.PlayShoot();
    }
}
