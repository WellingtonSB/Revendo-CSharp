// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarRecebimentoParaUsuarioEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarRecebimentoParaUsuarioEventArgs : EventArgs
  {
    private string _msg;
    private string _msgIdentificadorErro;
    private EnumEstadoNotificacaoParaUsuario _estadoNotificacao;
    private EnumEstadoResultadoFinalProcesso _estadoResultadoProcesso;
    private int _empregadorID = int.MinValue;
    private string _local = "";

    public NotificarRecebimentoParaUsuarioEventArgs(
      string msg,
      EnumEstadoNotificacaoParaUsuario estadoNotificacao,
      int empregadorId,
      string local)
    {
      this._msg = msg;
      this._estadoNotificacao = estadoNotificacao;
      this._estadoResultadoProcesso = EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado;
      this._empregadorID = empregadorId;
      this._local = local;
    }

    public NotificarRecebimentoParaUsuarioEventArgs(
      string msg,
      EnumEstadoNotificacaoParaUsuario estadoNotificacao,
      EnumEstadoResultadoFinalProcesso estadoResultadoProcesso,
      int empregadorId,
      string local)
    {
      this._msg = msg;
      this._estadoNotificacao = estadoNotificacao;
      this._estadoResultadoProcesso = estadoResultadoProcesso;
      this._empregadorID = empregadorId;
      this._local = local;
    }

    public NotificarRecebimentoParaUsuarioEventArgs(
      string msg,
      EnumEstadoNotificacaoParaUsuario estadoNotificacao,
      string msgIdentificadorErro,
      int empregadorId,
      string local)
    {
      this._msg = msg;
      this._estadoNotificacao = estadoNotificacao;
      this._msgIdentificadorErro = msgIdentificadorErro;
      this._empregadorID = empregadorId;
      this._local = local;
    }

    public int EmpregadorID => this._empregadorID;

    public string Msg => this._msg;

    public string Local => this._local;

    public string MsgIdentificadorErro => this._msgIdentificadorErro;

    public EnumEstadoNotificacaoParaUsuario EstadoNotificacao => this._estadoNotificacao;

    public EnumEstadoResultadoFinalProcesso EstadoResultadoProcesso
    {
      get => this._estadoResultadoProcesso;
      set => this._estadoResultadoProcesso = value;
    }
  }
}
