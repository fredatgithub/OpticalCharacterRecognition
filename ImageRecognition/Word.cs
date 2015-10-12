using System;
using System.Collections.Generic;

namespace ImageRecognition
{
  internal sealed class Word
  {
    public Word()
    {
    }

    internal List<WordInfo> GetHorizontalWord(List<CharacterInfo> recoginizedCharacter)
    {
      List<CharacterInfo> characterInfos = recoginizedCharacter.FindAll((CharacterInfo x) => x.CharacterDirection == Direction.Horizontal);
      characterInfos.Sort((CharacterInfo sObject1, CharacterInfo sObject2) => sObject1.LeftXCor.CompareTo(sObject2.LeftXCor));
      List<WordInfo> wordInfos = new List<WordInfo>();
      while (characterInfos.Count > 0)
      {
        List<CharacterInfo> characterInfos1 = characterInfos.FindAll((CharacterInfo x) => (x.m_color.R != characterInfos[0].m_color.R || x.m_color.G != characterInfos[0].m_color.G || x.m_color.B != characterInfos[0].m_color.B || Math.Abs((int)(characterInfos[0].m_bgColor.R + characterInfos[0].m_bgColor.G + characterInfos[0].m_bgColor.B - (x.m_bgColor.R + x.m_bgColor.G + x.m_bgColor.B))) >= 100 || x.LeftXCor < characterInfos[0].LeftXCor || x.LeftXCor - characterInfos[0].RightXCor >= 230 ? false : (x.TopYCor < characterInfos[0].TopYCor || x.TopYCor > characterInfos[0].BottomYCor ? (characterInfos[0].TopYCor < x.TopYCor ? false : characterInfos[0].TopYCor <= x.BottomYCor) : true)));
        List<CharacterInfo> characterInfos2 = new List<CharacterInfo>();
        string str = characterInfos1[0].Character.ToString();
        characterInfos2.Add(characterInfos1[0]);
        characterInfos.Remove(characterInfos1[0]);
        bool flag = (str.Contains(".") || str.Contains("-") || str.Contains("_") ? true : str.Contains("|"));
        for (short i = 1; i < characterInfos1.Count; i = (short)(i + 1))
        {
          short leftXCor = (short)(characterInfos1[i].LeftXCor - characterInfos2[characterInfos2.Count - 1].RightXCor);
          if ((leftXCor < 0 ? false : leftXCor <= 4))
          {
            str = string.Format("{0}{1}{2}", str, (leftXCor > 5 ? " " : string.Empty), characterInfos1[i].Character);
            characterInfos.Remove(characterInfos1[i]);
            characterInfos2.Add(characterInfos1[i]);
            flag = flag & (str.Contains(".") || str.Contains("-") || str.Contains("_") ? true : str.Contains("|"));
          }
        }
        if ((str.Length <= 0 ? false : !flag))
        {
          WordInfo wordInfo = new WordInfo()
          {
            CharacterInfo = new List<CharacterInfo>()
          };
          short num = characterInfos2[0].LeftXCor;
          short rightXCor = characterInfos2[0].RightXCor;
          short topYCor = characterInfos2[0].TopYCor;
          short bottomYCor = characterInfos2[0].BottomYCor;
          foreach (CharacterInfo characterInfo in characterInfos2)
          {
            CharacterInfo characterInfo1 = characterInfo;
            int num1 = recoginizedCharacter.FindIndex((CharacterInfo x) => (x.LeftXCor != characterInfo1.LeftXCor || x.TopYCor != characterInfo1.TopYCor ? false : x.CharacterDirection == Direction.Vertical));
            if (num1 > -1)
            {
              recoginizedCharacter.RemoveAt(num1);
            }
            num = Math.Min(characterInfo.LeftXCor, num);
            rightXCor = Math.Max(characterInfo.RightXCor, rightXCor);
            topYCor = Math.Min(characterInfo.TopYCor, topYCor);
            bottomYCor = Math.Max(characterInfo.BottomYCor, bottomYCor);
            wordInfo.BottomYCor = characterInfos2[0].TopYCor;
            wordInfo.Direction = Direction.Horizontal;
            wordInfo.CharacterInfo.Add(characterInfo);
          }
          wordInfo.Word = str;
          wordInfo.FontWeight = characterInfos2[0].m_fontWeight;
          wordInfo.Color = characterInfos2[0].m_color;
          wordInfo.LeftXCor = num;
          wordInfo.RightXCor = rightXCor;
          wordInfo.TopYCor = topYCor;
          wordInfo.BottomYCor = bottomYCor;
          wordInfos.Add(wordInfo);
        }
      }
      return wordInfos;
    }

