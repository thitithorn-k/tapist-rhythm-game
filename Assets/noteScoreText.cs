using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class noteScoreText : MonoBehaviour {

    private int showTime = 3000;

    private void Start() {

    }

    void FixedUpdate () {
        if(showTime > 0) {
            showTime--;
        } else {
            Destroy(this.gameObject);
        }

	}
}
