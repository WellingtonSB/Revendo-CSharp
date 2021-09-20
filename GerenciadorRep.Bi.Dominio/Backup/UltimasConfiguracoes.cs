// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.UltimasConfiguracoes
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class UltimasConfiguracoes
  {
    public static UltimasConfiguracoes RecuperarUltimasConfiguracoes()
    {
      try
      {
        return new UltimasConfiguracoes().RecuperarUltimasConfiguracoes();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void GravarUltimasConfiguracoes(UltimasConfiguracoes ultimasConfigs)
    {
      try
      {
        new UltimasConfiguracoes().GravarUltimasConfiguracoes(ultimasConfigs);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
