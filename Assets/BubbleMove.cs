using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BubbleMove : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    Vector2 MAX_VELOCITY = new Vector2(0, 1);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate() {
        //if(Mathf.Abs(rb.velocity.y) > 2) {
        //    rb.velocity = MAX_VELOCITY;
        //}
    }
}
