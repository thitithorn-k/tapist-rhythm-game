using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class note : MonoBehaviour {

    int spawnCount = 200;

    public int noteKey;
    public int noteID;
    public int noteTime;
    public int noteEnd;
    private int offsetTime;
    private float ringProgress;
    private bool longNotePressed;
    private int longNoteStatus;

    int toKey = -1;

    AudioSource songAD;
    AudioSource AD;

	void Start () {
        offsetTime = gameData.main.spawnOffset;
        songData.main.spawned.Add(new spawnedNote(noteID, noteTime, noteKey));
        if(noteEnd != 0) {
            transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1);
        }
        songAD = gameData.main.audioS;
        AD = GameObject.Find("hit").GetComponent<AudioSource>();
	}

    void FixedUpdate() {
        #region spawn
        if (spawnCount > 0) {
            spawnCount -= 1;
        } else if (spawnCount != -99) {
            GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            spawnCount = -99;
        }
        #endregion

        int songTime = (int)((float)((float)songAD.timeSamples / (float)songAD.clip.frequency)*1000);

        if (!longNotePressed) {
            if (noteTime - songTime > 9) {
                ringProgress = (float)(((float)(songTime - (noteTime - offsetTime)) / (float)(noteTime - (noteTime - offsetTime))));
                transform.GetChild(0).GetComponent<Image>().fillAmount = ringProgress;
            } else {
                transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
            }
        } else {
            if (noteEnd > songTime) {
                ringProgress = (float)(((float)(noteEnd - songTime) / (float)(noteEnd - noteTime)));
                transform.GetChild(0).GetComponent<Image>().fillAmount = ringProgress;
            } else {
                transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
            }
        }
        

        if(noteID == gameData.main.hitNoteProgress) {
            if (!noteEditor.main.editMode) {
                string scoreText = "";

                if (getInput.main.key[noteKey] == 1 && !longNotePressed) {
                    getInput.main.key[noteKey] = 0;
                    float inputOffset = Mathf.Abs(songTime - noteTime);
                    if (inputOffset <= gameData.main.timming300) {
                        scoreText = "Perfect";
                        songData.main.spawned[noteID].noteStatus = 1;
                    } else if (inputOffset <= gameData.main.timming100) {
                        scoreText = "Good";
                        songData.main.spawned[noteID].noteStatus = 2;
                    } else if (inputOffset <= gameData.main.timming50) {
                        scoreText = "Fine";
                        songData.main.spawned[noteID].noteStatus = 3;
                    } else {
                        scoreText = "Bad";
                        songData.main.spawned[noteID].noteStatus = 4;
                    }
                    AD.Play();
                    if (noteEnd == 0) {
                        SpawnScoreText(scoreText);
                        gameData.main.hitNoteProgress += 1;
                        Destroy(this.gameObject);
                    } else {
                        transform.GetChild(0).GetComponent<Image>().fillClockwise = false;
                        longNotePressed = true;
                    }
                }

                if (longNotePressed) {


                    if (getInput.main.key[noteKey] == 2) {
                        getInput.main.key[noteKey] = 0;
                        float inputOffset = Mathf.Abs(songTime - noteEnd);
                        if (inputOffset <= gameData.main.timming300) {
                            longNoteStatus = 1;
                        } else if (inputOffset <= gameData.main.timming100) {
                            longNoteStatus = 2;
                        } else if (inputOffset <= gameData.main.timming50) {
                            longNoteStatus = 3;
                        } else {
                            longNoteStatus = 4;
                        }
                        if(longNoteStatus > songData.main.spawned[noteID].noteStatus) {
                            songData.main.spawned[noteID].noteStatus = longNoteStatus;
                        }
                        if (songData.main.spawned[noteID].noteStatus == 1) {
                            scoreText = "Perfect";
                        } else if (songData.main.spawned[noteID].noteStatus == 2) {
                            scoreText = "Good";
                        } else if (songData.main.spawned[noteID].noteStatus == 3) {
                            scoreText = "Fine";
                        } else if (songData.main.spawned[noteID].noteStatus == 4) {
                            scoreText = "Bad";
                        }
                        AD.Play();
                        SpawnScoreText(scoreText);
                        gameData.main.hitNoteProgress += 1;
                        Destroy(this.gameObject);
                    }

                    if (gameData.main.time > noteEnd + gameData.main.timming50) {
                        songData.main.spawned[noteID].noteStatus = 4;
                        gameData.main.hitNoteProgress += 1;
                        scoreText = "Miss";
                        SpawnScoreText(scoreText);
                        Destroy(this.gameObject);
                    }
                }

                if (gameData.main.time > noteTime + gameData.main.timming50 && !longNotePressed) {
                    songData.main.spawned[noteID].noteStatus = 4;
                    gameData.main.hitNoteProgress += 1;
                    scoreText = "Miss";
                    SpawnScoreText(scoreText);
                    Destroy(this.gameObject);
                }
            } else {
                if(toKey == -1) {
                    if (getInput.main.key[0] == 1) {
                        toKey = 0;
                    } else if (getInput.main.key[1] == 1) {
                        toKey = 1;
                    } else if (getInput.main.key[2] == 1) {
                        toKey = 2;
                    } else if (getInput.main.key[3] == 1) {
                        toKey = 3;
                    }
                } else {
                    noteInfo oldNote = songData.main.songNoteInfo[noteID];
                    noteEditor.main.newNote.Add(new noteInfo(oldNote.pos, oldNote.start, toKey, oldNote.end));
                    AD.Play();
                    gameData.main.hitNoteProgress += 1;
                    Destroy(this.gameObject);
                }
                
            }

            
        }
    }

    void SpawnScoreText(string text) {
        GameObject score_text = Instantiate(gameData.main.noteScoreText);
        score_text.transform.SetParent(gameData.main.noteScoreCanvas.transform);
        score_text.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;
        score_text.GetComponent<Text>().text = text;
    }
}
