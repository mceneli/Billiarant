using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class ManagerScript : MonoBehaviour
{
    public Vector3 aimPosition;
    public Transform wball;
    public float force;
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.01f;
        lineRenderer.positionCount = 2;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
    }


    // Update is called once per frame
    void Update()
    {
        calculateAimPosition();
        shoot();
        drawLine();
    }

    void calculateAimPosition()
    {
        Plane plane = new Plane(Vector3.up, -wball.transform.position.y);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            aimPosition = ray.GetPoint(distance);
        }
    }

    void shoot()
    {
        if (Input.GetMouseButtonUp(0))
        {
            wball.GetComponent<Rigidbody>().AddForce((aimPosition - wball.position) * force, ForceMode.Impulse);
        }
    }

    void drawLine()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, wball.position);
        lineRenderer.SetPosition(1, aimPosition);

        if (Input.GetMouseButton(0))
        {
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
        
    }

}
