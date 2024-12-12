using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        CallupNextScene();
    }
    public void CallupNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
