using System.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using SFML.Graphics;

namespace ConsoleApp2
{
    public class PersistenceManager
    {
        private XmlDocument document;
        public PersistenceManager()
        {
            document = new XmlDocument();
        }

        public void LoadData()
        {
            document.Load("TestFile.xml");
            XmlNode test = document.SelectSingleNode("//users");
            foreach (XmlNode node in test.ChildNodes)
            {
                Console.Out.WriteLine(node.InnerText);
            }
        }

        public void SaveData()
        {

        }

        public XmlNode CreateLevelNode(string name)
        {
            XmlNode levelNode = document.CreateElement(name);
            return levelNode;
        }

        public XmlNode BrickToXml(string name, Vector2D position, Vector2D size, int life, int id, bool state)
        {
            XmlNode brick = document.CreateElement(name);
            brick.Attributes.Append(CreateAttribute("SizeX",size.X));
            brick.Attributes.Append(CreateAttribute("SizeY",size.Y));
            brick.Attributes.Append(CreateAttribute("PositionX",position.X));
            brick.Attributes.Append(CreateAttribute("PositionY",position.Y));
            brick.Attributes.Append(CreateAttribute("Life", life));
            brick.Attributes.Append(CreateAttribute("ID", id));
            brick.Attributes.Append(CreateAttribute("State", state));

            return brick;
        }

        public XmlAttribute CreateAttribute(string name, double value)
        {
            XmlAttribute attribute = document.CreateAttribute(name);
            attribute.Value = "" + value;
            return attribute;
        }

        public XmlAttribute CreateAttribute(string name, bool value)
        {
            XmlAttribute attribute = document.CreateAttribute(name);
            if (value)
            {
                attribute.Value = "t";
            }
            else
            {
                attribute.Value = "f";
            }
            return attribute;
        }

        public List<string> LoadLevelList()
        {
            document.Load("LevelData.xml");
            List<string> list = new List<string>();
            XmlNode level = document.LastChild;
            foreach (XmlNode node in level.ChildNodes)
            {
                list.Add(node.Name);
            }
            return list;
        }
        
        public void LoadLevel(string levelName)
        {
            document.Load("Level/" + levelName + ".tmx");
            XmlNode data = document.SelectSingleNode("//data");
            if (data == null)
            {
                throw new FileNotFoundException();
            }
            else
            {
                DataManager dataManager = DataManager.GetInstance();
                TileFactory tileFactory = new TileFactory();
                string levelData = data.InnerText;
                int count = 0;
                for (int i = 0; i < 1024; i++)
                {
                    for (int j = 0; j < 1024; j++)
                    {
                        string singleValue = "";

                        while(count < levelData.Length && levelData[count] != ',')
                        {
                            singleValue += levelData[count];
                            count++;
                        }
                        count++;
                        
                        int singleValueInt = int.Parse(singleValue);


                        TileFactory.TileType tileType = TileFactory.TileType.nothing;

                        if (singleValueInt > 0 && singleValueInt < 17)
                        {
                            tileType = TileFactory.TileType.wall;
                        }
                        
                        dataManager.TileManager.AddTile(tileFactory.CreateTile(tileType, singleValueInt), new Vector2D(j, i));
                    }
                }
            }
        }

        /*public void SaveLevel(string levelName, List<RectangleObject> bricks)
        {
            document.Load("LevelData.xml");
            XmlNode level = document.SelectSingleNode("//level/" + levelName );
            if(level == null)
            {
                level = document.CreateElement(levelName);
            }
            level.RemoveAll();
            document.DocumentElement.AppendChild(level);
            int brickID = 0;
            foreach (RectangleObject brick in bricks)
            {
                level.AppendChild(BrickToXml("brick",brick.Rectangle.Position,brick.Rectangle.Size,brick.Life,brickID++,true));
            }

            document.Save("LevelData.xml");
        }*/

        /*public void SaveSettings(bool fullscreen, int volume, int maxLifes, Vector2D windowSize, PongAndBreakoutAi.Difficulty up, PongAndBreakoutAi.Difficulty down)
        {
            document = new XmlDocument();
            XmlNode root = document.CreateElement("Settings");
            XmlAttribute attribute = CreateAttribute("Fullscreen", fullscreen);
            root.Attributes.Append(attribute);
            attribute = CreateAttribute("Volume", volume);
            root.Attributes.Append(attribute);
            attribute = CreateAttribute("MaxLifes", maxLifes);
            root.Attributes.Append(attribute);
            attribute = CreateAttribute("WindowSizeX", windowSize.X);
            root.Attributes.Append(attribute);
            attribute = CreateAttribute("WindowSizeY", windowSize.Y);
            root.Attributes.Append(attribute);
            attribute = CreateAttribute("DifficultyUp", (int)up);
            root.Attributes.Append(attribute);
            attribute = CreateAttribute("DifficultyDown", (int)down);
            root.Attributes.Append(attribute);
            document.AppendChild(root);
            document.Save("Settings.xml");
        }*/

        /*public Settings LoadSettings()
        {
            document.Load("Settings.xml");
            XmlNode root = document.LastChild;

            bool fullscreen = LoadBool(root.Attributes.GetNamedItem("Fullscreen").Value);
            int volume = int.Parse(root.Attributes.GetNamedItem("Volume").Value);
            int maxLifes = int.Parse(root.Attributes.GetNamedItem("MaxLifes").Value);
            Vector2D windowSize = new Vector2D(double.Parse(root.Attributes.GetNamedItem("WindowSizeX").Value), double.Parse(root.Attributes.GetNamedItem("WindowSizeY").Value));
            PongAndBreakoutAi.Difficulty left = (PongAndBreakoutAi.Difficulty)int.Parse(root.Attributes.GetNamedItem("DifficultyUp").Value);
            PongAndBreakoutAi.Difficulty right = (PongAndBreakoutAi.Difficulty)int.Parse(root.Attributes.GetNamedItem("DifficultyDown").Value);

            return new Settings(fullscreen, volume, maxLifes, windowSize, left, right);
            
        }*/

        public bool LoadBool(string value)
        {
            if (value == "t")
            {
                return true;
            }
            return false;
        }
    }
}