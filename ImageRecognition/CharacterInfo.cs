namespace ImageRecognition
{
  public sealed class CharacterInfo
  {
    internal PxColor m_color;

    internal PxColor m_bgColor;

    internal bool[,] m_pixelCoordinate;

    internal FontWeight m_fontWeight;

    public short BottomYCor{get;set;}

    public char Character{get;set;}

    public Direction CharacterDirection{get;set;}

    public byte Height{get;set;}

    public short LeftXCor{get;set;}

    public short RightXCor{get;set;}

    public short TopYCor{get;set;}

    public byte Width{get;set;}

    public CharacterInfo()
    {
    }
  }
}