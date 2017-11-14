using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Platforme.util
{
    public class GenericsSerializer
    {
        //unosi listu objekata T u xml fajl imena filename 
        public static void Serialize<T>(string filename, List<T> objToSerialize) where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                using (var sw = new StreamWriter($@"../../Data/{filename}"))
                {
                    serializer.Serialize(sw, objToSerialize);
                }
            }
            catch (Exception e)
            {
                e.StackTrace.ToString();
            }
        }
        public static List<T> DeSerialize<T>(string filename) where T : class
            //vraca listu podataka iz xml fajla
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<T>));

                using (var sw = new StreamReader($@"../../Data/{filename}"))
                {
                    return (List<T>)serializer.Deserialize(sw);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
