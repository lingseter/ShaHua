using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Utility
{
    public class SerializationHelper
    {
        public static string Serialize2Xml<T>(Type objType, T t) where T : class, new()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            XmlSerializer serializer = new XmlSerializer(objType);
            serializer.Serialize(writer, t);
            writer.Close();
            return sb.ToString();

        }

        public static T DeserializeObject<T>(Type objType, string objXml) where T : class, new()
        {
            StringReader strReader = new StringReader(objXml);
            XmlReader xmlReader = XmlReader.Create(strReader);
            XmlSerializer serializer = new XmlSerializer(objType);
            return serializer.Deserialize(xmlReader) as T;
        }

        public static string Serialize2Xml(object obj)
        {
            if (obj == null) return string.Empty;
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            xs.Serialize(stream, obj);
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        public static string Serialize2Xml(object obj, string defaultNamespaces, XmlSerializerNamespaces namespaces)
        {
            if (obj == null) return string.Empty;
            XmlSerializer xs = new XmlSerializer(obj.GetType(), defaultNamespaces);
            MemoryStream stream = new MemoryStream();
            xs.Serialize(stream, obj, namespaces);
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        public static string SerializeDataTableXml(DataTable dt)
        {
            XmlSerializer xs = new XmlSerializer(typeof(DataTable));
            MemoryStream stream = new MemoryStream();
            xs.Serialize(stream, dt);
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        public static string Serialize2Json(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T DeSerialize2Json<T>(string str) where T : class, new()
        {
            JsonSerializer js = new JsonSerializer();
            StringReader sr = new StringReader(str);
            T t = js.Deserialize<T>(new JsonTextReader(sr));
            return t;
        }

        public static List<T> DeSerialize2Json2List<T>(string str) where T : class, new()
        {
            JsonSerializer js = new JsonSerializer();
            StringReader sr = new StringReader(str);
            List<T> ts = js.Deserialize<List<T>>(new JsonTextReader(sr));
            return ts;
        }
    }
}
