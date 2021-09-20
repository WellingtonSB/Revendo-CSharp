// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarSeniorEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarSeniorEventArgs : EventArgs
  {
    private string _tipoNotificacao;
    private int _repId;
    private RepBase _repBase;

    public NotificarSeniorEventArgs(string tipoNotificacao)
    {
      this._tipoNotificacao = tipoNotificacao;
      this._repId = 0;
    }

    public NotificarSeniorEventArgs(string tipoNotificacao, int repId)
    {
      this._tipoNotificacao = tipoNotificacao;
      this._repId = repId;
    }

    public NotificarSeniorEventArgs(int repId)
    {
      this._tipoNotificacao = "";
      this._repId = repId;
    }

    public string TipoNotificacao => this._tipoNotificacao;

    public int RepID => this._repId;

    public RepBase repBase
    {
      get => this._repBase;
      set => this._repBase = value;
    }
  }
}
