using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    private float timer1 = 3f;
    private float oldTimer1 = 0;
    private int counter1 = 0;
    private int maxCounts = 10;
    private string egg;
    private bool triggered = false;

    private void OnEnable() {
        triggered = false;
    }
    private void Start() {
        oldTimer1 = timer1;
        egg = "ht" + "tps://w" + "ww.y" + "ou" + "tu" + "be.com/wat" + "ch?v=dQw4w" + "9WgXcQ";
    }
    void Update()
    {
        //Debug.Log(counter1);
        if(Input.GetKeyDown(KeyCode.M))
        {
            AddOneToCount();
        }

        timer1 -= Time.deltaTime;

        if(timer1 <= 0)
        {
            if(counter1 >= maxCounts && !triggered)
            {
                counter1 = 0;
                triggered = true;
                timer1 = oldTimer1*5;
                Application.OpenURL(egg);
            }
            else
            {
                counter1 = 0;
                timer1 = oldTimer1;
            }
        }
    }

    public void AddOneToCount()
    {
        counter1++;
    }
}
