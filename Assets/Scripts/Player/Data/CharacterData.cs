using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour {

    public string characterName = "无名";
    public int level = 0;
    public int hP = 5;
    public int aP = 25;
    public float aPr = 10;
    public int[] attack;
    public int[] defend;
    public int speed = 10;
    public int toughness = 0;
    public GameObject handheld;

}
