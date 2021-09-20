// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarRegistrosConfiguracoesEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarRegistrosConfiguracoesEventArgs : EventArgs
  {
    private RepBase _entRepBase;
    private FormatoCartao _entFormatoBarras;
    private FormatoCartao _entFormatoProx;
    private Relogio _entRelogio;
    private AjusteBiometrico _ajusteBio;

    public RepBase EntRepBase
    {
      get => this._entRepBase;
      set => this._entRepBase = value;
    }

    public FormatoCartao EntFormatoBarras
    {
      get => this._entFormatoBarras;
      set => this._entFormatoBarras = value;
    }

    public FormatoCartao EntFormatoProx
    {
      get => this._entFormatoProx;
      set => this._entFormatoProx = value;
    }

    public Relogio EntRelogio
    {
      get => this._entRelogio;
      set => this._entRelogio = value;
    }

    public AjusteBiometrico EntAjusteBio
    {
      get => this._ajusteBio;
      set => this._ajusteBio = value;
    }

    public NotificarRegistrosConfiguracoesEventArgs(
      RepBase entRepBase,
      FormatoCartao entFormatoBarras,
      FormatoCartao entFormatoProx,
      Relogio entRelogio,
      AjusteBiometrico entAjusteBiometrico)
    {
      this._entRepBase = entRepBase;
      this._entFormatoBarras = entFormatoBarras;
      this._entFormatoProx = entFormatoProx;
      this._entRelogio = entRelogio;
      this._ajusteBio = entAjusteBiometrico;
    }
  }
}
