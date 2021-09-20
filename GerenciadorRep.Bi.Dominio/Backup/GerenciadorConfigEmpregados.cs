// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigEmpregados
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcp.MsgTcpRep;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigEmpregados : TarefaAbstrata
  {
    public const byte POS_RESP_OPERACAO_ATOMICA = 2;
    private RepBase _rep;
    private GerenciadorConfigEmpregados.Estados _estadoRep;
    private int _index;
    private List<MsgTCPAplicacaoRegistroLOGEmpregados.ConfigLOG> _lstLogs = new List<MsgTCPAplicacaoRegistroLOGEmpregados.ConfigLOG>();
    private List<MsgTCPAplicacaoPosPISEmpregados.PosPis> _lstPosPIS = new List<MsgTCPAplicacaoPosPISEmpregados.PosPis>();
    private List<Empregado> _lstEmpregadosNoREP = new List<Empregado>();
    private List<Empregado> _lstEmpregadosNoCadastro = new List<Empregado>();
    private List<string> _lstDadosInvalidosEmpregados = new List<string>();
    private List<Empregado> _lstEmpregadosPisDupNoCadastro = new List<Empregado>();
    private List<Empregado> _lstEmpregadosPisVazioNoCadastro = new List<Empregado>();
    private List<string> _lstPisOrdenadaNoCadastro = new List<string>();
    private bool chamadaPeloSdk;
    private bool chamadaPeloDriverSenior;
    private int _totalEmpregadosCadastro;
    private int _totalLOGS;
    private int _totalPosPIS;
    public static GerenciadorConfigEmpregados _gerenciadorConfigEmpregados;

    public int index
    {
      get => this._index;
      set => this._index = value;
    }

    public List<MsgTCPAplicacaoRegistroLOGEmpregados.ConfigLOG> lstLogs
    {
      get => this._lstLogs;
      set => this._lstLogs = value;
    }

    public List<MsgTCPAplicacaoPosPISEmpregados.PosPis> lstPosPIS
    {
      get => this._lstPosPIS;
      set => this._lstPosPIS = value;
    }

    public bool ChamadaPeloDriverSenior
    {
      get => this.chamadaPeloDriverSenior;
      set => this.chamadaPeloDriverSenior = value;
    }

    public int totalEmpregadosCadastro
    {
      get => this._totalEmpregadosCadastro;
      set => this._totalEmpregadosCadastro = value;
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

    public static GerenciadorConfigEmpregados getInstance() => GerenciadorConfigEmpregados._gerenciadorConfigEmpregados != null ? GerenciadorConfigEmpregados._gerenciadorConfigEmpregados : new GerenciadorConfigEmpregados();

    public static GerenciadorConfigEmpregados getInstance(RepBase rep) => GerenciadorConfigEmpregados._gerenciadorConfigEmpregados != null ? GerenciadorConfigEmpregados._gerenciadorConfigEmpregados : new GerenciadorConfigEmpregados(rep);

    public static GerenciadorConfigEmpregados getInstance(
      RepBase rep,
      bool senior)
    {
      return GerenciadorConfigEmpregados._gerenciadorConfigEmpregados != null ? GerenciadorConfigEmpregados._gerenciadorConfigEmpregados : new GerenciadorConfigEmpregados(rep, senior);
    }

    public static GerenciadorConfigEmpregados getInstance(
      RepBase rep,
      List<Empregado> lstEmpregadosSdk)
    {
      return GerenciadorConfigEmpregados._gerenciadorConfigEmpregados != null ? GerenciadorConfigEmpregados._gerenciadorConfigEmpregados : new GerenciadorConfigEmpregados(rep, lstEmpregadosSdk);
    }

    public GerenciadorConfigEmpregados()
    {
    }

    public GerenciadorConfigEmpregados(RepBase rep)
    {
      this._rep = rep;
      this.chamadaPeloSdk = false;
    }

    public GerenciadorConfigEmpregados(RepBase rep, bool senior)
    {
      this._rep = rep;
      this.chamadaPeloSdk = false;
      this.chamadaPeloDriverSenior = senior;
    }

    public GerenciadorConfigEmpregados(RepBase rep, List<Empregado> lstEmpregadosSdk)
    {
      this._rep = rep;
      this._lstEmpregadosNoCadastro = lstEmpregadosSdk;
      this.chamadaPeloSdk = true;
    }

    private void VerificaEmpregadosAlterados()
    {
      foreach (Empregado empregado1 in this._lstEmpregadosNoCadastro)
      {
        bool flag = false;
        foreach (Empregado empregado2 in this._lstEmpregadosNoREP)
        {
          if (string.Compare(empregado1.Pis.PadLeft(12, '0'), empregado2.Pis) == 0 && (!empregado1.Nome.Trim().Equals(empregado2.Nome.Trim()) || !empregado1.NomeExibicao.Trim().Equals(empregado2.NomeExibicao.Trim()) || !empregado1.Cartao.Equals(empregado2.Cartao) || !empregado1.Senha.Trim().Equals(empregado2.Senha.Trim()) || !empregado1.VerificaBiometria.Equals(empregado2.VerificaBiometria)))
          {
            flag = true;
            break;
          }
        }
        if (flag)
          this._lstLogs.Add(new MsgTCPAplicacaoRegistroLOGEmpregados.ConfigLOG((byte) 65, empregado1.Pis, empregado1.Nome));
      }
    }

    private void VerificaEmpregadosExcluidos()
    {
      foreach (Empregado empregado1 in this._lstEmpregadosNoREP)
      {
        bool flag = false;
        foreach (Empregado empregado2 in this._lstEmpregadosNoCadastro)
        {
          if (string.Compare(empregado2.Pis.PadLeft(12, '0'), empregado1.Pis) == 0)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          this._lstLogs.Add(new MsgTCPAplicacaoRegistroLOGEmpregados.ConfigLOG((byte) 69, empregado1.Pis, empregado1.Nome));
      }
    }

    private void VerificarEmpregadosIncluidos()
    {
      foreach (Empregado empregado1 in this._lstEmpregadosNoCadastro)
      {
        bool flag = false;
        foreach (Empregado empregado2 in this._lstEmpregadosNoREP)
        {
          if (string.Compare(empregado1.Pis.PadLeft(12, '0'), empregado2.Pis) == 0)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          this._lstLogs.Add(new MsgTCPAplicacaoRegistroLOGEmpregados.ConfigLOG((byte) 73, empregado1.Pis, empregado1.Nome));
      }
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

    private void ProcessaDadosEmpregados()
    {
      if (!this.chamadaPeloSdk)
      {
        Empregado daEmpregado = new Empregado();
        Empregador empregadorEntity = new Empregador().PesquisarEmpregadorDeUmREP(this._rep.RepId);
        if (RegistrySingleton.GetInstance().UTILIZA_GRUPOS)
        {
          if (this._rep.grupoId == 0)
            this.CarregarListadeEmpregadoSemGrupo(daEmpregado, empregadorEntity);
          else
            this.CarregarListaDeEmpregadosComGrupo(empregadorEntity);
        }
        else
          this.CarregarListadeEmpregadoSemGrupo(daEmpregado, empregadorEntity);
        this._lstPisOrdenadaNoCadastro = !RegistrySingleton.GetInstance().UTILIZA_GRUPOS ? daEmpregado.PesquisarLstPISOrdenada(empregadorEntity.EmpregadorId) : (this._rep.grupoId != 0 ? daEmpregado.PesquisarLstPISOrdenadaGrupoRep(this._rep.grupoId, empregadorEntity.EmpregadorId) : daEmpregado.PesquisarLstPISOrdenada(empregadorEntity.EmpregadorId));
        if (this._lstEmpregadosNoCadastro.Count > 5000)
        {
          this._lstEmpregadosNoCadastro = this._lstEmpregadosNoCadastro.GetRange(0, 5000);
          List<string> stringList = new List<string>();
          foreach (Empregado empregado in this._lstEmpregadosNoCadastro)
          {
            foreach (string str in this._lstPisOrdenadaNoCadastro)
            {
              if (str.Equals(empregado.Pis))
                stringList.Add(str);
            }
          }
          stringList.Sort();
          this._lstPisOrdenadaNoCadastro = stringList;
        }
      }
      else
      {
        this._lstEmpregadosNoCadastro.Sort((Comparison<Empregado>) ((emp1, emp2) => emp1.Pis.CompareTo(emp2.Pis)));
        for (int index = 0; index < this._lstEmpregadosNoCadastro.Count; ++index)
          this._lstPisOrdenadaNoCadastro.Add(this._lstEmpregadosNoCadastro[index].Pis.Replace(".", string.Empty).Replace("-", string.Empty));
        this._lstEmpregadosNoCadastro.Sort((Comparison<Empregado>) ((emp1, emp2) => emp1.Cartao.CompareTo(emp2.Cartao)));
      }
      foreach (Empregado empregado in this._lstEmpregadosNoREP)
        empregado.Pis = empregado.Pis.Remove(0, 1);
    }

    private void CarregarListaDeEmpregadosComGrupo(Empregador empregadorEntity)
    {
      if (this.chamadaPeloDriverSenior)
        this._lstEmpregadosNoCadastro = GrupoRepXempregadoBI.PesquisarEmpregadosDoGrupoSenior(this._rep.grupoId, empregadorEntity.EmpregadorId, this._rep.TipoTerminalId);
      else
        this._lstEmpregadosNoCadastro = GrupoRepXempregadoBI.PesquisarEmpregadosDoGrupo(this._rep.grupoId, empregadorEntity.EmpregadorId);
    }

    private void CarregarListadeEmpregadoSemGrupo(
      Empregado daEmpregado,
      Empregador empregadorEntity)
    {
      if (this.chamadaPeloDriverSenior)
        this._lstEmpregadosNoCadastro = daEmpregado.RecuperarEmpregadosDBByEmpregadorSenior(empregadorEntity.EmpregadorId, this._rep.TipoTerminalId);
      else
        this._lstEmpregadosNoCadastro = daEmpregado.RecuperarEmpregadosDBByEmpregador(empregadorEntity.EmpregadorId);
    }

    public bool ValidaDadosEmpregadosPisDuplicados(
      ref string empregadosPisDupAux,
      int EmpregadorID,
      ref string empregadosSemPis)
    {
      if (!this.chamadaPeloSdk)
      {
        Empregado empregado = new Empregado();
        this._lstEmpregadosNoCadastro = empregado.RecuperarEmpregadosDBByEmpregador(EmpregadorID);
        this._lstPisOrdenadaNoCadastro = empregado.PesquisarLstPISOrdenada(EmpregadorID);
      }
      else
        this._lstEmpregadosNoCadastro.Sort((Comparison<Empregado>) ((emp1, emp2) => emp1.Cartao.CompareTo(emp2.Cartao)));
      foreach (Empregado empregado in this._lstEmpregadosNoREP)
        empregado.Pis = empregado.Pis.Remove(0, 1);
      foreach (string str in this._lstPisOrdenadaNoCadastro)
      {
        for (int index = 0; index < this._lstEmpregadosNoCadastro.Count; ++index)
        {
          if (this._lstEmpregadosNoCadastro[index].Pis.Replace(".", string.Empty).Replace("-", string.Empty).Equals(str))
          {
            this._lstPosPIS.Add(new MsgTCPAplicacaoPosPISEmpregados.PosPis((short) (index + 1)));
            break;
          }
        }
      }
      this._totalEmpregadosCadastro = this._lstEmpregadosNoCadastro.Count;
      this._totalPosPIS = this._lstPosPIS.Count;
      Empregado empregado1 = new Empregado();
      this._lstEmpregadosPisDupNoCadastro = empregado1.RecuperarEmpregadosDBByEmpregadorPisDuplicado(EmpregadorID);
      this._lstEmpregadosPisVazioNoCadastro = empregado1.RecuperarEmpregadosDBByEmpregadorSemPis(EmpregadorID);
      foreach (Empregado empregado2 in this._lstEmpregadosPisDupNoCadastro)
      {
        ref string local = ref empregadosPisDupAux;
        local = local + empregado2.Pis + " - " + empregado2.Nome + "\n";
      }
      foreach (Empregado empregado3 in this._lstEmpregadosPisVazioNoCadastro)
      {
        ref string local = ref empregadosSemPis;
        local = local + empregado3.Nome + "\n";
      }
      return this._totalEmpregadosCadastro == this._totalPosPIS && !(empregadosPisDupAux != "") && !(empregadosSemPis != "");
    }

    public override void IniciarProcesso()
    {
      this.NotificarParaUsuario(Resources.msgCARREGANDO_EMPREGADOS_BASE_DADOS, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this._rep.RepId, this._rep.Local);
      Application.DoEvents();
      Thread.Sleep(1);
      string empty = string.Empty;
      try
      {
        this.EncerrarConexao();
        this.ProcessaDadosEmpregados();
      }
      catch (Exception ex)
      {
        this.NotificarParaUsuario(ex.Message, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
        this.EncerrarConexao();
        return;
      }
      this._lstEmpregadosNoREP = new List<Empregado>();
      this._lstLogs = new List<MsgTCPAplicacaoRegistroLOGEmpregados.ConfigLOG>();
      this.index = 1;
      this.Conectar(this._rep);
    }

    private void EnviarSolicitacaoDadosEmpregados(int proximoIndex)
    {
      this._estadoRep = GerenciadorConfigEmpregados.Estados.estSolicitaDadosEmpregados;
      Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
      MsgTCPAplicacaoSolicitaDadosEmpregados solicitaDadosEmpregados = new MsgTCPAplicacaoSolicitaDadosEmpregados();
      solicitaDadosEmpregados.setIndex(proximoIndex);
      envelope.MsgAplicacao = (MsgTcpAplicacaoBase) solicitaDadosEmpregados;
      this.ClienteSocket.Enviar(envelope, true);
    }

    private void EnviarOperacaoAtomica(int operacao)
    {
      MsgTCPAplicacaoOperacaoAtomica aplicacaoOperacaoAtomica = new MsgTCPAplicacaoOperacaoAtomica();
      try
      {
        switch (operacao)
        {
          case 1:
            this._estadoRep = GerenciadorConfigEmpregados.Estados.estEnviaInicioOperacaoAtomica;
            aplicacaoOperacaoAtomica = new MsgTCPAplicacaoOperacaoAtomica((byte) 1);
            break;
          case 2:
            this._estadoRep = GerenciadorConfigEmpregados.Estados.estEnviaFimOperacaoAtomica;
            aplicacaoOperacaoAtomica = new MsgTCPAplicacaoOperacaoAtomica((byte) 2);
            break;
          case 3:
            this._estadoRep = GerenciadorConfigEmpregados.Estados.estAguardaFimOperacaoAtomica;
            aplicacaoOperacaoAtomica = new MsgTCPAplicacaoOperacaoAtomica((byte) 3);
            break;
        }
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) aplicacaoOperacaoAtomica
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviaDadosEmpregados(int index)
    {
      this.NotificarParaUsuario(Resources.msg_ENVIANDO_DADOS_EMPREGADOS, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this._rep.RepId, this._rep.Local);
      bool flag = false;
      switch (index)
      {
        case 0:
          index = 1;
          break;
        case 1:
          flag = true;
          break;
      }
      this._estadoRep = GerenciadorConfigEmpregados.Estados.estEnviaEmpregados;
      if (this._lstLogs.Count == 0)
      {
        this._estadoRep = GerenciadorConfigEmpregados.Estados.estFinal;
        this.NotificarParaUsuario(Resources.msg_PROCESSO_EMPREGADOS_FINALIZADO_SEM_ALTERACOES, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
        if (this.chamadaPeloSdk)
          return;
        new StatusAtualizacao().ExluirStatusAtualizacaoNoBDByRepId(this._rep.RepId);
      }
      else
      {
        MsgTCPAplicacaoRegistroEmpregados registroEmpregados = new MsgTCPAplicacaoRegistroEmpregados();
        foreach (Empregado empregado in this._lstEmpregadosNoCadastro)
        {
          MsgTCPAplicacaoRegistroEmpregados.ConfigEmpregado configEmpregado = new MsgTCPAplicacaoRegistroEmpregados.ConfigEmpregado(empregado.Pis, empregado.Cartao.ToString(), empregado.Nome, empregado.NomeExibicao, empregado.Senha, empregado.VerificaBiometria);
          if (!registroEmpregados.AddEmpregado(configEmpregado))
            break;
        }
        for (int index1 = 0; index1 < registroEmpregados.getRegMsg(); ++index1)
          this._lstEmpregadosNoCadastro.RemoveAt(0);
        registroEmpregados.setIndex(index);
        registroEmpregados.setRegTotal(this._totalEmpregadosCadastro);
        if (registroEmpregados.getRegMsg() > 0 && this._totalEmpregadosCadastro > 0)
          this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
          {
            MsgAplicacao = (MsgTcpAplicacaoBase) registroEmpregados
          }, true);
        else if (this._lstLogs.Count > 0 && this._totalEmpregadosCadastro == 0 && !flag)
        {
          Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
          registroEmpregados.setIndex(1);
          envelope.MsgAplicacao = (MsgTcpAplicacaoBase) registroEmpregados;
          this.ClienteSocket.Enviar(envelope, true);
        }
        else if (flag)
        {
          this._index = 1;
          this.EnviaPosPis(this._index);
        }
        else
        {
          this._index = 1;
          this.EnviaPosPis(this._index);
        }
      }
    }

    private void EnviaLOGSEmpregados(int index)
    {
      this._estadoRep = GerenciadorConfigEmpregados.Estados.estEnviaLogs;
      MsgTCPAplicacaoRegistroLOGEmpregados registroLogEmpregados = new MsgTCPAplicacaoRegistroLOGEmpregados();
      foreach (MsgTCPAplicacaoRegistroLOGEmpregados.ConfigLOG lstLog in this._lstLogs)
      {
        if (!registroLogEmpregados.AddConfigLOG(lstLog))
          break;
      }
      for (int index1 = 0; index1 < registroLogEmpregados.getRegMsg(); ++index1)
        this._lstLogs.RemoveAt(0);
      registroLogEmpregados.setIndex(index);
      registroLogEmpregados.setRegTotal(this._totalLOGS);
      if (registroLogEmpregados.getRegMsg() > 0)
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) registroLogEmpregados
        }, true);
      else
        this.EnviarOperacaoAtomica(2);
    }

    private void EnviaPosPis(int index)
    {
      this._estadoRep = GerenciadorConfigEmpregados.Estados.estEnviaPosPis;
      MsgTCPAplicacaoPosPISEmpregados posPisEmpregados = new MsgTCPAplicacaoPosPISEmpregados();
      foreach (MsgTCPAplicacaoPosPISEmpregados.PosPis posPis in this._lstPosPIS)
      {
        if (!posPisEmpregados.AddPosPis(posPis))
          break;
      }
      for (int index1 = 0; index1 < posPisEmpregados.getRegMsg(); ++index1)
        this._lstPosPIS.RemoveAt(0);
      posPisEmpregados.setIndex(index);
      posPisEmpregados.setRegTotal(this._totalPosPIS);
      if (posPisEmpregados.getRegMsg() > 0)
      {
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) posPisEmpregados
        }, true);
      }
      else
      {
        this._index = 1;
        this.EnviaLOGSEmpregados(this._index);
      }
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      try
      {
        switch (this._estadoRep)
        {
          case GerenciadorConfigEmpregados.Estados.estSolicitaDadosEmpregados:
            try
            {
              if (envelope.Grp != (byte) 4 || envelope.Cmd != (byte) 105)
                break;
              short int16_1 = Convert.ToInt16(((int) envelope.MsgAplicacaoEmBytes[4] << 8) + (int) envelope.MsgAplicacaoEmBytes[5]);
              short int16_2 = Convert.ToInt16(((int) envelope.MsgAplicacaoEmBytes[2] << 8) + (int) envelope.MsgAplicacaoEmBytes[3]);
              if (int16_1 > (short) 0 && int16_2 > (short) 0)
                this.AdicionarEmpregadosListaREP(envelope, int16_1);
              this.index += (int) int16_1;
              if (this.index <= (int) int16_2)
              {
                this.EnviarSolicitacaoDadosEmpregados(this.index);
                break;
              }
              this.VerificaEmpregadosExcluidos();
              this.VerificarEmpregadosIncluidos();
              this.VerificaEmpregadosAlterados();
              foreach (string str in this._lstPisOrdenadaNoCadastro)
              {
                for (int index = 0; index < this._lstEmpregadosNoCadastro.Count; ++index)
                {
                  if (this._lstEmpregadosNoCadastro[index].Pis.Replace(".", string.Empty).Replace("-", string.Empty).Equals(str))
                  {
                    this._lstPosPIS.Add(new MsgTCPAplicacaoPosPISEmpregados.PosPis((short) (index + 1)));
                    break;
                  }
                }
              }
              this._totalEmpregadosCadastro = this._lstEmpregadosNoCadastro.Count;
              this._totalPosPIS = this._lstPosPIS.Count;
              this._totalLOGS = this._lstLogs.Count;
              this._rep.TimeoutExtra = 1350000L;
              if (!this.chamadaPeloSdk)
                new TipoTerminal().AtualizarSincronizado(new TipoTerminal()
                {
                  RepId = this._rep.RepId,
                  Sincronizado = true
                });
              if (this._lstLogs.Count == 0)
              {
                this.EncerrarConexao();
                this._estadoRep = GerenciadorConfigEmpregados.Estados.estFinal;
                this.NotificarParaUsuario(Resources.msg_PROCESSO_EMPREGADOS_FINALIZADO_SEM_ALTERACOES, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
                if (this.chamadaPeloSdk)
                  break;
                new StatusAtualizacao().ExluirStatusAtualizacaoNoBDByRepId(this._rep.RepId);
                break;
              }
              this.EnviarOperacaoAtomica(1);
              break;
            }
            catch (Exception ex)
            {
              throw;
            }
          case GerenciadorConfigEmpregados.Estados.estEnviaInicioOperacaoAtomica:
            try
            {
              if (envelope.Grp != (byte) 4 || envelope.Cmd != (byte) 103 || envelope.MsgAplicacaoEmBytes[2] != (byte) 1)
                break;
              this.index = 0;
              this.EnviaDadosEmpregados(this.index);
              break;
            }
            catch (Exception ex)
            {
              throw;
            }
          case GerenciadorConfigEmpregados.Estados.estEnviaEmpregados:
            try
            {
              if (envelope.Grp != (byte) 4 || envelope.Cmd != (byte) 101)
                break;
              this.EnviaDadosEmpregados(((int) envelope.MsgAplicacaoEmBytes[4] << 8) + (int) envelope.MsgAplicacaoEmBytes[5]);
              break;
            }
            catch (Exception ex)
            {
              throw;
            }
          case GerenciadorConfigEmpregados.Estados.estEnviaPosPis:
            try
            {
              if (envelope.Grp != (byte) 4 || envelope.Cmd != (byte) 107)
                break;
              this.EnviaPosPis(((int) envelope.MsgAplicacaoEmBytes[4] << 8) + (int) envelope.MsgAplicacaoEmBytes[5]);
              break;
            }
            catch (Exception ex)
            {
              throw;
            }
          case GerenciadorConfigEmpregados.Estados.estEnviaLogs:
            try
            {
              if (envelope.Grp != (byte) 4 || envelope.Cmd != (byte) 104)
                break;
              this.EnviaLOGSEmpregados(((int) envelope.MsgAplicacaoEmBytes[4] << 8) + (int) envelope.MsgAplicacaoEmBytes[5]);
              break;
            }
            catch (Exception ex)
            {
              throw;
            }
          case GerenciadorConfigEmpregados.Estados.estEnviaFimOperacaoAtomica:
            try
            {
              if (envelope.Grp != (byte) 4 || envelope.Cmd != (byte) 103 || envelope.MsgAplicacaoEmBytes[2] != (byte) 2)
                break;
              this.EnviarOperacaoAtomica(3);
              break;
            }
            catch (Exception ex)
            {
              throw;
            }
          case GerenciadorConfigEmpregados.Estados.estAguardaFimOperacaoAtomica:
            try
            {
              if (envelope.Grp != (byte) 4 || envelope.Cmd != (byte) 103)
                break;
              if (envelope.MsgAplicacaoEmBytes[2] == (byte) 3)
              {
                Thread.Sleep(200);
                this.EnviarOperacaoAtomica(3);
                break;
              }
              if (envelope.MsgAplicacaoEmBytes[2] == (byte) 4)
              {
                this.EncerrarConexao();
                this.NotificarParaUsuario(Resources.msg_PROCESSO_EMPREGADOS_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
                if (this.chamadaPeloSdk)
                  break;
                new StatusAtualizacao().ExluirStatusAtualizacaoNoBDByRepId(this._rep.RepId);
                break;
              }
              if (envelope.MsgAplicacaoEmBytes[2] != (byte) 5)
                break;
              this.NotificarParaUsuario(Resources.msgNENHUMA_RESPOSTA_ENVIO_LOGS, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
              break;
            }
            catch (Exception ex)
            {
              throw;
            }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public SortableBindingList<EmpregadosInvalidos> ProcessaDadosInvalidosEmpregados(
      int EmpregadorId)
    {
      return new Empregado().RecuperarEmpregadosDBByEmpregadorDadosInvalidos(EmpregadorId);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
      {
        this.NotificarParaUsuario(Resources.msgENVIANDO_CONFIGURACOES, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.aindaNaoFinalizado, this._rep.RepId, this._rep.Local);
        this.EnviarSolicitacaoDadosEmpregados(this.index);
      }
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigEmpregados.Estados.estSolicitaDadosEmpregados:
          menssagem = Resources.msgTIMEOUT_SOLICIT_DADOS_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregados.Estados.estProcessaDadosEmpregados:
          menssagem = Resources.msgTIMEOUT_PROCESSA_DADOS_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregados.Estados.estEnviaInicioOperacaoAtomica:
          menssagem = Resources.msgTIMEOUT_ENVIO_INICIO_OP_ATOMICA;
          break;
        case GerenciadorConfigEmpregados.Estados.estEnviaEmpregados:
          menssagem = Resources.msgTIMEOUT_ENVIO_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregados.Estados.estEnviaPosPis:
          menssagem = Resources.msgTIMEOUT_ENVIO_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregados.Estados.estEnviaLogs:
          menssagem = Resources.msgTIMEOUT_ENVIO_LOGS;
          break;
        case GerenciadorConfigEmpregados.Estados.estEnviaFimOperacaoAtomica:
          menssagem = Resources.msgTIMEOUT_ENVIA_FIM_OP_ATOMICA;
          break;
        case GerenciadorConfigEmpregados.Estados.estAguardaFimOperacaoAtomica:
          menssagem = Resources.msgTIMEOUT_AGUARDA_FIM_OP_ATOMICA;
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
        case GerenciadorConfigEmpregados.Estados.estSolicitaDadosEmpregados:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_SOLICIT_DADOS_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregados.Estados.estProcessaDadosEmpregados:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_PROCESSA_DADOS_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregados.Estados.estEnviaInicioOperacaoAtomica:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_INICIO_OP_ATOMICA;
          break;
        case GerenciadorConfigEmpregados.Estados.estEnviaEmpregados:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregados.Estados.estEnviaPosPis:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_EMPREGADOS;
          break;
        case GerenciadorConfigEmpregados.Estados.estEnviaLogs:
          menssagem = Resources.msgNENHUMA_RESPOSTA_ENVIO_LOGS;
          break;
        case GerenciadorConfigEmpregados.Estados.estEnviaFimOperacaoAtomica:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIA_FIM_OP_ATOMICA;
          break;
        case GerenciadorConfigEmpregados.Estados.estAguardaFimOperacaoAtomica:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_AGUARDA_FIM_OP_ATOMICA;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estSolicitaDadosEmpregados,
      estProcessaDadosEmpregados,
      estEnviaInicioOperacaoAtomica,
      estEnviaEmpregados,
      estEnviaPosPis,
      estEnviaLogs,
      estEnviaFimOperacaoAtomica,
      estAguardaFimOperacaoAtomica,
      estFinal,
    }
  }
}
