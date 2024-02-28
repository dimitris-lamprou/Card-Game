using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveExp
{
    public static void StoreExp(int exp)
    {
        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/Hero.bin";
        FileStream stream = new(path, FileMode.Create);

        formatter.Serialize(stream, exp);
        stream.Close();
    }
}
