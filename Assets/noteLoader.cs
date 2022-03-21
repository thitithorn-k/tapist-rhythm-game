using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class noteLoader : MonoBehaviour {

    public static noteLoader main;

    private List<String> info;

    private void Start() {
        main = this;
    }

    public  bool Load(string fileName) {
        info = new List<string>();
        try {
            string line;
            StreamReader theReader = new StreamReader(fileName, Encoding.Default);

            using (theReader) {
                do {
                    line = theReader.ReadLine();
                    if (line != null) {
                        info.Add(line);
                    }
                }

                while (line != null);
                theReader.Close();
                AddInfo();
                return true;
            }
        }

        catch(Exception e) {
            Debug.Log("bug");
            return false;
        }
    }

    private void AddInfo() {
        songData.main.songNoteInfo = new List<noteInfo>();
        songData.main.spawned = new List<spawnedNote>();
        int state = 0;
        while(info.Count > 0) {
            if(state == 0) {
                if (info[0] != "[Metadata]") {
                    info.RemoveAt(0);
                } else {
                    state = 1;
                    info.RemoveAt(0);
                }
            } else
            if(state == 1) {
                if (info[0] != "[note]") {
                    string[] entries = info[0].Split(':');
                    if (entries[0] == "Title")
                        songData.main.title = entries[1];
                    else if (entries[0] == "TitleUnicode")
                        songData.main.titleUni = entries[1];
                    else if (entries[0] == "Artist")
                        songData.main.artist = entries[1];
                    else if (entries[0] == "ArtistUnicode")
                        songData.main.artistUni = entries[1];
                    else if (entries[0] == "Creator")
                        songData.main.creator = entries[1];
                    else if (entries[0] == "Version")
                        songData.main.version = entries[1];
                    else if (entries[0] == "Source")
                        songData.main.source = entries[1];
                    info.RemoveAt(0);
                } else {
                    state = 2;
                    info.RemoveAt(0);
                }
            } else
            if (state == 2) {
                string[] entries = info[0].Split(',');
                Vector2 pos = new Vector2(int.Parse(entries[0]), int.Parse(entries[1]));
                int start = int.Parse(entries[2]);
                int note = int.Parse(entries[3]);
                int end = int.Parse(entries[4]);
                songData.main.songNoteInfo.Add(new noteInfo(pos,start,note,end));
                info.RemoveAt(0);
            }
        }
        songData.main.noteAmount = songData.main.songNoteInfo.Count;
    }
}

public class noteInfo {

    public Vector2 pos { get; set; }
    public int start { get; set; }
    public int note { get; set; }
    public int end { get; set; }

    public noteInfo(Vector2 pos, int start, int note, int end) {
        this.pos = pos;
        this.start = start;
        this.note = note;
        this.end = end;
    }
}
