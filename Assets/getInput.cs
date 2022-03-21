using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getInput : MonoBehaviour {

    public static getInput main;
    public int[] key = new int[4];
    public int[] keyCooldown = new int[4];
    public int coolDown = 30;

	private void Start () {
        main = this;
	}

    private void FixedUpdate() {
        for(int i=0; i < 4; i++) {
            key[i] = 0;
            if (keyCooldown[i] > 0)
                keyCooldown[i]--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) & keyCooldown[0] == 0) {
            key[0] = 1;
            keyCooldown[0] = coolDown;
        }      
        if (Input.GetKeyDown(KeyCode.DownArrow) & keyCooldown[1] == 0) {
            key[1] = 1;
            keyCooldown[1] = coolDown;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) & keyCooldown[2] == 0) {
            key[2] = 1;
            keyCooldown[2] = coolDown;
        }    
        if (Input.GetKeyDown(KeyCode.UpArrow) & keyCooldown[3] == 0) {
            key[3] = 1;
            keyCooldown[3] = coolDown;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
            key[0] = 2;
        if (Input.GetKeyUp(KeyCode.DownArrow))
            key[1] = 2;
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            key[2] = 2;
        if (Input.GetKeyUp(KeyCode.UpArrow))
            key[3] = 2;
    }
}
