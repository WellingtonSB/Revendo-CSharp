// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.AtualizarPictureEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Windows.Forms;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class AtualizarPictureEventArgs : EventArgs
  {
    private PictureBox _pic;
    private bool _visible;

    public AtualizarPictureEventArgs(PictureBox pic, bool visible)
    {
      this._pic = pic;
      this._visible = visible;
    }

    public PictureBox PIC => this._pic;

    public bool Visible => this._visible;
  }
}
