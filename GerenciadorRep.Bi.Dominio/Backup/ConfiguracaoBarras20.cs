// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.ConfiguracaoBarras20
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System.Collections.Generic;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class ConfiguracaoBarras20
  {
    public ConfiguracaoBarras20 Pesquisar(int rep) => new ConfiguracaoBarras20().Pesquisar(rep);

    public bool Gravar(ConfiguracaoBarras20 config) => new ConfiguracaoBarras20().Gravar(config);

    public bool GravarLista(List<ConfiguracaoBarras20> listConfig) => new ConfiguracaoBarras20().InserirLista(listConfig);

    public List<ConfiguracaoBarras20> Pesquisar() => new ConfiguracaoBarras20().Pesquisar();

    public int VerificarSeExistTLM(int rep) => new ConfiguracaoBarras20().PesquisarTLM(rep);

    public int VerificarSeExisteCartaoBarras20(int rep)
    {
      try
      {
        return new ConfiguracaoBarras20().VerificarSeExisteCartaoBarras20(rep);
      }
      catch
      {
        return 0;
      }
    }

    public void AtualizarIgnoraFTPadrao(int rep, bool ignorar) => new ConfiguracaoBarras20().AtualizarIgnoraFTPadrao(rep, ignorar);

    public static void ExcluirBarras20(int rep) => new ConfiguracaoBarras20().Deletar(rep);
  }
}
