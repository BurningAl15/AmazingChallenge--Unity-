using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    #region Components
    Rigidbody rgb;
    #endregion
    
    #region Variables
    public int type;
    float startDelay, delay;
    float rotation;
    #endregion

    #region Instantiable Gameobjects
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    GameObject particle;
    #endregion

    private void Awake()
    {
        rgb = GetComponentInChildren<Rigidbody>();
    }

    void Start () {
        switch (type)
        {
            case 0:
                startDelay = 5f;

                break;
            case 1:
            case 2:
                startDelay = 3f;

                break;
        }

        delay = startDelay;
    }
	
	void Update () {
        delay -=Time.deltaTime;
        if(delay<=0)
        {
            Rotating();
            Shooting();
            delay = startDelay;
        }
	}

    void Rotating()
    {
        switch (type)
        {
            /*
             Enemigo A (Amarillo): No se mueve. Dispara cada 5 segundos.
             Enemigo B (Anaranjado): No se mueve. Rota aleatoriamente de forma instantánea cada 3 segundos y dispara.
             Enemigo C (Rojizo): Rota mirando al jugador cada 3 segundos y dispara. 
             */
            case 0:
                rgb.rotation = this.transform.rotation;

                break;
            case 1:
                rgb.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                break;
            case 2:
                rotation = Mathf.Atan2((FindObjectOfType<PlayerMove>().transform.position.z - transform.position.z), (FindObjectOfType<PlayerMove>().transform.position.x - transform.position.x)) * Mathf.Rad2Deg;
                rgb.rotation = Quaternion.Euler(0f, -rotation, 0f); ;

                break;
        }
    }

    void Shooting()
    {
        float speed = 20f;
        GameObject bulletInstance = Instantiate(bulletPrefab, this.transform.position, GetComponent<Rigidbody>().rotation);
        var force = bulletInstance.AddComponent<ConstantForce>();
        force.relativeForce = Vector3.right * speed;
        bulletInstance.tag = "EnemyBullet";
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("PlayerBullet"))
        {
            MusicManager.instance.PlayHit();
            GameObject spawnedParticle = Instantiate(particle, this.transform.position, Quaternion.Euler(90f, 0f, 0f));
            spawnedParticle.GetComponent<ParticleSystemRenderer>().material = this.GetComponentInChildren<Renderer>().material;
        }
    }
}
