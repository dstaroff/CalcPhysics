using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDrawer : MonoBehaviour
{
    public float ThetaScale = 0.01f;

    protected float Radius;
    protected int Size;
    protected LineRenderer LineDrawer;
    protected float Theta;

    void Start()
    {
        Radius = GameObject.Find("Particles").GetComponent<ParticleManager>().CircleRadius;
        LineDrawer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);

        LineDrawer.positionCount = Size;
        for (var i = 0; i < Size; ++i)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = Radius * Mathf.Cos(Theta);
            float y = Radius * Mathf.Sin(Theta);
            LineDrawer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
