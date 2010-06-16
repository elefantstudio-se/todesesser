using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace ConPlug
{
    public class ConPlug
    {
        //Variables
        private string filelocation = "";
        private bool parsed = false;
        private Hashtable settings;
        private string[] lines;
        //Constructor
        public ConPlug(string file)
        {
            this.filelocation = file;
            this.settings = new Hashtable();
        }
        //StartParse
        public void StartParse()
        {
            if (parsed == true)
            {
                throw new Exception("File Already Parsed, If you want to Refresh use Refresh().");
            }
            else
            {
                //Parse the File Set
                lines = File.ReadAllLines(this.filelocation);
                foreach (string line in lines)
                {
                    if (line.StartsWith("#") == false)
                    {
                        string[] sLine = Regex.Split(line, "\t");
                        //Index     Desc
                        //0         Name
                        //1         Value
                        settings.Add(sLine[0], sLine[1]);
                    }
                }
            }
        }

        public void Reload()
        {
            settings = new Hashtable();
            lines = new string[1];
            parsed = false;

            StartParse();
        }

        public void SetValue(string Name, string Value)
        {
            settings[Name] = Value;
        }

        public void Save()
        {
            //Compare Table with File Data.
            string[] lines = File.ReadAllLines(this.filelocation);
            Hashtable fileSettings = new Hashtable();
            foreach (string line in lines)
            {
                if (line.StartsWith("#") == false)
                {
                    string[] sLine = Regex.Split(line, "\t");
                    //Index     Desc
                    //0         Name
                    //1         Value
                    fileSettings.Add(sLine[0], sLine[1]);
                }
            }
            //Find Not-Existent or Changed Keys.
            Hashtable newData = new Hashtable();
            Hashtable changedData = new Hashtable();
            foreach (string sKey in settings.Keys)
            {
                if (fileSettings.ContainsKey(sKey) == true)
                {
                    if (fileSettings[sKey].ToString() != settings[sKey].ToString())
                    {
                        changedData.Add(sKey, settings[sKey]);
                    }
                }
                else
                {
                    newData.Add(sKey, settings[sKey]);
                }
            }
            //Save New Data
            foreach (string key in newData)
            {
                File.AppendAllText(this.filelocation, key + "\t" + newData[key]);
            }
            //Save Changed Data
            //Reload Lines
            lines = File.ReadAllLines(this.filelocation);
            //Loop Through Keys:
            foreach(string key in changedData.Keys)
            {
                //Loop through lines in File to find the line num
                // of the changed key
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("#") == false)
                    {
                        string[] sLine = Regex.Split(lines[i], "\t");
                        //Index     Desc
                        //0         Name
                        //1         Value
                        if(sLine[0] == key)
                        {
                            lines[i] = key + "\t" + changedData[key];
                        }
                    }
                }
            }
            //Write Out New Data
            File.WriteAllLines(this.filelocation, lines);
        }

        //Get Setting Value
        public string GetValue(string Name)
        {
            return settings[Name].ToString();
        }
        //Get Setting String Array
        public string[] GetStringArray(string Name)
        {
            return Regex.Split(settings[Name].ToString(), "!@!");
        }
        //Get Setting Bool Array
        public bool[] GetBoolArray(string Name)
        {
            bool[] tmp;
            string[] r = Regex.Split(settings[Name].ToString(), "!@!");
            tmp = new bool[r.Length];
            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = bool.Parse(r[i]);
            }
            return tmp;
        }
        //Get Double Dimension String Array
        public Hashtable GetDStringArray(string Name)
        {
            string[] dimensions = Regex.Split(settings[Name].ToString(), "!@!");
            Hashtable output = new Hashtable();
            for (int i = 0; i < dimensions.Length; i++)
            {
                string[] e = Regex.Split(dimensions[i], "->");

                output.Add(e[0], e[1]);
            }

            return output;
        }
        //Get Colour
        public Color GetColour(string Name)
        {
            string[] sp = Regex.Split(settings[Name].ToString(), ", ");
            Color tmpColour = Color.FromArgb(int.Parse(sp[0]), int.Parse(sp[1]), int.Parse(sp[2]));

            return tmpColour;
        }
        //GetPlug
        public static ConPlug GetPlug(string configFile)
        {
            ConPlug tmpPlug = new ConPlug(configFile);
            return tmpPlug;
        }
    }
}
