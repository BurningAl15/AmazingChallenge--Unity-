using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    TrailRenderer trail;

    private void Awake()
    {
        trail = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        Gradient grad = new Gradient();
        GradientColorKey[] gck = new GradientColorKey[1];
        GradientAlphaKey[] gak = new GradientAlphaKey[2];
        gck[0].color = GetComponent<Renderer>().material.color;
        gck[0].time = 0.0f;

        gak[0].alpha = 1.0f;
        gak[0].time = 0.0f;
        gak[1].alpha = 0.0f;
        gak[1].time = 1.0f;
        grad.SetKeys(gck, gak);

        trail.colorGradient = grad;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.tag.Equals("PlayerBullet") && other.tag.Equals("Enemy"))
        {
            Destroy(other.transform.parent.gameObject);
            Destroy(this.gameObject);
        }

        if (this.tag.Equals("EnemyBullet") && other.tag.Equals("Player"))
        {
            Debug.Log("You lose!");
            Destroy(this.gameObject);
        }
    }
}
