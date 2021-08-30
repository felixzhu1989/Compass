using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Common
{

    public class SerializeObjectToString
    {
        /// <summary>
        /// 将Object对象（已被标记为可序列化）序列化为二进制字符串
        /// </summary>
        public string SerializeObject(object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            string result = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                byte[] byt = new byte[stream.Length];
                byt = stream.ToArray();
                result = Convert.ToBase64String(byt);
                stream.Flush();
            }
            return result;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public object DeserializeObject(string str)
        {
            IFormatter formatter = new BinaryFormatter();
            byte[] byt = Convert.FromBase64String(str);
            object obj = null;
            using (Stream stream = new MemoryStream(byt, 0, byt.Length))
            {
                obj = formatter.Deserialize(stream);
            }
            return obj;
        }
    }
}
