using System.Collections.Generic;

namespace ImageRecognition
{
  internal class MatchCharacter
  {
    public MatchCharacter()
    {
    }

    public List<CharacterInfo> GetMatchCharacter(List<ConnectedPixel> connectingPixels)
    {
      CharacterPixel characterPixel = new CharacterPixel();
      List<CharacterInfo> characterInfos = new List<CharacterInfo>();
      List<ConnectedPixel> connectedPixels = connectingPixels.FindAll((ConnectedPixel x) => (x.m_width > 18 ? false : x.m_height <= 18));
      foreach (CharacterInfo mCharacterInfo in characterPixel.m_characterInfo)
      {
        foreach (ConnectedPixel connectedPixel in connectedPixels.FindAll((ConnectedPixel x) => (x.m_width != mCharacterInfo.Width || x.m_height != mCharacterInfo.Height ? false : this.MatchPixel(x, mCharacterInfo))))
        {
          CharacterInfo characterInfo = new CharacterInfo()
          {
            m_color = connectedPixel.m_color,
            m_bgColor = connectedPixel.m_bgColor,
            m_fontWeight = mCharacterInfo.m_fontWeight,
            TopYCor = connectedPixel.m_topYCor,
            BottomYCor = connectedPixel.m_bottomYCor,
            LeftXCor = connectedPixel.m_leftXCor,
            RightXCor = connectedPixel.m_rightXCor,
            Width = (byte)connectedPixel.m_width,
            Height = (byte)connectedPixel.m_height,
            CharacterDirection = mCharacterInfo.CharacterDirection,
            Character = mCharacterInfo.Character
          };
          characterInfos.Add(characterInfo);
          connectingPixels.Remove(connectedPixel);
        }
      }
      return characterInfos;
    }

    public List<CharacterInfo> MatchJoinedCharacter(List<ConnectedPixel> connectingPixels)
    {
      List<CharacterInfo> characterInfos = new List<CharacterInfo>();
      CharacterPixel characterPixel = new CharacterPixel();
      foreach (ConnectedPixel connectedPixel in connectingPixels.FindAll((ConnectedPixel x) => (x.m_width <= 6 || x.m_height <= 3 || x.m_width > 25 ? false : x.m_height <= 25)))
      {
        short mLeftXCor = connectedPixel.m_leftXCor;
        short num = connectedPixel.m_leftXCor;
        short num1 = 4;
        while (mLeftXCor <= connectedPixel.m_rightXCor)
        {
          mLeftXCor = (short)(num + num1);
          if (mLeftXCor <= connectedPixel.m_rightXCor + 1)
          {
            List<PixelPoint> pixelPoints = new List<PixelPoint>();
            short num2 = 0;
            short num3 = 0;
            for (int i = 0; i < connectedPixel.m_height; i++)
            {
              bool mPixelCoordinate = true;
              short num4 = 0;
              for (int j = num; j < mLeftXCor; j++)
              {
                mPixelCoordinate = mPixelCoordinate & !connectedPixel.m_pixelCoordinate[j - connectedPixel.m_leftXCor, i];
                if (connectedPixel.m_pixelCoordinate[j - connectedPixel.m_leftXCor, i])
                {
                  pixelPoints.Add(new PixelPoint(num4, num2));
                }
                num4 = (short)(num4 + 1);
              }
              if (mPixelCoordinate)
              {
                num3 = (short)(num3 + 1);
              }
              else
              {
                num2 = (short)(num2 + 1);
              }
            }
            pixelPoints.Sort((PixelPoint object1, PixelPoint object2) => object1.XCor.CompareTo(object2.XCor));
            PixelPoint item = pixelPoints[pixelPoints.Count - 1];
            short xCor = item.XCor;
            item = pixelPoints[0];
            short xCor1 = (short)(xCor - item.XCor + 1);
            pixelPoints.Sort((PixelPoint object1, PixelPoint object2) => object1.YCor.CompareTo(object2.YCor));
            item = pixelPoints[pixelPoints.Count - 1];
            short yCor = item.YCor;
            item = pixelPoints[0];
            short yCor1 = (short)(yCor - item.YCor + 1);
            bool[,] flagArray = new bool[xCor1, yCor1];
            foreach (PixelPoint pixelPoint in pixelPoints)
            {
              flagArray[pixelPoint.XCor, pixelPoint.YCor] = true;
            }
            ConnectedPixel connectedPixel1 = new ConnectedPixel()
            {
              m_width = xCor1,
              m_height = yCor1,
              m_pixelCoordinate = flagArray,
              m_color = connectedPixel.m_color
            };
            ConnectedPixel connectedPixel2 = connectedPixel1;
            int num5 = characterPixel.m_characterInfo.FindIndex((CharacterInfo x) => this.MatchPixel(connectedPixel2, x));
            if (num5 == -1)
            {
              num1 = (short)(num1 + 1);
            }
            else
            {
              CharacterInfo characterInfo = new CharacterInfo()
              {
                m_color = connectedPixel2.m_color,
                m_bgColor = connectedPixel.m_bgColor,
                Character = characterPixel.m_characterInfo[num5].Character,
                TopYCor = (short)(connectedPixel.m_topYCor + num3),
                LeftXCor = num,
                BottomYCor = (short)(connectedPixel.m_topYCor + num3 + characterPixel.m_characterInfo[num5].Height - 1),
                RightXCor = (short)(mLeftXCor - 1),
                CharacterDirection = characterPixel.m_characterInfo[num5].CharacterDirection,
                Width = characterPixel.m_characterInfo[num5].Width,
                Height = characterPixel.m_characterInfo[num5].Height
              };
              characterInfos.Add(characterInfo);
              num = mLeftXCor;
              num1 = 4;
            }
            if (num1 > 5)
            {
              break;
            }
          }
          else
          {
            break;
          }
        }
      }
      return characterInfos;
    }

