using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    [Header("Atribut Umum")]
    public string name;
    public int missionType;
    public int houseNumber;

    [Header("Atribut Pemeriksaan")]
    public string jumlahTunggakan;
    [System.Serializable]
    public class DateDetail
    {
        public int day;
        public int month;
        public int year;
    }
    public DateDetail jatuhTempo;

    [Header("Atribut Penyidikan")]
    public string namaJalan;
    public string warnaRumah;

    [Header("Atribut Penagihan")]
    public bool selesai = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void AssignMission(int i)
    {
        houseNumber = i;
        GameManager.instance.houseArray[i].GetComponent<HouseManager>().onGoing = true;

        name = GameManager.instance.houseArray[i].GetComponent<HouseManager>().name;
        UIManager.instance.atasnamaText.text = $"a/n {name}";
        
        missionType = GameManager.instance.houseArray[i].GetComponent<HouseManager>().missionType;
        
        jumlahTunggakan = GameManager.instance.houseArray[i].GetComponent<HouseManager>().jumlahTunggakan;
        UIManager.instance.tunggakanText.text = jumlahTunggakan;

        jatuhTempo.day = GameManager.instance.houseArray[i].GetComponent<HouseManager>().jatuhTempo.day;
        jatuhTempo.month = GameManager.instance.houseArray[i].GetComponent<HouseManager>().jatuhTempo.month;
        jatuhTempo.year = GameManager.instance.houseArray[i].GetComponent<HouseManager>().jatuhTempo.year;
        UIManager.instance.dateText.text = $"{jatuhTempo.day}/{jatuhTempo.month}/{jatuhTempo.year}";


        namaJalan = GameManager.instance.houseArray[i].GetComponent<HouseManager>().namaJalan;
        warnaRumah = GameManager.instance.houseArray[i].GetComponent<HouseManager>().warnaRumah;
        UIManager.instance.hintText.text = $"{namaJalan}\n{warnaRumah}";

        selesai = GameManager.instance.houseArray[i].GetComponent<HouseManager>().selesai;
    }
    
    public void SeekHouse()
    {
        GameManager.instance.houseArray[houseNumber].GetComponent<HouseManager>().ShowPinPoint();
    }

    public void Penyidikan()
    {
        UIManager.instance.progressBar.SetActive(true);
        UIManager.instance.progressBar.GetComponent<FillBar>().progressSidik = true;
    }

    public void HasilPenyidikan()
    {
        UIManager.instance.progressBar.SetActive(false);
        UIManager.instance.progressBar.GetComponent<FillBar>().progressSidik = false;

        //Pop Up hasil penyidikan
        //UIManager.instance.hasilPenyidikanPanel.SetActive(true);
    }

}
