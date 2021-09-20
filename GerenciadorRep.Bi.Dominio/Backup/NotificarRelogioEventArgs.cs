// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarRelogioEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarRelogioEventArgs : EventArgs
  {
    private string _relogioNoRep = "";

    public NotificarRelogioEventArgs(byte[] relogio) => this._relogioNoRep = relogio[4].ToString().PadLeft(2, '0') + "/" + relogio[3].ToString().PadLeft(2, '0') + "/" + (int.Parse(relogio[2].ToString()) % 100).ToString() + " " + relogio[5].ToString().PadLeft(2, '0') + ":" + relogio[6].ToString().PadLeft(2, '0') + ":" + relogio[7].ToString().PadLeft(2, '0');

    public string RelogioNoRep => this._relogioNoRep;
  }
}
