// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigSolicitarConfigGeraisREPRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigSolicitarConfigGeraisREPRepPlus : TarefaAbstrata
  {
    private int _empregadorId;
    private string _serialNoREP;
    private Relogio _entRelogio;
    private FormatoCartao _formatoBarrasEnt;
    private FormatoCartao _formatoProxEnt;
    private ConfiguracaoBarras20 _confBarras20;
    private RepBase _configGeraisEnt;
    private AjusteBiometrico _ajusteBioEnt = new AjusteBiometrico();
    private RepBase _rep;
    private bool _chamadaPeloSdk;
    private GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados _estadoRep;
    public static GerenciadorConfigSolicitarConfigGeraisREPRepPlus _gerenciadorConfigSolicitarConfigGeraisREPRepPlus;

    public event EventHandler<NotificarRegistrosConfiguracoesEventArgs> OnNotificarConfiguracoesParaSdk;

    public event EventHandler<NotificarRecebimentoParaUsuarioEventArgs> OnNotificarRecebimentoUsuario;

    public static GerenciadorConfigSolicitarConfigGeraisREPRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigSolicitarConfigGeraisREPRepPlus._gerenciadorConfigSolicitarConfigGeraisREPRepPlus != null ? GerenciadorConfigSolicitarConfigGeraisREPRepPlus._gerenciadorConfigSolicitarConfigGeraisREPRepPlus : new GerenciadorConfigSolicitarConfigGeraisREPRepPlus(rep);
    }

    public static GerenciadorConfigSolicitarConfigGeraisREPRepPlus getInstance() => GerenciadorConfigSolicitarConfigGeraisREPRepPlus._gerenciadorConfigSolicitarConfigGeraisREPRepPlus != null ? GerenciadorConfigSolicitarConfigGeraisREPRepPlus._gerenciadorConfigSolicitarConfigGeraisREPRepPlus : new GerenciadorConfigSolicitarConfigGeraisREPRepPlus();

    public static GerenciadorConfigSolicitarConfigGeraisREPRepPlus getInstance(
      RepBase rep,
      bool chamadaSDK)
    {
      return GerenciadorConfigSolicitarConfigGeraisREPRepPlus._gerenciadorConfigSolicitarConfigGeraisREPRepPlus != null ? GerenciadorConfigSolicitarConfigGeraisREPRepPlus._gerenciadorConfigSolicitarConfigGeraisREPRepPlus : new GerenciadorConfigSolicitarConfigGeraisREPRepPlus(rep, chamadaSDK);
    }

    public static GerenciadorConfigSolicitarConfigGeraisREPRepPlus getInstance(
      RepBase rep,
      int empregadorId)
    {
      return GerenciadorConfigSolicitarConfigGeraisREPRepPlus._gerenciadorConfigSolicitarConfigGeraisREPRepPlus != null ? GerenciadorConfigSolicitarConfigGeraisREPRepPlus._gerenciadorConfigSolicitarConfigGeraisREPRepPlus : new GerenciadorConfigSolicitarConfigGeraisREPRepPlus(rep, empregadorId);
    }

    public GerenciadorConfigSolicitarConfigGeraisREPRepPlus()
    {
    }

    public GerenciadorConfigSolicitarConfigGeraisREPRepPlus(RepBase rep) => this._rep = rep;

    public GerenciadorConfigSolicitarConfigGeraisREPRepPlus(RepBase rep, int empregadorId)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
      this._empregadorId = empregadorId;
    }

    public GerenciadorConfigSolicitarConfigGeraisREPRepPlus(RepBase rep, bool chamadaSDK)
    {
      this._rep = rep;
      this._chamadaPeloSdk = chamadaSDK;
    }

    public override void IniciarProcesso()
    {
      this.NotificarRecebimentoParaUsuario(Resources.msgPROCESSO_DE_SOLICITACAO_DE_CONFIGURACOES_ANDAMENTO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado);
      this.Conectar(this._rep);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      switch (this._estadoRep)
      {
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoes:
          if (envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 101)
            break;
          this.RecuperaConfiguracoesDoREP(envelope);
          if (this._rep.VersaoFW == 4)
          {
            this.EnviarSolicitacaoBarras20();
            break;
          }
          if (this._rep.VersaoFW == 3 || this._rep.VersaoFW == 1)
          {
            this.EnviarSolicitacaoConfiguracoesGeraisRepClientDNS();
            break;
          }
          this.EnviarSolicitacaoConfiguracoesGeraisRepClient();
          break;
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoBarras20:
          if (envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 120)
            break;
          this.RecuperaConfiguracoesBarras20(envelope);
          this.EnviarSolicitacaoConfiguracoesGeraisRepClientDNS();
          break;
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesRepClient:
          if (envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 104)
            break;
          this.RecuperaConfiguracoesRepClientDoREP(envelope);
          if (!this._chamadaPeloSdk)
          {
            if (this._rep.VersaoFW == 4)
            {
              this.EnviarSolicitacaoConfiguracoesGeraisRepNuvem();
              break;
            }
            this.EncerrarConexao();
            this.GravarConfiguracoesGeraisNoBD();
            this.NotificarRecebimentoParaUsuario(Resources.msgPROCESSO_DE_SOLICITACAO_DE_CONFIGURACOES_SUCESSO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso);
            break;
          }
          this.NotificaMensagemParaSdk(this._configGeraisEnt, this._formatoBarrasEnt, this._formatoProxEnt, this._entRelogio, this._ajusteBioEnt);
          this.EncerrarConexao();
          break;
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesRepClientDNS:
          if (envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 117)
            break;
          this.RecuperaConfiguracoesDoREPDNS(envelope);
          this.EnviarSolicitacaoConfiguracoesGeraisRepNomeRep();
          break;
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesRepNomeRep:
          if (envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 118)
            break;
          this.RecuperaConfiguracoesDoREPNomeRep(envelope);
          if (this._rep.ModeloFim == 1)
          {
            this.EnviarSolicitacaoConfiguracoesGeraisAjusteBiometricoCama();
            break;
          }
          if (this._rep.ModeloFim == 2)
          {
            this.EnviarSolicitacaoConfiguracoesGeraisAjusteBiometricoSagem();
            break;
          }
          this.EnviarSolicitacaoConfiguracoesGeraisAjusteBiometricoNitgen();
          break;
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesRepNuvem:
          if (envelope.Grp != (byte) 15 || envelope.Cmd != (byte) 121)
            break;
          this.RecuperaConfiguracoesRepNuvem(envelope);
          if (!this._chamadaPeloSdk)
          {
            this.EncerrarConexao();
            this.GravarConfiguracoesGeraisNoBD();
            this.NotificarRecebimentoParaUsuario(Resources.msgPROCESSO_DE_SOLICITACAO_DE_CONFIGURACOES_SUCESSO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso);
            break;
          }
          this.NotificaMensagemParaSdk(this._configGeraisEnt, this._formatoBarrasEnt, this._formatoProxEnt, this._entRelogio, this._ajusteBioEnt);
          this.EncerrarConexao();
          break;
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesAjusteBiometricoNitgen:
          if (envelope.Grp != (byte) 19 || envelope.Cmd != (byte) 103)
            break;
          this.RecuperaConfiguracoesDoREPAjusteBiometricoNitgen(envelope);
          this.EnviarSolicitacaoConfiguracoesGeraisRepClient();
          break;
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesAjusteBiometricoCama:
          if (envelope.Grp != (byte) 20 || envelope.Cmd != (byte) 102)
            break;
          this.RecuperaConfiguracoesDoREPAjusteBiometricoCama(envelope);
          this.EnviarSolicitacaoConfiguracoesGeraisRepClient();
          break;
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesAjusteBiometricoSagem:
          if (envelope.Grp != (byte) 21 || envelope.Cmd != (byte) 102)
            break;
          this.RecuperaConfiguracoesDoREPAjusteBiometricoSagem(envelope);
          this.EnviarSolicitacaoConfiguracoesGeraisRepClient();
          break;
      }
    }

    private void GravarConfiguracoesGeraisNoBD()
    {
      RepBase repBase1 = new RepBase();
      SortableBindingList<RepBase> sortableBindingList = repBase1.PesquisarRepsPorEmpregador(this._empregadorId);
      bool flag = false;
      int formatoId1 = 0;
      int formatoId2 = 0;
      switch (this._formatoBarrasEnt.formatoCartaoID)
      {
        case 0:
          formatoId1 = 0;
          break;
        case 1:
          formatoId1 = 3;
          break;
        case 2:
          formatoId1 = 4;
          break;
        case 3:
          formatoId1 = 6;
          break;
        case 4:
          formatoId1 = 5;
          break;
        case 5:
          formatoId1 = 9;
          break;
      }
      if (this._rep.VersaoFW == 4 && (this._confBarras20.tab1TipoCartao != (int) byte.MaxValue || this._confBarras20.tab2TipoCartao != (int) byte.MaxValue || this._confBarras20.tab3TipoCartao != (int) byte.MaxValue))
        formatoId1 = this._confBarras20.ignorarFormatoPrincipal ? 10 : formatoId1;
      switch (this._formatoProxEnt.formatoCartaoID)
      {
        case 0:
          formatoId2 = 7;
          break;
        case 1:
          if (this._formatoProxEnt.formatoCartao == "00 00 00 00 00 00 00 00 00 10 11 12 13 14 15 16")
          {
            formatoId2 = 11;
            break;
          }
          formatoId2 = 1;
          this._formatoProxEnt.formatoCartao = "";
          break;
        case 2:
          if (this._formatoProxEnt.formatoCartao == "00 00 00 00 00 06 07 08 09 10 11 12 13 14 15 16")
          {
            formatoId2 = 12;
            break;
          }
          if (this._formatoProxEnt.formatoCartao == "00 00 03 04 05 06 07 08 09 10 11 12 13 14 15 16")
          {
            formatoId2 = 13;
            break;
          }
          formatoId2 = 2;
          this._formatoProxEnt.formatoCartao = "";
          break;
      }
      foreach (RepBase RepBaseEnt in (Collection<RepBase>) sortableBindingList)
      {
        if (RepBaseEnt.Serial.Equals(this._serialNoREP) || RepBaseEnt.Ip.Equals(this._rep.IpAddress))
        {
          this._configGeraisEnt.Ip = RepBaseEnt.Ip;
          this._configGeraisEnt.Local = RepBaseEnt.Local;
          this._configGeraisEnt.Porta = RepBaseEnt.Porta;
          this._configGeraisEnt.RepId = RepBaseEnt.RepId;
          this._configGeraisEnt.TerminalId = RepBaseEnt.TerminalId;
          this._configGeraisEnt.grupoId = RepBaseEnt.grupoId;
          this._configGeraisEnt.Desc = RepBaseEnt.Desc;
          this._configGeraisEnt.EmpregadorId = RepBaseEnt.EmpregadorId;
          this._configGeraisEnt.Serial = this._serialNoREP;
          this._configGeraisEnt.ChaveComunicacao = this._rep.ChaveComunicacao;
          this._configGeraisEnt.FormatoCartaoId = formatoId1;
          this._configGeraisEnt.FormatoCartaoProxId = formatoId2;
          RepBaseEnt.SenhaComunic = this._configGeraisEnt.SenhaComunic;
          RepBaseEnt.SenhaBio = this._configGeraisEnt.SenhaBio;
          RepBaseEnt.SenhaRelogio = this._configGeraisEnt.SenhaRelogio;
          repBase1.AtualizarRep(this._configGeraisEnt);
          repBase1.AtualizarConfiguracoes(RepBaseEnt);
          this._entRelogio.ConfiguracaoId = RepBaseEnt.ConfiguracaoId;
          new Relogio().AtualizarHorVeraoMulti(this._entRelogio);
          FormatoCartao.AtualizarFormatoPadraoLivreRepPlus(this._formatoBarrasEnt.formatoCartao, formatoId1, this._formatoBarrasEnt.numDigitosFixos, RepBaseEnt.ConfiguracaoId);
          FormatoCartao.AtualizarFormatoPadraoAbatrackRepPlus(this._formatoProxEnt.formatoCartao, formatoId2, this._formatoProxEnt.numDigitosFixos, RepBaseEnt.ConfiguracaoId);
          new RepAFD().AtualizarSerialREP(new RepAFD()
          {
            repID = this._configGeraisEnt.RepId
          }, this._serialNoREP);
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      this._configGeraisEnt.Ip = this._rep.IpAddress;
      this._configGeraisEnt.Host = this._rep.Host;
      this._configGeraisEnt.TipoConexao = this._rep.TipoConexao;
      this._configGeraisEnt.Local = this._rep.Local;
      this._configGeraisEnt.Porta = this._rep.Port;
      this._configGeraisEnt.TerminalId = this._rep.TipoTerminalId;
      this._configGeraisEnt.grupoId = 0;
      this._configGeraisEnt.Desc = this._rep.Local;
      this._configGeraisEnt.EmpregadorId = this._empregadorId;
      this._configGeraisEnt.ChaveComunicacao = this._rep.ChaveComunicacao;
      this._configGeraisEnt.Serial = this._serialNoREP;
      this._configGeraisEnt.FormatoCartaoId = formatoId1;
      this._configGeraisEnt.FormatoCartaoProxId = formatoId2;
      TipoTerminal tipoTerminal = new TipoTerminal();
      if (tipoTerminal.InserirRep(this._configGeraisEnt) == 0)
        return;
      tipoTerminal.InserirConfiguracoesRep(this._configGeraisEnt);
      int num = repBase1.PesquisarUltimoIdConfiguracaoGeral();
      FormatoCartao.AtualizarFormatoPadraoLivreRepPlus(this._formatoBarrasEnt.formatoCartao, formatoId1, this._formatoBarrasEnt.numDigitosFixos, num);
      FormatoCartao.AtualizarFormatoPadraoAbatrackRepPlus(this._formatoProxEnt.formatoCartao, formatoId2, this._formatoProxEnt.numDigitosFixos, num);
      RepAFD repAfd = new RepAFD();
      RepAFD _repAFD = new RepAFD();
      RepBase repBase2 = new RepBase();
      _repAFD.repID = repBase2.PesquisarUltimoIdRep();
      repAfd.AtualizarSerialREP(_repAFD, this._serialNoREP);
      this._entRelogio.ConfiguracaoId = num;
      new Relogio().AtualizarHorVeraoMulti(this._entRelogio);
      RepBio repBio = new RepBio();
      RepBio RepBioEnt = new RepBio();
      int ajusteBio1 = repBio.PequisarPrimeiroAjusteBiometrico();
      if (this._rep.VersaoFW == 3 || this._rep.VersaoFW == 1 || this._rep.VersaoFW == 4)
      {
        if (this._ajusteBioEnt.BioIdent == 0 && this._ajusteBioEnt.BioVerif == 0 && this._ajusteBioEnt.BioCaptura == 0 && this._ajusteBioEnt.BioTimeOut == 0 && this._ajusteBioEnt.BioLFD == 0)
        {
          if (this._ajusteBioEnt.BioIdentCama != 0 && this._ajusteBioEnt.BioTimeOutCama != 0)
          {
            int ajusteBio2 = this.EncontrouAjusteBioCompativelDB(this._ajusteBioEnt, 1);
            if (ajusteBio2 != 0)
              this.InserirConfiguracoesBio(num, ajusteBio2, this._ajusteBioEnt);
          }
          else if (this._ajusteBioEnt.IdentSagem != 0 && this._ajusteBioEnt.VerifSagem != 0)
          {
            int ajusteBio3 = this.EncontrouAjusteBioCompativelDB(this._ajusteBioEnt, 2);
            if (ajusteBio3 != 0)
              this.InserirConfiguracoesBio(num, ajusteBio3, this._ajusteBioEnt);
          }
        }
        else
        {
          int ajusteBio4 = this.EncontrouAjusteBioCompativelDB(this._ajusteBioEnt, 0);
          if (ajusteBio4 != 0)
            this.InserirConfiguracoesBio(num, ajusteBio4, this._ajusteBioEnt);
        }
      }
      else if (ajusteBio1 != 0)
        this.InserirConfiguracoesBio(num, ajusteBio1, this._ajusteBioEnt);
      RepBioEnt.BioLFD = this._ajusteBioEnt.BioLFD;
      RepBioEnt.ConfiguracaoId = num;
      repBio.AtualizarNivelLFD(RepBioEnt);
      if (this._rep.VersaoFW != 4 || this._confBarras20.tab1TipoCartao == (int) byte.MaxValue && this._confBarras20.tab2TipoCartao == (int) byte.MaxValue && this._confBarras20.tab3TipoCartao == (int) byte.MaxValue)
        return;
      ConfiguracaoBarras20 configuracaoBarras20 = new ConfiguracaoBarras20();
      this._confBarras20.RepId = _repAFD.repID;
      configuracaoBarras20.Gravar(this._confBarras20);
    }

    private int EncontrouAjusteBioCompativelDB(AjusteBiometrico ajuste, int tipoPlacaFim)
    {
      int num = 0;
      foreach (RepBio repBio in new RepBio().RecuperaListaAjusteBiometrico())
      {
        switch (tipoPlacaFim)
        {
          case 0:
            if (ajuste.BioIdent == repBio.BioIdent && ajuste.BioVerif == repBio.BioVerif && ajuste.BioFiltro == repBio.BioFiltro && ajuste.BioCaptura == repBio.BioCaptura && ajuste.BioTimeOut == repBio.BioTimeOut)
              return repBio.AjusteBiometricoId;
            continue;
          case 1:
            if (ajuste.BioIdentCama == repBio.BioIdentCama && ajuste.BioTimeOutCama == repBio.BioTimeOutCama)
              return repBio.AjusteBiometricoId;
            continue;
          case 2:
            if (ajuste.VerifSagem == repBio.VerifSagem && ajuste.IdentSagem == repBio.VerifSagem)
              return repBio.AjusteBiometricoId;
            continue;
          default:
            continue;
        }
      }
      return num;
    }

    private bool InserirConfiguracoesBio(
      int ConfiguracaoId,
      int ajusteBio,
      AjusteBiometrico dedoVivo)
    {
      bool flag = false;
      try
      {
        if (new TipoTerminal().InserirConfiguracoesBio(ConfiguracaoId, ajusteBio, dedoVivo) > 0)
          flag = true;
      }
      catch
      {
      }
      return flag;
    }

    private void NotificaMensagemParaSdk(
      RepBase entRepBase,
      FormatoCartao entFormatoBarras,
      FormatoCartao entFormatoProx,
      Relogio entRelogio,
      AjusteBiometrico entAjusteBiometrico)
    {
      NotificarRegistrosConfiguracoesEventArgs e = new NotificarRegistrosConfiguracoesEventArgs(entRepBase, entFormatoBarras, entFormatoProx, entRelogio, entAjusteBiometrico);
      if (this.OnNotificarConfiguracoesParaSdk == null)
        return;
      this.OnNotificarConfiguracoesParaSdk((object) this, e);
    }

    private void RecuperaConfiguracoesDoREP(Envelope envelope)
    {
      this._entRelogio = new Relogio(envelope.MsgAplicacaoEmBytes);
      this._configGeraisEnt = new RepBase(envelope.MsgAplicacaoEmBytes, 0);
      this._formatoBarrasEnt = new FormatoCartao(envelope.MsgAplicacaoEmBytes, 0);
      this._formatoProxEnt = new FormatoCartao(envelope.MsgAplicacaoEmBytes, 1);
    }

    private void RecuperaConfiguracoesBarras20(Envelope envelope) => this._confBarras20 = new ConfiguracaoBarras20(envelope.MsgAplicacaoEmBytes);

    private void RecuperaConfiguracoesRepClientDoREP(Envelope envelope)
    {
      RepBase repBase = new RepBase(envelope.MsgAplicacaoEmBytes, 1);
      this._configGeraisEnt.repClient = repBase.repClient;
      this._configGeraisEnt.ipServidor = repBase.ipServidor;
      this._configGeraisEnt.portaServidor = repBase.portaServidor;
      this._configGeraisEnt.mascaraRede = repBase.mascaraRede;
      this._configGeraisEnt.Gateway = repBase.Gateway;
      this._configGeraisEnt.intervaloConexao = repBase.intervaloConexao;
      this._configGeraisEnt.tempoEspera = 20;
      if (this._configGeraisEnt.NomeRep != null)
        return;
      this._configGeraisEnt.NomeRep = string.Empty;
    }

    private void RecuperaConfiguracoesDoREPDNS(Envelope envelope)
    {
      this._configGeraisEnt.TipoConexaoDNS = (int) envelope.MsgAplicacaoEmBytes[2];
      this._configGeraisEnt.habilitaNuvem = this._configGeraisEnt.TipoConexaoDNS == 3;
      byte[] bytes = new byte[512];
      Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 3, (Array) bytes, 0, bytes.Length);
      this._configGeraisEnt.nomeServidor = Encoding.Default.GetString(bytes).Replace("\0", "");
      byte[] numArray = new byte[4];
      Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 515, (Array) numArray, 0, numArray.Length);
      string str = "";
      foreach (byte num in numArray)
        str = str + num.ToString() + ".";
      this._configGeraisEnt.DNS = str.Substring(0, str.Length - 1);
    }

    private void RecuperaConfiguracoesDoREPNomeRep(Envelope envelope)
    {
      byte[] bytes = new byte[32];
      Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 2, (Array) bytes, 0, bytes.Length);
      this._configGeraisEnt.NomeRep = Encoding.Default.GetString(bytes).Replace("\0", "").Replace("º", "");
    }

    private void RecuperaConfiguracoesRepNuvem(Envelope envelope)
    {
      byte[] numArray1 = new byte[4];
      Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 2, (Array) numArray1, 0, numArray1.Length);
      this._configGeraisEnt.intervaloNuvem = BitConverter.ToInt32(numArray1, 0);
      byte[] numArray2 = new byte[2];
      Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 6, (Array) numArray2, 0, numArray2.Length);
      string s = "";
      Array.Reverse((Array) numArray2);
      foreach (byte num in numArray2)
        s += num.ToString("X").PadLeft(2, '0');
      this._configGeraisEnt.portaNuvem = (int) long.Parse(s, NumberStyles.HexNumber);
    }

    private void RecuperaConfiguracoesDoREPAjusteBiometricoNitgen(Envelope envelope)
    {
      this._ajusteBioEnt = new AjusteBiometrico();
      this._ajusteBioEnt.BioIdent = (int) envelope.MsgAplicacaoEmBytes[4];
      this._ajusteBioEnt.BioVerif = (int) envelope.MsgAplicacaoEmBytes[5];
      this._ajusteBioEnt.BioFiltro = (int) envelope.MsgAplicacaoEmBytes[6];
      this._ajusteBioEnt.BioCaptura = (int) envelope.MsgAplicacaoEmBytes[7];
      this._ajusteBioEnt.BioTimeOut = (int) envelope.MsgAplicacaoEmBytes[8];
      this._ajusteBioEnt.BioLFD = (int) envelope.MsgAplicacaoEmBytes[9];
    }

    private void RecuperaConfiguracoesDoREPAjusteBiometricoCama(Envelope envelope)
    {
      this._ajusteBioEnt = new AjusteBiometrico();
      this._ajusteBioEnt.BioIdentCama = (int) envelope.MsgAplicacaoEmBytes[4];
      this._ajusteBioEnt.BioTimeOutCama = (int) envelope.MsgAplicacaoEmBytes[5];
      this._ajusteBioEnt.BioDedoDuplicado = envelope.MsgAplicacaoEmBytes[6] != (byte) 0;
    }

    private void RecuperaConfiguracoesDoREPAjusteBiometricoSagem(Envelope envelope)
    {
      this._ajusteBioEnt = new AjusteBiometrico();
      this._ajusteBioEnt.IdentSagem = (int) envelope.MsgAplicacaoEmBytes[4];
      this._ajusteBioEnt.VerifSagem = (int) envelope.MsgAplicacaoEmBytes[5];
      this._ajusteBioEnt.BioFiltro = (int) envelope.MsgAplicacaoEmBytes[6];
      this._ajusteBioEnt.AdvancedMadchine = envelope.MsgAplicacaoEmBytes[7];
      this._ajusteBioEnt.TimeoutSagem = (int) envelope.MsgAplicacaoEmBytes[8];
      this._ajusteBioEnt.DedoDuplicadoSagem = (int) envelope.MsgAplicacaoEmBytes[9];
      this._ajusteBioEnt.FFD = (int) envelope.MsgAplicacaoEmBytes[10];
    }

    private void EnviarSolicitacaoConfiguracoesGerais()
    {
      this._estadoRep = GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoes;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaConfiguracoesGeraisRepPlus()
      }, true);
    }

    private void EnviarSolicitacaoConfiguracoesGeraisAjusteBiometricoNitgen()
    {
      this._estadoRep = GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesAjusteBiometricoNitgen;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaConfiguracoesGeraisAjusteBiometricoRepPlus((byte) 19, (byte) 3)
      }, true);
    }

    private void EnviarSolicitacaoConfiguracoesGeraisAjusteBiometricoCama()
    {
      this._estadoRep = GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesAjusteBiometricoCama;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaConfiguracoesGeraisAjusteBiometricoRepPlus((byte) 20, (byte) 2)
      }, true);
    }

    private void EnviarSolicitacaoConfiguracoesGeraisAjusteBiometricoSagem()
    {
      this._estadoRep = GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesAjusteBiometricoSagem;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaConfiguracoesGeraisAjusteBiometricoRepPlus((byte) 21, (byte) 2)
      }, true);
    }

    private void EnviarSolicitacaoConfiguracoesGeraisRepClient()
    {
      this._estadoRep = GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesRepClient;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaConfiguracoesGeraisRepClientRepPlus()
      }, true);
    }

    private void EnviarSolicitacaoConfiguracoesGeraisRepNuvem()
    {
      this._estadoRep = GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesRepNuvem;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaConfiguracoesGeraisRepNuvem()
      }, true);
    }

    private void EnviarSolicitacaoConfiguracoesGeraisRepClientDNS()
    {
      this._estadoRep = GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesRepClientDNS;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaConfiguracoesGeraisRepClientDNSRepPlus()
      }, true);
    }

    private void EnviarSolicitacaoBarras20()
    {
      this._estadoRep = GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoBarras20;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaBarras20()
      }, true);
    }

    private void EnviarSolicitacaoConfiguracoesGeraisRepNomeRep()
    {
      this._estadoRep = GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesRepNomeRep;
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTCPAplicacaoSolicitaConfiguracoesGeraisRepNomeRepPlus()
      }, true);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
      {
        this._serialNoREP = this._rep.Serial;
        this.EnviarSolicitacaoConfiguracoesGerais();
      }
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string _msg = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoes:
          _msg = Resources.msgTIMEOUT_ENVIO_CONFIG_GERAL;
          break;
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesRepClient:
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesRepNuvem:
          _msg = Resources.msgTIMEOUT_ENVIO_CONFIG_GERAL;
          break;
      }
      this.EncerrarConexao();
      this.NotificarRecebimentoParaUsuario(_msg, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha);
    }

    public override void TratarNenhumaResposta()
    {
      string _msg = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoes:
          _msg = Resources.msgNENHUMA_RESPOSTA_APOS_CONFIG_GERAL;
          break;
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesRepClient:
        case GerenciadorConfigSolicitarConfigGeraisREPRepPlus.Estados.estEnvioSolicitacaoConfiguracoesRepNuvem:
          _msg = Resources.msgNENHUMA_RESPOSTA_APOS_CONFIG_GERAL;
          break;
      }
      this.EncerrarConexao();
      this.NotificarRecebimentoParaUsuario(_msg, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha);
    }

    private void NotificarRecebimentoParaUsuario(
      string _msg,
      EnumEstadoNotificacaoParaUsuario enumEstadoNotificacaoParaUsuario,
      EnumEstadoResultadoFinalProcesso enumEstadoResultadoFinalProcesso)
    {
      NotificarRecebimentoParaUsuarioEventArgs e = new NotificarRecebimentoParaUsuarioEventArgs(_msg, enumEstadoNotificacaoParaUsuario, enumEstadoResultadoFinalProcesso, this._empregadorId, this._rep.Local);
      if (this.OnNotificarRecebimentoUsuario == null)
        return;
      this.OnNotificarRecebimentoUsuario((object) this, e);
    }

    private new enum Estados
    {
      estEnvioSolicitacaoConfiguracoes,
      estEnvioSolicitacaoBarras20,
      estEnvioSolicitacaoConfiguracoesRepClient,
      estEnvioSolicitacaoConfiguracoesRepClientDNS,
      estEnvioSolicitacaoConfiguracoesRepNomeRep,
      estEnvioSolicitacaoConfiguracoesRepNuvem,
      estEnvioSolicitacaoConfiguracoesAjusteBiometricoNitgen,
      estEnvioSolicitacaoConfiguracoesAjusteBiometricoCama,
      estEnvioSolicitacaoConfiguracoesAjusteBiometricoSagem,
    }
  }
}
