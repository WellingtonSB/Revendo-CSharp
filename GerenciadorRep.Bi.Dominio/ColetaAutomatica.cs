// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.ColetaAutomatica
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class ColetaAutomatica
  {
    public static ColetaAutomatica PesquisarColetaAutomatica()
    {
      ColetaAutomatica coletaAutomatica = new ColetaAutomatica();
      ColetaAutomatica objColetaAuto;
      try
      {
        objColetaAuto = new ColetaAutomatica().PesquisarColetaAutomatica();
        ColetaAutomatica.AssociarIntervaloSegundos(objColetaAuto);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return objColetaAuto;
    }

    public static bool AlterarColetaAutomatica(ColetaAutomatica _objColetaAuto)
    {
      try
      {
        return new ColetaAutomatica().AlterarColetaAutomatica(_objColetaAuto);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private static void AssociarIntervaloSegundos(ColetaAutomatica objColetaAuto)
    {
      switch (objColetaAuto.ColetaAutoIntervaloIndex)
      {
        case 0:
          objColetaAuto.ColetaAutoIntervaloS = 600L;
          break;
        case 1:
          objColetaAuto.ColetaAutoIntervaloS = 900L;
          break;
        case 2:
          objColetaAuto.ColetaAutoIntervaloS = 1200L;
          break;
        case 3:
          objColetaAuto.ColetaAutoIntervaloS = 1500L;
          break;
        case 4:
          objColetaAuto.ColetaAutoIntervaloS = 1800L;
          break;
        case 5:
          objColetaAuto.ColetaAutoIntervaloS = 2100L;
          break;
        case 6:
          objColetaAuto.ColetaAutoIntervaloS = 2400L;
          break;
        case 7:
          objColetaAuto.ColetaAutoIntervaloS = 2700L;
          break;
        case 8:
          objColetaAuto.ColetaAutoIntervaloS = 3000L;
          break;
        case 9:
          objColetaAuto.ColetaAutoIntervaloS = 3300L;
          break;
        case 10:
          objColetaAuto.ColetaAutoIntervaloS = 3600L;
          break;
        case 11:
          objColetaAuto.ColetaAutoIntervaloS = 5400L;
          break;
        case 12:
          objColetaAuto.ColetaAutoIntervaloS = 7200L;
          break;
        case 13:
          objColetaAuto.ColetaAutoIntervaloS = 9000L;
          break;
        case 14:
          objColetaAuto.ColetaAutoIntervaloS = 10800L;
          break;
        case 15:
          objColetaAuto.ColetaAutoIntervaloS = 12600L;
          break;
        case 16:
          objColetaAuto.ColetaAutoIntervaloS = 14400L;
          break;
        case 17:
          objColetaAuto.ColetaAutoIntervaloS = 16200L;
          break;
        case 18:
          objColetaAuto.ColetaAutoIntervaloS = 18000L;
          break;
        case 19:
          objColetaAuto.ColetaAutoIntervaloS = 19800L;
          break;
        case 20:
          objColetaAuto.ColetaAutoIntervaloS = 21600L;
          break;
        case 21:
          objColetaAuto.ColetaAutoIntervaloS = 23400L;
          break;
        case 22:
          objColetaAuto.ColetaAutoIntervaloS = 25200L;
          break;
        case 23:
          objColetaAuto.ColetaAutoIntervaloS = 27000L;
          break;
        case 24:
          objColetaAuto.ColetaAutoIntervaloS = 28800L;
          break;
        case 26:
          objColetaAuto.ColetaAutoIntervaloS = 180L;
          break;
        default:
          objColetaAuto.ColetaAutoIntervaloS = 3600L;
          break;
      }
    }

    public static bool PermissaoColeta()
    {
      ColetaAutomatica coletaAutomatica1 = new ColetaAutomatica();
      ColetaAutomatica coletaAutomatica2 = new ColetaAutomatica();
      ColetaAutomatica coletaAutomatica3 = coletaAutomatica1.PesquisarColetaAutomatica();
      string str = Environment.MachineName.ToUpper().Trim();
      try
      {
        return coletaAutomatica3.NomeMaquina.Trim() == "" || coletaAutomatica3.NomeMaquina == null || coletaAutomatica3.NomeMaquina.ToUpper().Trim() == str;
      }
      catch
      {
        return false;
      }
    }
  }
}
