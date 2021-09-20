// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorChavePublica
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.GerenciadorRep.Bi.Dominio.Properties;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorChavePublica : TarefaAbstrata
  {
    private RepBase _rep;
    private string _chavePublica = string.Empty;
    private GerenciadorChavePublica.Estados _estadoRep;
    public static GerenciadorChavePublica _gerenciadorChavePublica;

    public event EventHandler<NotificarParaUsuarioEventArgs> OnNotificarChavePublica;

    public static GerenciadorChavePublica getInstance() => GerenciadorChavePublica._gerenciadorChavePublica != null ? GerenciadorChavePublica._gerenciadorChavePublica : new GerenciadorChavePublica();

    public static GerenciadorChavePublica getInstance(RepBase rep) => GerenciadorChavePublica._gerenciadorChavePublica != null ? GerenciadorChavePublica._gerenciadorChavePublica : new GerenciadorChavePublica(rep);

    public GerenciadorChavePublica()
    {
    }

    public GerenciadorChavePublica(RepBase rep) => this._rep = rep;

    public override void IniciarProcesso() => this.Conectar(this._rep);

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorChavePublica.Estados.estAguardandoChavePublica)
        return;
      byte[] numArray = new byte[64];
      if (envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 108)
        return;
      Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 2, (Array) numArray, 0, 64);
      for (int index = 0; index < numArray.Length; ++index)
      {
        GerenciadorChavePublica gerenciadorChavePublica = this;
        gerenciadorChavePublica._chavePublica = gerenciadorChavePublica._chavePublica + numArray[index].ToString("X").PadLeft(2, '0') + " ";
      }
      if (this.OnNotificarChavePublica != null)
        this.OnNotificarChavePublica((object) this.OnNotificarChavePublica, new NotificarParaUsuarioEventArgs(this._chavePublica, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, ""));
      this.EncerrarConexao();
    }

    private void EnviarSolicitacaoChavePublica()
    {
      Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
      this._estadoRep = GerenciadorChavePublica.Estados.estAguardandoChavePublica;
      MsgTcpAplicacaoSolicitaChavePublica solicitaChavePublica = new MsgTcpAplicacaoSolicitaChavePublica();
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) solicitaChavePublica;
      this.ClienteSocket.Enviar(envelope, true);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarSolicitacaoChavePublica();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck() => this.EncerrarConexao();

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorChavePublica.Estados.estAguardandoChavePublica)
        menssagem = Resources.msgNENHUMA_RESPOSTA_SOLIC_CONFIG_REP;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estAguardandoChavePublica,
    }
  }
}
