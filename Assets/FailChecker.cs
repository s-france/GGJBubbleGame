using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FailChecker : MonoBehaviour


{
    
    [SerializeField] GameObject character;
    [SerializeField] GameObject bubble;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Invoke("CheckFailure", 5);
    }

    void CheckFailure() {
        if(character == null || bubble == null) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
