// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarSolicitacaoInfoRep
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarSolicitacaoInfoRep : EventArgs
  {
    private string _serialRep;
    private string _versaoFwRep;
    private bool _repBloqueado;
    private int _status;
    private int _modeloBiometrico;

    public NotificarSolicitacaoInfoRep(string SerialRep, string VersaoFWRep, bool bloqueado)
    {
      this._serialRep = SerialRep;
      this._versaoFwRep = VersaoFWRep;
      this._repBloqueado = bloqueado;
    }

    public NotificarSolicitacaoInfoRep(
      string SerialRep,
      string VersaoFWRep,
      int Status,
      int ModeloBiometrico)
    {
      this._serialRep = SerialRep;
      this._versaoFwRep = VersaoFWRep;
      this._status = Status;
      this._modeloBiometrico = ModeloBiometrico;
      this._repBloqueado = Status != 0;
    }

    public string SerialRep => this._serialRep;

    public string VersaoFWRep => this._versaoFwRep;

    public bool RepBloqueado => this._repBloqueado;

    public int StatusRep => this._status;

    public int ModeloBiometrico => this._modeloBiometrico;
  }
}
