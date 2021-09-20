// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarStatusImpressoraEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarStatusImpressoraEventArgs : EventArgs
  {
    private ResultadoStatusImpressora _resultado = new ResultadoStatusImpressora();

    public NotificarStatusImpressoraEventArgs(
      int repID,
      string rep_desc,
      int grupoRep,
      string msgImpressora1,
      string msgImpressora2,
      string horario,
      Constantes.STATUS_COMUNICACAO statusComunic,
      Constantes.STATUS_IMPRESSORA statusImpressora1,
      Constantes.STATUS_IMPRESSORA statusImpressora2,
      Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA statusProcesso)
    {
      this._resultado.RepID = repID;
      this._resultado.Rep_desc = rep_desc;
      this._resultado.grupoRepId = grupoRep;
      this._resultado.MsgImpressora1 = msgImpressora1;
      this._resultado.MsgImpressora2 = msgImpressora2;
      this._resultado.HorarioVerificacao = horario;
      this._resultado.Comunicacao = statusComunic;
      this._resultado.StatusImpressora1 = statusImpressora1;
      this._resultado.StatusImpressora2 = statusImpressora2;
      this._resultado.StatusProcesso = statusProcesso;
    }

    public NotificarStatusImpressoraEventArgs(
      int repID,
      string rep_desc,
      int grupoRep,
      string msgImpressora1,
      string msgImpressora2,
      string horario,
      Constantes.STATUS_COMUNICACAO statusComunic,
      Constantes.STATUS_IMPRESSORA_REP_PLUS statusImpressora1,
      Constantes.STATUS_PROCESSO_VERIFICA_IMPRESSORA statusProcesso)
    {
      this._resultado.RepID = repID;
      this._resultado.Rep_desc = rep_desc;
      this._resultado.grupoRepId = grupoRep;
      this._resultado.MsgImpressora1 = msgImpressora1;
      this._resultado.MsgImpressora2 = msgImpressora2;
      this._resultado.HorarioVerificacao = horario;
      this._resultado.Comunicacao = statusComunic;
      this._resultado.StatusImpressoraRepPlus1 = statusImpressora1;
      this._resultado.StatusProcesso = statusProcesso;
    }

    public ResultadoStatusImpressora Resultado => this._resultado;
  }
}
