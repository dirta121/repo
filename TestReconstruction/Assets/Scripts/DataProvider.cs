using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class DataProvider : MonoBehaviour
{
    static Data[] ReadData()
    {
        var json = ReadString();

       return JsonConvert.DeserializeObject<Data[]>(json);
    }

   public static Data[] GetData()
    {
        return ReadData();
    }

    static string ReadString()
    {
        using (StreamReader reader = new StreamReader("Assets/Resources/reconstruction_description.json"))
            return reader.ReadToEnd();
    }
}

