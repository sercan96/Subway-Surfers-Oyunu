using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    

    public GameObject altin;

    public GameObject miknatis;
    public GameObject araba;
    public GameObject tas;
    public GameObject kutuk;

    List<GameObject> altinlar;  //Alt?nlar buradaki listeden ??kacak
    List<GameObject> digerleri; // Di?er objeler rastgelebu listeden ??kacak.

    Transform cocuk;

    int altinPuan;

    public TMPro.TextMeshProUGUI puanText;  // TMP eri?mek i?in bunu kullan?yoruz


    playerController playerC;

    public GameObject oyun_durdurma_paneli;


    private void Start()
    {
        cocuk = GameObject.Find("cocuk").transform;

        altinlar = new List<GameObject>();
        digerleri = new List<GameObject>();


        uretme(altin, 10, altinlar);   // uretme fonksiyonuna nelerin oldu?unu belirledik.
        uretme(miknatis, 3, digerleri);
        uretme(araba, 3, digerleri);
        uretme(tas, 3, digerleri);
        uretme(kutuk, 3, digerleri);

        InvokeRepeating("altin_uret", 0.0f, 1.0f); // 1-Hangi fonksiyon, 2- Ne zaman ?al??acak,3- Ka? saniyede bir ?al??acak.
        InvokeRepeating("engel_uret", 1.5f, 3.0f);  // 1.5f dedi?i ?ey ayn? anda ayn? yerde objelerin ?retilmemesi

        playerC = GameObject.Find("cocuk").GetComponent<playerController>();


    }

    void engel_uret()  // di?er t?m objeler i?in bu fonksiyon olu?turuldu
    {
        int rast = Random.Range(0, digerleri.Count);  // 0 ile di?erleri yani 12 obje aras?nda rastgele birini se?mesini sa?l?caz.
        if (digerleri[rast].activeSelf == false)  // di?er objeler g?r?n?r de?ilse
        {
            digerleri[rast].SetActive(true);

            int rastgele = Random.Range(0, 2);
            if (rastgele == 0)
            {
                digerleri[rast].transform.position = new Vector3(-1.97f, 0f, cocuk.position.z + 10f);  // z ekseni cocu?un ilerledi?i taraf oldu?u i?in alt?n her zaman ilerisinde olmas? gerekir.
            }
            else
            {
                digerleri[rast].transform.position = new Vector3(0.55f, 0f, cocuk.position.z + 10f);
            }
            if(digerleri[rast].tag == "Miknatis")
            {
                if(playerC.miknatis_alindi== true)
                {
                    miknatis.gameObject.SetActive(false);
                }
            }

            if (digerleri[rast].name == "Miknatis(Clone)")
            {
                digerleri[rast].transform.position = new Vector3(-1.97f, 0.53f, cocuk.position.z + 10f);
            }
            if(digerleri[rast].name == "araba(Clone")
            {
                digerleri[rast].transform.position = new Vector3(-1.97f, 0.2f, cocuk.position.z + 10f);
            }

            return;
        }
        else  // Olaki ??kan say?lardan biri daha ?ncede ??km??t?r ve g?r?n?r haldedir. E?er ?yleyse yani else;
        {
            foreach (GameObject nesne in digerleri)
            {
                if (nesne.activeSelf == false)
                {
                    nesne.SetActive(true);

                    int rastgele = Random.Range(0, 2);
                    if (rastgele == 0)
                    {
                        nesne.transform.position = new Vector3(-1.97f, 0f, cocuk.position.z + 10f);  // z ekseni cocu?un ilerledi?i taraf oldu?u i?in alt?n her zaman ilerisinde olmas? gerekir.
                    }
                    else
                    {
                        nesne.transform.position = new Vector3(0.55f, 0f, cocuk.position.z + 10f);
                    }
                    if (nesne.tag == "Miknatis")
                    {
                        if (playerC.miknatis_alindi == true)
                        {
                            miknatis.gameObject.SetActive(false);
                            
                        }
                    }
                    if(nesne.name =="Miknatis(Clone)")
                    {
                        nesne.transform.position = new Vector3(-1.97f, 0.53f, cocuk.position.z + 10f);
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
                    altin.transform.position = new Vector3(-2f, 0.5f, cocuk.position.z + 10f);  // z ekseni cocu?un ilerledi?i taraf oldu?u i?in alt?n her zaman ilerisinde olmas? gerekir.
                }
                else
                {
                    altin.transform.position = new Vector3(1.19f, 0.5f, cocuk.position.z + 10f);
                }

                return; // Bir kere aktif yap?p tekrar pasif duruma ge?mesini sa?lar. Aksi halde ne kadar g?r?nmez alt?n?m?z varsa hepsini aktif yapacakt?r.
            }
        }
    }


    // oyun ba?lad???nda belli bir miktar alt?n ve di?erlerini ?reten bir fonks. olacak(uretme)
    // 1-) ?retilecek nesne
    // 2-) Miktar
    // 3-) Hangi listeye eklenicek
    public void uretme(GameObject nesne, int miktar, List<GameObject> liste)
    {
        for (int i = 0; i < miktar; i++)
        {
            GameObject yeni_nesne = Instantiate(nesne); // nesne olu?sun. Bu nesneyi fonksiyonu ?a??r?rken biz belirleyece?iz.
            yeni_nesne.SetActive(false); // Bunu yapmam?z?n sebebi hepsinin g?r?n?r olmas?n? engellemek.
            liste.Add(yeni_nesne);
        }
    }
    public void puanArttir()
    {
        altinPuan += 10;
        puanText.text = "SKOR : " + altinPuan;
    }
    
    public void tekrarOyna()
    {
        SceneManager.LoadScene("oyun");
        Time.timeScale = 1.0f; // oyun ba?las?n.
    }

    public void oyundanCik()
    {
        Application.Quit();
    }

    public void DevamEt()
    {
        oyun_durdurma_paneli.SetActive(false);

        Time.timeScale = 1.0f;
    }
    public void oyunDurdur()
    {
        oyun_durdurma_paneli.SetActive(true);
        Time.timeScale = 0.0f;
    }
}

