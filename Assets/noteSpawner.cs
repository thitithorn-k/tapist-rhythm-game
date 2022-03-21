using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteSpawner : MonoBehaviour {

    public GameObject[] note;
	
	void FixedUpdate () {
        if (gameData.main.playing) {
            int noteProgressAdd = 0;
            for(int i = gameData.main.noteProgress; i < gameData.main.noteProgress + 4; i++) {
                if(i < songData.main.noteAmount) {
                    if (gameData.main.time >= songData.main.songNoteInfo[i].start - gameData.main.spawnOffset) {
                        GameObject spawnNote = Instantiate(note[songData.main.songNoteInfo[i].note],gameData.main.canvas.transform);
                        spawnNote.GetComponent<RectTransform>().localPosition = new Vector3(songData.main.songNoteInfo[i].pos.x - 256, songData.main.songNoteInfo[i].pos.y - 192,0);
                        note spawnNoteComponent = spawnNote.GetComponent<note>();
                        spawnNoteComponent.noteTime = songData.main.songNoteInfo[i].start + 100;
                        if(songData.main.songNoteInfo[i].end != 0) {
                            spawnNoteComponent.noteEnd = songData.main.songNoteInfo[i].end + 100;
                        }
                        spawnNoteComponent.noteID = i;
                        spawnNoteComponent.noteKey = songData.main.songNoteInfo[i].note;
                        noteProgressAdd += 1;
                    }
                }     
            }
            gameData.main.noteProgress += noteProgressAdd;
        }
	}
}
