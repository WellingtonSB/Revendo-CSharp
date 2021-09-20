// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarParaUsuarioEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarParaUsuarioEventArgs : EventArgs
  {
    private string _msg;
    private string _msgIdentificadorErro;
    private EnumEstadoNotificacaoParaUsuario _estadoNotificacao;
    private EnumEstadoResultadoFinalProcesso _estadoResultadoProcesso;
    private int _repID = int.MinValue;
    private string _repDesc = string.Empty;
    private int _idResultadoBio;

    public NotificarParaUsuarioEventArgs(string msg)
    {
      this._msg = msg;
      this._estadoNotificacao = EnumEstadoNotificacaoParaUsuario.nenhumEstado;
    }

    public NotificarParaUsuarioEventArgs(
      string msg,
      EnumEstadoNotificacaoParaUsuario estadoNotificacao,
      int repdId,
      string repDesc)
    {
      this._msg = msg;
      this._estadoNotificacao = estadoNotificacao;
      this._estadoResultadoProcesso = EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado;
      this._repID = repdId;
      this._repDesc = repDesc;
    }

    public NotificarParaUsuarioEventArgs(
      string msg,
      EnumEstadoNotificacaoParaUsuario estadoNotificacao,
      EnumEstadoResultadoFinalProcesso estadoResultadoProcesso,
      int repdId,
      string repDesc)
    {
      this._msg = msg;
      this._estadoNotificacao = estadoNotificacao;
      this._estadoResultadoProcesso = estadoResultadoProcesso;
      this._repID = repdId;
      this._repDesc = repDesc;
    }

    public NotificarParaUsuarioEventArgs(
      int idResultadoBio,
      string msg,
      EnumEstadoNotificacaoParaUsuario estadoNotificacao,
      EnumEstadoResultadoFinalProcesso estadoResultadoProcesso,
      int repdId,
      string repDesc)
    {
      this._msg = msg;
      this._idResultadoBio = idResultadoBio;
      this._estadoNotificacao = estadoNotificacao;
      this._estadoResultadoProcesso = estadoResultadoProcesso;
      this._repID = repdId;
      this._repDesc = repDesc;
    }

    public NotificarParaUsuarioEventArgs(
      string msg,
      EnumEstadoNotificacaoParaUsuario estadoNotificacao,
      string msgIdentificadorErro,
      int repdId,
      string repDesc)
    {
      this._msg = msg;
      this._estadoNotificacao = estadoNotificacao;
      this._msgIdentificadorErro = msgIdentificadorErro;
      this._repID = repdId;
      this._repDesc = repDesc;
    }

    public int RepId => this._repID;

    public int IdResultadoBio => this._idResultadoBio;

    public string RepDesc => this._repDesc;

    public string Msg => this._msg;

    public string MsgIdentificadorErro => this._msgIdentificadorErro;

    public EnumEstadoNotificacaoParaUsuario EstadoNotificacao => this._estadoNotificacao;

    public EnumEstadoResultadoFinalProcesso EstadoResultadoProcesso
    {
      get => this._estadoResultadoProcesso;
      set => this._estadoResultadoProcesso = value;
    }
  }
}
