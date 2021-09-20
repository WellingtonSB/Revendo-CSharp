// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.HorarioGerarArquivoAFD
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class HorarioGerarArquivoAFD
  {
    public static HorarioGerarArquivoAFD PesquisarHorarioGerarArquivoAFD()
    {
      HorarioGerarArquivoAFD horarioGerarArquivoAfd = new HorarioGerarArquivoAFD();
      try
      {
        return new HorarioGerarArquivoAFD().PesquisarHorarioGerarArquivoAFD();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static bool AlterarHorarioGerarArquivoAFD(
      HorarioGerarArquivoAFD _objHorarioGerarArquivoAFD)
    {
      try
      {
        return new HorarioGerarArquivoAFD().AlterarHorarioGerarArquivoAFD(_objHorarioGerarArquivoAFD);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
