using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] Hand;
    public Statistics statistics;

    public const string fileName = "stats.json";

    // Start is called before the first frame update
    void Awake()
    {
        VpMachine.Init();
        VpMachine.OnHandDealt += HandDealt;
        VpMachine.OnCardsDrawn += HandDealt;

        try
        {
            if (File.Exists(Application.persistentDataPath + "/" + fileName))
                using (var reader = new StreamReader(Application.persistentDataPath + "/" + fileName))
                {
                    var rawJson = reader.ReadToEnd();
                    VpMachine.statistics = JsonUtility.FromJson<Statistics>(rawJson);
                }
        }
        catch
        {
        }

        if (VpMachine.statistics == null)
            VpMachine.statistics = new Statistics();
    }

    // Update is called once per frame
    void HandDealt(VpMachine.DealEventArgs e)
    {
        for (var i = 0; i < 5; i++)
            Hand[i].GetComponent<Card>().SetCard(e.Hand[i]);
    }

    private void OnDestroy()
    {

        using (var writer = new StreamWriter(Application.persistentDataPath + "/" + fileName))
        {
            writer.Write(JsonUtility.ToJson(VpMachine.statistics));
        }
    }
}
