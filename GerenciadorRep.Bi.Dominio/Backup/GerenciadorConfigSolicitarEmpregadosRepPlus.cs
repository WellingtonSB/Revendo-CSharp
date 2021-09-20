// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigSolicitarEmpregadosRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigSolicitarEmpregadosRepPlus : TarefaAbstrata
  {
    private int _empregadorId;
    private bool _comInstrucoes;
    private RepBase _rep;
    private GerenciadorConfigSolicitarEmpregadosRepPlus.Estados _estadoRep;
    private int _index;
    private List<Empregado> _lstEmpregadosNoREP = new List<Empregado>();
    private bool _chamadaPeloSdk;
    public static GerenciadorConfigSolicitarEmpregadosRepPlus _gerenciadorConfigSolicitarEmpregadosRepPlus;

    public int index
    {
      get => this._index;
      set => this._index = value;
    }

    public event EventHandler<NotificarRegistrosEmpregadosEventArgs> OnNotificarEmpregadosParaSdk;

    public event EventHandler<NotificarRecebimentoParaUsuarioEventArgs> OnNotificarRecebimentoUsuario;

    public static GerenciadorConfigSolicitarEmpregadosRepPlus getInstance() => GerenciadorConfigSolicitarEmpregadosRepPlus._gerenciadorConfigSolicitarEmpregadosRepPlus != null ? GerenciadorConfigSolicitarEmpregadosRepPlus._gerenciadorConfigSolicitarEmpregadosRepPlus : new GerenciadorConfigSolicitarEmpregadosRepPlus();

    public static GerenciadorConfigSolicitarEmpregadosRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigSolicitarEmpregadosRepPlus._gerenciadorConfigSolicitarEmpregadosRepPlus != null ? GerenciadorConfigSolicitarEmpregadosRepPlus._gerenciadorConfigSolicitarEmpregadosRepPlus : new GerenciadorConfigSolicitarEmpregadosRepPlus(rep);
    }

    public static GerenciadorConfigSolicitarEmpregadosRepPlus getInstance(
      RepBase rep,
      bool chamadaPeloSdk)
    {
      return GerenciadorConfigSolicitarEmpregadosRepPlus._gerenciadorConfigSolicitarEmpregadosRepPlus != null ? GerenciadorConfigSolicitarEmpregadosRepPlus._gerenciadorConfigSolicitarEmpregadosRepPlus : new GerenciadorConfigSolicitarEmpregadosRepPlus(rep, chamadaPeloSdk);
    }

    public static GerenciadorConfigSolicitarEmpregadosRepPlus getInstance(
      RepBase rep,
      int empregadorId)
    {
      return GerenciadorConfigSolicitarEmpregadosRepPlus._gerenciadorConfigSolicitarEmpregadosRepPlus != null ? GerenciadorConfigSolicitarEmpregadosRepPlus._gerenciadorConfigSolicitarEmpregadosRepPlus : new GerenciadorConfigSolicitarEmpregadosRepPlus(rep, empregadorId);
    }

    public GerenciadorConfigSolicitarEmpregadosRepPlus()
    {
    }

    public GerenciadorConfigSolicitarEmpregadosRepPlus(RepBase rep) => this._rep = rep;

    public GerenciadorConfigSolicitarEmpregadosRepPlus(RepBase rep, int empregadorId)
    {
      this._rep = rep;
      this._empregadorId = empregadorId;
      this._chamadaPeloSdk = false;
    }

    public GerenciadorConfigSolicitarEmpregadosRepPlus(RepBase rep, bool chamadaPeloSdk)
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
          byte[] dados = new byte[100];
          Array.Copy((Array) envelope.MsgAplicacaoEmBytes, 10 + 100 * index, (Array) dados, 0, 100);
          this._lstEmpregadosNoREP.Add(new Empregado(dados, true));
        }
        catch
        {
        }
      }
    }

    public override void IniciarProcesso()
    {
      this.NotificarRecebimentoParaUsuario(Resources.msgPROCESSO_DE_SOLICITACAO_DE_EMPREGADOS_ANDAMENTO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado);
      this._lstEmpregadosNoREP = new List<Empregado>();
      this.index = 1;
      this.Conectar(this._rep);
    }

    private void EnviarSolicitacaoDadosEmpregados(int proximoIndex)
    {
      this._estadoRep = GerenciadorConfigSolicitarEmpregadosRepPlus.Estados.estSolicitaDadosEmpregados;
      Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
      MsgTCPAplicacaoSolicitaDadosEmpregadosRepPlus empregadosRepPlus = new MsgTCPAplicacaoSolicitaDadosEmpregadosRepPlus();
      empregadosRepPlus.setIndex(proximoIndex);
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) empregadosRepPlus;
      this.ClienteSocket.Enviar(envelope, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      try
      {
        if (this._estadoRep != GerenciadorConfigSolicitarEmpregadosRepPlus.Estados.estSolicitaDadosEmpregados)
          return;
        try
        {
          if (envelope.Grp != (byte) 11 || envelope.Cmd != (byte) 105)
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
            {
              this.NotificaMensagemParaSdk(this._lstEmpregadosNoREP);
              this.NotificarParaUsuario("", EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, 0, "");
            }
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

    private void ConvertToWiegandFC(Empregado empFC)
    {
      try
      {
        string str = empFC.CartaoProx.ToString().PadLeft(8, '0');
        string s = Convert.ToInt16(str.Substring(0, 3)).ToString("X") + Convert.ToInt64(str.Substring(3)).ToString("X");
        empFC.CartaoProx = ulong.Parse(s, NumberStyles.HexNumber);
      }
      catch
      {
      }
    }

    private void GravarEmpregadosNoBD()
    {
      this.NotificarRecebimentoParaUsuario(Resources.msgPROCESSO_DE_SOLICITACAO_DE_EMPREGADOS_GRAVANDO_BANCO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado);
      Empregado empregado1 = new Empregado();
      List<Empregado> empregadoList = new Empregado().PesquisarListaEmpregadosPorEmpregador(this._empregadorId);
      using (List<Empregado>.Enumerator enumerator = this._lstEmpregadosNoREP.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Empregado entEmpregado = enumerator.Current;
          entEmpregado.EmpregadorId = this._empregadorId;
          if (RegistrySingleton.GetInstance().FORMATO_WIEGAND_DEC)
            this.ConvertToWiegandFC(entEmpregado);
          if (empregadoList.Exists((Predicate<Empregado>) (x => x.Pis == entEmpregado.Pis)))
          {
            Empregado empregado2 = empregadoList.Find((Predicate<Empregado>) (x => x.Pis == entEmpregado.Pis));
            entEmpregado.EmpregadoId = empregado2.EmpregadoId;
            empregado1.AtualizarEmpregado(entEmpregado);
          }
          else if ((!empregadoList.Exists((Predicate<Empregado>) (x => (long) x.CartaoBarras == (long) entEmpregado.CartaoBarras)) || entEmpregado.CartaoBarras == 0UL) && (!empregadoList.Exists((Predicate<Empregado>) (x => (long) x.CartaoProx == (long) entEmpregado.CartaoProx)) || entEmpregado.CartaoProx == 0UL) && (!empregadoList.Exists((Predicate<Empregado>) (x => (long) x.Teclado == (long) entEmpregado.Teclado)) || entEmpregado.Teclado == 0UL))
            empregado1.InserirEmpregadoPorEmpregador(entEmpregado);
        }
      }
      this.NotificarRecebimentoParaUsuario(Resources.msgPROCESSO_DE_SOLICITACAO_DE_EMPREGADOS_SUCESSO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso);
    }

    private void NotificaMensagemParaSdk(List<Empregado> entEmpregados)
    {
      NotificarRegistrosEmpregadosEventArgs e = new NotificarRegistrosEmpregadosEventArgs(entEmpregados);
      if (this.OnNotificarEmpregadosParaSdk == null)
        return;
      this.OnNotificarEmpregadosParaSdk((object) this, e);
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
      if (this._estadoRep == GerenciadorConfigSolicitarEmpregadosRepPlus.Estados.estSolicitaDadosEmpregados)
        _msg = Resources.msgTIMEOUT_SOLICIT_DADOS_EMPREGADOS;
      this.EncerrarConexao();
      this.NotificarRecebimentoParaUsuario(_msg, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha);
    }

    public override void TratarNenhumaResposta()
    {
      string _msg = string.Empty;
      if (this._estadoRep == GerenciadorConfigSolicitarEmpregadosRepPlus.Estados.estSolicitaDadosEmpregados)
        _msg = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_DADOS_EMPREGADOS;
      this.EncerrarConexao();
      this.NotificarRecebimentoParaUsuario(_msg, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha);
    }

    private void NotificarRecebimentoParaUsuario(
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
