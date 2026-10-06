using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LibraryData
{
    public class BackupHelper
    {
        public void converttojson(List<Book> list, string path)
        {
            StreamWriter writer=new StreamWriter(path);
            string jsonstring = JsonSerializer.Serialize(list);
            writer.Write(jsonstring);
            writer.Close();
            Console.WriteLine("Json Created SuccessFully");
        }
        public List<Book> Readfromjson(string path)
        {
            StreamReader reader=new StreamReader(path);
            string jsonstring=reader.ReadToEnd();
            reader.Close();
            List<Book> list = JsonSerializer.Deserialize<List<Book>>(jsonstring);
            return list;
        }
    }
}
