using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firecontrol : MonoBehaviour
{
    public Camera kamera;
    public LayerMask monsterkatman;
    private karakterkontrol hpkontrol;
    private Animator anim;

    public float atesEtmeAraligi = 0.2f; 
    private bool atesEdiyor = false;

    AudioSource seskaynagi;
    public AudioClip atesetmeses;
    public AudioClip sarjorses;

    public ParticleSystem muzzle;

    private float sarjor = 30; 
    private float cepane = 300; 
    private float sarjorkapasite = 30; 

    void Start()
    {
        if (kamera == null)
        {
            kamera = Camera.main;
        }
        hpkontrol = GetComponent<karakterkontrol>();
        anim = GetComponent<Animator>();
        seskaynagi = GetComponent<AudioSource>();
        muzzle = GetComponentInChildren<ParticleSystem>();
      

    }

    void Update()
    {
        if (hpkontrol != null && hpkontrol.hayattami())
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                if (sarjor > 0)
                {
                    ateset(); 
                    anim.SetBool("atesetme", true); 

                    if (muzzle != null)
                    {
                        muzzle.Play(); 
                    }

                    seskaynagi.PlayOneShot(atesetmeses); 
                }
                else if (cepane > 0 && !anim.GetBool("sarjordegistirme"))
                {
                    StartCoroutine(Reload()); 
                }
            }
            else if (Input.GetMouseButtonUp(0)) 
            {
                anim.SetBool("atesetme", false); 

                if (muzzle != null && muzzle.isPlaying)
                {
                    muzzle.Stop(); 
                }
            }

            if (Input.GetKeyDown(KeyCode.R) && cepane > 0 && sarjor < sarjorkapasite)
            {
                StartCoroutine(Reload()); 
            }
        }

    }

    private IEnumerator AtesEtmeDöngüsü()
    {
        atesEdiyor = true;
        while (atesEdiyor && sarjor > 0)
        {
            ateset(); 
            yield return new WaitForSeconds(atesEtmeAraligi); 
        }

        
        if (sarjor <= 0 && cepane > 0)
        {
            
            StartCoroutine(Reload());
        }
    }
    public void sarjordegistirmese() 
    {
        seskaynagi.PlayOneShot(sarjorses);
    }

    public void ateset()
    {
        sarjor--;

        
        Ray ray = kamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, monsterkatman))
        {
            monster monsterScript = hit.collider.GetComponent<monster>();
            if (monsterScript != null)
            {
                monsterScript.hasar(); 
                
            }
            else
            {
                
            }
        }
        else
        {
            Debug.Log("Hiçbir nesneye çarpmadý.");
        }
    }

    private IEnumerator Reload()
    {
        anim.SetBool("sarjordegistirme", true);

        
        yield return new WaitForSeconds(2f); 

        
        float mermiler = Mathf.Min(cepane, sarjorkapasite - sarjor); 
        cepane -= mermiler; 
        sarjor += mermiler; 

        anim.SetBool("sarjordegistirme", false); 
       
    }
    private IEnumerator OynatAtesSesi()
    {
        seskaynagi.PlayOneShot(atesetmeses); 
        yield return null; 
    }

   

    public float getsarjor()
    {
        return sarjor;
    }
    public float getcephane()
    {
        return cepane;
    }
}
