// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorRelogioRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using TopData.Framework.Comunicacao;
using TopData.GerenciadorRep.Bi.Dominio.Properties;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorRelogioRepPlus : TarefaAbstrata
  {
    private int _incrementoHoras;
    private DateTime dataHoraSenior;
    private bool chamadaSenior;
    private RepBase _rep;
    private GerenciadorRelogioRepPlus.Estados _estadoRep;
    public static GerenciadorRelogioRepPlus _gerenciadorRelogioRepPlus;

    public static GerenciadorRelogioRepPlus getInstance() => GerenciadorRelogioRepPlus._gerenciadorRelogioRepPlus != null ? GerenciadorRelogioRepPlus._gerenciadorRelogioRepPlus : new GerenciadorRelogioRepPlus();

    public static GerenciadorRelogioRepPlus getInstance(RepBase rep) => GerenciadorRelogioRepPlus._gerenciadorRelogioRepPlus != null ? GerenciadorRelogioRepPlus._gerenciadorRelogioRepPlus : new GerenciadorRelogioRepPlus(rep);

    public static GerenciadorRelogioRepPlus getInstance(
      RepBase rep,
      DateTime dataHoraSenior,
      bool chamadaSenior)
    {
      return GerenciadorRelogioRepPlus._gerenciadorRelogioRepPlus != null ? GerenciadorRelogioRepPlus._gerenciadorRelogioRepPlus : new GerenciadorRelogioRepPlus(rep, dataHoraSenior, chamadaSenior);
    }

    public static GerenciadorRelogioRepPlus getInstance(
      RepBase rep,
      int incrementoHora)
    {
      return GerenciadorRelogioRepPlus._gerenciadorRelogioRepPlus != null ? GerenciadorRelogioRepPlus._gerenciadorRelogioRepPlus : new GerenciadorRelogioRepPlus(rep, incrementoHora);
    }

    public GerenciadorRelogioRepPlus()
    {
    }

    public GerenciadorRelogioRepPlus(RepBase rep, DateTime dataHoraSenior, bool chamadaSenior)
    {
      this._rep = rep;
      this.dataHoraSenior = dataHoraSenior;
      this.chamadaSenior = chamadaSenior;
    }

    public GerenciadorRelogioRepPlus(RepBase rep) => this._rep = rep;

    public GerenciadorRelogioRepPlus(RepBase rep, int incrementoHoras)
    {
      this._rep = rep;
      this._incrementoHoras = incrementoHoras;
    }

    ~GerenciadorRelogioRepPlus()
    {
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarRelogio()
    {
      try
      {
        this._estadoRep = GerenciadorRelogioRepPlus.Estados.estEnvioRelogio;
        DateTime now = DateTime.Now;
        DateTime dateTime = this.chamadaSenior ? this.dataHoraSenior : DateTime.Now.AddHours((double) this._incrementoHoras);
        Envelope envelope = new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0);
        MsgTcpAplicacaoRelogioRepPlus aplicacaoRelogioRepPlus = new MsgTcpAplicacaoRelogioRepPlus();
        aplicacaoRelogioRepPlus.Dia = Convert.ToByte(dateTime.Day);
        aplicacaoRelogioRepPlus.Mes = Convert.ToByte(dateTime.Month);
        dateTime.Year.ToString();
        aplicacaoRelogioRepPlus.Ano = (byte) (DateTime.Now.Year % 100);
        aplicacaoRelogioRepPlus.Hora = Convert.ToByte(dateTime.Hour);
        aplicacaoRelogioRepPlus.Minuto = Convert.ToByte(dateTime.Minute);
        aplicacaoRelogioRepPlus.Segundo = Convert.ToByte(dateTime.Second);
        envelope.MsgAplicacao = (MsgTcpAplicacaoBase) aplicacaoRelogioRepPlus;
        this.ClienteSocket.Enviar(envelope, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    public override void TratarEnvelope(Envelope envelope)
    {
      if (this._estadoRep != GerenciadorRelogioRepPlus.Estados.estEnvioRelogio || envelope.Grp != (byte) 10 || envelope.Cmd != (byte) 102)
        return;
      if (envelope.MsgAplicacaoEmBytes[2] == (byte) 0)
      {
        this.NotificarParaUsuario(Resources.msgRELOGIO_ATUALIZADO, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
        this.EncerrarConexao();
      }
      else
      {
        this.EncerrarConexao();
        this.NotificarParaUsuario(envelope.MsgAplicacaoEmBytes[2] != (byte) 1 ? "Erro de execução" : "Erro de parâmetro", EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
      }
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
        this.EnviarRelogio();
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorRelogioRepPlus.Estados.estEnvioRelogio)
        menssagem = Resources.msgTIMEOUT_ENVIO_RELOGIO;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      if (this._estadoRep == GerenciadorRelogioRepPlus.Estados.estEnvioRelogio)
        menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_RELOGIO;
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioRelogio,
    }
  }
}
