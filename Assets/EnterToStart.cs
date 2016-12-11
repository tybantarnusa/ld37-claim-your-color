using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterToStart : MonoBehaviour {

    private AudioSource sfx;
    private bool flag;

	void Start () {
        sfx = GameObject.Find("StartSFX").GetComponent<AudioSource>();
        flag = false;
	}
	
	void Update () {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space)) && !flag)
        {
            GameObject.Find("Press ENTER").GetComponent<Animator>().enabled = false;
            GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
            sfx.Play();
            flag = true;
        }

        if (flag && !sfx.isPlaying)
        {
            SceneManager.LoadScene("playscene");
        }
	}
}
