using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float speed;
    private Rigidbody2D rb2d;
    private Powerup powerup;
    private Powerup powerupOn;
    private CameraShake cam;
    private LevelScript level;

    private Transform tiles;

    [SerializeField] private GameObject vertical, horizontal;

    private AudioSource usePowerSFX;

    void Start ()
    {
        usePowerSFX = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        powerup = Powerup.NONE;
        level = GameObject.Find("LevelScript").GetComponent<LevelScript>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        tiles = GameObject.Find("Tiles").transform;
    }

    void Update()
    {
        if (!level.IsStarted())
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UsePower();
        }

        PowerupUpdate();
        ReturnNormal();

    }

    void FixedUpdate () {
        if (!level.IsStarted())
        {
            rb2d.velocity = Vector2.zero;
            return;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        rb2d.velocity = new Vector2(moveX * speed, moveY * speed);

       
	}

    private void PowerupUpdate()
    {
        switch (powerupOn)
        {
            case Powerup.BOMB:
                GetComponent<CircleCollider2D>().radius = Mathf.Lerp(GetComponent<CircleCollider2D>().radius, 5f, 0.5f);
                break;
        }

    }

    private void ReturnNormal()
    {
        if (GetComponent<CircleCollider2D>().radius > 2.9f)
        {
            GetComponent<CircleCollider2D>().radius = 0.34f;
            powerupOn = Powerup.NONE;
        }
    }

    private void UsePower()
    {
        if (powerup != Powerup.NONE)
        {
            cam.Shake();
            usePowerSFX.Play();
        }

        switch (powerup)
        {
            case Powerup.BOMB:
                powerupOn = Powerup.BOMB;
                break;
            case Powerup.HORIZONTAL:
                Instantiate(horizontal, transform.position, Quaternion.identity);
                break;
            case Powerup.VERTICAL:
                Instantiate(vertical, transform.position, Quaternion.identity);
                break;
            case Powerup.PLUS:
                Instantiate(horizontal, transform.position, Quaternion.identity);
                Instantiate(vertical, transform.position, Quaternion.identity);
                break;
            case Powerup.SALT:
                for (int i = 0; i < tiles.childCount; i++)
                {
                    Tile tile = tiles.GetChild(i).gameObject.GetComponent<Tile>();

                    if (Random.Range(0f, 100f) < 2f)
                        tile.SetPlayerNumber(int.Parse(tag));
                }
                break;
        }
        powerup = Powerup.NONE;
    }

    public void SetPowerup(Powerup powerup)
    {
        this.powerup = powerup;
    }

    public Powerup GetPowerup()
    {
        return powerup;
    }
}
