using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    private Color[] colors = new Color[] {
        new Color(1, 1, 1, 0),
        new Color(1, 0, 0, 0.75f),
        new Color(0, 0, 1, 0.75f),
        new Color(0, 1, 0, 0.75f),
        new Color(1, 1, 0, 0.75f)
    };
    private SpriteRenderer sprite;
    private int on;

	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        on = 0;
	}
	
	void Update () {
        sprite.color = colors[on];
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        on = int.Parse(collision.tag);
    }

    public int GetPlayerNumber()
    {
        return on;
    }

    public void SetPlayerNumber(int id)
    {
        on = id;
    }
}
