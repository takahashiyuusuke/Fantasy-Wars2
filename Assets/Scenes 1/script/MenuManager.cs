using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour{
    public SceneChengeManager sceneChengeManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void BattoleStart(){
        switch (sceneChengeManager.id)
        {
            case 1:
                SceneManager.LoadScene("1Stage");
                break;
            case 2:
                SceneManager.LoadScene("2Stage");
                break;
            case 3:
                SceneManager.LoadScene("3Stage");
                break;
            case 4:
                SceneManager.LoadScene("4Stage");
                break;
            case 5:
                SceneManager.LoadScene("5Stage");
                break;
            case 6:
                SceneManager.LoadScene("6Stage");
                break;
            case 7:
                SceneManager.LoadScene("7Stage");
                break;
            case 8:
                SceneManager.LoadScene("8Stage");
                break;
            case 9:
                SceneManager.LoadScene("9Stage");
                break;
            case 10:
                SceneManager.LoadScene("LastStage");
                break;
        }
		
	}
}
