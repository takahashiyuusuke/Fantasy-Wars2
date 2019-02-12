using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    // BGM
    public AudioClip PlayerTurnBGM;
    public AudioClip EnemyTurnBGM;

    // SE
    public AudioClip clickSE;
    public AudioClip turnChengeSE;



    // AudioSource
    private AudioSource BGMSource;
    private AudioSource SESource;


    // Use this for initialization
    void Start () {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        BGMSource = audioSources[0];
        SESource = audioSources[1];

        BGMSource.clip = PlayerTurnBGM;
        BGMSource.Play();
        SESource.clip = clickSE;
        
    }
	
	// Update is called once per frame
	void Update () {

	}

    //自ターンBGMに切り替える
    public void ChangePlayerBGM() {
        BGMSource.clip = PlayerTurnBGM;
        BGMSource.Play();
    }

    // 敵ターンBGMに切り替える
    public void ChangeEnemyBGM() {
        BGMSource.clip = EnemyTurnBGM;
        BGMSource.Play();
    }

    // キャラクリック時のSE
    public void ClickSE() {
        SESource.clip = clickSE;
        SESource.Play();
    }

    // ターン切り替え時のSE
    public void TurnChengeSE() {
        SESource.clip = turnChengeSE;
        SESource.Play();
    }

    // 

}
