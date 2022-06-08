using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    private GameMaster _gm;

    void Awake()
    {
        _gm = GameObject.FindObjectOfType<GameMaster>();
    }

    public void Save()
    {
        // Binanry Formatter -- write data to file
        FileStream file = new FileStream(Application.persistentDataPath + "/game.dat", FileMode.OpenOrCreate);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(file, _gm.stats);
        file.Close();
    }

    public void Load()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/game.dat", FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        _gm.stats = (GameStats)formatter.Deserialize(file); 
        file.Close();
    }
}
