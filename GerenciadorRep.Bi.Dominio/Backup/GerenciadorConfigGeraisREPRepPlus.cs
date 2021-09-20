// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigGeraisREPRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigGeraisREPRepPlus : TarefaAbstrata
  {
    private Relogio _entRelogio;
    private FormatoCartao _formatoEntBarras;
    private FormatoCartao _formatoEntProx;
    private RepBase _rep;
    private bool _chamadaPeloSdk;
    private GerenciadorConfigGeraisREPRepPlus.Estados _estadoRep;
    public static GerenciadorConfigGeraisREPRepPlus _gerenciadorConfigGeraisREPRepPlus;

    public static GerenciadorConfigGeraisREPRepPlus getInstance() => GerenciadorConfigGeraisREPRepPlus._gerenciadorConfigGeraisREPRepPlus != null ? GerenciadorConfigGeraisREPRepPlus._gerenciadorConfigGeraisREPRepPlus : new GerenciadorConfigGeraisREPRepPlus();

    public static GerenciadorConfigGeraisREPRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigGeraisREPRepPlus._gerenciadorConfigGeraisREPRepPlus != null ? GerenciadorConfigGeraisREPRepPlus._gerenciadorConfigGeraisREPRepPlus : new GerenciadorConfigGeraisREPRepPlus(rep);
    }

    public static GerenciadorConfigGeraisREPRepPlus getInstance(
      RepBase rep,
      Relogio entRelogio,
      FormatoCartao formatoCartaoBarras,
      FormatoCartao formatoCartaoProx)
    {
      return GerenciadorConfigGeraisREPRepPlus._gerenciadorConfigGeraisREPRepPlus != null ? GerenciadorConfigGeraisREPRepPlus._gerenciadorConfigGeraisREPRepPlus : new GerenciadorConfigGeraisREPRepPlus(rep, entRelogio, formatoCartaoBarras, formatoCartaoProx);
    }

    public GerenciadorConfigGeraisREPRepPlus()
    {
    }

    public GerenciadorConfigGeraisREPRepPlus(RepBase rep)
    {
      this._rep = rep;
      this._chamadaPeloSdk = false;
    }

    public GerenciadorConfigGeraisREPRepPlus(
      RepBase rep,
      Relogio entRelogio,
      FormatoCartao formatoCartaoBarras,
      FormatoCartao formatoCartaoProx)
    {
      this._rep = rep;
      this._entRelogio = entRelogio;
      this._formatoEntProx = formatoCartaoProx;
      this._formatoEntBarras = formatoCartaoBarras;
      this._chamadaPeloSdk = true;
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarConfiguracoesGerais()
    {
      this._estadoRep = GerenciadorConfigGeraisREPRepPlus.Estados.estEnvioConfiguracoes;
      if (!this._chamadaPeloSdk)
        this._entRelogio = new Relogio().PesquisarHorVeraoMulti();
      try
      {
        string message = this.ValidaParametrosInnerRep(this._rep.Local, this._rep.SenhaComunic, this._rep.SenhaRelogio, this._rep.SenhaBio, this._rep.TipoTerminalId);
        if (message != "")
          throw new Exception(message);
        try
        {
          Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
          int num1 = 0;
          int num2 = 0;
          if (!this._chamadaPeloSdk)
          {
            this._formatoEntBarras = FormatoCartao.PesquisarFormatoCartaoBarrasByRepIdRepPlus(this._rep.RepId);
            this._formatoEntProx = FormatoCartao.PesquisarFormatoCartaoProxByRepIdRepPlus(this._rep.RepId);
            switch (this._formatoEntBarras.formatoCartaoID)
            {
              case 0:
                num1 = 0;
                break;
              case 3:
                num1 = 1;
                break;
              case 4:
                num1 = 2;
                break;
              case 5:
                num1 = 4;
                break;
              case 6:
                num1 = 3;
                break;
            }
            switch (this._formatoEntProx.formatoCartaoID)
            {
              case 1:
                num2 = 1;
                break;
              case 2:
                num2 = 2;
                break;
              case 7:
                num2 = 0;
                break;
            }
          }
          MsgTCPAplicacaoConfigGeralREPRepPlus configGeralRepRepPlus = new MsgTCPAplicacaoConfigGeralREPRepPlus(this._entRelogio.InicioHorVerao, this._entRelogio.FimHorVerao, this._rep.SenhaComunic, this._rep.SenhaBio, this._rep.SenhaRelogio, this._formatoEntBarras.formatoCartao, this._formatoEntProx.formatoCartao, (long) this._formatoEntBarras.numDigitosFixos, (long) num1, (long) num2);
          envelope.MsgAplicacao = (MsgTcpAplicacaoBase) configGeralRepRepPlus;
          this.ClienteSocket.Enviar(envelope, true);
        }
        catch
        {
        }
      }
      catch (Exception ex)
      {
        this.NotificarParaUsuario(ex.Message.ToString(), EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
      }
    }

    private string ValidaParametrosInnerRep(
      string local,
      string senhaComunic,
      string senhaRelogio,
      string senhaBio,
      int TerminalId)
    {
      ulong result = 0;
      try
      {
        string str = local.Trim();
        if (str.Length > 100)
          return "Campo local maior que 100 dígitos";
        if (str == string.Empty)
          return "Campo local não pode ser vazio.";
        string s1 = senhaComunic.Trim();
        bool flag1 = ulong.TryParse(s1, out result);
        if (s1.Length != 6 && s1 != string.Empty)
          return "Campo senha de comunicação diferente que 6 dígitos";
        if (!flag1 && s1 != string.Empty)
          return "Campo senha de comunicação não pode ser carácter, somente número";
        if (s1 == string.Empty)
          return "Campo senha de comunicação não pode ser vazio";
        string s2 = senhaRelogio.Trim();
        bool flag2 = ulong.TryParse(s2, out result);
        if (s2.Length != 6 && s2 != string.Empty)
          return "Campo senha do relógio diferente que 6 dígitos";
        if (!flag2 && s2 != string.Empty)
          return "Campo senha do relógio não pode ser carácter, somente número";
        if (s2 == string.Empty)
          return "Campo senha do relógio não pode ser vazio";
        if (TerminalId == 1 || TerminalId == 6 || TerminalId == 7)
        {
          string s3 = senhaBio.Trim();
          if (!ulong.TryParse(s3, out result) && s3 != string.Empty)
            return "Campo senha de biometria não pode ser carácter, somente número";
          if (s3.Length != 6 && s3 != string.Empty)
            return "Campo senha de biometria diferente que 6 dígitos";
          if (s3 == string.Empty)
            return "Campo senha de biometria não pode ser vazio";
        }
        return "";
      }
      catch
      {
        return "Erro de conversão no banco de dados";
      }
    }

    private void EnviarBufferCFGTlm()
    {
      this._estadoRep = GerenciadorConfigGeraisREPRepPlus.Estados.estEnvioBufferCFGTlm;
      ArquivoCFGEntity arquivoCfgEntity = (ArquivoCFGEntity) null;
      ArquivoCFGBI arquivoCfgbi = new ArquivoCFGBI();
      if (!this._chamadaPeloSdk)
        arquivoCfgEntity = arquivoCfgbi.PesquisarArquivoCFGPorRepID(this._rep.RepId);
      this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
      {
        MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoBufferCFGTlm(PadraoTLM.GetByteArray(arquivoCfgEntity.CFG))
      }, true);
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      switch (this._estadoRep)
      {
        case GerenciadorConfigGeraisREPRepPlus.Estados.estEnvioConfiguracoes:
          if (envelope.Grp != (byte) 10 || envelope.Cmd != (byte) 101)
            break;
          if (!this._chamadaPeloSdk)
          {
            if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
            {
              if (FormatoCartao.PesquisarFormatoCartaoBarrasByRepIdRepPlus(this._rep.RepId).formatoCartaoID == 6)
              {
                this.EnviarBufferCFGTlm();
                break;
              }
              this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
              this.EncerrarConexao();
              break;
            }
            this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
            this.EncerrarConexao();
            break;
          }
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
          this.EncerrarConexao();
          break;
        case GerenciadorConfigGeraisREPRepPlus.Estados.estEnvioBufferCFGTlm:
          if (envelope.Grp != (byte) 10 || envelope.MsgAplicacaoEmBytes[2] != (byte) 103)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
          {
            this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            this.EncerrarConexao();
            break;
          }
          this.NotificarParaUsuario(Resources.msgPROCESSO_DE_ENVIO_DE_CONFIGURACOES_FINALIZADO, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          this.EncerrarConexao();
          break;
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarConfiguracoesGerais();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorConfigGeraisREPRepPlus.Estados.estEnvioConfiguracoes)
        menssagem = Resources.msgTIMEOUT_ENVIO_CONFIG_GERAL;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigGeraisREPRepPlus.Estados.estEnvioConfiguracoes:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_CONFIG_GERAL;
          break;
        case GerenciadorConfigGeraisREPRepPlus.Estados.estEnvioBufferCFGTlm:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_CONFIG_GERAL;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioConfiguracoes,
      estEnvioBufferCFGTlm,
      estFinal,
    }
  }
}
