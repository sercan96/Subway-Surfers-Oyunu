using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yok_ol : MonoBehaviour
{
    // Karakterim kutuk,araba,tas objelerini geçtiyse yok olsun.
    Transform cocuk;

    private void Start()
    {
        cocuk = GameObject.Find("cocuk").GetComponent<Transform>();
    }
    void Update()
    {
        if (transform.position.z < (cocuk.position.z - 5.0f))
        {
            gameObject.SetActive(false);
        }



        //float mesafe = Vector3.Distance(transform.position, cocuk.position);
        //if(mesafe <= 0)
        //{
        //    gameObject.SetActive(false);
        //}


    }

}
