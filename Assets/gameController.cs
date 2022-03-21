using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {
    
    public static gameController main;
    public string songName;
    public bool load;
    public bool play;
    public bool reset;

    private AudioSource songAD;

	void Start () {
        main = this;
        songAD = gameData.main.audioS;
	}

    private void Update() {
        if (load) {
            bool test = noteLoader.main.Load("Assets/" + songName + ".tap");
            load = false;
            gameData.main.time = 0;
        }
    }

    void FixedUpdate() {

        if (play) {
            if (!gameData.main.playing) {
                gameData.main.playing = true;
                GetComponent<AudioSource>().Play();
                GetComponent<AudioSource>().Pause();
                gameData.main.time = (int)(GetComponent<AudioSource>().time * 1000);
                GetComponent<AudioSource>().Play();
            }
            gameData.main.time = (int)((float)((float)songAD.timeSamples / (float)songAD.clip.frequency) * 1000);
        } else {
            if (gameData.main.playing) {
                GetComponent<AudioSource>().Pause();
                gameData.main.playing = false;
            }
            
        }

        if (reset) {
            reset = false;
            GetComponent<AudioSource>().Stop();
            gameData.main.time = 0;
        }
    }

}
