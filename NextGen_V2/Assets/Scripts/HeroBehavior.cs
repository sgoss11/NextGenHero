using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroBehavior : MonoBehaviour {

    public EggStatSystem mEggStat = null;
    public float mHeroSpeed = 20f;
    private bool mFMousePos = false;
    public float kHeroRotateSpeed = 90f/2f; // 90-degrees in 2 seconds
	// Use this for initialization

	void Start () {
        Debug.Assert(mEggStat != null);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateMotion();
        BoundPosition();
        ProcessEggSpwan();
    }

    private void UpdateMotion()
    {
        Vector3 pos = transform.position;
        if (Input.GetKeyDown(KeyCode.M))
        {
            mFMousePos = !mFMousePos;
        }
        if (!mFMousePos)
        {
            mHeroSpeed += Input.GetAxis("Vertical");
            pos += transform.up * (mHeroSpeed * Time.smoothDeltaTime);
            transform.Rotate(Vector3.forward, -1f * Input.GetAxis("Horizontal") *
                                    (kHeroRotateSpeed * Time.smoothDeltaTime));
        }
        else
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
        }
        transform.position = pos;
    }

    private void BoundPosition()
    {
        GlobalBehavior.WorldBoundStatus status = GlobalBehavior.sTheGlobalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);
        switch (status)
        {
            case GlobalBehavior.WorldBoundStatus.CollideBottom:
            case GlobalBehavior.WorldBoundStatus.CollideTop:
                transform.up = new Vector3(transform.up.x, -transform.up.y, 0.0f);
                break;
            case GlobalBehavior.WorldBoundStatus.CollideLeft:
            case GlobalBehavior.WorldBoundStatus.CollideRight:
                transform.up = new Vector3(-transform.up.x, transform.up.y, 0.0f);
                break;
        }
    }

    private void ProcessEggSpwan()
    {
        if (mEggStat.CanSpawn()) {
            if (Input.GetKey("space"))
                mEggStat.SpawnAnEgg(transform.position, transform.up);
        }
    }

}
