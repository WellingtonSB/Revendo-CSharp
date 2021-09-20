// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.HabilitarTextBoxEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Windows.Forms;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class HabilitarTextBoxEventArgs : EventArgs
  {
    private TextBox _text;
    private bool _habilita;

    public HabilitarTextBoxEventArgs(TextBox text, bool habilita)
    {
      this._text = text;
      this._habilita = habilita;
    }

    public TextBox TEXT => this._text;

    public bool Habilita => this._habilita;
  }
}
