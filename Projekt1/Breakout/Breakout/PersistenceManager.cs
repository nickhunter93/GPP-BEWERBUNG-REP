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

        public List<RectangleObject> LoadLevel(string levelName)
        {
            document.Load("LevelData.xml");
            XmlNode level = document.SelectSingleNode("//level/" + levelName);
            if (level == null)
            {
                throw new FileNotFoundException();
            }
            else
            {
                List<RectangleObject> bricks = new List<RectangleObject>();
                foreach (XmlNode node in level.ChildNodes)
                {
                    Vector2D size = new Vector2D(Double.Parse(node.Attributes.GetNamedItem("SizeX").Value) , Double.Parse(node.Attributes.GetNamedItem("SizeY").Value));
                    Vector2D position = new Vector2D(Double.Parse(node.Attributes.GetNamedItem("PositionX").Value), Double.Parse(node.Attributes.GetNamedItem("PositionY").Value));
                    RectangleObject brick = new RectangleObject(size, int.Parse(node.Attributes.GetNamedItem("Life").Value));
                    brick.Rectangle.Position = position;
                    brick.Life = int.Parse(node.Attributes.GetNamedItem("Life").Value);
                    brick.ID = int.Parse(node.Attributes.GetNamedItem("ID").Value);
                    //brick.Rectangle.FillColor = Color.White;

                    bricks.Add(brick);
                }
                return bricks;
            }
        }

        public void SaveLevel(string levelName, List<RectangleObject> bricks)
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
        }
    }
}