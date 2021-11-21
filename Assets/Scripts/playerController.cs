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
    public GameObject oyun_bitti_paneli;

    public int desiredLane = 1;
    public float laneDistance = 2.5f;


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
        SetLane();
        //if (Input.touchCount > 0)
        //{
        //    Touch parmak = Input.GetTouch(0);

        //    if(parmak.deltaPosition.x > 50.0f)
        //    {
        //        sag = true;
        //        sol = false;
        //    }
        //    if (parmak.deltaPosition.x < -50.0f)
        //    {
        //        sag = false;
        //        sol = true;
        //    }
        //    if (parmak.deltaPosition.y > 50.0f)
        //    {
        //        rigi.velocity = Vector3.zero;
        //        rigi.velocity = Vector3.up * ziplamahizi ;
        //    }

        //}

    }
    private void FixedUpdate()
    {
        ChangeLane();
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("engel"))
        {
            oyun_bitti_paneli.SetActive(true);
            Time.timeScale = 0.0f; // Oyunu durdur.
        }
    }

    public void ChangeLane()
    {
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0) targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2) targetPosition += Vector3.right * laneDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, 10 * Time.deltaTime);
    }

    private void SetLane()
    {
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3) desiredLane = 2;
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1) desiredLane = 0;
        }
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

