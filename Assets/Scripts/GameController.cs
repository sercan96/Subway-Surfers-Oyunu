using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject altin;

    public GameObject miknatis;
    public GameObject araba;
    public GameObject tas;
    public GameObject kutuk;

    List<GameObject> altinlar;  //Altýnlar buradaki listeden çýkacak
    List<GameObject> digerleri; // Diðer objeler rastgelebu listeden çýkacak.

    Transform cocuk;

    private void Start()
    {
        cocuk = GameObject.Find("cocuk").transform;

        altinlar = new List<GameObject>();
        digerleri = new List<GameObject>();


        uretme(altin, 10, altinlar);   // uretme fonksiyonuna nelerin olduðunu belirledik.
        uretme(miknatis, 3, digerleri);
        uretme(araba, 3, digerleri);
        uretme(tas, 3, digerleri);
        uretme(kutuk, 3, digerleri);

        InvokeRepeating("altin_uret", 0.0f, 1.0f); // 1-Hangi fonksiyon, 2- Ne zaman çalýþacak,3- Kaç saniyede bir çalýþacak.
        InvokeRepeating("engel_uret", 1f, 3f);


    }

    void engel_uret()  // diðer tüm objeler için bu fonksiyon oluþturuldu
    {
        int rast = Random.Range(0, digerleri.Count);  // 0 ile diðerleri yani 12 obje arasýnda rastgele birini seçmesini saðlýcaz.
        if (digerleri[rast].activeSelf == false)  // diðer objeler görünür deðilse
        {
            digerleri[rast].SetActive(true);

            int rastgele = Random.Range(0, 2);
            if (rastgele == 0)
            {
                digerleri[rast].transform.position = new Vector3(-2f, 0f, cocuk.position.z + 10f);  // z ekseni cocuðun ilerlediði taraf olduðu için altýn her zaman ilerisinde olmasý gerekir.
            }
            else
            {
                digerleri[rast].transform.position = new Vector3(1.19f, 0f, cocuk.position.z + 10f);
            }

            return;
        }
        else  // Olaki çýkan sayýlardan biri daha öncede çýkmýþtýr ve görünür haldedir. Eðer öyleyse yani else;
        {
            foreach (GameObject nesne in digerleri)
            {
                if (nesne.activeSelf == false)
                {
                    nesne.SetActive(true);

                    int rastgele = Random.Range(0, 2);
                    if (rastgele == 0)
                    {
                        nesne.transform.position = new Vector3(-2f, 0f, cocuk.position.z + 10f);  // z ekseni cocuðun ilerlediði taraf olduðu için altýn her zaman ilerisinde olmasý gerekir.
                    }
                    else
                    {
                        nesne.transform.position = new Vector3(1.19f, 0f, cocuk.position.z + 10f);
                    }

                    return;
                }
            }
        }
    }





    void altin_uret()
    {
        foreach (GameObject altin in altinlar)    // 1- Hangi obje, 2- Hangi liste
        {
            if (altin.activeSelf == false)   // altinlar pasif durumda ise
            {
                altin.SetActive(true); // Altin objesini aktif yap

                int rastgele = Random.Range(0, 2);
                if (rastgele == 0)
                {
                    altin.transform.position = new Vector3(-2f, 0.5f, cocuk.position.z + 10f);  // z ekseni cocuðun ilerlediði taraf olduðu için altýn her zaman ilerisinde olmasý gerekir.
                }
                else
                {
                    altin.transform.position = new Vector3(1.19f, 0.5f, cocuk.position.z + 10f);
                }

                return; // Bir kere aktif yapýp tekrar pasif duruma geçmesini saðlar. Aksi halde ne kadar görünmez altýnýmýz varsa hepsini aktif yapacaktýr.
            }
        }
    }


    // oyun baþladýðýnda belli bir miktar altýn ve diðerlerini üreten bir fonks. olacak(uretme)
    // 1-) üretilecek nesne
    // 2-) Miktar
    // 3-) Hangi listeye eklenicek
    public void uretme(GameObject nesne, int miktar, List<GameObject> liste)
    {
        for (int i = 0; i < miktar; i++)
        {
            GameObject yeni_nesne = Instantiate(nesne); // nesne oluþsun. Bu nesneyi fonksiyonu çaðýrýrken biz belirleyeceðiz.
            yeni_nesne.SetActive(false); // Bunu yapmamýzýn sebebi hepsinin görünür olmasýný engellemek.
            liste.Add(yeni_nesne);
        }
    }
}

