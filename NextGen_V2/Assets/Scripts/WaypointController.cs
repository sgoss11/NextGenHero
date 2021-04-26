using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{

    //visible 
    SpriteRenderer wayPoint = null;

    //position
    Vector3 init;

    private GlobalBehavior global;

    // Start is called before the first frame update
    void Start()
    {
        init = transform.position;
        wayPoint = GetComponent<SpriteRenderer>();

        global = GameObject.Find("GameManager").GetComponent<GlobalBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        WaypointVisibility();
    }


    public void WaypointVisibility()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            wayPoint.enabled = !wayPoint.enabled;
        }
    }

    //hit by eggs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Egg") && wayPoint.enabled)
        {
            wayPoint.color = new Color(wayPoint.color.r, wayPoint.color.g, wayPoint.color.b, wayPoint.color.a - 0.25f);
            if (wayPoint.color.a == 0)
            {
                wayPoint.color = new Color(1f, 1f, 1f, 1f);
                float newX = Random.Range(0, 15f);
                float newY = Random.Range(0, 15f);
                transform.position = new Vector3(init.x + newX, init.y + newY);

            }
        }
    }
}
