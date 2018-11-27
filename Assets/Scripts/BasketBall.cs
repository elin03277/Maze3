using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour {

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.name == "Wall") {
            //Make wall sound
        } else if (col.gameObject.name == "Quad") {
            //Make floor sound
        }
    }

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.name == "AI") {
            //Make AI sound
            //Update Score
            ScoreManager.score += 1;
            Destroy(gameObject);
        }
    }
}
