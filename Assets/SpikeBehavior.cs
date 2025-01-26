using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeBehavior : MonoBehaviour
{
    [SerializeField] AudioSource popSFX;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision){
        popSFX.Play();

        Destroy(collision.gameObject);
    }
}
