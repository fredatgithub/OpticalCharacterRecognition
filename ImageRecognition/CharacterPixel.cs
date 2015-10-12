using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ImageRecognition
{
  class CharacterPixel
  {
    private const string dataSource = "PixelFormat.xml";

    private readonly XDocument XmlDoc;

    private readonly string FullDbPath = string.Empty;

    internal List<CharacterInfo> m_characterInfo = new List<CharacterInfo>();

    internal CharacterPixel()
    {
      this.FullDbPath = Path.GetFullPath("PixelFormat.xml");
      this.XmlDoc = XDocument.Load(this.FullDbPath);
      this.GenerateChracterPixel();
    }

    private static bool[,] ChangeOrderVerticalLeftToRight(bool[,] pixInfo)
    {
      bool[,] flagArray;
      if (pixInfo != null)
      {
        byte length = (byte)pixInfo.GetLength(0);
        byte num = (byte)pixInfo.GetLength(1);
        bool[,] flagArray1 = new bool[num, length];
        for (short i = 0; i < length; i = (short)(i + 1))
        {
          for (short j = 0; j < num; j = (short)(j + 1))
          {
            short num1 = (short)(length - i - 1);
            flagArray1[j, i] = pixInfo[num1, j];
          }
        }
        flagArray = flagArray1;
      }
      else
      {
        flagArray = null;
      }
      return flagArray;
    }

    internal List<Dimension> DistinctWidthHeight()
    {
      List<Dimension> dimensions = new List<Dimension>();
      foreach (CharacterInfo mCharacterInfo in this.m_characterInfo)
      {
        Dimension dimension = new Dimension()
        {
          m_height = mCharacterInfo.Height,
          m_width = mCharacterInfo.Width
        };
        if (!dimensions.Contains(dimension))
        {
          Dimension dimension1 = new Dimension()
          {
            m_height = mCharacterInfo.Height,
            m_width = mCharacterInfo.Width
          };
          dimensions.Add(dimension1);
        }
      }
      return dimensions;
    }

    private void GenerateChracterPixel()
    {
      Regex regex = new Regex("(?<xCor>(\\d)+)[,](?<yCor>(\\d)+)");
      string[] strArrays = new string[] { "TypeOne", "TypeTwo" };
      for (int i = 0; i < (int)strArrays.Length; i++)
      {
        string str = strArrays[i];
        string str1 = str;
        var collection =
            from subitem in this.XmlDoc.Descendants(str).Elements<XElement>("CharacterInfo")
            let xElement = subitem.Element("ParamValue")
            where xElement != null
            let element = subitem.Element("PixelInfo")
            where element != null
            select new { Character = char.Parse(xElement.Value), PixelInfo = element.Value, CharacterDirection = Direction.Horizontal, FontWeight = (str1 == "TypeTwo" ? FontWeight.Bold : FontWeight.Normal), Pixel = regex.Matches(element.Value) };
        foreach (var variable in collection)
        {
          List<PixelPoint> list = (
              from Match oMatch in variable.Pixel
              let xCor = short.Parse(oMatch.Groups["xCor"].Value)
              let yCor = short.Parse(oMatch.Groups["yCor"].Value)
              select new PixelPoint(xCor, yCor)).ToList<PixelPoint>();
          CharacterInfo characterInfo = new CharacterInfo();
          if (list.Count > 0)
          {
            list.Sort((PixelPoint object1, PixelPoint object2) => object1.XCor.CompareTo(object2.XCor));
            PixelPoint item = list[list.Count - 1];
            short num = item.XCor;
            item = list[0];
            byte num1 = (byte)(num - item.XCor + 1);
            list.Sort((PixelPoint object1, PixelPoint object2) => object1.YCor.CompareTo(object2.YCor));
            item = list[list.Count - 1];
            short num2 = item.YCor;
            item = list[0];
            byte num3 = (byte)(num2 - item.YCor + 1);
            bool[,] flagArray = new bool[num1, num3];
            foreach (PixelPoint pixelPoint in list)
            {
              flagArray[pixelPoint.XCor, pixelPoint.YCor] = true;
            }
            characterInfo.Character = variable.Character;
            characterInfo.m_fontWeight = variable.FontWeight;
            characterInfo.CharacterDirection = variable.CharacterDirection;
            characterInfo.m_pixelCoordinate = flagArray;
            characterInfo.Width = num1;
            characterInfo.Height = num3;
            this.m_characterInfo.Add(characterInfo);
            CharacterInfo characterInfo1 = new CharacterInfo()
            {
              Character = variable.Character,
              m_fontWeight = variable.FontWeight,
              CharacterDirection = Direction.Vertical,
              m_pixelCoordinate = CharacterPixel.ChangeOrderVerticalLeftToRight(flagArray),
              Width = num3,
              Height = num1
            };
            characterInfo = characterInfo1;
            this.m_characterInfo.Add(characterInfo);
          }
        }
      }
    }
  }
}