// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorTemplatesBio
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
  public class GerenciadorTemplatesBio : TarefaAbstrata
  {
    private RepBase _rep;
    private bool _chamadaSdk;
    private int _modeloBioSdk;
    private List<UsuarioBio> _listaBioSdk = new List<UsuarioBio>();
    private Empregador _empregador;
    private SortableBindingList<UsuarioBio> _listaUsuario;
    private SortableBindingList<UsuarioBio> _listaUsuarioNoDb;
    private Queue<MsgTcpAplicacaoSolicitaExclusaoUsuarioBioRep> _queueMsgSolicitExclusaoUsuarioBio;
    private Queue<MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4K> _queueMsgSolicitExclusaoUsuarioBio1_4K;
    private Queue<MsgTcpAplicacaoSolicitaInclusaoUsuarioBioRep> _queueMsgSolicitInclusaoUsuarioBio;
    private Queue<MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4K> _queueMsgSolicitInclusaoUsuarioBio1_4K;
    private Queue<MsgTcpAplicacaoSolicitaUsuarioBioRep> _queueMsgSolicitUsuarioBio;
    private Queue<MsgTcpAplicacaoSolicitaUsuarioBio1_4K> _queueMsgSolicitUsuarioBio1_4K;
    private List<MsgTcpAplicacaoRespostaUsuarioBio> _lstMsgRespostaUsuarioBio;
    private List<MsgTcpAplicacaoRespostaUsuarioBio1_4K> _lstMsgRespostaUsuarioBio1_4K;
    private GerenciadorTemplatesBio.Estados _estadoRep;
    private ProcessoTemplates _tipoProcessoTemplates;
    private MsgTcpAplicacaoRespostaPacoteUsuarioBio _msgRespostaPacoteTemplates;
    private MsgTcpAplicacaoRespostaPacoteUsuarioBio1_4K _msgRespostaPacoteTemplates1_4K;
    private MsgTcpAplicacaoRespostaUsuarioBio _msgRespostaTemplateUsuario;
    private MsgTcpAplicacaoRespostaUsuarioBio1_4K _msgRespostaTemplateUsuario1_4K;
    private bool _placaFim1_4K = true;
    private int _totTemplatesParaProcessar;
    public static GerenciadorTemplatesBio _gerenciadorTemplatesBio;

    public Empregador Empregador
    {
      get => this._empregador;
      set => this._empregador = value;
    }

    public bool PlacaFim1_4K
    {
      get => this._placaFim1_4K;
      set => this._placaFim1_4K = value;
    }

    public event EventHandler<NotificarProgressBarEventArgs> OnNotificarProgressBar;

    public event EventHandler<NotificarModeloBIOEventArgs> OnNotificarModeloBIO;

    public static GerenciadorTemplatesBio getInstance() => GerenciadorTemplatesBio._gerenciadorTemplatesBio != null ? GerenciadorTemplatesBio._gerenciadorTemplatesBio : new GerenciadorTemplatesBio();

    public static GerenciadorTemplatesBio getInstance(RepBase rep) => GerenciadorTemplatesBio._gerenciadorTemplatesBio != null ? GerenciadorTemplatesBio._gerenciadorTemplatesBio : new GerenciadorTemplatesBio(rep);

    public static GerenciadorTemplatesBio getInstance(
      RepBase rep,
      int modeloBioSdk)
    {
      return GerenciadorTemplatesBio._gerenciadorTemplatesBio != null ? GerenciadorTemplatesBio._gerenciadorTemplatesBio : new GerenciadorTemplatesBio(rep, modeloBioSdk);
    }

    public GerenciadorTemplatesBio()
    {
    }

    public GerenciadorTemplatesBio(RepBase rep)
    {
      this._rep = rep;
      this._chamadaSdk = false;
      this._empregador = this._rep.Empregador.PesquisarEmpregadorDeUmREP(this._rep.RepId);
    }

    public GerenciadorTemplatesBio(RepBase rep, int modeloBioSdk)
    {
      this._rep = rep;
      this._chamadaSdk = true;
      this._modeloBioSdk = modeloBioSdk;
      this._listaUsuario = new SortableBindingList<UsuarioBio>();
    }

    ~GerenciadorTemplatesBio()
    {
    }

    public SortableBindingList<UsuarioBio> ListaUsuariosBio
    {
      get => this._listaUsuario;
      set => this._listaUsuario = value;
    }

    public SortableBindingList<UsuarioBio> ListaUsuariosBioNoDb
    {
      get => this._listaUsuarioNoDb;
      set => this._listaUsuarioNoDb = value;
    }

    public void IniciarProcesso(ProcessoTemplates processoTemp)
    {
      this._tipoProcessoTemplates = processoTemp;
      this.Conectar(this._rep);
    }

    private void EnviarSolicitacaoPacoteTemplates(byte numPacote)
    {
      try
      {
        this._estadoRep = GerenciadorTemplatesBio.Estados.estSolicitacaoPacoteTemplates;
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        if (this._rep.redeRemota)
          envelope.MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaPacoteUsuarioBio_Pequeno()
          {
            NumPac = numPacote,
            Leitor = (byte) 0
          };
        else
          envelope.MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaPacoteUsuarioBio_Grande()
          {
            NumPac = numPacote,
            Leitor = (byte) 0
          };
        this.ClienteSocket.Enviar(envelope, true);
        Thread.Sleep(Convert.ToInt32(RegistrySingleton.GetInstance().TIMEOUT_BASE / 10));
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
        this._estadoRep = GerenciadorTemplatesBio.Estados.estSolicitacaoPacoteTemplates1_4K;
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        if (this._rep.redeRemota)
          envelope.MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaPacoteUsuarioBio1_4K_Pequeno()
          {
            NumPac = numPacote,
            Leitor = (byte) 0
          };
        else
          envelope.MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaPacoteUsuarioBio1_4K_Grande()
          {
            NumPac = numPacote,
            Leitor = (byte) 0
          };
        this.ClienteSocket.Enviar(envelope, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoExclusaoUsuarioBio()
    {
      try
      {
        this._estadoRep = GerenciadorTemplatesBio.Estados.estSolicitacaoExclusaoUsuarioBio;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) this._queueMsgSolicitExclusaoUsuarioBio.Peek()
        }, true);
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
        this._estadoRep = GerenciadorTemplatesBio.Estados.estSolicitacaoExclusaoUsuarioBio1_4K;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) this._queueMsgSolicitExclusaoUsuarioBio1_4K.Peek()
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoUsuarioBio()
    {
      try
      {
        this._estadoRep = GerenciadorTemplatesBio.Estados.estSolicitacaoTemplateUsuarioBio;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) this._queueMsgSolicitUsuarioBio.Peek()
        }, true);
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
        this._estadoRep = GerenciadorTemplatesBio.Estados.estSolicitacaoTemplateUsuarioBio1_4K;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) this._queueMsgSolicitUsuarioBio1_4K.Peek()
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoInclusaoUsuarioBio()
    {
      try
      {
        this._estadoRep = GerenciadorTemplatesBio.Estados.estSolicitacaoInclusaoUsuarioBio;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) this._queueMsgSolicitInclusaoUsuarioBio.Peek()
        }, true);
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
        this._estadoRep = GerenciadorTemplatesBio.Estados.estSolicitacaoInclusaoUsuarioBio1_4K;
        DateTime now = DateTime.Now;
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4K inclusaoUsuarioBio14K = this._queueMsgSolicitInclusaoUsuarioBio1_4K.Peek();
        inclusaoUsuarioBio14K.CentenaAno = (byte) 20;
        inclusaoUsuarioBio14K.RestoAno = (byte) (DateTime.Now.Year % 100);
        inclusaoUsuarioBio14K.Mes = Convert.ToByte(now.Month);
        inclusaoUsuarioBio14K.Dia = Convert.ToByte(now.Day);
        inclusaoUsuarioBio14K.Hora = Convert.ToByte(now.Hour);
        inclusaoUsuarioBio14K.Minuto = Convert.ToByte(now.Minute);
        inclusaoUsuarioBio14K.Segundo = Convert.ToByte(now.Second);
        inclusaoUsuarioBio14K.Reservado = (byte) 0;
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) inclusaoUsuarioBio14K;
        this.ClienteSocket.Enviar(envelope, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoInfo()
    {
      try
      {
        this._estadoRep = GerenciadorTemplatesBio.Estados.estSolicitacaoInfo;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaInformacaoBio()
          {
            Info = (byte) 1,
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

    public override void TratarEnvelope(Envelope envelope)
    {
      switch (this._estadoRep)
      {
        case GerenciadorTemplatesBio.Estados.estSolicitacaoInfo:
          if (envelope.Grp != (byte) 6 || envelope.Cmd != (byte) 100)
            break;
          if (this.AbrirMsgRespostaInfo(envelope).Resultado != (byte) 1)
          {
            this.EnviarSolicitacaoInfo();
            break;
          }
          switch (this._tipoProcessoTemplates)
          {
            case ProcessoTemplates.recuperarTemplates:
              if (this._placaFim1_4K)
              {
                this._listaUsuario = new SortableBindingList<UsuarioBio>();
                this.EnviarSolicitacaoPacoteTemplates1_4K((byte) 0);
                return;
              }
              this._listaUsuario = new SortableBindingList<UsuarioBio>();
              this.EnviarSolicitacaoPacoteTemplates((byte) 0);
              return;
            case ProcessoTemplates.excluirTemplates:
              this.NotificarParaUsuario(Resources.msgENVIO_EXCLUSAO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
              if (this._placaFim1_4K)
              {
                this.EnviarSolicitacaoExclusaoUsuarioBio1_4K();
                return;
              }
              this.EnviarSolicitacaoExclusaoUsuarioBio();
              return;
            case ProcessoTemplates.importarTemplates:
              this.NotificarParaUsuario(Resources.msgENVIO_SOLICITACAO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
              if (this._placaFim1_4K)
              {
                this._lstMsgRespostaUsuarioBio1_4K = new List<MsgTcpAplicacaoRespostaUsuarioBio1_4K>();
                this.EnviarSolicitacaoUsuarioBio1_4K();
                return;
              }
              this._lstMsgRespostaUsuarioBio = new List<MsgTcpAplicacaoRespostaUsuarioBio>();
              this.EnviarSolicitacaoUsuarioBio();
              return;
            case ProcessoTemplates.exportarTemplates:
              this.NotificarParaUsuario(Resources.msgENVIO_INCLUSAO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
              if (this._placaFim1_4K)
              {
                this.EnviarSolicitacaoInclusaoUsuarioBio1_4K();
                return;
              }
              this.CarregarMsgSolicitInclusaoUsuarioBio();
              this.EnviarSolicitacaoInclusaoUsuarioBio();
              return;
            case ProcessoTemplates.solicitar_modelo_biometria:
              this.EncerrarConexao();
              this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              return;
            default:
              this.EncerrarConexao();
              this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
              return;
          }
        case GerenciadorTemplatesBio.Estados.estSolicitacaoPacoteTemplates:
          if (envelope.Grp != (byte) 6 || envelope.Cmd != (byte) 110 && envelope.Cmd != (byte) 111)
            break;
          this.AbrirMsgRespostaSolicitacaoPacoteTemplates(envelope);
          if (this._msgRespostaPacoteTemplates.Resultado == (byte) 1)
          {
            if (this._msgRespostaPacoteTemplates.NumUsrCad > (ushort) 0)
            {
              byte numPacote = (byte) ((uint) this._msgRespostaPacoteTemplates.NumPac + 1U);
              this.AssociarEmpregadoBio();
              if ((int) numPacote <= (int) this._msgRespostaPacoteTemplates.TotPac)
              {
                this.EnviarSolicitacaoPacoteTemplates(numPacote);
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
          string menssagem1 = this.ExtrairRespostaBio(this._msgRespostaPacoteTemplates.Resultado);
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem1, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoExclusaoUsuarioBio:
          if (envelope.Grp != (byte) 6 || envelope.Cmd != (byte) 121)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 1 || envelope.MsgAplicacaoEmBytes[2] == (byte) 5)
          {
            if (this._chamadaSdk)
              this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
            if (this._queueMsgSolicitExclusaoUsuarioBio.Count > 1)
            {
              this._queueMsgSolicitExclusaoUsuarioBio.Dequeue();
              this.EnviarSolicitacaoExclusaoUsuarioBio();
              NotificarProgressBarEventArgs e = new NotificarProgressBarEventArgs(this._totTemplatesParaProcessar);
              if (this.OnNotificarProgressBar == null)
                break;
              this.OnNotificarProgressBar((object) this, e);
              break;
            }
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgEXCLUSAO_TEMPLATE_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            break;
          }
          string menssagem2 = this.ExtrairRespostaBio(envelope.MsgAplicacaoEmBytes[2]);
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem2, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoTemplateUsuarioBio:
          if (envelope.Grp != (byte) 6 || envelope.Cmd != (byte) 120)
            break;
          this.AbrirMsgRespostaSolicitacaoTemplateUsuario(envelope);
          if (this._msgRespostaTemplateUsuario.Resultado == (byte) 1 || this._msgRespostaTemplateUsuario.Resultado == (byte) 5)
          {
            this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
            this.GravarTemplates(this._lstMsgRespostaUsuarioBio[this._lstMsgRespostaUsuarioBio.Count - 1]);
            if (this._queueMsgSolicitUsuarioBio.Count > 1)
            {
              this._queueMsgSolicitUsuarioBio.Dequeue();
              this.EnviarSolicitacaoUsuarioBio();
              NotificarProgressBarEventArgs e = new NotificarProgressBarEventArgs(this._totTemplatesParaProcessar);
              if (this.OnNotificarProgressBar == null)
                break;
              this.OnNotificarProgressBar((object) this, e);
              break;
            }
            if (this._lstMsgRespostaUsuarioBio.Count > 0)
            {
              this.NotificarParaUsuario(Resources.msgINICIO_PROCESSO_IMPORTACAO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              this.EncerrarConexao();
              this.NotificarParaUsuario(Resources.msgPROCESSO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              break;
            }
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgNAO_ENCONTRADO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.nenhumEstado, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
            break;
          }
          string menssagem3 = this.ExtrairRespostaBio(this._msgRespostaTemplateUsuario.Resultado);
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem3, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoInclusaoUsuarioBio:
          if (envelope.Grp != (byte) 6 || envelope.Cmd != (byte) 122)
            break;
          Thread.Sleep(150);
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 1 || envelope.MsgAplicacaoEmBytes[2] == (byte) 5 || envelope.MsgAplicacaoEmBytes[2] == (byte) 4 || envelope.MsgAplicacaoEmBytes[2] == (byte) 22)
          {
            this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
            if (this._queueMsgSolicitInclusaoUsuarioBio.Count > 1)
            {
              this._queueMsgSolicitInclusaoUsuarioBio.Dequeue();
              Thread.Sleep(30);
              this.EnviarSolicitacaoInclusaoUsuarioBio();
              NotificarProgressBarEventArgs e = new NotificarProgressBarEventArgs(this._totTemplatesParaProcessar);
              if (this.OnNotificarProgressBar == null)
                break;
              this.OnNotificarProgressBar((object) this, e);
              break;
            }
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgPROCESSO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            break;
          }
          this.ExtrairRespostaBio(envelope.MsgAplicacaoEmBytes[2]);
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgPROCESSO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoPacoteTemplates1_4K:
          if (envelope.Grp != (byte) 7 || envelope.Cmd != (byte) 110 && envelope.Cmd != (byte) 111)
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
          string menssagem4 = this.ExtrairRespostaBio(this._msgRespostaPacoteTemplates1_4K.Resultado);
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem4, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoExclusaoUsuarioBio1_4K:
          if (envelope.Grp != (byte) 7 || envelope.Cmd != (byte) 121)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 1 || envelope.MsgAplicacaoEmBytes[2] == (byte) 5)
          {
            if (this._chamadaSdk)
              this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
            if (this._queueMsgSolicitExclusaoUsuarioBio1_4K.Count > 1)
            {
              this._queueMsgSolicitExclusaoUsuarioBio1_4K.Dequeue();
              this.EnviarSolicitacaoExclusaoUsuarioBio1_4K();
              NotificarProgressBarEventArgs e = new NotificarProgressBarEventArgs(this._totTemplatesParaProcessar);
              if (this.OnNotificarProgressBar == null)
                break;
              this.OnNotificarProgressBar((object) this, e);
              break;
            }
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgEXCLUSAO_TEMPLATE_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            break;
          }
          string menssagem5 = this.ExtrairRespostaBio(envelope.MsgAplicacaoEmBytes[2]);
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem5, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoTemplateUsuarioBio1_4K:
          if (envelope.Grp != (byte) 7 || envelope.Cmd != (byte) 120)
            break;
          this.AbrirMsgRespostaSolicitacaoTemplateUsuario1_4K(envelope);
          if (this._msgRespostaTemplateUsuario1_4K.Resultado == (byte) 1 || this._msgRespostaTemplateUsuario1_4K.Resultado == (byte) 5)
          {
            long result = long.MinValue;
            if (!long.TryParse(this._msgRespostaTemplateUsuario1_4K.NumUsuario, out result) && this._queueMsgSolicitUsuarioBio1_4K.Count > 1)
            {
              this.EnviarSolicitacaoUsuarioBio1_4K();
              break;
            }
            this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
            this.GravarTemplates1_4K(this._lstMsgRespostaUsuarioBio1_4K[this._lstMsgRespostaUsuarioBio1_4K.Count - 1]);
            if (this._queueMsgSolicitUsuarioBio1_4K.Count > 1)
            {
              this._queueMsgSolicitUsuarioBio1_4K.Dequeue();
              this.EnviarSolicitacaoUsuarioBio1_4K();
              NotificarProgressBarEventArgs e = new NotificarProgressBarEventArgs(this._totTemplatesParaProcessar);
              if (this.OnNotificarProgressBar == null)
                break;
              this.OnNotificarProgressBar((object) this, e);
              break;
            }
            if (this._lstMsgRespostaUsuarioBio1_4K.Count > 0)
            {
              this.EncerrarConexao();
              this.NotificarParaUsuario(Resources.msgPROCESSO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              break;
            }
            this.NotificarParaUsuario(Resources.msgNAO_ENCONTRADO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.nenhumEstado, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
            break;
          }
          string menssagem6 = this.ExtrairRespostaBio(this._msgRespostaTemplateUsuario1_4K.Resultado);
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem6, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoInclusaoUsuarioBio1_4K:
          if (envelope.Grp != (byte) 7 || envelope.Cmd != (byte) 122)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 1 || envelope.MsgAplicacaoEmBytes[2] == (byte) 5 || envelope.MsgAplicacaoEmBytes[2] == (byte) 4 || envelope.MsgAplicacaoEmBytes[2] == (byte) 22)
          {
            this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
            if (this._queueMsgSolicitInclusaoUsuarioBio1_4K.Count > 1)
            {
              this._queueMsgSolicitInclusaoUsuarioBio1_4K.Dequeue();
              Thread.Sleep(30);
              this.EnviarSolicitacaoInclusaoUsuarioBio1_4K();
              NotificarProgressBarEventArgs e = new NotificarProgressBarEventArgs(this._totTemplatesParaProcessar);
              if (this.OnNotificarProgressBar == null)
                break;
              this.OnNotificarProgressBar((object) this, e);
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
          string menssagem7 = this.ExtrairRespostaBio(envelope.MsgAplicacaoEmBytes[2]);
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem7, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
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
        case 22:
          str = Resources.msgBIO_TEMPLATE_INVALIDO;
          break;
        default:
          str = Resources.msgBIO_ERRO_COMUNICACAO;
          break;
      }
      return str;
    }

    private MsgTcpAplicacaoRespostaInformacaoBio AbrirMsgRespostaInfo(
      Envelope envelope)
    {
      MsgTcpAplicacaoRespostaInformacaoBio respostaInformacaoBio = new MsgTcpAplicacaoRespostaInformacaoBio();
      byte[] aplicacaoEmBytes = envelope.MsgAplicacaoEmBytes;
      respostaInformacaoBio.Resultado = Convert.ToByte(aplicacaoEmBytes[2].ToString("X"));
      if (respostaInformacaoBio.Resultado == (byte) 1)
      {
        respostaInformacaoBio.Modelo = Convert.ToByte(aplicacaoEmBytes[4].ToString("X"));
        if (respostaInformacaoBio.Modelo == (byte) 30 || respostaInformacaoBio.Modelo == (byte) 40)
        {
          this._placaFim1_4K = false;
          NotificarModeloBIOEventArgs e = new NotificarModeloBIOEventArgs(1, this._rep.RepId);
          if (this.OnNotificarModeloBIO != null)
            this.OnNotificarModeloBIO((object) this, e);
        }
        else if (respostaInformacaoBio.Modelo == (byte) 53)
        {
          this._placaFim1_4K = false;
          NotificarModeloBIOEventArgs e = new NotificarModeloBIOEventArgs(4, this._rep.RepId);
          if (this.OnNotificarModeloBIO != null)
            this.OnNotificarModeloBIO((object) this, e);
        }
        else if (respostaInformacaoBio.Modelo == (byte) 60)
        {
          this._placaFim1_4K = true;
          NotificarModeloBIOEventArgs e = new NotificarModeloBIOEventArgs(5, this._rep.RepId);
          if (this.OnNotificarModeloBIO != null)
            this.OnNotificarModeloBIO((object) this, e);
        }
        else
        {
          this._placaFim1_4K = true;
          NotificarModeloBIOEventArgs e = new NotificarModeloBIOEventArgs(2, this._rep.RepId);
          if (this.OnNotificarModeloBIO != null)
            this.OnNotificarModeloBIO((object) this, e);
        }
      }
      return respostaInformacaoBio;
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
      this._msgRespostaPacoteTemplates1_4K = new MsgTcpAplicacaoRespostaPacoteUsuarioBio1_4K();
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

    private void AbrirMsgRespostaSolicitacaoTemplateUsuario(Envelope envelope)
    {
      this._msgRespostaTemplateUsuario = new MsgTcpAplicacaoRespostaUsuarioBio();
      byte[] aplicacaoEmBytes = envelope.MsgAplicacaoEmBytes;
      this._msgRespostaTemplateUsuario.Resultado = aplicacaoEmBytes[2];
      byte[] numArray = new byte[10];
      Array.Copy((Array) aplicacaoEmBytes, 5, (Array) numArray, 0, numArray.Length);
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
      this._msgRespostaTemplateUsuario.NumUsuario = stringBuilder.ToString();
      if (this._msgRespostaTemplateUsuario.Resultado != (byte) 1)
        return;
      this._msgRespostaTemplateUsuario.TipoUsuario = aplicacaoEmBytes[4];
      Array.Copy((Array) aplicacaoEmBytes, 15, (Array) this._msgRespostaTemplateUsuario.SenhaUsuario, 0, this._msgRespostaTemplateUsuario.SenhaUsuario.Length);
      Array.Copy((Array) aplicacaoEmBytes, 31, (Array) this._msgRespostaTemplateUsuario.Template1, 0, this._msgRespostaTemplateUsuario.Template1.Length);
      Array.Copy((Array) aplicacaoEmBytes, 431, (Array) this._msgRespostaTemplateUsuario.Template2, 0, this._msgRespostaTemplateUsuario.Template2.Length);
      this._lstMsgRespostaUsuarioBio.Add(this._msgRespostaTemplateUsuario);
    }

    private void AbrirMsgRespostaSolicitacaoTemplateUsuario1_4K(Envelope envelope)
    {
      this._msgRespostaTemplateUsuario1_4K = new MsgTcpAplicacaoRespostaUsuarioBio1_4K();
      byte[] aplicacaoEmBytes = envelope.MsgAplicacaoEmBytes;
      this._msgRespostaTemplateUsuario1_4K.Resultado = aplicacaoEmBytes[2];
      byte[] numArray = new byte[11];
      Array.Copy((Array) aplicacaoEmBytes, 5, (Array) numArray, 0, numArray.Length);
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
      this._msgRespostaTemplateUsuario1_4K.TipoUsuario = aplicacaoEmBytes[4];
      Array.Copy((Array) aplicacaoEmBytes, 16, (Array) this._msgRespostaTemplateUsuario1_4K.SenhaUsuario, 0, this._msgRespostaTemplateUsuario1_4K.SenhaUsuario.Length);
      Array.Copy((Array) aplicacaoEmBytes, 32, (Array) this._msgRespostaTemplateUsuario1_4K.Template1, 0, this._msgRespostaTemplateUsuario1_4K.Template1.Length);
      Array.Copy((Array) aplicacaoEmBytes, 436, (Array) this._msgRespostaTemplateUsuario1_4K.Template2, 0, this._msgRespostaTemplateUsuario1_4K.Template2.Length);
      this._lstMsgRespostaUsuarioBio1_4K.Add(this._msgRespostaTemplateUsuario1_4K);
    }

    private void AssociarEmpregadoBio()
    {
      List<UsuarioBio> lstUsuariosBio = this._msgRespostaPacoteTemplates.LstUsuariosBio;
      if (this._chamadaSdk)
      {
        foreach (UsuarioBio usuarioBio in lstUsuariosBio)
          this._listaUsuario.Add(new UsuarioBio()
          {
            IdBiometria = ulong.Parse(usuarioBio.IdBiometria)
          });
      }
      else
      {
        Empregado empregado1 = new Empregado();
        foreach (UsuarioBio usuarioBio1 in lstUsuariosBio)
        {
          ulong num = ulong.Parse(usuarioBio1.IdBiometria);
          Empregado empregado2 = new Empregado();
          new Empregado().EmpregadorId = this._empregador.EmpregadorId;
          Empregado empregado3 = empregado2.PesquisarEmpregadoPorCartao(this._empregador.EmpregadorId, (long) num);
          UsuarioBio usuarioBio2 = empregado1.AssociarEmpregadoBio((ulong) empregado3.EmpregadoId, this._empregador.EmpregadorId);
          usuarioBio2.IdBiometria = num;
          if (usuarioBio2.Nome == "")
            usuarioBio2.Nome = Resources.msgNOME_NAO_CADASTRADO;
          this._listaUsuario.Add(usuarioBio2);
        }
      }
    }

    private void AssociarEmpregadoBio1_4K()
    {
      List<UsuarioBio> lstUsuariosBio = this._msgRespostaPacoteTemplates1_4K.LstUsuariosBio;
      if (this._chamadaSdk)
      {
        foreach (UsuarioBio usuarioBio in lstUsuariosBio)
          this._listaUsuario.Add(new UsuarioBio()
          {
            IdBiometria = ulong.Parse(usuarioBio.IdBiometria)
          });
      }
      else
      {
        Empregado empregado1 = new Empregado();
        try
        {
          foreach (UsuarioBio usuarioBio1 in lstUsuariosBio)
          {
            ulong num = ulong.Parse(usuarioBio1.IdBiometria);
            Empregado empregado2 = new Empregado();
            new Empregado().EmpregadorId = this._empregador.EmpregadorId;
            Empregado empregado3 = empregado2.PesquisarEmpregadoPorCartao(this._empregador.EmpregadorId, (long) num);
            UsuarioBio usuarioBio2 = empregado1.AssociarEmpregadoBio((ulong) empregado3.EmpregadoId, this._empregador.EmpregadorId);
            usuarioBio2.IdBiometria = num;
            if (usuarioBio2.Nome == "")
              usuarioBio2.Nome = Resources.msgNOME_NAO_CADASTRADO;
            this._listaUsuario.Add(usuarioBio2);
          }
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

    private byte[] AbrirIdUsuarioEmBytes(string idUsuario)
    {
      byte[] numArray = new byte[8];
      int index1 = 0;
      int length = idUsuario.Length;
      for (int index2 = 0; index2 < length; index2 += 2)
      {
        string s = idUsuario[index2].ToString() + idUsuario[index2 + 1].ToString();
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
        case GerenciadorTemplatesBio.Estados.estSolicitacaoExclusaoUsuarioBio:
          num = ulong.Parse(this.DecrementaUmByteporByte(this._queueMsgSolicitExclusaoUsuarioBio.Peek().NumUsuario));
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoTemplateUsuarioBio:
          num = ulong.Parse(this.DecrementaUmByteporByte(this._queueMsgSolicitUsuarioBio.Peek().NumUsuario));
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoInclusaoUsuarioBio:
          num = ulong.Parse(this.DecrementaUmByteporByte(this._queueMsgSolicitInclusaoUsuarioBio.Peek().NumUsuario));
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoExclusaoUsuarioBio1_4K:
          num = ulong.Parse(this.DecrementaUmByteporByte(this._queueMsgSolicitExclusaoUsuarioBio1_4K.Peek().NumUsuario));
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoTemplateUsuarioBio1_4K:
          num = ulong.Parse(this.DecrementaUmByteporByte(this._queueMsgSolicitUsuarioBio1_4K.Peek().NumUsuario));
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoInclusaoUsuarioBio1_4K:
          num = ulong.Parse(this.DecrementaUmByteporByte(this._queueMsgSolicitInclusaoUsuarioBio1_4K.Peek().NumUsuario));
          break;
      }
      if (this._estadoRep == GerenciadorTemplatesBio.Estados.estSolicitacaoInclusaoUsuarioBio || this._estadoRep == GerenciadorTemplatesBio.Estados.estSolicitacaoInclusaoUsuarioBio1_4K)
      {
        foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this._listaUsuarioNoDb)
        {
          if ((long) usuarioBio.IdBiometria == (long) num)
          {
            usuarioBio.Status = this.ExtrairRespostaBio(resOperacao);
            usuarioBio.IdResultado = (int) resOperacao;
            break;
          }
        }
      }
      else
      {
        foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this._listaUsuario)
        {
          if ((long) usuarioBio.IdBiometria == (long) num)
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
      foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this._listaUsuario)
      {
        if ((long) usuarioBio.IdBiometria == (long) num)
        {
          this._listaUsuario.RemoveAt(index);
          break;
        }
        ++index;
      }
    }

    public void CarregaListaSdkParaProcessar(
      ProcessoTemplates tipoProcessoTemplates,
      List<UsuarioBio> listaBio)
    {
      this._listaUsuario = new SortableBindingList<UsuarioBio>();
      this._listaUsuarioNoDb = new SortableBindingList<UsuarioBio>();
      switch (tipoProcessoTemplates)
      {
        case ProcessoTemplates.excluirTemplates:
          foreach (UsuarioBio usuarioBio in listaBio)
            this._listaUsuario.Add(usuarioBio);
          this.CarregarMsgSolicitExclusaoUsuarioBio();
          break;
        case ProcessoTemplates.importarTemplates:
          foreach (UsuarioBio usuarioBio in listaBio)
            this._listaUsuario.Add(usuarioBio);
          this.CarregarMsgSolicitUsuarioBio();
          break;
        case ProcessoTemplates.exportarTemplates:
          foreach (UsuarioBio usuarioBio in listaBio)
            this._listaUsuarioNoDb.Add(usuarioBio);
          this.CarregarMsgSolicitInclusaoUsuarioBio();
          break;
      }
    }

    public void CarregarMsgSolicitExclusaoUsuarioBio()
    {
      this._queueMsgSolicitExclusaoUsuarioBio = new Queue<MsgTcpAplicacaoSolicitaExclusaoUsuarioBioRep>();
      this._queueMsgSolicitExclusaoUsuarioBio1_4K = new Queue<MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4K>();
      foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this._listaUsuario)
      {
        if (usuarioBio.Selecionado || this._chamadaSdk)
        {
          MsgTcpAplicacaoSolicitaExclusaoUsuarioBioRep exclusaoUsuarioBioRep = new MsgTcpAplicacaoSolicitaExclusaoUsuarioBioRep();
          exclusaoUsuarioBioRep.Leitor = (byte) 0;
          exclusaoUsuarioBioRep.NumUsuario = usuarioBio.IdBiometria.ToString().PadLeft(16, '0');
          StringBuilder stringBuilder1 = new StringBuilder();
          byte[] numArray = new byte[8];
          foreach (int abrirIdUsuarioEmByte in this.AbrirIdUsuarioEmBytes(exclusaoUsuarioBioRep.NumUsuario))
          {
            int num = abrirIdUsuarioEmByte + 1;
            stringBuilder1.Append(num.ToString("x").PadLeft(2, '0'));
          }
          exclusaoUsuarioBioRep.NumUsuario = stringBuilder1.ToString();
          this._queueMsgSolicitExclusaoUsuarioBio.Enqueue(exclusaoUsuarioBioRep);
          MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4K exclusaoUsuarioBio14K = new MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4K();
          exclusaoUsuarioBio14K.Leitor = (byte) 0;
          exclusaoUsuarioBio14K.NumUsuario = usuarioBio.IdBiometria.ToString().PadLeft(16, '0');
          StringBuilder stringBuilder2 = new StringBuilder();
          numArray = new byte[8];
          foreach (int abrirIdUsuarioEmByte in this.AbrirIdUsuarioEmBytes(exclusaoUsuarioBio14K.NumUsuario))
          {
            int num = abrirIdUsuarioEmByte + 1;
            stringBuilder2.Append(num.ToString("x").PadLeft(2, '0'));
          }
          exclusaoUsuarioBio14K.NumUsuario = stringBuilder2.ToString();
          this._queueMsgSolicitExclusaoUsuarioBio1_4K.Enqueue(exclusaoUsuarioBio14K);
        }
      }
    }

    public void CarregarMsgSolicitInclusaoUsuarioBio()
    {
      this._queueMsgSolicitInclusaoUsuarioBio = new Queue<MsgTcpAplicacaoSolicitaInclusaoUsuarioBioRep>();
      this._queueMsgSolicitInclusaoUsuarioBio1_4K = new Queue<MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4K>();
      byte[] numArray;
      foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this._listaUsuarioNoDb)
      {
        if (usuarioBio.Selecionado || this._chamadaSdk)
        {
          if (!this._placaFim1_4K)
          {
            MsgTcpAplicacaoSolicitaInclusaoUsuarioBioRep inclusaoUsuarioBioRep = new MsgTcpAplicacaoSolicitaInclusaoUsuarioBioRep();
            inclusaoUsuarioBioRep.Leitor = (byte) 0;
            inclusaoUsuarioBioRep.NumUsuario = usuarioBio.IdBiometria.ToString().PadLeft(16, '0');
            StringBuilder stringBuilder = new StringBuilder();
            numArray = new byte[8];
            foreach (int abrirIdUsuarioEmByte in this.AbrirIdUsuarioEmBytes(inclusaoUsuarioBioRep.NumUsuario))
            {
              int num = abrirIdUsuarioEmByte + 1;
              stringBuilder.Append(num.ToString("x").PadLeft(2, '0'));
            }
            inclusaoUsuarioBioRep.NumUsuario = stringBuilder.ToString();
            inclusaoUsuarioBioRep.TipoUsuario = (byte) 0;
            inclusaoUsuarioBioRep.SenhaUsuario = this.AbrirSenhaEmBytes(usuarioBio.Senha);
            try
            {
              TemplatesBio tempBioEnt = new TemplatesBio();
              TemplatesBio templatesBio1 = new TemplatesBio();
              tempBioEnt.Template1 = usuarioBio.Template1;
              tempBioEnt.Template2 = usuarioBio.Template2;
              TemplatesBio templatesBio2 = BSP_BI.ConverteTemp2030para3030(tempBioEnt);
              inclusaoUsuarioBioRep.Template1 = this.AbrirTemplateEmBytes(templatesBio2.Template1);
              inclusaoUsuarioBioRep.Template2 = this.AbrirTemplateEmBytes(templatesBio2.Template2);
            }
            catch (Exception ex)
            {
            }
            this._queueMsgSolicitInclusaoUsuarioBio.Enqueue(inclusaoUsuarioBioRep);
          }
          else
          {
            MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4K inclusaoUsuarioBio14K = new MsgTcpAplicacaoSolicitaInclusaoUsuarioBio1_4K();
            inclusaoUsuarioBio14K.Leitor = (byte) 0;
            inclusaoUsuarioBio14K.NumUsuario = usuarioBio.IdBiometria.ToString().PadLeft(16, '0');
            StringBuilder stringBuilder = new StringBuilder();
            numArray = new byte[8];
            foreach (int abrirIdUsuarioEmByte in this.AbrirIdUsuarioEmBytes(inclusaoUsuarioBio14K.NumUsuario))
            {
              int num = abrirIdUsuarioEmByte + 1;
              stringBuilder.Append(num.ToString("x").PadLeft(2, '0'));
            }
            inclusaoUsuarioBio14K.NumUsuario = stringBuilder.ToString();
            inclusaoUsuarioBio14K.TipoUsuario = (byte) 0;
            inclusaoUsuarioBio14K.SenhaUsuario = this.AbrirSenhaEmBytes(usuarioBio.Senha);
            inclusaoUsuarioBio14K.Template1 = this.AbrirTemplateEmBytes1_4K(usuarioBio.Template1);
            inclusaoUsuarioBio14K.Template2 = this.AbrirTemplateEmBytes1_4K(usuarioBio.Template2);
            this._queueMsgSolicitInclusaoUsuarioBio1_4K.Enqueue(inclusaoUsuarioBio14K);
          }
        }
      }
    }

    public void CarregarMsgSolicitUsuarioBio()
    {
      this._queueMsgSolicitUsuarioBio = new Queue<MsgTcpAplicacaoSolicitaUsuarioBioRep>();
      this._queueMsgSolicitUsuarioBio1_4K = new Queue<MsgTcpAplicacaoSolicitaUsuarioBio1_4K>();
      foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this._listaUsuario)
      {
        if (usuarioBio.Selecionado || this._chamadaSdk)
        {
          MsgTcpAplicacaoSolicitaUsuarioBioRep solicitaUsuarioBioRep = new MsgTcpAplicacaoSolicitaUsuarioBioRep();
          solicitaUsuarioBioRep.Leitor = (byte) 0;
          solicitaUsuarioBioRep.NumUsuario = usuarioBio.IdBiometria.ToString().PadLeft(16, '0');
          StringBuilder stringBuilder1 = new StringBuilder();
          byte[] numArray = new byte[8];
          foreach (int abrirIdUsuarioEmByte in this.AbrirIdUsuarioEmBytes(solicitaUsuarioBioRep.NumUsuario))
          {
            int num = abrirIdUsuarioEmByte + 1;
            stringBuilder1.Append(num.ToString("x").PadLeft(2, '0'));
          }
          solicitaUsuarioBioRep.NumUsuario = stringBuilder1.ToString();
          this._queueMsgSolicitUsuarioBio.Enqueue(solicitaUsuarioBioRep);
          MsgTcpAplicacaoSolicitaUsuarioBio1_4K solicitaUsuarioBio14K = new MsgTcpAplicacaoSolicitaUsuarioBio1_4K();
          solicitaUsuarioBio14K.Leitor = (byte) 0;
          solicitaUsuarioBio14K.NumUsuario = usuarioBio.IdBiometria.ToString().PadLeft(16, '0');
          StringBuilder stringBuilder2 = new StringBuilder();
          numArray = new byte[8];
          foreach (int abrirIdUsuarioEmByte in this.AbrirIdUsuarioEmBytes(solicitaUsuarioBio14K.NumUsuario))
          {
            int num = abrirIdUsuarioEmByte + 1;
            stringBuilder2.Append(num.ToString("x").PadLeft(2, '0'));
          }
          solicitaUsuarioBio14K.NumUsuario = stringBuilder2.ToString();
          this._queueMsgSolicitUsuarioBio1_4K.Enqueue(solicitaUsuarioBio14K);
        }
      }
    }

    private void GravarTemplates()
    {
      if (this._chamadaSdk)
      {
        this._listaUsuario.Clear();
        foreach (MsgTcpAplicacaoRespostaUsuarioBio respostaUsuarioBio in this._lstMsgRespostaUsuarioBio)
        {
          UsuarioBio usuarioBio = new UsuarioBio();
          usuarioBio.IdBiometria = (ulong) long.Parse(respostaUsuarioBio.NumUsuario);
          TemplatesBio templatesBio1 = new TemplatesBio();
          TemplatesBio tempBioEnt = new TemplatesBio();
          StringBuilder stringBuilder1 = new StringBuilder();
          foreach (byte num in respostaUsuarioBio.Template1)
          {
            string str = num.ToString("x");
            if (str.Length == 1)
              str = "0" + str;
            stringBuilder1.Append(str);
          }
          tempBioEnt.Template1 = stringBuilder1.ToString();
          StringBuilder stringBuilder2 = new StringBuilder();
          foreach (byte num in respostaUsuarioBio.Template2)
          {
            string str = num.ToString("x");
            if (str.Length == 1)
              str = "0" + str;
            stringBuilder2.Append(str);
          }
          tempBioEnt.Template2 = stringBuilder2.ToString();
          TemplatesBio templatesBio2 = BSP_BI.ConverteTemp3030para2030(tempBioEnt);
          usuarioBio.Template1 = templatesBio2.Template1;
          usuarioBio.Template2 = templatesBio2.Template2;
          usuarioBio.TipoTemplate = 2;
          this._listaUsuario.Add(usuarioBio);
        }
      }
      else
      {
        TemplateBio templateBio1 = new TemplateBio();
        try
        {
          foreach (MsgTcpAplicacaoRespostaUsuarioBio respostaUsuarioBio in this._lstMsgRespostaUsuarioBio)
          {
            TemplatesBio templateBio2 = new TemplatesBio();
            long cartao = long.Parse(respostaUsuarioBio.NumUsuario);
            Empregado empregado1 = new Empregado();
            new Empregado().EmpregadorId = this._empregador.EmpregadorId;
            Empregado empregado2 = empregado1.PesquisarEmpregadoPorCartao(this._empregador.EmpregadorId, cartao);
            templateBio2.EmpregadoID = empregado2.EmpregadoId;
            templateBio2.IdBiometrico = cartao;
            StringBuilder stringBuilder3 = new StringBuilder();
            foreach (byte num in respostaUsuarioBio.SenhaUsuario)
            {
              string str = num.ToString("x");
              if (str.Length == 1)
                str = "0" + str;
              stringBuilder3.Append(str);
            }
            templateBio2.Senha = stringBuilder3.ToString();
            TemplatesBio templatesBio3 = new TemplatesBio();
            TemplatesBio tempBioEnt = new TemplatesBio();
            StringBuilder stringBuilder4 = new StringBuilder();
            foreach (byte num in respostaUsuarioBio.Template1)
            {
              string str = num.ToString("x");
              if (str.Length == 1)
                str = "0" + str;
              stringBuilder4.Append(str);
            }
            tempBioEnt.Template1 = stringBuilder4.ToString();
            StringBuilder stringBuilder5 = new StringBuilder();
            foreach (byte num in respostaUsuarioBio.Template2)
            {
              string str = num.ToString("x");
              if (str.Length == 1)
                str = "0" + str;
              stringBuilder5.Append(str);
            }
            tempBioEnt.Template2 = stringBuilder5.ToString();
            TemplatesBio templatesBio4 = BSP_BI.ConverteTemp3030para2030(tempBioEnt);
            templateBio2.Template1 = templatesBio4.Template1;
            templateBio2.Template2 = templatesBio4.Template2;
            templateBio2.EmpregadorID = this._empregador.EmpregadorId;
            templateBio2.TipoTemplate = 2;
            if (templateBio2.EmpregadoID == 0)
              templateBio1.ExcluirTemplatesIdBiometria(templateBio2, this._empregador.EmpregadorId);
            else
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

    private void GravarTemplates(
      MsgTcpAplicacaoRespostaUsuarioBio _lstMsgRespostaUsuarioBio)
    {
      if (this._chamadaSdk)
      {
        UsuarioBio usuarioBio = new UsuarioBio();
        usuarioBio.IdBiometria = (ulong) long.Parse(_lstMsgRespostaUsuarioBio.NumUsuario);
        TemplatesBio templatesBio1 = new TemplatesBio();
        TemplatesBio tempBioEnt = new TemplatesBio();
        StringBuilder stringBuilder1 = new StringBuilder();
        foreach (byte num in _lstMsgRespostaUsuarioBio.Template1)
        {
          string str = num.ToString("x");
          if (str.Length == 1)
            str = "0" + str;
          stringBuilder1.Append(str);
        }
        tempBioEnt.Template1 = stringBuilder1.ToString();
        StringBuilder stringBuilder2 = new StringBuilder();
        foreach (byte num in _lstMsgRespostaUsuarioBio.Template2)
        {
          string str = num.ToString("x");
          if (str.Length == 1)
            str = "0" + str;
          stringBuilder2.Append(str);
        }
        tempBioEnt.Template2 = stringBuilder2.ToString();
        TemplatesBio templatesBio2 = BSP_BI.ConverteTemp3030para2030(tempBioEnt);
        usuarioBio.Template1 = templatesBio2.Template1;
        usuarioBio.Template2 = templatesBio2.Template2;
        usuarioBio.TipoTemplate = 2;
        this._listaUsuario.Add(usuarioBio);
      }
      else
      {
        TemplateBio templateBio1 = new TemplateBio();
        try
        {
          TemplatesBio templateBio2 = new TemplatesBio();
          long cartao = long.Parse(_lstMsgRespostaUsuarioBio.NumUsuario);
          Empregado empregado1 = new Empregado();
          new Empregado().EmpregadorId = this._empregador.EmpregadorId;
          Empregado empregado2 = empregado1.PesquisarEmpregadoPorCartao(this._empregador.EmpregadorId, cartao);
          templateBio2.EmpregadoID = empregado2.EmpregadoId;
          templateBio2.IdBiometrico = cartao;
          StringBuilder stringBuilder3 = new StringBuilder();
          foreach (byte num in _lstMsgRespostaUsuarioBio.SenhaUsuario)
          {
            string str = num.ToString("x");
            if (str.Length == 1)
              str = "0" + str;
            stringBuilder3.Append(str);
          }
          templateBio2.Senha = stringBuilder3.ToString();
          TemplatesBio templatesBio3 = new TemplatesBio();
          TemplatesBio tempBioEnt = new TemplatesBio();
          StringBuilder stringBuilder4 = new StringBuilder();
          foreach (byte num in _lstMsgRespostaUsuarioBio.Template1)
          {
            string str = num.ToString("x");
            if (str.Length == 1)
              str = "0" + str;
            stringBuilder4.Append(str);
          }
          tempBioEnt.Template1 = stringBuilder4.ToString();
          StringBuilder stringBuilder5 = new StringBuilder();
          foreach (byte num in _lstMsgRespostaUsuarioBio.Template2)
          {
            string str = num.ToString("x");
            if (str.Length == 1)
              str = "0" + str;
            stringBuilder5.Append(str);
          }
          tempBioEnt.Template2 = stringBuilder5.ToString();
          TemplatesBio templatesBio4 = BSP_BI.ConverteTemp3030para2030(tempBioEnt);
          templateBio2.Template1 = templatesBio4.Template1;
          templateBio2.Template2 = templatesBio4.Template2;
          templateBio2.EmpregadorID = this._empregador.EmpregadorId;
          templateBio2.TipoTemplate = 2;
          if (templateBio2.EmpregadoID == 0)
            templateBio1.ExcluirTemplatesIdBiometria(templateBio2, this._empregador.EmpregadorId);
          else
            templateBio1.ExcluirTemplates(templateBio2, this._empregador.EmpregadorId, "TemplatesNitgen");
          templateBio1.InserirTemplates(templateBio2, "TemplatesNitgen");
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

    private void GravarTemplates1_4K()
    {
      if (this._chamadaSdk)
      {
        this._listaUsuario.Clear();
        foreach (MsgTcpAplicacaoRespostaUsuarioBio1_4K respostaUsuarioBio14K in this._lstMsgRespostaUsuarioBio1_4K)
        {
          UsuarioBio usuarioBio = new UsuarioBio();
          usuarioBio.IdBiometria = (ulong) long.Parse(respostaUsuarioBio14K.NumUsuario);
          StringBuilder stringBuilder1 = new StringBuilder();
          foreach (byte num in respostaUsuarioBio14K.Template1)
          {
            string str = num.ToString("x");
            if (str.Length == 1)
              str = "0" + str;
            stringBuilder1.Append(str);
          }
          usuarioBio.Template1 = stringBuilder1.ToString();
          StringBuilder stringBuilder2 = new StringBuilder();
          foreach (byte num in respostaUsuarioBio14K.Template2)
          {
            string str = num.ToString("x");
            if (str.Length == 1)
              str = "0" + str;
            stringBuilder2.Append(str);
          }
          usuarioBio.Template2 = stringBuilder2.ToString();
          usuarioBio.TipoTemplate = 2;
          this._listaUsuario.Add(usuarioBio);
        }
      }
      else
      {
        TemplateBio templateBio1 = new TemplateBio();
        try
        {
          foreach (MsgTcpAplicacaoRespostaUsuarioBio1_4K respostaUsuarioBio14K in this._lstMsgRespostaUsuarioBio1_4K)
          {
            TemplatesBio templateBio2 = new TemplatesBio();
            long cartao = long.Parse(respostaUsuarioBio14K.NumUsuario);
            Empregado empregado1 = new Empregado();
            new Empregado().EmpregadorId = this._empregador.EmpregadorId;
            Empregado empregado2 = empregado1.PesquisarEmpregadoPorCartao(this._empregador.EmpregadorId, cartao);
            templateBio2.EmpregadoID = empregado2.EmpregadoId;
            templateBio2.IdBiometrico = cartao;
            StringBuilder stringBuilder3 = new StringBuilder();
            foreach (byte num in respostaUsuarioBio14K.SenhaUsuario)
            {
              string str = num.ToString("x");
              if (str.Length == 1)
                str = "0" + str;
              stringBuilder3.Append(str);
            }
            templateBio2.Senha = stringBuilder3.ToString();
            StringBuilder stringBuilder4 = new StringBuilder();
            foreach (byte num in respostaUsuarioBio14K.Template1)
            {
              string str = num.ToString("x");
              if (str.Length == 1)
                str = "0" + str;
              stringBuilder4.Append(str);
            }
            templateBio2.Template1 = stringBuilder4.ToString();
            StringBuilder stringBuilder5 = new StringBuilder();
            foreach (byte num in respostaUsuarioBio14K.Template2)
            {
              string str = num.ToString("x");
              if (str.Length == 1)
                str = "0" + str;
              stringBuilder5.Append(str);
            }
            templateBio2.Template2 = stringBuilder5.ToString();
            templateBio2.EmpregadorID = this._empregador.EmpregadorId;
            templateBio2.TipoTemplate = 2;
            if (templateBio2.EmpregadoID == 0)
              templateBio1.ExcluirTemplatesIdBiometria(templateBio2, this._empregador.EmpregadorId);
            else
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
      MsgTcpAplicacaoRespostaUsuarioBio1_4K _lstMsgRespostaUsuarioBio1_4K)
    {
      if (this._chamadaSdk)
      {
        UsuarioBio usuarioBio = new UsuarioBio();
        usuarioBio.IdBiometria = (ulong) long.Parse(_lstMsgRespostaUsuarioBio1_4K.NumUsuario);
        StringBuilder stringBuilder1 = new StringBuilder();
        foreach (byte num in _lstMsgRespostaUsuarioBio1_4K.Template1)
        {
          string str = num.ToString("x");
          if (str.Length == 1)
            str = "0" + str;
          stringBuilder1.Append(str);
        }
        usuarioBio.Template1 = stringBuilder1.ToString();
        StringBuilder stringBuilder2 = new StringBuilder();
        foreach (byte num in _lstMsgRespostaUsuarioBio1_4K.Template2)
        {
          string str = num.ToString("x");
          if (str.Length == 1)
            str = "0" + str;
          stringBuilder2.Append(str);
        }
        usuarioBio.Template2 = stringBuilder2.ToString();
        usuarioBio.TipoTemplate = 2;
        this._listaUsuario.Add(usuarioBio);
      }
      else
      {
        TemplateBio templateBio1 = new TemplateBio();
        try
        {
          TemplatesBio templateBio2 = new TemplatesBio();
          long cartao = long.Parse(_lstMsgRespostaUsuarioBio1_4K.NumUsuario);
          Empregado empregado1 = new Empregado();
          new Empregado().EmpregadorId = this._empregador.EmpregadorId;
          Empregado empregado2 = empregado1.PesquisarEmpregadoPorCartao(this._empregador.EmpregadorId, cartao);
          templateBio2.EmpregadoID = empregado2.EmpregadoId;
          templateBio2.IdBiometrico = cartao;
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
          StringBuilder stringBuilder5 = new StringBuilder();
          foreach (byte num in _lstMsgRespostaUsuarioBio1_4K.Template2)
          {
            string str = num.ToString("x");
            if (str.Length == 1)
              str = "0" + str;
            stringBuilder5.Append(str);
          }
          templateBio2.Template2 = stringBuilder5.ToString();
          templateBio2.EmpregadorID = this._empregador.EmpregadorId;
          templateBio2.TipoTemplate = 2;
          if (templateBio2.EmpregadoID == 0)
            templateBio1.ExcluirTemplatesIdBiometria(templateBio2, this._empregador.EmpregadorId);
          else
            templateBio1.ExcluirTemplates(templateBio2, this._empregador.EmpregadorId, "TemplatesNitgen");
          templateBio1.InserirTemplates(templateBio2, "TemplatesNitgen");
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

    public void CarregaListaSdkProcessada(
      ref List<UsuarioBio> listaBio,
      ProcessoTemplates tipoProcessoTemplates)
    {
      listaBio.Clear();
      if (tipoProcessoTemplates == ProcessoTemplates.exportarTemplates)
      {
        foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this._listaUsuarioNoDb)
          listaBio.Add(usuarioBio);
      }
      else
      {
        foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this._listaUsuario)
          listaBio.Add(usuarioBio);
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
      {
        switch (this._tipoProcessoTemplates)
        {
          case ProcessoTemplates.excluirTemplates:
            this.CarregarMsgSolicitExclusaoUsuarioBio();
            if (this._queueMsgSolicitExclusaoUsuarioBio.Count > 0)
            {
              this._totTemplatesParaProcessar = this._queueMsgSolicitExclusaoUsuarioBio.Count;
              break;
            }
            if (this._queueMsgSolicitExclusaoUsuarioBio1_4K.Count > 0)
            {
              this._totTemplatesParaProcessar = this._queueMsgSolicitExclusaoUsuarioBio1_4K.Count;
              break;
            }
            break;
          case ProcessoTemplates.importarTemplates:
            this.CarregarMsgSolicitUsuarioBio();
            if (this._queueMsgSolicitUsuarioBio.Count > 0)
              this._totTemplatesParaProcessar = this._queueMsgSolicitUsuarioBio.Count;
            else if (this._queueMsgSolicitUsuarioBio1_4K.Count > 0)
              this._totTemplatesParaProcessar = this._queueMsgSolicitUsuarioBio1_4K.Count;
            if (this._chamadaSdk)
            {
              this._listaUsuario.Clear();
              break;
            }
            break;
          case ProcessoTemplates.exportarTemplates:
            this.CarregarMsgSolicitInclusaoUsuarioBio();
            if (this._queueMsgSolicitInclusaoUsuarioBio.Count > 0)
            {
              this._totTemplatesParaProcessar = this._queueMsgSolicitInclusaoUsuarioBio.Count;
              break;
            }
            if (this._queueMsgSolicitInclusaoUsuarioBio1_4K.Count > 0)
            {
              this._totTemplatesParaProcessar = this._queueMsgSolicitInclusaoUsuarioBio1_4K.Count;
              break;
            }
            break;
        }
        this.EnviarSolicitacaoInfo();
      }
      else
        this.EncerrarConexao();
    }

    public override void IniciarProcesso()
    {
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorTemplatesBio.Estados.estSolicitacaoInfo:
          menssagem = Resources.msgTIMEOUT_SOLICIT_INFO_BIO;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoPacoteTemplates:
          menssagem = Resources.msgTIMEOUT_SOLICIT_TEMPLATE;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoExclusaoUsuarioBio:
          menssagem = Resources.msgTIMEOUT_SOLICIT_EXCLUSAO_TEMPLATE;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoTemplateUsuarioBio:
          menssagem = Resources.msgTIMEOUT_SOLICIT_TEMPLATE_USUARIO;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoInclusaoUsuarioBio:
          menssagem = Resources.msgTIMEOUT_SOLICIT_INCLUSAO_TEMPLATE;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoPacoteTemplates1_4K:
          menssagem = Resources.msgTIMEOUT_SOLICIT_TEMPLATE;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoExclusaoUsuarioBio1_4K:
          menssagem = Resources.msgTIMEOUT_SOLICIT_EXCLUSAO_TEMPLATE;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoTemplateUsuarioBio1_4K:
          menssagem = Resources.msgTIMEOUT_SOLICIT_TEMPLATE_USUARIO;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoInclusaoUsuarioBio1_4K:
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
        case GerenciadorTemplatesBio.Estados.estSolicitacaoInfo:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_INFO_BIO;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoPacoteTemplates:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_TEMPLATE;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoExclusaoUsuarioBio:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_EXCLUSAO_TEMPLATE;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoTemplateUsuarioBio:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_TEMPLATE_USUARIO;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoInclusaoUsuarioBio:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_INCLUSAO_TEMPLATE;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoPacoteTemplates1_4K:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_TEMPLATE;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoExclusaoUsuarioBio1_4K:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_EXCLUSAO_TEMPLATE;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoTemplateUsuarioBio1_4K:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_TEMPLATE_USUARIO;
          break;
        case GerenciadorTemplatesBio.Estados.estSolicitacaoInclusaoUsuarioBio1_4K:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_INCLUSAO_TEMPLATE;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estSolicitacaoInfo,
      estSolicitacaoPacoteTemplates,
      estSolicitacaoExclusaoUsuarioBio,
      estSolicitacaoTemplateUsuarioBio,
      estSolicitacaoInclusaoUsuarioBio,
      estSolicitacaoPacoteTemplates1_4K,
      estSolicitacaoExclusaoUsuarioBio1_4K,
      estSolicitacaoTemplateUsuarioBio1_4K,
      estSolicitacaoInclusaoUsuarioBio1_4K,
    }
  }
}
