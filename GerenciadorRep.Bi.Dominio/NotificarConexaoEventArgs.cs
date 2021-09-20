// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarConexaoEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarConexaoEventArgs : EventArgs
  {
    private EnumEstadoNotificacaoParaUsuario _estadoNotificacao;
    private int _repID = int.MinValue;
    private byte _statusRep;

    public NotificarConexaoEventArgs(
      EnumEstadoNotificacaoParaUsuario resultado,
      int repId,
      byte statusRep)
    {
      this._repID = repId;
      this._estadoNotificacao = resultado;
      this._statusRep = statusRep;
    }

    public int RepId => this._repID;

    public byte StatusRep => this._statusRep;

    public EnumEstadoNotificacaoParaUsuario EstadoNotificacao => this._estadoNotificacao;
  }
}
