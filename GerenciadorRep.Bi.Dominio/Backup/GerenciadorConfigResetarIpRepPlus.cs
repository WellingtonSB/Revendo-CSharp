// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigResetarIpRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcpRep;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigResetarIpRepPlus : TarefaAbstrata
  {
    private RepBase _rep;
    private GerenciadorConfigResetarIpRepPlus.Estados _estadoRep;
    private string _ipPadrao;
    public static GerenciadorConfigResetarIpRepPlus _gerenciadorConfigResetarIpRepPlus;

    public string IpPadrao
    {
      get => this._ipPadrao;
      set => this._ipPadrao = value;
    }

    public static GerenciadorConfigResetarIpRepPlus getInstance() => GerenciadorConfigResetarIpRepPlus._gerenciadorConfigResetarIpRepPlus != null ? GerenciadorConfigResetarIpRepPlus._gerenciadorConfigResetarIpRepPlus : new GerenciadorConfigResetarIpRepPlus();

    public static GerenciadorConfigResetarIpRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigResetarIpRepPlus._gerenciadorConfigResetarIpRepPlus != null ? GerenciadorConfigResetarIpRepPlus._gerenciadorConfigResetarIpRepPlus : new GerenciadorConfigResetarIpRepPlus(rep);
    }

    public static GerenciadorConfigResetarIpRepPlus getInstance(
      RepBase rep,
      string ipPadrao)
    {
      return GerenciadorConfigResetarIpRepPlus._gerenciadorConfigResetarIpRepPlus != null ? GerenciadorConfigResetarIpRepPlus._gerenciadorConfigResetarIpRepPlus : new GerenciadorConfigResetarIpRepPlus(rep, ipPadrao);
    }

    public GerenciadorConfigResetarIpRepPlus()
    {
    }

    public GerenciadorConfigResetarIpRepPlus(RepBase rep) => this._rep = rep;

    public GerenciadorConfigResetarIpRepPlus(RepBase rep, string ipPadrao)
    {
      this._ipPadrao = ipPadrao;
      this._rep = rep;
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarConfigResetarIp()
    {
      this._estadoRep = GerenciadorConfigResetarIpRepPlus.Estados.estEnvioIpPadrao;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoResetarIp(this._ipPadrao)
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorConfigResetarIpRepPlus.Estados.estEnvioIpPadrao || envelope.Grp != (byte) 10 || envelope.Cmd != (byte) 108)
        return;
      if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
      {
        this.EncerrarConexao();
        this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
      }
      else
      {
        this.EncerrarConexao();
        this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarConfigResetarIp();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      this.EncerrarConexao();
      this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      this.EncerrarConexao();
      this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioIpPadrao,
    }
  }
}
