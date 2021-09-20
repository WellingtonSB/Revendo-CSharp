// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.GerenciadorConfigGeraisRepPlus
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using TopData.Framework.Comunicacao;
using TopData.GerenciadorRep.Bi.Dominio.Properties;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class GerenciadorConfigGeraisRepPlus : TarefaAbstrata
  {
    private const int NUM_USUARIO_POR_LISTA = 15;
    private MsgTcpAplicacaoConfigBio _msgConfigBio;
    private List<MsgTcpAplicacaoLstFaixaHorarios> _lstMsgLstFaixaHorario;
    private List<MsgTcpAplicacaoLstCartoes> _lstMsgLstCartoes;
    private List<MsgTcpAplicacaoLstUsuarios> _lstMsgLstUsuarios;
    private List<MsgTcpAplicacaoLstTipoUsuarios> _lstMsgLstTipoUsuarios;
    private RepBase _rep;
    private int _totRegUsuarios = int.MinValue;
    private bool _isBio;
    private GerenciadorConfigGeraisRepPlus.Estados _estadoRep;
    public static GerenciadorConfigGeraisRepPlus _gerenciadorConfigGeraisRepPlus;

    public static GerenciadorConfigGeraisRepPlus getInstance() => GerenciadorConfigGeraisRepPlus._gerenciadorConfigGeraisRepPlus != null ? GerenciadorConfigGeraisRepPlus._gerenciadorConfigGeraisRepPlus : new GerenciadorConfigGeraisRepPlus();

    public static GerenciadorConfigGeraisRepPlus getInstance(
      RepBase rep)
    {
      return GerenciadorConfigGeraisRepPlus._gerenciadorConfigGeraisRepPlus != null ? GerenciadorConfigGeraisRepPlus._gerenciadorConfigGeraisRepPlus : new GerenciadorConfigGeraisRepPlus(rep);
    }

    public GerenciadorConfigGeraisRepPlus()
    {
    }

    public GerenciadorConfigGeraisRepPlus(RepBase rep)
    {
      this._rep = rep;
      this._isBio = this._rep is RepBio;
      this.InicializarClasses();
    }

    ~GerenciadorConfigGeraisRepPlus()
    {
    }

    private void InicializarClasses()
    {
      this._msgConfigBio = new MsgTcpAplicacaoConfigBio();
      this._lstMsgLstCartoes = new List<MsgTcpAplicacaoLstCartoes>();
      this._lstMsgLstUsuarios = new List<MsgTcpAplicacaoLstUsuarios>();
      this._lstMsgLstTipoUsuarios = new List<MsgTcpAplicacaoLstTipoUsuarios>();
      this._lstMsgLstFaixaHorario = new List<MsgTcpAplicacaoLstFaixaHorarios>();
    }

    public override void IniciarProcesso() => this.Conectar(this._rep);

    private void EnviarLstCartoes(int index)
    {
      try
      {
        this._estadoRep = GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstCartoes;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) this._lstMsgLstCartoes[index]
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarLstUsuarios(int index)
    {
      try
      {
        this._estadoRep = GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstUsuarios;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) this._lstMsgLstUsuarios[index]
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarLstTipoUsuarios(int index)
    {
      try
      {
        this._estadoRep = GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstTipoUsuarios;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) this._lstMsgLstTipoUsuarios[index]
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarLstFaixaHorarios(int index)
    {
      try
      {
        this._estadoRep = GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstFaixaHorario;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) this._lstMsgLstFaixaHorario[index]
        }, true);
      }
      catch (Exception ex)
      {
        this.EncerrarConexao();
        throw;
      }
    }

    private void EnviarConfigBio()
    {
      try
      {
        this._estadoRep = GerenciadorConfigGeraisRepPlus.Estados.estEnvioConfigBio;
        this.ClienteSocket.Enviar(new Envelope(this.ClienteSocket.NsuSw, (byte) 0, this.ClienteSocket.NsuRx, this.ClienteSocket.NumTerminal, (ushort) 0)
        {
          MsgAplicacao = (MsgTcpAplicacaoBase) this._msgConfigBio
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
      switch (this._estadoRep)
      {
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioConfigGeral:
          if (envelope.Grp != (byte) 3 || envelope.Cmd != (byte) 0 || envelope.MsgAplicacaoEmBytes[2] != (byte) 1)
            break;
          this.EnviarLstCartoes(0);
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstCartoes:
          if (envelope.Grp != (byte) 4 || envelope.Cmd != (byte) 0 || envelope.MsgAplicacaoEmBytes[2] != (byte) 1)
            break;
          if (this._lstMsgLstUsuarios.Count > 0)
          {
            this.EnviarLstUsuarios(0);
            break;
          }
          this.EnviarLstTipoUsuarios(0);
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstUsuarios:
          if (envelope.Grp != (byte) 4 || envelope.Cmd != (byte) 0 || envelope.MsgAplicacaoEmBytes[2] != (byte) 2)
            break;
          byte[] numArray1 = new byte[2]
          {
            envelope.MsgAplicacaoEmBytes[5],
            (byte) 0
          };
          numArray1[0] = envelope.MsgAplicacaoEmBytes[6];
          ushort uint16 = BitConverter.ToUInt16(numArray1, 0);
          if (uint16 > (ushort) 0 && (int) uint16 < this._totRegUsuarios)
          {
            this.EnviarLstUsuarios((int) uint16 / 15);
            break;
          }
          this.EnviarLstTipoUsuarios(0);
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstTipoUsuarios:
          if (envelope.Grp != (byte) 4 || envelope.Cmd != (byte) 0 || envelope.MsgAplicacaoEmBytes[2] != (byte) 3)
            break;
          this.EnviarLstFaixaHorarios(0);
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstFaixaHorario:
          if (envelope.Grp != (byte) 4 || envelope.Cmd != (byte) 0 || envelope.MsgAplicacaoEmBytes[2] != (byte) 4)
            break;
          byte[] numArray2 = new byte[2]
          {
            envelope.MsgAplicacaoEmBytes[5],
            (byte) 0
          };
          numArray2[0] = envelope.MsgAplicacaoEmBytes[6];
          if ((int) BitConverter.ToUInt16(numArray2, 0) > (int) this._lstMsgLstFaixaHorario[0].RegTotal)
          {
            if (this._isBio)
            {
              this.EnviarConfigBio();
              break;
            }
            this.NotificarParaUsuario(Resources.msgCONFIGURACOES_ATUALIZADAS, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            break;
          }
          this.EnviarLstFaixaHorarios(1);
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioConfigBio:
          if (envelope.Grp != (byte) 12 || envelope.Cmd != (byte) 101)
            break;
          if (envelope.MsgAplicacaoEmBytes[2] == (byte) 1)
          {
            this.NotificarParaUsuario(Resources.msgCONFIGURACOES_ATUALIZADAS, EnumEstadoNotificacaoParaUsuario.comSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComSucesso, this._rep.RepId, this._rep.Local);
            break;
          }
          string menssagem = this.ExtrairRespostaBio(envelope.MsgAplicacaoEmBytes[2]);
          this.EncerrarConexao();
          this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
          break;
      }
    }

    private string ExtrairRespostaBio(byte respostaBio)
    {
      string str = "";
      switch (respostaBio)
      {
        case 0:
          str = Resources.msgBIO_ERRO_COMUNICACAO;
          break;
        case 2:
          str = Resources.msgBIO_FALHA_COMANDO;
          break;
        case 3:
          str = Resources.msgBIO_NAO_MODO_MASTER;
          break;
        case 4:
          str = Resources.msgBIO_USUARIO_JA_CADASTRADO;
          break;
        case 5:
          str = Resources.msgBIO_USUARIO_NAO_CADASTRADO;
          break;
        case 6:
          str = Resources.msgBIO_BASE_CHEIA;
          break;
        case 7:
          str = Resources.msgBIO_TIME_OUT_COMUNICACAO;
          break;
        case 9:
          str = Resources.msgBIO_PARAMETRO_INVALIDO;
          break;
        case 22:
          str = Resources.msgBIO_TEMPLATE_INVALIDO;
          break;
      }
      return str;
    }

    private Faixa InicializarFaixaHorarioSabado(Faixa faixaHorarios)
    {
      faixaHorarios.HoraIniSabado = byte.MaxValue;
      faixaHorarios.MinutoIniSabado = byte.MaxValue;
      faixaHorarios.HoraFimSabado = byte.MaxValue;
      faixaHorarios.MinutoFimSabado = byte.MaxValue;
      return faixaHorarios;
    }

    private Faixa InicializarFaixaHorarioSexta(Faixa faixaHorarios)
    {
      faixaHorarios.HoraIniSexta = byte.MaxValue;
      faixaHorarios.MinutoIniSexta = byte.MaxValue;
      faixaHorarios.HoraFimSexta = byte.MaxValue;
      faixaHorarios.MinutoFimSexta = byte.MaxValue;
      return faixaHorarios;
    }

    private Faixa InicializarFaixaHorarioQuinta(Faixa faixaHorarios)
    {
      faixaHorarios.HoraIniQuinta = byte.MaxValue;
      faixaHorarios.MinutoIniQuinta = byte.MaxValue;
      faixaHorarios.HoraFimQuinta = byte.MaxValue;
      faixaHorarios.MinutoFimQuinta = byte.MaxValue;
      return faixaHorarios;
    }

    private Faixa InicializarFaixaHorarioQuarta(Faixa faixaHorarios)
    {
      faixaHorarios.HoraIniQuarta = byte.MaxValue;
      faixaHorarios.MinutoIniQuarta = byte.MaxValue;
      faixaHorarios.HoraFimQuarta = byte.MaxValue;
      faixaHorarios.MinutoFimQuarta = byte.MaxValue;
      return faixaHorarios;
    }

    private Faixa InicializarFaixaHorarioTerca(Faixa faixaHorarios)
    {
      faixaHorarios.HoraIniTerca = byte.MaxValue;
      faixaHorarios.MinutoIniTerca = byte.MaxValue;
      faixaHorarios.HoraFimTerca = byte.MaxValue;
      faixaHorarios.MinutoFimTerca = byte.MaxValue;
      return faixaHorarios;
    }

    private Faixa InicializarFaixaHorarioSegunda(Faixa faixaHorarios)
    {
      faixaHorarios.HoraIniSegunda = byte.MaxValue;
      faixaHorarios.MinutoIniSegunda = byte.MaxValue;
      faixaHorarios.HoraFimSegunda = byte.MaxValue;
      faixaHorarios.MinutoFimSegunda = byte.MaxValue;
      return faixaHorarios;
    }

    private Faixa InicializarFaixaHorarioDomingo(Faixa faixaHorarios)
    {
      faixaHorarios.HoraIniDomingo = byte.MaxValue;
      faixaHorarios.MinutoIniDomingo = byte.MaxValue;
      faixaHorarios.HoraFimDomingo = byte.MaxValue;
      faixaHorarios.MinutoFimDomingo = byte.MaxValue;
      return faixaHorarios;
    }

    private Faixa InicializarFaixaHorarioFeriado(Faixa faixaHorarios)
    {
      faixaHorarios.HoraFimFeriado = byte.MaxValue;
      faixaHorarios.HoraIniFeriado = byte.MaxValue;
      faixaHorarios.MinutoFimFeriado = byte.MaxValue;
      faixaHorarios.HoraIniFeriado = byte.MaxValue;
      return faixaHorarios;
    }

    private void CarregarDadosConfigBio()
    {
      RepBio repBio = new RepBio().RecuperaAjusteBiometricoPorRep(this._rep.RepId);
      this._msgConfigBio.Leitor = (byte) 0;
      this._msgConfigBio.Capt = (byte) repBio.BioCaptura;
      this._msgConfigBio.Filtro = (byte) repBio.BioFiltro;
      this._msgConfigBio.SegIden = (byte) repBio.BioIdent;
      this._msgConfigBio.SegVerif = (byte) repBio.BioVerif;
    }

    public override void TarefaAbstrata_OnNotificarConexao(NotificarConexaoEventArgs e)
    {
      if (e.EstadoNotificacao == EnumEstadoNotificacaoParaUsuario.comSucesso)
      {
        this.CarregarDadosConfigBio();
        this.EnviarConfigBio();
      }
      else
        this.EncerrarConexao();
    }

    public override void TratarTimeoutAck()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioConfigGeral:
          menssagem = Resources.msgTIMEOUT_ENVIO_CONFIG_GERAL;
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstCartoes:
          menssagem = Resources.msgTIMEOUT_ENVIO_LST_CARTOES;
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstUsuarios:
          menssagem = Resources.msgTIMEOUT_ENVIO_LST_USUARIOS;
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstTipoUsuarios:
          menssagem = Resources.msgTIMEOUT_ENVIO_LST_TIPO_USUARIOS;
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstFaixaHorario:
          menssagem = Resources.msgTIMEOUT_ENVIO_LST_FAIXA_HORARIO;
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioConfigBio:
          menssagem = Resources.msgTIMEOUT_ENVIO_CONFIG_BIO;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    public override void TratarNenhumaResposta()
    {
      string menssagem = string.Empty;
      switch (this._estadoRep)
      {
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioConfigGeral:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_CONFIG_GERAL;
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstCartoes:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_LST_CARTOES;
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstUsuarios:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_LST_USUARIOS;
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstTipoUsuarios:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_LST_TIPO_USUARIO;
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioLstFaixaHorario:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_LST_FAIXA_HORARIO;
          break;
        case GerenciadorConfigGeraisRepPlus.Estados.estEnvioConfigBio:
          menssagem = Resources.msgNENHUMA_RESPOSTA_APOS_ENVIO_CONFIG_BIO;
          break;
      }
      this.EncerrarConexao();
      this.NotificarParaUsuario(menssagem, EnumEstadoNotificacaoParaUsuario.semSucesso, EnumEstadoResultadoFinalProcesso.finalizadoComFalha, this._rep.RepId, this._rep.Local);
    }

    private new enum Estados
    {
      estEnvioConfigGeral,
      estEnvioLstCartoes,
      estEnvioLstUsuarios,
      estEnvioLstTipoUsuarios,
      estEnvioLstFaixaHorario,
      estEnvioConfigBio,
    }
  }
}
