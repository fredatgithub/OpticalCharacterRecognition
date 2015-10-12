using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace ImageRecognition
{
  internal class ImageLabeling
  {
    public ImageLabeling()
    {
    }

    internal Dictionary<short, List<PixelPoint>> GetImageLabel(Bitmap bitmap)
    {
      byte[] numArray;
      int num;
      int num1;
      short i;
      short j;
      short num2;
      ImageLabeling.GetRgbArrayFromImage(bitmap, out numArray, out num, out num1);
      List<PixelPoint> pixelPoints = new List<PixelPoint>()
            {
                new PixelPoint(-1, -1),
                new PixelPoint(0, -1),
                new PixelPoint(1, -1),
                new PixelPoint(-1, 0),
                new PixelPoint(1, 0),
                new PixelPoint(-1, 1),
                new PixelPoint(0, 1),
                new PixelPoint(1, 1)
            };
      List<PixelPoint> pixelPoints1 = pixelPoints;
      short width = (short)(bitmap.Width * 3);
      short[,] numArray1 = new short[bitmap.Width, bitmap.Height];
      short num3 = 1;
      Dictionary<short, List<short>> nums = new Dictionary<short, List<short>>();
      for (i = 1; i < bitmap.Height - 1; i = (short)(i + 1))
      {
        for (j = 3; j < bitmap.Width - 3; j = (short)(j + 1))
        {
          List<short> nums1 = new List<short>();
          int num4 = i * width + j * 3 + i * num1;
          byte[] numArray2 = new byte[] { numArray[num4 + 2], numArray[num4 + 1], numArray[num4] };
          byte[] numArray3 = numArray2;
          PxColor pxColor = new PxColor()
          {
            R = numArray3[0],
            G = numArray3[1],
            B = numArray3[2]
          };
          num2 = 32767;
          List<PixelPoint>.Enumerator enumerator = pixelPoints1.GetEnumerator();
          while (enumerator.MoveNext())
          {
            PixelPoint current = enumerator.Current;
            short yCor = (short)(current.YCor + i);
            short xCor = (short)(current.XCor + j);
            int num5 = yCor * width + xCor * 3 + yCor * num1;
            numArray2 = new byte[] { numArray[num5 + 2], numArray[num5 + 1], numArray[num5] };
            byte[] numArray4 = numArray2;
            if ((numArray3[0] != numArray4[0] || numArray3[1] != numArray4[1] ? false : numArray3[2] == numArray4[2]))
            {
              if ((numArray1[xCor, yCor] == 0 ? false : !nums1.Contains(numArray1[xCor, yCor])))
              {
                num2 = Math.Min(num2, numArray1[xCor, yCor]);
                nums1.Add(numArray1[xCor, yCor]);
              }
            }
          }
          if (num2 == 32767)
          {
            short num6 = num3;
            num3 = (short)(num6 + 1);
            num2 = num6;
          }
          numArray1[j, i] = num2;
          if (!nums.ContainsKey(num2))
          {
            nums.Add(num2, new List<short>());
          }
          foreach (short num7 in nums1)
          {
            if (!nums[num2].Contains(num7))
            {
              nums[num2].Add(num7);
            }
            if (!nums.ContainsKey(num7))
            {
              nums.Add(num7, new List<short>());
            }
            if (!nums[num7].Contains(num2))
            {
              nums[num7].Add(num2);
            }
          }
        }
      }
      Dictionary<short, short> nums2 = ImageLabeling.RecordEquailance(nums, num3);
      nums.Clear();
      Dictionary<short, List<PixelPoint>> nums3 = new Dictionary<short, List<PixelPoint>>();
      for (i = 0; i < bitmap.Height; i = (short)(i + 1))
      {
        for (j = 0; j < bitmap.Width; j = (short)(j + 1))
        {
          num2 = (nums2.ContainsKey(numArray1[j, i]) ? nums2[numArray1[j, i]] : numArray1[j, i]);
          if (!nums3.ContainsKey(num2))
          {
            nums3.Add(num2, new List<PixelPoint>());
          }
          nums3[num2].Add(new PixelPoint(j, i));
        }
      }
      return nums3;
    }

    internal static void GetRgbArrayFromImage(Bitmap bitmap, out byte[] rgbValues, out int bytes, out int nOffset)
    {
      BitmapData bitmapDatum = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
      IntPtr scan0 = bitmapDatum.Scan0;
      bytes = Math.Abs(bitmapDatum.Stride) * bitmap.Height;
      rgbValues = new byte[bytes];
      nOffset = bitmapDatum.Stride - bitmapDatum.Width * 3;
      Marshal.Copy(scan0, rgbValues, 0, bytes);
      bitmap.UnlockBits(bitmapDatum);
    }

    private static Dictionary<short, short> RecordEquailance(IDictionary<short, List<short>> dicUnqualLabelId, short maxLabelId)
    {
      Dictionary<short, short> nums = new Dictionary<short, short>();
      short num = 1;
      while (num <= maxLabelId)
      {
        if (!dicUnqualLabelId.ContainsKey(num))
        {
          num = (short)(num + 1);
        }
        else if ((dicUnqualLabelId[num].Count == 0 ? false : !nums.ContainsKey(num)))
        {
          List<short> nums1 = new List<short>()
                    {
                        num
                    };
          short num1 = 0;
          while (num1 != nums1.Count)
          {
            List<short> item = dicUnqualLabelId[nums1[num1]];
            if (item.Count != 0)
            {
              foreach (short num2 in
                  from addId in item
                  where !nums1.Contains(addId)
                  select addId)
              {
                nums1.Add(num2);
              }
              num1 = (short)(num1 + 1);
            }
            else
            {
              num1 = (short)(num1 + 1);
            }
          }
          nums1.Sort();
          foreach (short num3 in nums1)
          {
            nums[num3] = nums1[0];
          }
          num = (short)(num + 1);
        }
        else
        {
          num = (short)(num + 1);
        }
      }
      return nums;
    }
  }
}