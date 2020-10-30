using UnityEngine;
using UnityEngine.UI;

public class Samara_Heart : MonoBehaviour {
    private Text scoreText;
    private GameObject winText;
    private int score;

    private void Awake() {
        scoreText = GameObject.Find("Canvas/Score").GetComponent<Text>();
        winText = GameObject.Find("Canvas/You Win");
        winText.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision) {
        UnityEngine.Debug.Log("Trigger entered: " + collision.name);
        if (collision.name.StartsWith("Magic_Orb")) {
            score += 1;
            scoreText.text = "Score: " + score;
            if (score >= 30) {
                winText.SetActive(true);
                Destroy(GameObject.Find("Samara"));
            }
        }
    }
}
