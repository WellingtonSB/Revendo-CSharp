// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.TipoTerminal
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class TipoTerminal
  {
    public ConfiguracoesBio PesquisarConfiguracaoBioPorRep(int ConfiguracaoId)
    {
      ConfiguracoesBio configuracoesBio = (ConfiguracoesBio) null;
      try
      {
        configuracoesBio = new TipoTerminal().PesquisarConfiguracaoBioPorRep(ConfiguracaoId);
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
      return configuracoesBio;
    }

    public List<TipoTerminal> PesquisarDescricaoTipoTerminal()
    {
      List<TipoTerminal> tipoTerminalList = (List<TipoTerminal>) null;
      try
      {
        tipoTerminalList = new TipoTerminal().PesquisarDescricaoTipoTerminal();
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
      return tipoTerminalList;
    }

    public int AtualizarTipoTerminal(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new TipoTerminal().AtualizarTipoTerminal(RepBaseEnt);
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

    public int AtualizarSincronizado(TipoTerminal TipoTerminalEnt)
    {
      int num = 0;
      try
      {
        num = new TipoTerminal().AtualizarSincronizado(TipoTerminalEnt);
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

    public int AtualizarRep(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().AtualizarRep(RepBaseEnt);
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

    public int InserirRep(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().InserirRep(RepBaseEnt);
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

    public int InserirRepSenior(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().InserirRepSenior(RepBaseEnt);
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

    public int ExcluirRepPorEmpregador(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        new ArquivoBilhete().ExcluirArquivoAFDPorRepId(RepBaseEnt.RepId);
      }
      catch
      {
      }
      try
      {
        num = new RepBase().ExcluirRepPorId(RepBaseEnt);
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

    public int AtualizarConfiguracoesRep(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().AtualizarConfiguracoes(RepBaseEnt);
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

    public int InserirConfiguracoesRep(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().InserirConfiguracoes(RepBaseEnt);
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

    public int InserirRepConfiguracalGeralSenior(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().InserirRepConfiguracalGeral(RepBaseEnt);
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

    public int InserirConfiguracoesRepSenior(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().InserirConfiguracoesSenior(RepBaseEnt);
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

    public SortableBindingList<RepBase> PesquisarRepPorEmpregador(
      int idEmpregador)
    {
      SortableBindingList<RepBase> sortableBindingList = (SortableBindingList<RepBase>) null;
      try
      {
        sortableBindingList = new RepBase().PesquisarRepPorEmpregador(idEmpregador);
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

    public SortableBindingList<RepBase> PesquisarRepPorEmpregadorSenior(
      int idEmpregador)
    {
      SortableBindingList<RepBase> sortableBindingList = (SortableBindingList<RepBase>) null;
      try
      {
        sortableBindingList = new RepBase().PesquisarRepPorEmpregadorSenior(idEmpregador);
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

    public int VerificarSeExisteDescricaoPorEmpregador(RepBase repEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().VerificarSeExisteDescricaoPorEmpregador(repEnt);
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

    public int VerificarSeExisteDescricaoPorEmpregadorSenior(RepBase repEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().VerificarSeExisteDescricaoPorEmpregadorSenior(repEnt);
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

    public int VerificarSeExisteIp(RepBase repEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().VerificarSeExisteIp(repEnt);
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

    public int VerificarSeExisteIpSenior(RepBase repEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().VerificarSeExisteIpSenior(repEnt);
      }
      catch (AppTopdataException ex)
      {
        if (ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          throw;
      }
      return num;
    }

    public int VerificarSePortaServidorDuplicada(RepBase repEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().VerificarSePortaServidorDuplicada(repEnt);
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

    public int VerificarSePortaServidorDuplicadaSenior(RepBase repEnt)
    {
      int num = 0;
      try
      {
        num = new RepBase().VerificarSePortaServidorDuplicadaSenior(repEnt);
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

    public int VerificarPelosMenosUmRepCadastrado()
    {
      int num = 0;
      try
      {
        num = new RepBase().VerificarPelosMenosUmRepCadastrado();
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

    public int VerificarPelosMenosUmRepBioCadastrado()
    {
      int num = 0;
      try
      {
        num = new RepBase().VerificarPelosMenosUmRepBioCadastrado();
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

    public int VerificarPelosMenosUmRepCadastradoPorEmpregador(int idEmpregador)
    {
      int num = 0;
      try
      {
        num = new RepBase().VerificarPelosMenosUmRepCadastradoPorEmpregador(idEmpregador);
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

    public int VerificarPelosMenosUmRepPlusCadastradoPorEmpregador(int idEmpregador)
    {
      int num = 0;
      try
      {
        num = new RepBase().VerificarPelosMenosUmRepPlusCadastradoPorEmpregador(idEmpregador);
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

    public void AtualizarStatusSincronizadoReps()
    {
      try
      {
        new TipoTerminal().AtualizarStatusSincronizadoReps();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int InserirConfiguracoesBio(int configuracao, int ajusteBio, AjusteBiometrico dedoVivo)
    {
      int num = 0;
      try
      {
        num = new RepBase().InserirConfiguracoesBio(configuracao, ajusteBio, dedoVivo);
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

    public int AtualizarRepSincronizacao(TipoTerminal TipoTerminalEnt)
    {
      int num = 0;
      try
      {
        num = new TipoTerminal().AtualizarRepSincronizacao(TipoTerminalEnt);
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

    public int AtualizarRepStatusOnline(TipoTerminal TipoTerminalEnt, int statusOnline)
    {
      int num = 0;
      try
      {
        num = new TipoTerminal().AtualizarRepStatusOnline(TipoTerminalEnt, statusOnline);
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

    public int AtualizarRepSenior(RepBase RepBaseEnt)
    {
      int num = 0;
      try
      {
        RepBase repBase = new RepBase();
        RepBaseEnt.Ativo = true;
        num = repBase.AtualizarRepSenior(RepBaseEnt);
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

    public int DesconectarReps()
    {
      int num = 0;
      try
      {
        num = new RepBase().DesconectarRepsSenior();
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

    public int AtualizarRepsPorEmpregadorSenior(int empregadorId)
    {
      int num = 0;
      try
      {
        num = new TipoTerminal().AtualizarRepsPorEmpregadorSenior(empregadorId);
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

    public int VerificarSeExisteConfiguracaoBio(int ConfigBioId)
    {
      int num = 0;
      try
      {
        num = new RepBase().VerificarSeExisteConfiguracaoBio(ConfigBioId);
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
  }
}
