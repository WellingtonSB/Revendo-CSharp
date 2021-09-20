// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.EnvioREP
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
  public class EnvioREP
  {
    private long _Id;

    public long Id
    {
      get => this._Id;
      set => this._Id = value;
    }

    public int AtualizarEnvioREP(EnvioRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().AtualizarEnvioRep(envioRepEnt);
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

    public int AtualizarEnvioREPSenior(EnvioRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().AtualizarEnvioRepSenior(envioRepEnt);
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

    public int AtualizarEnvioREPModeloFim(EnvioRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().AtualizarEnvioRepModeloFim(envioRepEnt);
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

    public int AtualizarEnvioREPModeloTemplate(EnvioRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().AtualizarEnvioREPModeloTemplate(envioRepEnt);
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

    public int AtualizarEnvioREPIniciado(EnvioRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().AtualizarEnvioRepIniciado(envioRepEnt);
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

    public int AtualizarTemplatesEnvioREPProcessado(int empregadoId, int IDrep)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().AtualizarTemplatesEnvioRepProcessado(empregadoId, IDrep);
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

    public int AtualizarTemplatesEnvioREPDesfazerProcessado(int IDrep)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().AtualizarTemplatesEnvioRepDesfazerProcessado(IDrep);
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

    public static event EventHandler<NotificarInicioEnvioRepEventArgs> OnNotificarInicioEnvioConfiguracoes;

    public static event EventHandler<EventArgs> OnNotificarFimDeEnvioConfiguracoes;

    public static event EventHandler<NotificarInicioEnvioRepEventArgs> OnNotificarInicioEnvioRelogio;

    public static event EventHandler<EventArgs> OnNotificarFimDeEnvioRelogio;

    public int InserirEnvioREP(EnvioRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().InserirEnvioRep(envioRepEnt);
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

    public bool ExisteEnvioRep(EnvioRep envioRepEnt)
    {
      bool flag = false;
      try
      {
        flag = new EnvioRep().ExisteEnvioRep(envioRepEnt);
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
      return flag;
    }

    public int InserirTemplatesREPAtualizado(EnvioTemplatesRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().InserirTemplatesREPAtualizado(envioRepEnt);
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

    public int InserirTemplatesEnvioREP(EnvioTemplatesRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().InserirTemplatesEnvioREP(envioRepEnt);
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

    public int InserirTemplatesEnvioREPSenior(EnvioTemplatesRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().InserirTemplatesEnvioREPSenior(envioRepEnt);
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

    public int ExcluirEnvioREP(EnvioRep envioRepEnt) => new EnvioRep().ExcluirEnvioRep(envioRepEnt);

    public int ExcluirEnvioREPPorREP(int RepID)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().ExcluirEnvioRepPorREP(RepID);
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

    public int ExcluirTemplatesEnvioInvalidos(int RepID)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().ExcluirTemplatesEnvioInvalidos(RepID);
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

    public int CancelarEnvioREP(EnvioRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().CancelarEnvioRep(envioRepEnt);
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

    public int ExcluirTemplatesREP(EnvioTemplatesRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().ExcluirTemplatesREP(envioRepEnt);
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

    public int ExcluirTemplatesEnviados(EnvioRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().ExcluirTemplatesEnviados(envioRepEnt);
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

    public int ExcluirTemplatesEnviadosAcao(EnvioTemplatesRep envioRepEnt) => new EnvioRep().ExcluirTemplatesEnviadosAcao(envioRepEnt);

    public int ExcluirTemplatesEnviadosSenior(EnvioRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().ExcluirTemplatesEnviadosSenior(envioRepEnt);
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

    public int ExcluirTemplatesRecebidosSenior(EnvioRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().ExcluirTemplatesRecebidosSenior(envioRepEnt);
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

    public EnvioRep PesquisarRetornoEnvioREP(EnvioRep envioRepEnt)
    {
      EnvioRep envioRep = (EnvioRep) null;
      try
      {
        envioRep = new EnvioRep().PesquisarRetornoEnvioREP(envioRepEnt);
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
      return envioRep;
    }

    public List<EnvioRep> PesquisarEnvioREPGeral()
    {
      List<EnvioRep> envioRepList = new List<EnvioRep>();
      try
      {
        envioRepList = new EnvioRep().PesquisarEnvioREPGeral();
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
      return envioRepList;
    }

    public List<EnvioRep> PesquisarEnvioREPNaoEnviados()
    {
      List<EnvioRep> envioRepList = new List<EnvioRep>();
      try
      {
        envioRepList = new EnvioRep().PesquisarEnvioREPNaoEnviados();
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
      return envioRepList;
    }

    public List<EnvioRep> PesquisarCancelarEnvioREP()
    {
      List<EnvioRep> envioRepList = new List<EnvioRep>();
      try
      {
        envioRepList = new EnvioRep().PesquisarCancelarEnvioREP();
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
      return envioRepList;
    }

    public List<EnvioRep> PesquisarCancelarEnvioREP(int repId)
    {
      List<EnvioRep> envioRepList = new List<EnvioRep>();
      try
      {
        envioRepList = new EnvioRep().PesquisarCancelarEnvioREP(repId);
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
      return envioRepList;
    }

    public EnvioRep PesquisarEnvioREPPorRep(int RepId)
    {
      EnvioRep envioRep1 = new EnvioRep();
      EnvioRep envioRep2 = new EnvioRep().PesquisarEnvioREPPorRep(RepId);
      this._Id = envioRep2.ID;
      return envioRep2;
    }

    public EnvioRep PesquisarEnvioREPPorRegistro(int RepId, int tipoRegistro)
    {
      EnvioRep envioRep = new EnvioRep();
      try
      {
        envioRep = new EnvioRep().PesquisarEnvioREPPorRegistro(RepId, tipoRegistro);
        this._Id = envioRep.ID;
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
      return envioRep;
    }

    public EnvioRep PesquisarEnvioREPPorRegistroTemplate(int RepId, string acaoTemplate)
    {
      EnvioRep envioRep = new EnvioRep();
      try
      {
        envioRep = new EnvioRep().PesquisarEnvioREPPorRegistroTemplate(RepId, acaoTemplate);
        this._Id = envioRep.ID;
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
      return envioRep;
    }

    public EnvioRep PesquisarEnvioREPPorRepNaoEnviados(int RepId)
    {
      EnvioRep envioRep = new EnvioRep();
      try
      {
        envioRep = new EnvioRep().PesquisarEnvioREPPorRepNaoEnviados(RepId);
        this._Id = envioRep.ID;
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
      return envioRep;
    }

    public EnvioRep PesquisarEnvioREPPorRepNaoIniciadoSenior(int RepId)
    {
      EnvioRep envioRep = new EnvioRep();
      try
      {
        envioRep = new EnvioRep().PesquisarEnvioREPPorRepNaoIniciadoSenior(RepId);
        this._Id = envioRep.ID;
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
      return envioRep;
    }

    public SortableBindingList<UsuarioBio> PesquisarEnvioTemplatesREPPorRep(
      int RepId,
      string Acao)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarEnvioTemplatesREPPorRep(RepId, Acao);
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

    public SortableBindingList<UsuarioBio> PesquisarEnvioTemplatesREPPorRepPlus(
      int RepId,
      string Acao)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarEnvioTemplatesREPPorRepPlus(RepId, Acao);
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

    public SortableBindingList<UsuarioBio> PesquisarEnvioTemplatesSagemREPPorRepPlus(
      int RepId,
      string Acao)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarEnvioTemplatesSagemREPPorRepPlus(RepId, Acao);
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

    public SortableBindingList<UsuarioBio> PesquisarEnvioTemplatesCamaREPPorRepPlus(
      int RepId,
      string Acao)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarEnvioTemplatesCamaREPPorRepPlus(RepId, Acao);
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

    public SortableBindingList<UsuarioBio> PesquisarEnvioTemplatesREPPorRepSenior(
      int RepId,
      int tipoTerminal)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarEnvioTemplatesREPPorRepSenior(RepId, tipoTerminal);
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

    public SortableBindingList<UsuarioBio> PesquisarEnvioTemplatesCamaREPPorRepSenior(
      int RepId)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarEnvioTemplatesCamaREPPorRepSenior(RepId);
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

    public SortableBindingList<UsuarioBio> PesquisarEnvioTemplatesSagemREPPorRepSenior(
      int RepId)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarEnvioTemplatesSagemREPPorRepSenior(RepId);
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

    public SortableBindingList<UsuarioBio> PesquisarEnvioTemplatesREPPorRepPlusSenior(
      int RepId)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarEnvioTemplatesREPPorRepPlusSenior(RepId);
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

    public SortableBindingList<UsuarioBio> PesquisarEnvioTemplatesCamaREPPorRepPlusSenior(
      int RepId)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarEnvioTemplatesCamaREPPorRepPlusSenior(RepId);
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

    public SortableBindingList<UsuarioBio> PesquisarEnvioTemplatesSagemREPPorRepPlusSenior(
      int RepId)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarEnvioTemplatesSagemREPPorRepPlusSenior(RepId);
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

    public bool PesquisarTemplatesEnvioRepExclusaoExisteSenior()
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        return new EnvioRep().PesquisarTemplatesEnvioRepExclusaoExisteSenior();
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

    public SortableBindingList<UsuarioBio> PesquisarTemplatesNoRep(
      int RepId,
      int Status)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarTemplatesNoRep(RepId, Status);
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

    public SortableBindingList<UsuarioBio> PesquisarTemplatesNoRepPlus(
      int RepId,
      int Status)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarTemplatesNoRepPlus(RepId, Status);
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

    public SortableBindingList<UsuarioBio> PesquisarTemplatesAEnviarSenior(
      RepBase rep)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarEnvioTemplatesParaEnvio(rep);
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

    public SortableBindingList<UsuarioBio> PesquisarTemplatesAEnviarSeniorRepPlus(
      RepBase RepId,
      int tecnologia)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarEnvioTemplatesParaEnvioRepPlus(RepId, tecnologia);
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

    public SortableBindingList<int> PesquisarTemplatesCadastrados(
      int empregadorId)
    {
      SortableBindingList<int> sortableBindingList = (SortableBindingList<int>) null;
      try
      {
        sortableBindingList = new EnvioRep().PesquisarTemplatesCadastrados(empregadorId);
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

    public int ExcluirTemplatesRepUsuarioSenior(long cartao, int tecnologiaBio)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().ExcluirTemplatesSeniorRepUsuario(cartao, tecnologiaBio);
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

    public int ExcluirTemplatesREPAtualizado(int repId, string Acao)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().ExcluirTemplatesREPAtualizado(repId, Acao);
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

    public SortableBindingList<UsuarioBio> PesquisarTemplatesNoRepPlusNaoEnviados(
      int RepId,
      int Status,
      int TipoBiometria)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarTemplatesNoRepPlusNaoEnviados(RepId, Status, TipoBiometria);
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

    public int ExcluirEnvioREPEnviados()
    {
      int num = 0;
      try
      {
        num = new EnvioRep().ExcluirEnvioRepEnviados();
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

    public int ExcluirTemplatesEnvioREP()
    {
      int num = 0;
      try
      {
        num = new EnvioRep().ExcluirTemplatesEnvioREP();
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

    public SortableBindingList<UsuarioBio> PesquisarTemplatesNoRepNaoEnviados(
      int RepId,
      int Status)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarTemplatesNoRepNaoEnviados(RepId, Status);
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

    public SortableBindingList<UsuarioBio> PesquisarTemplatesNoRepNaoRecebidos(
      RepBase entrep)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarTemplatesNoRepNaoRecebidos(entrep);
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

    public SortableBindingList<UsuarioBio> PesquisarTemplatesNoRepPlusNaoRecebidos(
      RepBase entRep,
      int tipoBiometria)
    {
      SortableBindingList<UsuarioBio> sortableBindingList = new SortableBindingList<UsuarioBio>();
      try
      {
        sortableBindingList = new EnvioRep().PesquisarTemplatesNoRepPlusNaoRecebidos(entRep, tipoBiometria);
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

    public List<EnvioRep> PesquisarEnvioREPEnviados() => new EnvioRep().PesquisarEnvioREPEnviados();

    public bool TemplateEnvioRepExiste(EnvioTemplatesRep envioRepEnt)
    {
      try
      {
        return new EnvioRep().TemplateEnvioRepExiste(envioRepEnt);
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

    public bool TemplateEnvioRepExisteNoRepSenior(EnvioTemplatesRep envioRepEnt)
    {
      try
      {
        return new EnvioRep().TemplateExisteNoRepSenior(envioRepEnt);
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

    public int InserirTemplatesREPAtualizadoSenior(EnvioTemplatesRep envioRepEnt)
    {
      int num = 0;
      try
      {
        num = new EnvioRep().InserirTemplatesREPAtualizadoSenior(envioRepEnt);
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
