// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.NotificarExclusaoEmpregadosEventArgs
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using TopData.GerenciadorRep.Bi.Entidade;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class NotificarExclusaoEmpregadosEventArgs : EventArgs
  {
    private ResultadoAtualizacaoEmpregados _resultado = new ResultadoAtualizacaoEmpregados();
    private List<ResultadoAtualizacaoEmpregados> _lstResultado = new List<ResultadoAtualizacaoEmpregados>();

    public NotificarExclusaoEmpregadosEventArgs(byte statusProcesso, string pis)
    {
      switch (statusProcesso)
      {
        case 0:
          this._resultado.MsgInclusao = "Usuário excluído";
          break;
        case 1:
          this._resultado.MsgInclusao = "Usuário não existe";
          break;
        case 2:
          this._resultado.MsgInclusao = "Não realizou a operação";
          break;
        case 200:
          this._resultado.MsgInclusao = "Cpf não cadastrado";
          break;
      }
      this._resultado.Pis = pis;
      this._resultado.Retorno = (int) statusProcesso;
    }

    public NotificarExclusaoEmpregadosEventArgs(
      List<ResultadoAtualizacaoEmpregados> resultadoProcesso)
    {
      this._lstResultado = resultadoProcesso;
    }

    public ResultadoAtualizacaoEmpregados Resultado => this._resultado;

    public List<ResultadoAtualizacaoEmpregados> ListResultado => this._lstResultado;
  }
}
