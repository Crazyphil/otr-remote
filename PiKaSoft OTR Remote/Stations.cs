using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Crazysoft.OTRRemote
{
    class Stations : IEnumerable
    {
        private ArrayList _stations = new ArrayList();

        public void Add(string name, string replacement)
        {
            if (Contains(name) == -1)
            {
                Station newStation = new Station();
                newStation.Replacements.Add(replacement);
                _stations.Add(newStation);
            }
        }

        public void Add(Station station)
        {
            if (Contains(station.Name) == -1)
            {
                _stations.Add(station);
            }
        }

        public void Add(string name)
        {
            if (Contains(name) == -1)
            {
                Station newStation = new Station();
                _stations.Add(newStation);
            }
        }

        public void Add(string name, string[] replacements)
        {
            if (Contains(name) == -1)
            {
                Station newStation = new Station();
                newStation.Replacements.AddRange(replacements);
                _stations.Add(newStation);
            }
        }

        public int Contains(string name)
        {
            int i = 0;
            foreach (Station station in _stations)
            {
                if (station.Name == name)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        public void Clear()
        {
            _stations.Clear();
        }

        public Station Find(string replacement)
        {
            foreach (Station station in _stations)
            {
                if (Program.Settings.Sections["Stations"].Keys["StationsCaseSensitive"] == null || Convert.ToBoolean(Program.Settings.Sections["Stations"].Keys["StationsCaseSensitive"].Value))
                {
                    if (station.Replacements.Contains(replacement))
                    {
                        return station;
                    }
                }
                else
                {
                    foreach (string rep in station.Replacements)
                    {
                        if (replacement.ToLower() == rep.ToLower())
                        {
                            return station;
                        }
                    }
                }
            }
            return null;
        }

        public bool Load()
        {
            try
            {
                if (File.Exists(Path.Combine(Application.StartupPath, "Stations.xml")))
                {
                    // Load document
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Path.Combine(Application.StartupPath, "Stations.xml"));

                    // Check, if the XML file is a stations file
                    if (doc.DocumentElement.Name != "stations")
                    {
                        return false;
                    }

                    XmlElement root = doc.DocumentElement;

                    // Loop thorugh every station element
                    foreach (XmlNode station in root.GetElementsByTagName("station"))
                    {
                        if (station.Attributes["name"] == null)
                        {
                            continue;
                        }
                        Station newStation = new Station(station.Attributes["name"].InnerText);
                        foreach (XmlNode replacement in station.ChildNodes)
                        {
                            newStation.Replacements.Add(replacement.InnerText);
                        }

                        Add(newStation);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Save()
        {
#if !DEBUG
            try
            {
#endif
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                XmlWriter writer = XmlWriter.Create(Path.Combine(Application.StartupPath, "stations.xml"), settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("stations");
                foreach (Station station in _stations)
                {
                    writer.WriteStartElement("station");
                    writer.WriteAttributeString("name", station.Name);
                    foreach (string replacement in station.Replacements)
                    {
                        writer.WriteElementString("alt", replacement);
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();

                return true;
#if !DEBUG
            }
            catch
            {
                return false;
            }
#endif
        }

        public IEnumerator GetEnumerator()
        {
            return _stations.GetEnumerator(0, _stations.Count);
        }

        public Station this[string name]
        {
            get
            {
                if (Contains(name) > -1)
                {
                    return (Station)_stations[Contains(name)];
                }
                else
                {
                    return null;
                }
            }
        }
	
        public Station this[int index]
        {
            get
            {
                return (Station)_stations[index];
            }
        }
    }

    class Station
    {
        public Station() {}

        public Station(string name)
        {
            Name = name;
        }

        public Station(string name, string replacement)
        {
            Name = name;
            Replacements.Add(replacement);
        }

        public Station(string name, string[] replacements)
        {
            Name = name;
            Replacements.AddRange(replacements);
        }

        public new string ToString()
        {
            return _Name;
        }

        public class ReplacementCollection : IEnumerable
        {
            private ArrayList _replacements = new ArrayList();

            public void Add(string replacement)
            {
                _replacements.Add(replacement);
            }
            
            public void AddRange(string[] replacements)
            {
                _replacements.AddRange(replacements);
            }

            public void Clear()
            {
                _replacements.Clear();
            }

            public bool Contains(string replacement)
            {
                return _replacements.Contains(replacement);
            }

            public void Remove(string replacement)
            {
                _replacements.Remove(replacement);
            }

            public IEnumerator GetEnumerator()
            {
                return _replacements.GetEnumerator();
            }

            public string this[int index]
            {
                get
                {
                    return _replacements[index].ToString();
                }
            }
        }

        private ReplacementCollection _replacements = new ReplacementCollection();
        public ReplacementCollection Replacements
        {
            get {
                return _replacements;
            }
        }

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
    }
}
