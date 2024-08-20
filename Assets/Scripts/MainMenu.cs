using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.Rebind();
        animator.Update(0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("UndeadAssault", LoadSceneMode.Single);
    }

    public void Options()
    {
        //options
    }

    public void Qiut()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
