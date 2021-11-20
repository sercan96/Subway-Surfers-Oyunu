using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altin : MonoBehaviour
{
    playerController playerC;
    Transform cocuk;
    void Start()
    {
        playerC = GameObject.Find("cocuk").GetComponent<playerController>();
        cocuk = GameObject.Find("cocuk").GetComponent<Transform>();
    }

   
    void Update()
    {
        float mesafe = Vector3.Distance(transform.position, cocuk.position);
        if (playerC.miknatis_alindi == true)
        {
      
            if(mesafe < 5.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, cocuk.position, Time.deltaTime * 5f);
            }
            // Altýnýn kendi pozisyonundan çoçuðun pozisyonuna ilerlemesini saðlayacak komut.
        }
        if(mesafe <= 0) //obje çocugun gerisinde kaldýysa
        {
            gameObject.SetActive(false);
        }
    }
}
