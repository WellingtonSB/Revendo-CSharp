// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorColeta
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Text;
using TopData.Framework.Comunicacao;
using TopData.Framework.Comunicacao.MsgTcpRep;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorColeta : TarefaAbstrata
  {
    private RepBase _rep;
    private uint _ultimoNsr;
    private bool _primeiroPedido;
    private bool _primeiroPedidoSemMarcacoes;
    private GerenciadorColeta.Estados _estadoRep;
    private uint _totBilhetesRecebidos;
    private uint _totBilhetesProcessados;
    private uint _totBilhetesCarimboInvalido;
    private bool _recuperarTodasMarcacoes;
    public static GerenciadorColeta _gerenciadorColeta;

    public event EventHandler<NotificarTotBilhetesParaUsuarioEventArgs> OnNotificarBilhetesProcessadosParaUsuario;

    public static GerenciadorColeta getInstance() => GerenciadorColeta._gerenciadorColeta != null ? GerenciadorColeta._gerenciadorColeta : new GerenciadorColeta();

    public static GerenciadorColeta getInstance(RepBase rep) => GerenciadorColeta._gerenciadorColeta != null ? GerenciadorColeta._gerenciadorColeta : new GerenciadorColeta(rep);

    public GerenciadorColeta()
    {
    }

    public GerenciadorColeta(RepBase rep) => this._rep = rep;

    ~GerenciadorColeta()
    {
    }

    public uint TotBilhetesRecebidos => this._totBilhetesRecebidos;

    public uint TotBilhetesProcessados => this._totBilhetesProcessados;

    public uint TotBilhetesCarimboInvalido => this._totBilhetesCarimboInvalido;

    public void IniciarProcesso(bool recuperarTodasMarcacoes, uint ultimoNsr)
    {
      this._totBilhetesRecebidos = 0U;
      this._totBilhetesProcessados = 0U;
      this._totBilhetesCarimboInvalido = 0U;
      this._ultimoNsr = ultimoNsr;
      this._recuperarTodasMarcacoes = recuperarTodasMarcacoes;
      this._primeiroPedidoSemMarcacoes = false;
      this._primeiroPedido = true;
      this.Conectar(this._rep);
    }

    private void EnviarSolicitacaoBilheteRep(uint nsr)
    {
      try
      {
        this._estadoRep = GerenciadorColeta.Estados.estSolicitoBilhete;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) new MsgTcpAplicacaoSolicitaBilheteRep(nsr)
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorColeta.Estados.estSolicitoBilhete || envelope.Grp != (byte) 5 || envelope.Cmd != (byte) 1)
        return;
      MsgTcpAplicacaoRespostaBilheteRep msgRespostaBilhete = this.AbrirMsgRespostaBilheteRep(envelope);
      if (msgRespostaBilhete.TerminouBilhete)
      {
        if (this._primeiroPedidoSemMarcacoes)
        {
          this.NotificarParaUsuario(Resources.msg_NENHUM_BILHETES, EnumEstadoNotificacaoParaUsuario.nenhumEstado, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
        }
        else
        {
          this.GravarMarcacoesRep(msgRespostaBilhete);
          this.NotificarParaUsuario(Resources.msg_BILHETES_BAIXADOS, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
          if (!this._recuperarTodasMarcacoes)
          {
            this.NotificarParaUsuario(Resources.msgPROCESSANDO_ARQUIVO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.nenhumaMarcacao, this._rep.RepId, this._rep.Local);
            this.ProcessarMarcacoesRep();
          }
          this.NotificarParaUsuario(Resources.msgPROCESSO_FINALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
        }
        this.EncerrarConexao();
      }
      else
      {
        this.GravarMarcacoesRep(msgRespostaBilhete);
        this.EnviarSolicitacaoBilheteRep(this._ultimoNsr);
      }
    }

    private void GravarMarcacoesRep(
      MsgTcpAplicacaoRespostaBilheteRep msgRespostaBilhete)
    {
      try
      {
        Bilhete bilhete1 = new Bilhete();
        Bilhete bilhete2 = new Bilhete();
        foreach (BilheteRep lstBilhete in msgRespostaBilhete.LstBilhetes)
        {
          DateTime dateTime1 = new DateTime(2000 + (int) lstBilhete.Ano, (int) lstBilhete.Mes, (int) lstBilhete.Dia);
          bilhete2.Data = dateTime1;
          DateTime dateTime2 = new DateTime(2000 + (int) lstBilhete.Ano, (int) lstBilhete.Mes, (int) lstBilhete.Dia, (int) lstBilhete.Hora, (int) lstBilhete.Minuto, 0);
          bilhete2.Hora = dateTime2;
          bilhete2.RepId = this._rep.RepId;
          bilhete2.Processado = false;
          StringBuilder stringBuilder = new StringBuilder();
          foreach (byte num in lstBilhete.PIS)
            stringBuilder.Append(num.ToString("x").PadLeft(2, '0'));
          bilhete2.Pis = stringBuilder.ToString();
          if (bilhete2.Pis.Substring(0, 1) == "0")
            bilhete2.Pis = bilhete2.Pis.Substring(1, bilhete2.Pis.Length - 1);
          Array.Reverse((Array) lstBilhete.Nsr);
          uint uint32 = BitConverter.ToUInt32(lstBilhete.Nsr, 0);
          bilhete2.Nsr = uint32;
          CultureInfo cultureInfo = new CultureInfo("");
          bilhete2.Carimbo = CheckSum.GerarCheckSumRep(bilhete2.Pis, dateTime2.ToString("G", (IFormatProvider) cultureInfo), bilhete2.Nsr.ToString());
          int num1 = bilhete1.InserirBilheteRep(bilhete2, CommandType.Text);
          this._ultimoNsr = bilhete2.Nsr;
          if (num1 > 0)
            ++this._totBilhetesRecebidos;
          NotificarTotBilhetesParaUsuarioEventArgs e = new NotificarTotBilhetesParaUsuarioEventArgs(this._rep.RepId, this._totBilhetesRecebidos.ToString());
          if (this.OnNotificarBilhetesProcessadosParaUsuario != null)
            this.OnNotificarBilhetesProcessadosParaUsuario((object) this, e);
        }
      }
      catch (AppTopdataException ex)
      {
        if (!ExceptionPolicy.HandleException((Exception) ex, "Politica Erro Tratado"))
          return;
        throw;
      }
      catch (Exception ex)
      {
        string aconteceuErroInesperado = Resources.errACONTECEU_ERRO_INESPERADO;
        ex.Data.Add((object) "mensagem", (object) aconteceuErroInesperado);
        if (!ExceptionPolicy.HandleException(ex, "Politica Dominio"))
          return;
        throw;
      }
    }

    private MsgTcpAplicacaoRespostaBilheteRep AbrirMsgRespostaBilheteRep(
      Envelope envelope)
    {
      int num = 180;
      MsgTcpAplicacaoRespostaBilheteRep respostaBilheteRep = new MsgTcpAplicacaoRespostaBilheteRep();
      byte[] aplicacaoEmBytes = envelope.MsgAplicacaoEmBytes;
      byte[] numArray1 = new byte[num * 15];
      Array.Copy((Array) aplicacaoEmBytes, 2, (Array) numArray1, 0, numArray1.Length);
      for (int index = 0; index < num; ++index)
      {
        BilheteRep bilhete = new BilheteRep();
        bilhete.Dia = numArray1[4 + index * 15];
        if (bilhete.Dia == (byte) 0)
        {
          respostaBilheteRep.TerminouBilhete = true;
          if (this._primeiroPedido)
            this._primeiroPedidoSemMarcacoes = true;
          return respostaBilheteRep;
        }
        byte[] numArray2 = new byte[4]
        {
          numArray1[index * 15],
          numArray1[1 + index * 15],
          numArray1[2 + index * 15],
          numArray1[3 + index * 15]
        };
        bilhete.Nsr = numArray2;
        bilhete.Mes = numArray1[5 + index * 15];
        bilhete.Ano = numArray1[6 + index * 15];
        bilhete.Hora = numArray1[7 + index * 15];
        bilhete.Minuto = numArray1[8 + index * 15];
        byte[] numArray3 = new byte[6]
        {
          numArray1[9 + index * 15],
          numArray1[10 + index * 15],
          numArray1[11 + index * 15],
          numArray1[12 + index * 15],
          numArray1[13 + index * 15],
          numArray1[14 + index * 15]
        };
        bilhete.PIS = numArray3;
        this._primeiroPedido = false;
        respostaBilheteRep.Add(bilhete);
      }
      return respostaBilheteRep;
    }

    public void ProcessarMarcacoesRep()
    {
      this._totBilhetesProcessados = 0U;
      ArquivoBilhete arquivoBilhete = this._rep.ArquivoBilhete;
      SortableBindingList<ArquivoBilhete> sortableBindingList1 = new SortableBindingList<ArquivoBilhete>();
      int empregadorId = this._rep.Empregador.PesquisarEmpregadorDeUmREP(this._rep.RepId).EmpregadorId;
      SortableBindingList<ArquivoBilhete> sortableBindingList2 = arquivoBilhete.PesquisarArquivoBilhetePorEmpregador(empregadorId);
      Bilhete bilhete1 = new Bilhete();
      foreach (ArquivoBilhete arquivo in (Collection<ArquivoBilhete>) sortableBindingList2)
      {
        foreach (Bilhete bilhete2 in bilhete1.PesquisarBilhetesRep())
        {
          arquivoBilhete.AssociarEntidadeArquivoBilhete(arquivo);
          CultureInfo cultureInfo = new CultureInfo("");
          if (CheckSum.ValidaCheckSumRep(bilhete2.Pis, bilhete2.Hora.ToString("G", (IFormatProvider) cultureInfo), bilhete2.Nsr.ToString(), bilhete2.Carimbo))
          {
            ++this._totBilhetesProcessados;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("010 ");
            DateTime data = bilhete2.Data;
            string str1 = data.Day.ToString().PadLeft(2, '0');
            string str2 = data.Month.ToString().PadLeft(2, '0');
            string str3 = data.Year.ToString();
            switch (str3.Length)
            {
              case 1:
                str3 = "0" + str3;
                break;
              case 3:
                str3 = str3.Substring(1);
                break;
              case 4:
                str3 = str3.Substring(2);
                break;
            }
            stringBuilder.Append(str1 + "/" + str2 + "/" + str3 + " ");
            DateTime hora = bilhete2.Hora;
            string str4 = hora.Hour.ToString().PadLeft(2, '0');
            string str5 = hora.Minute.ToString().PadLeft(2, '0');
            string str6 = "00";
            stringBuilder.Append(str4 + ":" + str5 + ":" + str6 + " ");
            stringBuilder.Append(bilhete2.Cartao.ToString().PadLeft(16, '0') + " ");
            string str7 = this._rep.NumTerminal.ToString();
            stringBuilder.Append(str7.PadLeft(3, '0') + " ");
            stringBuilder.Append(bilhete2.Pis.PadLeft(11, '0') + " ");
            string str8 = stringBuilder.ToString();
            arquivoBilhete.ProcessarArquivoBilhete(str8.Trim());
          }
          else
            ++this._totBilhetesCarimboInvalido;
        }
      }
      bilhete1.AtualizarBilhetesRepProcessados(CommandType.Text);
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e) => throw new NotImplementedException();

    public override void IniciarProcesso() => throw new NotImplementedException();

    public override void TratarTimeoutAck() => throw new NotImplementedException();

    public override void TratarNenhumaResposta() => throw new NotImplementedException();

    private new enum Estados
    {
      estSolicitoBilhete,
    }
  }
}
