// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.Empregado
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class Empregado
  {
    public event EventHandler<NotificarProgressBarEventArgs> OnNotificarProgressBar;

    public int InserirEmpregado(Empregado empregadoEnt)
    {
      int num = 0;
      try
      {
        num = new Empregado().InserirEmpregado(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int InserirEmpregadoPorEmpregador(Empregado empregadoEnt)
    {
      int num = 0;
      try
      {
        num = new Empregado().InserirEmpregadoPorEmpregador(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int InserirEmpregadoPorEmpregadorSenior(Empregado empregadoEnt)
    {
      int num = 0;
      try
      {
        num = new Empregado().InserirEmpregadoPorEmpregadorSenior(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int InserirLogAtualizacao(StatusAtualizacao status)
    {
      int num = 0;
      try
      {
        num = new Empregado().InserirLogAtualizacao(status);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int InserirListaEmpregados(List<Empregado> listaEmpregados)
    {
      int num = 0;
      try
      {
        Empregado empregado = new Empregado();
        empregado.OnNotificarProgressBar += new EventHandler<EventArgsCustomizados.NotificarProgressBarEventArgs>(this.empregado_OnNotificarProgressBar);
        num = empregado.InserirListaEmpregados(listaEmpregados);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int AlterarListaEmpregados(List<Empregado> listaEmpregados)
    {
      int num = 0;
      try
      {
        Empregado empregado = new Empregado();
        empregado.OnNotificarProgressBar += new EventHandler<EventArgsCustomizados.NotificarProgressBarEventArgs>(this.empregado_OnNotificarProgressBar);
        num = empregado.AlterarListaEmpregados(listaEmpregados);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int ExcluirListaEmpregados(List<Empregado> listaEmpregados)
    {
      int num = 0;
      try
      {
        Empregado empregado = new Empregado();
        empregado.OnNotificarProgressBar += new EventHandler<EventArgsCustomizados.NotificarProgressBarEventArgs>(this.empregado_OnNotificarProgressBar);
        num = empregado.ExcluirListaEmpregados(listaEmpregados);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    private void empregado_OnNotificarProgressBar(
      object sender,
      EventArgsCustomizados.NotificarProgressBarEventArgs e)
    {
      if (this.OnNotificarProgressBar == null)
        return;
      this.OnNotificarProgressBar((object) this.OnNotificarProgressBar, new NotificarProgressBarEventArgs(e.Incremento));
    }

    public int AtualizarEmpregado(Empregado empregadoEnt)
    {
      int num = 0;
      try
      {
        num = new Empregado().AtualizarEmpregado(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public SortableBindingList<Empregado> PesquisarEmpregados()
    {
      SortableBindingList<Empregado> sortableBindingList = (SortableBindingList<Empregado>) null;
      try
      {
        sortableBindingList = new Empregado().PesquisarEmpregados();
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return sortableBindingList;
    }

    public virtual SortableBindingList<Empregado> PesquisarEmpregadosPorEmpregador(
      int idEmpregador)
    {
      SortableBindingList<Empregado> sortableBindingList = (SortableBindingList<Empregado>) null;
      try
      {
        sortableBindingList = new Empregado().PesquisarEmpregadosPorEmpregador(idEmpregador);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return sortableBindingList;
    }

    public virtual List<Empregado> PesquisarListaEmpregadosPorEmpregador(
      int idEmpregador)
    {
      List<Empregado> empregadoList = (List<Empregado>) null;
      try
      {
        empregadoList = new Empregado().PesquisarListaEmpregadosPorEmpregador(idEmpregador);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregadoList;
    }

    public SortableBindingList<Empregado> PesquisarEmpregadosPorEmpregadorSenior(
      int grupoRepId)
    {
      SortableBindingList<Empregado> sortableBindingList = (SortableBindingList<Empregado>) null;
      try
      {
        sortableBindingList = new Empregado().PesquisarEmpregadosPorEmpregadorSenior(grupoRepId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return sortableBindingList;
    }

    public bool LimiteDeEmpregadosUltrapassado(int tamanhoLista, int empregadorId, bool repPlus)
    {
      int num = this.PesquisarQuantidadeEmpregadoPorEmpregador(new Empregado()
      {
        EmpregadorId = empregadorId
      }) + tamanhoLista;
      return num > 10000 && repPlus || num > 5000 && !repPlus;
    }

    public SortableBindingList<StatusAtualizacao> PesquisarStatusAtualizacaoByRep(
      int RepId)
    {
      SortableBindingList<StatusAtualizacao> sortableBindingList = (SortableBindingList<StatusAtualizacao>) null;
      try
      {
        sortableBindingList = new Empregado().PesquisarStatusAtualizacaoByRep(RepId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return sortableBindingList;
    }

    public SortableBindingList<StatusAtualizacao> PesquisarTodosStatusAtualizacao(
      int idEmpregador)
    {
      SortableBindingList<StatusAtualizacao> sortableBindingList = (SortableBindingList<StatusAtualizacao>) null;
      try
      {
        sortableBindingList = new Empregado().PesquisarTodosStatusAtualizacao(idEmpregador);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return sortableBindingList;
    }

    public int ExcluirStatusAtualizacao()
    {
      int num = 0;
      try
      {
        num = new Empregado().ExcluirStatusAtualizacao();
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int ExcluirStatusAtualizacaoByRepId(int RepId)
    {
      int num = 0;
      try
      {
        num = new Empregado().ExcluirStatusAtualizacaoByRepId(RepId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int ExcluirStatusAtualizacaoByEmpregadoId(int EmpregadoId)
    {
      int num = 0;
      try
      {
        num = new Empregado().ExcluirStatusAtualizacaoByEmpregadoId(EmpregadoId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public virtual SortableBindingList<Empregado> PesquisarEmpregadosPorEmpregadorOrdenadoPis(
      int idEmpregador)
    {
      SortableBindingList<Empregado> sortableBindingList = (SortableBindingList<Empregado>) null;
      try
      {
        sortableBindingList = new Empregado().PesquisarEmpregadosPorEmpregadorOrdenadoPis(idEmpregador);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return sortableBindingList;
    }

    public virtual Empregado PesquisarEmpregadosPorPis(Empregado empregadoLista)
    {
      Empregado empregado = (Empregado) null;
      try
      {
        empregado = new Empregado().PesquisarEmpregadosPorPis(empregadoLista);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregado;
    }

    public SortableBindingList<Empregado> PesquisarEmpregadosPorEmpregadorComFiltro(
      int idEmpregador,
      string nome,
      string cartao,
      string pis)
    {
      SortableBindingList<Empregado> sortableBindingList = (SortableBindingList<Empregado>) null;
      try
      {
        sortableBindingList = new Empregado().PesquisarEmpregadosPorEmpregadorComFiltro(idEmpregador, nome, cartao, pis);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return sortableBindingList;
    }

    public SortableBindingList<Empregado> PesquisarEmpregadosPorEmpregadorComFiltroSenior(
      RepBase rep,
      string nome,
      string cartao,
      string pis)
    {
      SortableBindingList<Empregado> sortableBindingList = (SortableBindingList<Empregado>) null;
      try
      {
        sortableBindingList = new Empregado().PesquisarEmpregadosPorEmpregadorComFiltroSenior(rep, nome, cartao, pis);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return sortableBindingList;
    }

    public Empregado PesquisarEmpregadoPorCartao(int idEmpregador, long cartao)
    {
      Empregado empregado = (Empregado) null;
      try
      {
        empregado = new Empregado().PesquisarEmpregadoPorCartao(idEmpregador, cartao);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregado;
    }

    public Empregado PesquisarEmpregadoPorPersonId(Empregado empregadoEnt)
    {
      Empregado empregado = (Empregado) null;
      try
      {
        empregado = new Empregado().PesquisarEmpregadoPorPersonId(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregado;
    }

    public Empregado PesquisarEmpregadoPorPis(Empregado EnTempregado)
    {
      Empregado empregado = (Empregado) null;
      try
      {
        empregado = new Empregado().PesquisarEmpregadoPorPis(EnTempregado);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregado;
    }

    public Empregado PesquisarEmpregadoPorCartao(Empregado EnTempregado)
    {
      Empregado empregado = (Empregado) null;
      try
      {
        empregado = new Empregado().PesquisarEmpregadoPorCartao(EnTempregado);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregado;
    }

    public Empregado PesquisarEmpregadoPorId(int empregadoId)
    {
      Empregado empregado = (Empregado) null;
      try
      {
        empregado = new Empregado().PesquisarEmpregadoPorId(empregadoId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregado;
    }

    public int PesquisarUltimoIdEmpregado(string pis, int empregadorId)
    {
      int num = 0;
      try
      {
        num = new Empregado().PesquisarUltimoIdEmpregado(pis, empregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public List<string> PesquisarLstPISOrdenada(int empregadorId)
    {
      List<string> stringList = new List<string>();
      try
      {
        stringList = new Empregado().PesquisarLstPISOrdenada(empregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return stringList;
    }

    public List<string> PesquisarLstPISOrdenadaGrupoRep(int grupoId, int empregadorId)
    {
      List<string> stringList = new List<string>();
      try
      {
        stringList = new Empregado().PesquisarLstPISOrdenadaGrupoRep(grupoId, empregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return stringList;
    }

    public int ExcluirEmpregado(Empregado empregadoEnt)
    {
      int num = 0;
      try
      {
        num = new Empregado().ExcluirEmpregado(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int ExcluirEmpregadoPorEmpregador(Empregado empregadoEnt)
    {
      int num1 = 0;
      try
      {
        Empregado empregado = new Empregado();
        num1 = empregado.ExcluirEmpregadoPorEmpregador(empregadoEnt);
        if (num1 > 0)
        {
          RepBase repBase1 = new RepBase();
          SortableBindingList<GrupoRepXempregados> sortableBindingList1 = GrupoRepXempregadoBI.PesquisarGruposDoEmpregado(empregadoEnt.EmpregadoId);
          if (RegistrySingleton.GetInstance().UTILIZA_GRUPOS && sortableBindingList1.Count > 0)
          {
            foreach (GrupoRepXempregados grupoRepXempregados in (Collection<GrupoRepXempregados>) sortableBindingList1)
            {
              foreach (RepBase repBase2 in (Collection<RepBase>) repBase1.PesquisarRepsPorGrupo(grupoRepXempregados.grupoID))
                num1 = empregado.InserirLogAtualizacao(new StatusAtualizacao()
                {
                  TipoAtualizacao = "E",
                  Pis = empregadoEnt.Pis,
                  RepId = repBase2.RepId,
                  Status = Resources.msgSTATUS_ATUALIZACAO_EXCLUSAO,
                  EmpregadoId = empregadoEnt.EmpregadoId,
                  Nome = empregadoEnt.Nome,
                  EmpregadorId = empregadoEnt.EmpregadorId
                });
            }
          }
          else
          {
            SortableBindingList<RepBase> sortableBindingList2 = repBase1.PesquisarRepsPorEmpregador(empregadoEnt.EmpregadorId);
            int num2 = this.PesquisarQuantidadeEmpregadoPorEmpregador(new Empregado()
            {
              EmpregadorId = empregadoEnt.EmpregadorId
            });
            foreach (RepBase repBase3 in (Collection<RepBase>) sortableBindingList2)
            {
              if (num2 >= 9999)
                new TipoTerminal().AtualizarSincronizado(new TipoTerminal()
                {
                  Sincronizado = false,
                  RepId = repBase3.RepId
                });
              else
                num1 = empregado.InserirLogAtualizacao(new StatusAtualizacao()
                {
                  TipoAtualizacao = "E",
                  Pis = empregadoEnt.Pis,
                  RepId = repBase3.RepId,
                  Status = Resources.msgSTATUS_ATUALIZACAO_EXCLUSAO,
                  EmpregadoId = empregadoEnt.EmpregadoId,
                  Nome = empregadoEnt.Nome,
                  EmpregadorId = empregadoEnt.EmpregadorId
                });
            }
          }
        }
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num1;
    }

    public int ExcluirEmpregadoPorEmpregadorSenior(Empregado empregadoEnt)
    {
      int num = 0;
      try
      {
        Empregado empregado = new Empregado();
        num = empregado.ExcluirEmpregadoDoRepSeniorSenior(empregadoEnt);
        if (num > 0)
        {
          foreach (RepBase repBase in (Collection<RepBase>) new RepBase().PesquisarRepsPorEmpregador(empregadoEnt.EmpregadorId))
            num = empregado.InserirLogAtualizacao(new StatusAtualizacao()
            {
              TipoAtualizacao = "E",
              Pis = empregadoEnt.Pis,
              RepId = repBase.RepId,
              Status = Resources.msgSTATUS_ATUALIZACAO_EXCLUSAO,
              EmpregadoId = empregadoEnt.EmpregadoId,
              Nome = empregadoEnt.Nome,
              EmpregadorId = empregadoEnt.EmpregadorId
            });
        }
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int ExcluirEmpregadoComPisExistente(Empregado empregado)
    {
      int num = 0;
      try
      {
        GrupoRepXempregadosDAO repXempregadosDao = new GrupoRepXempregadosDAO();
        Empregado empregado1 = new Empregado();
        num = repXempregadosDao.ExcluirEmpregadoDosGrupos(empregado.EmpregadoId);
        num += empregado1.ExcluirEmpregado(empregado);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int ZerarCartaoTecnologiaEmpregadoSenior(
      Empregado empregadoEnt,
      string cartao,
      int tecnologia)
    {
      int num = 0;
      try
      {
        num = new Empregado().ZerarCartaoTecnologiaSenior(empregadoEnt, cartao, tecnologia);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int ExcluirTodosEmpregados(SortableBindingList<Empregado> Empregados)
    {
      int num = 0;
      try
      {
        num = new Empregado().ExcluirTodosEmpregados(Empregados);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int ExcluirTodosEmpregadosPorEmpregador(int empregadorId)
    {
      int num = 0;
      try
      {
        num = new Empregado().ExcluirTodosEmpregadosPorEmpregador(empregadorId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int PesquisarQuantidadeEmpregado(Empregado empregadoEnt)
    {
      int num = 0;
      try
      {
        num = new Empregado().PesquisarQuantidadeEmpregado(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int PesquisarQuantidadeEmpregadoPorEmpregador(Empregado empregadoEnt)
    {
      int num = 0;
      try
      {
        num = new Empregado().PesquisarQuantidadeEmpregadoPorEmpregador(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int VerificarSeExisteCartaoPorEmpregador(Empregado empregadoEnt)
    {
      int num = 0;
      try
      {
        num = new Empregado().VerificarSeExisteCartaoPorEmpregador(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int VerificarSeExistePisPorEmpregador(Empregado empregadoEnt)
    {
      int num = 0;
      try
      {
        num = new Empregado().VerificarSeExistePisPorEmpregador(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int ExcluirListaEmpregados(SortableBindingList<Empregado> lstEmpregados)
    {
      int num = 0;
      try
      {
        num = new Empregado().ExcluirEmpregadosPorEmpregadorComTransacao(lstEmpregados);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int VerificarSeExisteCartaoProxPorEmpregadorRepPlus(Empregado empregadoEnt)
    {
      int num = 0;
      try
      {
        num = new Empregado().VerificarSeExisteCartaoProxPorEmpregadorRepPlus(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int VerificarSeExisteCartaoBarrasPorEmpregadorRepPlus(Empregado empregadoEnt)
    {
      int num = 0;
      try
      {
        num = new Empregado().VerificarSeExisteCartaoBarrasPorEmpregadorRepPlus(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int VerificarSeExisteTecladoPorEmpregadorRepPlus(Empregado empregadoEnt)
    {
      int num = 0;
      try
      {
        num = new Empregado().VerificarSeExisteTecladoPorEmpregadorRepPlus(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int ExcluirEmpregadoTemplateGrupo(int grupoRepId)
    {
      int num = 0;
      try
      {
        Empregado empregado = new Empregado();
        num = empregado.ExcluirGrupoRepXusuarioSenior(grupoRepId);
        num = empregado.ExcluirTemplateGrupoSenior(grupoRepId);
        num = empregado.ExcluirTemplateCamaGrupoSenior(grupoRepId);
        num = empregado.ExcluirTemplateSagemGrupoSenior(grupoRepId);
        num = empregado.ExcluirEmpregadoGrupoSenior(grupoRepId);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public bool PesquisarEmpregadoParaExclusaoCartaoSenior(
      Empregado empregadoEnt,
      string tipoCartao,
      ulong cartao)
    {
      try
      {
        return new Empregado().PesquisarEmpregadoParaExclusaoCartaoSenior(empregadoEnt, tipoCartao, cartao);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return false;
    }

    public Empregado PesquisarEmpregadoPorPersonIdSenior(Empregado empregadoEnt)
    {
      try
      {
        empregadoEnt = new Empregado().PesquisarEmpregadoPorPersonIdSenior(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return empregadoEnt;
    }

    public bool PisUtilizado(Empregado empregadoEnt)
    {
      try
      {
        return new Empregado().PisUtilizado(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return false;
    }

    public bool CartaoUtilizado(Empregado empregadoEnt)
    {
      try
      {
        return new Empregado().CartaoUtilizado(empregadoEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return false;
    }

    public void AtualizarSenhaEmpregado(string senha, int grupoRepId)
    {
      try
      {
        new Empregado().AtualizarSenhaEmpregado(senha, grupoRepId);
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (!ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          return;
        throw;
      }
    }
  }
}
