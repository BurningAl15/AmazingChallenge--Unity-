using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour {

    [SerializeField]
    float delay;

	void Update () {
        Destroy(this.gameObject, delay);
	}
}
