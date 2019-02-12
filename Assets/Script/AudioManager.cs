using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    // BGM
    public AudioClip BGM;
    public AudioClip BGM1;

    // SE
    public AudioClip turnChengeSE;
    public AudioClip clickSE;

    private AudioSource BGMSource;
    private AudioSource SESource;

    // Use this for initialization
    void Start () {
        // audioSourceの振り分け
        AudioSource[] audioSources = GetComponents<AudioSource>();
        BGMSource = audioSources[0];
        SESource = audioSources[1];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerTurnBGM() {
        BGMSource.clip = BGM1;
        BGMSource.Play();
    }

    public void EnemyTurnBGM() {
        BGMSource.clip = BGM1;
        BGMSource.Play();
    }




    // ここから下がSEの処理

    // ターン切り替えSE
    public void TurnChengeSE() {
        SESource.clip = turnChengeSE;
        SESource.Play();
    }

    // クリックしたときのSE
    public void ClickSE() {
        SESource.clip = turnChengeSE;
        SESource.Play();
    }
}
