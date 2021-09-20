// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorTemplatesBioRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading;
using TopData.Framework.Comunicacao;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorTemplatesBioRepPlus : TarefasBioAbstract
  {
    private RepBase _rep;
    private bool _chamadaSdk;
    private ConfiguracoesGerais _configEnt = new ConfiguracoesGerais();
    private int _modeloBioSdk;
    private List<UsuarioBio> _listaBioSdk = new List<UsuarioBio>();
    private Empregador _empregador;
    private Queue<MsgTcpAplicacaoSolicitaExclusaoUsuarioBioRepRepPlus> _queueMsgSolicitExclusaoUsuarioBio;
    private Queue<MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4KRepPlus> _queueMsgSolicitExclusaoUsuarioBio1_4K;
    private Queue<MsgTcpAplicacaoSolicitaInclusaoUsuarioBioRepRepPlus> _queueMsgSolicitInclusaoUsuarioBio;
    private Queue<MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4KRepPlus> _queueMsgSolicitInclusaoUsuarioBio1_4K;
    private Queue<MsgTcpAplicacaoSolicitaUsuarioBioRepRepPlus> _queueMsgSolicitUsuarioBio;
    private Queue<MsgTcpAplicacaoSolicitaUsuarioBio1_4KRepPlus> _queueMsgSolicitUsuarioBio1_4K;
    private List<MsgTcpAplicacaoRespostaUsuarioBio> _lstMsgRespostaUsuarioBio;
    private List<MsgTcpAplicacaoRespostaUsuarioBio1_4KRepPlus> _lstMsgRespostaUsuarioBio1_4K;
    private GerenciadorTemplatesBioRepPlus.Estados _estadoRep;
    private ProcessoTemplates _tipoProcessoTemplates;
    private MsgTcpAplicacaoRespostaPacoteUsuarioBio _msgRespostaPacoteTemplates;
    private MsgTcpAplicacaoRespostaPacoteUsuarioBio1_4KRepPlus _msgRespostaPacoteTemplates1_4K;
    private MsgTcpAplicacaoRespostaUsuarioBio _msgRespostaTemplateUsuario;
    private MsgTcpAplicacaoRespostaUsuarioBio1_4KRepPlus _msgRespostaTemplateUsuario1_4K;
    private bool _placaFim1_4K = true;
    public static GerenciadorTemplatesBioRepPlus _gerenciadorTemplatesBioRepPlus;

    public Empregador Empregador
    {
      get => this._empregador;
      set => this._empregador = value;
    }

    public static GerenciadorTemplatesBioRepPlus getInstance() => GerenciadorTemplatesBioRepPlus._gerenciadorTemplatesBioRepPlus != null ? GerenciadorTemplatesBioRepPlus._gerenciadorTemplatesBioRepPlus : new GerenciadorTemplatesBioRepPlus();

    public static GerenciadorTemplatesBioRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorTemplatesBioRepPlus._gerenciadorTemplatesBioRepPlus != null ? GerenciadorTemplatesBioRepPlus._gerenciadorTemplatesBioRepPlus : new GerenciadorTemplatesBioRepPlus(rep);
    }

    public static GerenciadorTemplatesBioRepPlus getInstance(
      RepBase rep,
      int modeloBioSdk)
    {
      return GerenciadorTemplatesBioRepPlus._gerenciadorTemplatesBioRepPlus != null ? GerenciadorTemplatesBioRepPlus._gerenciadorTemplatesBioRepPlus : new GerenciadorTemplatesBioRepPlus(rep, modeloBioSdk);
    }

    public GerenciadorTemplatesBioRepPlus()
    {
    }

    public GerenciadorTemplatesBioRepPlus(RepBase rep)
    {
      this._rep = rep;
      this._chamadaSdk = false;
      this._empregador = this._rep.Empregador.PesquisarEmpregadorDeUmREP(this._rep.RepId);
    }

    public GerenciadorTemplatesBioRepPlus(RepBase rep, int modeloBioSdk)
    {
      this._rep = rep;
      this._chamadaSdk = true;
      this._modeloBioSdk = modeloBioSdk;
      this.ListaUsuariosBio = new SortableBindingList<UsuarioBio>();
    }

    ~GerenciadorTemplatesBioRepPlus()
    {
    }

    public override void IniciarProcesso(ProcessoTemplates processoTemp)
    {
      ConfiguracoesGerais configuracoesGerais = new ConfiguracoesGerais();
      if (!this._chamadaSdk)
        this._configEnt = configuracoesGerais.PesquisarConfigGerais();
      this._tipoProcessoTemplates = processoTemp;
      this.Conectar(this._rep);
    }

    private void EnviarSolicitacaoInfo()
    {
      try
      {
        this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInfo;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaInformacaoBioRepPlus()
          {
            Info = (byte) 1,
            GrupoTemplates = (byte) 19,
            Leitor = (byte) 0
          }
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoInfoCAMA()
    {
      try
      {
        this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInfoCama;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaInformacaoBioRepPlus()
          {
            Info = (byte) 1,
            GrupoTemplates = (byte) 20,
            Leitor = (byte) 0
          }
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoPacoteTemplates1_4K(byte numPacote)
    {
      try
      {
        this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoPacoteTemplates1_4K;
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        MsgTcpAplicacaoSolicitaPacoteUsuarioBio1_4K_PequenoRepPlus obj = new MsgTcpAplicacaoSolicitaPacoteUsuarioBio1_4K_PequenoRepPlus();
        obj.NumPac = numPacote;
        obj.Leitor = (byte) 0;
        if (this._rep.ModeloFim == 0 || this._rep.ModeloFim == 5)
        {
          this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoPacoteTemplates1_4K;
          obj.Grupo = (byte) 19;
        }
        else
        {
          this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoPacoteTemplates1_4KCAMA;
          obj.Grupo = (byte) 20;
        }
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) obj;
        this.ClienteSocket.Enviar(envelope, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoExclusaoUsuarioBio1_4K()
    {
      try
      {
        this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoExclusaoUsuarioBio1_4K;
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4KRepPlus usuarioBio14KrepPlus = this._queueMsgSolicitExclusaoUsuarioBio1_4K.Peek();
        if (this._rep.ModeloFim == 0 || this._rep.ModeloFim == 5)
        {
          this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoExclusaoUsuarioBio1_4K;
          usuarioBio14KrepPlus.Grupo = (byte) 19;
          usuarioBio14KrepPlus.CMD = this._rep.VersaoFW == 3 || this._rep.VersaoFW == 0 || this._rep.VersaoFW == 1 || this._rep.VersaoFW == 0 || this._rep.VersaoFW == 4 ? (byte) 31 : (byte) 21;
        }
        else
        {
          this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoExclusaoUsuarioBio1_4KCAMA;
          usuarioBio14KrepPlus.Grupo = (byte) 20;
        }
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) usuarioBio14KrepPlus;
        this.ClienteSocket.Enviar(envelope, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoUsuarioBio1_4K()
    {
      try
      {
        this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoTemplateUsuarioBio1_4K;
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        MsgTcpAplicacaoSolicitaUsuarioBio1_4KRepPlus usuarioBio14KrepPlus = this._queueMsgSolicitUsuarioBio1_4K.Peek();
        if (this._rep.ModeloFim == 0 || this._rep.ModeloFim == 5)
        {
          this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoTemplateUsuarioBio1_4K;
          usuarioBio14KrepPlus.Grupo = (byte) 19;
          usuarioBio14KrepPlus.CMD = this._rep.VersaoFW == 3 || this._rep.VersaoFW == 1 || this._rep.VersaoFW == 0 || this._rep.VersaoFW == 4 ? (byte) 30 : (byte) 20;
        }
        else
        {
          this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoTemplateUsuarioBio1_4KCAMA;
          usuarioBio14KrepPlus.Grupo = (byte) 20;
        }
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) usuarioBio14KrepPlus;
        this.ClienteSocket.Enviar(envelope, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoUsuarioBio1_4KCama()
    {
      try
      {
        this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoTemplateUsuarioBio1_4K;
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        MsgTcpAplicacaoSolicitaUsuarioBio1_4KRepPlus usuarioBio14KrepPlus = this._queueMsgSolicitUsuarioBio1_4K.Peek();
        this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoTemplateUsuarioBio1_4KCAMA;
        usuarioBio14KrepPlus.Grupo = (byte) 20;
        usuarioBio14KrepPlus.CMD = (byte) 30;
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) usuarioBio14KrepPlus;
        this.ClienteSocket.Enviar(envelope, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoInclusaoUsuarioBio1_4K()
    {
      try
      {
        this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInclusaoUsuarioBio1_4K;
        DateTime now = DateTime.Now;
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4KRepPlus usuarioBio14KrepPlus = this._queueMsgSolicitInclusaoUsuarioBio1_4K.Peek();
        if (this._rep.ModeloFim == 0 || this._rep.ModeloFim == 5)
        {
          this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInclusaoUsuarioBio1_4K;
          usuarioBio14KrepPlus.Grupo = (byte) 19;
        }
        else
        {
          this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInclusaoUsuarioBio1_4KCAMA;
          usuarioBio14KrepPlus.Grupo = (byte) 20;
        }
        usuarioBio14KrepPlus.CentenaAno = (byte) 20;
        usuarioBio14KrepPlus.RestoAno = (byte) (DateTime.Now.Year % 100);
        usuarioBio14KrepPlus.Mes = Convert.ToByte(now.Month);
        usuarioBio14KrepPlus.Dia = Convert.ToByte(now.Day);
        usuarioBio14KrepPlus.Hora = Convert.ToByte(now.Hour);
        usuarioBio14KrepPlus.Minuto = Convert.ToByte(now.Minute);
        usuarioBio14KrepPlus.Segundo = Convert.ToByte(now.Second);
        usuarioBio14KrepPlus.Reservado = (byte) 0;
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) usuarioBio14KrepPlus;
        this.ClienteSocket.Enviar(envelope, true);
      }
      catch
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoInclusaoUsuarioBio1_4KSegundaDigital()
    {
      try
      {
        this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInclusaoUsuarioBio1_4K;
        DateTime now = DateTime.Now;
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4KRepPlus usuarioBio14KrepPlus = this._queueMsgSolicitInclusaoUsuarioBio1_4K.Peek();
        this._estadoRep = GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInclusaoUsuarioBio1_4KCAMA;
        usuarioBio14KrepPlus.Grupo = (byte) 20;
        usuarioBio14KrepPlus.CMD = (byte) 32;
        usuarioBio14KrepPlus.CentenaAno = (byte) 20;
        usuarioBio14KrepPlus.RestoAno = (byte) (DateTime.Now.Year % 100);
        usuarioBio14KrepPlus.Mes = Convert.ToByte(now.Month);
        usuarioBio14KrepPlus.Dia = Convert.ToByte(now.Day);
        usuarioBio14KrepPlus.Hora = Convert.ToByte(now.Hour);
        usuarioBio14KrepPlus.Minuto = Convert.ToByte(now.Minute);
        usuarioBio14KrepPlus.Segundo = Convert.ToByte(now.Second);
        usuarioBio14KrepPlus.Reservado = (byte) 0;
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) usuarioBio14KrepPlus;
        this.ClienteSocket.Enviar(envelope, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      NotificarProgressBarEventArgs eNotificaProgress = (NotificarProgressBarEventArgs) null;
      switch (this._estadoRep)
      {
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInfo:
          if (envelope.Grp != (byte) 19 || envelope.Cmd != (byte) 100)
            break;
          if (this.AbrirMsgRespostaInfo(envelope).Resultado != (byte) 1)
          {
            this.EnviarSolicitacaoInfo();
            break;
          }
          switch (this._tipoProcessoTemplates)
          {
            case ProcessoTemplates.recuperarTemplates:
              this.ListaUsuariosBio = new SortableBindingList<UsuarioBio>();
              this.EnviarSolicitacaoPacoteTemplates1_4K((byte) 0);
              return;
            case ProcessoTemplates.excluirTemplates:
              this.NotificarParaUsuario(Resources.msgENVIO_EXCLUSAO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
              this.EnviarSolicitacaoExclusaoUsuarioBio1_4K();
              return;
            case ProcessoTemplates.importarTemplates:
              this.NotificarParaUsuario(Resources.msgENVIO_SOLICITACAO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
              this._lstMsgRespostaUsuarioBio1_4K = new List<MsgTcpAplicacaoRespostaUsuarioBio1_4KRepPlus>();
              this.EnviarSolicitacaoUsuarioBio1_4K();
              return;
            case ProcessoTemplates.exportarTemplates:
              this.NotificarParaUsuario(Resources.msgENVIO_INCLUSAO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
              this.EnviarSolicitacaoInclusaoUsuarioBio1_4K();
              return;
            default:
              this.EncerrarConexao();
              this.NotificarParaUsuario(string.Empty, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
              return;
          }
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInfoCama:
          if (envelope.Grp != (byte) 20 || envelope.Cmd != (byte) 100)
            break;
          if (this.AbrirMsgRespostaInfoCAMA(envelope).Resultado != (byte) 1)
          {
            this.EnviarSolicitacaoInfoCAMA();
            break;
          }
          switch (this._tipoProcessoTemplates)
          {
            case ProcessoTemplates.recuperarTemplates:
              this.ListaUsuariosBio = new SortableBindingList<UsuarioBio>();
              this.EnviarSolicitacaoPacoteTemplates1_4K((byte) 0);
              return;
            case ProcessoTemplates.excluirTemplates:
              this.NotificarParaUsuario(Resources.msgENVIO_EXCLUSAO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
              this.EnviarSolicitacaoExclusaoUsuarioBio1_4K();
              return;
            case ProcessoTemplates.importarTemplates:
              this.NotificarParaUsuario(Resources.msgENVIO_SOLICITACAO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
              this._lstMsgRespostaUsuarioBio1_4K = new List<MsgTcpAplicacaoRespostaUsuarioBio1_4KRepPlus>();
              this.EnviarSolicitacaoUsuarioBio1_4K();
              return;
            case ProcessoTemplates.exportarTemplates:
              this.NotificarParaUsuario(Resources.msgENVIO_INCLUSAO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
              this.EnviarSolicitacaoInclusaoUsuarioBio1_4K();
              return;
            default:
              this.EncerrarConexao();
              this.NotificarParaUsuario(string.Empty, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
              return;
          }
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoPacoteTemplates1_4K:
          if (envelope.Grp != (byte) 19 || envelope.Cmd != (byte) 110 && envelope.Cmd != (byte) 111)
            break;
          this.AbrirMsgRespostaSolicitacaoPacoteTemplates1_4K(envelope);
          if (this._msgRespostaPacoteTemplates1_4K.Resultado == (byte) 1)
          {
            if (this._msgRespostaPacoteTemplates1_4K.NumUsrCad > (ushort) 0)
            {
              byte numPacote = (byte) ((uint) this._msgRespostaPacoteTemplates1_4K.NumPac + 1U);
              this.AssociarEmpregadoBio1_4K();
              if ((int) numPacote <= (int) this._msgRespostaPacoteTemplates1_4K.TotPac)
              {
                this.EnviarSolicitacaoPacoteTemplates1_4K(numPacote);
                break;
              }
              this.EncerrarConexao();
              this.NotificarParaUsuario(Resources.msgPROCESSO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              break;
            }
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgNENHUM_USUARIO_BIO_CADASTRADOS, EnumEstadoNotificacaoParaUsuario.nenhumEstado, EnumEstadoResultadoFinalProcesso.nenhumUsuarioBioCadastrados, this._rep.RepId, this._rep.Local);
            break;
          }
          string menssagem1 = this.ExtrairRespostaBio(this._msgRespostaPacoteTemplates1_4K.Resultado);
          if (!Resources.msgBIO_OCUPADO.Equals(menssagem1))
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(menssagem1, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            break;
          }
          this.EnviarSolicitacaoPacoteTemplates1_4K(this._msgRespostaPacoteTemplates1_4K.NumPac);
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoExclusaoUsuarioBio1_4K:
          if (envelope.Grp != (byte) 19 || envelope.Cmd != (byte) 121)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 1 || envelope.MsgAplicacaoEmBytes[2] == (byte) 5)
          {
            if (this._chamadaSdk)
              this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
            if (this._queueMsgSolicitExclusaoUsuarioBio1_4K.Count > 1)
            {
              this._queueMsgSolicitExclusaoUsuarioBio1_4K.Dequeue();
              this.EnviarSolicitacaoExclusaoUsuarioBio1_4K();
              this.RaiseProgressBar(eNotificaProgress);
              break;
            }
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgEXCLUSAO_TEMPLATE_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            break;
          }
          string menssagem2 = this.ExtrairRespostaBio(envelope.MsgAplicacaoEmBytes[2]);
          if (!Resources.msgBIO_OCUPADO.Equals(menssagem2))
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(menssagem2, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            break;
          }
          this.EnviarSolicitacaoExclusaoUsuarioBio1_4K();
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoTemplateUsuarioBio1_4K:
          if (envelope.Grp != (byte) 19 || envelope.Cmd != (byte) 120)
            break;
          this.AbrirMsgRespostaSolicitacaoTemplateUsuario1_4K(envelope);
          if (this._msgRespostaTemplateUsuario1_4K.Resultado == (byte) 1 || this._msgRespostaTemplateUsuario1_4K.Resultado == (byte) 5)
          {
            long result = long.MinValue;
            if (!this._msgRespostaTemplateUsuario1_4K.NumUsuario.Equals("ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff"))
            {
              if (!long.TryParse(this._msgRespostaTemplateUsuario1_4K.NumUsuario, out result) && this._queueMsgSolicitUsuarioBio1_4K.Count > 1)
              {
                this.EnviarSolicitacaoUsuarioBio1_4K();
                break;
              }
              this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
              if (this._lstMsgRespostaUsuarioBio1_4K.Count > 0)
                this.GravarTemplates1_4K(this._lstMsgRespostaUsuarioBio1_4K[this._lstMsgRespostaUsuarioBio1_4K.Count - 1]);
            }
            if (this._queueMsgSolicitUsuarioBio1_4K.Count > 1)
            {
              this._queueMsgSolicitUsuarioBio1_4K.Dequeue();
              this.EnviarSolicitacaoUsuarioBio1_4K();
              this.RaiseProgressBar(new NotificarProgressBarEventArgs(this.TotTemplatesParaProcessar));
              break;
            }
            if (this._lstMsgRespostaUsuarioBio1_4K.Count > 0)
            {
              this.EncerrarConexao();
              this.NotificarParaUsuario(Resources.msgPROCESSO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              break;
            }
            this.EncerrarConexao();
            if (this._chamadaSdk)
            {
              this.NotificarParaUsuario(Resources.msgNAO_ENCONTRADO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.nenhumEstado, EnumEstadoResultadoFinalProcesso.nenhumUsuarioBioCadastrados, this._rep.RepId, this._rep.Local);
              break;
            }
            this.NotificarParaUsuario(Resources.msgNAO_ENCONTRADO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.nenhumEstado, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
            break;
          }
          string menssagem3 = this.ExtrairRespostaBio(this._msgRespostaTemplateUsuario1_4K.Resultado);
          if (!Resources.msgBIO_OCUPADO.Equals(menssagem3))
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(menssagem3, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            break;
          }
          this.EnviarSolicitacaoUsuarioBio1_4K();
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInclusaoUsuarioBio1_4K:
          if (envelope.Grp != (byte) 19 || envelope.Cmd != (byte) 122)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 1 || envelope.MsgAplicacaoEmBytes[2] == (byte) 5 || envelope.MsgAplicacaoEmBytes[2] == (byte) 4)
          {
            this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
            if (this._queueMsgSolicitInclusaoUsuarioBio1_4K.Count > 1)
            {
              this._queueMsgSolicitInclusaoUsuarioBio1_4K.Dequeue();
              Thread.Sleep(30);
              this.EnviarSolicitacaoInclusaoUsuarioBio1_4K();
              this.RaiseProgressBar(new NotificarProgressBarEventArgs(this.TotTemplatesParaProcessar));
              break;
            }
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgPROCESSO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            break;
          }
          if (envelope.MsgAplicacaoEmBytes[2] > (byte) 22)
          {
            this.EnviarSolicitacaoInclusaoUsuarioBio1_4K();
            break;
          }
          string menssagem4 = envelope.MsgAplicacaoEmBytes[2] != (byte) 22 ? this.ExtrairRespostaBio(envelope.MsgAplicacaoEmBytes[2]) : Resources.msgBIO_TEMPLATE_REPITIDA;
          if (!Resources.msgBIO_OCUPADO.Equals(menssagem4))
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(menssagem4, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            break;
          }
          this.EnviarSolicitacaoInclusaoUsuarioBio1_4K();
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoPacoteTemplates1_4KCAMA:
          if (envelope.Grp != (byte) 20 || envelope.Cmd != (byte) 110 && envelope.Cmd != (byte) 111)
            break;
          this.AbrirMsgRespostaSolicitacaoPacoteTemplates1_4K(envelope);
          if (this._msgRespostaPacoteTemplates1_4K.Resultado == (byte) 1)
          {
            if (this._msgRespostaPacoteTemplates1_4K.NumUsrCad > (ushort) 0)
            {
              byte numPacote = (byte) ((uint) this._msgRespostaPacoteTemplates1_4K.NumPac + 1U);
              this.AssociarEmpregadoBio1_4K();
              if ((int) numPacote <= (int) this._msgRespostaPacoteTemplates1_4K.TotPac)
              {
                this.EnviarSolicitacaoPacoteTemplates1_4K(numPacote);
                break;
              }
              this.EncerrarConexao();
              this.NotificarParaUsuario(Resources.msgPROCESSO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              break;
            }
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgNENHUM_USUARIO_BIO_CADASTRADOS, EnumEstadoNotificacaoParaUsuario.nenhumEstado, EnumEstadoResultadoFinalProcesso.nenhumUsuarioBioCadastrados, this._rep.RepId, this._rep.Local);
            break;
          }
          string menssagem5 = this.ExtrairRespostaBio(this._msgRespostaPacoteTemplates1_4K.Resultado);
          if (!Resources.msgBIO_OCUPADO.Equals(menssagem5))
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(menssagem5, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            break;
          }
          this.EnviarSolicitacaoPacoteTemplates1_4K(this._msgRespostaPacoteTemplates1_4K.NumPac);
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoExclusaoUsuarioBio1_4KCAMA:
          if (envelope.Grp != (byte) 20 || envelope.Cmd != (byte) 121 && envelope.Cmd != (byte) 131)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 1 || envelope.MsgAplicacaoEmBytes[2] == (byte) 5)
          {
            if (this._chamadaSdk)
              this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
            if (this._queueMsgSolicitExclusaoUsuarioBio1_4K.Count > 1)
            {
              this._queueMsgSolicitExclusaoUsuarioBio1_4K.Dequeue();
              this.EnviarSolicitacaoExclusaoUsuarioBio1_4K();
              this.RaiseProgressBar(new NotificarProgressBarEventArgs(this.TotTemplatesParaProcessar));
              break;
            }
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgEXCLUSAO_TEMPLATE_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            break;
          }
          string menssagem6 = this.ExtrairRespostaBio(envelope.MsgAplicacaoEmBytes[2]);
          if (!Resources.msgBIO_OCUPADO.Equals(menssagem6))
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(menssagem6, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            break;
          }
          this.EnviarSolicitacaoExclusaoUsuarioBio1_4K();
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoTemplateUsuarioBio1_4KCAMA:
          if (envelope.Grp != (byte) 20 || envelope.Cmd != (byte) 120 && envelope.Cmd != (byte) 130)
            break;
          if (envelope.Cmd == (byte) 120)
            this.AbrirMsgRespostaSolicitacaoTemplateUsuario1_4K(envelope);
          else
            this.AbrirMsgRespostaSolicitacaoTemplateUsuario1_4KSegundoTemplate(envelope);
          if (this._msgRespostaTemplateUsuario1_4K.Resultado == (byte) 1 || this._msgRespostaTemplateUsuario1_4K.Resultado == (byte) 5)
          {
            long result = long.MinValue;
            if (!this._msgRespostaTemplateUsuario1_4K.NumUsuario.Equals("ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff"))
            {
              if (!long.TryParse(this._msgRespostaTemplateUsuario1_4K.NumUsuario, out result) && this._queueMsgSolicitUsuarioBio1_4K.Count > 1)
              {
                this.EnviarSolicitacaoUsuarioBio1_4K();
                break;
              }
              this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
              if (envelope.Cmd == (byte) 120 && envelope.MsgAplicacaoEmBytes[39] == (byte) 1)
              {
                this.EnviarSolicitacaoUsuarioBio1_4KCama();
                break;
              }
              this.GravarTemplates1_4K(this._lstMsgRespostaUsuarioBio1_4K[this._lstMsgRespostaUsuarioBio1_4K.Count - 1]);
            }
            if (this._queueMsgSolicitUsuarioBio1_4K.Count > 1)
            {
              this._queueMsgSolicitUsuarioBio1_4K.Dequeue();
              this.EnviarSolicitacaoUsuarioBio1_4K();
              this.RaiseProgressBar(new NotificarProgressBarEventArgs(this.TotTemplatesParaProcessar));
              break;
            }
            if (this._lstMsgRespostaUsuarioBio1_4K.Count > 0)
            {
              this.EncerrarConexao();
              this.NotificarParaUsuario(Resources.msgPROCESSO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              break;
            }
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgNAO_ENCONTRADO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.nenhumEstado, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
            break;
          }
          string menssagem7 = this.ExtrairRespostaBio(this._msgRespostaTemplateUsuario1_4K.Resultado);
          if (!Resources.msgBIO_OCUPADO.Equals(menssagem7))
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(menssagem7, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            break;
          }
          this.EnviarSolicitacaoUsuarioBio1_4K();
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInclusaoUsuarioBio1_4KCAMA:
          if (envelope.Grp != (byte) 20 || envelope.Cmd != (byte) 122 && envelope.Cmd != (byte) 132)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 1 || envelope.MsgAplicacaoEmBytes[2] == (byte) 5 || envelope.MsgAplicacaoEmBytes[2] == (byte) 4)
          {
            this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
            if (this._queueMsgSolicitInclusaoUsuarioBio1_4K.Count > 1)
            {
              this._queueMsgSolicitInclusaoUsuarioBio1_4K.Dequeue();
              Thread.Sleep(30);
              this.EnviarSolicitacaoInclusaoUsuarioBio1_4K();
              this.RaiseProgressBar(new NotificarProgressBarEventArgs(this.TotTemplatesParaProcessar));
              break;
            }
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgPROCESSO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            break;
          }
          if (envelope.MsgAplicacaoEmBytes[2] > (byte) 22)
          {
            this.EnviarSolicitacaoInclusaoUsuarioBio1_4K();
            break;
          }
          string menssagem8 = envelope.MsgAplicacaoEmBytes[2] != (byte) 22 ? this.ExtrairRespostaBio(envelope.MsgAplicacaoEmBytes[2]) : Resources.msgBIO_TEMPLATE_REPITIDA;
          if (!Resources.msgBIO_OCUPADO.Equals(menssagem8))
          {
            this.EncerrarConexao();
            if (envelope.MsgAplicacaoEmBytes[2] == (byte) 6)
            {
              this.NotificarParaUsuario(envelope.MsgAplicacaoEmBytes[2], menssagem8, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              break;
            }
            this.NotificarParaUsuario(menssagem8, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            break;
          }
          this.EnviarSolicitacaoInclusaoUsuarioBio1_4K();
          break;
      }
    }

    private string ExtrairRespostaBio(byte respostaBio)
    {
      string str;
      switch (respostaBio)
      {
        case 0:
          str = Resources.msgBIO_ERRO_COMUNICACAO;
          break;
        case 1:
          str = Resources.msgBIO_PROCESSADO_SUCESSO;
          break;
        case 2:
          str = Resources.msgBIO_FALHA_COMANDO;
          break;
        case 3:
          str = Resources.msgBIO_NAO_MODO_MASTER;
          break;
        case 4:
          str = Resources.msgBIO_USUARIO_JA_CADASTRADO;
          break;
        case 5:
          str = Resources.msgBIO_USUARIO_NAO_CADASTRADO;
          break;
        case 6:
          str = Resources.msgBIO_BASE_CHEIA;
          break;
        case 7:
          str = Resources.msgBIO_TIME_OUT_COMUNICACAO;
          break;
        case 9:
          str = Resources.msgBIO_PARAMETRO_INVALIDO;
          break;
        case 11:
          str = Resources.msgBIO_OCUPADO;
          break;
        case 22:
          str = Resources.msgBIO_TEMPLATE_INVALIDO;
          break;
        default:
          str = Resources.msgBIO_ERRO_COMUNICACAO;
          break;
      }
      return str;
    }

    private MsgTcpAplicacaoRespostaInformacaoBioRepPlusCAMA AbrirMsgRespostaInfoCAMA(
      Envelope envelope)
    {
      MsgTcpAplicacaoRespostaInformacaoBioRepPlusCAMA informacaoBioRepPlusCama = new MsgTcpAplicacaoRespostaInformacaoBioRepPlusCAMA();
      byte[] aplicacaoEmBytes = envelope.MsgAplicacaoEmBytes;
      informacaoBioRepPlusCama.Resultado = Convert.ToByte(aplicacaoEmBytes[2].ToString("X"));
      if (informacaoBioRepPlusCama.Resultado == (byte) 1)
      {
        byte[] numArray = new byte[14];
        Array.Copy((Array) aplicacaoEmBytes, 0, (Array) numArray, 0, 14);
        foreach (byte num in numArray)
          informacaoBioRepPlusCama.Modelo += num.ToString("X");
        informacaoBioRepPlusCama.Modelo = informacaoBioRepPlusCama.Modelo.Trim();
        this._placaFim1_4K = true;
        this.RaiseNotificarModeloBio(new NotificarModeloBIOEventArgs(2, this._rep.RepId));
      }
      return informacaoBioRepPlusCama;
    }

    private MsgTcpAplicacaoRespostaInformacaoBioRepPlus AbrirMsgRespostaInfo(
      Envelope envelope)
    {
      MsgTcpAplicacaoRespostaInformacaoBioRepPlus informacaoBioRepPlus = new MsgTcpAplicacaoRespostaInformacaoBioRepPlus();
      byte[] aplicacaoEmBytes = envelope.MsgAplicacaoEmBytes;
      informacaoBioRepPlus.Resultado = aplicacaoEmBytes[2];
      if (informacaoBioRepPlus.Resultado == (byte) 1)
      {
        informacaoBioRepPlus.Modelo = Convert.ToByte(aplicacaoEmBytes[4].ToString("X"));
        if (informacaoBioRepPlus.Modelo == (byte) 30 || informacaoBioRepPlus.Modelo == (byte) 40)
        {
          this._placaFim1_4K = false;
          this.RaiseNotificarModeloBio(new NotificarModeloBIOEventArgs(1, this._rep.RepId));
        }
        else if (informacaoBioRepPlus.Modelo == (byte) 53)
        {
          this._placaFim1_4K = false;
          this.RaiseNotificarModeloBio(new NotificarModeloBIOEventArgs(4, this._rep.RepId));
        }
        else if (informacaoBioRepPlus.Modelo == (byte) 60)
        {
          this._placaFim1_4K = true;
          this.RaiseNotificarModeloBio(new NotificarModeloBIOEventArgs(5, this._rep.RepId));
        }
        else
        {
          this._placaFim1_4K = true;
          this.RaiseNotificarModeloBio(new NotificarModeloBIOEventArgs(2, this._rep.RepId));
        }
      }
      return informacaoBioRepPlus;
    }

    private void AbrirMsgRespostaSolicitacaoPacoteTemplates(Envelope envelope)
    {
      byte[] numArray1 = new byte[2];
      byte[] numArray2 = new byte[2];
      this._msgRespostaPacoteTemplates = new MsgTcpAplicacaoRespostaPacoteUsuarioBio();
      byte[] aplicacaoEmBytes = envelope.MsgAplicacaoEmBytes;
      this._msgRespostaPacoteTemplates.Resultado = aplicacaoEmBytes[2];
      this._msgRespostaPacoteTemplates.NumPac = aplicacaoEmBytes[4];
      this._msgRespostaPacoteTemplates.TotPac = aplicacaoEmBytes[5];
      numArray1[0] = aplicacaoEmBytes[6];
      numArray1[1] = aplicacaoEmBytes[7];
      Array.Reverse((Array) numArray1);
      ushort uint16_1 = BitConverter.ToUInt16(numArray1, 0);
      this._msgRespostaPacoteTemplates.NumUsrCad = uint16_1;
      numArray2[0] = aplicacaoEmBytes[8];
      numArray2[1] = aplicacaoEmBytes[9];
      Array.Reverse((Array) numArray2);
      ushort uint16_2 = BitConverter.ToUInt16(numArray2, 0);
      this._msgRespostaPacoteTemplates.IdSize = uint16_2;
      if (uint16_1 == (ushort) 0)
        return;
      int num1 = (int) uint16_1;
      byte[] numArray3 = new byte[num1 * (int) uint16_2];
      Array.Copy((Array) aplicacaoEmBytes, 10, (Array) numArray3, 0, numArray3.Length);
      for (int index = 0; index < num1; ++index)
      {
        UsuarioBio usuarioBio = new UsuarioBio();
        StringBuilder stringBuilder = new StringBuilder();
        byte[] numArray4 = new byte[(int) uint16_2];
        Array.Copy((Array) numArray3, index * (int) uint16_2, (Array) numArray4, 0, (int) uint16_2);
        int num2 = 0;
        foreach (byte num3 in numArray4)
        {
          if (num2 != 8)
          {
            int num4 = (int) num3;
            --num4;
            stringBuilder.Append(num4.ToString("x").PadLeft(2, '0'));
            ++num2;
          }
          else
            break;
        }
        usuarioBio.IdBiometria = stringBuilder.ToString();
        this._msgRespostaPacoteTemplates.Add(usuarioBio);
      }
    }

    private void AbrirMsgRespostaSolicitacaoPacoteTemplates1_4K(Envelope envelope)
    {
      byte[] numArray1 = new byte[2];
      byte[] numArray2 = new byte[2];
      this._msgRespostaPacoteTemplates1_4K = new MsgTcpAplicacaoRespostaPacoteUsuarioBio1_4KRepPlus();
      byte[] aplicacaoEmBytes = envelope.MsgAplicacaoEmBytes;
      this._msgRespostaPacoteTemplates1_4K.Resultado = aplicacaoEmBytes[2];
      this._msgRespostaPacoteTemplates1_4K.NumPac = aplicacaoEmBytes[4];
      this._msgRespostaPacoteTemplates1_4K.TotPac = aplicacaoEmBytes[5];
      numArray1[0] = aplicacaoEmBytes[6];
      numArray1[1] = aplicacaoEmBytes[7];
      Array.Reverse((Array) numArray1);
      ushort uint16_1 = BitConverter.ToUInt16(numArray1, 0);
      this._msgRespostaPacoteTemplates1_4K.NumUsrCad = uint16_1;
      numArray2[0] = aplicacaoEmBytes[8];
      numArray2[1] = aplicacaoEmBytes[9];
      Array.Reverse((Array) numArray2);
      ushort uint16_2 = BitConverter.ToUInt16(numArray2, 0);
      this._msgRespostaPacoteTemplates1_4K.IdSize = uint16_2;
      if (uint16_1 == (ushort) 0)
        return;
      int num1 = (int) uint16_1;
      byte[] numArray3 = new byte[num1 * (int) uint16_2];
      Array.Copy((Array) aplicacaoEmBytes, 10, (Array) numArray3, 0, numArray3.Length);
      for (int index = 0; index < num1; ++index)
      {
        UsuarioBio usuarioBio = new UsuarioBio();
        StringBuilder stringBuilder = new StringBuilder();
        byte[] numArray4 = new byte[(int) uint16_2];
        Array.Copy((Array) numArray3, index * (int) uint16_2, (Array) numArray4, 0, (int) uint16_2);
        int num2 = 0;
        foreach (byte num3 in numArray4)
        {
          if (num2 != 8)
          {
            int num4 = (int) num3;
            --num4;
            stringBuilder.Append(num4.ToString("x").PadLeft(2, '0'));
            ++num2;
          }
          else
            break;
        }
        usuarioBio.IdBiometria = stringBuilder.ToString();
        this._msgRespostaPacoteTemplates1_4K.Add(usuarioBio);
      }
    }

    private void AbrirMsgRespostaSolicitacaoTemplateUsuario1_4K(Envelope envelope)
    {
      this._msgRespostaTemplateUsuario1_4K = new MsgTcpAplicacaoRespostaUsuarioBio1_4KRepPlus(this._rep.ModeloFim);
      byte[] aplicacaoEmBytes = envelope.MsgAplicacaoEmBytes;
      this._msgRespostaTemplateUsuario1_4K.Resultado = aplicacaoEmBytes[2];
      byte[] numArray = new byte[11];
      Array.Copy((Array) aplicacaoEmBytes, 9, (Array) numArray, 0, numArray.Length);
      StringBuilder stringBuilder = new StringBuilder();
      int num1 = 0;
      foreach (int num2 in numArray)
      {
        if (num1 != 8)
        {
          int num3 = num2 - 1;
          stringBuilder.Append(num3.ToString("x").PadLeft(2, '0'));
          ++num1;
        }
        else
          break;
      }
      this._msgRespostaTemplateUsuario1_4K.NumUsuario = stringBuilder.ToString();
      if (this._msgRespostaTemplateUsuario1_4K.Resultado != (byte) 1)
        return;
      long result = long.MinValue;
      if (!long.TryParse(this._msgRespostaTemplateUsuario1_4K.NumUsuario, out result))
        return;
      this._msgRespostaTemplateUsuario1_4K.TipoUsuario = aplicacaoEmBytes[9];
      if (this._rep.ModeloFim == 0 || this._rep.ModeloFim == 5)
      {
        Array.Copy((Array) aplicacaoEmBytes, 20, (Array) this._msgRespostaTemplateUsuario1_4K.SenhaUsuario, 0, this._msgRespostaTemplateUsuario1_4K.SenhaUsuario.Length);
        Array.Copy((Array) aplicacaoEmBytes, 72, (Array) this._msgRespostaTemplateUsuario1_4K.Template1, 0, this._msgRespostaTemplateUsuario1_4K.Template1.Length);
        if (this._rep.VersaoFW == 3 || this._rep.VersaoFW == 1 || this._rep.VersaoFW == 0 || this._rep.VersaoFW == 4)
          Array.Copy((Array) aplicacaoEmBytes, 476, (Array) this._msgRespostaTemplateUsuario1_4K.Template2, 0, this._msgRespostaTemplateUsuario1_4K.Template2.Length);
      }
      else
      {
        this._msgRespostaTemplateUsuario1_4K.SenhaUsuario = new byte[16];
        Array.Copy((Array) aplicacaoEmBytes, 76, (Array) this._msgRespostaTemplateUsuario1_4K.Template1, 0, this._msgRespostaTemplateUsuario1_4K.Template1.Length);
      }
      this._lstMsgRespostaUsuarioBio1_4K.Add(this._msgRespostaTemplateUsuario1_4K);
    }

    private void AbrirMsgRespostaSolicitacaoTemplateUsuario1_4KSegundoTemplate(Envelope envelope)
    {
      this._msgRespostaTemplateUsuario1_4K = new MsgTcpAplicacaoRespostaUsuarioBio1_4KRepPlus(this._rep.ModeloFim);
      byte[] aplicacaoEmBytes = envelope.MsgAplicacaoEmBytes;
      this._msgRespostaTemplateUsuario1_4K.Resultado = aplicacaoEmBytes[2];
      byte[] numArray = new byte[11];
      Array.Copy((Array) aplicacaoEmBytes, 9, (Array) numArray, 0, numArray.Length);
      StringBuilder stringBuilder = new StringBuilder();
      int num1 = 0;
      foreach (int num2 in numArray)
      {
        if (num1 != 8)
        {
          int num3 = num2 - 1;
          stringBuilder.Append(num3.ToString("x").PadLeft(2, '0'));
          ++num1;
        }
        else
          break;
      }
      this._msgRespostaTemplateUsuario1_4K.NumUsuario = stringBuilder.ToString();
      if (this._msgRespostaTemplateUsuario1_4K.Resultado != (byte) 1)
        return;
      long result = long.MinValue;
      if (long.TryParse(this._msgRespostaTemplateUsuario1_4K.NumUsuario, out result))
      {
        this._msgRespostaTemplateUsuario1_4K.TipoUsuario = aplicacaoEmBytes[9];
        this._msgRespostaTemplateUsuario1_4K.SenhaUsuario = new byte[16];
        Array.Copy((Array) aplicacaoEmBytes, 76, (Array) this._msgRespostaTemplateUsuario1_4K.Template2, 0, this._msgRespostaTemplateUsuario1_4K.Template1.Length);
      }
      this._lstMsgRespostaUsuarioBio1_4K.Find((Predicate<MsgTcpAplicacaoRespostaUsuarioBio1_4KRepPlus>) (a => a.NumUsuario == this._msgRespostaTemplateUsuario1_4K.NumUsuario)).Template2 = this._msgRespostaTemplateUsuario1_4K.Template2;
    }

    private void AssociarEmpregadoBio1_4K()
    {
      List<UsuarioBio> lstUsuariosBio = this._msgRespostaPacoteTemplates1_4K.LstUsuariosBio;
      if (this._chamadaSdk)
      {
        foreach (UsuarioBio usuarioBio in lstUsuariosBio)
          this.ListaUsuariosBio.Add(new UsuarioBio()
          {
            IdBiometria = ulong.Parse(usuarioBio.IdBiometria),
            Pis = ulong.Parse(usuarioBio.IdBiometria).ToString()
          });
      }
      else
      {
        try
        {
          foreach (UsuarioBio usuarioBio in lstUsuariosBio)
            this.ListaUsuariosBio.Add(new UsuarioBio()
            {
              IdBiometria = ulong.Parse(usuarioBio.IdBiometria),
              Pis = usuarioBio.IdBiometria.Substring(usuarioBio.IdBiometria.Length - 12, 12),
              Nome = Resources.msgNOME_NAO_CADASTRADO
            });
        }
        catch (Exception ex)
        {
        }
      }
    }

    private byte[] AbrirTemplateEmBytes(string template)
    {
      byte[] numArray = new byte[400];
      int index1 = 0;
      int length = template.Length;
      for (int index2 = 0; index2 < length; index2 += 2)
      {
        string s = template[index2].ToString() + template[index2 + 1].ToString();
        numArray[index1] = (byte) int.Parse(s, NumberStyles.HexNumber);
        ++index1;
      }
      return numArray;
    }

    private byte[] AbrirTemplateEmBytes1_4K(string template)
    {
      byte[] numArray = new byte[404];
      int index1 = 0;
      int length = template.Length;
      if (this._rep.ModeloFim == 1)
        numArray = new byte[498];
      for (int index2 = 0; index2 < length; index2 += 2)
      {
        string s = template[index2].ToString() + template[index2 + 1].ToString();
        numArray[index1] = (byte) int.Parse(s, NumberStyles.HexNumber);
        ++index1;
      }
      return numArray;
    }

    private byte[] AbrirSenhaEmBytes(string senha)
    {
      byte[] numArray = new byte[16];
      int index1 = 0;
      if (this._chamadaSdk)
        senha = "546f7053656372657400000000000000";
      int length = senha.Length;
      for (int index2 = 0; index2 < length; index2 += 2)
      {
        string s = senha[index2].ToString() + senha[index2 + 1].ToString();
        numArray[index1] = (byte) int.Parse(s, NumberStyles.HexNumber);
        ++index1;
      }
      return numArray;
    }

    private byte[] AbrirPisUsuarioEmBytes(string PisUsuario)
    {
      byte[] numArray = new byte[11];
      int index1 = 0;
      int length = PisUsuario.Length;
      for (int index2 = 0; index2 < length; index2 += 2)
      {
        string s = PisUsuario[index2].ToString() + PisUsuario[index2 + 1].ToString();
        numArray[index1] = (byte) int.Parse(s, NumberStyles.HexNumber);
        ++index1;
      }
      return numArray;
    }

    private void AtualizaEstadoListaUsuario(byte resOperacao)
    {
      ulong num = 0;
      switch (this._estadoRep)
      {
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoExclusaoUsuarioBio1_4K:
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoExclusaoUsuarioBio1_4KCAMA:
          MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4KRepPlus usuarioBio14KrepPlus1 = this._queueMsgSolicitExclusaoUsuarioBio1_4K.Peek();
          usuarioBio14KrepPlus1.Grupo = this._rep.ModeloFim == 0 || this._rep.ModeloFim == 5 ? (byte) 19 : (byte) 20;
          num = ulong.Parse(this.DecrementaUmByteporByte(usuarioBio14KrepPlus1.NumUsuario));
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoTemplateUsuarioBio1_4K:
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoTemplateUsuarioBio1_4KCAMA:
          MsgTcpAplicacaoSolicitaUsuarioBio1_4KRepPlus usuarioBio14KrepPlus2 = this._queueMsgSolicitUsuarioBio1_4K.Peek();
          usuarioBio14KrepPlus2.Grupo = this._rep.ModeloFim == 0 || this._rep.ModeloFim == 5 ? (byte) 19 : (byte) 20;
          num = ulong.Parse(this.DecrementaUmByteporByte(usuarioBio14KrepPlus2.NumUsuario));
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInclusaoUsuarioBio1_4K:
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInclusaoUsuarioBio1_4KCAMA:
          MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4KRepPlus usuarioBio14KrepPlus3 = this._queueMsgSolicitInclusaoUsuarioBio1_4K.Peek();
          usuarioBio14KrepPlus3.Grupo = this._rep.ModeloFim == 0 || this._rep.ModeloFim == 5 ? (byte) 19 : (byte) 20;
          num = ulong.Parse(this.DecrementaUmByteporByte(usuarioBio14KrepPlus3.NumUsuario));
          break;
      }
      if (this._estadoRep == GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInclusaoUsuarioBio1_4K || this._estadoRep == GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInclusaoUsuarioBio1_4KCAMA)
      {
        foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this.ListaUsuariosBioNoDb)
        {
          if ((long) ulong.Parse(usuarioBio.Pis.ToString()) == (long) num)
          {
            usuarioBio.Status = this.ExtrairRespostaBio(resOperacao);
            usuarioBio.IdResultado = (int) resOperacao;
            break;
          }
        }
      }
      else
      {
        foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this.ListaUsuariosBio)
        {
          if ((long) ulong.Parse(usuarioBio.Pis.ToString()) == (long) num)
          {
            usuarioBio.Status = this.ExtrairRespostaBio(resOperacao);
            usuarioBio.IdResultado = (int) resOperacao;
            break;
          }
        }
      }
    }

    private string DecrementaUmByteporByte(string numUsuario)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < 8; ++index)
      {
        int num = int.Parse(numUsuario.Substring(index * 2, 2), NumberStyles.HexNumber) - 1;
        stringBuilder.Append(num.ToString("x").PadLeft(2, '0'));
      }
      return stringBuilder.ToString();
    }

    private void ExcluiUsuarioDaListaUsuario()
    {
      int index = 0;
      ulong num = ulong.Parse(this._queueMsgSolicitExclusaoUsuarioBio.Peek().NumUsuario);
      foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this.ListaUsuariosBio)
      {
        if ((long) usuarioBio.IdBiometria == (long) num)
        {
          this.ListaUsuariosBio.RemoveAt(index);
          break;
        }
        ++index;
      }
    }

    public override void CarregaListaSdkParaProcessar(
      ProcessoTemplates tipoProcessoTemplates,
      List<UsuarioBio> listaBio)
    {
      this.ListaUsuariosBio = new SortableBindingList<UsuarioBio>();
      this.ListaUsuariosBioNoDb = new SortableBindingList<UsuarioBio>();
      switch (tipoProcessoTemplates)
      {
        case ProcessoTemplates.excluirTemplates:
          using (List<UsuarioBio>.Enumerator enumerator = listaBio.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.ListaUsuariosBio.Add(enumerator.Current);
            break;
          }
        case ProcessoTemplates.importarTemplates:
          using (List<UsuarioBio>.Enumerator enumerator = listaBio.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.ListaUsuariosBio.Add(enumerator.Current);
            break;
          }
        case ProcessoTemplates.exportarTemplates:
          using (List<UsuarioBio>.Enumerator enumerator = listaBio.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.ListaUsuariosBioNoDb.Add(enumerator.Current);
            break;
          }
      }
    }

    public override void CarregarMsgSolicitExclusaoUsuarioBio()
    {
      this._queueMsgSolicitExclusaoUsuarioBio = new Queue<MsgTcpAplicacaoSolicitaExclusaoUsuarioBioRepRepPlus>();
      this._queueMsgSolicitExclusaoUsuarioBio1_4K = new Queue<MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4KRepPlus>();
      foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this.ListaUsuariosBio)
      {
        if (usuarioBio.Selecionado || this._chamadaSdk)
        {
          MsgTcpAplicacaoSolicitaExclusaoUsuarioBioRepRepPlus usuarioBioRepRepPlus = new MsgTcpAplicacaoSolicitaExclusaoUsuarioBioRepRepPlus();
          usuarioBioRepRepPlus.Leitor = (byte) 0;
          usuarioBioRepRepPlus.NumUsuario = usuarioBio.Pis.ToString().PadLeft(16, '0');
          StringBuilder stringBuilder1 = new StringBuilder();
          byte[] numArray = new byte[8];
          foreach (int pisUsuarioEmByte in this.AbrirPisUsuarioEmBytes(usuarioBioRepRepPlus.NumUsuario))
          {
            int num = pisUsuarioEmByte + 1;
            stringBuilder1.Append(num.ToString("x").PadLeft(2, '0'));
          }
          usuarioBioRepRepPlus.NumUsuario = stringBuilder1.ToString();
          this._queueMsgSolicitExclusaoUsuarioBio.Enqueue(usuarioBioRepRepPlus);
          MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4KRepPlus usuarioBio14KrepPlus = new MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4KRepPlus();
          usuarioBio14KrepPlus.Leitor = (byte) 0;
          usuarioBio14KrepPlus.Grupo = (byte) 19;
          usuarioBio14KrepPlus.NumUsuario = usuarioBio.Pis.ToString().PadLeft(16, '0');
          StringBuilder stringBuilder2 = new StringBuilder();
          numArray = new byte[8];
          foreach (int pisUsuarioEmByte in this.AbrirPisUsuarioEmBytes(usuarioBio14KrepPlus.NumUsuario))
          {
            int num = pisUsuarioEmByte + 1;
            stringBuilder2.Append(num.ToString("x").PadLeft(2, '0'));
          }
          usuarioBio14KrepPlus.NumUsuario = stringBuilder2.ToString();
          this._queueMsgSolicitExclusaoUsuarioBio1_4K.Enqueue(usuarioBio14KrepPlus);
        }
      }
    }

    public override void CarregarMsgSolicitInclusaoUsuarioBio()
    {
      this._queueMsgSolicitInclusaoUsuarioBio = new Queue<MsgTcpAplicacaoSolicitaInclusaoUsuarioBioRepRepPlus>();
      this._queueMsgSolicitInclusaoUsuarioBio1_4K = new Queue<MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4KRepPlus>();
      StringBuilder builderIdBiometria;
      byte[] idUsuarioEmBytes;
      foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this.ListaUsuariosBioNoDb)
      {
        if (usuarioBio.Selecionado || this._chamadaSdk)
        {
          if (this._rep.ModeloFim == 0 || this._rep.ModeloFim == 5)
          {
            MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4KRepPlus usuarioBio14KrepPlus = this._rep.VersaoFW == 3 || this._rep.VersaoFW == 1 || this._rep.VersaoFW == 0 || this._rep.VersaoFW == 4 ? new MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4KRepPlus((byte) 19, (byte) 32) : new MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4KRepPlus((byte) 19, (byte) 22);
            if (usuarioBio.Template1.Length == 400)
            {
              try
              {
                TemplatesBio templatesBio1 = new TemplatesBio();
                TemplatesBio templatesBio2 = BSP_BI.ConverteTemp3030para2030(new TemplatesBio()
                {
                  Template1 = usuarioBio.Template1,
                  Template2 = usuarioBio.Template2
                });
                usuarioBio14KrepPlus.Template1 = this.AbrirTemplateEmBytes1_4K(templatesBio2.Template1);
                if (this._rep.VersaoFW != 3 && this._rep.VersaoFW != 1 && this._rep.VersaoFW != 0)
                {
                  if (this._rep.VersaoFW != 4)
                    goto label_15;
                }
                usuarioBio14KrepPlus.Template2 = new byte[404];
                if (!templatesBio2.Template1.Equals(templatesBio2.Template2))
                  usuarioBio14KrepPlus.Template2 = this.AbrirTemplateEmBytes1_4K(templatesBio2.Template2);
              }
              catch (Exception ex)
              {
              }
            }
            else
            {
              if (this._chamadaSdk && (usuarioBio.Template1.Length != 808 || usuarioBio.Template2.Length != 808 && !usuarioBio.Template2.Equals("")))
              {
                this.EncerrarConexao();
                this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.templateIncompativel, this._rep.RepId, this._rep.Local);
                break;
              }
              usuarioBio14KrepPlus.Template1 = this.AbrirTemplateEmBytes1_4K(usuarioBio.Template1);
              if (this._rep.VersaoFW == 3 || this._rep.VersaoFW == 1 || this._rep.VersaoFW == 0 || this._rep.VersaoFW == 4)
              {
                usuarioBio14KrepPlus.Template2 = new byte[404];
                if (!usuarioBio.Template1.Equals(usuarioBio.Template2))
                  usuarioBio14KrepPlus.Template2 = this.AbrirTemplateEmBytes1_4K(usuarioBio.Template2);
              }
            }
label_15:
            usuarioBio14KrepPlus.Sobrescrever_digitais = this._configEnt.SobrescreverDigitais;
            usuarioBio14KrepPlus.Leitor = (byte) 0;
            usuarioBio14KrepPlus.NumUsuario = usuarioBio.Pis.ToString().PadLeft(16, '0');
            builderIdBiometria = new StringBuilder();
            idUsuarioEmBytes = new byte[8];
            idUsuarioEmBytes = this.AbrirPisUsuarioEmBytes(usuarioBio14KrepPlus.NumUsuario);
            foreach (int num1 in idUsuarioEmBytes)
            {
              int num2 = num1 + 1;
              builderIdBiometria.Append(num2.ToString("x").PadLeft(2, '0'));
            }
            usuarioBio14KrepPlus.NumUsuario = builderIdBiometria.ToString();
            usuarioBio14KrepPlus.TipoUsuario = (byte) 0;
            usuarioBio14KrepPlus.SenhaUsuario = this.AbrirSenhaEmBytes(usuarioBio.Senha);
            usuarioBio14KrepPlus.Infonivelseguranca = byte.MaxValue;
            usuarioBio14KrepPlus.Nivelseguranca = byte.MaxValue;
            this._queueMsgSolicitInclusaoUsuarioBio1_4K.Enqueue(usuarioBio14KrepPlus);
          }
          else
          {
            if (this._chamadaSdk && (usuarioBio.Template1.Length != 996 || usuarioBio.Template2.Length != 996 && !usuarioBio.Template2.Equals("")))
            {
              this.EncerrarConexao();
              this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.templateIncompativel, this._rep.RepId, this._rep.Local);
              break;
            }
            if (usuarioBio.Template1 == usuarioBio.Template2 || usuarioBio.Template2.Equals("") || this._rep.TipoTerminalId == 17 && this._rep.VersaoBaixaFW < 47 && this._rep.VersaoFW < 4 || this._rep.TipoTerminalId == 18 && this._rep.VersaoBaixaFW < 3)
            {
              this.CarregarObjetoInclusao(new MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4KRepPlus((byte) 20, (byte) 22)
              {
                Template2 = new byte[usuarioBio.Template1.Length]
              }, usuarioBio, out idUsuarioEmBytes, out builderIdBiometria);
            }
            else
            {
              MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4KRepPlus msgSolicitInclusaoUsuarioBio1_4K1 = new MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4KRepPlus((byte) 20, (byte) 22);
              MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4KRepPlus msgSolicitInclusaoUsuarioBio1_4K2 = new MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4KRepPlus((byte) 20, (byte) 32);
              this.CarregarObjetoInclusao(msgSolicitInclusaoUsuarioBio1_4K1, usuarioBio, out idUsuarioEmBytes, out builderIdBiometria);
              usuarioBio.Template1 = usuarioBio.Template2;
              this.CarregarObjetoInclusao(msgSolicitInclusaoUsuarioBio1_4K2, usuarioBio, out idUsuarioEmBytes, out builderIdBiometria);
            }
          }
        }
      }
    }

    private void CarregarObjetoInclusao(
      MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4KRepPlus msgSolicitInclusaoUsuarioBio1_4K,
      UsuarioBio usuarioBio,
      out byte[] idUsuarioEmBytes,
      out StringBuilder builderIdBiometria)
    {
      msgSolicitInclusaoUsuarioBio1_4K.Template1 = this.AbrirTemplateEmBytes1_4K(usuarioBio.Template1);
      msgSolicitInclusaoUsuarioBio1_4K.Sobrescrever_digitais = this._configEnt.SobrescreverDigitais;
      msgSolicitInclusaoUsuarioBio1_4K.Leitor = (byte) 0;
      msgSolicitInclusaoUsuarioBio1_4K.NumUsuario = usuarioBio.Pis.ToString().PadLeft(16, '0');
      builderIdBiometria = new StringBuilder();
      idUsuarioEmBytes = new byte[8];
      idUsuarioEmBytes = this.AbrirPisUsuarioEmBytes(msgSolicitInclusaoUsuarioBio1_4K.NumUsuario);
      foreach (int num1 in idUsuarioEmBytes)
      {
        int num2 = num1 + 1;
        builderIdBiometria.Append(num2.ToString("x").PadLeft(2, '0'));
      }
      msgSolicitInclusaoUsuarioBio1_4K.NumUsuario = builderIdBiometria.ToString();
      msgSolicitInclusaoUsuarioBio1_4K.TipoUsuario = (byte) 0;
      msgSolicitInclusaoUsuarioBio1_4K.SenhaUsuario = new byte[16];
      msgSolicitInclusaoUsuarioBio1_4K.Infonivelseguranca = (byte) 0;
      msgSolicitInclusaoUsuarioBio1_4K.Nivelseguranca = (byte) 0;
      this._queueMsgSolicitInclusaoUsuarioBio1_4K.Enqueue(msgSolicitInclusaoUsuarioBio1_4K);
    }

    public override void CarregarMsgSolicitUsuarioBio()
    {
      this._queueMsgSolicitUsuarioBio = new Queue<MsgTcpAplicacaoSolicitaUsuarioBioRepRepPlus>();
      this._queueMsgSolicitUsuarioBio1_4K = new Queue<MsgTcpAplicacaoSolicitaUsuarioBio1_4KRepPlus>();
      foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this.ListaUsuariosBio)
      {
        if (usuarioBio.Selecionado || this._chamadaSdk)
        {
          MsgTcpAplicacaoSolicitaUsuarioBioRepRepPlus usuarioBioRepRepPlus = new MsgTcpAplicacaoSolicitaUsuarioBioRepRepPlus();
          usuarioBioRepRepPlus.Leitor = (byte) 0;
          usuarioBioRepRepPlus.NumUsuario = usuarioBio.Pis.ToString().PadLeft(16, '0');
          StringBuilder stringBuilder1 = new StringBuilder();
          byte[] numArray = new byte[8];
          foreach (int pisUsuarioEmByte in this.AbrirPisUsuarioEmBytes(usuarioBioRepRepPlus.NumUsuario))
          {
            int num = pisUsuarioEmByte + 1;
            stringBuilder1.Append(num.ToString("x").PadLeft(2, '0'));
          }
          usuarioBioRepRepPlus.NumUsuario = stringBuilder1.ToString();
          this._queueMsgSolicitUsuarioBio.Enqueue(usuarioBioRepRepPlus);
          MsgTcpAplicacaoSolicitaUsuarioBio1_4KRepPlus usuarioBio14KrepPlus = new MsgTcpAplicacaoSolicitaUsuarioBio1_4KRepPlus();
          usuarioBio14KrepPlus.Leitor = (byte) 0;
          usuarioBio14KrepPlus.NumUsuario = usuarioBio.Pis.ToString().PadLeft(16, '0');
          StringBuilder stringBuilder2 = new StringBuilder();
          numArray = new byte[8];
          foreach (int pisUsuarioEmByte in this.AbrirPisUsuarioEmBytes(usuarioBio14KrepPlus.NumUsuario))
          {
            int num = pisUsuarioEmByte + 1;
            stringBuilder2.Append(num.ToString("x").PadLeft(2, '0'));
          }
          usuarioBio14KrepPlus.NumUsuario = stringBuilder2.ToString();
          this._queueMsgSolicitUsuarioBio1_4K.Enqueue(usuarioBio14KrepPlus);
        }
      }
    }

    private void GravarTemplates1_4K()
    {
      if (this._chamadaSdk)
      {
        this.ListaUsuariosBio.Clear();
        foreach (MsgTcpAplicacaoRespostaUsuarioBio1_4KRepPlus usuarioBio14KrepPlus in this._lstMsgRespostaUsuarioBio1_4K)
        {
          UsuarioBio usuarioBio = new UsuarioBio();
          string numUsuario = usuarioBio14KrepPlus.NumUsuario;
          usuarioBio.Pis = ulong.Parse(numUsuario.ToString()).ToString();
          StringBuilder stringBuilder1 = new StringBuilder();
          foreach (byte num in usuarioBio14KrepPlus.Template1)
          {
            string str = num.ToString("x");
            if (str.Length == 1)
              str = "0" + str;
            stringBuilder1.Append(str);
          }
          usuarioBio.Template1 = stringBuilder1.ToString();
          if (this._rep.VersaoFW == 3 || this._rep.VersaoFW == 1 || this._rep.VersaoFW == 0 || this._rep.VersaoFW == 4)
          {
            bool flag = true;
            StringBuilder stringBuilder2 = new StringBuilder();
            foreach (byte num in usuarioBio14KrepPlus.Template2)
            {
              string str = num.ToString("x");
              if (str.Length == 1)
                str = "0" + str;
              stringBuilder2.Append(str);
              if (num > (byte) 0)
                flag = false;
            }
            usuarioBio.Template2 = stringBuilder2.ToString();
            if (flag)
              usuarioBio.Template2 = usuarioBio.Template1;
          }
          else
            usuarioBio.Template2 = usuarioBio.Template1;
          usuarioBio.TipoTemplate = 2;
          this.ListaUsuariosBio.Add(usuarioBio);
        }
      }
      else
      {
        TemplateBio templateBio1 = new TemplateBio();
        try
        {
          foreach (MsgTcpAplicacaoRespostaUsuarioBio1_4KRepPlus usuarioBio14KrepPlus in this._lstMsgRespostaUsuarioBio1_4K)
          {
            TemplatesBio templateBio2 = new TemplatesBio();
            string numUsuario = usuarioBio14KrepPlus.NumUsuario;
            Empregado empregado = new Empregado().PesquisarEmpregadosPorPis(new Empregado()
            {
              Pis = ulong.Parse(numUsuario).ToString(),
              EmpregadorId = this._empregador.EmpregadorId
            });
            templateBio2.Pis = ulong.Parse(numUsuario.ToString()).ToString();
            StringBuilder stringBuilder3 = new StringBuilder();
            foreach (byte num in usuarioBio14KrepPlus.SenhaUsuario)
            {
              string str = num.ToString("x");
              if (str.Length == 1)
                str = "0" + str;
              stringBuilder3.Append(str);
            }
            templateBio2.Senha = stringBuilder3.ToString();
            StringBuilder stringBuilder4 = new StringBuilder();
            foreach (byte num in usuarioBio14KrepPlus.Template1)
            {
              string str = num.ToString("x");
              if (str.Length == 1)
                str = "0" + str;
              stringBuilder4.Append(str);
            }
            templateBio2.Template1 = stringBuilder4.ToString();
            if (this._rep.VersaoFW == 3 || this._rep.VersaoFW == 1 || this._rep.VersaoFW == 0 || this._rep.VersaoFW == 4)
            {
              bool flag = true;
              StringBuilder stringBuilder5 = new StringBuilder();
              foreach (byte num in usuarioBio14KrepPlus.Template2)
              {
                string str = num.ToString("x");
                if (str.Length == 1)
                  str = "0" + str;
                stringBuilder5.Append(str);
                if (num > (byte) 0)
                  flag = false;
              }
              templateBio2.Template2 = stringBuilder5.ToString();
              if (flag)
                templateBio2.Template2 = templateBio2.Template1;
            }
            else
              templateBio2.Template2 = templateBio2.Template1;
            templateBio2.EmpregadorID = this._empregador.EmpregadorId;
            templateBio2.TipoTemplate = 2;
            templateBio2.InfoNivelSeguranca = (int) byte.MaxValue;
            templateBio2.NivelSeguranca = (int) byte.MaxValue;
            templateBio2.EmpregadoID = empregado.EmpregadoId;
            templateBio1.ExcluirTemplates(templateBio2, this._empregador.EmpregadorId, "TemplatesNitgen");
            templateBio1.InserirTemplates(templateBio2, "TemplatesNitgen");
          }
        }
        catch (AppTopdataException ex)
        {
          this.NotificarParaUsuario(Resources.msgERRO_CONEXAO_INTERROMPIDA_ERRO_DB, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          if (!ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
            return;
          throw;
        }
        catch (Exception ex)
        {
          this.NotificarParaUsuario(Resources.msgERRO_CONEXAO_INTERROMPIDA_ERRO_IN, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          ex.Data.Add((object) "mensagem", (object) Resources.msgERRO_CONEXAO_INTERROMPIDA_ERRO_IN);
          if (!ExceptionPolicy.HandleException(ex, "Politica Dominio"))
            return;
          throw;
        }
      }
    }

    private void GravarTemplates1_4K(
      MsgTcpAplicacaoRespostaUsuarioBio1_4KRepPlus _lstMsgRespostaUsuarioBio1_4K)
    {
      if (this._chamadaSdk)
      {
        UsuarioBio usuarioBio = new UsuarioBio();
        string numUsuario = _lstMsgRespostaUsuarioBio1_4K.NumUsuario;
        usuarioBio.Pis = ulong.Parse(numUsuario.ToString()).ToString();
        StringBuilder stringBuilder1 = new StringBuilder();
        foreach (byte num in _lstMsgRespostaUsuarioBio1_4K.Template1)
        {
          string str = num.ToString("x");
          if (str.Length == 1)
            str = "0" + str;
          stringBuilder1.Append(str);
        }
        usuarioBio.Template1 = stringBuilder1.ToString();
        if (this._rep.VersaoFW == 3 || this._rep.VersaoFW == 1 || this._rep.VersaoFW == 0 || this._rep.VersaoFW == 4)
        {
          bool flag = true;
          StringBuilder stringBuilder2 = new StringBuilder();
          foreach (byte num in _lstMsgRespostaUsuarioBio1_4K.Template2)
          {
            string str = num.ToString("x");
            if (str.Length == 1)
              str = "0" + str;
            stringBuilder2.Append(str);
            if (num > (byte) 0)
              flag = false;
          }
          usuarioBio.Template2 = stringBuilder2.ToString();
          if (flag)
            usuarioBio.Template2 = usuarioBio.Template1;
        }
        else
          usuarioBio.Template2 = usuarioBio.Template1;
        usuarioBio.TipoTemplate = 2;
        this.ListaUsuariosBio.Add(usuarioBio);
      }
      else
      {
        TemplateBio templateBio1 = new TemplateBio();
        try
        {
          TemplatesBio templateBio2 = new TemplatesBio();
          string numUsuario = _lstMsgRespostaUsuarioBio1_4K.NumUsuario;
          Empregado empregado = new Empregado().PesquisarEmpregadosPorPis(new Empregado()
          {
            Pis = ulong.Parse(numUsuario).ToString().PadLeft(12, '0'),
            EmpregadorId = this._empregador.EmpregadorId
          });
          templateBio2.Pis = ulong.Parse(numUsuario.ToString()).ToString().PadLeft(12, '0');
          StringBuilder stringBuilder3 = new StringBuilder();
          foreach (byte num in _lstMsgRespostaUsuarioBio1_4K.SenhaUsuario)
          {
            string str = num.ToString("x");
            if (str.Length == 1)
              str = "0" + str;
            stringBuilder3.Append(str);
          }
          templateBio2.Senha = stringBuilder3.ToString();
          StringBuilder stringBuilder4 = new StringBuilder();
          foreach (byte num in _lstMsgRespostaUsuarioBio1_4K.Template1)
          {
            string str = num.ToString("x");
            if (str.Length == 1)
              str = "0" + str;
            stringBuilder4.Append(str);
          }
          templateBio2.Template1 = stringBuilder4.ToString();
          bool flag = true;
          foreach (byte num in _lstMsgRespostaUsuarioBio1_4K.Template2)
          {
            if (num != (byte) 0)
            {
              flag = false;
              break;
            }
          }
          if (this._rep.VersaoFW == 3 || !flag || this._rep.VersaoFW == 1 || this._rep.VersaoFW == 0 || this._rep.VersaoFW == 4)
          {
            StringBuilder stringBuilder5 = new StringBuilder();
            foreach (byte num in _lstMsgRespostaUsuarioBio1_4K.Template2)
            {
              string str = num.ToString("x");
              if (str.Length == 1)
                str = "0" + str;
              stringBuilder5.Append(str);
              if (num > (byte) 0)
                flag = false;
            }
            templateBio2.Template2 = stringBuilder5.ToString();
            if (flag)
              templateBio2.Template2 = templateBio2.Template1;
          }
          else
            templateBio2.Template2 = templateBio2.Template1;
          templateBio2.EmpregadorID = this._empregador.EmpregadorId;
          templateBio2.InfoNivelSeguranca = (int) byte.MaxValue;
          templateBio2.NivelSeguranca = (int) byte.MaxValue;
          templateBio2.EmpregadoID = empregado.EmpregadoId;
          if (this._rep.ModeloFim == 0 || this._rep.ModeloFim == 5)
          {
            templateBio2.TipoTemplate = 2;
            templateBio1.ExcluirTemplates(templateBio2, this._empregador.EmpregadorId, "TemplatesNitgen");
            templateBio1.InserirTemplates(templateBio2, "TemplatesNitgen");
          }
          else
          {
            templateBio2.TipoTemplate = 4;
            templateBio1.ExcluirTemplatesCAMA(templateBio2, this._empregador.EmpregadorId);
            templateBio1.InserirTemplatesCAMA(templateBio2);
          }
        }
        catch (AppTopdataException ex)
        {
          this.NotificarParaUsuario(Resources.msgERRO_CONEXAO_INTERROMPIDA_ERRO_DB, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          if (!ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
            return;
          throw;
        }
        catch (Exception ex)
        {
          this.NotificarParaUsuario(Resources.msgERRO_CONEXAO_INTERROMPIDA_ERRO_IN, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          ex.Data.Add((object) "mensagem", (object) Resources.msgERRO_CONEXAO_INTERROMPIDA_ERRO_IN);
          if (!ExceptionPolicy.HandleException(ex, "Politica Dominio"))
            return;
          throw;
        }
      }
    }

    public static int ExcluirTemplatesBD(int idBiometrico) => new TemplateBio().ExcluirTemplates(idBiometrico, "TemplatesNitgen");

    public static int ExcluirTemplatesCAMABD(int idBiometrico) => new TemplateBio().ExcluirTemplates(idBiometrico, "TemplatesCama");

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
      {
        this.RaiseNotificarTipoPlacaFim(new NotificarTipoPlacaFIMEventArgs(this._rep.ModeloFim, this._rep.RepId));
        switch (this._tipoProcessoTemplates)
        {
          case ProcessoTemplates.excluirTemplates:
            this.CarregarMsgSolicitExclusaoUsuarioBio();
            if (this._queueMsgSolicitExclusaoUsuarioBio.Count > 0)
            {
              this.TotTemplatesParaProcessar = this._queueMsgSolicitExclusaoUsuarioBio.Count;
              break;
            }
            if (this._queueMsgSolicitExclusaoUsuarioBio1_4K.Count > 0)
            {
              this.TotTemplatesParaProcessar = this._queueMsgSolicitExclusaoUsuarioBio1_4K.Count;
              break;
            }
            break;
          case ProcessoTemplates.importarTemplates:
            this.CarregarMsgSolicitUsuarioBio();
            if (this._queueMsgSolicitUsuarioBio.Count > 0)
              this.TotTemplatesParaProcessar = this._queueMsgSolicitUsuarioBio.Count;
            else if (this._queueMsgSolicitUsuarioBio1_4K.Count > 0)
              this.TotTemplatesParaProcessar = this._queueMsgSolicitUsuarioBio1_4K.Count;
            if (this._chamadaSdk)
            {
              this.ListaUsuariosBio.Clear();
              break;
            }
            break;
          case ProcessoTemplates.exportarTemplates:
            this.CarregarMsgSolicitInclusaoUsuarioBio();
            if (this._queueMsgSolicitInclusaoUsuarioBio.Count > 0)
            {
              this.TotTemplatesParaProcessar = this._queueMsgSolicitInclusaoUsuarioBio.Count;
              break;
            }
            if (this._queueMsgSolicitInclusaoUsuarioBio1_4K.Count > 0)
            {
              this.TotTemplatesParaProcessar = this._queueMsgSolicitInclusaoUsuarioBio1_4K.Count;
              break;
            }
            break;
          case ProcessoTemplates.solicitar_modelo_biometria:
            this.EncerrarConexao();
            this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            return;
        }
        if (this._rep.ModeloFim != (int) byte.MaxValue)
        {
          if (this._rep.ModeloFim == 0 || this._rep.ModeloFim == 5)
            this.EnviarSolicitacaoInfo();
          else if (this._rep.ModeloFim == 1)
          {
            this.EnviarSolicitacaoInfoCAMA();
          }
          else
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          }
        }
        else
        {
          this.EncerrarConexao();
          if (this._rep.ModeloFim == (int) byte.MaxValue)
            this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          else
            this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        }
      }
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInfo:
          menssagem = Resources.msgTIMEOUT_SOLICIT_INFO_BIO;
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoPacoteTemplates1_4K:
          menssagem = Resources.msgTIMEOUT_SOLICIT_TEMPLATE;
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoExclusaoUsuarioBio1_4K:
          menssagem = Resources.msgTIMEOUT_SOLICIT_EXCLUSAO_TEMPLATE;
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoTemplateUsuarioBio1_4K:
          menssagem = Resources.msgTIMEOUT_SOLICIT_TEMPLATE_USUARIO;
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInclusaoUsuarioBio1_4K:
          menssagem = Resources.msgTIMEOUT_SOLICIT_INCLUSAO_TEMPLATE;
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
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInfo:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_INFO_BIO;
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoPacoteTemplates1_4K:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_TEMPLATE;
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoExclusaoUsuarioBio1_4K:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_EXCLUSAO_TEMPLATE;
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoTemplateUsuarioBio1_4K:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_TEMPLATE_USUARIO;
          break;
        case GerenciadorTemplatesBioRepPlus.Estados.estSolicitacaoInclusaoUsuarioBio1_4K:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_INCLUSAO_TEMPLATE;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void CarregaListaSdkProcessada(
      ref List<UsuarioBio> listaBio,
      ProcessoTemplates tipoProcessoTemplates)
    {
      listaBio.Clear();
      if (tipoProcessoTemplates == ProcessoTemplates.exportarTemplates)
      {
        foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this.ListaUsuariosBioNoDb)
          listaBio.Add(usuarioBio);
      }
      else
      {
        foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this.ListaUsuariosBio)
          listaBio.Add(usuarioBio);
      }
    }

    public override void IniciarProcesso()
    {
    }

    private new enum Estados
    {
      estSolicitacaoInfo,
      estSolicitacaoInfoCama,
      estSolicitacaoPacoteTemplates1_4K,
      estSolicitacaoExclusaoUsuarioBio1_4K,
      estSolicitacaoTemplateUsuarioBio1_4K,
      estSolicitacaoInclusaoUsuarioBio1_4K,
      estSolicitacaoPacoteTemplates1_4KCAMA,
      estSolicitacaoExclusaoUsuarioBio1_4KCAMA,
      estSolicitacaoTemplateUsuarioBio1_4KCAMA,
      estSolicitacaoInclusaoUsuarioBio1_4KCAMA,
    }
  }
}
