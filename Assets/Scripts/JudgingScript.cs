using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JudgingScript : MonoBehaviour {

    private float timer;
    private LevelScript level;
    private GameObject percentCover;
    private GameObject winner;
    private GameObject gameOver;

	void Start () {
        timer = 0;
        level = GetComponent<LevelScript>();
        percentCover = GameObject.Find("PercentCover");
        winner = GameObject.Find("Winner");
        gameOver = GameObject.Find("GameOver");
    }
	
	void Update () {
        if (level.IsFinished())
        {
            timer += Time.deltaTime;

            if (timer > 1.5f)
            {
                percentCover.GetComponent<RectTransform>().localScale = new Vector3(1, Mathf.Lerp(percentCover.GetComponent<RectTransform>().localScale.y, 0, 0.1f), 1);
                Vector3 wp = winner.GetComponent<RectTransform>().anchoredPosition;
                winner.GetComponent<Image>().enabled = true;
                winner.GetComponent<RectTransform>().anchoredPosition = new Vector3(wp.x, StringToPosition(level.GetWinner()), wp.z);
            }

            if (timer > 3f)
            {
                gameOver.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
                gameOver.transform.GetChild(1).gameObject.GetComponent<Text>().color = StringToColor(level.GetWinner());
                gameOver.transform.GetChild(1).gameObject.GetComponent<Text>().text = level.GetWinner() + " WINS!";
                gameOver.transform.GetChild(1).gameObject.GetComponent<Text>().enabled = true;

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Escape))
                    SceneManager.LoadScene("titlescreen");
            }
        }
	}

    private Color StringToColor(string s)
    {
        if (s == "RED") return Color.red;
        if (s == "BLUE") return Color.blue;
        if (s == "GREEN") return Color.green;
        if (s == "YELLOW") return Color.yellow;
        return Color.black;
    }

    private float StringToPosition(string s)
    {
        if (s == "RED") return -22f;
        if (s == "BLUE") return -42f;
        if (s == "GREEN") return -61.5f;
        if (s == "YELLOW") return -80f;
        return float.NaN;
    }
}
