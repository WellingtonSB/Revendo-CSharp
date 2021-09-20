// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigREPClientRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigREPClientRepPlus : TarefaAbstrata
  {
    private RepBase _rep;
    private GerenciadorConfigREPClientRepPlus.Estados _estadoRep;
    public static GerenciadorConfigREPClientRepPlus _gerenciadorConfigREPClientRepPlus;

    public static GerenciadorConfigREPClientRepPlus getInstance() => GerenciadorConfigREPClientRepPlus._gerenciadorConfigREPClientRepPlus != null ? GerenciadorConfigREPClientRepPlus._gerenciadorConfigREPClientRepPlus : new GerenciadorConfigREPClientRepPlus();

    public static GerenciadorConfigREPClientRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigREPClientRepPlus._gerenciadorConfigREPClientRepPlus != null ? GerenciadorConfigREPClientRepPlus._gerenciadorConfigREPClientRepPlus : new GerenciadorConfigREPClientRepPlus(rep);
    }

    public GerenciadorConfigREPClientRepPlus()
    {
    }

    public GerenciadorConfigREPClientRepPlus(RepBase rep) => this._rep = rep;

    public override void IniciarProcesso()
    {
      this._rep.EnvioConfigAvancadas = true;
      this.Conectar(this._rep);
    }

    private void EnviarConfigRepClient()
    {
      this._estadoRep = GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepClient;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoConfigRepClientRepPlus(this._rep.repClient, this._rep.PortaVariavel, this._rep.ipServidor, (short) this._rep.portaServidor, this._rep.mascaraRede, this._rep.Gateway, (byte) this._rep.intervaloConexao)
      }, true);
    }

    private void EnviarConfigRepClientDNS()
    {
      this._estadoRep = GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepClientDNS;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoConfigRepClientRepPlusDNS(this._rep.nomeServidor, this._rep.DNS, this._rep.TipoConexaoDNS)
      }, true);
    }

    private void EnviarConfigRepClientNomeRep()
    {
      this._estadoRep = GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepClientNomeRep;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoConfigRepPlusNomeRep(this._rep.NomeRep)
      }, true);
    }

    private void EnviarConfigRepClientNuvem()
    {
      this._estadoRep = GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepNuvem;
      Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
      RepBase repBase = this._rep.PesquisarRepPorID(this._rep.RepId);
      MsgTCPAplicacaoConfigRepNuvem aplicacaoConfigRepNuvem = new MsgTCPAplicacaoConfigRepNuvem(repBase.intervaloNuvem, (short) repBase.portaNuvem);
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) aplicacaoConfigRepNuvem;
      this.ClienteSocket.Enviar(envelope, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      switch (this._estadoRep)
      {
        case GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepClient:
          if (envelope.Grp == (byte) 10 && envelope.Cmd == (byte) 104)
          {
            if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
            {
              if (envelope.MsgAplicacaoEmBytes.Length < 3)
              {
                this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
                this.EncerrarConexao();
                break;
              }
              if (this._rep.VersaoFW == 3 || this._rep.VersaoFW == 1 || this._rep.VersaoFW == 4)
              {
                this.EnviarConfigRepClientDNS();
                break;
              }
              this.EncerrarConexao();
              this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              break;
            }
            this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            this.EncerrarConexao();
            break;
          }
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          this.EncerrarConexao();
          break;
        case GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepClientDNS:
          if (envelope.Grp == (byte) 10 && envelope.Cmd == (byte) 110)
          {
            if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
            {
              this.EnviarConfigRepClientNomeRep();
              break;
            }
            this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            this.EncerrarConexao();
            break;
          }
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          this.EncerrarConexao();
          break;
        case GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepClientNomeRep:
          if (envelope.Grp == (byte) 10 && envelope.Cmd == (byte) 111)
          {
            if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
            {
              if (this._rep.VersaoFW == 4 && !this.ChamadaSDK)
              {
                this.EnviarConfigRepClientNuvem();
                break;
              }
              this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              this.EncerrarConexao();
              break;
            }
            this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            this.EncerrarConexao();
            break;
          }
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          this.EncerrarConexao();
          break;
        case GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepNuvem:
          if (envelope.Grp == (byte) 10 && envelope.Cmd == (byte) 113)
          {
            if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
            {
              this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              this.EncerrarConexao();
              break;
            }
            this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            this.EncerrarConexao();
            break;
          }
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          this.EncerrarConexao();
          break;
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarConfigRepClient();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepClient:
        case GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepClientDNS:
        case GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepClientNomeRep:
        case GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepNuvem:
          menssagem = Resources.msgTIMEOUT_ENVIO_CONFIG_GERAL;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepClient:
        case GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepClientDNS:
        case GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepClientNomeRep:
        case GerenciadorConfigREPClientRepPlus.Estados.estEnvioConfigRepNuvem:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_CONFIG_GERAL;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioConfigRepClient,
      estEnvioConfigRepClientDNS,
      estEnvioConfigRepClientNomeRep,
      estEnvioConfigRepNuvem,
    }
  }
}
