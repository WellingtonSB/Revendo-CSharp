// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigEmpregadosExclusaoRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigEmpregadosExclusaoRepPlus : TarefaAbstrata
  {
    private RepBase _rep;
    private GerenciadorConfigEmpregadosExclusaoRepPlus.Estados _estadoRep;
    private int _index;
    private List<MsgTCPAplicacaoPosPISEmpregados.PosPis> _lstPosPIS = new List<MsgTCPAplicacaoPosPISEmpregados.PosPis>();
    private List<Empregado> _lstEmpregadosNoCadastro = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroOrdenado = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroExclusao = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroExclusaoEnviado = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastroExclusaoEnviadoRetorno = new List<Empregado>();
    private List<ResultadoAtualizacaoEmpregados> _lstResultadoExcluidos = new List<ResultadoAtualizacaoEmpregados>();
    private List<ResultadoAtualizacaoEmpregados> _lstResultadoExclusaoSDK = new List<ResultadoAtualizacaoEmpregados>();
    private List<string> _lstPisOrdenadaNoCadastro = new List<string>();
    private bool _chamadaPeloSdk;
    private int _totalEmpregadosCadastroExclusao;
    private int _totalLOGS;
    private int _totalPosPIS;
    public static GerenciadorConfigEmpregadosExclusaoRepPlus _gerenciadorConfigEmpregadosExclusaoRepPlus;

    public int index
    {
      get => this._index;
      set => this._index = value;
    }

    public List<MsgTCPAplicacaoPosPISEmpregados.PosPis> lstPosPIS
    {
      get => this._lstPosPIS;
      set => this._lstPosPIS = value;
    }

    public int totalEmpregadosCadastroExclusao
    {
      get => this._totalEmpregadosCadastroExclusao;
      set => this._totalEmpregadosCadastroExclusao = value;
    }

    public int totalLOGS
    {
      get => this._totalLOGS;
      set => this._totalLOGS = value;
    }

    public int totalPosPIS
    {
      get => this._totalPosPIS;
      set => this._totalPosPIS = value;
    }

    public event EventHandler<NotificarExclusaoEmpregadosEventArgs> OnNotificarEmpregadosExclusaoParaSdk;

    public static GerenciadorConfigEmpregadosExclusaoRepPlus getInstance() => GerenciadorConfigEmpregadosExclusaoRepPlus._gerenciadorConfigEmpregadosExclusaoRepPlus != null ? GerenciadorConfigEmpregadosExclusaoRepPlus._gerenciadorConfigEmpregadosExclusaoRepPlus : new GerenciadorConfigEmpregadosExclusaoRepPlus();

    public static GerenciadorConfigEmpregadosExclusaoRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigEmpregadosExclusaoRepPlus._gerenciadorConfigEmpregadosExclusaoRepPlus != null ? GerenciadorConfigEmpregadosExclusaoRepPlus._gerenciadorConfigEmpregadosExclusaoRepPlus : new GerenciadorConfigEmpregadosExclusaoRepPlus(rep);
    }

    public static GerenciadorConfigEmpregadosExclusaoRepPlus getInstance(
      RepBase rep,
      List<Empregado> lstEmpregadosSdk)
    {
      return GerenciadorConfigEmpregadosExclusaoRepPlus._gerenciadorConfigEmpregadosExclusaoRepPlus != null ? GerenciadorConfigEmpregadosExclusaoRepPlus._gerenciadorConfigEmpregadosExclusaoRepPlus : new GerenciadorConfigEmpregadosExclusaoRepPlus(rep, lstEmpregadosSdk);
    }

    public GerenciadorConfigEmpregadosExclusaoRepPlus()
    {
    }

    public GerenciadorConfigEmpregadosExclusaoRepPlus(RepBase rep)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
    }

    public GerenciadorConfigEmpregadosExclusaoRepPlus(RepBase rep, List<Empregado> lstEmpregadosSdk)
    {
      this._rep = rep;
      this._lstEmpregadosNoCadastroExclusao = lstEmpregadosSdk;
      this._chamadaPeloSdk = true;
      this._totalEmpregadosCadastroExclusao = lstEmpregadosSdk.Count;
    }

    private void ProcessaDadosEmpregadosExclusao()
    {
      if (!this._chamadaPeloSdk)
      {
        Empregado empregado = new Empregado();
        Empregador empregador = new Empregador().PesquisarEmpregadorDeUmREP(this._rep.RepId);
        this._lstEmpregadosNoCadastroExclusao = !RegistrySingleton.GetInstance().UTILIZA_GRUPOS ? empregado.RecuperarEmpregadosDBExclusaoByEmpregador(this._rep.RepId, empregador.EmpregadorId) : (this._rep.grupoId != 0 ? GrupoRepXempregadoBI.PesquisarEmpregadosComInstrucaoExclusaoDoGrupo(this._rep.RepId, empregador.EmpregadorId) : empregado.RecuperarEmpregadosDBExclusaoByEmpregador(this._rep.RepId, empregador.EmpregadorId));
        this._lstPisOrdenadaNoCadastro = !RegistrySingleton.GetInstance().UTILIZA_GRUPOS ? empregado.PesquisarLstPISOrdenadaExclusao(empregador.EmpregadorId) : (this._rep.grupoId != 0 ? empregado.PesquisarLstPISOrdenadaGrupoRepExclusao(this._rep.grupoId, empregador.EmpregadorId) : empregado.PesquisarLstPISOrdenadaExclusao(empregador.EmpregadorId));
      }
      else
      {
        this._lstEmpregadosNoCadastroExclusao.Sort((Comparison<Empregado>) ((emp1, emp2) => emp1.Pis.CompareTo(emp2.Pis)));
        for (int index = 0; index < this._lstEmpregadosNoCadastroExclusao.Count; ++index)
          this._lstPisOrdenadaNoCadastro.Add(this._lstEmpregadosNoCadastroExclusao[index].Pis.Replace(".", string.Empty).Replace("-", string.Empty));
      }
      foreach (string str in this._lstPisOrdenadaNoCadastro)
      {
        for (int index = 0; index < this._lstEmpregadosNoCadastroExclusao.Count; ++index)
        {
          if (this._lstEmpregadosNoCadastroExclusao[index].Pis.Replace(".", string.Empty).Replace("-", string.Empty).Equals(str))
          {
            this._lstPosPIS.Add(new MsgTCPAplicacaoPosPISEmpregados.PosPis((short) (index + 1)));
            break;
          }
        }
      }
      this._totalEmpregadosCadastroExclusao = this._lstEmpregadosNoCadastroExclusao.Count;
      this._totalPosPIS = this._lstPosPIS.Count;
      this._rep.TimeoutExtra = (long) (this._totalEmpregadosCadastroExclusao * 100 * 200);
      int count = this._lstEmpregadosNoCadastroExclusao.Count;
    }

    public override void IniciarProcesso()
    {
      this.index = 1;
      this.Conectar(this._rep);
    }

    private void EnviaDadosEmpregadosExclusao()
    {
      this.NotificarParaUsuario(Resources.msg_ENVIANDO_DADOS_EMPREGADOS, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this._rep.RepId, this._rep.Local);
      bool flag = false;
      this._estadoRep = GerenciadorConfigEmpregadosExclusaoRepPlus.Estados.estEnviaEmpregadosExclusao;
      if (this._lstEmpregadosNoCadastroExclusao.Count == 0)
        return;
      MsgTCPAplicacaoRegistroEmpregadosExclusaoRepPlus empregadosExclusaoRepPlus = new MsgTCPAplicacaoRegistroEmpregadosExclusaoRepPlus();
      this._lstEmpregadosNoCadastroExclusaoEnviadoRetorno = new List<Empregado>();
      foreach (Empregado empregado in this._lstEmpregadosNoCadastroExclusao)
      {
        MsgTCPAplicacaoRegistroEmpregadosExclusaoRepPlus.ConfigEmpregado configEmpregado = new MsgTCPAplicacaoRegistroEmpregadosExclusaoRepPlus.ConfigEmpregado(empregado.Pis);
        if (empregadosExclusaoRepPlus.AddEmpregado(configEmpregado))
          this._lstEmpregadosNoCadastroExclusaoEnviadoRetorno.Add(empregado);
        else
          break;
      }
      for (int index = 0; index < empregadosExclusaoRepPlus.getRegMsg(); ++index)
      {
        this._lstEmpregadosNoCadastroExclusaoEnviado.Add(this._lstEmpregadosNoCadastroExclusao[0]);
        this._lstEmpregadosNoCadastroExclusao.RemoveAt(0);
      }
      empregadosExclusaoRepPlus.setIndex(this.index);
      empregadosExclusaoRepPlus.setRegTotal(this._lstEmpregadosNoCadastroExclusaoEnviadoRetorno.Count);
      if (empregadosExclusaoRepPlus.getRegMsg() > 0 && this._lstEmpregadosNoCadastroExclusaoEnviado.Count > 0)
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) empregadosExclusaoRepPlus
        }, true);
      else if (this._lstEmpregadosNoCadastroExclusaoEnviado.Count == 0 && !flag)
      {
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        empregadosExclusaoRepPlus.setIndex(1);
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) empregadosExclusaoRepPlus;
        this.ClienteSocket.Enviar(envelope, true);
      }
      else if (flag)
        this._index = 1;
      else
        this._index = 1;
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      try
      {
        if (this._estadoRep != GerenciadorConfigEmpregadosExclusaoRepPlus.Estados.estEnviaEmpregadosExclusao)
          return;
        try
        {
          if (envelope.Grp != (byte) 11 || envelope.Cmd != (byte) 108)
            return;
          if (!this._chamadaPeloSdk && this._lstEmpregadosNoCadastroExclusao.Count <= 0)
          {
            TipoTerminal TipoTerminalEnt = new TipoTerminal();
            TipoTerminal tipoTerminal = new TipoTerminal();
            TipoTerminalEnt.RepId = this._rep.RepId;
            TipoTerminalEnt.Sincronizado = true;
            tipoTerminal.AtualizarSincronizado(TipoTerminalEnt);
          }
          this.VerificarRetornoExcluidos(envelope);
          if (this._lstEmpregadosNoCadastroExclusao.Count <= 0)
          {
            this._estadoRep = GerenciadorConfigEmpregadosExclusaoRepPlus.Estados.estFinal;
            this.EncerrarConexao();
            this.NotificarParaUsuario(Resources.msg_PROCESSO_EMPREGADOS_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
          }
          else
            this.EnviaDadosEmpregadosExclusao();
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

    private void VerificarRetornoExcluidos(Envelope env)
    {
      if (!this._chamadaPeloSdk)
      {
        byte num = 2;
        Empregado empregado = new Empregado();
        foreach (Empregado empregadoEnt in this._lstEmpregadosNoCadastroExclusaoEnviadoRetorno)
        {
          NotificarExclusaoEmpregadosEventArgs empregadosEventArgs = new NotificarExclusaoEmpregadosEventArgs(env.MsgAplicacaoEmBytes[(int) num], empregadoEnt.Pis);
          ++num;
          empregado.GravaLogAtualizacao(empregadoEnt.EmpregadorId, empregadoEnt.EmpregadoId, empregadosEventArgs.Resultado.MsgInclusao, this._rep.RepId);
          if (empregadosEventArgs.Resultado.Retorno == 0 || empregadosEventArgs.Resultado.Retorno == 1)
          {
            empregadoEnt.Processado = true;
            empregado.ExcluirEmpregadoExcluidosDoRep(empregadoEnt, this._rep.RepId);
            empregado.ExcluirStatusAtualizacaoByRepEmpregado(empregadoEnt.EmpregadoId, this._rep.RepId);
          }
          else
            empregado.GravaLogAtualizacao(empregadoEnt.EmpregadorId, empregadoEnt.EmpregadoId, empregadosEventArgs.Resultado.MsgInclusao, this._rep.RepId);
        }
        this._lstEmpregadosNoCadastroExclusaoEnviado.Clear();
        this._lstEmpregadosNoCadastroExclusaoEnviado = new List<Empregado>();
      }
      else
      {
        byte num = 2;
        foreach (Empregado empregado in this._lstEmpregadosNoCadastroExclusaoEnviadoRetorno)
        {
          NotificarExclusaoEmpregadosEventArgs empregadosEventArgs = new NotificarExclusaoEmpregadosEventArgs(env.MsgAplicacaoEmBytes[(int) num], empregado.Pis);
          ++num;
          this._lstResultadoExclusaoSDK.Add(empregadosEventArgs.Resultado);
        }
        if (this._lstResultadoExclusaoSDK.Count != this._totalEmpregadosCadastroExclusao)
          return;
        this.NotificaMensagemParaSdk(this._lstResultadoExclusaoSDK);
      }
    }

    private void NotificaMensagemParaSdk(
      List<ResultadoAtualizacaoEmpregados> entAtualizacaoEmpregados)
    {
      NotificarExclusaoEmpregadosEventArgs e = new NotificarExclusaoEmpregadosEventArgs(entAtualizacaoEmpregados);
      if (this.OnNotificarEmpregadosExclusaoParaSdk == null)
        return;
      this.OnNotificarEmpregadosExclusaoParaSdk((object) this, e);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviaDadosEmpregadosExclusao();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigEmpregadosExclusaoRepPlus.Estados.estEnviaEmpregadosExclusao)
        menssagem = Resources.msgTIMEOUT_ENVIO_EMPREGADOS;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigEmpregadosExclusaoRepPlus.Estados.estEnviaEmpregadosExclusao)
        menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_EMPREGADOS;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnviaEmpregadosExclusao,
      estEnviaPosPis,
      estFinal,
    }
  }
}
