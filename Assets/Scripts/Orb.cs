using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Orb : MonoBehaviour {
    private float m_Speed = 200f;
    private float m_Lifespan = 10f;
    private Rigidbody m_Rigidbody;

    private void Awake() {
        m_Rigidbody = GetComponent<Rigidbody>();
        GameObject.Find("Arduino").GetComponent<Arduino>().AddOrb(GetComponent<VisualEffect>());
    }

    private void Start() {
        m_Rigidbody.AddForce(m_Rigidbody.transform.forward * m_Speed);
        Destroy(gameObject, m_Lifespan);
    }
}
