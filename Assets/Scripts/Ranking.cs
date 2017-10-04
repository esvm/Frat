using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class Ranking
    {
        [XmlAttribute("name")]
        public string name;
        [XmlAttribute("score")]
        public int score;

        public static List<Ranking> ranking = new List<Ranking>();

        public Ranking()
        {

        }

        public Ranking(string name, int score)
        {
            this.name = name;
            this.score = score;

        }
        public void Serialize()
        {
            var serializer = new XmlSerializer(typeof(List<Ranking>));
            var stream = new FileStream(Path.Combine(Application.persistentDataPath, "ranking.xml"), FileMode.Create);
            serializer.Serialize(stream, ranking);
            stream.Close();
        }

        public void Deserialize()
        {
            var deserializer = new XmlSerializer(typeof(List<Ranking>));
            if (File.Exists(Path.Combine(Application.persistentDataPath, "ranking.xml")))
            {
                var stream = new FileStream(Path.Combine(Application.persistentDataPath, "ranking.xml"), FileMode.Open);
                ranking = deserializer.Deserialize(stream) as List<Ranking>;
                stream.Close();
                ranking = ranking.OrderByDescending(p => p.score).ToList();
            }
            else
            {
                ranking = new List<Ranking>();
                ranking.Add(new Ranking("padrão", 10));
            }
        }
    }



}
