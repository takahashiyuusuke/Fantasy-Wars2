using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeController : MonoBehaviour {

    int hp = 0;

    LineRenderer lineRenderer;
    UnitInfo unitInfo;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        unitInfo = transform.parent.gameObject.GetComponent<UnitInfo>();

        hp = unitInfo.hp;
        lineRenderer.SetPosition(1, new Vector3(Mathf.Clamp01((float)hp / (float)unitInfo.vitality) - 0.5f, -0.3f, 0));
    }
	
	// Update is called once per frame
	void Update () {
        // HPゲージの加算/減算と更新
        if (unitInfo.hp < hp)
            lineRenderer.SetPosition(1, new Vector3(Mathf.Clamp01((float)hp-- / (float)unitInfo.vitality) - 0.5f, -0.3f, 0));
        else if (unitInfo.hp > hp)
            lineRenderer.SetPosition(1, new Vector3(Mathf.Clamp01((float)hp++ / (float)unitInfo.vitality) - 0.5f, -0.3f, 0));
    }
}
