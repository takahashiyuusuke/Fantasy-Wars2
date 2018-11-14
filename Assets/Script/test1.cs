using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int x = 1;
        int y = 2;
        int z = 3;
        test01(x, y, z);
        test02(x, y, z);
        test03();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void test01(int a, int b, int c) {
        int answer1 = a + b + c;
        //Debug.Log("test1:" + answer1);
    }
    public void test02(int a, int b, int c) {
        int answer2 = a * b + c;
        //Debug.Log("test2:" + answer2);
    }
    public void test03() {
        int[] array = { 0, 1, 2, 3, 4, 5 };
        foreach (int i in array){
            Debug.Log("array:" + i);
        }
    }
}