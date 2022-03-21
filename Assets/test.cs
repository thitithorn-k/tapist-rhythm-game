using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class test : MonoBehaviour {

    public bool setTime;
    private bool a;
    private int storeTime;

    public int songTime;

    private AudioSource AS;

    Stopwatch watch;

    private void Start() {
        AS = GetComponent<AudioSource>();
        watch = new Stopwatch();
        watch.Start();
    }

    private int coolDown = 0;
    public int set = 2000;

    private int count;
    void FixedUpdate () {
        //UnityEngine.Debug.Log(watch.ElapsedMilliseconds);
        if (gameData.main.playing) {
            if (setTime) {
                if (!a) {
                    storeTime = (int)(AS.time * 1000);
                    a = true;
                } else {
                    if((int)(AS.time * 1000) != storeTime) {
                        gameData.main.time = (int)(AS.time * 1000);
                        setTime = false;
                        a = false;
                    }
                }
                
            } else {
                if (storeTime != (int)(AS.time * 1000)) {
                    storeTime = (int)(AS.time * 1000);
                    //UnityEngine.Debug.Log(gameData.main.time + " : " + (int)(AS.time * 1000) + " /// " + (gameData.main.time - (int)(AS.time * 1000)));
                    //Debug.Log((float)(AS.timeSamples/(float)AS.clip.frequency));
                    
                }

            }



            /* if(songTime != (int)(AS.time*1000)) {
                 songTime = (int)(AS.time * 1000);
                 if(Mathf.Abs(gameData.main.time - songTime) > 50) {
                     gameData.main.time = songTime;
                     Debug.Log("Change");
                 }
             }*/

        }

	}
}
