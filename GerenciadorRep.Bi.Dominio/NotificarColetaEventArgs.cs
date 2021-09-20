// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarColetaEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarColetaEventArgs : EventArgs
  {
    private ResultadoColetaAuto _resultado = new ResultadoColetaAuto();

    public NotificarColetaEventArgs(
      int repID,
      string repDesc,
      int totRegistrosColetados,
      string horaInicio,
      string horaFim,
      Constantes.RESULTADO_COLETA status)
    {
      this._resultado.RepID = repID;
      this._resultado.Rep_desc = repDesc;
      this._resultado.TotRegistrosColetados = totRegistrosColetados;
      this._resultado.HoraInicio = horaInicio;
      this._resultado.HoraFim = horaFim;
      this._resultado.Status = status;
    }

    public ResultadoColetaAuto Resultado => this._resultado;
  }
}
