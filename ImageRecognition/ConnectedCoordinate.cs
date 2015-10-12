using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImageRecognition
{
  class ConnectedCoordinate
  {
    public ConnectedCoordinate()
    {
    }

    private static void CheckSpecialSeperatedDotCharacter(ConnectedPixel connectElement, List<ConnectedPixel> lstConnectedElement, CharacterInfo ibold, CharacterInfo iUnbold, KeyValuePair<short, List<PixelPoint>> currentConnectedpixel)
    {
      bool flag;
      if (connectElement.m_width != 2 || connectElement.m_height != 6)
      {
        flag = (connectElement.m_width != 1 ? true : connectElement.m_height != 6);
      }
      else
      {
        flag = false;
      }
      if (flag)
      {
        List<PixelPoint>.Enumerator enumerator = currentConnectedpixel.Value.GetEnumerator();
        while (enumerator.MoveNext())
        {
          PixelPoint current = enumerator.Current;
          connectElement.m_pixelCoordinate[current.XCor - connectElement.m_leftXCor, current.YCor - connectElement.m_topYCor] = true;
        }
      }
      else
      {
        int num = (connectElement.m_width == 2 ? 2 : 3);
        int num1 = lstConnectedElement.FindIndex((ConnectedPixel x) => (x.m_bottomYCor != connectElement.m_topYCor - num || x.m_leftXCor != connectElement.m_leftXCor || x.m_color.R != connectElement.m_color.R || x.m_color.G != connectElement.m_color.G ? false : x.m_color.B == connectElement.m_color.B));
        if (num1 > -1)
        {
          connectElement.m_height = (short)(connectElement.m_height + num);
          connectElement.m_topYCor = lstConnectedElement[num1].m_topYCor;
          connectElement.m_pixelCoordinate = (connectElement.m_width == 2 ? ibold.m_pixelCoordinate : iUnbold.m_pixelCoordinate);
          lstConnectedElement.RemoveAt(num1);
        }
      }
    }

    private static void GetbackgroundColor(ConnectedPixel connectElement, ref byte[] rgbValues, int nWidth, int nOffset)
    {
      List<PixelPoint> pixelPoints = new List<PixelPoint>()
            {
                new PixelPoint((short)(connectElement.m_leftXCor - 1), connectElement.m_topYCor),
                new PixelPoint((short)(connectElement.m_leftXCor + 1), connectElement.m_topYCor),
                new PixelPoint((short)(connectElement.m_rightXCor - 1), connectElement.m_bottomYCor),
                new PixelPoint((short)(connectElement.m_rightXCor + 1), connectElement.m_bottomYCor)
            };
      List<PixelPoint> pixelPoints1 = pixelPoints;
      short r = (short)(connectElement.m_color.R + connectElement.m_color.G + connectElement.m_color.B);
      List<PixelPoint>.Enumerator enumerator = pixelPoints1.GetEnumerator();
      int num = 0;
      while (enumerator.MoveNext())
      {
        PixelPoint current = enumerator.Current;
        int yCor = current.YCor * nWidth + current.XCor * 3 + current.YCor * nOffset;
        if ((yCor < 0 ? false : yCor <= (int)rgbValues.Length - 3))
        {
          short num1 = (short)(rgbValues[yCor + 2] + rgbValues[yCor + 1] + rgbValues[yCor]);
          short num2 = (short)Math.Abs((int)(r - num1));
          if (num2 > 0)
          {
            if ((num <= 0 ? true : num < num2))
            {
              connectElement.m_bgColor.R = rgbValues[yCor + 2];
              connectElement.m_bgColor.G = rgbValues[yCor + 1];
              connectElement.m_bgColor.B = rgbValues[yCor];
              num = num1;
            }
          }
        }
      }
    }

    internal List<ConnectedPixel> GetConnectedMarkPixel(ConnectedPixel markConnected, short minCount)
    {
      List<ConnectedPixel> connectedPixels = new List<ConnectedPixel>();
      List<int> nums = new List<int>();
      int mHeight = (markConnected.m_height - 1) * markConnected.m_width + (markConnected.m_width - 1);
      bool[] flagArray = new bool[mHeight + 1];
      for (short i = 0; i < markConnected.m_width; i = (short)(i + 1))
      {
        for (short j = 0; j < markConnected.m_height; j = (short)(j + 1))
        {
          if (markConnected.m_pixelCoordinate[i, j])
          {
            int mWidth = j * markConnected.m_width + i;
            nums.Add(mWidth);
            flagArray[mWidth] = true;
          }
        }
      }
      List<int> nums1 = new List<int>();
      int num = 0;
      while (num != nums.Count)
      {
        if (!nums1.Contains(nums[num]))
        {
          List<int> nums2 = new List<int>()
                    {
                        nums[num]
                    };
          List<PixelPoint> pixelPoints = new List<PixelPoint>();
          for (int k = 0; k != nums2.Count; k++)
          {
            int item = nums2[k];
            int mWidth1 = item - markConnected.m_width - 1;
            int num1 = item - markConnected.m_width;
            int mWidth2 = item - markConnected.m_width + 1;
            int num2 = item - 1;
            int num3 = item + 1;
            int mWidth3 = item + markConnected.m_width - 1;
            int mWidth4 = item + markConnected.m_width;
            int num4 = item + markConnected.m_width + 1;
            ConnectedCoordinate.MatchNeighbConnected(markConnected, mWidth1, mHeight, nums2);
            ConnectedCoordinate.MatchNeighbConnected(markConnected, num1, mHeight, nums2);
            ConnectedCoordinate.MatchNeighbConnected(markConnected, mWidth2, mHeight, nums2);
            ConnectedCoordinate.MatchNeighbConnected(markConnected, num2, mHeight, nums2);
            ConnectedCoordinate.MatchNeighbConnected(markConnected, num3, mHeight, nums2);
            ConnectedCoordinate.MatchNeighbConnected(markConnected, mWidth3, mHeight, nums2);
            ConnectedCoordinate.MatchNeighbConnected(markConnected, mWidth4, mHeight, nums2);
            ConnectedCoordinate.MatchNeighbConnected(markConnected, num4, mHeight, nums2);
            nums1.Add(nums2[k]);
            short num5 = (short)Math.Floor((double)nums2[k] / (double)markConnected.m_width);
            short item1 = (short)(nums2[k] - num5 * markConnected.m_width);
            pixelPoints.Add(new PixelPoint(item1, num5));
          }
          if (nums2.Count > minCount)
          {
            pixelPoints.Sort((PixelPoint sObject1, PixelPoint sObject2) => sObject1.YCor.CompareTo(sObject2.YCor));
            int yCor = pixelPoints.FirstOrDefault<PixelPoint>().YCor;
            int yCor1 = pixelPoints.LastOrDefault<PixelPoint>().YCor;
            pixelPoints.Sort((PixelPoint sObject1, PixelPoint sObject2) => sObject1.XCor.CompareTo(sObject2.XCor));
            int xCor = pixelPoints.FirstOrDefault<PixelPoint>().XCor;
            int xCor1 = pixelPoints.LastOrDefault<PixelPoint>().XCor;
            short num6 = (short)(xCor1 - xCor + 1);
            short num7 = (short)(yCor1 - yCor + 1);
            ConnectedPixel connectedPixel = new ConnectedPixel()
            {
              m_pixelCoordinate = new bool[num6, num7],
              m_color = markConnected.m_color,
              m_bgColor = markConnected.m_bgColor,
              m_width = num6,
              m_height = num7,
              m_topYCor = (short)(markConnected.m_topYCor + yCor)
            };
            ConnectedPixel mTopYCor = connectedPixel;
            mTopYCor.m_bottomYCor = (short)(mTopYCor.m_topYCor + num7 - 1);
            mTopYCor.m_leftXCor = (short)(markConnected.m_leftXCor + xCor);
            mTopYCor.m_rightXCor = (short)(mTopYCor.m_leftXCor + num6 - 1);
            List<PixelPoint>.Enumerator enumerator = pixelPoints.GetEnumerator();
            while (enumerator.MoveNext())
            {
              PixelPoint current = enumerator.Current;
              mTopYCor.m_pixelCoordinate[current.XCor - xCor, current.YCor - yCor] = true;
            }
            connectedPixels.Add(mTopYCor);
          }
          num++;
        }
        else
        {
          num++;
        }
      }
      return connectedPixels;
    }

    internal List<ConnectedPixel> GetConnectedPixel(Bitmap bitmap, Dictionary<short, List<PixelPoint>> dicPixelPointConncected)
    {
      byte[] numArray;
      int num;
      int num1;
      ImageLabeling.GetRgbArrayFromImage(bitmap, out numArray, out num, out num1);
      List<ConnectedPixel> connectedPixels = new List<ConnectedPixel>();
      short width = (short)(bitmap.Width * 3);
      CharacterPixel characterPixel = new CharacterPixel();
      CharacterInfo characterInfo = characterPixel.m_characterInfo.Find((CharacterInfo x) => (x.CharacterDirection != Direction.Horizontal || x.Character != 'i' ? false : x.m_fontWeight == FontWeight.Bold));
      CharacterInfo characterInfo1 = characterPixel.m_characterInfo.Find((CharacterInfo x) => (x.CharacterDirection != Direction.Horizontal || x.Character != 'i' ? false : x.m_fontWeight == FontWeight.Normal));
      characterPixel.m_characterInfo.RemoveAll((CharacterInfo x) => (x == characterInfo ? false : x != characterInfo1));
      foreach (KeyValuePair<short, List<PixelPoint>> keyValuePair in dicPixelPointConncected)
      {
        if (keyValuePair.Value.Count <= 4000)
        {
          keyValuePair.Value.Sort((PixelPoint sObject1, PixelPoint sObject2) => sObject1.YCor.CompareTo(sObject2.YCor));
          PixelPoint pixelPoint = keyValuePair.Value.FirstOrDefault<PixelPoint>();
          int yCor = pixelPoint.YCor;
          pixelPoint = keyValuePair.Value.LastOrDefault<PixelPoint>();
          int yCor1 = pixelPoint.YCor;
          keyValuePair.Value.Sort((PixelPoint sObject1, PixelPoint sObject2) => sObject1.XCor.CompareTo(sObject2.XCor));
          pixelPoint = keyValuePair.Value.FirstOrDefault<PixelPoint>();
          int xCor = pixelPoint.XCor;
          pixelPoint = keyValuePair.Value.LastOrDefault<PixelPoint>();
          int xCor1 = pixelPoint.XCor;
          pixelPoint = keyValuePair.Value.First<PixelPoint>();
          int yCor2 = pixelPoint.YCor * width; // was short why?
          pixelPoint = keyValuePair.Value.First<PixelPoint>();
          int xCor2 = yCor2 + pixelPoint.XCor * 3;
          pixelPoint = keyValuePair.Value.First<PixelPoint>();
          int num2 = xCor2 + pixelPoint.YCor * num1;
          short num3 = (short)(xCor1 - xCor + 1);
          short num4 = (short)(yCor1 - yCor + 1);
          ConnectedPixel connectedPixel = new ConnectedPixel()
          {
            m_pixelCoordinate = new bool[num3, num4]
          };
          PxColor pxColor = new PxColor()
          {
            R = numArray[num2 + 2],
            G = numArray[num2 + 1],
            B = numArray[num2]
          };
          connectedPixel.m_color = pxColor;
          connectedPixel.m_height = num4;
          connectedPixel.m_width = num3;
          connectedPixel.m_topYCor = (short)yCor;
          connectedPixel.m_bottomYCor = (short)yCor1;
          connectedPixel.m_leftXCor = (short)xCor;
          connectedPixel.m_rightXCor = (short)xCor1;
          ConnectedPixel connectedPixel1 = connectedPixel;
          ConnectedCoordinate.CheckSpecialSeperatedDotCharacter(connectedPixel1, connectedPixels, characterInfo, characterInfo1, keyValuePair);
          ConnectedCoordinate.GetbackgroundColor(connectedPixel1, ref numArray, width, num1);
          connectedPixels.Add(connectedPixel1);
        }
      }
      return connectedPixels;
    }

    private static void MatchNeighbConnected(ConnectedPixel markConnected, int neighindex, int maxIndex, ICollection<int> procossedId)
    {
      if ((neighindex < 0 ? false : neighindex <= maxIndex))
      {
        short num = (short)Math.Floor((double)neighindex / (double)markConnected.m_width);
        short num1 = (short)(neighindex - num * markConnected.m_width);
        if ((!markConnected.m_pixelCoordinate[num1, num] ? false : !procossedId.Contains(neighindex)))
        {
          procossedId.Add(neighindex);
        }
      }
    }
  }
}