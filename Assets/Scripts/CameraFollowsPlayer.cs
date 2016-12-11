using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayer : MonoBehaviour {
    [SerializeField] private float offset;
    private GameObject target;

	void Start () {
        target = GameObject.Find("Player");
	}
	
	void FixedUpdate () {
        Vector2 targetPos = target.transform.position;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPos.x + offset, 0.1f), Mathf.Lerp(transform.position.y, targetPos.y, 0.1f), transform.position.z);
	}
}
