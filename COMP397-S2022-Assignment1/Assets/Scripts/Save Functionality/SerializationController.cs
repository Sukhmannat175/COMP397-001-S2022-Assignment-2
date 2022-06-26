using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SerializationController
{
    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/xml"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/xml");
        }

        string path = Application.persistentDataPath + "/saves/" + saveName + ".save";
        string pathXml = Application.persistentDataPath + "/xml/" + saveName + ".xml";

        FileStream file = File.Create(path);

        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));

        using (FileStream stream = new FileStream(pathXml, FileMode.Create))
        {
            serializer.Serialize(stream, saveData);
        }

        formatter.Serialize(file, saveData);
        file.Close();

        return true;
    }

    public static object Load(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();   

        FileStream file = File.Open(path, FileMode.Open); 

        try
        {
            object data = formatter.Deserialize(file);
            file.Close();
            return data;
        }
        catch
        {
            Debug.LogError("Error");
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        SurrogateSelector selector = new SurrogateSelector();

        Vector3Surrogate vector3Surrogate = new Vector3Surrogate();
        QuaternionSurrogate quaternionSurrogate = new QuaternionSurrogate();

        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
        selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);
        formatter.SurrogateSelector = selector;

        return formatter;
    }
}
