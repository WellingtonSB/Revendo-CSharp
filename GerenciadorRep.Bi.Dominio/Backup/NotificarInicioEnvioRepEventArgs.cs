// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarInicioEnvioRepEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarInicioEnvioRepEventArgs : EventArgs
  {
    private long _intervalo;

    public NotificarInicioEnvioRepEventArgs(long intervalo) => this._intervalo = intervalo;

    public long Intervalo => this._intervalo;
  }
}
