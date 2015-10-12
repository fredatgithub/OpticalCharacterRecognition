using System.Collections.Generic;

namespace ImageRecognition
{
  public sealed class WordInfo
  {
    public short BottomYCor
    {
      get;
      set;
    }

    public List<CharacterInfo> CharacterInfo
    {
      get;
      set;
    }

    public PxColor Color
    {
      get;
      set;
    }

    public Direction Direction
    {
      get;
      set;
    }

    public FontWeight FontWeight
    {
      get;
      set;
    }

    public short LeftXCor
    {
      get;
      set;
    }

    public short RightXCor
    {
      get;
      set;
    }

    public short TopYCor
    {
      get;
      set;
    }

    public string Word
    {
      get;
      set;
    }

    public WordInfo()
    {
    }
  }
}