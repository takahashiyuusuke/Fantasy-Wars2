using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    // BGM
    public AudioClip bgmClip1, bgmClip2;

    // SE
    public AudioClip seClip1, seClip2;

    private AudioSource[] audioSource;
    //private AudioSource SESource;


    // Use this for initialization
    void Start () {
        audioSource = gameObject.GetComponents<AudioSource>();
    }

	// Update is called once per frame
	void Update () {
		
	}

    // プレイヤーターンBGM
    public void PlayerTurnBGM() {
        audioSource[0].clip = bgmClip1;
        audioSource[0].Play();
    }

    // 敵ターンBGM
    public void EnemyTurnBGM() {
        audioSource[0].clip = bgmClip2;
        audioSource[0].Play();
    }

    // ターン切り替え時のSE
    public void TurnChengeSE() {
        audioSource[1].GetComponent<AudioSource>().PlayOneShot(seClip1);
    }

    // キャラクタータッチ時のSE
    public void ClickSE() {
        audioSource[1].GetComponent<AudioSource>().PlayOneShot(seClip2);
    }
}
