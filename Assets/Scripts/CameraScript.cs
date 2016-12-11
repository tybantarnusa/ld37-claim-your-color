using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

    private LevelScript level;
    private Vector3 target;
    private Camera cam;

    void Start () {
        level = GameObject.Find("LevelScript").GetComponent<LevelScript>();
        target = new Vector3(13.6f, 20f, transform.position.z);
        cam = GetComponent<Camera>();
    }
	
	void Update () {
		if (level.IsFinished())
        {
            GameObject.Find("GameOver").GetComponent<Text>().enabled = true;
            GetComponent<CameraFollowsPlayer>().enabled = false;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.x, 0.1f), Mathf.Lerp(transform.position.y, target.y, 0.1f), target.z);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 33, 0.1f);
        }
	}
}
