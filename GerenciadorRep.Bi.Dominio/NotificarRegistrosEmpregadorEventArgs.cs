// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarRegistrosEmpregadorEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarRegistrosEmpregadorEventArgs : EventArgs
  {
    private Empregador _entEmpregador;

    public Empregador EntEmpregador
    {
      get => this._entEmpregador;
      set => this._entEmpregador = value;
    }

    public NotificarRegistrosEmpregadorEventArgs(Empregador entEmpregador) => this._entEmpregador = entEmpregador;
  }
}
