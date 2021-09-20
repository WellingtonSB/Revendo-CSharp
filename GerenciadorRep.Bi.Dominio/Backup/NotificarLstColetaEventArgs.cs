// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarLstColetaEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarLstColetaEventArgs : EventArgs
  {
    private List<ResultadoColetaAuto> _lstResultado = new List<ResultadoColetaAuto>();

    public NotificarLstColetaEventArgs(List<ResultadoColetaAuto> lstResultado) => this._lstResultado = lstResultado;

    public List<ResultadoColetaAuto> LstResultado => this._lstResultado;
  }
}
