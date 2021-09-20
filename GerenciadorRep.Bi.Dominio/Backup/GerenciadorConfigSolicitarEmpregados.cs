// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigSolicitarEmpregados
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigSolicitarEmpregados : TarefaAbstrata
  {
    public const byte POS_RESP_OPERACAO_ATOMICA = 2;
    private int _empregadorId;
    private RepBase _rep;
    private GerenciadorConfigSolicitarEmpregados.Estados _estadoRep;
    private int _index;
    private List<Empregado> _lstEmpregadosNoREP = new List<Empregado>();
    private bool _chamadaPeloSdk;
    public static GerenciadorConfigSolicitarEmpregados _gerenciadorConfigSolicitarEmpregados;

    public int index
    {
      get => this._index;
      set => this._index = value;
    }

    public event EventHandler<NotificarRegistrosEmpregadosEventArgs> OnNotificarEmpregadosParaSdk;

    public event EventHandler<NotificarRecebimentoParaUsuarioEventArgs> OnNotificarRecebimentoUsuario;

    public static GerenciadorConfigSolicitarEmpregados getInstance() => GerenciadorConfigSolicitarEmpregados._gerenciadorConfigSolicitarEmpregados != null ? GerenciadorConfigSolicitarEmpregados._gerenciadorConfigSolicitarEmpregados : new GerenciadorConfigSolicitarEmpregados();

    public static GerenciadorConfigSolicitarEmpregados getInstance(
      RepBase rep)
    {
      return GerenciadorConfigSolicitarEmpregados._gerenciadorConfigSolicitarEmpregados != null ? GerenciadorConfigSolicitarEmpregados._gerenciadorConfigSolicitarEmpregados : new GerenciadorConfigSolicitarEmpregados(rep);
    }

    public static GerenciadorConfigSolicitarEmpregados getInstance(
      RepBase rep,
      bool chamadaPeloSdk)
    {
      return GerenciadorConfigSolicitarEmpregados._gerenciadorConfigSolicitarEmpregados != null ? GerenciadorConfigSolicitarEmpregados._gerenciadorConfigSolicitarEmpregados : new GerenciadorConfigSolicitarEmpregados(rep, chamadaPeloSdk);
    }

    public static GerenciadorConfigSolicitarEmpregados getInstance(
      RepBase rep,
      int empregadorId)
    {
      return GerenciadorConfigSolicitarEmpregados._gerenciadorConfigSolicitarEmpregados != null ? GerenciadorConfigSolicitarEmpregados._gerenciadorConfigSolicitarEmpregados : new GerenciadorConfigSolicitarEmpregados(rep, empregadorId);
    }

    public GerenciadorConfigSolicitarEmpregados()
    {
    }

    public GerenciadorConfigSolicitarEmpregados(RepBase rep) => this._rep = rep;

    public GerenciadorConfigSolicitarEmpregados(RepBase rep, int empregadorId)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
      this._empregadorId = empregadorId;
    }

    public GerenciadorConfigSolicitarEmpregados(RepBase rep, bool chamadaPeloSdk)
    {
      this._rep = rep;
      this._chamadaPeloSdk = chamadaPeloSdk;
    }

    private void AdicionarEmpregadosListaREP(Envelope envelope, short quantidadeRegMsg)
    {
      for (int index = 0; index < (int) quantidadeRegMsg; ++index)
      {
        try
        {
          byte[] dados = new byte[84];
          Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 8 + 84 * index, (Array) dados, 0, 84);
          this._lstEmpregadosNoREP.Add(new Empregado(dados, false));
        }
        catch
        {
        }
      }
    }

    public override void IniciarProcesso()
    {
      this._lstEmpregadosNoREP = new List<Empregado>();
      this.index = 1;
      this.Conectar(this._rep);
    }

    private void EnviarSolicitacaoDadosEmpregados(int proximoIndex)
    {
      this._estadoRep = GerenciadorConfigSolicitarEmpregados.Estados.estSolicitaDadosEmpregados;
      Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
      MsgTCPAplicacaoSolicitaDadosEmpregados solicitaDadosEmpregados = new MsgTCPAplicacaoSolicitaDadosEmpregados();
      solicitaDadosEmpregados.setIndex(proximoIndex);
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) solicitaDadosEmpregados;
      this.ClienteSocket.Enviar(envelope, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      try
      {
        if (this._estadoRep != GerenciadorConfigSolicitarEmpregados.Estados.estSolicitaDadosEmpregados)
          return;
        try
        {
          if (envelope.Grp != (byte) 4 || envelope.Cmd != (byte) 105)
            return;
          short int16_1 = Convert.ToInt16(((int) envelope.MsgAplicacaoEmBytes[4] << 8) + (int) envelope.MsgAplicacaoEmBytes[5]);
          short int16_2 = Convert.ToInt16(((int) envelope.MsgAplicacaoEmBytes[2] << 8) + (int) envelope.MsgAplicacaoEmBytes[3]);
          if (int16_1 > (short) 0 && int16_2 > (short) 0)
            this.AdicionarEmpregadosListaREP(envelope, int16_1);
          this.index += (int) int16_1;
          if (this.index <= (int) int16_2)
          {
            this.EnviarSolicitacaoDadosEmpregados(this.index);
          }
          else
          {
            this.EncerrarConexao();
            if (this._chamadaPeloSdk)
              this.NotificaMensagemParaSdk(this._lstEmpregadosNoREP);
            else
              this.GravarEmpregadosNoBD();
          }
        }
        catch (Exception ex)
        {
          throw;
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private void GravarEmpregadosNoBD()
    {
      this.NotificaMensagemParaUsuario(Resources.msgPROCESSO_DE_SOLICITACAO_DE_EMPREGADOS_GRAVANDO_BANCO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado);
      Empregado empregado1 = new Empregado();
      foreach (Empregado empregado2 in this._lstEmpregadosNoREP)
      {
        empregado2.EmpregadorId = this._empregadorId;
        empregado2.Pis = empregado2.Pis.Substring(1, 11);
        if (empregado1.VerificarSeExisteCartaoPorEmpregador(empregado2) != 0)
        {
          Empregado empregado3 = empregado1.PesquisarEmpregadosPorPis(empregado2);
          empregado2.EmpregadoId = empregado3.EmpregadoId;
          empregado1.AtualizarEmpregado(empregado2);
        }
        else
          empregado1.InserirEmpregadoPorEmpregador(empregado2);
      }
      this.NotificaMensagemParaUsuario(Resources.msgPROCESSO_DE_SOLICITACAO_DE_EMPREGADOS_SUCESSO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso);
    }

    private void NotificaMensagemParaSdk(List<Empregado> entEmpregado)
    {
      NotificarRegistrosEmpregadosEventArgs e = new NotificarRegistrosEmpregadosEventArgs(entEmpregado);
      if (this.OnNotificarEmpregadosParaSdk == null)
        return;
      this.OnNotificarEmpregadosParaSdk((object) this, e);
    }

    public SortableBindingList<EmpregadosInvalidos> ProcessaDadosInvalidosEmpregados(
      int EmpregadorId)
    {
      return new Empregado().RecuperarEmpregadosDBByEmpregadorDadosInvalidos(EmpregadorId);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarSolicitacaoDadosEmpregados(this.index);
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string _msg = string.Empty;
      if (this._estadoRep == GerenciadorConfigSolicitarEmpregados.Estados.estSolicitaDadosEmpregados)
        _msg = Resources.msgTIMEOUT_SOLICIT_DADOS_EMPREGADOS;
      this.EncerrarConexao();
      this.NotificaMensagemParaUsuario(_msg, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha);
    }

    public override void TratarNenhumaResposta()
    {
      string _msg = string.Empty;
      if (this._estadoRep == GerenciadorConfigSolicitarEmpregados.Estados.estSolicitaDadosEmpregados)
        _msg = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_DADOS_EMPREGADOS;
      this.EncerrarConexao();
      this.NotificaMensagemParaUsuario(_msg, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha);
    }

    private void NotificaMensagemParaUsuario(
      string _msg,
      EnumEstadoNotificacaoParaUsuario enumEstadoNotificacaoParaUsuario,
      EnumEstadoResultadoFinalProcesso enumEstadoResultadoFinalProcesso)
    {
      NotificarRecebimentoParaUsuarioEventArgs e = new NotificarRecebimentoParaUsuarioEventArgs(_msg, enumEstadoNotificacaoParaUsuario, enumEstadoResultadoFinalProcesso, this._empregadorId, "");
      if (this.OnNotificarRecebimentoUsuario == null)
        return;
      this.OnNotificarRecebimentoUsuario((object) this, e);
    }

    private new enum Estados
    {
      estSolicitaDadosEmpregados,
    }
  }
}
