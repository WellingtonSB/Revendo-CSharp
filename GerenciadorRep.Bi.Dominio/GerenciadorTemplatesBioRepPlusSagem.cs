// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorTemplatesBioRepPlusSagem
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorTemplatesBioRepPlusSagem : TarefasBioAbstract
  {
    private ConfiguracoesGerais configEnt = new ConfiguracoesGerais();
    private bool _chamadaSdk;
    private int _modeloBioSdk;
    private List<UsuarioBio> listaBioSdk = new List<UsuarioBio>();
    private RepBase rep;
    private Empregador _empregador;
    private Queue<MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4KRepPlus> queueMsgSolicitExclusaoUsuarioBio;
    private Queue<MsgTcpAplicacaoSolicitaInclusaoUsuarioBioRepPlusSagem> queueMsgSolicitInclusaoUsuarioBio;
    private Queue<MsgTcpAplicacaoSolicitaUsuarioBioRepRepPlus> queueMsgSolicitUsuarioBio;
    private List<MsgTcpAplicacaoRespostaUsuarioBioSagemRepPlus> lstMsgRespostaUsuarioBio;
    private MsgTcpAplicacaoRespostaPacoteUsuarioBio msgRespostaPacoteTemplates;
    private MsgTcpAplicacaoRespostaUsuarioBio msgRespostaTemplateUsuario;
    private MsgTcpAplicacaoRespostaUsuarioBioSagemRepPlus msgRespostaTemplateSagemUsuario;
    private GerenciadorTemplatesBioRepPlusSagem.Estados estadoRep;
    private bool _placaFim1_4K = true;
    private ProcessoTemplates tipoProcessoTemplates;
    public static GerenciadorTemplatesBioRepPlusSagem _gerenciadorTemplatesBioRepPlus;

    public Empregador Empregador
    {
      get => this._empregador;
      set => this._empregador = value;
    }

    public static GerenciadorTemplatesBioRepPlusSagem getInstance() => GerenciadorTemplatesBioRepPlusSagem._gerenciadorTemplatesBioRepPlus != null ? GerenciadorTemplatesBioRepPlusSagem._gerenciadorTemplatesBioRepPlus : new GerenciadorTemplatesBioRepPlusSagem();

    public static GerenciadorTemplatesBioRepPlusSagem getInstance(
      RepBase rep)
    {
      return GerenciadorTemplatesBioRepPlusSagem._gerenciadorTemplatesBioRepPlus != null ? GerenciadorTemplatesBioRepPlusSagem._gerenciadorTemplatesBioRepPlus : new GerenciadorTemplatesBioRepPlusSagem(rep);
    }

    public static GerenciadorTemplatesBioRepPlusSagem getInstance(
      RepBase rep,
      int modeloBioSdk)
    {
      return GerenciadorTemplatesBioRepPlusSagem._gerenciadorTemplatesBioRepPlus != null ? GerenciadorTemplatesBioRepPlusSagem._gerenciadorTemplatesBioRepPlus : new GerenciadorTemplatesBioRepPlusSagem(rep, modeloBioSdk);
    }

    public GerenciadorTemplatesBioRepPlusSagem()
    {
    }

    public GerenciadorTemplatesBioRepPlusSagem(RepBase rep)
    {
      this.rep = rep;
      this._chamadaSdk = false;
      this._empregador = this.rep.Empregador.PesquisarEmpregadorDeUmREP(this.rep.RepId);
    }

    public GerenciadorTemplatesBioRepPlusSagem(RepBase rep, int modeloBioSdk)
    {
      this.rep = rep;
      this._chamadaSdk = true;
      this._modeloBioSdk = modeloBioSdk;
      this.ListaUsuariosBio = new SortableBindingList<UsuarioBio>();
    }

    ~GerenciadorTemplatesBioRepPlusSagem()
    {
    }

    public override void IniciarProcesso(ProcessoTemplates processoTemp)
    {
      ConfiguracoesGerais configuracoesGerais = new ConfiguracoesGerais();
      if (!this._chamadaSdk)
        this.configEnt = configuracoesGerais.PesquisarConfigGerais();
      this.tipoProcessoTemplates = processoTemp;
      this.rep.TimeoutExtra = 50000L;
      this.Conectar(this.rep);
    }

    private void EnviarSolicitacaoInfoSagem()
    {
      try
      {
        this.estadoRep = GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoInfoSagem;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaInformacaoBioRepPlus()
          {
            Info = (byte) 1,
            GrupoTemplates = (byte) 21,
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

    private void EnviarSolicitacaoPacoteTemplatesSagem(byte numPacote)
    {
      try
      {
        this.estadoRep = GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoPacoteTemplatesSagem;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaPacoteUsuarioBio1_4K_PequenoRepPlus()
          {
            NumPac = numPacote,
            Leitor = (byte) 0,
            Grupo = (byte) 21
          }
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoExclusaoUsuarioBio_Sagem()
    {
      try
      {
        this.estadoRep = GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoExclusaoUsuarioBioSagem;
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4KRepPlus usuarioBio14KrepPlus = this.queueMsgSolicitExclusaoUsuarioBio.Peek();
        usuarioBio14KrepPlus.Grupo = (byte) 21;
        usuarioBio14KrepPlus.CMD = (byte) 21;
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) usuarioBio14KrepPlus;
        this.ClienteSocket.Enviar(envelope, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoUsuarioBioSagem()
    {
      try
      {
        this.estadoRep = GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoTemplateUsuarioBioSagem;
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        MsgTcpAplicacaoSolicitaUsuarioBioRepRepPlus usuarioBioRepRepPlus = this.queueMsgSolicitUsuarioBio.Peek();
        usuarioBioRepRepPlus.GRP = (byte) 21;
        usuarioBioRepRepPlus.CMD = (byte) 20;
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) usuarioBioRepRepPlus;
        this.ClienteSocket.Enviar(envelope, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarSolicitacaoInclusaoUsuarioBioSagem()
    {
      try
      {
        this.estadoRep = GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoInclusaoUsuarioBioSagem;
        DateTime now = DateTime.Now;
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        MsgTcpAplicacaoSolicitaInclusaoUsuarioBioRepPlusSagem usuarioBioRepPlusSagem = this.queueMsgSolicitInclusaoUsuarioBio.Peek();
        usuarioBioRepPlusSagem.Grupo = (byte) 21;
        usuarioBioRepPlusSagem.Comando = (byte) 22;
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) usuarioBioRepPlusSagem;
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
      switch (this.estadoRep)
      {
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoInfoSagem:
          this.TratarInformacaoInfo(envelope);
          break;
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoPacoteTemplatesSagem:
          this.TratarListaUsuariosBio(envelope);
          break;
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoExclusaoUsuarioBioSagem:
          this.TratarRespostaExclusaoBio(envelope, eNotificaProgress);
          break;
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoTemplateUsuarioBioSagem:
          this.TratarSolicitacaoTemplate(envelope, eNotificaProgress);
          break;
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoInclusaoUsuarioBioSagem:
          this.TratarRespostaInclusao(envelope, eNotificaProgress);
          break;
      }
    }

    private void TratarRespostaInclusao(
      Envelope envelope,
      NotificarProgressBarEventArgs eNotificaProgress)
    {
      if (envelope.Grp != (byte) 21 || envelope.Cmd != (byte) 122)
        return;
      if (envelope.MsgAplicacaoEmBytes[2] == (byte) 1 || envelope.MsgAplicacaoEmBytes[2] == (byte) 5 || envelope.MsgAplicacaoEmBytes[2] == (byte) 4)
      {
        this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
        if (this.queueMsgSolicitInclusaoUsuarioBio.Count > 1)
        {
          this.queueMsgSolicitInclusaoUsuarioBio.Dequeue();
          Thread.Sleep(30);
          this.EnviarSolicitacaoInclusaoUsuarioBioSagem();
          eNotificaProgress = new NotificarProgressBarEventArgs(this.TotTemplatesParaProcessar);
          this.RaiseProgressBar(eNotificaProgress);
        }
        else
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgPROCESSO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this.rep.RepId, this.rep.Local);
        }
      }
      else if (envelope.MsgAplicacaoEmBytes[2] > (byte) 22)
      {
        this.EnviarSolicitacaoInclusaoUsuarioBioSagem();
      }
      else
      {
        string menssagem = envelope.MsgAplicacaoEmBytes[2] != (byte) 22 ? this.ExtrairRespostaBio(envelope.MsgAplicacaoEmBytes[2]) : Resources.msgBIO_TEMPLATE_REPITIDA;
        if (!Resources.msgBIO_OCUPADO.Equals(menssagem))
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this.rep.RepId, this.rep.Local);
        }
        else
          this.EnviarSolicitacaoInclusaoUsuarioBioSagem();
      }
    }

    private NotificarProgressBarEventArgs TratarRespostaExclusaoBio(
      Envelope envelope,
      NotificarProgressBarEventArgs eNotificaProgress)
    {
      if (envelope.Grp == (byte) 21 && envelope.Cmd == (byte) 121)
      {
        if (envelope.MsgAplicacaoEmBytes[2] == (byte) 1 || envelope.MsgAplicacaoEmBytes[2] == (byte) 5)
        {
          if (this._chamadaSdk)
            this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
          if (this.queueMsgSolicitExclusaoUsuarioBio.Count > 1)
          {
            this.queueMsgSolicitExclusaoUsuarioBio.Dequeue();
            this.EnviarSolicitacaoExclusaoUsuarioBio_Sagem();
            eNotificaProgress = new NotificarProgressBarEventArgs(this.TotTemplatesParaProcessar);
            this.RaiseProgressBar(eNotificaProgress);
          }
          else
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgEXCLUSAO_TEMPLATE_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this.rep.RepId, this.rep.Local);
          }
        }
        else
        {
          string menssagem = this.ExtrairRespostaBio(envelope.MsgAplicacaoEmBytes[2]);
          if (!Resources.msgBIO_OCUPADO.Equals(menssagem))
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this.rep.RepId, this.rep.Local);
          }
          else
            this.EnviarSolicitacaoExclusaoUsuarioBio_Sagem();
        }
      }
      return eNotificaProgress;
    }

    private void TratarListaUsuariosBio(Envelope envelope)
    {
      if (envelope.Grp != (byte) 21 || envelope.Cmd != (byte) 111)
        return;
      this.AbrirMsgRespostaSolicitacaoPacoteTemplatesSagem(envelope);
      if (this.msgRespostaPacoteTemplates.Resultado == (byte) 1)
      {
        if (this.msgRespostaPacoteTemplates.NumUsrCad > (ushort) 0)
        {
          byte numPacote = (byte) ((uint) this.msgRespostaPacoteTemplates.NumPac + 1U);
          this.AssociarEmpregadoBioSagem();
          if ((int) numPacote <= (int) this.msgRespostaPacoteTemplates.TotPac)
          {
            this.EnviarSolicitacaoPacoteTemplatesSagem(numPacote);
          }
          else
          {
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msgPROCESSO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this.rep.RepId, this.rep.Local);
          }
        }
        else
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgNENHUM_USUARIO_BIO_CADASTRADOS, EnumEstadoNotificacaoParaUsuario.nenhumEstado, EnumEstadoResultadoFinalProcesso.nenhumUsuarioBioCadastrados, this.rep.RepId, this.rep.Local);
        }
      }
      else
      {
        string menssagem = this.ExtrairRespostaBio(this.msgRespostaPacoteTemplates.Resultado);
        if (!Resources.msgBIO_OCUPADO.Equals(menssagem))
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this.rep.RepId, this.rep.Local);
        }
        else
          this.EnviarSolicitacaoPacoteTemplatesSagem(this.msgRespostaPacoteTemplates.NumPac);
      }
    }

    private void TratarSolicitacaoTemplate(
      Envelope envelope,
      NotificarProgressBarEventArgs eNotificaProgress)
    {
      if (envelope.Grp != (byte) 21 || envelope.Cmd != (byte) 120)
        return;
      this.AbrirMsgRespostaSolicitacaoTemplateUsuarioSagem(envelope);
      if (this.msgRespostaTemplateSagemUsuario.Resultado == (byte) 1 || this.msgRespostaTemplateSagemUsuario.Resultado == (byte) 5)
      {
        long result = long.MinValue;
        if (!this.msgRespostaTemplateSagemUsuario.NumUsuario.Equals("ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff"))
        {
          if (!long.TryParse(this.msgRespostaTemplateSagemUsuario.NumUsuario, out result) && this.queueMsgSolicitUsuarioBio.Count > 1)
          {
            this.EnviarSolicitacaoUsuarioBioSagem();
            return;
          }
          this.AtualizaEstadoListaUsuario(envelope.MsgAplicacaoEmBytes[2]);
          if (this.lstMsgRespostaUsuarioBio.Count > 0)
            this.GravarTemplates(this.lstMsgRespostaUsuarioBio[this.lstMsgRespostaUsuarioBio.Count - 1]);
        }
        if (this.queueMsgSolicitUsuarioBio.Count > 1)
        {
          this.queueMsgSolicitUsuarioBio.Dequeue();
          this.EnviarSolicitacaoUsuarioBioSagem();
          eNotificaProgress = new NotificarProgressBarEventArgs(this.TotTemplatesParaProcessar);
          this.RaiseProgressBar(eNotificaProgress);
        }
        else if (this.lstMsgRespostaUsuarioBio.Count > 0)
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(Resources.msgPROCESSO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this.rep.RepId, this.rep.Local);
        }
        else
        {
          this.EncerrarConexao();
          if (this._chamadaSdk)
            this.NotificarParaUsuario(Resources.msgNAO_ENCONTRADO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.nenhumEstado, EnumEstadoResultadoFinalProcesso.nenhumUsuarioBioCadastrados, this.rep.RepId, this.rep.Local);
          else
            this.NotificarParaUsuario(Resources.msgNAO_ENCONTRADO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.nenhumEstado, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this.rep.RepId, this.rep.Local);
        }
      }
      else
      {
        string menssagem = this.ExtrairRespostaBio(this.msgRespostaTemplateSagemUsuario.Resultado);
        if (!Resources.msgBIO_OCUPADO.Equals(menssagem))
        {
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this.rep.RepId, this.rep.Local);
        }
        else
          this.EnviarSolicitacaoUsuarioBioSagem();
      }
    }

    private void TratarInformacaoInfo(Envelope envelope)
    {
      if (envelope.Grp != (byte) 21 || envelope.Cmd != (byte) 100)
        return;
      if (this.AbrirMsgRespostaInfoSagem(envelope).Resultado != (byte) 1)
      {
        this.EnviarSolicitacaoInfoSagem();
      }
      else
      {
        switch (this.tipoProcessoTemplates)
        {
          case ProcessoTemplates.recuperarTemplates:
            this.ListaUsuariosBio = new SortableBindingList<UsuarioBio>();
            this.EnviarSolicitacaoPacoteTemplatesSagem((byte) 0);
            break;
          case ProcessoTemplates.excluirTemplates:
            this.NotificarParaUsuario(Resources.msgENVIO_EXCLUSAO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this.rep.RepId, this.rep.Local);
            this.EnviarSolicitacaoExclusaoUsuarioBio_Sagem();
            break;
          case ProcessoTemplates.importarTemplates:
            this.NotificarParaUsuario(Resources.msgENVIO_SOLICITACAO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this.rep.RepId, this.rep.Local);
            this.lstMsgRespostaUsuarioBio = new List<MsgTcpAplicacaoRespostaUsuarioBioSagemRepPlus>();
            this.EnviarSolicitacaoUsuarioBioSagem();
            break;
          case ProcessoTemplates.exportarTemplates:
            this.NotificarParaUsuario(Resources.msgENVIO_INCLUSAO_TEMPLATE, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this.rep.RepId, this.rep.Local);
            this.EnviarSolicitacaoInclusaoUsuarioBioSagem();
            break;
          case ProcessoTemplates.solicitar_informacoes:
            this.EncerrarConexao();
            this.NotificarParaUsuario(string.Empty, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this.rep.RepId, this.rep.Local);
            break;
          default:
            this.EncerrarConexao();
            this.NotificarParaUsuario(string.Empty, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this.rep.RepId, this.rep.Local);
            break;
        }
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

    private MsgTcpAplicacaoRespostaInformacaoBioRepPlusSagem AbrirMsgRespostaInfoSagem(
      Envelope envelope)
    {
      byte[] numArray = new byte[2];
      string s = "";
      byte[] aplicacaoEmBytes = envelope.MsgAplicacaoEmBytes;
      Array.Copy((Array) aplicacaoEmBytes, 25, (Array) numArray, 0, numArray.Length);
      Array.Reverse((Array) numArray);
      foreach (byte num in numArray)
        s += num.ToString("X").PadLeft(2, '0');
      this.QtdBioModulo = int.Parse(s, NumberStyles.HexNumber);
      MsgTcpAplicacaoRespostaInformacaoBioRepPlusSagem informacaoBioRepPlusSagem = new MsgTcpAplicacaoRespostaInformacaoBioRepPlusSagem(aplicacaoEmBytes);
      if (informacaoBioRepPlusSagem.Resultado == (byte) 1)
        this.RaiseNotificarModeloBio(new NotificarModeloBIOEventArgs(5, this.rep.RepId));
      return informacaoBioRepPlusSagem;
    }

    private void AbrirMsgRespostaSolicitacaoPacoteTemplatesSagem(Envelope envelope) => this.msgRespostaPacoteTemplates = new MsgTcpAplicacaoRespostaPacoteUsuarioBio(envelope.MsgAplicacaoEmBytes);

    private void AbrirMsgRespostaSolicitacaoTemplateUsuarioSagem(Envelope envelope)
    {
      this.msgRespostaTemplateSagemUsuario = new MsgTcpAplicacaoRespostaUsuarioBioSagemRepPlus(envelope.MsgAplicacaoEmBytes);
      this.lstMsgRespostaUsuarioBio.Add(this.msgRespostaTemplateSagemUsuario);
    }

    private void AssociarEmpregadoBioSagem()
    {
      List<UsuarioBio> lstUsuariosBio = this.msgRespostaPacoteTemplates.LstUsuariosBio;
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
          Trace.WriteLine(ex.Message);
        }
      }
    }

    private byte[] ConverterTemplateEmBytes(string template)
    {
      if (template.Contains("null"))
        return new byte[0];
      byte[] numArray = new byte[256];
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

    private byte[] ConverterPisUsuarioEmBytes(string PisUsuario)
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
      switch (this.estadoRep)
      {
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoExclusaoUsuarioBioSagem:
          MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4KRepPlus usuarioBio14KrepPlus = this.queueMsgSolicitExclusaoUsuarioBio.Peek();
          usuarioBio14KrepPlus.Grupo = (byte) 21;
          num = ulong.Parse(this.DecrementaUmByteporByte(usuarioBio14KrepPlus.NumUsuario));
          break;
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoTemplateUsuarioBioSagem:
          MsgTcpAplicacaoSolicitaUsuarioBioRepRepPlus usuarioBioRepRepPlus = this.queueMsgSolicitUsuarioBio.Peek();
          usuarioBioRepRepPlus.GRP = (byte) 21;
          num = ulong.Parse(this.DecrementaUmByteporByte(usuarioBioRepRepPlus.NumUsuario));
          break;
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoInclusaoUsuarioBioSagem:
          MsgTcpAplicacaoSolicitaInclusaoUsuarioBioRepPlusSagem usuarioBioRepPlusSagem = this.queueMsgSolicitInclusaoUsuarioBio.Peek();
          usuarioBioRepPlusSagem.Grupo = (byte) 21;
          num = ulong.Parse(this.DecrementaUmByteporByte(usuarioBioRepPlusSagem.NumUsuario));
          break;
      }
      if (this.estadoRep == GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoInclusaoUsuarioBioSagem)
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

    public override void CarregarMsgSolicitExclusaoUsuarioBio()
    {
      this.queueMsgSolicitExclusaoUsuarioBio = new Queue<MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4KRepPlus>();
      foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this.ListaUsuariosBio)
      {
        if (usuarioBio.Selecionado || this._chamadaSdk)
        {
          MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4KRepPlus usuarioBio14KrepPlus = new MsgTcpAplicacaoSolicitaExclusaoUsuarioBio1_4KRepPlus()
          {
            Grupo = 21,
            CMD = 21,
            Leitor = 0,
            NumUsuario = usuarioBio.Pis.ToString().PadLeft(16, '0')
          };
          usuarioBio14KrepPlus.NumUsuario = this.ConverterNumeroUsuario(usuarioBio14KrepPlus.NumUsuario);
          this.queueMsgSolicitExclusaoUsuarioBio.Enqueue(usuarioBio14KrepPlus);
        }
      }
    }

    private string ConverterNumeroUsuario(string usuario)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (int pisUsuarioEmByte in this.ConverterPisUsuarioEmBytes(usuario))
      {
        int num = pisUsuarioEmByte + 1;
        stringBuilder.Append(num.ToString("x").PadLeft(2, '0'));
      }
      return stringBuilder.ToString();
    }

    public override void CarregarMsgSolicitInclusaoUsuarioBio()
    {
      this.queueMsgSolicitInclusaoUsuarioBio = new Queue<MsgTcpAplicacaoSolicitaInclusaoUsuarioBioRepPlusSagem>();
      foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this.ListaUsuariosBioNoDb)
      {
        if (usuarioBio.Selecionado || this._chamadaSdk)
        {
          MsgTcpAplicacaoSolicitaInclusaoUsuarioBioRepPlusSagem usuarioBioRepPlusSagem = new MsgTcpAplicacaoSolicitaInclusaoUsuarioBioRepPlusSagem((byte) 21, (byte) 22);
          usuarioBioRepPlusSagem.Template1 = this.ConverterTemplateEmBytes(usuarioBio.Template1);
          usuarioBioRepPlusSagem.QuantidadeTemplates = (byte) 1;
          if (usuarioBio.Template2 != null && usuarioBio.Template2.Length == 512 && usuarioBio.Template1 != usuarioBio.Template2)
          {
            usuarioBioRepPlusSagem.Template2 = this.ConverterTemplateEmBytes(usuarioBio.Template2);
            usuarioBioRepPlusSagem.QuantidadeTemplates = (byte) 2;
          }
          usuarioBioRepPlusSagem.Sobrescrever_digitais = Convert.ToByte(this.configEnt.SobrescreverDigitais);
          usuarioBioRepPlusSagem.Leitor = (byte) 0;
          usuarioBioRepPlusSagem.NumUsuario = usuarioBio.Pis.ToString().PadLeft(16, '0');
          usuarioBioRepPlusSagem.NumUsuario = this.ConverterNumeroUsuario(usuarioBioRepPlusSagem.NumUsuario);
          this.queueMsgSolicitInclusaoUsuarioBio.Enqueue(usuarioBioRepPlusSagem);
        }
      }
    }

    public override void CarregarMsgSolicitUsuarioBio()
    {
      this.queueMsgSolicitUsuarioBio = new Queue<MsgTcpAplicacaoSolicitaUsuarioBioRepRepPlus>();
      this.queueMsgSolicitUsuarioBio = new Queue<MsgTcpAplicacaoSolicitaUsuarioBioRepRepPlus>();
      foreach (UsuarioBio usuarioBio in (Collection<UsuarioBio>) this.ListaUsuariosBio)
      {
        if (usuarioBio.Selecionado || this._chamadaSdk)
        {
          MsgTcpAplicacaoSolicitaUsuarioBioRepRepPlus usuarioBioRepRepPlus = new MsgTcpAplicacaoSolicitaUsuarioBioRepRepPlus()
          {
            Leitor = 0,
            GRP = 21,
            CMD = 20,
            NumUsuario = usuarioBio.Pis.ToString().PadLeft(16, '0')
          };
          usuarioBioRepRepPlus.NumUsuario = this.ConverterNumeroUsuario(usuarioBioRepRepPlus.NumUsuario);
          this.queueMsgSolicitUsuarioBio.Enqueue(usuarioBioRepRepPlus);
        }
      }
    }

    private void GravarTemplates(
      MsgTcpAplicacaoRespostaUsuarioBioSagemRepPlus lstMsgRespostaUsuarioBio)
    {
      if (this._chamadaSdk)
      {
        UsuarioBio usuarioBio = new UsuarioBio();
        string numUsuario = lstMsgRespostaUsuarioBio.NumUsuario;
        usuarioBio.Pis = ulong.Parse(numUsuario.ToString()).ToString();
        if (lstMsgRespostaUsuarioBio.Quantidade == (byte) 1)
        {
          usuarioBio.Template1 = this.ConverterTemplateGravacao(lstMsgRespostaUsuarioBio.Template1);
          usuarioBio.Template2 = this.ConverterTemplateGravacao(lstMsgRespostaUsuarioBio.Template1);
        }
        else if (lstMsgRespostaUsuarioBio.Quantidade == (byte) 2)
        {
          usuarioBio.Template1 = this.ConverterTemplateGravacao(lstMsgRespostaUsuarioBio.Template1);
          usuarioBio.Template2 = this.ConverterTemplateGravacao(lstMsgRespostaUsuarioBio.Template2);
        }
        usuarioBio.TipoTemplate = 5;
        this.ListaUsuariosBio.Add(usuarioBio);
      }
      else
      {
        TemplateBio templateBio1 = new TemplateBio();
        try
        {
          TemplatesBio templateBio2 = new TemplatesBio();
          string numUsuario = lstMsgRespostaUsuarioBio.NumUsuario;
          Empregado empregado = new Empregado().PesquisarEmpregadosPorPis(new Empregado()
          {
            Pis = ulong.Parse(numUsuario).ToString().PadLeft(12, '0'),
            EmpregadorId = this._empregador.EmpregadorId
          });
          templateBio2.Pis = ulong.Parse(numUsuario.ToString()).ToString().PadLeft(12, '0');
          templateBio2.EmpregadoID = empregado.EmpregadoId;
          if (lstMsgRespostaUsuarioBio.Quantidade == (byte) 1)
          {
            templateBio2.Template1 = this.ConverterTemplateGravacao(lstMsgRespostaUsuarioBio.Template1);
            templateBio2.Template2 = this.ConverterTemplateGravacao(lstMsgRespostaUsuarioBio.Template1);
          }
          else if (lstMsgRespostaUsuarioBio.Quantidade == (byte) 2)
          {
            templateBio2.Template1 = this.ConverterTemplateGravacao(lstMsgRespostaUsuarioBio.Template1);
            templateBio2.Template2 = this.ConverterTemplateGravacao(lstMsgRespostaUsuarioBio.Template2);
          }
          templateBio2.EmpregadorID = this._empregador.EmpregadorId;
          templateBio2.TipoTemplate = 5;
          templateBio1.ExcluirTemplates(templateBio2, this._empregador.EmpregadorId, "TemplatesSagem");
          templateBio1.InserirTemplates(templateBio2, "TemplatesSagem");
        }
        catch (AppTopdataException ex)
        {
          this.NotificarParaUsuario(Resources.msgERRO_CONEXAO_INTERROMPIDA_ERRO_DB, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this.rep.RepId, this.rep.Local);
          if (!ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
            return;
          throw;
        }
        catch (Exception ex)
        {
          this.NotificarParaUsuario(Resources.msgERRO_CONEXAO_INTERROMPIDA_ERRO_IN, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this.rep.RepId, this.rep.Local);
          ex.Data.Add((object) "mensagem", (object) Resources.msgERRO_CONEXAO_INTERROMPIDA_ERRO_IN);
          if (!ExceptionPolicy.HandleException(ex, "Politica Dominio"))
            return;
          throw;
        }
      }
    }

    private string ConverterTemplateGravacao(byte[] template)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (byte num in template)
      {
        string str = num.ToString("x");
        if (str.Length == 1)
          str = "0" + str;
        stringBuilder.Append(str);
      }
      return stringBuilder.ToString();
    }

    public static int ExcluirTemplatesBD(int idBiometria) => new TemplateBio().ExcluirTemplates(idBiometria, "TemplatesSagem");

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

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
      {
        this.RaiseNotificarTipoPlacaFim(new NotificarTipoPlacaFIMEventArgs(this.rep.ModeloFim, this.rep.RepId));
        switch (this.tipoProcessoTemplates)
        {
          case ProcessoTemplates.excluirTemplates:
            this.CarregarMsgSolicitExclusaoUsuarioBio();
            if (this.queueMsgSolicitExclusaoUsuarioBio.Count > 0)
            {
              this.TotTemplatesParaProcessar = this.queueMsgSolicitExclusaoUsuarioBio.Count;
              break;
            }
            break;
          case ProcessoTemplates.importarTemplates:
            this.CarregarMsgSolicitUsuarioBio();
            if (this.queueMsgSolicitUsuarioBio.Count > 0)
              this.TotTemplatesParaProcessar = this.queueMsgSolicitUsuarioBio.Count;
            if (this._chamadaSdk)
            {
              this.ListaUsuariosBio.Clear();
              break;
            }
            break;
          case ProcessoTemplates.exportarTemplates:
            this.CarregarMsgSolicitInclusaoUsuarioBio();
            if (this.queueMsgSolicitInclusaoUsuarioBio.Count > 0)
            {
              this.TotTemplatesParaProcessar = this.queueMsgSolicitInclusaoUsuarioBio.Count;
              break;
            }
            if (this.queueMsgSolicitInclusaoUsuarioBio.Count > 0)
            {
              this.TotTemplatesParaProcessar = this.queueMsgSolicitInclusaoUsuarioBio.Count;
              break;
            }
            break;
          case ProcessoTemplates.solicitar_modelo_biometria:
            this.EncerrarConexao();
            this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this.rep.RepId, this.rep.Local);
            return;
        }
        if (this.rep.ModeloFim != (int) byte.MaxValue)
        {
          this.EnviarSolicitacaoInfoSagem();
        }
        else
        {
          this.semBio = true;
          this.EncerrarConexao();
          if (this.rep.ModeloFim == (int) byte.MaxValue)
            this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this.rep.RepId, this.rep.Local);
          else
            this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this.rep.RepId, this.rep.Local);
        }
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
      switch (this.estadoRep)
      {
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoInfoSagem:
          menssagem = Resources.msgTIMEOUT_SOLICIT_INFO_BIO;
          break;
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoPacoteTemplatesSagem:
          menssagem = Resources.msgTIMEOUT_SOLICIT_TEMPLATE;
          break;
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoExclusaoUsuarioBioSagem:
          menssagem = Resources.msgTIMEOUT_SOLICIT_EXCLUSAO_TEMPLATE;
          break;
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoTemplateUsuarioBioSagem:
          menssagem = Resources.msgTIMEOUT_SOLICIT_TEMPLATE_USUARIO;
          break;
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoInclusaoUsuarioBioSagem:
          menssagem = Resources.msgTIMEOUT_SOLICIT_INCLUSAO_TEMPLATE;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this.rep.RepId, this.rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      switch (this.estadoRep)
      {
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoInfoSagem:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_INFO_BIO;
          break;
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoPacoteTemplatesSagem:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_TEMPLATE;
          break;
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoExclusaoUsuarioBioSagem:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_EXCLUSAO_TEMPLATE;
          break;
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoTemplateUsuarioBioSagem:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_TEMPLATE_USUARIO;
          break;
        case GerenciadorTemplatesBioRepPlusSagem.Estados.estSolicitacaoInclusaoUsuarioBioSagem:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_INCLUSAO_TEMPLATE;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this.rep.RepId, this.rep.Local);
    }

    private new enum Estados
    {
      estSolicitacaoInfoSagem,
      estSolicitacaoPacoteTemplatesSagem,
      estSolicitacaoExclusaoUsuarioBioSagem,
      estSolicitacaoTemplateUsuarioBioSagem,
      estSolicitacaoInclusaoUsuarioBioSagem,
    }
  }
}
