using UnityEngine;
using UnityEngine.UI;

public class Samara_Move : MonoBehaviour {
    public Transform Target;
    public Transform Samara;
    private float speed = 3f;
    private ParticleSystem _Particles;
    private float rand;
    private Vector3 newPosition;

    private float teleportTimer = 0;
    private Transform lookAtTarget;
    private float trueSpeed = 1;

    private float timer = 2f;
    private float interval = 0.5f;
    private GameObject End_Panel;
    private GameObject Hand;
    private float distance;
    private GameObject Sam;

    private void Awake() {
        lookAtTarget = GameObject.Find("Main Camera").transform;
        Sam = GameObject.Find("Samara");
        _Particles = GameObject.Find("Fog").GetComponent<ParticleSystem>();
    }

    private void Start() {
        End_Panel = GameObject.Find("Canvas/End_Panel");
        Hand = GameObject.Find("Hand/Posed");
    }

    private void Update() {
        if (Samara != null) {
            if (Samara != null && Samara.position.z - Target.position.z > 0.5f) {
                float step = trueSpeed * Time.deltaTime; // calculate distance to move 
                Samara.position = Vector3.MoveTowards(Samara.position, Target.position, step);

                teleportTimer += Time.deltaTime;
                if (teleportTimer > 0.75f) {
                    Samara.position = new Vector3(Random.Range(-3f, 3f), Samara.position.y, Samara.position.z);
                    teleportTimer -= 0.75f;

                    trueSpeed = speed * Random.Range(0.2f, 1f);
                }

                timer += Time.deltaTime;
                if (timer >= interval) {
                    int randomValue = Random.Range(0, 2);
                    Samara.gameObject.SetActive(randomValue == 1);
                    timer = 0;
                }
            } else {
                Samara.position = Target.position;
                Samara.gameObject.SetActive(true);
            }

            Samara.LookAt(lookAtTarget);
            if (Samara.transform != null) {
                float dist = Vector3.Distance(Samara.position, Hand.transform.position);
                if (dist < 2f) {
                    End_Panel.SetActive(true);
                    Destroy(Sam);
                } else {
                    End_Panel.SetActive(false);
                }
            }
        }
    }
}
