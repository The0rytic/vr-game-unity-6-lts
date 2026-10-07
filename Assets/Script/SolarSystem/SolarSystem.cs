using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    readonly float G = 100f;
    GameObject[] celestials;
    bool orbitStopped = false;

    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");
        InitialVelocity();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        Gravity();
    }

    void Gravity()
    {
        if (orbitStopped) return;

        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (a != b)
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * G * m1 * m2 / (r * r));
                }
            }
        }
    }

    void InitialVelocity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (a != b)
                {
                    if (a.GetComponent<Rigidbody>().isKinematic) continue;

                    PlanetSettings settings = a.GetComponent<PlanetSettings>();
                    bool isEliptical = settings != null && settings.isElipticalOrbit;

                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform);

                    if (isEliptical)
                    {
                        a.GetComponent<Rigidbody>().linearVelocity += a.transform.right * Mathf.Sqrt(G * m2 * ((2 / r) - (1 / (r * 1.5f))));
                    }
                    else
                    {
                        a.GetComponent<Rigidbody>().linearVelocity += a.transform.right * Mathf.Sqrt(G * m2 / r);
                    }
                }
            }
        }
    }

    public void StopOrbits()
    {
        orbitStopped = true;
        foreach (GameObject a in celestials)
        {
            if (a.GetComponent<Rigidbody>().isKinematic) continue;
            a.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            a.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    public void ResumeOrbits()
    {
        orbitStopped = false;
        InitialVelocity();
    }
}