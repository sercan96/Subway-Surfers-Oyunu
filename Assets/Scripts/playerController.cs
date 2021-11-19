using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public GameObject toz;
    bool zipladi = false;
    float hiz = 2f;
    bool sag;
    bool sol;
    float ziplamahizi = 5f;
    Rigidbody rigi;


    private void Start()
    {
        rigi = GetComponent<Rigidbody>();
    }
    private void OnCollisionStay(Collision collision)  //�arp��ma devam ediyorken
    {
        zipladi = false;
        if(toz.activeSelf == false)
        {
            toz.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision collision)  //�arp��ma bitti�inde
    {
        zipladi = true;
        if (toz.activeSelf == true)
        {
            toz.SetActive(false);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * hiz);
        Vector3 saggit = new Vector3(1f, transform.position.y, transform.position.z); // Sa� 1 kadar gidebilsin
        Vector3 solgit = new Vector3(-2.35f, transform.position.y, transform.position.z); // Sol -2.35 kadar gidebilsin.
        if (Input.touchCount > 0)
        {
            Touch parmak = Input.GetTouch(0);

            if(parmak.deltaPosition.x > 50.0f)
            {
                sag = true;
                sol = false;
            }
            if (parmak.deltaPosition.x < -50.0f)
            {
                sag = false;
                sol = true;
            }
            //if (parmak.deltaPosition.y > 50.0f)
            //{
            //    rigi.velocity = Vector3.zero;
            //    rigi.velocity = Vector3.up * ziplamahizi;
            //}
            if (sag==true)
            {
                transform.position = Vector3.Lerp(transform.position, saggit, Time.deltaTime*hiz);
            }
            if (sol == true)
            {
                transform.position = Vector3.Lerp(transform.position, solgit, Time.deltaTime *hiz);
            }
        }

    }
    public void Ziplabtn()
    {
       
            rigi.velocity = Vector3.zero; // Her zaman e�it bir �ekilde z�plamas�n� sa�lar.
            rigi.velocity = Vector3.up * ziplamahizi *Time.deltaTime ;
        
       
    }







    //public void KarakterHareket()
    //{
    //Z y�n�nde sabit hareket X y�n�nde sa� sol ve Y y�n�nde z�plamay� ger�ekle�tiren kod.

    //float horizontal = Input.GetAxis("Horizontal");
    ////float vertical = Input.GetAxis("Vertical");
    //float jump = Input.GetAxis("Jump");
    //transform.Translate(new Vector3(horizontal * Time.deltaTime * 3f, jump * Time.deltaTime * 5f));
    //transform.Translate(Vector3.forward*Time.deltaTime*3f);

    //}

}

