using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDetection : MonoBehaviour {

    public GameObject player;
    int enemies;
    int bosses;

    // Use this for initialization
    void Start () {
        enemies = 0;
        bosses = 0;
	}
	
	// Update is called once per frame
	void Update () {
        EnemyDetect(player.transform.position, 5);
	}

    void EnemyDetect(Vector3 center, float radius) {
        bosses = 0;
        enemies = 0;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach(Collider obj in hitColliders) {
            if (obj.tag == "Enemy") {
                enemies++;
            }
        }

        if (enemies > 0) {
            AudioManager.instance.Volume("dayMusic", 1.0f);
            AudioManager.instance.Volume("nightMusic", 1.0f);
        } else {
            if (AudioManager.instance.fogOn) {
                AudioManager.instance.Volume("dayMusic", 0.25f);
                AudioManager.instance.Volume("nightMusic", 0.25f);
            } else {
                AudioManager.instance.Volume("dayMusic", 0.5f);
                AudioManager.instance.Volume("nightMusic", 0.5f);

            }
        }
    }
}
