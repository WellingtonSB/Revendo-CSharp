// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.TarefasBioAbstract
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Ferramentas;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public abstract class TarefasBioAbstract : TarefaAbstrata
  {
    public abstract void CarregaListaSdkProcessada(
      ref List<UsuarioBio> listaBio,
      ProcessoTemplates tipoProcessoTemplates);

    public abstract void CarregaListaSdkParaProcessar(
      ProcessoTemplates tipoProcessoTemplates,
      List<UsuarioBio> listaBio);

    public abstract void CarregarMsgSolicitExclusaoUsuarioBio();

    public abstract void CarregarMsgSolicitInclusaoUsuarioBio();

    public abstract void CarregarMsgSolicitUsuarioBio();

    public abstract void IniciarProcesso(ProcessoTemplates processoTemp);

    public SortableBindingList<UsuarioBio> ListaUsuariosBioNoDb { get; set; }

    public SortableBindingList<UsuarioBio> ListaUsuariosBio { get; set; }

    public int TotTemplatesParaProcessar { get; set; }

    public int QtdBioModulo { get; set; }

    public bool semBio { get; set; }

    public event EventHandler<NotificarProgressBarEventArgs> OnNotificarProgressBar;

    public event EventHandler<NotificarModeloBIOEventArgs> OnNotificarModeloBIO;

    public event EventHandler<NotificarTipoPlacaFIMEventArgs> OnNotificarTipoPlacaFIM;

    public event EventHandler<NotificarVersaoFWEventArgs> OnNotificarVersaoFW;

    public void RaiseProgressBar(NotificarProgressBarEventArgs eNotificaProgress)
    {
      eNotificaProgress = new NotificarProgressBarEventArgs(this.TotTemplatesParaProcessar);
      if (this.OnNotificarProgressBar == null)
        return;
      this.OnNotificarProgressBar((object) this, eNotificaProgress);
    }

    public void RaiseNotificarModeloBio(NotificarModeloBIOEventArgs eNotifica)
    {
      if (this.OnNotificarModeloBIO == null)
        return;
      this.OnNotificarModeloBIO((object) this, eNotifica);
    }

    public void RaiseNotificarTipoPlacaFim(NotificarTipoPlacaFIMEventArgs eNotificaFIM)
    {
      if (this.OnNotificarTipoPlacaFIM == null)
        return;
      this.OnNotificarTipoPlacaFIM((object) this, eNotificaFIM);
    }
  }
}
