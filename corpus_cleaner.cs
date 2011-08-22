using System;
using System.Collections.Generic;
using System.IO;

public class CorpusCleaner
{
  private const string WORD_LIST_FILE_NAME = "WORD_LIST.TXT";
  private const string CORPUS_FILE_NAME = "CORPUS.TXT";
  private const string CLEANED_CORPUS_OUTPUT_FILE_NAME = "CLEAN_CORPUS.TXT";

  static public void Main (string[] args)
  {
    List<string> wordListTokens = TokenizeFile(WORD_LIST_FILE_NAME);
    Console.WriteLine("Word List Token Count: " + wordListTokens.Count.ToString());

    List<string> corpusTokens = TokenizeFile(CORPUS_FILE_NAME);
    Console.WriteLine("Corpus Token Count: " + corpusTokens.Count.ToString());

    List<string> cleanedCorpusTokens = ReturnTokensContainedInWordList(corpusTokens, wordListTokens);
    Console.WriteLine("Cleaned Corpus Token Count: " + cleanedCorpusTokens.Count.ToString());

    using (StreamWriter sw = new StreamWriter(CLEANED_CORPUS_OUTPUT_FILE_NAME))
    {
      foreach (string cleanedCorpusToken in cleanedCorpusTokens)
      {
        sw.WriteLine(cleanedCorpusToken);
      }
    }
	}

  private static List<string> TokenizeFile (String FileName)
  {
    List<string> tokenList = new List<string>();

    using (StreamReader sr = new StreamReader(FileName))
    {
      String line;

      while ((line = sr.ReadLine()) != null)
      {
        string[] words = line.Split(' ');

        foreach (string word in words)
        {
          tokenList.Add(SanitizeWord(word));
        }
      }
    }
   return tokenList;
  }

  private static string SanitizeWord(String DirtyString)
  {
    char[] arr = DirtyString.ToCharArray();
    arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-')));

    return new string(arr).ToLower();
  }

  private static List<string> ReturnTokensContainedInWordList(List<string> DirtyTokenList, List<string> WordListTokens)
  {
    int counter = 0;

    List<string> cleanTokenList = new List<string>();

    foreach (string token in DirtyTokenList)
    {
      if (WordListTokens.Contains(token))
      {
        cleanTokenList.Add(token);
      }

      Console.Write("\r{0} ", ++counter);
    }

    return cleanTokenList;
  }
}

