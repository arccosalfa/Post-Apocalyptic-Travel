using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karakterkontrol : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    private float charhýz; 
    [SerializeField]
    private float kosmaHiz; 
    private float hp = 100;
    bool yasiyormu;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        yasiyormu = true;
    }

    void Update()
    {
        if (hp <= 0)
        {
            yasiyormu = false;
            animator.SetBool("yasiyormu", yasiyormu);
        }
        if (yasiyormu == true)
        {
            hareket();
        }
    }

    public float gethp()
    {
        return hp;
    }

    public bool hayattami()
    {
        return yasiyormu;
    }

    void hareket()
    {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool("Running", isRunning);

        
        float aktifHiz = isRunning ? kosmaHiz : charhýz;

        
        animator.SetFloat("horizontal", yatay);
        animator.SetFloat("vertical", dikey);

        
        this.gameObject.transform.Translate(yatay * aktifHiz * Time.deltaTime, 0, dikey * aktifHiz * Time.deltaTime);
    }

    public void hasaral()
    {
        hp -= Random.Range(5, 12);
    }
}
