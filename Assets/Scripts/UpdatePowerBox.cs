using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePowerBox : MonoBehaviour {

    [SerializeField] private Sprite emptySprite;

    [SerializeField]
    private Sprite bombSprite;

    [SerializeField]
    private Sprite saltSprite;

    [SerializeField]
    private Sprite verticalSprite;

    [SerializeField]
    private Sprite horizontalSprite;

    [SerializeField]
    private Sprite plusSprite;

    private GameObject player;
    private Image powerIcon;
    private Text powerName;

    void Start () {
        player = GameObject.Find("Player");
        powerIcon = GameObject.Find("PowerIcon").GetComponent<Image>();
        powerName = GameObject.Find("PowerBoxName").GetComponent<Text>();
    }
	
	void Update () {
        Powerup p = player.GetComponent<Player>().GetPowerup();

        switch (p)
        {
            case Powerup.BOMB:
                powerIcon.sprite = bombSprite;
                powerName.text = "BOMB";
                break;
            case Powerup.VERTICAL:
                powerIcon.sprite = verticalSprite;
                powerName.text = "VERTICAL";
                break;
            case Powerup.HORIZONTAL:
                powerIcon.sprite = horizontalSprite;
                powerName.text = "HORIZONTAL";
                break;
            case Powerup.PLUS:
                powerIcon.sprite = plusSprite;
                powerName.text = "PLUS";
                break;
            case Powerup.SALT:
                powerIcon.sprite = saltSprite;
                powerName.text = "SCATTER";
                break;
            default:
                powerIcon.sprite = emptySprite;
                powerName.text = "NONE";
                break;
        }
    }
}
