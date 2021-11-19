using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    Transform cocuk;
    float hiz = 4f;
    void Start()
    {
        cocuk = GameObject.Find("cocuk").transform;   
    }

   
    void Update()
    {
        Vector3 pos = new Vector3(cocuk.position.x, transform.position.y, cocuk.position.z - 1.5f);
        transform.position = Vector3.Lerp(transform.position, pos, hiz*Time.deltaTime);   //Kamera pozisyonu ile cocuk obejesi arasýnda belli bir süre gecikmeli gitsin.
        //transform.position = pos;  //Obje hareket ettiðinde ayný anda kamerada hareket eder.
    }

}
