// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarRegistroDoNsrSolicitadoEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarRegistroDoNsrSolicitadoEventArgs : EventArgs
  {
    private REPAFD.ResultadoBuscaRegistroNsr _resultadoBuscaRegistroNsr;
    private string _registroNsrSolicitado;
    private string _dataRegistro;
    private int _tipoRegistro;
    private string _ultimoNsrRep;
    private string _numeroSerialRep;

    public NotificarRegistroDoNsrSolicitadoEventArgs(
      REPAFD.ResultadoBuscaRegistroNsr resultadoBuscaRegistroNsr,
      string registroNsrSolicitado,
      string dataRegistro,
      int tipoRegistro,
      string ultimoNsrRep,
      string numeroSerialRep)
    {
      this._resultadoBuscaRegistroNsr = resultadoBuscaRegistroNsr;
      this._registroNsrSolicitado = registroNsrSolicitado;
      this._dataRegistro = dataRegistro;
      this._tipoRegistro = tipoRegistro;
      this._ultimoNsrRep = ultimoNsrRep;
      this._numeroSerialRep = numeroSerialRep;
    }

    public REPAFD.ResultadoBuscaRegistroNsr ResultadoBuscaRegistroNsr => this._resultadoBuscaRegistroNsr;

    public string RegistroNsrSolicitado => this._registroNsrSolicitado;

    public string DataRegistro => this._dataRegistro;

    public int TipoRegistro => this._tipoRegistro;

    public string UltimoNsrRep => this._ultimoNsrRep;

    public string NumeroSerialRep => this._numeroSerialRep;
  }
}
