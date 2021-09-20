﻿// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.RepAFD
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using System.Text;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class RepAFD
  {
    private static ushort[] CrcTable = new ushort[256]
    {
      (ushort) 0,
      (ushort) 49345,
      (ushort) 49537,
      (ushort) 320,
      (ushort) 49921,
      (ushort) 960,
      (ushort) 640,
      (ushort) 49729,
      (ushort) 50689,
      (ushort) 1728,
      (ushort) 1920,
      (ushort) 51009,
      (ushort) 1280,
      (ushort) 50625,
      (ushort) 50305,
      (ushort) 1088,
      (ushort) 52225,
      (ushort) 3264,
      (ushort) 3456,
      (ushort) 52545,
      (ushort) 3840,
      (ushort) 53185,
      (ushort) 52865,
      (ushort) 3648,
      (ushort) 2560,
      (ushort) 51905,
      (ushort) 52097,
      (ushort) 2880,
      (ushort) 51457,
      (ushort) 2496,
      (ushort) 2176,
      (ushort) 51265,
      (ushort) 55297,
      (ushort) 6336,
      (ushort) 6528,
      (ushort) 55617,
      (ushort) 6912,
      (ushort) 56257,
      (ushort) 55937,
      (ushort) 6720,
      (ushort) 7680,
      (ushort) 57025,
      (ushort) 57217,
      (ushort) 8000,
      (ushort) 56577,
      (ushort) 7616,
      (ushort) 7296,
      (ushort) 56385,
      (ushort) 5120,
      (ushort) 54465,
      (ushort) 54657,
      (ushort) 5440,
      (ushort) 55041,
      (ushort) 6080,
      (ushort) 5760,
      (ushort) 54849,
      (ushort) 53761,
      (ushort) 4800,
      (ushort) 4992,
      (ushort) 54081,
      (ushort) 4352,
      (ushort) 53697,
      (ushort) 53377,
      (ushort) 4160,
      (ushort) 61441,
      (ushort) 12480,
      (ushort) 12672,
      (ushort) 61761,
      (ushort) 13056,
      (ushort) 62401,
      (ushort) 62081,
      (ushort) 12864,
      (ushort) 13824,
      (ushort) 63169,
      (ushort) 63361,
      (ushort) 14144,
      (ushort) 62721,
      (ushort) 13760,
      (ushort) 13440,
      (ushort) 62529,
      (ushort) 15360,
      (ushort) 64705,
      (ushort) 64897,
      (ushort) 15680,
      (ushort) 65281,
      (ushort) 16320,
      (ushort) 16000,
      (ushort) 65089,
      (ushort) 64001,
      (ushort) 15040,
      (ushort) 15232,
      (ushort) 64321,
      (ushort) 14592,
      (ushort) 63937,
      (ushort) 63617,
      (ushort) 14400,
      (ushort) 10240,
      (ushort) 59585,
      (ushort) 59777,
      (ushort) 10560,
      (ushort) 60161,
      (ushort) 11200,
      (ushort) 10880,
      (ushort) 59969,
      (ushort) 60929,
      (ushort) 11968,
      (ushort) 12160,
      (ushort) 61249,
      (ushort) 11520,
      (ushort) 60865,
      (ushort) 60545,
      (ushort) 11328,
      (ushort) 58369,
      (ushort) 9408,
      (ushort) 9600,
      (ushort) 58689,
      (ushort) 9984,
      (ushort) 59329,
      (ushort) 59009,
      (ushort) 9792,
      (ushort) 8704,
      (ushort) 58049,
      (ushort) 58241,
      (ushort) 9024,
      (ushort) 57601,
      (ushort) 8640,
      (ushort) 8320,
      (ushort) 57409,
      (ushort) 40961,
      (ushort) 24768,
      (ushort) 24960,
      (ushort) 41281,
      (ushort) 25344,
      (ushort) 41921,
      (ushort) 41601,
      (ushort) 25152,
      (ushort) 26112,
      (ushort) 42689,
      (ushort) 42881,
      (ushort) 26432,
      (ushort) 42241,
      (ushort) 26048,
      (ushort) 25728,
      (ushort) 42049,
      (ushort) 27648,
      (ushort) 44225,
      (ushort) 44417,
      (ushort) 27968,
      (ushort) 44801,
      (ushort) 28608,
      (ushort) 28288,
      (ushort) 44609,
      (ushort) 43521,
      (ushort) 27328,
      (ushort) 27520,
      (ushort) 43841,
      (ushort) 26880,
      (ushort) 43457,
      (ushort) 43137,
      (ushort) 26688,
      (ushort) 30720,
      (ushort) 47297,
      (ushort) 47489,
      (ushort) 31040,
      (ushort) 47873,
      (ushort) 31680,
      (ushort) 31360,
      (ushort) 47681,
      (ushort) 48641,
      (ushort) 32448,
      (ushort) 32640,
      (ushort) 48961,
      (ushort) 32000,
      (ushort) 48577,
      (ushort) 48257,
      (ushort) 31808,
      (ushort) 46081,
      (ushort) 29888,
      (ushort) 30080,
      (ushort) 46401,
      (ushort) 30464,
      (ushort) 47041,
      (ushort) 46721,
      (ushort) 30272,
      (ushort) 29184,
      (ushort) 45761,
      (ushort) 45953,
      (ushort) 29504,
      (ushort) 45313,
      (ushort) 29120,
      (ushort) 28800,
      (ushort) 45121,
      (ushort) 20480,
      (ushort) 37057,
      (ushort) 37249,
      (ushort) 20800,
      (ushort) 37633,
      (ushort) 21440,
      (ushort) 21120,
      (ushort) 37441,
      (ushort) 38401,
      (ushort) 22208,
      (ushort) 22400,
      (ushort) 38721,
      (ushort) 21760,
      (ushort) 38337,
      (ushort) 38017,
      (ushort) 21568,
      (ushort) 39937,
      (ushort) 23744,
      (ushort) 23936,
      (ushort) 40257,
      (ushort) 24320,
      (ushort) 40897,
      (ushort) 40577,
      (ushort) 24128,
      (ushort) 23040,
      (ushort) 39617,
      (ushort) 39809,
      (ushort) 23360,
      (ushort) 39169,
      (ushort) 22976,
      (ushort) 22656,
      (ushort) 38977,
      (ushort) 34817,
      (ushort) 18624,
      (ushort) 18816,
      (ushort) 35137,
      (ushort) 19200,
      (ushort) 35777,
      (ushort) 35457,
      (ushort) 19008,
      (ushort) 19968,
      (ushort) 36545,
      (ushort) 36737,
      (ushort) 20288,
      (ushort) 36097,
      (ushort) 19904,
      (ushort) 19584,
      (ushort) 35905,
      (ushort) 17408,
      (ushort) 33985,
      (ushort) 34177,
      (ushort) 17728,
      (ushort) 34561,
      (ushort) 18368,
      (ushort) 18048,
      (ushort) 34369,
      (ushort) 33281,
      (ushort) 17088,
      (ushort) 17280,
      (ushort) 33601,
      (ushort) 16640,
      (ushort) 33217,
      (ushort) 32897,
      (ushort) 16448
    };

    internal static RepAFD InicializaBaseRepAFD(RepBase _rep, string _serialNoREP)
    {
      RepAFD repAfd1 = new RepAFD();
      RepAFD repAfd2 = new RepAFD();
      try
      {
        repAfd1.repID = _rep.RepId;
        ConfiguracoesGerais configuracoesGerais = new ConfiguracoesGerais().PesquisarConfigGerais();
        if (RepAFD.VerificarSerialREP(_serialNoREP, repAfd1))
        {
          if (!configuracoesGerais.UnificaColetaAFD)
          {
            repAfd1 = repAfd2.PesquisarRegistroRepAFD(repAfd1);
            if (repAfd1.RepAFDId == 0)
            {
              RepAFD.GerarRegistroTabelaRepAFD(_rep, repAfd1);
              RepAFD.GerarRegistroTabelaRepColetaAFD(_rep, repAfd1);
              repAfd1 = repAfd2.PesquisarRegistroRepAFD(repAfd1);
            }
            if (!repAfd2.ExisteTabelaREPAFD(repAfd1))
              repAfd2.ApagaECriaTabelaREPAFD(repAfd1);
          }
          else
          {
            repAfd1 = repAfd2.PesquisarRegistroRepColetaAFD(repAfd1);
            if (!(repAfd1.posMem == "000 000 000 000") && !(repAfd1.posMem == ""))
            {
              if (repAfd1.posMem != null)
                goto label_16;
            }
            RepAFD.GerarRegistroTabelaRepColetaAFD(_rep, repAfd1);
            repAfd1 = repAfd2.PesquisarRegistroRepColetaAFD(repAfd1);
          }
        }
        else
        {
          repAfd2.AtualizarSerialREP(repAfd1, _serialNoREP);
          repAfd2.ExcluirRegistrosRepAFD(repAfd1);
          if (!configuracoesGerais.UnificaColetaAFD)
          {
            if (RepAFD.GerarRegistroTabelaRepAFD(_rep, repAfd1))
            {
              RepAFD.GerarRegistroTabelaRepColetaAFD(_rep, repAfd1);
              repAfd1 = repAfd2.PesquisarRegistroRepAFD(repAfd1);
              repAfd2.ApagaECriaTabelaREPAFD(repAfd1);
            }
          }
          else if (RepAFD.GerarRegistroTabelaRepColetaAFD(_rep, repAfd1))
            repAfd1 = repAfd2.PesquisarRegistroRepColetaAFD(repAfd1);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
label_16:
      return repAfd1;
    }

    private static bool VerificarSerialREP(string _serialNoREP, RepAFD _objRepAFD) => new RepAFD().VerificaSerialREP(_objRepAFD, _serialNoREP);

    private static bool GerarRegistroTabelaRepAFD(RepBase _rep, RepAFD _repAFDEnt)
    {
      RepAFD repAfd = new RepAFD();
      _repAFDEnt.repID = _rep.RepId;
      _repAFDEnt.nomeTabela = "AFD_" + _repAFDEnt.repID.ToString().PadLeft(4, '0');
      _repAFDEnt.posMem = "000 000 000 000";
      return repAfd.InserirRegistroRepAFD(_repAFDEnt);
    }

    private static bool GerarRegistroTabelaRepColetaAFD(RepBase _rep, RepAFD _repAFDEnt)
    {
      RepAFD repAfd = new RepAFD();
      _repAFDEnt.repID = _rep.RepId;
      _repAFDEnt.nomeTabela = "ColetaAFD";
      _repAFDEnt.posMem = "000 000 000 000";
      return repAfd.AtualizarRegistroRepColetaAFD(_repAFDEnt);
    }

    internal static bool AtualizarRegistroRepColetaAFD(RepAFD _repAFD)
    {
      try
      {
        return new RepAFD().AtualizarRegistroRepColetaAFD(_repAFD);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    internal static int AtualizarRegistroRepAFD(RepAFD _repAFD)
    {
      try
      {
        return new RepAFD().AtualizarRegistroRepAFD(_repAFD);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static RepAFD PesquisarRepAFDPorREP(int repID)
    {
      RepAFD repAfd = new RepAFD();
      RepAFD _repAFD = new RepAFD();
      _repAFD.repID = repID;
      return !new ConfiguracoesGerais().PesquisarConfigGerais().UnificaColetaAFD ? repAfd.PesquisarRegistroRepAFD(_repAFD) : repAfd.PesquisarRegistroRepColetaAFD(_repAFD);
    }

    public static int ProcessaArquivoCustomizado(ArquivoCustomizadoEnt arquivo) => new RepAFD().ProcessaArquivoCustomizado(arquivo);

    public static List<string> ListarTabelasRepAfdArquivo(int arquivo) => new RepAFD().ListarTabelasRepAfdArquivo(arquivo);

    public static List<ArquivoCustomizadoEnt> PesquisarArquivoCustomizado(
      int ArquivoId)
    {
      RepAFD repAfd = new RepAFD();
      new ConfiguracoesGerais().PesquisarConfigGerais();
      return repAfd.PesquisarArquivoCustomizado(ArquivoId);
    }

    public static List<ArquivoCustomizadoEnt> PesquisarArquivoCustomizado(
      int ArquivoId,
      DateTime inicio,
      DateTime fim)
    {
      RepAFD repAfd = new RepAFD();
      new ConfiguracoesGerais().PesquisarConfigGerais();
      return repAfd.PesquisarArquivoCustomizado(ArquivoId, inicio, fim);
    }

    public static bool ExcluirTabelaREPAFD(RepAFD repAFD) => new RepAFD().ExcluirTabelaREPAFD(repAFD);

    public static bool ExcluirTodosRegistrosRepAFD() => new RepAFD().ExcluirTodosRegistrosRepAFD();

    public static int ExcluirRegistrosRepAFD(RepAFD repAFD) => new RepAFD().ExcluirRegistrosRepAFD(repAFD);

    public static bool AtualizarUltimaMemoriaNsrTabelaREP(RepAFD repAFD) => new RepAFD().AtualizarUltimaMemoriaNsrTabelaREP(repAFD);

    public static int ExcluirRegistrosColetaAFD(RepAFD repAFD) => new RepAFD().ExcluirRegistrosColetaAFD(repAFD);

    public static int ZerarNsrColetaAFD(RepAFD repAFD) => new RepAFD().ZerarNsrColetaAFD(repAFD);

    public static int ZerarNsrTodosReps() => new RepAFD().ZerarNsrTodosReps();

    public static bool CriaTabelaColetaAFD() => new RepAFD().CriaTabelaColetaAFD();

    public static void CriaColunasRepColetaAFD() => new RepAFD().CriaColunasRepColetaAFD();

    public static void ExcluirColunasRepColetaAFD() => new RepAFD().ExcluirColunasRepColetaAFD();

    public static void ExcluirTabelaColetaAFD() => new RepAFD().ExcluirTabelaColetaAFD();

    public static List<RepAFD> PesquisarRegistrosRepAFD() => new RepAFD().PesquisarRegistrosRepAFD();

    public static List<RepAFD> RecuperaListaRepAFD() => new RepAFD().RecuperaListaRepAFD();

    public static RepAFD RecuperaRepAFD(int repId) => new RepAFD().RecuperarRepAFD(repId);

    public static void ExcluirRepAFDPorRep(int repId)
    {
      try
      {
        new RepAFD().ExcluirRepAFDPorRep(repId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static string GerarCRC(string registroAfd)
    {
      try
      {
        byte[] bytes = Encoding.Default.GetBytes(registroAfd);
        ushort num1 = ushort.MaxValue;
        foreach (byte num2 in bytes)
          num1 = (ushort) ((uint) num1 >> 8 ^ (uint) RepAFD.CrcTable[((int) num1 ^ (int) num2) & (int) byte.MaxValue]);
        return num1.ToString("X").PadLeft(4, '0');
      }
      catch
      {
        return "";
      }
    }
  }
}
