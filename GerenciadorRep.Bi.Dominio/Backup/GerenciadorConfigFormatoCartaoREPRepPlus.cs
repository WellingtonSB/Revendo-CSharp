// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigFormatoCartaoREPRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Threading;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigFormatoCartaoREPRepPlus : TarefaAbstrata
  {
    private FormatoCartao _formatoEntBarras;
    private FormatoCartao _formatoEntProx;
    private ConfiguracaoBarras20 _configBarras20;
    private FormatoCartao _solicitaformatoBarrasEnt;
    private FormatoCartao _solicitaformatoProxEnt;
    private ConfiguracaoBarras20 confBarras20;
    private ArquivoCFGEntity _arquivoCFGEntSDK = new ArquivoCFGEntity();
    private bool _enviarSomenteCFGTLM;
    private RepBase _rep;
    private bool _chamadaPeloSdk;
    private GerenciadorConfigFormatoCartaoREPRepPlus.Estados _estadoRep;
    public static GerenciadorConfigFormatoCartaoREPRepPlus _gerenciadorConfigFormatoCartaoREPRepPlus;

    public static GerenciadorConfigFormatoCartaoREPRepPlus getInstance() => GerenciadorConfigFormatoCartaoREPRepPlus._gerenciadorConfigFormatoCartaoREPRepPlus != null ? GerenciadorConfigFormatoCartaoREPRepPlus._gerenciadorConfigFormatoCartaoREPRepPlus : new GerenciadorConfigFormatoCartaoREPRepPlus();

    public static GerenciadorConfigFormatoCartaoREPRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigFormatoCartaoREPRepPlus._gerenciadorConfigFormatoCartaoREPRepPlus != null ? GerenciadorConfigFormatoCartaoREPRepPlus._gerenciadorConfigFormatoCartaoREPRepPlus : new GerenciadorConfigFormatoCartaoREPRepPlus(rep);
    }

    public static GerenciadorConfigFormatoCartaoREPRepPlus getInstance(
      RepBase rep,
      FormatoCartao formatoCartaoBarras,
      FormatoCartao formatoCartaoProx)
    {
      return GerenciadorConfigFormatoCartaoREPRepPlus._gerenciadorConfigFormatoCartaoREPRepPlus != null ? GerenciadorConfigFormatoCartaoREPRepPlus._gerenciadorConfigFormatoCartaoREPRepPlus : new GerenciadorConfigFormatoCartaoREPRepPlus(rep, formatoCartaoBarras, formatoCartaoProx);
    }

    public static GerenciadorConfigFormatoCartaoREPRepPlus getInstance(
      RepBase rep,
      FormatoCartao formatoCartaoBarras,
      FormatoCartao formatoCartaoProx,
      ArquivoCFGEntity arquivoCFGTLM,
      ConfiguracaoBarras20 confBarras20)
    {
      return GerenciadorConfigFormatoCartaoREPRepPlus._gerenciadorConfigFormatoCartaoREPRepPlus != null ? GerenciadorConfigFormatoCartaoREPRepPlus._gerenciadorConfigFormatoCartaoREPRepPlus : new GerenciadorConfigFormatoCartaoREPRepPlus(rep, formatoCartaoBarras, formatoCartaoProx, arquivoCFGTLM, confBarras20);
    }

    public static GerenciadorConfigFormatoCartaoREPRepPlus getInstance(
      RepBase rep,
      FormatoCartao formatoCartaoBarras,
      FormatoCartao formatoCartaoProx,
      ArquivoCFGEntity arquivoCFGTLM,
      bool enviarSomenteCFGTLM)
    {
      return GerenciadorConfigFormatoCartaoREPRepPlus._gerenciadorConfigFormatoCartaoREPRepPlus != null ? GerenciadorConfigFormatoCartaoREPRepPlus._gerenciadorConfigFormatoCartaoREPRepPlus : new GerenciadorConfigFormatoCartaoREPRepPlus(rep, formatoCartaoBarras, formatoCartaoProx, arquivoCFGTLM, enviarSomenteCFGTLM);
    }

    public GerenciadorConfigFormatoCartaoREPRepPlus() => this._enviarSomenteCFGTLM = false;

    public GerenciadorConfigFormatoCartaoREPRepPlus(RepBase rep)
    {
      this._rep = rep;
      this._enviarSomenteCFGTLM = false;
    }

    public GerenciadorConfigFormatoCartaoREPRepPlus(
      RepBase rep,
      FormatoCartao formatoCartaoBarras,
      FormatoCartao formatoCartaoProx)
    {
      this._rep = rep;
      this._formatoEntProx = formatoCartaoProx;
      this._formatoEntBarras = formatoCartaoBarras;
      this._chamadaPeloSdk = true;
      this._enviarSomenteCFGTLM = false;
    }

    public GerenciadorConfigFormatoCartaoREPRepPlus(
      RepBase rep,
      FormatoCartao formatoCartaoBarras,
      FormatoCartao formatoCartaoProx,
      ArquivoCFGEntity arquivoCFGTLM,
      ConfiguracaoBarras20 confBarras20)
    {
      this._rep = rep;
      this._formatoEntProx = formatoCartaoProx;
      this._formatoEntBarras = formatoCartaoBarras;
      this._configBarras20 = confBarras20;
      this._configBarras20.ignorarFormatoPrincipal = false;
      this._chamadaPeloSdk = true;
      this._arquivoCFGEntSDK = arquivoCFGTLM;
      this._enviarSomenteCFGTLM = false;
    }

    public GerenciadorConfigFormatoCartaoREPRepPlus(
      RepBase rep,
      FormatoCartao formatoCartaoBarras,
      FormatoCartao formatoCartaoProx,
      ArquivoCFGEntity arquivoCFGTLM,
      bool enviarSomenteCFGTLM)
    {
      this._rep = rep;
      this._formatoEntProx = formatoCartaoProx;
      this._formatoEntBarras = formatoCartaoBarras;
      this._chamadaPeloSdk = true;
      this._arquivoCFGEntSDK = arquivoCFGTLM;
      this._enviarSomenteCFGTLM = enviarSomenteCFGTLM;
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarConfiguracoesGeraisFormatoCartao()
    {
      this._estadoRep = GerenciadorConfigFormatoCartaoREPRepPlus.Estados.estEnvioConfiguracoesFormatoCartao;
      try
      {
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        int num1 = 0;
        int num2 = 0;
        if (!this._chamadaPeloSdk)
        {
          this._formatoEntBarras = FormatoCartao.PesquisarFormatoCartaoBarrasByRepIdRepPlus(this._rep.RepId);
          this._formatoEntProx = FormatoCartao.PesquisarFormatoCartaoProxByRepIdRepPlus(this._rep.RepId);
          switch (this._formatoEntBarras.formatoCartaoID)
          {
            case 0:
            case 10:
              num1 = 0;
              break;
            case 3:
              num1 = 1;
              break;
            case 4:
              num1 = 2;
              break;
            case 5:
              num1 = 4;
              break;
            case 6:
              num1 = 3;
              break;
            case 8:
              num1 = this._rep.VersaoFW != 2 ? 5 : 0;
              break;
            case 9:
              num1 = this._rep.VersaoFW != 2 ? 5 : 0;
              break;
          }
          switch (this._formatoEntProx.formatoCartaoID)
          {
            case 1:
            case 11:
              num2 = 1;
              break;
            case 2:
            case 12:
            case 13:
            case 14:
              num2 = 2;
              break;
            case 7:
              num2 = 0;
              break;
          }
        }
        else
        {
          num1 = this._formatoEntBarras.formatoCartaoID;
          if (this._rep.VersaoFW == 2 && this._formatoEntBarras.formatoCartaoID == 5)
            num1 = 0;
          num2 = this._formatoEntProx.formatoCartaoID;
        }
        if (this._formatoEntBarras.formatoCartao.Equals("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00"))
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgFORMATO_CARTAO_BARRAS_ZERADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        }
        else if (this._formatoEntProx.formatoCartao.Equals("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00"))
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgFORMATO_CARTAO_PROX_ZERADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        }
        else
        {
          MsgTCPAplicacaoConfigFormatoCartaoREPRepPlus cartaoRepRepPlus = new MsgTCPAplicacaoConfigFormatoCartaoREPRepPlus(this._formatoEntBarras.formatoCartao, this._formatoEntProx.formatoCartao, (long) this._formatoEntBarras.numDigitosFixos, (long) num1, (long) num2);
          envelope.MsgAplicacao = (MsgTcpAplicacaoBase) cartaoRepRepPlus;
          this.ClienteSocket.Enviar(envelope, true);
        }
      }
      catch (Exception ex)
      {
        this.NotificarParaUsuario(ex.Message.ToString(), EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
      }
    }

    private void EnviarBufferCFGTlm()
    {
      this._estadoRep = GerenciadorConfigFormatoCartaoREPRepPlus.Estados.estEnvioBufferCFGTlm;
      ArquivoCFGBI arquivoCfgbi = new ArquivoCFGBI();
      ArquivoCFGEntity arquivoCfgEntity = this._chamadaPeloSdk ? this._arquivoCFGEntSDK : arquivoCfgbi.PesquisarArquivoCFGPorRepID(this._rep.RepId);
      if (arquivoCfgEntity.CFG.Equals(""))
      {
        this._estadoRep = GerenciadorConfigFormatoCartaoREPRepPlus.Estados.estFinal;
        this.NotificarParaUsuario(Resources.msgAVISO_ARQUIVO_CONFIG_TLM_VAZIO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        this.EncerrarConexao();
      }
      else
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoBufferCFGTlmRepPlus(PadraoTLM.GetByteArray(arquivoCfgEntity.CFG))
        }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      switch (this._estadoRep)
      {
        case GerenciadorConfigFormatoCartaoREPRepPlus.Estados.estEnvioConfiguracoesFormatoCartao:
          if (envelope.Grp != (byte) 10 || envelope.Cmd != (byte) 106)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
          {
            ConfiguracaoBarras20 configuracaoBarras20 = new ConfiguracaoBarras20();
            FormatoCartao formatoCartao;
            if (!this._chamadaPeloSdk)
            {
              formatoCartao = FormatoCartao.PesquisarFormatoCartaoBarrasByRepIdRepPlus(this._rep.RepId);
            }
            else
            {
              formatoCartao = this._formatoEntBarras;
              if (formatoCartao.formatoCartaoID == 3)
                formatoCartao.formatoCartaoID = 6;
            }
            if (!this._chamadaPeloSdk)
            {
              if (formatoCartao.formatoCartaoID == 6 || configuracaoBarras20.VerificarSeExistTLM(this._rep.RepId) > 0)
              {
                this.EnviarBufferCFGTlm();
                break;
              }
              if (this._rep.VersaoFW == 4)
              {
                this.EnviarBarras20();
                break;
              }
              this.EncerrarConexao();
              this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              break;
            }
            if (this._arquivoCFGEntSDK.CFG.Equals(""))
            {
              if (this._rep.VersaoFW == 4)
              {
                this.EnviarBarras20();
                break;
              }
              this.EncerrarConexao();
              this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              break;
            }
            this.EnviarBufferCFGTlm();
            break;
          }
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 2)
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(string.Format(Resources.msgPROCESSO_DE_ENVIO_DE_CARTAO_FINALIZADO_JA_ATUALIZADO, (object) Environment.NewLine), EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            break;
          }
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
        case GerenciadorConfigFormatoCartaoREPRepPlus.Estados.estEnvioBufferCFGTlm:
          if (envelope.Grp != (byte) 10 || envelope.MsgAplicacaoEmBytes[1] != (byte) 103)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
          {
            if (this._rep.VersaoFW == 4)
            {
              this.EnviarBarras20();
              break;
            }
            this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            this.EncerrarConexao();
            break;
          }
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          this.EncerrarConexao();
          break;
        case GerenciadorConfigFormatoCartaoREPRepPlus.Estados.estEnvioBarras20:
          if (envelope.Grp != (byte) 10 || envelope.Cmd != (byte) 112)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
          {
            this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            this.EncerrarConexao();
            break;
          }
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 2)
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(string.Format(Resources.msgPROCESSO_DE_ENVIO_DE_CARTAO_FINALIZADO_JA_ATUALIZADO, (object) Environment.NewLine), EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            break;
          }
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          this.EncerrarConexao();
          break;
        case GerenciadorConfigFormatoCartaoREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoes:
          if (envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 101)
            break;
          this.RecuperaConfiguracoesDoREP(envelope);
          FormatoCartao formatoCartao1 = FormatoCartao.PesquisarFormatoCartaoBarrasByRepIdRepPlus(this._rep.RepId);
          FormatoCartao formatoCartao2 = FormatoCartao.PesquisarFormatoCartaoProxByRepIdRepPlus(this._rep.RepId);
          if (this._solicitaformatoBarrasEnt != null && this._solicitaformatoProxEnt != null)
          {
            if (this._solicitaformatoBarrasEnt.formatoCartao != null && this._solicitaformatoProxEnt.formatoCartao != null)
            {
              int num1 = 0;
              int num2 = 7;
              switch (this._solicitaformatoBarrasEnt.formatoCartaoID)
              {
                case 0:
                  num1 = 0;
                  break;
                case 1:
                  num1 = 3;
                  break;
                case 2:
                  num1 = 4;
                  break;
                case 3:
                  num1 = 6;
                  break;
                case 4:
                  num1 = 5;
                  break;
                case 5:
                  num1 = formatoCartao1.formatoCartaoID == 8 || formatoCartao1.formatoCartaoID == 9 ? formatoCartao1.formatoCartaoID : 9;
                  break;
              }
              switch (this._solicitaformatoProxEnt.formatoCartaoID)
              {
                case 0:
                  num2 = 7;
                  break;
                case 1:
                  num2 = 1;
                  break;
                case 2:
                  num2 = 2;
                  break;
              }
              bool flag1 = false;
              bool flag2 = false;
              if (num1 == 0)
                flag1 = !formatoCartao1.formatoCartao.Equals(this._solicitaformatoBarrasEnt.formatoCartao);
              if (num2 == 7)
                flag2 = !formatoCartao2.formatoCartao.Equals(this._solicitaformatoProxEnt.formatoCartao);
              if ((formatoCartao1.formatoCartaoID == num1 || num1 == 0 && formatoCartao1.formatoCartao == this._solicitaformatoBarrasEnt.formatoCartao && this._rep.VersaoFW == 3) && formatoCartao2.formatoCartaoID == num2 && formatoCartao2.formatoCartao == this._solicitaformatoProxEnt.formatoCartao && !flag1 && !flag2)
              {
                if ((formatoCartao1.formatoCartaoID == 5 || formatoCartao1.formatoCartaoID == 10 || formatoCartao1.formatoCartaoID == 6) && num1 == 0)
                {
                  this.EnviarConfiguracoesGeraisFormatoCartao();
                  break;
                }
                if (this._rep.VersaoFW == 4)
                {
                  this.EnviarSolicitacaoBarras20();
                  break;
                }
                this.EncerrarConexao();
                this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CARTOES_FINALIZADO_SEM_ALTERACOES, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
                break;
              }
              this.EnviarConfiguracoesGeraisFormatoCartao();
              break;
            }
            this.EnviarConfiguracoesGeraisFormatoCartao();
            break;
          }
          this.EnviarConfiguracoesGeraisFormatoCartao();
          break;
        case GerenciadorConfigFormatoCartaoREPRepPlus.Estados.estEnvioSolicitacaoBarras20:
          if (envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 120)
            break;
          this.RecuperaConfiguracoesBarras20(envelope);
          if (!new ConfiguracaoBarras20().Pesquisar(this._rep.RepId).Equals((object) this.confBarras20))
          {
            this.EnviarBarras20();
            break;
          }
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CARTOES_FINALIZADO_SEM_ALTERACOES, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
          break;
      }
    }

    private void RecuperaConfiguracoesDoREP(Envelope envelope)
    {
      this._solicitaformatoBarrasEnt = new FormatoCartao(envelope.MsgAplicacaoEmBytes, 0);
      this._solicitaformatoProxEnt = new FormatoCartao(envelope.MsgAplicacaoEmBytes, 1);
      if (this._chamadaPeloSdk)
        return;
      try
      {
        if (!this._solicitaformatoBarrasEnt.formatoCartao.Equals("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00") || !this._solicitaformatoProxEnt.formatoCartao.Equals("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00"))
          return;
        new TipoTerminal().AtualizarSincronizado(new TipoTerminal()
        {
          RepId = this._rep.RepId,
          Sincronizado = false
        });
        this._rep.AtualizarConfiguracoesLeitorCPF(new RepBase()
        {
          ConfiguracaoLeitorCpf = "",
          ConfiguracaoLeitorDataHora = DateTime.MinValue,
          RepId = this._rep.RepId
        });
        Thread.Sleep(5);
      }
      catch
      {
      }
    }

    private void EnviarSolicitacaoConfiguracoesGerais()
    {
      this._estadoRep = GerenciadorConfigFormatoCartaoREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoes;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaConfiguracoesGeraisRepPlus()
      }, true);
    }

    private void EnviarSolicitacaoBarras20()
    {
      this._estadoRep = GerenciadorConfigFormatoCartaoREPRepPlus.Estados.estEnvioSolicitacaoBarras20;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaBarras20()
      }, true);
    }

    private void EnviarBarras20()
    {
      this._estadoRep = GerenciadorConfigFormatoCartaoREPRepPlus.Estados.estEnvioBarras20;
      ConfiguracaoBarras20 configuracaoBarras20_1 = new ConfiguracaoBarras20();
      ConfiguracaoBarras20 configuracaoBarras20_2;
      if (this._chamadaPeloSdk)
        configuracaoBarras20_2 = this._configBarras20;
      else if (configuracaoBarras20_1.VerificarSeExisteCartaoBarras20(this._rep.RepId) > 0)
      {
        configuracaoBarras20_2 = new ConfiguracaoBarras20().Pesquisar(this._rep.RepId);
      }
      else
      {
        configuracaoBarras20_2 = new ConfiguracaoBarras20();
        configuracaoBarras20_2.tab1TipoCartao = (int) byte.MaxValue;
        configuracaoBarras20_2.tab1QtdDigitos = 0;
        configuracaoBarras20_2.tab1DigitosLidos = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
        configuracaoBarras20_2.tab2TipoCartao = (int) byte.MaxValue;
        configuracaoBarras20_2.tab2QtdDigitos = 0;
        configuracaoBarras20_2.tab2DigitosLidos = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
        configuracaoBarras20_2.tab3TipoCartao = (int) byte.MaxValue;
        configuracaoBarras20_2.tab3QtdDigitos = 0;
        configuracaoBarras20_2.tab3DigitosLidos = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
      }
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPFormatoBarras20()
        {
          IgnorarFormato = Convert.ToByte(configuracaoBarras20_2.ignorarFormatoPrincipal),
          digitosTabela1 = this.ConverterStringbyte(configuracaoBarras20_2.tab1DigitosLidos.Trim()),
          QtdDigitosTabela1 = Convert.ToByte(configuracaoBarras20_2.tab1QtdDigitos),
          TipoCartaoTabela1 = Convert.ToByte(configuracaoBarras20_2.tipoPadraoRep(configuracaoBarras20_2.tab1TipoCartao)),
          digitosTabela2 = this.ConverterStringbyte(configuracaoBarras20_2.tab2DigitosLidos.Trim()),
          QtdDigitosTabela2 = Convert.ToByte(configuracaoBarras20_2.tab2QtdDigitos),
          TipoCartaoTabela2 = Convert.ToByte(configuracaoBarras20_2.tipoPadraoRep(configuracaoBarras20_2.tab2TipoCartao)),
          digitosTabela3 = this.ConverterStringbyte(configuracaoBarras20_2.tab3DigitosLidos.Trim()),
          QtdDigitosTabela3 = Convert.ToByte(configuracaoBarras20_2.tab3QtdDigitos),
          TipoCartaoTabela3 = Convert.ToByte(configuracaoBarras20_2.tipoPadraoRep(configuracaoBarras20_2.tab3TipoCartao))
        }
      }, true);
    }

    private byte[] ConverterStringbyte(string dados)
    {
      byte[] numArray = new byte[16];
      string[] strArray = dados.Split(' ');
      for (int index = 0; index < strArray.Length; ++index)
        numArray[index] = Convert.ToByte(strArray[index]);
      return numArray;
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
      {
        this.AtualizarSerial();
        if (int.Parse(this._rep.Serial.Substring(5, 5)) == 364 || int.Parse(this._rep.Serial.Substring(5, 5)) == 365 || int.Parse(this._rep.Serial.Substring(5, 5)) == 366 || int.Parse(this._rep.Serial.Substring(5, 5)) == 397 || int.Parse(this._rep.Serial.Substring(5, 5)) == 398 || int.Parse(this._rep.Serial.Substring(5, 5)) == 472 || int.Parse(this._rep.Serial.Substring(5, 5)) == 473 || int.Parse(this._rep.Serial.Substring(5, 5)) == 474 || int.Parse(this._rep.Serial.Substring(5, 5)) == 470 || int.Parse(this._rep.Serial.Substring(5, 5)) == 471 || int.Parse(this._rep.Serial.Substring(5, 5)) == 514 || int.Parse(this._rep.Serial.Substring(5, 5)) == 515 || int.Parse(this._rep.Serial.Substring(5, 5)) == 516 || int.Parse(this._rep.Serial.Substring(5, 5)) == 517 || int.Parse(this._rep.Serial.Substring(5, 5)) == 518)
        {
          if (this._rep.TipoTerminalId == 13 || this._rep.TipoTerminalId == 15 || this._rep.TipoTerminalId == 14 || this._rep.TipoTerminalId == 17 || this._rep.TipoTerminalId == 19 || this._rep.TipoTerminalId == 21 || this._rep.TipoTerminalId == 28 || this._rep.TipoTerminalId == 29 || this._rep.TipoTerminalId == 26)
          {
            if (!this._chamadaPeloSdk)
              this.EnviarSolicitacaoConfiguracoesGerais();
            else if (!this._enviarSomenteCFGTLM)
              this.EnviarConfiguracoesGeraisFormatoCartao();
            else
              this.EnviarBufferCFGTlm();
          }
          else
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgERRO_EQUIPAMENTO_CONFIGURADO_DIFERENTE_CONECTADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          }
        }
        else if (int.Parse(this._rep.Serial.Substring(5, 5)) == 99999)
        {
          if (this._rep.TipoTerminalId == 16 || this._rep.TipoTerminalId == 18 || this._rep.TipoTerminalId == 20 || this._rep.TipoTerminalId == 27 || this._chamadaPeloSdk)
          {
            if (!this._chamadaPeloSdk)
              this.EnviarSolicitacaoConfiguracoesGerais();
            else if (!this._enviarSomenteCFGTLM)
              this.EnviarConfiguracoesGeraisFormatoCartao();
            else
              this.EnviarBufferCFGTlm();
          }
          else
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgERRO_EQUIPAMENTO_CONFIGURADO_DIFERENTE_CONECTADO_DEMO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          }
        }
        else if (int.Parse(this._rep.Serial.Substring(5, 5)) == 18)
        {
          if (this._rep.TipoTerminalId == 24 || this._rep.TipoTerminalId == 25 || this._rep.TipoTerminalId == 32 || this._chamadaPeloSdk)
          {
            if (!this._chamadaPeloSdk)
              this.EnviarSolicitacaoConfiguracoesGerais();
            else if (!this._enviarSomenteCFGTLM)
              this.EnviarConfiguracoesGeraisFormatoCartao();
            else
              this.EnviarBufferCFGTlm();
          }
          else
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgERRO_EQUIPAMENTO_CONFIGURADO_DIFERENTE_CONECTADO_373, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          }
        }
        else if (int.Parse(this._rep.Serial.Substring(5, 5)) == 20)
        {
          if (this._rep.TipoTerminalId == 30 || this._rep.TipoTerminalId == 31 || this._chamadaPeloSdk)
          {
            if (!this._chamadaPeloSdk)
              this.EnviarSolicitacaoConfiguracoesGerais();
            else if (!this._enviarSomenteCFGTLM)
              this.EnviarConfiguracoesGeraisFormatoCartao();
            else
              this.EnviarBufferCFGTlm();
          }
          else
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgERRO_EQUIPAMENTO_CONFIGURADO_DIFERENTE_TASK_373, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          }
        }
        else
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgERRO_EQUIPAMENTO_CONFIGURADO_DIFERENTE_CONECTADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        }
      }
      else
        this.EncerrarConexao();
    }

    private void AtualizarSerial()
    {
      if (!RegistrySingleton.GetInstance().VALIDAR_SERIAL)
        return;
      new RepAFD().AtualizarSerialREP(new RepAFD()
      {
        repID = this._rep.RepId
      }, this._rep.Serial);
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigFormatoCartaoREPRepPlus.Estados.estEnvioConfiguracoesFormatoCartao)
        menssagem = Resources.msgTIMEOUT_ENVIO_CONFIG_GERAL;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigFormatoCartaoREPRepPlus.Estados.estEnvioConfiguracoesFormatoCartao:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_CONFIG_GERAL;
          break;
        case GerenciadorConfigFormatoCartaoREPRepPlus.Estados.estEnvioBufferCFGTlm:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_CONFIG_GERAL;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private void RecuperaConfiguracoesBarras20(Envelope envelope) => this.confBarras20 = new ConfiguracaoBarras20(envelope.MsgAplicacaoEmBytes);

    private new enum Estados
    {
      estEnvioConfiguracoesFormatoCartao,
      estEnvioBufferCFGTlm,
      estEnvioBarras20,
      estFinal,
      estEnvioSolicitacaoConfiguracoes,
      estEnvioSolicitacaoBarras20,
    }
  }
}
