using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroll : MonoBehaviour
{
    public Transform hedef;
    public Vector3 mesafe;
    [SerializeField]
    private float farehassasiyeti;
    float fareX;
    float fareY;

    Vector3 objrot;
    public Transform charrotatiaon;
    karakterkontrol karakterhp;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        karakterhp = GameObject.Find("Player").GetComponent<karakterkontrol>();
    }

   
    void Update()
    {
        
    }
    void LateUpdate()
    {
        if (karakterhp != null && karakterhp.hayattami())
        {
            
            this.transform.position = Vector3.Lerp(this.transform.position, hedef.position + mesafe, Time.deltaTime * 10);

            
            fareX += Input.GetAxis("Mouse X") * farehassasiyeti;
            fareY -= Input.GetAxis("Mouse Y") * farehassasiyeti;

            
            fareY = Mathf.Clamp(fareY, -40f, 30f);

            
            this.transform.rotation = Quaternion.Euler(fareY, fareX, 0);

            // Karakter (vücut) dönüþü
            Vector3 bodyRotation = charrotatiaon.localEulerAngles;
            bodyRotation.x = fareY; 
            bodyRotation.y = 0;     
            bodyRotation.z = 0;     
            charrotatiaon.localEulerAngles = bodyRotation;

            // Karakter hedefinin dönüþü (saða/sola)
            Vector3 characterRotation = hedef.eulerAngles;
            characterRotation.y = fareX;
            characterRotation.x = 0;
            characterRotation.z = 0;
            hedef.eulerAngles = characterRotation;
        }

        
    }
}
