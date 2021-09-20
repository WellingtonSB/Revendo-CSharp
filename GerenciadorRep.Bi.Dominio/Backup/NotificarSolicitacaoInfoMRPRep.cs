// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarSolicitacaoInfoMRPRep
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarSolicitacaoInfoMRPRep : EventArgs
  {
    private string _versaoFwMRPRep;
    private string _idAPL;
    private string _idMRP;

    public NotificarSolicitacaoInfoMRPRep(string VersaoFWMRPRep) => this._versaoFwMRPRep = VersaoFWMRPRep;

    public NotificarSolicitacaoInfoMRPRep(string VersaoFWMRPRep, string IDAPL, string IDMRP)
    {
      this._versaoFwMRPRep = VersaoFWMRPRep;
      this._idAPL = IDAPL;
      this._idMRP = IDMRP;
    }

    public string VersaoFwMRPRep => this._versaoFwMRPRep;

    public string IDAPL => this._idAPL;

    public string IDMRP => this._idMRP;
  }
}
