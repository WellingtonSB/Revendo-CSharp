// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.MigracaoAccessSqlServer
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class MigracaoAccessSqlServer
  {
    public const int Inner_Rep_Plus_Bio_LFD = 13;
    public const int Inner_Rep_Plus_Bio_Prox = 14;
    public const int Inner_Rep_Plus_Bio_Barras = 15;
    public const int Inner_Rep_Plus_Bio_Demo_LFD = 16;
    public const int Inner_Rep_Plus_Bio_LC = 17;
    public const int Inner_Rep_Plus_Bio_Demo_LC = 18;
    public const int Inner_Rep_Plus_Bio = 19;
    public const int Inner_Rep_Plus_Bio_Demo = 20;
    private static MigracaoAccessSqlServer EventMigra = new MigracaoAccessSqlServer();

    public static event EventHandler<NotificarMigraAccessSqlServerEventArgs> OnNotificarMigracao;

    public static bool MigrarBase()
    {
      RegistroAFD.OnNotificarTabAFD += new EventHandler<NotificarMigraAccessSqlServerEventArgs>(MigracaoAccessSqlServer.RegistroAFD_OnNotificarTabAFD);
      MigracaoAccessSqlServer migracaoAccessSqlServer = new MigracaoAccessSqlServer();
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_LIMPAR_BASE_SQL_INI, -1);
      migracaoAccessSqlServer.LimparBaseSqlServer();
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_LIMPAR_BASE_SQL_FIM, -1);
      MigracaoAccessSqlServer.NotificarEvento("", -1);
      try
      {
        MigracaoAccessSqlServer.MigrarEmpregadores();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_EMPREGADOR_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarEmpregado();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_EMPREGADO_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarGrupoOperador();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_GRUPOOPERADOR_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarGrupoOperadorPrivilegio();
      }
      catch
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_GRUPOOPERADORPRIVILEGIO_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarOperador();
      }
      catch
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_OPERADOR_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarArquivoAFD();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_ARQUIVOAFD_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarArquivoCFG();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_ARQUIVOCFG_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarColetaAutomatica();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_COLETAAUTOMATICA_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarConfiguracaoHorarioVerao();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_CONFIGURACAOHORARIOVERAO_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarConfiguracoes();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_CONFIGURACOES_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarFormatoCartao();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_FORMATOCARTAO_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarManutencaoDB();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_MANUTENCAODB_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarTemplates();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TEMPLATES_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarTemplatesCAMA();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TEMPLATES_CAMA_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarTemplatesLM();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TEMPLATES_LM_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarTipoArquivoMetaDado();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TIPOARQUIVOMETADADO_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarTipoTerminalMetaDado();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TIPOTERMINALMETADADO_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarVersao();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_VERSAO_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarGrupoRep();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_GRUPOREP_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarGrupoRepXusuario();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_GRUPOREPxUSUARIO_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarRep();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_REP_ERRO, -1);
        return false;
      }
      try
      {
        migracaoAccessSqlServer.LimparTabela("RepConfiguracaoGeral");
        MigracaoAccessSqlServer.MigrarRepConfiguracoesGerais();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_REPCONFIGURACAOGERAL_ERRO, -1);
        return false;
      }
      try
      {
        migracaoAccessSqlServer.LimparTabela("ConfiguracaoGeral");
        MigracaoAccessSqlServer.MigrarConfiguracaoGeral();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_CONFIGURACAOGERAL_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarAjusteBiometrico();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_AJUSTE_BIOMETRICO_ERRO, -1);
        return false;
      }
      try
      {
        MigracaoAccessSqlServer.MigrarConfiguracaoBio();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_CONFIGURACAO_BIO_ERRO, -1);
        return false;
      }
      if (!RegistrySingleton.GetInstance().UNIFICARCOLETAAFD)
      {
        try
        {
          MigracaoAccessSqlServer.MigrarRepAFD();
        }
        catch (Exception ex)
        {
          MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_REPAFD_ERRO, -1);
          return false;
        }
        try
        {
          MigracaoAccessSqlServer.MigrarTabelasTempAFD();
        }
        catch (Exception ex)
        {
          MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_AFD_ERRO, -1);
          return false;
        }
      }
      else
      {
        try
        {
          MigracaoAccessSqlServer.MigrarColetaAFD();
        }
        catch (Exception ex)
        {
          MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_AFD_ERRO, -1);
          return false;
        }
      }
      try
      {
        MigracaoAccessSqlServer.MigrarHorarioGerarArquivoAFD();
      }
      catch (Exception ex)
      {
        MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_AFD_ERRO, -1);
        return false;
      }
      return true;
    }

    private static bool MigrarEmpregadores()
    {
      int qtd = 0;
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_EMPREGADOR_INI, -1);
      try
      {
        ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        SortableBindingList<Empregador> sortableBindingList = new Empregador().PesquisarEmpregadores();
        if (sortableBindingList.Count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          Empregador empregador = new Empregador();
          foreach (Empregador EmpregadorEnt in (Collection<Empregador>) sortableBindingList)
          {
            if (empregador.InserirEmpregadorComID(EmpregadorEnt) > 0)
              ++qtd;
          }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_EMPREGADOR_FIM, qtd);
      return true;
    }

    private static bool MigrarEmpregado()
    {
      int qtd = 0;
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_EMPREGADO_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        Empregado empregado1 = new Empregado();
        empregado1.OnNotificarProgressBar += new EventHandler<EventArgsCustomizados.NotificarProgressBarEventArgs>(MigracaoAccessSqlServer.Empregado_OnNotificar);
        List<Empregado> listaEmpregados = empregado1.RecuperarEmpregadosDB();
        if (listaEmpregados.Count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          qtd = listaEmpregados.Count;
          Empregado empregado2 = new Empregado();
          empregado2.OnNotificarProgressBar += new EventHandler<EventArgsCustomizados.NotificarProgressBarEventArgs>(MigracaoAccessSqlServer.EmpregadoSql_OnNotificar);
          empregado2.InserirListaEmpregadosComID(listaEmpregados);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_EMPREGADO_FIM, qtd);
      return true;
    }

    private static bool MigrarGrupoOperador()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_GRUPOOPERADOR_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<GrupoOperador> listGrupoOperador = new GrupoOperador().PesquisarGrupoOperador();
        count = listGrupoOperador.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new GrupoOperador().InserirListaGrupoOperador(listGrupoOperador);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_GRUPOOPERADOR_FIM, count);
      return true;
    }

    private static void MigrarGrupoOperadorPrivilegio()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_GRUPOOPERADORPRIVILEGIO_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<GrupoOperadorPrivilegio> listaOperador = new GrupoOperadorPrivilegio().PesquisarGrupoOperadorPrivilegio();
        count = listaOperador.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new GrupoOperadorPrivilegio().InserirListaGrupoOperadorPrivilegio(listaOperador);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_GRUPOOPERADORPRIVILEGIO_FIM, count);
    }

    private static void MigrarOperador()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_OPERADOR_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        SortableBindingList<Operador> listaOperador = new Operador().PesquisarOperador();
        count = listaOperador.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new Operador().InserirListaOperadorID(listaOperador);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_OPERADOR_FIM, count);
    }

    private static void MigrarArquivoAFD()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_ARQUIVOAFD_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<ArquivoBilhete> listaArquivo = new ArquivoBilhete().PesquisarArquivosAFD();
        count = listaArquivo.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new ArquivoBilhete().InserirListaArquivoAFD(listaArquivo);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_ARQUIVOAFD_FIM, count);
    }

    private static void MigrarArquivoCFG()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_ARQUIVOCFG_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<ArquivoCFGEntity> ListaArquivoCFGEnt = new ArquivoCFGDA().PesquisarArquivoCFG(CommandType.Text);
        count = ListaArquivoCFGEnt.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new ArquivoCFGDA().InserirListaArquivoCFG(ListaArquivoCFGEnt);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_ARQUIVOCFG_FIM, count);
    }

    private static void MigrarColetaAutomatica()
    {
      int qtd = 0;
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_COLETAAUTOMATICA_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        ColetaAutomatica _objColetaAuto = new ColetaAutomatica().PesquisarColetaAutomatica();
        if (_objColetaAuto != null)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new ColetaAutomatica().InserirColetaAutomatica(_objColetaAuto);
          ++qtd;
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_COLETAAUTOMATICA_FIM, qtd);
    }

    private static void MigrarConfiguracaoHorarioVerao()
    {
      int qtd = 0;
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_CONFIGURACAOHORARIOVERAO_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        Relogio relogio = new Relogio().PesquisarHorVeraoMulti();
        if (relogio != null)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new Relogio().InserirHorVeraoMulti(relogio);
          ++qtd;
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_CONFIGURACAOHORARIOVERAO_FIM, qtd);
    }

    private static void MigrarConfiguracoes()
    {
      int num = 0;
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_CONFIGURACOES_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int qtd;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        new AtualizadorRegistroDAO().CarregarRegistrySingletonDaBase();
        RegistrySingleton instance = RegistrySingleton.GetInstance();
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
        new ManutencaoDB().InserirConfiguracoes(instance);
        qtd = num + 1;
        new AtualizadorRegistroDAO().CarregarRegistrySingletonDaBase();
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_CONFIGURACOES_FIM, qtd);
    }

    private static bool MigrarFormatoCartao()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_FORMATOCARTAO_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<FormatoCartao> listaFormatoCartao = new FormatoCartao().PesquisarFormatosCartaoMigracao();
        count = listaFormatoCartao.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new FormatoCartao().InserirListaFormatosCartao(listaFormatoCartao);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_FORMATOCARTAO_FIM, count);
      return true;
    }

    private static void MigrarManutencaoDB()
    {
      int qtd = 0;
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_MANUTENCAODB_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        ManutencaoDB manutencaoDB = new ManutencaoDB().PesquisarManutencaoDB();
        if (manutencaoDB != null)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new ManutencaoDB().InserirManutencaoDB(manutencaoDB);
          ++qtd;
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_MANUTENCAODB_FIM, qtd);
    }

    private static bool MigrarTemplates()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TEMPLATES_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        TemplateBio templateBio1 = new TemplateBio();
        templateBio1.OnNotificarImportacaoTemplate += new EventHandler<EventArgsCustomizados.NotificarImportacaoTemplate>(MigracaoAccessSqlServer.TemplatesAccess_OnNotificar);
        List<UsuarioBio> ListaTemplates = templateBio1.PesquisarTemplatesID();
        count = ListaTemplates.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          TemplateBio templateBio2 = new TemplateBio();
          templateBio2.OnNotificarImportacaoTemplate += new EventHandler<EventArgsCustomizados.NotificarImportacaoTemplate>(MigracaoAccessSqlServer.TemplatesSql_OnNotificar);
          templateBio2.InserirListaTemplatesID(ListaTemplates);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TEMPLATES_FIM, count);
      return true;
    }

    private static bool MigrarTemplatesCAMA()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TEMPLATES_CAMA_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        TemplateBio templateBio1 = new TemplateBio();
        templateBio1.OnNotificarImportacaoTemplate += new EventHandler<EventArgsCustomizados.NotificarImportacaoTemplate>(MigracaoAccessSqlServer.TemplatesAccess_OnNotificar);
        List<UsuarioBio> ListaTemplates = templateBio1.PesquisarTemplatesIDCama();
        count = ListaTemplates.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          TemplateBio templateBio2 = new TemplateBio();
          templateBio2.OnNotificarImportacaoTemplate += new EventHandler<EventArgsCustomizados.NotificarImportacaoTemplate>(MigracaoAccessSqlServer.TemplatesSql_OnNotificar);
          templateBio2.InserirListaTemplatesIDCama(ListaTemplates);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TEMPLATES_CAMA_FIM, count);
      return true;
    }

    private static bool MigrarTemplatesLM()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TEMPLATES_LM_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        TemplateBio templateBio1 = new TemplateBio();
        templateBio1.OnNotificarImportacaoTemplate += new EventHandler<EventArgsCustomizados.NotificarImportacaoTemplate>(MigracaoAccessSqlServer.TemplatesAccess_OnNotificar);
        List<UsuarioBio> ListaTemplates = templateBio1.PesquisarTemplatesIDLM();
        count = ListaTemplates.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          TemplateBio templateBio2 = new TemplateBio();
          templateBio2.OnNotificarImportacaoTemplate += new EventHandler<EventArgsCustomizados.NotificarImportacaoTemplate>(MigracaoAccessSqlServer.TemplatesSql_OnNotificar);
          templateBio2.InserirListaTemplatesIDLM(ListaTemplates);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TEMPLATES_CAMA_FIM, count);
      return true;
    }

    private static bool MigrarTipoArquivoMetaDado()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TIPOARQUIVOMETADADO_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<TipoArquivoMetaDado> ListaTipoArquivo = new ManutencaoDB().PesquisarTipoArquivoMetaDado();
        count = ListaTipoArquivo.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new ManutencaoDB().InserirTipoArquivoMetaDado(ListaTipoArquivo);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TIPOARQUIVOMETADADO_FIM, count);
      return true;
    }

    private static bool MigrarTipoTerminalMetaDado()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TIPOTERMINALMETADADO_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<TipoTerminal> ListaTipoTerminal = new TipoTerminal().PesquisarDescricaoTipoTerminal();
        count = ListaTipoTerminal.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new TipoTerminal().InserirTipoTerminalMetaDado(ListaTipoTerminal);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_TIPOTERMINALMETADADO_FIM, count);
      return true;
    }

    private static bool MigrarVersao()
    {
      int qtd = 0;
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_VERSAO_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        Versao versaoEnt = new VersaoDA().PesquisarVersao();
        if (versaoEnt != null)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new VersaoDA().InserirVersao(versaoEnt, CommandType.Text);
          ++qtd;
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_VERSAO_FIM, qtd);
      return true;
    }

    private static bool MigrarGrupoRep()
    {
      int qtd = 0;
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_GRUPOREP_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        SortableBindingList<GrupoRep> lstGrupoRepEnt = new GrupoRepDAO().PesquisarTodosGrupos();
        if (lstGrupoRepEnt != null)
        {
          if (lstGrupoRepEnt.Count > 0)
          {
            MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
            new GrupoRepDAO().InserirGrupoRepComID(lstGrupoRepEnt);
            qtd = lstGrupoRepEnt.Count;
          }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_GRUPOREP_FIM, qtd);
      return true;
    }

    private static bool MigrarGrupoRepXusuario()
    {
      int qtd = 0;
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_GRUPOREPxUSUARIO_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        SortableBindingList<GrupoRepXempregados> LstAssociacoes = new GrupoRepXempregadosDAO().PesquisarGruposXempregados();
        if (LstAssociacoes != null)
        {
          if (LstAssociacoes.Count > 0)
          {
            MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
            new GrupoRepXempregadosDAO().InserirListaGrupoRepXempregados(LstAssociacoes);
            qtd = LstAssociacoes.Count;
          }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_GRUPOREPxUSUARIO_FIM, qtd);
      return true;
    }

    private static void MigrarRep()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_REP_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<RepBase> ListaRepBase = new RepBase().PesquisarReps();
        count = ListaRepBase.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new RepBase().InserirListaRepID(ListaRepBase);
        }
        MigracaoAccessSqlServer.MigrarFormatoCartaoRepPlus();
        MigracaoAccessSqlServer.MigrarConfiguracaoBarras20();
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_REP_FIM, count);
    }

    private static void MigrarConfiguracaoBarras20()
    {
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<ConfiguracaoBarras20> Listconf = new ConfiguracaoBarras20().Pesquisar();
        if (Listconf.Count <= 0)
          return;
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
        new ConfiguracaoBarras20().InserirLista(Listconf);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private static void MigrarRepConfiguracoesGerais()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_REPCONFIGURACAOGERAL_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<RepBase> repBaseList = new RepBase().PesquisarConfiguracoesGeraisReps();
        count = repBaseList.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          RepBase repBase = new RepBase();
          foreach (RepBase RepBaseEnt in repBaseList)
            repBase.InserirRepConfiguracalGeral(RepBaseEnt);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_REPCONFIGURACAOGERAL_FIM, count);
    }

    private static void MigrarConfiguracaoGeral()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_CONFIGURACAOGERAL_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<ConfiguracaoGeral> lstConfigGeralEnt = new RepBase().PesquisarConfiguracaoGeral();
        count = lstConfigGeralEnt.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new RepBase().InserirConfiguracaoGeral(lstConfigGeralEnt);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_CONFIGURACAOGERAL_FIM, count);
    }

    private static void MigrarFormatoCartaoRepPlus()
    {
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<RepBase> repBaseList1 = new RepBase().PesquisarReps();
        if (repBaseList1.Count <= 0)
          return;
        foreach (RepBase repBase1 in repBaseList1)
        {
          if (repBase1.TerminalId == 19 || repBase1.TerminalId == 14 || repBase1.TerminalId == 15 || repBase1.TerminalId == 20 || repBase1.TerminalId == 17 || repBase1.TerminalId == 18 || repBase1.TerminalId == 13 || repBase1.TerminalId == 16)
          {
            MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
            FormatoCartao formatoCartao1 = new FormatoCartao();
            FormatoCartao formatoCartao2 = new FormatoCartao();
            FormatoCartao formatoCartao3 = new FormatoCartao().PesquisarLstFormatosCartaoBarrasRepPlus(repBase1.FormatoCartaoId, repBase1.ConfiguracaoId);
            if (repBase1.FormatoCartaoId != 0)
              formatoCartao3.formatoCartao = "";
            FormatoCartao formatoCartao4 = new FormatoCartao().PesquisarLstFormatosCartaoProxRepPlus(repBase1.FormatoCartaoProxId, repBase1.ConfiguracaoId);
            MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
            FormatoCartao formatoCartao5 = new FormatoCartao();
            FormatoCartao formatoCartao6 = new FormatoCartao();
            List<RepBase> repBaseList2 = new RepBase().PesquisarReps();
            int ConfiguracaoId = 0;
            foreach (RepBase repBase2 in repBaseList2)
            {
              if (repBase2.Desc.Equals(repBase1.Desc))
              {
                ConfiguracaoId = repBase2.ConfiguracaoId;
                break;
              }
            }
            formatoCartao6.AtualizarFormatoPadraoLivreRepPlus(formatoCartao3.formatoCartao, formatoCartao3.formatoCartaoID, formatoCartao3.numDigitosFixos, ConfiguracaoId);
            if (formatoCartao4.formatoCartaoID != 7)
              formatoCartao4.formatoCartao = "";
            formatoCartao5.AtualizarFormatoPadraoAbatrackRepPlus(formatoCartao4.formatoCartao, formatoCartao4.formatoCartaoID, formatoCartao4.numDigitosFixos, ConfiguracaoId);
          }
          else
          {
            MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
            FormatoCartao formatoCartao7 = new FormatoCartao();
            FormatoCartao formatoCartao8 = FormatoCartao.PesquisarLstFormatosCartao(repBase1.FormatoCartaoId, repBase1.ConfiguracaoId);
            if (repBase1.FormatoCartaoId != 0)
              formatoCartao8.formatoCartao = "";
            MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
            List<RepBase> repBaseList3 = new RepBase().PesquisarReps();
            int configuracaoId = 0;
            foreach (RepBase repBase3 in repBaseList3)
            {
              if (repBase3.Desc.Equals(repBase1.Desc))
              {
                configuracaoId = repBase3.ConfiguracaoId;
                break;
              }
            }
            FormatoCartao.AtualizarFormatoPadraoLivre(formatoCartao8.formatoCartao, formatoCartao8.formatoCartaoID, configuracaoId);
          }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private static void MigrarRepAFD()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_REPAFD_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<RepAFD> listaRepAFD = new RepAFD().RecuperaListaRepAFD();
        count = listaRepAFD.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new RepAFD().InsereListaRepAFD(listaRepAFD);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_REPAFD_FIM, count);
    }

    private static void MigrarColetaAFD()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_COLETAAFD_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<RegistroAFD> listaRegistrosAFD = new RegistroAFD().PesquisarTodosOsRegistrosColetaAFD();
        count = listaRegistrosAFD.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new RegistroAFD().MigrarRegistrosAFDUnificado(listaRegistrosAFD);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_COLETAAFD_FIM, count);
    }

    private static void MigrarHorarioGerarArquivoAFD()
    {
      int qtd = 1;
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_HORARIO_ARQUIVO_AFD_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        HorarioGerarArquivoAFD _objHorarioGerarArquivoAFD = new HorarioGerarArquivoAFD().PesquisarHorarioGerarArquivoAFD();
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
        new HorarioGerarArquivoAFD().AlterarHorarioGerarArquivoAFD(_objHorarioGerarArquivoAFD);
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_HORARIO_ARQUIVO_AFD_FIM, qtd);
    }

    private static void MigrarTabelasTempAFD()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_AFD_INI_ALL, -1);
      MigracaoAccessSqlServer.NotificarEvento("", -1);
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        RepAFD repAfd = new RepAFD();
        RegistroAFD registroAfd1 = new RegistroAFD();
        List<RepAFD> repAfdList = repAfd.RecuperaListaRepAFD();
        if (repAfdList.Count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          RegistroAFD registroAfd2 = new RegistroAFD();
          foreach (RepAFD _repAFD in repAfdList)
          {
            bool flag = true;
            int UltNSR = 0;
            int qtd = 0;
            MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_AFD_INI + _repAFD.nomeTabela, -1);
            while (flag)
            {
              List<RegistroAFD> registroAfdList = new List<RegistroAFD>();
              MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
              List<RegistroAFD> listaRegistrosAFD = new RegistroAFD().PesquisarTodosOsRegistrosAFD(_repAFD, UltNSR);
              int count = listaRegistrosAFD.Count;
              if (count > 0)
              {
                qtd += count;
                UltNSR = listaRegistrosAFD[listaRegistrosAFD.Count - 1].NSR;
                MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
                new RegistroAFD().GravarRegistrosAFD(listaRegistrosAFD, _repAFD);
              }
              else
                flag = false;
            }
            MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_AFD_FIM + _repAFD.nomeTabela + Resources.msgMIGRA_TABELA_AFD_FIM2, qtd);
          }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_AFD_FIM_ALL, -1);
    }

    private static void MigrarAjusteBiometrico()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_AJUSTE_BIOMETRICO_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<RepBio> listaAjusteBio = new RepBio().RecuperaListaAjusteBiometrico();
        count = listaAjusteBio.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new RepBio().InserirListaAjusteBiometrico(listaAjusteBio);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_AJUSTE_BIOMETRICO_FIM, count);
    }

    private static void MigrarConfiguracaoBio()
    {
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_CONFIGURACAO_BIO_INI, -1);
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      int count;
      try
      {
        MigracaoAccessSqlServer.AlteraTipoConexaoDB(0);
        List<ConfiguracoesBio> listaConfigBio = new RepBio().RecuperarListaConfiguracoesBio();
        count = listaConfigBio.Count;
        if (count > 0)
        {
          MigracaoAccessSqlServer.AlteraTipoConexaoDB(1);
          new RepBio().InserirListaConfigBio(listaConfigBio);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      MigracaoAccessSqlServer.NotificarEvento(Resources.msgMIGRA_TABELA_CONFIGURACAO_BIO_FIM, count);
    }

    private static void NotificarEvento(string notificacao, int qtd)
    {
      if (MigracaoAccessSqlServer.OnNotificarMigracao == null)
        return;
      NotificarMigraAccessSqlServerEventArgs e = new NotificarMigraAccessSqlServerEventArgs(notificacao, qtd);
      MigracaoAccessSqlServer.OnNotificarMigracao((object) MigracaoAccessSqlServer.OnNotificarMigracao, e);
    }

    private static void AlteraTipoConexaoDB(int TipoConexao)
    {
      ConfigBD_Entidade configBdEntidade = new ConfigBD_Entidade();
      ConfigBD_Entidade instance = ConfigBD_BI.getInstance();
      instance.tipoConexao = TipoConexao;
      ConfigBD_BI.GravarArquivoIni(instance);
    }

    private static void Empregado_OnNotificar(
      object sender,
      EventArgsCustomizados.NotificarProgressBarEventArgs e)
    {
      MigracaoAccessSqlServer.NotificarEvento("Refresh", -5);
    }

    private static void EmpregadoSql_OnNotificar(
      object sender,
      EventArgsCustomizados.NotificarProgressBarEventArgs e)
    {
      MigracaoAccessSqlServer.NotificarEvento("Refresh", -5);
    }

    private static void TemplatesAccess_OnNotificar(
      object sender,
      EventArgsCustomizados.NotificarImportacaoTemplate e)
    {
      MigracaoAccessSqlServer.NotificarEvento("Refresh", -5);
    }

    private static void TemplatesSql_OnNotificar(
      object sender,
      EventArgsCustomizados.NotificarImportacaoTemplate e)
    {
      MigracaoAccessSqlServer.NotificarEvento("Refresh", -5);
    }

    private static void RegistroAFD_OnNotificarTabAFD(
      object sender,
      NotificarMigraAccessSqlServerEventArgs e)
    {
      MigracaoAccessSqlServer.NotificarEvento("Refresh", -5);
    }
  }
}
