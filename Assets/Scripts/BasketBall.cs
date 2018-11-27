using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour {

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.name == "Wall") {
            AudioManager.instance.Play("wall");
        } else if (col.gameObject.name == "Quad") {
            AudioManager.instance.Play("floor");
        }
    }

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.name == "AI") {
            AudioManager.instance.Play("ai");
            //Update Score
            ScoreManager.score += 1;
            Destroy(gameObject);
        }
    }
}
