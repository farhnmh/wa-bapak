using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    [Header("Atribut Umum")]
    public string name;
    public int missionType;
    public GameObject pinPoint;
    public Transform pinLocation;

    [Header("Atribut Pemeriksaan")]
    public string jumlahTunggakan;
    public bool onGoing;

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
    public bool penyidikan;

    [Header("Atribut Penagihan")]
    public bool selesai = false;

    void Start()
    {
        onGoing = false;

        // random tanggal jatuh tempo
        jatuhTempo.day = UnityEngine.Random.Range(1, 31);
        jatuhTempo.month = UnityEngine.Random.Range(1, 13);
        jatuhTempo.year = 2021;

        // random jumlah tunggakan
        int HaveTunggakan = Random.Range(0, 4);
        if (HaveTunggakan == 0)
            jumlahTunggakan = $"Rp. {0}";
        else if (HaveTunggakan > 0)
            jumlahTunggakan = $"Rp. {Random.Range(1, 6)} Juta";
    }

    public void ShowPinPoint()
    {
        Instantiate(pinPoint, new Vector3(pinLocation.position.x, pinLocation.position.y, pinLocation.position.z), Quaternion.identity);
    }

    
}
