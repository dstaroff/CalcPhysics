using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [Range(0, 30)]
    public int N;
    [Range(1f, 7f)]
    public float CircleRadius;
    [Range(-16f, 16f)]
    public float Charge;
    [Range(0f, 128f)]
    public float CoulombCoeff;

    public List<GameObject> Particles;
    public GameObject ParticlePrefab;

    Vector2 CoulombForce(GameObject particle1, GameObject particle2)
    {
        var _particle1 = particle1.GetComponent<Particle>();
        var _particle2 = particle2.GetComponent<Particle>();
        Vector2 vec = _particle1.transform.position - _particle2.transform.position;

        /*
        var distance = Mathf.Pow(_particle2.transform.position.x - _particle1.transform.position.x, 2) +
                       Mathf.Pow(_particle2.transform.position.y - _particle1.transform.position.y, 2);

        return CoulombCoeff * Mathf.Pow(Mathf.Abs(Charge) / distance, 2);
        */

        float mag = CoulombCoeff * Mathf.Pow(Charge, 2) / vec.sqrMagnitude;

        return vec.normalized * mag;
    }

    void Start () {
        Particles = new List<GameObject>(N);

        for (int i = 0; i < N; ++i)
        {
            Particles.Add(Instantiate(ParticlePrefab));
            Particles[i].transform.SetParent(transform);
            Particles[i].name = $"Particle[{i}]";
        }
    }

    void FixedUpdate () {
        for (var i = 0; i < N; ++i)
        {
            var rigid = Particles[i].GetComponent<Rigidbody2D>();
            rigid.velocity = Vector2.zero;
            rigid.angularVelocity = 0f;

            var force = new Vector2(0f, 0f);
            for (var j = 0; j < N; ++j)
            {
                if (i != j)
                {
                    force += CoulombForce(Particles[i], Particles[j]);
                }
            }
            
            Debug.Log($"{i}: [X: {force.x}, Y: {force.y}]");
            rigid.AddForce(force);

            Particles[i].GetComponent<Particle>().CheckLimits();
        }
    }
}
