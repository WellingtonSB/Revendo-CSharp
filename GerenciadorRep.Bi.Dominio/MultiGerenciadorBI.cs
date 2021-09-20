// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.MultiGerenciadorBI
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class MultiGerenciadorBI
  {
    public static int InserirMultiGerenciador(MultiGerenciador MultiGerenciadorEnt)
    {
      int num = 0;
      try
      {
        num = new MultiGerenciadorDAO().InserirMultiGerenciador(MultiGerenciadorEnt);
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

    public static int AlterarMultiGerenciador(MultiGerenciador multiGerenciadorEnt)
    {
      int num = 0;
      try
      {
        num = new MultiGerenciadorDAO().AlterarMultiGerenciador(multiGerenciadorEnt);
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

    public static int AtualizarArquivoAFD()
    {
      int num = 0;
      try
      {
        num = new MultiGerenciadorDAO().AtualizarArquivoAFD();
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

    public static int AtualizaEmpregado()
    {
      int num = 0;
      try
      {
        num = new MultiGerenciadorDAO().AtualizaEmpregado();
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

    public static int AtualizaEmpregador()
    {
      int num = 0;
      try
      {
        num = new MultiGerenciadorDAO().AtualizaEmpregador();
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

    public static int AtualizaRep()
    {
      int num = 0;
      try
      {
        num = new MultiGerenciadorDAO().AtualizaRep();
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

    public static int ExcluirMultiGerenciador(string NomeMaquina)
    {
      int num = 0;
      try
      {
        num = new MultiGerenciadorDAO().ExcluirMultiGerenciador(NomeMaquina);
      }
      catch (AppTopdataException ex)
      {
      }
      catch (Exception ex)
      {
      }
      return num;
    }

    public static int ExcluirMaquinaDaLista(string NomeMaquina)
    {
      int num = 0;
      try
      {
        num = new MultiGerenciadorDAO().ExcluirMultiGerenciador(NomeMaquina);
      }
      catch (AppTopdataException ex)
      {
      }
      catch (Exception ex)
      {
      }
      return num;
    }

    public static MultiGerenciador PesquisarMultiGerenciador(string NomeMaquina)
    {
      MultiGerenciador multiGerenciador = (MultiGerenciador) null;
      try
      {
        multiGerenciador = new MultiGerenciadorDAO().PesquisarMultiGerenciador(NomeMaquina);
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
      return multiGerenciador;
    }

    public static bool VerificarExistencia(string NomeMaquina)
    {
      bool flag = false;
      try
      {
        flag = new MultiGerenciadorDAO().VerificarExistencia(NomeMaquina);
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        try
        {
          ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
          if (ExceptionPolicy.HandleException(ex, "Politica Dominio"))
            throw;
        }
        catch
        {
        }
      }
      return flag;
    }

    public static MultiGerenciador RecuperaInstancia()
    {
      string NomeMaquina = Environment.MachineName.ToUpper().Trim();
      MultiGerenciador MultiGerenciadorEnt = new MultiGerenciador();
      try
      {
        if (MultiGerenciadorBI.VerificarExistencia(NomeMaquina))
          return MultiGerenciadorBI.PesquisarMultiGerenciador(NomeMaquina);
        MultiGerenciadorEnt.NomeMaquina = NomeMaquina;
        MultiGerenciadorEnt.AtualizaArquivoAFD = false;
        MultiGerenciadorEnt.AtualizaEmpregado = false;
        MultiGerenciadorEnt.AtualizaEmpregador = false;
        MultiGerenciadorEnt.AtualizaRep = false;
        MultiGerenciadorBI.InserirMultiGerenciador(MultiGerenciadorEnt);
        return MultiGerenciadorEnt;
      }
      catch
      {
        return MultiGerenciadorEnt;
      }
    }

    public static void AtualizacaoConcluida()
    {
      try
      {
        MultiGerenciador multiGerenciador = new MultiGerenciador();
        multiGerenciador.NomeMaquina = Environment.MachineName.ToUpper().Trim();
        multiGerenciador.AtualizaArquivoAFD = false;
        multiGerenciador.AtualizaEmpregado = false;
        multiGerenciador.AtualizaEmpregador = false;
        multiGerenciador.AtualizaRep = false;
        if (MultiGerenciadorBI.VerificarExistencia(multiGerenciador.NomeMaquina))
          MultiGerenciadorBI.AlterarMultiGerenciador(multiGerenciador);
        else
          MultiGerenciadorBI.InserirMultiGerenciador(multiGerenciador);
      }
      catch
      {
      }
    }
  }
}
