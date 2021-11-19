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
    float ziplamahizi = 4f;
    Rigidbody rigi;
    Animator anim;
    Transform yol_1;
    Transform yol_2;
    GameController gameC;
    public bool miknatis_alindi = false;


    private void Start()
    {
        rigi = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        yol_1 = GameObject.Find("yol_1").transform;
        yol_2 = GameObject.Find("yol_2").transform;

        gameC = GameObject.Find("_Script").GetComponent<GameController>();
    }
    private void OnCollisionStay(Collision collision)  //Çarpýþma devam ediyorken
    {
        zipladi = false;
        if(toz.activeSelf == false)
        {
            toz.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision collision)  //Çarpýþma bittiðinde
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
        Vector3 saggit = new Vector3(1f, transform.position.y, transform.position.z); // Sað 1 kadar gidebilsin
        Vector3 solgit = new Vector3(-2.35f, transform.position.y, transform.position.z); // Sol -2.35 kadar gidebilsin.
        if (Input.touchCount > 0)
        {
            Touch parmak = Input.GetTouch(0);

            if(parmak.deltaPosition.x > 100.0f)
            {
                sag = true;
                sol = false;
            }
            if (parmak.deltaPosition.x < -100.0f)
            {
                sag = false;
                sol = true;
            }
            if (parmak.deltaPosition.y > 100.0f)
            {
                rigi.velocity = Vector3.zero;
                rigi.velocity = Vector3.up * ziplamahizi;
            }
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
        if(zipladi ==false)
        {
            anim.SetTrigger("ziplama");
            rigi.velocity = Vector3.zero; //Her zaman eþit bir þekilde zýplamasýný saðlar.
            rigi.velocity = Vector3.up * ziplamahizi; // Zýplarken Time.delta.Time kullanýlmaz.
           
        }
   
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name =="yol_1")
        {
            yol_2.position = new Vector3(yol_2.position.x, yol_2.position.y, yol_1.position.z + 10.0f);
        }
        if (other.gameObject.name == "yol_2")
        {
            yol_1.position = new Vector3(yol_1.position.x, yol_1.position.y, yol_2.position.z + 10.0f);
        }

        if(other.gameObject.CompareTag("Altin"))
        {
            other.gameObject.SetActive(false);
            gameC.puanArttir();
            
        }
        if(other.gameObject.tag == "Miknatis")
        {
            other.gameObject.SetActive(false);
            miknatis_alindi = true;

            Invoke("miknatis_iptal", 10.0f);  // 10 saniye sonra bu fonksiyonu çalýþtýr.
        }
        
    }
    public void miknatis_iptal()
    {
        miknatis_alindi = false;
    }
        
        







    //public void KarakterHareket()
    //{
    //Z yönünde sabit hareket X yönünde sað sol ve Y yönünde zýplamayý gerçekleþtiren kod.

    //float horizontal = Input.GetAxis("Horizontal");
    ////float vertical = Input.GetAxis("Vertical");
    //float jump = Input.GetAxis("Jump");
    //transform.Translate(new Vector3(horizontal * Time.deltaTime * 3f, jump * Time.deltaTime * 5f));
    //transform.Translate(Vector3.forward*Time.deltaTime*3f);

    //}

}

