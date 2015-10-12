using System;
using System.Collections.Generic;
namespace ImageRecognition
{
  internal struct PixelPoint
  {
    private readonly short XCordinate;

    private readonly short YCordinate;

    internal short XCor
    {
      get
      {
        return this.XCordinate;
      }
    }

    internal short YCor
    {
      get
      {
        return this.YCordinate;
      }
    }

    internal PixelPoint(short tempxCor, short tempyCor)
    {
      this.XCordinate = tempxCor;
      this.YCordinate = tempyCor;
    }
  }
}