using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGoal : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] String NextLevel;
    SceneLoader sceneLoader;
    void Start()
    {
        sceneLoader = FindAnyObjectByType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        Debug.Log(collision.gameObject.ToString());
        if(LayerMask.LayerToName(collision.gameObject.layer) == "Bubble"){
            sceneLoader.LoadScene(NextLevel);
        //
        }
    }
}
