using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class noteEditor : MonoBehaviour {

    public static noteEditor main;
    public bool editMode;
    public List<noteInfo> newNote;
    public string outFileName;
    public bool export;

	void Start () {
        main = this;
        newNote = new List<noteInfo>();
	}
	
	// Update is called once per frame
	void Update () {
        if (export) {
            export = false;
            Export();
        }
	}

    void Export() {
        string path = "Assets/" + outFileName + ".txt";
        StreamWriter writer = new StreamWriter(path, true);
        for(int i = 0; i < newNote.Count; i++) {
            writer.WriteLine(newNote[i].pos.x + "," + newNote[i].pos.y + "," + newNote[i].start + "," + newNote[i].note + "," + newNote[i].end);
        }
        writer.Close();
    }
}
