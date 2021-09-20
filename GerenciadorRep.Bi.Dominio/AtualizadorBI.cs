// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.AtualizadorBI
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class AtualizadorBI
  {
    public static void VerificaAtualizacao()
    {
      try
      {
        AtualizadorBI.VerificaAtualizacaoBase();
      }
      catch
      {
        throw;
      }
      if (ConfigBD_BI.getInstance().tipoConexao == 1)
        AtualizadorBI.AtualizacoesGerais();
      try
      {
        new AtualizadorRegistroDAO().CarregarRegistrySingletonDaBase();
      }
      catch (Exception ex)
      {
        throw;
      }
      if (RegistrySingleton.GetInstance().TIMEOUT_BASE != 0 && RegistrySingleton.GetInstance().TIMEOUT_BASE >= 5000)
        return;
      RegistrySingleton.GetInstance().TIMEOUT_BASE = 5000;
    }

    private static void VerificaAtualizacaoRegistro()
    {
      AtualizadorRegistroDAO.VerificaRegistros();
      AtualizadorRegistroDAO.CarregarRegistrySingleton();
    }

    private static void VerificaAtualizacaoBase()
    {
      AtualizadorDAO atualizadorDao = new AtualizadorDAO();
      switch (atualizadorDao.GetVersaoDB())
      {
        case "1.0.0.0":
          atualizadorDao.AtualizarPara1_0_1_0();
          atualizadorDao.GetVersaoDB();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.1.0":
          atualizadorDao.AtualizarPara1_0_2_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.2.0":
          atualizadorDao.AtualizarPara1_0_3_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.3.0":
          atualizadorDao.AtualizarPara1_0_4_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.4.0":
          atualizadorDao.AtualizarPara1_0_5_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.5.0":
          atualizadorDao.AtualizarPara1_0_6_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.6.0":
          atualizadorDao.AtualizarPara1_0_7_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.7.0":
          atualizadorDao.AtualizarPara1_0_7_1();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.7.1":
          atualizadorDao.AtualizarPara1_0_8_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.8.0":
          atualizadorDao.AtualizarPara1_0_9_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.9.0":
          atualizadorDao.AtualizarPara1_0_10_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.10.0":
          atualizadorDao.AtualizarPara1_0_11_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.11.0":
          atualizadorDao.AtualizarPara1_0_12_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.12.0":
          atualizadorDao.AtualizarPara1_0_13_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.13.0":
          atualizadorDao.AtualizarPara1_0_14_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.14.0":
          atualizadorDao.AtualizarPara1_0_15_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.15.0":
        case "1.0.15.1":
          atualizadorDao.AtualizarPara1_0_16_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.16.0":
        case "1.0.16.1":
          atualizadorDao.AtualizarPara1_0_17_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.17.0":
          atualizadorDao.AtualizarPara1_0_18_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.18.0":
        case "1.0.18.1":
        case "1.0.18.2":
          atualizadorDao.AtualizarPara1_0_19_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "1.0.19.0":
        case "1.0.19.1":
        case "1.0.19.2":
        case "1.0.19.3":
          atualizadorDao.AtualizarPara2_0_0_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.0.0":
          atualizadorDao.AtualizarPara2_0_0_2_LiberacaoProducao();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.0.2":
          atualizadorDao.AtualizarPara2_0_0_3_LiberacaoProducao();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.0.3":
          atualizadorDao.AtualizarPara2_0_0_4_LiberacaoProducao();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.0.4":
          atualizadorDao.AtualizarPara2_0_1_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.1.0":
          atualizadorDao.AtualizarPara2_0_1_1();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.1.1":
          atualizadorDao.AtualizarPara2_0_1_2_LiberacaoProducao();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.1.2":
          atualizadorDao.AtualizarPara2_0_1_3_LiberacaoProducao();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.1.3":
          atualizadorDao.AtualizarPara2_0_1_4();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.1.4":
          atualizadorDao.AtualizarPara2_0_1_5();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.1.5":
          atualizadorDao.AtualizarPara2_0_1_6();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.1.6":
          atualizadorDao.AtualizarPara2_0_1_7_LiberacaoProducao();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.1.7":
          atualizadorDao.AtualizarPara2_0_2_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.2.0":
          atualizadorDao.AtualizarPara2_0_2_1();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.2.1":
          atualizadorDao.AtualizarPara2_0_2_5_LiberacaoProducao();
          atualizadorDao.AtualizarPara2_0_2_6();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.2.5":
          atualizadorDao.AtualizarPara2_0_2_6();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.2.6":
          atualizadorDao.AtualizarPara2_0_2_7();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.2.7":
          atualizadorDao.AtualizarPara2_0_2_8_LiberacaoProducao();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.2.8":
          atualizadorDao.AtualizarPara2_0_2_12_LiberacaoProducao();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.2.12":
          atualizadorDao.AtualizarPara2_0_2_14_LiberacaoProducao();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.2.14":
          atualizadorDao.AtualizarPara2_0_2_15();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "2.0.2.15":
        case "2.0.2.16":
          atualizadorDao.AtualizarPara3_0_0_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.0.0":
          atualizadorDao.AtualizarPara3_0_0_1();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.0.1":
          atualizadorDao.AtualizarPara3_0_0_2();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.0.2":
          atualizadorDao.AtualizarPara3_0_0_3();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.0.3":
          atualizadorDao.AtualizarPara3_0_0_4();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.0.4":
          atualizadorDao.AtualizarPara3_0_1_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.1.0":
          atualizadorDao.AtualizarPara3_0_1_1();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.1.1":
          atualizadorDao.AtualizarPara3_0_1_2_LiberacaoProducao();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.1.2":
          atualizadorDao.AtualizarPara3_0_1_3();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.1.3":
          atualizadorDao.AtualizarPara3_0_1_4();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.1.4":
          atualizadorDao.AtualizarPara3_0_1_5_Liberacao_Producao();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.1.5":
          atualizadorDao.AtualizarPara3_0_2_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.2.0":
          atualizadorDao.AtualizarPara3_0_2_1();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.2.1":
          atualizadorDao.AtualizarPara3_0_2_2_LiberacaoProducao();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.2.2":
          atualizadorDao.AtualizarPara3_0_2_3();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.2.3":
          atualizadorDao.AtualizarPara3_0_2_4();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.2.4":
          atualizadorDao.AtualizarPara3_0_2_5();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.2.5":
          atualizadorDao.AtualizarPara3_0_2_6();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.2.6":
          atualizadorDao.AtualizarPara3_0_2_7();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.2.7":
          atualizadorDao.AtualizarPara3_0_2_8();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.2.8":
          atualizadorDao.AtualizarPara3_0_2_9();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.2.9":
          atualizadorDao.AtualizarPara3_0_2_10();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.2.10":
          atualizadorDao.AtualizarPara3_0_2_11();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.2.11":
          atualizadorDao.AtualizarPara3_0_2_12();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.2.12":
          atualizadorDao.AtualizarPara3_0_2_13();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.2.13":
          atualizadorDao.AtualizarPara3_0_3_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.3.0":
          atualizadorDao.AtualizarPara3_0_3_1();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.3.1":
          atualizadorDao.AtualizarPara3_0_3_2();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.3.2":
          atualizadorDao.AtualizarPara3_0_3_3_LiberacaoProducao();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.3.3":
          atualizadorDao.AtualizarPara3_0_3_4();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.3.4":
          atualizadorDao.AtualizarPara3_0_3_5();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.3.5":
          atualizadorDao.AtualizarPara3_0_3_6();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.3.6":
          atualizadorDao.AtualizarPara3_0_3_7();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "3.0.3.7":
          atualizadorDao.AtualizarPara4_0_0_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.0.0.0":
          atualizadorDao.AtualizarPara4_0_0_1();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.0.0.1":
          atualizadorDao.AtualizarPara4_0_0_5();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.0.0.5":
          atualizadorDao.AtualizarPara4_0_0_6();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.0.0.6":
          atualizadorDao.AtualizarPara4_0_0_8();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.0.0.8":
          atualizadorDao.AtualizarPara4_0_0_10();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.0.0.10":
          atualizadorDao.AtualizarPara4_0_0_11();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.0.0.11":
          atualizadorDao.AtualizarPara4_1_0_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.1.0.0":
          atualizadorDao.AtualizarPara4_1_0_4();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.1.0.4":
          atualizadorDao.AtualizarPara4_2_0_4();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.2.0.4":
          atualizadorDao.AtualizarPara4_2_0_9();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.2.0.9":
          atualizadorDao.AtualizarPara4_2_0_10();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.2.0.10":
          atualizadorDao.AtualizarPara4_2_0_21();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.2.0.21":
          atualizadorDao.AtualizarPara4_2_0_25();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.2.0.25":
          atualizadorDao.AtualizarPara4_2_0_26();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.2.0.26":
          atualizadorDao.AtualizarPara4_4_0_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.4.0.0":
          atualizadorDao.AtualizarPara4_4_0_4();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.4.0.4":
          atualizadorDao.AtualizarPara4_4_0_10();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.4.0.10":
          atualizadorDao.AtualizarPara4_4_0_15();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.4.0.15":
          atualizadorDao.AtualizarPara4_4_0_17();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.4.0.17":
          atualizadorDao.AtualizarPara4_4_0_25();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.4.0.25":
          atualizadorDao.AtualizarPara4_4_0_26();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.4.0.26":
          atualizadorDao.AtualizarPara4_4_0_33();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.4.0.33":
          atualizadorDao.AtualizarPara4_4_0_34();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.4.0.34":
          atualizadorDao.AtualizarPara4_4_0_40();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.4.0.40":
          atualizadorDao.AtualizarPara4_4_0_43();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.4.0.43":
          atualizadorDao.AtualizarPara4_4_0_48();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
        case "4.4.0.48":
          atualizadorDao.AtualizarPara4_4_1_0();
          AtualizadorBI.VerificaAtualizacaoBase();
          break;
      }
    }

    private static void VerificaAtualizacaoBaseDriver()
    {
      AtualizadorDAO atualizadorDao = new AtualizadorDAO();
      string versaoDb = atualizadorDao.GetVersaoDB();
      if (versaoDb == "1.0.0.10" || versaoDb == "1.0.0.11")
      {
        atualizadorDao.AtualizarPara1_0_0_12Senior();
        versaoDb = atualizadorDao.GetVersaoDB();
      }
      if (versaoDb == "1.0.0.12")
      {
        atualizadorDao.AtualizarPara1_0_0_13Senior();
        versaoDb = atualizadorDao.GetVersaoDB();
      }
      if (versaoDb == "1.0.0.13")
      {
        atualizadorDao.AtualizarPara1_0_0_14Senior();
        atualizadorDao.AlterarTabelaRepAtivo();
        versaoDb = atualizadorDao.GetVersaoDB();
      }
      if (versaoDb == "1.0.0.14")
      {
        atualizadorDao.AtualizarPara1_0_0_15Senior();
        atualizadorDao.AtualizacoesBuild15();
        versaoDb = atualizadorDao.GetVersaoDB();
      }
      if (versaoDb == "1.0.0.15")
      {
        atualizadorDao.AtualizarPara1_0_1_0Senior();
        versaoDb = atualizadorDao.GetVersaoDB();
      }
      if (versaoDb == "1.0.1.0")
      {
        atualizadorDao.AtualizarPara1_0_1_1Senior();
        versaoDb = atualizadorDao.GetVersaoDB();
      }
      if (versaoDb == "1.0.1.1")
      {
        atualizadorDao.AtualizarPara1_0_1_2Senior();
        versaoDb = atualizadorDao.GetVersaoDB();
      }
      if (versaoDb == "1.0.1.2" || versaoDb == "1.0.1.3")
      {
        atualizadorDao.AtualizaPara1_0_1_4Senior();
        versaoDb = atualizadorDao.GetVersaoDB();
      }
      if (versaoDb == "1.0.1.4")
      {
        atualizadorDao.UpdateVersaoDB("1.0.1.5");
        atualizadorDao.UpdateVersaoProduto("1.0.1.5");
        versaoDb = atualizadorDao.GetVersaoDB();
      }
      if (versaoDb == "1.0.1.5")
      {
        atualizadorDao.AtualizarDriverPara1_0_1_7();
        versaoDb = atualizadorDao.GetVersaoDB();
      }
      if (versaoDb == "1.0.1.7")
      {
        atualizadorDao.AtualizarDriverPara1_0_1_9();
        versaoDb = atualizadorDao.GetVersaoDB();
      }
      if (versaoDb == "1.0.1.9")
      {
        atualizadorDao.AtualizarDriverPara1_0_1_13();
        versaoDb = atualizadorDao.GetVersaoDB();
      }
      if (versaoDb == "1.0.1.13")
      {
        atualizadorDao.AtualizarDriverPara1_0_1_18();
        versaoDb = atualizadorDao.GetVersaoDB();
      }
      if (versaoDb == "1.0.1.17" || versaoDb == "1.0.1.18")
      {
        atualizadorDao.AtualizarDriverPara1_0_1_25();
        versaoDb = atualizadorDao.GetVersaoDB();
      }
      if (!(versaoDb == "1.0.1.25"))
        return;
      atualizadorDao.AtualizarDriverPara1_0_1_26();
      atualizadorDao.GetVersaoDB();
    }

    public static void VerificaAtualizacaoDriverSenior()
    {
      try
      {
        AtualizadorBI.VerificaAtualizacaoBaseDriver();
      }
      catch
      {
        throw;
      }
      if (ConfigBD_BI.getInstance().tipoConexao == 1)
        AtualizadorBI.AtualizacoesGerais();
      try
      {
        new AtualizadorRegistroDAO().CarregarRegistrySingletonDaBaseSenior();
      }
      catch
      {
        throw;
      }
      if (RegistrySingleton.GetInstance().TIMEOUT_BASE != 0 && RegistrySingleton.GetInstance().TIMEOUT_BASE >= 5000)
        return;
      RegistrySingleton.GetInstance().TIMEOUT_BASE = 5000;
    }

    public static void VerificaRegistros() => AtualizadorBI.VerificaAtualizacaoRegistro();

    private static void AtualizacoesGerais() => new AtualizadorDAO().AtualizacoesGerais();

    public static string RecuperarVersaoBD()
    {
      try
      {
        return new AtualizadorDAO().GetVersaoDB();
      }
      catch
      {
        return "";
      }
    }

    public static void CompactarERepararBase()
    {
      try
      {
        new AtualizadorDAO().CompactarERepararAcces3_0_1_0();
      }
      catch
      {
      }
    }
  }
}
