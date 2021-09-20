// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.RegistroAFDRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using System.Text;
using TopData.Framework.Comunicacao;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class RegistroAFDRepPlus
  {
    internal static List<RegistroAFD> SepararBufferRegistrosAFD(
      byte[] byteArrayAux,
      int ultimoNSRREP,
      RepAFD _repAFD)
    {
      int sourceIndex = 0;
      List<RegistroAFD> registroAfdList = new List<RegistroAFD>();
      byte[] arrayEmpresa = new byte[281];
      byte[] arrayEmpregado = new byte[76];
      byte[] arrayAjusteRTC = new byte[22];
      byte[] arrayMarcacao = new byte[16];
      byte[] arrayEventosSensiveis = new byte[12];
      int ultimoNSRProcessado = _repAFD.ultimoNSR;
      while (sourceIndex < byteArrayAux.Length && ultimoNSRREP > ultimoNSRProcessado)
      {
        if (byteArrayAux[sourceIndex] == (byte) 163)
        {
          if (sourceIndex + 16 <= byteArrayAux.Length)
          {
            Array.Copy((Array) byteArrayAux, sourceIndex, (Array) arrayMarcacao, 0, 16);
            registroAfdList.Add(RegistroAFDRepPlus.TratarRegistroMarcacao(arrayMarcacao));
            ultimoNSRProcessado = registroAfdList[registroAfdList.Count - 1].NSR;
            _repAFD.AtualizaPosicao(16);
            _repAFD.AtualizaUltimoNSR(ultimoNSRProcessado);
            sourceIndex += 16;
          }
          else
            break;
        }
        else if (byteArrayAux[sourceIndex] == (byte) 165)
        {
          if (sourceIndex + 69 + 7 <= byteArrayAux.Length)
          {
            Array.Copy((Array) byteArrayAux, sourceIndex, (Array) arrayEmpregado, 0, 76);
            registroAfdList.Add(RegistroAFDRepPlus.TratarRegistroEmpregado(arrayEmpregado));
            ultimoNSRProcessado = registroAfdList[registroAfdList.Count - 1].NSR;
            _repAFD.AtualizaPosicao(69);
            _repAFD.AtualizaUltimoNSR(ultimoNSRProcessado);
            sourceIndex += 69;
          }
          else
            break;
        }
        else if (byteArrayAux[sourceIndex] == (byte) 162)
        {
          if (sourceIndex + 274 + 7 <= byteArrayAux.Length)
          {
            Array.Copy((Array) byteArrayAux, sourceIndex, (Array) arrayEmpresa, 0, 281);
            registroAfdList.Add(RegistroAFDRepPlus.TratarRegistroEmpresa(arrayEmpresa));
            ultimoNSRProcessado = registroAfdList[registroAfdList.Count - 1].NSR;
            _repAFD.AtualizaPosicao(274);
            _repAFD.AtualizaUltimoNSR(ultimoNSRProcessado);
            sourceIndex += 274;
          }
          else
            break;
        }
        else if (byteArrayAux[sourceIndex] == (byte) 164)
        {
          if (sourceIndex + 15 + 7 <= byteArrayAux.Length)
          {
            Array.Copy((Array) byteArrayAux, sourceIndex, (Array) arrayAjusteRTC, 0, 22);
            registroAfdList.Add(RegistroAFDRepPlus.TratarRegistroAjusteRTC(arrayAjusteRTC));
            ultimoNSRProcessado = registroAfdList[registroAfdList.Count - 1].NSR;
            _repAFD.AtualizaPosicao(15);
            _repAFD.AtualizaUltimoNSR(ultimoNSRProcessado);
            sourceIndex += 15;
          }
          else
            break;
        }
        else if (byteArrayAux[sourceIndex] == (byte) 166)
        {
          if (sourceIndex + 12 <= byteArrayAux.Length)
          {
            Array.Copy((Array) byteArrayAux, sourceIndex, (Array) arrayEventosSensiveis, 0, 12);
            registroAfdList.Add(RegistroAFDRepPlus.TratarRegistroEventosSensiveis(arrayEventosSensiveis));
            ultimoNSRProcessado = registroAfdList[registroAfdList.Count - 1].NSR;
            _repAFD.AtualizaPosicao(12);
            _repAFD.AtualizaUltimoNSR(ultimoNSRProcessado);
            sourceIndex += 12;
          }
          else
            break;
        }
        else
        {
          ++sourceIndex;
          _repAFD.AtualizaPosicao(1);
        }
      }
      return registroAfdList;
    }

    internal static RegistroAFD TratarRegistroEmpresa(byte[] arrayEmpresa)
    {
      RegistroAFD registroAfd = new RegistroAFD();
      byte[] numArray1 = new byte[4];
      byte[] numArray2 = new byte[5];
      byte[] numArray3 = new byte[7];
      byte[] numArray4 = new byte[6];
      byte[] bytes1 = new byte[150];
      byte[] bytes2 = new byte[100];
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      StringBuilder stringBuilder3 = new StringBuilder();
      StringBuilder stringBuilder4 = new StringBuilder();
      StringBuilder stringBuilder5 = new StringBuilder();
      Array.Copy((Array) arrayEmpresa, 1, (Array) numArray1, 0, 4);
      for (int index = 0; index < 4; ++index)
        stringBuilder1.Append(numArray1[index].ToString("X").PadLeft(2, '0'));
      registroAfd.NSR = Convert.ToInt32(stringBuilder1.ToString());
      Array.Copy((Array) arrayEmpresa, 5, (Array) numArray2, 0, 5);
      stringBuilder2.Append(numArray2[0].ToString("X").PadLeft(2, '0'));
      stringBuilder2.Append(numArray2[1].ToString("X").PadLeft(2, '0'));
      stringBuilder2.Append("20");
      stringBuilder2.Append(numArray2[2].ToString("X").PadLeft(2, '0'));
      stringBuilder2.Append(numArray2[3].ToString("X").PadLeft(2, '0'));
      stringBuilder2.Append(numArray2[4].ToString("X").PadLeft(2, '0'));
      Array.Reverse((Array) numArray2, 0, 3);
      for (int index = 0; index < numArray2.Length; ++index)
        registroAfd.dtRegistro += numArray2[index].ToString("X").PadLeft(2, '0');
      Array.Copy((Array) arrayEmpresa, 11, (Array) numArray3, 0, 7);
      Array.Copy((Array) arrayEmpresa, 18, (Array) numArray4, 0, 6);
      Array.Copy((Array) arrayEmpresa, 174, (Array) bytes2, 0, 100);
      Array.Copy((Array) arrayEmpresa, 24, (Array) bytes1, 0, 150);
      byte num1 = arrayEmpresa[10];
      for (int index = 0; index < bytes1.Length; ++index)
      {
        if (bytes1[index] == (byte) 176)
          bytes1[index] = (byte) 32;
      }
      for (int index = 0; index < bytes2.Length; ++index)
      {
        if (bytes2[index] == (byte) 176)
          bytes2[index] = (byte) 32;
      }
      string str1 = Encoding.Default.GetString(bytes1);
      string str2 = Encoding.Default.GetString(bytes2);
      for (int index = 0; index < 7; ++index)
        stringBuilder5.Append(numArray3[index].ToString("X").PadLeft(2, '0'));
      for (int index = 0; index < 6; ++index)
        stringBuilder4.Append(numArray4[index].ToString("X").PadLeft(2, '0'));
      registroAfd.tipoRegistro = 2;
      stringBuilder3.Append(registroAfd.NSR.ToString().PadLeft(9, '0'));
      stringBuilder3.Append(registroAfd.tipoRegistro.ToString());
      stringBuilder3.Append((object) stringBuilder2);
      stringBuilder3.Append(Convert.ToChar(num1).ToString());
      stringBuilder3.Append(stringBuilder5.ToString());
      stringBuilder3.Append(stringBuilder4.ToString());
      stringBuilder3.Append(str1);
      stringBuilder3.Append(str2);
      registroAfd.dadosRegistro = stringBuilder3.ToString();
      byte[] numArray5 = new byte[6];
      Array.Copy((Array) arrayEmpresa, 274L, (Array) numArray5, 0L, 6L);
      string str3 = "";
      foreach (byte num2 in numArray5)
        str3 += num2.ToString("X").PadLeft(2, '0');
      registroAfd.CPFResponsavel = str3.Substring(1, 11);
      return registroAfd;
    }

    internal static RegistroAFD TratarRegistroEmpregado(byte[] arrayEmpregado)
    {
      RegistroAFD registroAfd = new RegistroAFD();
      byte[] numArray1 = new byte[4];
      byte[] numArray2 = new byte[5];
      byte[] numArray3 = new byte[6];
      byte[] bytes = new byte[52];
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      StringBuilder stringBuilder3 = new StringBuilder();
      StringBuilder stringBuilder4 = new StringBuilder();
      Array.Copy((Array) arrayEmpregado, 1, (Array) numArray1, 0, 4);
      for (int index = 0; index < 4; ++index)
        stringBuilder1.Append(numArray1[index].ToString("X").PadLeft(2, '0'));
      registroAfd.NSR = Convert.ToInt32(stringBuilder1.ToString());
      Array.Copy((Array) arrayEmpregado, 5, (Array) numArray2, 0, 5);
      stringBuilder2.Append(numArray2[0].ToString("X").PadLeft(2, '0'));
      stringBuilder2.Append(numArray2[1].ToString("X").PadLeft(2, '0'));
      stringBuilder2.Append("20");
      stringBuilder2.Append(numArray2[2].ToString("X").PadLeft(2, '0'));
      stringBuilder2.Append(numArray2[3].ToString("X").PadLeft(2, '0'));
      stringBuilder2.Append(numArray2[4].ToString("X").PadLeft(2, '0'));
      Array.Reverse((Array) numArray2, 0, 3);
      for (int index = 0; index < numArray2.Length; ++index)
        registroAfd.dtRegistro += numArray2[index].ToString("X").PadLeft(2, '0');
      Array.Copy((Array) arrayEmpregado, 11, (Array) numArray3, 0, 6);
      Array.Copy((Array) arrayEmpregado, 17, (Array) bytes, 0, 52);
      byte num1 = arrayEmpregado[10];
      for (int index = 0; index < bytes.Length; ++index)
      {
        if (bytes[index] == (byte) 176)
          bytes[index] = (byte) 32;
      }
      string str1 = Encoding.Default.GetString(bytes);
      for (int index = 0; index < 6; ++index)
        stringBuilder4.Append(numArray3[index].ToString("X").PadLeft(2, '0'));
      registroAfd.tipoRegistro = 5;
      stringBuilder3.Append(registroAfd.NSR.ToString().PadLeft(9, '0'));
      stringBuilder3.Append(registroAfd.tipoRegistro);
      stringBuilder3.Append((object) stringBuilder2);
      stringBuilder3.Append(((char) num1).ToString());
      stringBuilder3.Append(stringBuilder4.ToString());
      stringBuilder3.Append(str1);
      registroAfd.dadosRegistro = stringBuilder3.ToString();
      byte[] numArray4 = new byte[6];
      Array.Copy((Array) arrayEmpregado, 70, (Array) numArray4, 0, 6);
      string str2 = "";
      foreach (byte num2 in numArray4)
        str2 += num2.ToString("X").PadLeft(2, '0');
      registroAfd.CPFResponsavel = str2.Substring(1, 11);
      return registroAfd;
    }

    internal static RegistroAFD TratarRegistroAjusteRTC(byte[] arrayAjusteRTC)
    {
      RegistroAFD registroAfd = new RegistroAFD();
      byte[] numArray1 = new byte[4];
      byte[] numArray2 = new byte[5];
      byte[] numArray3 = new byte[5];
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      StringBuilder stringBuilder3 = new StringBuilder();
      StringBuilder stringBuilder4 = new StringBuilder();
      Array.Copy((Array) arrayAjusteRTC, 1, (Array) numArray1, 0, 4);
      for (int index = 0; index < 4; ++index)
        stringBuilder4.Append(numArray1[index].ToString("X").PadLeft(2, '0'));
      registroAfd.NSR = Convert.ToInt32(stringBuilder4.ToString());
      Array.Copy((Array) arrayAjusteRTC, 5, (Array) numArray2, 0, 5);
      stringBuilder1.Append(numArray2[0].ToString("X").PadLeft(2, '0'));
      stringBuilder1.Append(numArray2[1].ToString("X").PadLeft(2, '0'));
      stringBuilder1.Append("20");
      stringBuilder1.Append(numArray2[2].ToString("X").PadLeft(2, '0'));
      stringBuilder1.Append(numArray2[3].ToString("X").PadLeft(2, '0'));
      stringBuilder1.Append(numArray2[4].ToString("X").PadLeft(2, '0'));
      Array.Reverse((Array) numArray2, 0, 3);
      for (int index = 0; index < numArray2.Length; ++index)
        registroAfd.dtRegistro += numArray2[index].ToString("X").PadLeft(2, '0');
      Array.Copy((Array) arrayAjusteRTC, 10, (Array) numArray3, 0, 5);
      stringBuilder2.Append(numArray3[0].ToString("X").PadLeft(2, '0'));
      stringBuilder2.Append(numArray3[1].ToString("X").PadLeft(2, '0'));
      stringBuilder2.Append("20");
      stringBuilder2.Append(numArray3[2].ToString("X").PadLeft(2, '0'));
      stringBuilder2.Append(numArray3[3].ToString("X").PadLeft(2, '0'));
      stringBuilder2.Append(numArray3[4].ToString("X").PadLeft(2, '0'));
      registroAfd.tipoRegistro = 4;
      stringBuilder3.Append(registroAfd.NSR.ToString().PadLeft(9, '0'));
      stringBuilder3.Append(registroAfd.tipoRegistro);
      stringBuilder3.Append((object) stringBuilder1);
      stringBuilder3.Append((object) stringBuilder2);
      registroAfd.dadosRegistro = stringBuilder3.ToString();
      byte[] numArray4 = new byte[6];
      Array.Copy((Array) arrayAjusteRTC, 16, (Array) numArray4, 0, 6);
      string str = "";
      foreach (byte num in numArray4)
        str += num.ToString("X").PadLeft(2, '0');
      registroAfd.CPFResponsavel = str.Substring(1, 11);
      return registroAfd;
    }

    internal static RegistroAFD TratarRegistroMarcacao(byte[] arrayMarcacao)
    {
      RegistroAFD registroAfd = new RegistroAFD();
      byte[] numArray1 = new byte[4];
      byte[] numArray2 = new byte[5];
      byte[] numArray3 = new byte[6];
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      StringBuilder stringBuilder3 = new StringBuilder();
      StringBuilder stringBuilder4 = new StringBuilder();
      Array.Copy((Array) arrayMarcacao, 1, (Array) numArray1, 0, 4);
      for (int index = 0; index < 4; ++index)
        stringBuilder4.Append(numArray1[index].ToString("X").PadLeft(2, '0'));
      registroAfd.NSR = Convert.ToInt32(stringBuilder4.ToString());
      Array.Copy((Array) arrayMarcacao, 5, (Array) numArray2, 0, 5);
      stringBuilder1.Append(numArray2[0].ToString("X").PadLeft(2, '0'));
      stringBuilder1.Append(numArray2[1].ToString("X").PadLeft(2, '0'));
      stringBuilder1.Append("20");
      stringBuilder1.Append(numArray2[2].ToString("X").PadLeft(2, '0'));
      stringBuilder1.Append(numArray2[3].ToString("X").PadLeft(2, '0'));
      stringBuilder1.Append(numArray2[4].ToString("X").PadLeft(2, '0'));
      Array.Reverse((Array) numArray2, 0, 3);
      for (int index = 0; index < numArray2.Length; ++index)
        registroAfd.dtRegistro += numArray2[index].ToString("X").PadLeft(2, '0');
      Array.Copy((Array) arrayMarcacao, 10, (Array) numArray3, 0, 6);
      for (int index = 0; index < 6; ++index)
        stringBuilder2.Append(numArray3[index].ToString("X").PadLeft(2, '0'));
      registroAfd.tipoRegistro = 3;
      stringBuilder3.Append(registroAfd.NSR.ToString().PadLeft(9, '0'));
      stringBuilder3.Append(registroAfd.tipoRegistro);
      stringBuilder3.Append((object) stringBuilder1);
      stringBuilder3.Append((object) stringBuilder2);
      registroAfd.dadosRegistro = stringBuilder3.ToString();
      return registroAfd;
    }

    internal static RegistroAFD TratarRegistroEventosSensiveis(
      byte[] arrayEventosSensiveis)
    {
      RegistroAFD registroAfd = new RegistroAFD();
      byte[] numArray1 = new byte[4];
      byte[] numArray2 = new byte[5];
      byte[] numArray3 = new byte[1];
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      StringBuilder stringBuilder3 = new StringBuilder();
      StringBuilder stringBuilder4 = new StringBuilder();
      Array.Copy((Array) arrayEventosSensiveis, 1, (Array) numArray1, 0, 4);
      for (int index = 0; index < 4; ++index)
        stringBuilder4.Append(numArray1[index].ToString("X").PadLeft(2, '0'));
      registroAfd.NSR = Convert.ToInt32(stringBuilder4.ToString());
      Array.Copy((Array) arrayEventosSensiveis, 5, (Array) numArray2, 0, 5);
      stringBuilder1.Append(numArray2[0].ToString("X").PadLeft(2, '0'));
      stringBuilder1.Append(numArray2[1].ToString("X").PadLeft(2, '0'));
      stringBuilder1.Append("20");
      stringBuilder1.Append(numArray2[2].ToString("X").PadLeft(2, '0'));
      stringBuilder1.Append(numArray2[3].ToString("X").PadLeft(2, '0'));
      stringBuilder1.Append(numArray2[4].ToString("X").PadLeft(2, '0'));
      Array.Reverse((Array) numArray2, 0, 3);
      for (int index = 0; index < numArray2.Length; ++index)
        registroAfd.dtRegistro += numArray2[index].ToString("X").PadLeft(2, '0');
      Array.Copy((Array) arrayEventosSensiveis, 10, (Array) numArray3, 0, 1);
      for (int index = 0; index < 1; ++index)
        stringBuilder2.Append(numArray3[index].ToString("X").PadLeft(2, '0'));
      registroAfd.tipoRegistro = 6;
      stringBuilder3.Append(registroAfd.NSR.ToString().PadLeft(9, '0'));
      stringBuilder3.Append(registroAfd.tipoRegistro);
      stringBuilder3.Append((object) stringBuilder1);
      stringBuilder3.Append((object) stringBuilder2);
      registroAfd.dadosRegistro = stringBuilder3.ToString();
      return registroAfd;
    }

    public static int GravarRegistrosAFD(List<RegistroAFD> listaRegistrosAFD, RepAFD _repAFD)
    {
      new RegistroAFD().GravarRegistrosAFD(listaRegistrosAFD, _repAFD);
      return 0;
    }

    public static int GravarRegistrosAFDComTransacao(
      List<RegistroAFD> listaRegistrosAFD,
      RepAFD _repAFD)
    {
      int num = 0;
      try
      {
        return new RegistroAFD().GravarRegistrosAFDComTransacao(listaRegistrosAFD, _repAFD);
      }
      catch (Exception ex)
      {
        num = -1;
        throw ex;
      }
    }

    public static List<RegistroAFD> PesquisarTodosOsRegistrosAFD(
      RepAFD _repAFD,
      int UltNSR)
    {
      return new RegistroAFD().PesquisarTodosOsRegistrosAFD(_repAFD, UltNSR);
    }

    public static RegistroAFD PesquisarRegistroMaiorDataAFD(RepAFD _repAFD) => new RegistroAFD().PesquisarRegistroMaiorDataAFD(_repAFD);

    public static RegistroAFD PesquisarRegistroMaiorDataAFD(
      RepAFD _repAFD,
      ParametrosExportacaoAFD paramExport)
    {
      return new RegistroAFD().PesquisarRegistroMaiorDataAFD(_repAFD, paramExport);
    }

    public static RegistroAFD PesquisarRegistroMenorDataAFD(RepAFD _repAFD) => new RegistroAFD().PesquisarRegistroMenorDataAFD(_repAFD);

    public static RegistroAFD PesquisarRegistroMenorDataAFD(
      RepAFD _repAFD,
      ParametrosExportacaoAFD paramExport)
    {
      return new RegistroAFD().PesquisarRegistroMenorDataAFD(_repAFD, paramExport);
    }

    public static RegistroAFD PesquisarUltimoRegistroEmpresa(RepAFD _repAFD) => new RegistroAFD().PesquisarUltimoRegistroEmpresa(_repAFD);

    internal static REPAFD.BlocoMemoria BlocoLidoContemNsrSolicitado(
      byte[] byteArrayAux,
      int nsrSolicitado,
      out int posicaoUltimoNsrBloco,
      out int ultimoNsrDoBloco)
    {
      byte[] numArray = new byte[4];
      int num1 = 0;
      int num2 = 0;
      bool flag1 = false;
      bool flag2 = true;
      StringBuilder stringBuilder1 = new StringBuilder();
      ultimoNsrDoBloco = 0;
      posicaoUltimoNsrBloco = 0;
      int index1 = 0;
      bool flag3 = false;
label_28:
      while (index1 < byteArrayAux.Length && !flag3)
      {
        if (byteArrayAux[index1] != (byte) 0)
          flag2 = false;
        if (byteArrayAux[index1] == (byte) 163 || byteArrayAux[index1] == (byte) 165 || byteArrayAux[index1] == (byte) 162 || byteArrayAux[index1] == (byte) 164 || byteArrayAux[index1] == (byte) 166)
        {
          flag3 = true;
          Array.Copy((Array) byteArrayAux, index1 + 1, (Array) numArray, 0, 4);
          StringBuilder stringBuilder2 = new StringBuilder();
          for (int index2 = 0; index2 < 4; ++index2)
            stringBuilder2.Append(numArray[index2].ToString("X").PadLeft(2, '0'));
          num1 = Convert.ToInt32(stringBuilder2.ToString());
          while (true)
          {
            if (index1 < byteArrayAux.Length && !flag1)
            {
              switch (byteArrayAux[index1])
              {
                case 162:
                  num2 = 281;
                  if (index1 + num2 <= byteArrayAux.Length)
                  {
                    posicaoUltimoNsrBloco += num2;
                    break;
                  }
                  break;
                case 163:
                  num2 = 16;
                  if (index1 + 16 <= byteArrayAux.Length)
                  {
                    posicaoUltimoNsrBloco += 16;
                    break;
                  }
                  break;
                case 164:
                  num2 = 22;
                  if (index1 + num2 <= byteArrayAux.Length)
                  {
                    posicaoUltimoNsrBloco += num2;
                    break;
                  }
                  break;
                case 165:
                  num2 = 76;
                  if (index1 + num2 <= byteArrayAux.Length)
                  {
                    posicaoUltimoNsrBloco += num2;
                    break;
                  }
                  break;
                case 166:
                  num2 = 12;
                  if (index1 + 12 <= byteArrayAux.Length)
                  {
                    posicaoUltimoNsrBloco += 12;
                    break;
                  }
                  break;
                default:
                  flag1 = true;
                  break;
              }
              if (index1 + num2 <= byteArrayAux.Length && !flag1)
              {
                Array.Copy((Array) byteArrayAux, index1 + 1, (Array) numArray, 0, 4);
                StringBuilder stringBuilder3 = new StringBuilder();
                for (int index3 = 0; index3 < 4; ++index3)
                  stringBuilder3.Append(numArray[index3].ToString("X").PadLeft(2, '0'));
                ultimoNsrDoBloco = Convert.ToInt32(stringBuilder3.ToString());
                index1 += num2;
              }
              else
                flag1 = true;
            }
            else
              goto label_28;
          }
        }
        else
          ++index1;
      }
      if (flag2)
        return REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MAIOR;
      if (!flag3)
        return REPAFD.BlocoMemoria.SEM_REGISTROS;
      if (num1 > nsrSolicitado)
        return REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MENOR;
      return ultimoNsrDoBloco < nsrSolicitado ? REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MAIOR : REPAFD.BlocoMemoria.CONTEM_NSR;
    }

    internal static REPAFD.BlocoMemoria BlocoLidoContemNsrSolicitadoSenior(
      byte[] byteArrayAux,
      int nsrSolicitado,
      out int posicaoUltimoNsrBloco,
      out int ultimoNsrDoBloco)
    {
      int num1 = 0;
      byte[] numArray = new byte[4];
      int num2 = 0;
      int num3 = 0;
      bool flag1 = false;
      bool flag2 = true;
      StringBuilder stringBuilder1 = new StringBuilder();
      ultimoNsrDoBloco = 0;
      posicaoUltimoNsrBloco = 0;
      int index1 = 0;
      bool flag3 = false;
label_30:
      while (index1 < byteArrayAux.Length && !flag3)
      {
        if (byteArrayAux[index1] != (byte) 0)
          flag2 = false;
        if (byteArrayAux[index1] == (byte) 163 || byteArrayAux[index1] == (byte) 165 || byteArrayAux[index1] == (byte) 162 || byteArrayAux[index1] == (byte) 164 || byteArrayAux[index1] == (byte) 166)
        {
          flag3 = true;
          Array.Copy((Array) byteArrayAux, index1 + 1, (Array) numArray, 0, 4);
          StringBuilder stringBuilder2 = new StringBuilder();
          for (int index2 = 0; index2 < 4; ++index2)
            stringBuilder2.Append(numArray[index2].ToString("X").PadLeft(2, '0'));
          num2 = Convert.ToInt32(stringBuilder2.ToString());
          while (true)
          {
            if (index1 < byteArrayAux.Length && !flag1)
            {
              switch (byteArrayAux[index1])
              {
                case 162:
                  num3 = 281;
                  if (index1 + num3 <= byteArrayAux.Length)
                  {
                    posicaoUltimoNsrBloco += num3;
                    break;
                  }
                  break;
                case 163:
                  num3 = 16;
                  if (index1 + 16 <= byteArrayAux.Length)
                  {
                    posicaoUltimoNsrBloco += 16;
                    break;
                  }
                  break;
                case 164:
                  num3 = 22;
                  if (index1 + num3 <= byteArrayAux.Length)
                  {
                    posicaoUltimoNsrBloco += num3;
                    break;
                  }
                  break;
                case 165:
                  num3 = 76;
                  if (index1 + num3 <= byteArrayAux.Length)
                  {
                    posicaoUltimoNsrBloco += num3;
                    break;
                  }
                  break;
                case 166:
                  num3 = 12;
                  if (index1 + 12 <= byteArrayAux.Length)
                  {
                    posicaoUltimoNsrBloco += 12;
                    break;
                  }
                  break;
                default:
                  flag1 = true;
                  break;
              }
              if (index1 + num3 <= byteArrayAux.Length && !flag1)
              {
                Array.Copy((Array) byteArrayAux, index1 + 1, (Array) numArray, 0, 4);
                StringBuilder stringBuilder3 = new StringBuilder();
                for (int index3 = 0; index3 < 4; ++index3)
                  stringBuilder3.Append(numArray[index3].ToString("X").PadLeft(2, '0'));
                ultimoNsrDoBloco = Convert.ToInt32(stringBuilder3.ToString());
                if (ultimoNsrDoBloco != nsrSolicitado)
                  index1 += num3;
                else
                  break;
              }
              else
                flag1 = true;
            }
            else
              goto label_30;
          }
          index1 += num3;
          num1 = index1;
        }
        else
          ++index1;
      }
      if (flag2)
        return REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MAIOR;
      if (!flag3)
        return REPAFD.BlocoMemoria.SEM_REGISTROS;
      if (num2 > nsrSolicitado)
        return REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MENOR;
      if (ultimoNsrDoBloco < nsrSolicitado)
        return REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MAIOR;
      if (ultimoNsrDoBloco != nsrSolicitado || num1 <= 0)
        return REPAFD.BlocoMemoria.CONTEM_NSR;
      posicaoUltimoNsrBloco = num1;
      return REPAFD.BlocoMemoria.CONTEM_NSR;
    }

    internal static REPAFD.BlocoMemoria BlocoLidoContemNsrSolicitadoSeniorAntigo(
      byte[] byteArrayAux,
      int nsrSolicitado,
      out int posicaoUltimoNsrBloco,
      out int ultimoNsrDoBloco)
    {
      int num1 = 0;
      byte[] numArray = new byte[4];
      int num2 = 0;
      int num3 = 0;
      bool flag1 = false;
      bool flag2 = true;
      StringBuilder stringBuilder1 = new StringBuilder();
      ultimoNsrDoBloco = 0;
      posicaoUltimoNsrBloco = 0;
      int index1 = 0;
      bool flag3 = false;
label_25:
      while (index1 < byteArrayAux.Length && !flag3)
      {
        if (byteArrayAux[index1] != (byte) 0)
          flag2 = false;
        if (byteArrayAux[index1] == (byte) 163 || byteArrayAux[index1] == (byte) 165 || byteArrayAux[index1] == (byte) 162 || byteArrayAux[index1] == (byte) 164 || byteArrayAux[index1] == (byte) 166)
        {
          flag3 = true;
          Array.Copy((Array) byteArrayAux, index1 + 1, (Array) numArray, 0, 4);
          StringBuilder stringBuilder2 = new StringBuilder();
          for (int index2 = 0; index2 < 4; ++index2)
            stringBuilder2.Append(numArray[index2].ToString("X").PadLeft(2, '0'));
          num2 = Convert.ToInt32(stringBuilder2.ToString());
          while (true)
          {
            if (index1 < byteArrayAux.Length && !flag1)
            {
              switch (byteArrayAux[index1])
              {
                case 162:
                  num3 = 280;
                  break;
                case 163:
                  num3 = 16;
                  break;
                case 164:
                  num3 = 22;
                  break;
                case 165:
                  num3 = 76;
                  break;
                case 166:
                  num3 = 12;
                  break;
                default:
                  flag1 = true;
                  break;
              }
              if (index1 + num3 <= byteArrayAux.Length && !flag1)
              {
                posicaoUltimoNsrBloco = index1;
                Array.Copy((Array) byteArrayAux, index1 + 1, (Array) numArray, 0, 4);
                StringBuilder stringBuilder3 = new StringBuilder();
                for (int index3 = 0; index3 < 4; ++index3)
                  stringBuilder3.Append(numArray[index3].ToString("X").PadLeft(2, '0'));
                ultimoNsrDoBloco = Convert.ToInt32(stringBuilder3.ToString());
                if (ultimoNsrDoBloco != nsrSolicitado)
                  index1 += num3;
                else
                  break;
              }
              else
                flag1 = true;
            }
            else
              goto label_25;
          }
          index1 += num3;
          num1 = index1;
        }
        else
          ++index1;
      }
      if (flag2)
        return REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MAIOR;
      if (!flag3)
        return REPAFD.BlocoMemoria.SEM_REGISTROS;
      if (num2 > nsrSolicitado)
        return REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MENOR;
      if (ultimoNsrDoBloco < nsrSolicitado)
        return REPAFD.BlocoMemoria.SEM_NSR_BUSCAR_MAIOR;
      if (ultimoNsrDoBloco != nsrSolicitado || num1 <= 0)
        return REPAFD.BlocoMemoria.CONTEM_NSR;
      posicaoUltimoNsrBloco = num1;
      return REPAFD.BlocoMemoria.CONTEM_NSR;
    }

    internal static RegistroAFD LeRegistroDoNsrSolicitado(
      byte[] byteArrayAux,
      int nsrSolicitado)
    {
      int sourceIndex = 0;
      RegistroAFD registroAfd = new RegistroAFD();
      byte[] arrayEmpresa = new byte[281];
      byte[] arrayEmpregado = new byte[76];
      byte[] arrayAjusteRTC = new byte[22];
      byte[] arrayMarcacao = new byte[16];
      byte[] arrayEventosSensiveis = new byte[12];
      byte[] numArray = new byte[4];
      StringBuilder stringBuilder1 = new StringBuilder();
      while (sourceIndex < byteArrayAux.Length)
      {
        try
        {
          if (byteArrayAux[sourceIndex] == (byte) 163)
          {
            Array.Copy((Array) byteArrayAux, sourceIndex + 1, (Array) numArray, 0, 4);
            StringBuilder stringBuilder2 = new StringBuilder();
            for (int index = 0; index < 4; ++index)
              stringBuilder2.Append(numArray[index].ToString("X").PadLeft(2, '0'));
            if (Convert.ToInt32(stringBuilder2.ToString()) == nsrSolicitado)
            {
              Array.Copy((Array) byteArrayAux, sourceIndex, (Array) arrayMarcacao, 0, 16);
              registroAfd = RegistroAFDRepPlus.TratarRegistroMarcacao(arrayMarcacao);
              break;
            }
            sourceIndex += 16;
          }
          else if (byteArrayAux[sourceIndex] == (byte) 165)
          {
            Array.Copy((Array) byteArrayAux, sourceIndex + 1, (Array) numArray, 0, 4);
            StringBuilder stringBuilder3 = new StringBuilder();
            for (int index = 0; index < 4; ++index)
              stringBuilder3.Append(numArray[index].ToString("X").PadLeft(2, '0'));
            if (Convert.ToInt32(stringBuilder3.ToString()) == nsrSolicitado)
            {
              Array.Copy((Array) byteArrayAux, sourceIndex, (Array) arrayEmpregado, 0, 69);
              registroAfd = RegistroAFDRepPlus.TratarRegistroEmpregado(arrayEmpregado);
              break;
            }
            sourceIndex += 69;
          }
          else if (byteArrayAux[sourceIndex] == (byte) 162)
          {
            Array.Copy((Array) byteArrayAux, sourceIndex + 1, (Array) numArray, 0, 4);
            StringBuilder stringBuilder4 = new StringBuilder();
            for (int index = 0; index < 4; ++index)
              stringBuilder4.Append(numArray[index].ToString("X").PadLeft(2, '0'));
            if (Convert.ToInt32(stringBuilder4.ToString()) == nsrSolicitado)
            {
              Array.Copy((Array) byteArrayAux, sourceIndex, (Array) arrayEmpresa, 0, 274);
              registroAfd = RegistroAFDRepPlus.TratarRegistroEmpresa(arrayEmpresa);
              break;
            }
            sourceIndex += 274;
          }
          else if (byteArrayAux[sourceIndex] == (byte) 164)
          {
            Array.Copy((Array) byteArrayAux, sourceIndex + 1, (Array) numArray, 0, 4);
            StringBuilder stringBuilder5 = new StringBuilder();
            for (int index = 0; index < 4; ++index)
              stringBuilder5.Append(numArray[index].ToString("X").PadLeft(2, '0'));
            if (Convert.ToInt32(stringBuilder5.ToString()) == nsrSolicitado)
            {
              Array.Copy((Array) byteArrayAux, sourceIndex, (Array) arrayAjusteRTC, 0, 15);
              registroAfd = RegistroAFDRepPlus.TratarRegistroAjusteRTC(arrayAjusteRTC);
              break;
            }
            sourceIndex += 15;
          }
          else if (byteArrayAux[sourceIndex] == (byte) 166)
          {
            Array.Copy((Array) byteArrayAux, sourceIndex + 1, (Array) numArray, 0, 4);
            StringBuilder stringBuilder6 = new StringBuilder();
            for (int index = 0; index < 4; ++index)
              stringBuilder6.Append(numArray[index].ToString("X").PadLeft(2, '0'));
            if (Convert.ToInt32(stringBuilder6.ToString()) == nsrSolicitado)
            {
              Array.Copy((Array) byteArrayAux, sourceIndex, (Array) arrayEventosSensiveis, 0, 12);
              registroAfd = RegistroAFDRepPlus.TratarRegistroEventosSensiveis(arrayEventosSensiveis);
              break;
            }
            sourceIndex += 12;
          }
          else
            ++sourceIndex;
        }
        catch
        {
          ++sourceIndex;
        }
      }
      return registroAfd;
    }
  }
}
