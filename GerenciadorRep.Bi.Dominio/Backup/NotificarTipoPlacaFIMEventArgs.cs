// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarTipoPlacaFIMEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarTipoPlacaFIMEventArgs : EventArgs
  {
    private int tipoPlacaFim;

    public int repId { get; set; }

    public NotificarTipoPlacaFIMEventArgs(int tipoPlacaFim, int repId)
    {
      this.tipoPlacaFim = tipoPlacaFim;
      this.repId = repId;
    }

    public int TipoPlacaFim => this.tipoPlacaFim;
  }
}
