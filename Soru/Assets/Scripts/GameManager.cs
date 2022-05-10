using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int soruIndex;

    public GameObject HosgeldinPaneli;
    public GameObject SoruPaneli;
    public GameObject SonucPaneli;

    public Text soruEditor;
    public Text SonucEditor;
    public List<Text> SoruSikkiEditor;

    List<Soru> Sorular;
    public enum Durumlar
    {
        Hosgeldin,
        Soru,
        Sonuc
    }

    private Durumlar durum;
    // Start is called before the first frame update
    void Start()
    {
        Sorular = new List<Soru>();

        soruIndex = 0;

        //hiçbir panel gözükmeycek
        durum = Durumlar.Hosgeldin;
        HosgeldinPaneli.SetActive(false);
        SoruPaneli.SetActive(false);
        SonucPaneli.SetActive(false);

        SoruUret("Türkiye'nin baþkenti hangisidir", new List<string>{ "Ýstanbul", "Ankara", "Ýzmir", "Bursa" }, "B");
       
        SoruUret("Su kaç derecede kaynar", new List<string> { "70", "80", "90", "100" }, "D");

        SoruUret("Fatih Sultan Mehmet kaç yýlýnda Ýstanbul' u feth etmiþtir", new List<string> { "1923", "1881", "1453", "2023" }, "C");



    }

    // Update is called once per frame
    void Update()
    {
        switch (durum)
        {
            case Durumlar.Hosgeldin:
                Hosgeldin();
                break;
            case Durumlar.Soru:
                Soru();
                break;
            case Durumlar.Sonuc:
                Sonuc();
                break;
        }
    }

    private void SoruUret(string soruMetin, List<string> soruSik, string dogruSik)
    {
        Soru soru = new Soru();

        soru.SoruMetin = soruMetin;
        soru.DogruSikHarfi = dogruSik;
        soru.Siklar = soruSik;
        Sorular.Add(soru);
    }

    private void Hosgeldin()
    {
        HosgeldinPaneli.SetActive(true);
        SoruPaneli.SetActive(false);
        SonucPaneli.SetActive(false);
        soruIndex = 0;
    }

    private void Soru()
    {
        soruEditor.text = Sorular[soruIndex].SoruMetin;

        int soruSikIndex = 0;
        foreach(var s in SoruSikkiEditor)
        {

            s.text = Sorular[soruIndex].Siklar[soruSikIndex];
            soruSikIndex++;
        }

        HosgeldinPaneli.SetActive(false);
        SoruPaneli.SetActive(true);
        SonucPaneli.SetActive(false);
    }
    private void Sonuc()
    {
        HosgeldinPaneli.SetActive(false);
        SoruPaneli.SetActive(false);
        SonucPaneli.SetActive(true);


    }

    public void DogruSecenek(string secenek)
    {
        if (secenek == Sorular[soruIndex].DogruSikHarfi)
        {
            SonucEditor.text = "Doðru";
        }
        else
        {
            SonucEditor.text = "Yanlýþ";
        }
        
        durum = Durumlar.Sonuc;
    }

    public void HosgeldinButonu()
    {
        durum = Durumlar.Soru;
    }

    public void SonucButonu()
    {
        soruIndex++;
        if (soruIndex == Sorular.Count)
        {

            durum = Durumlar.Hosgeldin;
            return;
        }
        durum = Durumlar.Soru;
    }
}
