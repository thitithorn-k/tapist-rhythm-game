using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class songData : MonoBehaviour {

    public static songData main;

    public float noteSpeed;
    public string title;
    public string titleUni;
    public string artist;
    public string artistUni;
    public string creator;
    public string version;
    public string source;

    [SerializeField]
    public List<noteInfo> songNoteInfo = new List<noteInfo>();
    public List<spawnedNote> spawned = new List<spawnedNote>();

    public int noteAmount;

    void Start() {
        main = this;
    }

}

public class spawnedNote {
    public int noteID;
    public int noteTime;
    public int noteKey;
    public int noteStatus;

    public spawnedNote(int noteID, int noteTime, int noteKey) {
        this.noteID = noteID;
        this.noteTime = noteTime;
        this.noteKey = noteKey;
        noteStatus = 0;
    }
}
