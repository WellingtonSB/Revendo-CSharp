// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarRegistroDoNsrSolicitadoSeniorEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarRegistroDoNsrSolicitadoSeniorEventArgs : EventArgs
  {
    private REPAFD.ResultadoBuscaRegistroNsr resultadoBuscaRegistroNsr;
    private string registroNsrSolicitado;
    private string dataRegistro;
    private int tipoRegistro;
    private string nsrSolicitado;
    private string numeroSerialRep;
    private string posMem;
    private int repId;

    public int RepId
    {
      get => this.repId;
      set => this.repId = value;
    }

    public string PosMem
    {
      get => this.posMem;
      set => this.posMem = value;
    }

    public NotificarRegistroDoNsrSolicitadoSeniorEventArgs(
      REPAFD.ResultadoBuscaRegistroNsr resultadoBuscaRegistroNsr,
      string registroNsrSolicitado,
      string dataRegistro,
      int tipoRegistro,
      string nsrSolicitado,
      string numeroSerialRep,
      string posMem,
      int repId)
    {
      this.resultadoBuscaRegistroNsr = resultadoBuscaRegistroNsr;
      this.registroNsrSolicitado = registroNsrSolicitado;
      this.dataRegistro = dataRegistro;
      this.tipoRegistro = tipoRegistro;
      this.nsrSolicitado = nsrSolicitado;
      this.numeroSerialRep = numeroSerialRep;
      this.posMem = posMem;
      this.repId = repId;
    }

    public REPAFD.ResultadoBuscaRegistroNsr ResultadoBuscaRegistroNsr => this.resultadoBuscaRegistroNsr;

    public string RegistroNsrSolicitado => this.registroNsrSolicitado;

    public string DataRegistro => this.dataRegistro;

    public int TipoRegistro => this.tipoRegistro;

    public string NsrSolicitado => this.nsrSolicitado;

    public string NumeroSerialRep => this.numeroSerialRep;
  }
}
