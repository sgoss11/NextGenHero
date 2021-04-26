using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSupport : MonoBehaviour
{
    public Camera cam;
    private Bounds worldBound;

    // Use this for initialization
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        worldBound = new Bounds();
        float maxX = cam.orthographicSize * cam.aspect;
        float maxY = cam.orthographicSize;
        float sizeX = 2 * maxX;
        float sizeY = 2 * maxY;
        Vector3 c = cam.transform.position;
        c.z = 0.0f;
        worldBound.center = c;
        worldBound.size = new Vector3(sizeX, sizeY, 1f);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public Bounds GetWorldBounds()
    {
        return worldBound;
    }
    public bool inInside(Bounds b1)
    {
        return isInsideBounds(b1, worldBound);
    }
    public bool isInsideBounds(Bounds b1, Bounds b2)
    {
        return ((b1.min.x < b2.min.x) && (b1.min.y < b2.min.y) &&
            (b1.max.x < b2.max.x) && (b1.max.y < b2.max.y));
    }
}
