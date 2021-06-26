using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Atas Nama")]
    public TextMeshProUGUI atasnamaText;

    [Header("Pemeriksaan")]
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI tunggakanText;

    [Header("Penyidikan")]
    public TextMeshProUGUI hintText;
    public GameObject progressBar;
    public GameObject hasilPenyidikanPanel;
    public TextMeshProUGUI hasilPenyidikanText;

    //[Header("Penagihan")]
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

}
