using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupVertical : MonoBehaviour {

    private BoxCollider2D col;
    private float target;

    void Start () {
        col = GetComponent<BoxCollider2D>();
        target = 101;
    }
	
	void Update () {
        col.size = new Vector2(0.2f, Mathf.Lerp(col.size.y, target, 0.1f));	
        if (col.size.y > target - 0.1f)
        {
            Destroy(gameObject);
        }
	}
}
