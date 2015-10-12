namespace ImageRecognition
{
  internal class ConnectedPixel
  {
    internal bool[,] m_pixelCoordinate;

    internal PxColor m_color;

    internal PxColor m_bgColor;

    internal short m_width;

    internal short m_height;

    internal short m_leftXCor;

    internal short m_rightXCor;

    internal short m_topYCor;

    internal short m_bottomYCor;

    public ConnectedPixel() { }
  }
}