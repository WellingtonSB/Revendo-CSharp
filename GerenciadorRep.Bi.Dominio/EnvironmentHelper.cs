// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.EnvironmentHelper
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public static class EnvironmentHelper
  {
    public static bool VerificarRepEmUso(int repId)
    {
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      ConfigBD_BI.getInstance();
      RepComunicacao repComunicacao = RepComunicacaoBI.PesquisarRepComunicacao(repId);
      return repComunicacao != null && repComunicacao.NomeMaquina != Environment.MachineName.ToUpper().Trim() && repComunicacao.DtHrUtilizacao >= DateTime.Now.AddMinutes(-5.0) && repComunicacao.NomeMaquina != Environment.MachineName.ToUpper().Trim();
    }

    public static bool VerificarRepEmUsoServico(int repId)
    {
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      ConfigBD_BI.getInstance();
      return RepComunicacaoBI.PesquisarRepComunicacao(repId) != null;
    }

    public static void LiberarRep()
    {
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      ConfigBD_BI.getInstance();
      try
      {
        RepComunicacaoBI.ExcluirMaquinaComunicacao(Environment.MachineName.ToUpper().Trim());
      }
      catch (Exception ex)
      {
      }
    }

    public static void LiberarRep(int RepID)
    {
      try
      {
        RepComunicacaoBI.ExcluirMaquinaComunicacao(RepID);
      }
      catch
      {
      }
    }

    public static void SetarRepEmUso(int repId)
    {
      RepComunicacao RepComunicEnt = RepComunicacaoBI.PesquisarRepComunicacao(repId);
      if (RepComunicEnt != null)
      {
        RepComunicEnt.DtHrUtilizacao = DateTime.Now;
        RepComunicEnt.NomeMaquina = Environment.MachineName.ToUpper().Trim();
        RepComunicEnt.RepID = repId;
        RepComunicacaoBI.AlterarRepComunicacao(RepComunicEnt);
      }
      else
        RepComunicacaoBI.InserirRepComunicacao(new RepComunicacao()
        {
          DtHrUtilizacao = DateTime.Now,
          NomeMaquina = Environment.MachineName.ToUpper().Trim(),
          RepID = repId
        });
    }

    public static void AtualizarServico()
    {
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      if (ConfigBD_BI.getInstance().tipoConexao == 0)
        return;
      Servico ComunicServ = ServicoBI.PesquisarServico();
      if (ComunicServ != null)
      {
        ComunicServ.DtHrUtilizacao = ServicoBI.PesquisarDataSistema();
        ServicoBI.AlterarServico(ComunicServ);
      }
      else
        ServicoBI.InserirServico(new Servico()
        {
          DtHrUtilizacao = ServicoBI.PesquisarDataSistema()
        });
    }

    public static Servico PesquisarServico()
    {
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      ConfigBD_Entidade instance = ConfigBD_BI.getInstance();
      Servico servico = (Servico) null;
      if (instance.tipoConexao != 0)
        servico = ServicoBI.PesquisarServico();
      return servico;
    }

    public static bool VerifyMachineName(string nomeServidor) => !(nomeServidor != "") || !(nomeServidor != Environment.MachineName.ToUpper().Trim());
  }
}
