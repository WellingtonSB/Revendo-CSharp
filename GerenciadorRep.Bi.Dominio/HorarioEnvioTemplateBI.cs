﻿// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.HorarioEnvioTemplateBI
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System.Collections.Generic;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class HorarioEnvioTemplateBI
  {
    private HorarioEnvioTemplateDAO restricoesBase = new HorarioEnvioTemplateDAO();

    public List<HorarioEnvioTemplate> Exibir() => this.restricoesBase.Listar();

    public int Inserir(HorarioEnvioTemplate restricao) => this.restricoesBase.Inserir(restricao);

    public int Atualizar(HorarioEnvioTemplate restricao) => this.restricoesBase.Atualizar(restricao);

    public int Remover(int codigo) => this.restricoesBase.Remove(codigo);
  }
}
