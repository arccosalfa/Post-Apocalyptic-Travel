using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arayuz : MonoBehaviour
{
    public Text mermitext;
    public Text hptext;
    GameObject oyuncu;
    void Start()
    {
        oyuncu = GameObject.Find("Player");
    }

    
    void Update()
    {
        mermitext.text = oyuncu.GetComponent<firecontrol>().getsarjor().ToString()+"/"+ oyuncu.GetComponent<firecontrol>().getcephane().ToString();
        hptext.text = "HP:" + oyuncu.GetComponent<karakterkontrol>().gethp();
    }
}
