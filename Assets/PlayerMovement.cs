using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera camera;
    Vector2 Cursor_position = new Vector2();
    Vector2 Wind_Direction = new Vector2();
    float APPLIED_FORCE = 20;
    //Vector2 Up = new Vector2.up;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor_position = (Vector2) Input.mousePosition;
        Cursor_position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);


        Wind_Direction = (Vector2) camera.ScreenToWorldPoint(Cursor_position) - (Vector2)transform.position;
        
        //Debug.Log(Wind_Direction);
        //Debug.Log(Input.mousePosition);
        //Debug.Log(camera.ScreenToWorldPoint(Cursor_position));
        //transform.rotation = Quaternion.AngleAxis(Vector2.Angle(rb.position, (Vector2) camera.ScreenToWorldPoint(Cursor_position)), Vector3.forward);
        //transform.rotation = Quaternion.AngleAxis(Vector2.Angle((Vector2) camera.ScreenToWorldPoint(Cursor_position), rb.position), -Vector3.forward);
        //transform.LookAt((Vector2)camera.ScreenToWorldPoint(Cursor_position), );
        
        RotatePlayer(Wind_Direction);

    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(Vector2.up * APPLIED_FORCE);
        }
        if (Input.GetKey(KeyCode.A)) {
            rb.AddForce(Vector2.left * APPLIED_FORCE);
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(Vector2.down * APPLIED_FORCE);
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(Vector2.right * APPLIED_FORCE);
        }
    }

    void RotatePlayer(Vector2 dir)
    {
        float angle = Vector2.SignedAngle(Vector2.up, dir);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
