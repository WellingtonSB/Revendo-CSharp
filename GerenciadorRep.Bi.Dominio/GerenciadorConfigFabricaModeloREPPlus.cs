// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigFabricaModeloREPPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigFabricaModeloREPPlus : TarefaAbstrata
  {
    private Relogio _entRelogio;
    private FormatoCartao _formatoEnt;
    private RepBase _rep;
    private GerenciadorConfigFabricaModeloREPPlus.Estados _estadoRep;
    private ConfigFabricaREP _configFabrica;
    public static GerenciadorConfigFabricaModeloREPPlus _gerenciadorConfigFabricaModeloREPPlus;

    public ConfigFabricaREP ConfigFabrica
    {
      get => this._configFabrica;
      set => this._configFabrica = value;
    }

    public event EventHandler<NotificarLiberacaoModeloRepPlusEventArgs> OnNotificarEnvioModeloRep;

    public event EventHandler<EventArgs> OnNotificarEnvioConfigImpressoras;

    public static GerenciadorConfigFabricaModeloREPPlus getInstance() => GerenciadorConfigFabricaModeloREPPlus._gerenciadorConfigFabricaModeloREPPlus != null ? GerenciadorConfigFabricaModeloREPPlus._gerenciadorConfigFabricaModeloREPPlus : new GerenciadorConfigFabricaModeloREPPlus();

    public static GerenciadorConfigFabricaModeloREPPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigFabricaModeloREPPlus._gerenciadorConfigFabricaModeloREPPlus != null ? GerenciadorConfigFabricaModeloREPPlus._gerenciadorConfigFabricaModeloREPPlus : new GerenciadorConfigFabricaModeloREPPlus(rep);
    }

    public static GerenciadorConfigFabricaModeloREPPlus getInstance(
      RepBase rep,
      ConfigFabricaREP configFabrica)
    {
      return GerenciadorConfigFabricaModeloREPPlus._gerenciadorConfigFabricaModeloREPPlus != null ? GerenciadorConfigFabricaModeloREPPlus._gerenciadorConfigFabricaModeloREPPlus : new GerenciadorConfigFabricaModeloREPPlus(rep, configFabrica);
    }

    public GerenciadorConfigFabricaModeloREPPlus()
    {
    }

    public GerenciadorConfigFabricaModeloREPPlus(RepBase rep) => this._rep = rep;

    public GerenciadorConfigFabricaModeloREPPlus(RepBase rep, ConfigFabricaREP configFabrica)
    {
      this._configFabrica = configFabrica;
      this._rep = rep;
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarConfigModeloRep()
    {
      this._estadoRep = GerenciadorConfigFabricaModeloREPPlus.Estados.estEnvioModeloRep;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoModeloRepPlus(this._configFabrica.ModeloRep)
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorConfigFabricaModeloREPPlus.Estados.estEnvioModeloRep || envelope.Grp != (byte) 14 || envelope.Cmd != (byte) 112)
        return;
      if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0 || envelope.MsgAplicacaoEmBytes[2] == (byte) 2)
      {
        NotificarLiberacaoModeloRepPlusEventArgs e = new NotificarLiberacaoModeloRepPlusEventArgs(true);
        if (this.OnNotificarEnvioModeloRep == null)
          return;
        this.EncerrarConexao();
        this.OnNotificarEnvioModeloRep((object) this, e);
      }
      else
      {
        NotificarLiberacaoModeloRepPlusEventArgs e = new NotificarLiberacaoModeloRepPlusEventArgs(false);
        if (this.OnNotificarEnvioModeloRep == null)
          return;
        this.EncerrarConexao();
        this.OnNotificarEnvioModeloRep((object) this, e);
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarConfigModeloRep();
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
      estEnvioConfigSerial_MAC,
      estEnvioConfigImpressora,
      estEnvioSolicInfo,
      estEnvioModeloRep,
      estFinal,
    }
  }
}
