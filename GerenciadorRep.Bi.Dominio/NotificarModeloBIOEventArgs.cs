// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarModeloBIOEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarModeloBIOEventArgs : EventArgs
  {
    private int _modeloBIO;
    private int _repId;

    public NotificarModeloBIOEventArgs(int modeloBIO, int repId)
    {
      this._modeloBIO = modeloBIO;
      this._repId = repId;
    }

    public int ModeloBIO => this._modeloBIO;

    public int RepId => this._repId;
  }
}
