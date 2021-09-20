// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.AtualizarGroupBoxEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class AtualizarGroupBoxEventArgs : EventArgs
  {
    private GroupBox _groupBox;
    private Color _cor = Color.Green;

    public AtualizarGroupBoxEventArgs(GroupBox groupBox, Color cor)
    {
      this._groupBox = groupBox;
      this._cor = cor;
    }

    public GroupBox GroupBox => this._groupBox;

    public Color Cor => this._cor;
  }
}
