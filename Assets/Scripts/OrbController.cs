using UnityEngine;

public class OrbController : MonoBehaviour {
    public GameObject _MagicOrb;    // this is a reference to your projectile prefab
    public Transform m_SpawnTransform;
    private GameObject Hand;
    private Arduino arduino;
    public AudioClip explosion;
    AudioSource audioSource;
    private void Start() {
        Hand = GameObject.Find("Hand/Posed");
        arduino = GameObject.Find("Arduino").GetComponent<Arduino>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        transform.rotation = Quaternion.Euler(-arduino.YVal, -arduino.XVal, 0);

        if (arduino.ButtonState == 0) {
            Instantiate(_MagicOrb, m_SpawnTransform.position, m_SpawnTransform.rotation);
            audioSource.PlayOneShot(explosion, 0.7F);
        }
    }
}
