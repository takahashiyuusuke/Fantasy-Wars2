using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChengeManager : MonoBehaviour {

    public int id;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(id);
	}

    public void Stage01() {
        id = 1;
        //SceneManager.LoadScene("1Stage");
    }
    public void Stage02() {
        id = 2;
        // SceneManager.LoadScene("2Stage");
    }
    public void Stage03() {
        id = 3;
        // SceneManager.LoadScene("3Stage");
    }
    public void Stage04() {
        id = 4;
        // SceneManager.LoadScene("4Stage");
    }
    public void Stage05() {
        id = 5;
        // SceneManager.LoadScene("5Stage");
    }
    public void Stage06() {
        id = 6;
        // SceneManager.LoadScene("6Stage");
    }
    public void Stage07() {
        id = 7;
        // SceneManager.LoadScene("7Stage");
    }
    public void Stage08() {
        id = 8;
        // SceneManager.LoadScene("8Stage");
    }
    public void Stage09() {
        id = 9;
        // SceneManager.LoadScene("9Stage");
    }
    public void Stage10() {
        id = 10;
        // SceneManager.LoadScene("LastStage");
    }
}
