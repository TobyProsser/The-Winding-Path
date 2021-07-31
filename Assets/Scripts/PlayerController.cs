using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody ThisRB;

    public float PlayerSpeed;
    public float TimeTilIncrease;
    public float SideSpeed;

    public GameObject GameController;
    private GameControllerScript GCS;

    public bool CanMove = true;

    private void Awake()
    {
        ThisRB = this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GCS = GameController.GetComponent<GameControllerScript>();
        StartCoroutine(SpeedIncrease());
    }

    private void Update()
    {
        if (Input.touchCount > 0 && CanMove)
        {
            var touch = Input.touches[0];
            if (touch.position.x < Screen.width / 2)
            {
                ThisRB.velocity += new Vector3(0, 0, SideSpeed/2);
            }
            else if (touch.position.x > Screen.width / 2)
            {
                ThisRB.velocity += new Vector3(0, 0, -SideSpeed/2);
            }
        }
    }

    void LateUpdate()
    {
        if (CanMove)
        {
            ThisRB.velocity = new Vector3(PlayerSpeed, ThisRB.velocity.y, ThisRB.velocity.z);

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                ThisRB.velocity += new Vector3(0, 0, SideSpeed);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                ThisRB.velocity += new Vector3(0, 0, -SideSpeed);
            }
        }
    }

    private IEnumerator SpeedIncrease()
    {
        while (true)
        {
            if (PlayerSpeed <= 3.2f)
            {
                PlayerSpeed += .05f;
                SideSpeed += .008f;
            }

            if (GCS.Score >= 200 && GCS.Score <= 500)
            {
                PlayerSpeed += .005f;
                SideSpeed += .0005f;
            }
            else if (GCS.Score >= 700)
            {
                PlayerSpeed += .004f;
                SideSpeed += .0004f;
            }

            yield return new WaitForSeconds(TimeTilIncrease);
        }
    }
}