    internal List<WordInfo> GetVerticalWord(List<CharacterInfo> recoginizedCharacter)
    {
      List<CharacterInfo> characterInfos = recoginizedCharacter.FindAll((CharacterInfo x) => x.CharacterDirection == Direction.Vertical);
      characterInfos.Sort((CharacterInfo sObject1, CharacterInfo sObject2) => sObject1.BottomYCor.CompareTo(sObject2.BottomYCor));
      List<WordInfo> wordInfos = new List<WordInfo>();
      while (characterInfos.Count > 0)
      {
        CharacterInfo item = characterInfos[characterInfos.Count - 1];
        List<CharacterInfo> characterInfos1 = characterInfos.FindAll((CharacterInfo x) => (x.m_color.R != item.m_color.R || x.m_color.G != item.m_color.G || x.m_color.B != item.m_color.B || x.BottomYCor > item.BottomYCor || item.TopYCor - x.BottomYCor <= 0 || item.TopYCor - x.BottomYCor >= 200 ? false : (x.LeftXCor < item.LeftXCor || x.LeftXCor > item.RightXCor ? (item.LeftXCor < x.LeftXCor ? false : item.LeftXCor <= x.RightXCor) : true)));
        if (characterInfos1.Count != 0)
        {
          List<CharacterInfo> characterInfos2 = new List<CharacterInfo>();
          string str = item.Character.ToString();
          characterInfos2.Add(item);
          characterInfos.Remove(item);
          bool flag = (str.Contains(".") ? true : str.Contains("-"));
          for (short i = (short)(characterInfos1.Count - 1); i > -1; i = (short)(i - 1))
          {
            short topYCor = (short)(characterInfos2[characterInfos2.Count - 1].TopYCor - characterInfos1[i].BottomYCor);
            if ((topYCor < 0 ? false : topYCor <= 12))
            {
              str = string.Format("{0}{1}{2}", str, (topYCor > 5 ? " " : string.Empty), characterInfos1[i].Character);
              characterInfos.Remove(characterInfos1[i]);
              characterInfos2.Add(characterInfos1[i]);
              flag = flag & (str.Contains(".") ? true : str.Contains("-"));
            }
          }
          if ((str.Length <= 0 ? false : !flag))
          {
            WordInfo wordInfo = new WordInfo()
            {
              CharacterInfo = new List<CharacterInfo>()
            };
            short leftXCor = characterInfos2[0].LeftXCor;
            short rightXCor = characterInfos2[0].RightXCor;
            short num = characterInfos2[0].TopYCor;
            short bottomYCor = characterInfos2[0].BottomYCor;
            foreach (CharacterInfo characterInfo in characterInfos2)
            {
              leftXCor = Math.Min(characterInfo.LeftXCor, leftXCor);
              rightXCor = Math.Max(characterInfo.RightXCor, rightXCor);
              num = Math.Min(characterInfo.TopYCor, num);
              bottomYCor = Math.Max(characterInfo.BottomYCor, bottomYCor);
              wordInfo.BottomYCor = characterInfos2[0].TopYCor;
              wordInfo.Direction = Direction.Vertical;
              wordInfo.CharacterInfo.Add(characterInfo);
            }
            wordInfo.Word = str;
            wordInfo.Color = characterInfos2[0].m_color;
            wordInfo.FontWeight = characterInfos2[0].m_fontWeight;
            wordInfo.LeftXCor = leftXCor;
            wordInfo.RightXCor = rightXCor;
            wordInfo.TopYCor = num;
            wordInfo.BottomYCor = bottomYCor;
            wordInfos.Add(wordInfo);
          }
        }
        else
        {
          characterInfos.Remove(item);
        }
      }
      return wordInfos;
    }

    internal List<WordInfo> GetVerticalWord(List<WordInfo> recoginizedWordInfo)
    {
      List<WordInfo> wordInfos = recoginizedWordInfo.FindAll((WordInfo x) => x.Direction == Direction.Vertical);
      wordInfos.Sort((WordInfo sObject1, WordInfo sObject2) => sObject1.LeftXCor.CompareTo(sObject2.LeftXCor));
      List<WordInfo> wordInfos1 = new List<WordInfo>();
      while (wordInfos.Count > 0)
      {
        WordInfo item = wordInfos[0];
        List<WordInfo> wordInfos2 = wordInfos.FindAll((WordInfo x) => (x.Color.R != item.Color.R || x.Color.G != item.Color.G || x.Color.B != item.Color.B || x.LeftXCor - item.RightXCor <= 0 || x.LeftXCor - item.RightXCor >= 5 ? false : (x.TopYCor < item.TopYCor || x.TopYCor > item.BottomYCor ? (item.TopYCor < x.TopYCor ? false : item.TopYCor <= x.BottomYCor) : true)));
        string word = item.Word;
        WordInfo fontWeight = item;
        fontWeight.FontWeight = item.FontWeight;
        wordInfos.Remove(item);
        for (short i = (short)(wordInfos2.Count - 1); i > -1; i = (short)(i - 1))
        {
          word = string.Format("{0} {1}", word, wordInfos2[i].Word);
          fontWeight.CharacterInfo.AddRange(wordInfos2[i].CharacterInfo);
          wordInfos.Remove(wordInfos2[i]);
        }
        fontWeight.Word = word;
        wordInfos1.Add(fontWeight);
      }
      return wordInfos1;
    }
  }
}