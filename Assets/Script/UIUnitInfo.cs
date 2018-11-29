﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitInfo : MonoBehaviour {
    public GameObject selectUnitInfo;
    public Image faceImage;
    public Text nameText;
    public Text levelText;
    public Text lifeText;
    public GameObject gageImage;
    public RectTransform gageImageBk;

    public Color lifeGreen;
    public Color lifeYellow;
    public Color lifeRed;

    private float lifeRate;
    private RectTransform gageImageRect;
    private Image gageImageColor;

    void Start() {
        // ゲージイメージのコンポーネント取得
        gageImageRect = gageImage.GetComponent<RectTransform>();
        gageImageColor = gageImage.GetComponent<Image>();

        // ゲーム開始時は非表示
        selectUnitInfo.SetActive(false);
    }

    public void ShowUnitInfo(UnitInfo unitInfo) {
        // フォーカスユニットがいるならユニット情報を表示する
        selectUnitInfo.SetActive(true);
        faceImage.sprite = Resources.Load<Sprite>("Sprite/UnitFace/Chara" + unitInfo.id);
        nameText.text = unitInfo.name;
        levelText.text = string.Format("Lv: {0}", unitInfo.level);
        lifeText.text = string.Format("HP: {0}/{1}", unitInfo.hp, unitInfo.vitality);
        lifeRate = (float)unitInfo.hp / (float)unitInfo.vitality;
        gageImageRect.sizeDelta = new Vector2(
            lifeRate * (float)gageImageBk.sizeDelta.x,
                                          gageImageBk.sizeDelta.y);
        if (lifeRate <= 0.2)
            gageImageColor.color = lifeRed;
        else if (lifeRate <= 0.6)
            gageImageColor.color = lifeYellow;
        else if (lifeRate <= 1)
            gageImageColor.color = lifeGreen;
    }

    public void CloseUnitInfo() {
        selectUnitInfo.SetActive(false);
    }
}
