using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameData : MonoBehaviour {


    public static gameData main;

    public int player_score;
    public int game_score;

    public float time;
    public bool playing;
    public int spawnOffset = 1000;
    public int noteProgress;
    public int hitNoteProgress;

    public int OD = 0;
    public int timming300;
    public int timming100;
    public int timming50;

    public GameObject canvas;
    public GameObject noteScoreCanvas;
    public GameObject noteScoreText;
    public AudioSource audioS;

    void Start () {
        main = this;
        timming300 = 78 - 6 * OD;
        timming100 = 138 - 8 * OD;
        timming50 = 198 - 10 * OD;
        audioS = GetComponent<AudioSource>();
    }

}
