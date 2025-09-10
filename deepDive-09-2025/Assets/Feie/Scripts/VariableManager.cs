using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

public class VariableManager : MonoBehaviour
{
    public class VeriableData
    {
        public int index { get; set; }

        public int age_1a_q_1;
        public string gender_T1_M;
        public float bodylength_cm_all_m_1_T1_M;
        public float bodyweight_kg_all_m_1_T1_M;
    }

    public class Root
    {
        public VeriableData[] data { get; set; }
    }
    private static Root allData;
    private string fileDirectory;
    private PillarGenerator pillarGenerator;
    public void Start()
    {
        pillarGenerator = GameObject.FindGameObjectWithTag("PillarManager").GetComponent<PillarGenerator>();
        string fileContent = "";
        allData = null;
        using (FileStream fs = File.OpenRead(fileDirectory))
        {
            byte[] b = new byte[1024];
            UTF8Encoding temp = new UTF8Encoding(true);
            int readLen;
            while ((readLen = fs.Read(b, 0, b.Length)) > 0)
            {
                fileContent += temp.GetString(b, 0, readLen);
            }
            allData = JsonConvert.DeserializeObject<Root>(fileContent);
            pillarGenerator.PlacePilars(allData);
        }
    }

    public void Awake()
    {
        fileDirectory = Path.Combine(Application.dataPath, "Feie/Data", "altData.json"); //Directory
    }
}
