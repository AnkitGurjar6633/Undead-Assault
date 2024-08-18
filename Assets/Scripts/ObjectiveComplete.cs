using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveComplete : MonoBehaviour
{
    [Header("Objectives to Complete")]
    public TMP_Text objective1;
    public TMP_Text objective2;
    public TMP_Text objective3;
    public TMP_Text objective4;


    public static ObjectiveComplete instance;




    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Objective1Done()
    {
        objective1.text = "--" + objective1.text + "--";
        objective1.fontStyle = FontStyles.Strikethrough;
    }
    public void Objective2Done()
    {
        objective2.text = "--" + objective2.text + "--";
        objective2.fontStyle = FontStyles.Strikethrough;
    }
    public void Objective3Done()
    {
        objective3.text = "--" + objective3.text + "--";
        objective3.fontStyle = FontStyles.Strikethrough;
    }
    public void Objective4Done()
    {
        objective4.text = "--" + objective4.text + "--";
        objective4.fontStyle = FontStyles.Strikethrough;
    }
}
