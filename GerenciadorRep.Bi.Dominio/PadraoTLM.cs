// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.PadraoTLM
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class PadraoTLM
  {
    public static string GetCFGHex(string caminhoArquivo)
    {
      StringBuilder stringBuilder = new StringBuilder();
      byte[] numArray1 = (byte[]) null;
      byte[] numArray2;
      try
      {
        File.SetAttributes(caminhoArquivo, FileAttributes.Normal);
        BinaryReader binaryReader = new BinaryReader((Stream) File.Open(caminhoArquivo, FileMode.Open));
        FileInfo fileInfo = new FileInfo(caminhoArquivo);
        numArray1 = new byte[fileInfo.Length];
        numArray2 = binaryReader.ReadBytes((int) fileInfo.Length);
        binaryReader.Close();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      for (int index = 0; index < numArray2.Length; ++index)
        stringBuilder.Append(numArray2[index].ToString("X").PadLeft(2, '0'));
      return stringBuilder.ToString();
    }

    public static string GetNomeEmpresa(string cfg)
    {
      int[] numArray = new int[109];
      byte[] byteArray = PadraoTLM.GetByteArray(cfg);
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 1; index < 109; ++index)
        numArray[index] = (int) byteArray[index] ^ 105;
      for (int index = 1; index < 109 && numArray[index] != 0; ++index)
        stringBuilder.Append((char) numArray[index]);
      return stringBuilder.ToString();
    }

    public static byte[] GetByteArray(string cfg)
    {
      if (cfg.Length <= 0)
        return (byte[]) null;
      byte[] numArray = new byte[cfg.Length / 2];
      for (int index = 0; index < cfg.Length / 2; ++index)
        numArray[index] = (byte) int.Parse(cfg.Substring(index * 2, 2), NumberStyles.HexNumber);
      return numArray;
    }
  }
}