    internal bool MatchOverlayCharacter(ConnectedPixel connectingPixels, CharacterInfo characterInfo, List<short> diffMarkerXIndex)
    {
      bool flag;
      if ((characterInfo.Width == 0 || connectingPixels.m_width == 0 || characterInfo.Height != connectingPixels.m_height ? false : characterInfo.Width == connectingPixels.m_width))
      {
        short num = 0;
        for (int i = 0; i < characterInfo.Width; i++)
        {
          short num1 = 0;
          int num2 = 0;
          while (num2 < characterInfo.Height)
          {
            if ((connectingPixels.m_pixelCoordinate[i, num2] ? true : !characterInfo.m_pixelCoordinate[num, num1]))
            {
              if (diffMarkerXIndex.FindIndex((short s) => s == i) != -1)
              {
                num1 = (short)(num1 + 1);
              }
              else if (connectingPixels.m_pixelCoordinate[i, num2] == characterInfo.m_pixelCoordinate[num, num1])
              {
                num1 = (short)(num1 + 1);
              }
              else
              {
                flag = false;
                return flag;
              }
              num2++;
            }
            else
            {
              flag = false;
              return flag;
            }
          }
          num = (short)(num + 1);
        }
        flag = true;
      }
      else
      {
        flag = false;
      }
      return flag;
    }

    internal bool MatchPixel(ConnectedPixel connectedElement, CharacterInfo alphabetInfo)
    {
      bool flag;
      if (!(connectedElement.m_pixelCoordinate == null ? false : alphabetInfo.m_pixelCoordinate != null))
      {
        flag = false;
      }
      else if ((alphabetInfo.Width != connectedElement.m_width ? false : alphabetInfo.Height == connectedElement.m_height))
      {
        for (short i = 0; i < alphabetInfo.Width; i = (short)(i + 1))
        {
          short num = 0;
          while (num < alphabetInfo.Height)
          {
            if (alphabetInfo.m_pixelCoordinate[i, num] == connectedElement.m_pixelCoordinate[i, num])
            {
              num = (short)(num + 1);
            }
            else
            {
              flag = false;
              return flag;
            }
          }
        }
        flag = true;
      }
      else
      {
        flag = false;
      }
      return flag;
    }
  }
}