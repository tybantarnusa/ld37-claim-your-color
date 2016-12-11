using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupHorizontal : MonoBehaviour {

    private BoxCollider2D col;
    private float target;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        target = 101;
    }

    void Update()
    {
        col.size = new Vector2(Mathf.Lerp(col.size.x, target, 0.1f), 0.2f);
        if (col.size.x > target - 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
