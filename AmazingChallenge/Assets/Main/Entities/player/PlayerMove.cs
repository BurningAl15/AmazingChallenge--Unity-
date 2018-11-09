///Amazing Games Studio SAC - 2018.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
1. Identificar deficiencias en el codigo.
2. Corregir las deficiencias segun corresponda.
3. Mantener el codigo ordenado y facil de entender.
*/
public class PlayerMove : MonoBehaviour
{
    #region Components
    Rigidbody rgb;
    #endregion

    #region Variables
    float moveSpeed = 10f;

    Vector2 mousePosition;
    Vector3 ScreenPosition;
    float rotation;

    public bool isAlive;
    #endregion

    #region Instantiable Gameobjects
    [SerializeField]
    GameObject particle;

    [SerializeField]
    GameObject shakeableCamera;
    #endregion

    private void Awake()
    {
        rgb = GetComponent<Rigidbody>();
        isAlive = true;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Move(new Vector3(horizontal, 0f, vertical));


        if (Input.GetMouseButtonDown(0))
            LookingAt();

        //Check direction
        Debug.DrawRay(transform.position, new Vector3(ScreenPosition.x, 0.5f, ScreenPosition.z),Color.red);
    }

    void Move(Vector3 _direction)
    {
        rgb.velocity = _direction * moveSpeed;
        transform.position = new Vector3(Mathf.Clamp(rgb.position.x,-19.5f,19.5f),rgb.velocity.y , Mathf.Clamp(rgb.position.z,-9.5f,9.5f));
    }

    void LookingAt()
    {
        //Process the mouse position
        mousePosition = Input.mousePosition;
        ScreenPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        
        //Get the rotation until mouse
        rotation = Mathf.Atan2((ScreenPosition.z - transform.position.z), (ScreenPosition.x - transform.position.x)) * Mathf.Rad2Deg;
    
        //Set the rotation to the rigidbody of the playerMove
        rgb.rotation = Quaternion.Euler(0f, -rotation, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("EnemyBullet"))
        {
            //Plays hit sound
            MusicManager.instance.PlayHit();

            isAlive = false;
            //Make a fast animation of a Screenshake 
            shakeableCamera.GetComponent<Animator>().SetTrigger("Shoot");
            
            //Spawns particles when die
            GameObject spawnedParticle = Instantiate(particle, this.transform.position, Quaternion.Euler(90f,0f,0f));
            spawnedParticle.GetComponent<ParticleSystemRenderer>().material = this.GetComponentInChildren<Renderer>().material;
        }
    }
}