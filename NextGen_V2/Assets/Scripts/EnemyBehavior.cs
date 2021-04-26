using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBehavior : MonoBehaviour
{

	public float mSpeed = 20f;
	private GlobalBehavior global;

	//waypoints
	public GameObject[] waypoints = null;
	private int index;

	//hit 
	private int hit = 0;
	GlobalBehavior controller;

	//alpha color
	public Sprite plane;
	SpriteRenderer temp;
	private float alphacolor = 1.0f;

	// Use this for initialization
	void Start()
	{
		NewDirection();
		waypoints = GameObject.FindGameObjectsWithTag("WayPoint");
		global = GameObject.Find("GameManager").GetComponent<GlobalBehavior>();

		index = 0;
	}

    // Update is called once per frame
    void Update()
	{ 		//Setting the bounds of the world
		//transform.position += (mSpeed * Time.smoothDeltaTime) * transform.up;
	GlobalBehavior globalBehavior = GameObject.Find("GameManager").GetComponent<GlobalBehavior>();
	GlobalBehavior.WorldBoundStatus status = globalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);
		if (status != GlobalBehavior.WorldBoundStatus.Inside) {
			Debug.Log("collided position: " + this.transform.position);
			NewDirection();
	}

//Waypoint Controller
//transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, moveSpeed * Time.deltaTime);        
if (transform.position == waypoints[index].transform.position)
{
			index += 1;
}
if (index == waypoints.Length)
{
			index = 0;
}
    }

    // New direction will be something completely random!
    private void NewDirection()
	{

		Vector2 v = Random.insideUnitCircle;
		transform.up = new Vector3(v.x, v.y, 0.0f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (waypoints == null)
			return;
		if (collision.gameObject.CompareTag("WayPoint"))
			NewDirection();



		if (collision.gameObject.CompareTag("Player"))
		{
			Destroy(gameObject);
			controller = GameObject.FindGameObjectWithTag("GlobalBehavior").GetComponent("GlobalBehavior") as GlobalBehavior;
			controller.EnemyDestroyed();
		}
		hit++;
		if (collision.gameObject.CompareTag("Egg"))
		{
			SpriteRenderer temp = gameObject.GetComponent<SpriteRenderer>();
			temp.sprite = plane;
			alphacolor -= 0.25f;
			temp.color = new Color(1f, 1f, 1f, alphacolor);
			if (hit == 4)
			{
				Destroy(gameObject);
				controller = GameObject.FindGameObjectWithTag("GlobalBehavior").GetComponent("GlobalBehavior") as GlobalBehavior;
				controller.EnemyDestroyed();
			}
		}
	}


}