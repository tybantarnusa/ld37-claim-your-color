using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedAI : MonoBehaviour {

    [SerializeField] private int speed;

    private Rigidbody2D rb2d;

    private Vector2 direction;
    private float timer;
    private float timeLimit;
    [SerializeField]
    private GameObject vertical, horizontal;

    private LevelScript level;

    private Powerup powerup;
    private Powerup powerupOn;
    private float usePowerupChance;

    private CameraShake cam;
    private Transform tiles;
    private AudioSource usePowerSFX;


    void Start () {
        cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        level = GameObject.Find("LevelScript").GetComponent<LevelScript>();
        tiles = GameObject.Find("Tiles").transform;
        usePowerSFX = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        timer = 0;
        timeLimit = 0;
        rb2d = GetComponent<Rigidbody2D>();
        powerup = Powerup.NONE;
        usePowerupChance = 0.2f;
        Rethink();
	}

    void Update()
    {
        if (!level.IsStarted())
            return;

        timer += Time.deltaTime;
        if (timer > timeLimit)
            Rethink();

        PowerupUpdate();
        ReturnNormal();
    }

    void FixedUpdate () {
        if (!level.IsStarted())
        {
            rb2d.velocity = Vector2.zero;
            return;
        }

        rb2d.velocity = direction * speed;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.5f);
        if (hit.collider != null && hit.collider.tag == "Wall")
        {
            Rethink();
        }
	}

    private void Rethink()
    {
       if (powerup != Powerup.NONE)
        {
            timer = 0;
            timeLimit = Random.Range(0.2f, 2f);
            direction = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
            if (direction.x == 0 && direction.y == 0)
                Rethink();

            if (Random.Range(0f, 1f) < usePowerupChance)
            {
                UsePower();
                usePowerupChance = Random.Range(0.03f, 0.1f);
            }
        } else if (level.ThereAreAnyItem())
        {
            Vector3 nearestItem = level.GetNearestItem(transform.position);
            Vector3 heading = nearestItem - transform.position;
            direction = heading / heading.magnitude;
        } else
        {
            timer = 0;
            timeLimit = Random.Range(0.2f, 2f);
            direction = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
            if (direction.x == 0 && direction.y == 0)
                Rethink();
        }
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
                Instantiate(horizontal, transform.position, Quaternion.identity).tag = tag;
                break;
            case Powerup.VERTICAL:
                Instantiate(vertical, transform.position, Quaternion.identity).tag = tag;
                break;
            case Powerup.PLUS:
                Instantiate(horizontal, transform.position, Quaternion.identity).tag = tag;
                Instantiate(vertical, transform.position, Quaternion.identity).tag = tag;
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

    public void SetPowerup(Powerup p)
    {
        powerup = p;
    }
}
