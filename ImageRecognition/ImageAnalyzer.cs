using System;
using System.Collections.Generic;
using System.Drawing;

namespace ImageRecognition
{
  public class ImageAnalyzer
  {
    public ImageAnalyzer()
    {
    }

    public List<WordInfo> HorizontalWord(Bitmap bitmap)
    {
      ConnectedCoordinate connectedCoordinate = new ConnectedCoordinate();
      ImageLabeling imageLabeling = new ImageLabeling();
      MatchCharacter matchCharacter = new MatchCharacter();
      Word word = new Word();
      DateTime now = DateTime.Now;
      Dictionary<short, List<PixelPoint>> imageLabel = imageLabeling.GetImageLabel(bitmap);
      TimeSpan timeSpan = DateTime.Now - now;
      //Console.WriteLine("execution time image labeling {0} ms", timeSpan.TotalMilliseconds);
      List<ConnectedPixel> connectedPixel = connectedCoordinate.GetConnectedPixel(bitmap, imageLabel);
      List<CharacterInfo> characterInfos = matchCharacter.GetMatchCharacter(connectedPixel);
      characterInfos.AddRange(matchCharacter.MatchJoinedCharacter(connectedPixel));
      return word.GetHorizontalWord(characterInfos);
    }
  }
}