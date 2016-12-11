using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelScript : MonoBehaviour {

    private GameObject tiles;
    private int total1, total2, total3, total4;
    private Text percent1, percent2, percent3, percent4;
    [SerializeField] private float gameTime;
    private GameObject percentCover;
    private bool[] activeItem;
    private Vector3[] itemPos;
    private bool started;
    private string winner;
    private float countdown;
    private GameObject countdownUI;

	void Start () {
        tiles = GameObject.Find("Tiles");

        percent1 = GameObject.Find("Percent1").GetComponent<Text>();
        percent2 = GameObject.Find("Percent2").GetComponent<Text>();
        percent3 = GameObject.Find("Percent3").GetComponent<Text>();
        percent4 = GameObject.Find("Percent4").GetComponent<Text>();

        percentCover = GameObject.Find("PercentCover");
        percentCover.GetComponent<Image>().enabled = false;
        percentCover.GetComponent<RectTransform>().localScale = new Vector3(1, 0, 1);

        gameTime = 120;
        activeItem = new bool[] { true, true, true, true };
        GameObject items = GameObject.Find("Items");
        itemPos = new Vector3[]
        {
            items.transform.GetChild(0).position,
            items.transform.GetChild(1).position,
            items.transform.GetChild(2).position,
            items.transform.GetChild(3).position
        };

        countdownUI = GameObject.Find("Countdown");
        countdown = 4;
        started = false;
    }
	
	void Update () {
        if (gameTime == 120)
        {
            GameObject.Find("GameTime").GetComponent<Text>().text = "02:00";
            countdown -= Time.deltaTime;

            if (countdown < 1)
            {
                countdownUI.GetComponent<Text>().text = "START!";
                started = true;
            }
            else
            {
                countdownUI.GetComponent<Text>().text = "" + (int)countdown;
            }
        }

        if (gameTime < 119)
            countdownUI.SetActive(false);

        if (started)
        {
            gameTime -= Time.deltaTime;
            GameObject.Find("GameTime").GetComponent<Text>().text = ConvertTime(gameTime);
        }

        if (gameTime < 0)
        {
            GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
            gameTime = -1;
            GameObject.Find("GameTime").GetComponent<Text>().text = ConvertTime(gameTime);
            started = false;
        }

        if (gameTime < 30 && gameTime > 0)
        {
            percentCover.GetComponent<Image>().enabled = true;
            percentCover.GetComponent<RectTransform>().localScale = new Vector3(1, Mathf.Lerp(percentCover.GetComponent<RectTransform>().localScale.y, 1, 0.1f), 1);
        }

        for (int i = 0; i < tiles.transform.childCount; i++)
        {
            int n = tiles.transform.GetChild(i).gameObject.GetComponent<Tile>().GetPlayerNumber();
            switch (n) {
                case 1:
                    total1++;
                    break;
                case 2:
                    total2++;
                    break;
                case 3:
                    total3++;
                    break;
                case 4:
                    total4++;
                    break;
            }
        }

        percent1.text = "" + (int) ((((float) total1) / ((float) tiles.transform.childCount)) * 100) + "%";
        percent2.text = "" + (int) ((((float)total2) / ((float)tiles.transform.childCount)) * 100) + "%";
        percent3.text = "" + (int) ((((float)total3) / ((float)tiles.transform.childCount)) * 100) + "%";
        percent4.text = "" + (int) ((((float)total4) / ((float)tiles.transform.childCount)) * 100) + "%";

        int winnerN = -1;
        if (winnerN < total1)
        {
            winnerN = total1;
            winner = "RED";
        }
        if (winnerN < total2)
        {
            winnerN = total2;
            winner = "BLUE";
        }
        if (winnerN < total3)
        {
            winnerN = total3;
            winner = "GREEN";
        }
        if (winnerN < total4)
        {
            winnerN = total4;
            winner = "YELLOW";
        }

        total1 = total2 = total3 = total4 = 0;
    }

    private string ConvertTime(float t)
    {
        int e = (int)t + 1;
        int minute = e / 60;
        int second = e % 60;
        string sminute = minute < 10 ? "0" + minute : "" + minute;
        string ssecond = second < 10 ? "0" + second : "" + second;
        return sminute + ":" + ssecond;
    }

    public Vector3 GetNearestItem(Vector3 yourPosition)
    {
        Vector3 nearest = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        for (int i = 0; i < itemPos.Length; i++)
        {
            if (!activeItem[i])
                continue;

            float nearestDistance = (nearest - yourPosition).magnitude;
            float currentCheckDistance = (itemPos[i] - yourPosition).magnitude;

            if (currentCheckDistance < nearestDistance)
            {
                nearest = itemPos[i];
            }
        }

        return nearest;

    }

    public bool ThereAreAnyItem()
    {
        if (activeItem == null) return false;
        return activeItem[0] | activeItem[1] | activeItem[2] | activeItem[3];
    }

    public void DeactivateItem(int i)
    {
        activeItem[i] = false;
    }

    public void ActivateItem(int i)
    {
        activeItem[i] = true;
    }

    public bool IsStarted()
    {
        return started;
    }

    public bool IsFinished()
    {
        return gameTime < 0;
    }

    public string GetWinner()
    {
        return winner;
    }
}
