using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    private float shake = 0;
    private float decraseFactor = 1.0f;

	void Start () {
		
	}
	
	void Update () {
		if (shake > 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + Random.Range(-shake, shake), transform.localPosition.y + Random.Range(-shake, shake), transform.localPosition.z);
            shake -= Time.deltaTime * decraseFactor;
        } else
        {
            shake = 0f;
        }
	}

    public void Shake()
    {
        shake = 0.5f;
    }
}
