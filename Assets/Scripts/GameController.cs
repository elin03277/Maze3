using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController control;

    public Vector3 playerPos;
    public Vector3 enemyPos;
    public int score;
    public bool loaded;

	// Use this for initialization
	void Awake () {
		if (control == null) {
            DontDestroyOnLoad(gameObject);
            control = this;
        } else {
            Destroy(gameObject);
        }
	}

    private void OnGUI() {
        Scene curScene = SceneManager.GetActiveScene();
    }

    public void Save(int score, Vector3 playerPos, Vector3 enemyPos) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.OpenOrCreate);
        GameData data = new GameData();
        SurrogateSelector ss = new SurrogateSelector();
        Vector3SerializationSurrogate v3ss = new Vector3SerializationSurrogate();
        ss.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), v3ss);
        data.score = score;
        data.playerPos = playerPos;
        data.enemyPos = enemyPos;
        bf.Serialize(fs, data);
        fs.Close();
    }

    public void Load() {
        if (File.Exists(Application.persistentDataPath + "/GameData.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open, FileAccess.Read);
            GameData data = (GameData)bf.Deserialize(fs);
            fs.Close();
            SceneManager.LoadScene("MazeScene");
            loaded = true;
            playerPos = data.playerPos;
            enemyPos = data.enemyPos;
            score = data.score;
        }
    }
}
