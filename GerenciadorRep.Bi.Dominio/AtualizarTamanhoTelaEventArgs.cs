// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.AtualizarTamanhoTelaEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class AtualizarTamanhoTelaEventArgs : EventArgs
  {
    private int _tamanho;

    public AtualizarTamanhoTelaEventArgs(int tamanho) => this._tamanho = tamanho;

    public int Tamanho => this._tamanho;
  }
}
