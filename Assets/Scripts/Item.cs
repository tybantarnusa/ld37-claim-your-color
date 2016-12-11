using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    private SpriteRenderer sprite;
    private Animator anim;
    private GameObject shadow;
    private bool buffer;
    private float timer;
    private GameObject timeText;
    public Powerup[] powerups;

    private LevelScript level;

    [SerializeField]
    private int id;

    private AudioSource sfx;

    void Awake()
    {
        sfx = GameObject.Find("GetPower").GetComponent<AudioSource>();
        powerups = new Powerup[]
        {
            Powerup.BOMB,
            Powerup.VERTICAL,
            Powerup.HORIZONTAL,
            Powerup.PLUS,
            Powerup.SALT
        };
    }

    void Start () {
        level = GameObject.Find("LevelScript").GetComponent<LevelScript>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        shadow = transform.GetChild(0).gameObject;
        buffer = false;
        timer = 10;
        timeText = transform.GetChild(1).gameObject;
    }
	
	void Update () {
		if (buffer)
        {
            timer -= Time.deltaTime;
            timeText.GetComponent<TextMesh>().text = "" + ((int)timer + 1);
            if (timer < 0f)
            {
                timeText.SetActive(false);
                sprite.color = new Color(1, 1, 1, 1);
                anim.enabled = true;
                shadow.SetActive(true);
                GetComponent<CircleCollider2D>().enabled = true;
                timer = 10;
                buffer = false;
                level.ActivateItem(id);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int idx = Random.Range(0, powerups.Length);
        if (collision.gameObject.GetComponent<Player>() != null && !buffer)
        {
            sfx.Play();
            collision.gameObject.GetComponent<Player>().SetPowerup(powerups[idx]);
            Inactivate();
        }
        else if (collision.gameObject.GetComponent<AdvancedAI>() != null)
        {
            sfx.Play();
            collision.gameObject.GetComponent<AdvancedAI>().SetPowerup(powerups[idx]);
            Inactivate();
        }

    }

    private void Inactivate()
    {
        timeText.SetActive(true);
        sprite.color = new Color(1, 1, 1, 0.4f);
        anim.enabled = false;
        shadow.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = false;
        buffer = true;
        level.DeactivateItem(id);
    }
}
