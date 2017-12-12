using UnityEngine;
using Random = UnityEngine.Random;

public class Particle : MonoBehaviour
{
    protected Vector2 Direction;
    protected float CircleRadius;
    protected Rigidbody2D Rigid;

    public void CheckLimits()
    {
        var r = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.y, 2));
        if (!(r > CircleRadius)) return;

        var phi = Mathf.Atan2(transform.position.y, transform.position.x);
        r = CircleRadius;

        var x = r * Mathf.Cos(phi);
        var y = r * Mathf.Sin(phi);
        transform.position = new Vector2(x, y);
    }

    void Start ()
    {
        Rigid = GetComponent<Rigidbody2D>();

        var circleRadius = GameObject.Find("Particles")?.GetComponent<ParticleManager>()?.CircleRadius;
        if (circleRadius != null)
        {
            CircleRadius = (float) circleRadius;
        }
        else
        {
            CircleRadius = 0f;
        }
        
        // Init in polar coordinates
        var r = Random.Range(0f, CircleRadius);
        var phi = Random.Range(0f, 360f);
        var x = r * Mathf.Cos(Mathf.Deg2Rad * phi);
        var y = r * Mathf.Sin(Mathf.Deg2Rad * phi);
        transform.position = new Vector2(x, y);

        // Initial force to make particles move
        Rigid.AddForce(new Vector2(Random.Range(-CircleRadius / 2, CircleRadius / 2), Random.Range(-CircleRadius / 2, CircleRadius / 2)));
    }
	
	void Update () {

	}
}
