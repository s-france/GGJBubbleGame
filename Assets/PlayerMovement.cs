using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject windBox;
    Camera camera;
    [SerializeField] Collider2D col;
    bool onEdge = false;
    Vector2 Cursor_position = new Vector2();
    Vector2 Wind_Direction = new Vector2();
    float APPLIED_FORCE = 20;
    //Vector2 Up = new Vector2.up;

    Vector2 horizontal_input;
    Vector2 vertical_input;

    Vector2[] edges;
    ContactPoint2D[] contacts;
    


    void Start()
    {
        camera = FindFirstObjectByType<Camera>();

        edges = new Vector2[10];
        contacts = new ContactPoint2D[10];

        horizontal_input = Vector2.zero;
        vertical_input = Vector2.zero;
        
    }

    // Update is called once per frame
    void Update()
    {
        ReadInputs();

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

        if(MoveAlongEdge())
        {
            onEdge = true;
            
        } else
        {
            onEdge = false;

            //old free movement
            /*
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
            */



        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            windBox.SetActive(true);

            if(!onEdge)
            {
                rb.AddForce(-Wind_Direction.normalized * APPLIED_FORCE);
            }



        } else
        {
            windBox.SetActive(false);
        }

        


    }

    void ReadInputs()
    {
        vertical_input = Vector2.zero;
        horizontal_input = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            vertical_input += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vertical_input += Vector2.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontal_input += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontal_input += Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.Space) && onEdge)
        {
            EdgeDetach(0);
            //touching_edge = false;
        } 
        
    }

    void RotatePlayer(Vector2 dir)
    {
        float angle = Vector2.SignedAngle(Vector2.up, dir);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer) == "Walls")
        {
            Debug.Log("touched wall!");

            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }
    }

    void EdgeDetach(int idx)
    {
        Debug.Log("Edge Detach!");

        rb.AddForce(contacts[idx].normal * 5, ForceMode2D.Impulse);
    }

    bool MoveAlongEdge()
    {
        bool touching_edge = false;

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(EdgeDetach());
            return touching_edge;
        }
        */

        for(int i = 0; i<col.GetContacts(contacts); i++)
        {
            ContactPoint2D contact = contacts[i];
            edges[i] = Vector2.Perpendicular(contact.normal);

            Vector2 horizontal = new Vector2(edges[i].x, 0);
            Vector2 vertical = new Vector2(0, edges[i].y);

            if (vertical_input.magnitude > 0 && vertical.magnitude > 0)
            {
                rb.AddForce(vertical_input.y * -edges[i] * APPLIED_FORCE);
            }

            if (horizontal_input.magnitude >0 && horizontal.magnitude > 0)
            {
                rb.AddForce(horizontal_input.x * -edges[i] * APPLIED_FORCE);
            }
            
            touching_edge = true;
            


        }

        return touching_edge;



    }

}
