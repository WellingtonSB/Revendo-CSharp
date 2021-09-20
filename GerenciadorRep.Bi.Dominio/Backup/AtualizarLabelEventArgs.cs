// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.AtualizarLabelEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Windows.Forms;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class AtualizarLabelEventArgs : EventArgs
  {
    private Label _label;
    private bool _visible;
    private string _msg = string.Empty;

    public AtualizarLabelEventArgs(Label label, bool visible, string msg)
    {
      this._label = label;
      this._visible = visible;
      this._msg = msg;
    }

    public Label Label => this._label;

    public bool Visible => this._visible;

    public string Msg => this._msg;
  }
}
