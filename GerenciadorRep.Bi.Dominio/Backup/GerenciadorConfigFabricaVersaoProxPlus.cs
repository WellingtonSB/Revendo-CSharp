// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigFabricaVersaoProxPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigFabricaVersaoProxPlus : TarefaAbstrata
  {
    private Relogio _entRelogio;
    private FormatoCartao _formatoEnt;
    private RepBase _rep;
    private GerenciadorConfigFabricaVersaoProxPlus.Estados _estadoRep;
    private ConfigFabricaREP _configFabrica;
    public static GerenciadorConfigFabricaVersaoProxPlus _gerenciadorConfigFabricaVersaoProxPlus;

    public ConfigFabricaREP ConfigFabrica
    {
      get => this._configFabrica;
      set => this._configFabrica = value;
    }

    public event EventHandler<NotificarLiberacaoVersaoProxEventArgs> OnNotificarEnvioVersaoProx;

    public static GerenciadorConfigFabricaVersaoProxPlus getInstance() => GerenciadorConfigFabricaVersaoProxPlus._gerenciadorConfigFabricaVersaoProxPlus != null ? GerenciadorConfigFabricaVersaoProxPlus._gerenciadorConfigFabricaVersaoProxPlus : new GerenciadorConfigFabricaVersaoProxPlus();

    public static GerenciadorConfigFabricaVersaoProxPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigFabricaVersaoProxPlus._gerenciadorConfigFabricaVersaoProxPlus != null ? GerenciadorConfigFabricaVersaoProxPlus._gerenciadorConfigFabricaVersaoProxPlus : new GerenciadorConfigFabricaVersaoProxPlus(rep);
    }

    public static GerenciadorConfigFabricaVersaoProxPlus getInstance(
      RepBase rep,
      ConfigFabricaREP configFabrica)
    {
      return GerenciadorConfigFabricaVersaoProxPlus._gerenciadorConfigFabricaVersaoProxPlus != null ? GerenciadorConfigFabricaVersaoProxPlus._gerenciadorConfigFabricaVersaoProxPlus : new GerenciadorConfigFabricaVersaoProxPlus(rep, configFabrica);
    }

    public GerenciadorConfigFabricaVersaoProxPlus()
    {
    }

    public GerenciadorConfigFabricaVersaoProxPlus(RepBase rep) => this._rep = rep;

    public GerenciadorConfigFabricaVersaoProxPlus(RepBase rep, ConfigFabricaREP configFabrica)
    {
      this._configFabrica = configFabrica;
      this._rep = rep;
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarConfigVersaoProx()
    {
      this._estadoRep = GerenciadorConfigFabricaVersaoProxPlus.Estados.estEnvioVersaoProx;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoVersaoPCI(this._configFabrica.VersaoProx)
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorConfigFabricaVersaoProxPlus.Estados.estEnvioVersaoProx || envelope.Grp != (byte) 14 || envelope.Cmd != (byte) 111)
        return;
      if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
      {
        NotificarLiberacaoVersaoProxEventArgs e = new NotificarLiberacaoVersaoProxEventArgs(true);
        if (this.OnNotificarEnvioVersaoProx == null)
          return;
        this.EncerrarConexao();
        this.OnNotificarEnvioVersaoProx((object) this, e);
      }
      else
      {
        NotificarLiberacaoVersaoProxEventArgs e = new NotificarLiberacaoVersaoProxEventArgs(false);
        if (this.OnNotificarEnvioVersaoProx == null)
          return;
        this.EncerrarConexao();
        this.OnNotificarEnvioVersaoProx((object) this, e);
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarConfigVersaoProx();
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
      estEnvioVersaoProx,
      estFinal,
    }
  }
}
