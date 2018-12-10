using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDatas {
    public Struct.FieldBase GetData1() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ1";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {2,2,3,3,2,2},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {2,2,0,0,2,2},
            {2,2,0,0,2,2},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
        };
        return fieldBase;
    }

    public Struct.FieldBase GetData2() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ2";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {0,0,0,0,0,0},
            {9,9,1,0,0,0},
            {0,0,0,0,0,0},
			{0,0,0,0,1,0},
			{0,0,0,0,0,0},
			{24,24,0,0,0,8},
			{24,24,0,0,0,8},
            {0,0,0,0,8,8},
        };
        return fieldBase;
    }

    public Struct.FieldBase GetData3() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ3";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {10,10,11,12,12,12},
            {10,10,0,0,0,12},
            {0,0,0,1,1,12},
            {0,1,0,1,0,0},
            {6,6,1,0,0,11},
            {11,6,6,6,22,6},
            {0,0,0,0,0,0},
            {0,1,1,1,0,0},
        };
        return fieldBase;
    }

    public Struct.FieldBase GetData4() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ4";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {13,0,0,0,0,0},
            {11,0,1,1,0,11},
            {0,4,1,1,4,0},
            {0,1,1,4,4,0},
            {0,11,0,0,25,25},
            {13,0,0,0,0,11},
            {0,0,1,1,1,11},
            {11,0,0,0,0,11},
        };
        return fieldBase;
    }

    public Struct.FieldBase GetData5() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ5";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {0,0,0,11,0,0},
            {0,0,0,0,0,0},
            {1,0,1,4,0,0},
            {11,0,4,0,4,0},
            {0,0,0,0,0,0},
            {0,25,25,0,11,0},
            {0,4,0,0,0,0},
            {11,0,0,0,0,0},
        };
        return fieldBase;
    }

    public Struct.FieldBase GetData6() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ6";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {0,0,0,0,0,8},
            {0,13,0,0,25,8},
            {25,25,0,0,13,8},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {1,1,0,0,0,13},
            {1,0,0,0,0,8},
            {0,0,0,0,0,8},
        };
        return fieldBase;
    }
    public Struct.FieldBase GetData7() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ7";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {2,2,2,2,2,2},
            {2,2,2,23,2,2},
            {14,0,16,0,0,15},
            {0,0,0,0,0,0},
            {18,18,0,0,0,0},
            {18,18,0,0,0,14},
            {15,14,0,0,0,0},
            {0,0,0,0,0,0},
        };
        return fieldBase;
    }



    public Struct.FieldBase GetData8() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ8";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {0,0,0,0,0,0},
            {18,0,0,0,0,0},
            {0,0,0,7,0,0},
            {0,0,0,0,18,0},
            {0,7,0,0,7,0},
            {0,0,0,0,0,0},
            {7,17,0,0,17,7},
            {7,18,0,0,0,7},
        };
        return fieldBase;
    }


    public Struct.FieldBase GetData9() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ9";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {20,0,0,0,0,20},
            {20,0,5,5,0,20},
            {0,19,5,5,0,0},
            {5,0,0,0,0,0},
            {19,0,0,19,0,5},
            {0,19,5,5,0,19},
            {0,0,5,5,0,0},
            {0,0,0,0,0,0},
        };
        return fieldBase;
    }

    public Struct.FieldBase GetData10() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ10";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {21,21,21,21,21,21},
            {0,21,7,0,7,21},
            {0,0,0,0,0,21},
            {0,0,0,0,7,21},
            {0,0,0,0,0,21},
            {0,0,0,0,0,21},
            {0,0,0,0,0,0},
            {0,0,0,0,21,21},
        };
        return fieldBase;
    }
}

