// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.RepBio
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

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class RepBio : RepBase
  {
    public RepBio()
    {
    }

    public RepBio(string ipAddress, int port, ushort numTerminal)
      : base(ipAddress, port, numTerminal)
    {
    }

    public RepBio(
      string ipAddress,
      int port,
      ushort numTerminal,
      string chaveComunicacao,
      Empregador empregador,
      string local,
      string senhaRelogio,
      string senhaComunic,
      string senhaBio,
      bool repClient,
      int portaServidor,
      int tempoEspera,
      int tempoEsperaConexao)
      : base(ipAddress, port, numTerminal, chaveComunicacao, empregador, local, senhaRelogio, senhaComunic, senhaBio, repClient, portaServidor, tempoEspera, tempoEsperaConexao)
    {
    }

    public RepBio(
      string ipAddress,
      int port,
      ushort numTerminal,
      string chaveComunicacao,
      Empregador empregador,
      string local,
      string senhaRelogio,
      string senhaComunic,
      string senhaBio,
      bool repClient,
      int portaServidor,
      int tempoEspera,
      string cpf_responsavel)
      : base(ipAddress, port, numTerminal, chaveComunicacao, empregador, local, senhaRelogio, senhaComunic, senhaBio, repClient, portaServidor, tempoEspera, cpf_responsavel)
    {
    }

    public RepBio(
      string ipAddress,
      int port,
      ushort numTerminal,
      string chaveComunicacao,
      Empregador empregador,
      string local,
      string senhaRelogio,
      string senhaComunic,
      string senhaBio,
      bool repClient,
      int portaServidor,
      int tempoEspera,
      string cpf_responsavel,
      string hostDNS,
      string DNS,
      string nomeRep,
      bool DNSHabilitado,
      string host,
      int tipoConexao,
      int tipoTerminalId,
      int tempoEsperaConexao)
      : base(ipAddress, port, numTerminal, chaveComunicacao, empregador, local, senhaRelogio, senhaComunic, senhaBio, repClient, portaServidor, tempoEspera, cpf_responsavel, DNSHabilitado, hostDNS, DNS, nomeRep, host, tipoConexao, tipoTerminalId, tempoEsperaConexao)
    {
    }

    public RepBio(
      string ipAddress,
      int port,
      ushort numTerminal,
      string chaveComunicacao,
      bool repClient,
      bool portaVariavel,
      string ipServidor,
      int portaServidor,
      string mask,
      string gateway,
      int intervaloConexao,
      int tempoespera)
      : base(ipAddress, port, numTerminal, chaveComunicacao, repClient, portaVariavel, ipServidor, portaServidor, mask, gateway, intervaloConexao, tempoespera)
    {
    }

    public RepBio(
      string ipAddress,
      int port,
      ushort numTerminal,
      string chaveComunicacao,
      bool repClient,
      bool portaVariavel,
      string ipServidor,
      int portaServidor,
      string mask,
      string gateway,
      int intervaloConexao,
      int tempoespera,
      string cpf_responsavel)
      : base(ipAddress, port, numTerminal, chaveComunicacao, repClient, portaVariavel, ipServidor, portaServidor, mask, gateway, intervaloConexao, tempoespera, cpf_responsavel)
    {
    }

    public RepBio(
      string ipAddress,
      int port,
      ushort numTerminal,
      string chaveComunicacao,
      bool repClient,
      bool portaVariavel,
      string ipServidor,
      int portaServidor,
      string mask,
      string gateway,
      int intervaloConexao,
      int tempoespera,
      string cpf_responsavel,
      string hostDNS,
      string DNS,
      string nomeRep,
      bool DNSHabilitado)
      : base(ipAddress, port, numTerminal, chaveComunicacao, repClient, portaVariavel, ipServidor, portaServidor, mask, gateway, intervaloConexao, tempoespera, cpf_responsavel, DNSHabilitado, hostDNS, DNS, nomeRep)
    {
    }

    public RepBio(string ipAddress, int port, ushort numTerminal, bool multiRep)
      : base(ipAddress, port, numTerminal, multiRep)
    {
    }

    public RepBio(
      string ipAddress,
      int port,
      ushort numTerminal,
      bool multiRep,
      string host,
      int tipoConexao,
      string hostDNS,
      string DNS,
      int tipoConexaoDNS,
      string nomeRep)
      : base(ipAddress, port, numTerminal, multiRep, host, tipoConexao, hostDNS, DNS, tipoConexaoDNS, nomeRep)
    {
    }

    public int PesquisarAjusteBiometrico()
    {
      int num = 0;
      try
      {
        num = new RepBio().PesquisarAjusteBiometrico();
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

    public int PesquisarAjusteBiometricoMulti()
    {
      int num = 0;
      try
      {
        num = new RepBio().PesquisarAjusteBiometricoMulti();
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

    public int AtualizarAjusteBiometrico(RepBio RepBioEnt)
    {
      int num = 0;
      try
      {
        num = new RepBio().AtualizarAjusteBiometrico(RepBioEnt);
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

    public int AtualizarAjusteBiometricoMulti(RepBio RepBioEnt)
    {
      int num = 0;
      try
      {
        num = new RepBio().AtualizarAjusteBiometricoMulti(RepBioEnt);
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

    public int AtualizarConfiguracaoBio(RepBio RepBioEnt)
    {
      int num = 0;
      try
      {
        num = new RepBio().AtualizarConfiguracaoBio(RepBioEnt);
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

    public RepBio RecuperaAjusteBiometricoPorRep(int rep)
    {
      RepBio repBio = new RepBio();
      try
      {
        repBio = new RepBio().RecuperaAjusteBiometricoPorRep(rep);
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
      return repBio;
    }

    public int AtualizarNivelLFD(RepBio RepBioEnt)
    {
      int num = 0;
      try
      {
        num = new RepBio().AtualizarNivelLFD(RepBioEnt);
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

    public int PequisarPrimeiroAjusteBiometrico()
    {
      int num = 0;
      try
      {
        num = new RepBio().PequisarPrimeiroAjusteBiometrico();
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

    public int PequisarAjusteBiometricoRecomendado()
    {
      int num = 0;
      try
      {
        num = new RepBio().PequisarAjusteBiometricoRecomendado();
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

    public List<RepBio> RecuperaListaAjusteBiometrico()
    {
      List<RepBio> repBioList = new List<RepBio>();
      try
      {
        repBioList = new RepBio().RecuperaListaAjusteBiometrico();
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
      return repBioList;
    }
  }
}
