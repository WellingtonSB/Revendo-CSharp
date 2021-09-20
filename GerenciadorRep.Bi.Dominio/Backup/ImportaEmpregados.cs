// Decompiled with JetBrains decompiler
// Type: TopData.GerenciadorRep.Bi.Dominio.ImportaEmpregados
// Assembly: GerenciadorRep.Bi.Dominio, Version=4.4.1.19250, Culture=neutral, PublicKeyToken=null
// MVID: A8F921E6-2C85-4D10-9A12-A6FE85FD787E
// Assembly location: C:\Program Files (x86)\Topdata\SDK Inner Rep\DLLs\GerenciadorRep.Bi.Dominio.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TopData.Framework.Core;
using TopData.GerenciadorRep.Bi.Entidade;
using TopData.GerenciadorRep.Da;

namespace TopData.GerenciadorRep.Bi.Dominio
{
  public class ImportaEmpregados
  {
    private List<Empregado> _lstEmpregadosBase;

    public event EventHandler<NotificarProgressBarEventArgs> OnNotificarProgressBar;

    public RespostaImportacao ImportaArquivoEmpregados(
      string arquivoParaImportar,
      char separador,
      int empregadorId,
      bool formatoTopPontoWeb)
    {
      RespostaImportacao respostaImportacao = new RespostaImportacao();
      RespostaValidacao respostaValidacao1 = new RespostaValidacao();
      List<Empregado> listaEmpregados1 = new List<Empregado>();
      List<Empregado> listaEmpregados2 = new List<Empregado>();
      List<Empregado> listaEmpregados3 = new List<Empregado>();
      int num1 = 0;
      int num2 = 0;
      if (!File.Exists(arquivoParaImportar))
      {
        respostaImportacao.Status = TipoResposta.ARQUIVO_NAO_ENCONTRADO;
        return respostaImportacao;
      }
      try
      {
        string strOut = string.Empty;
        this._lstEmpregadosBase = new Empregado().PesquisarListaEmpregadosPorEmpregador(empregadorId);
        num1 = 0;
        foreach (string str in !formatoTopPontoWeb ? File.ReadAllLines(arquivoParaImportar, Encoding.UTF7) : File.ReadAllLines(arquivoParaImportar, Encoding.UTF8))
        {
          if (!str.Equals(""))
          {
            ++num1;
            string[] strArray = str.Split(separador);
            Empregado empregadoLista = new Empregado();
            empregadoLista.PersonId = string.Empty;
            empregadoLista.Nome = strArray[0].Trim().ToUpper();
            if (Ferramentas.RemoveCaracteresEspeciais(empregadoLista.Nome, out strOut))
              empregadoLista.Nome = strOut;
            empregadoLista.Pis = strArray[2].Trim().PadLeft(12, '0');
            empregadoLista.Cartao = (ulong) Convert.ToInt64(strArray[3].Trim());
            if (RegistrySingleton.GetInstance().ArquivoCustomizado == 1)
              empregadoLista.Matricula = empregadoLista.Cartao.ToString();
            empregadoLista.CartaoBarras = empregadoLista.Cartao;
            empregadoLista.CartaoProx = empregadoLista.Cartao;
            empregadoLista.Teclado = empregadoLista.Cartao;
            int num3 = 0;
            bool repPlus = false;
            bool cartaoAntigo = false;
            if (!strArray[4].Equals("S") && !strArray[4].Equals("N") && !strArray[4].Equals(""))
            {
              if (strArray.Length == 8)
              {
                empregadoLista.CartaoBarras = (ulong) Convert.ToInt64(strArray[3].Trim());
                empregadoLista.CartaoProx = (ulong) Convert.ToInt64(strArray[4].Trim());
                empregadoLista.Teclado = (ulong) Convert.ToInt64(strArray[5].Trim());
                empregadoLista.Cartao = empregadoLista.CartaoProx;
                num3 = 2;
              }
              else if (strArray.Length == 9)
              {
                if (strArray[8].Equals("E"))
                {
                  empregadoLista.CartaoBarras = (ulong) Convert.ToInt64(strArray[3].Trim());
                  empregadoLista.CartaoProx = (ulong) Convert.ToInt64(strArray[4].Trim());
                  empregadoLista.Teclado = (ulong) Convert.ToInt64(strArray[5].Trim());
                  empregadoLista.Cartao = empregadoLista.CartaoProx;
                  num3 = 2;
                }
                else
                {
                  empregadoLista.CartaoBarras = (ulong) Convert.ToInt64(strArray[4].Trim());
                  empregadoLista.CartaoProx = (ulong) Convert.ToInt64(strArray[5].Trim());
                  empregadoLista.Teclado = (ulong) Convert.ToInt64(strArray[6].Trim());
                  num3 = 3;
                }
                cartaoAntigo = true;
              }
              else
              {
                empregadoLista.CartaoBarras = (ulong) Convert.ToInt64(strArray[4].Trim());
                empregadoLista.CartaoProx = (ulong) Convert.ToInt64(strArray[5].Trim());
                empregadoLista.Teclado = (ulong) Convert.ToInt64(strArray[6].Trim());
                num3 = 3;
              }
              repPlus = true;
            }
            if (RegistrySingleton.GetInstance().FORMATO_WIEGAND_DEC)
              this.ConvertToWiegandFC(empregadoLista);
            empregadoLista.NomeExibicao = strArray[1].Trim().ToUpper();
            if (Ferramentas.RemoveCaracteresEspeciais(empregadoLista.NomeExibicao, out strOut))
              empregadoLista.NomeExibicao = strOut;
            if (strArray[4 + num3].Trim().ToUpper() == "S" || strArray[4 + num3].Trim().ToUpper() == "SIM")
            {
              empregadoLista.VerificaBiometria = true;
              empregadoLista.DuplaVerificacao = false;
            }
            else if (strArray[4 + num3].Trim().ToUpper() == "D")
            {
              empregadoLista.VerificaBiometria = false;
              empregadoLista.DuplaVerificacao = true;
            }
            else
            {
              empregadoLista.VerificaBiometria = false;
              empregadoLista.DuplaVerificacao = false;
            }
            Empregado empregado1 = new Empregado();
            Empregado empregado2 = new Empregado();
            empregadoLista.EmpregadorId = empregadorId;
            Empregado empregado3 = empregado1.PesquisarEmpregadosPorPis(empregadoLista);
            empregadoLista.Senha = empregado3.Senha != null ? (!empregado3.Senha.Equals("") ? (empregadoLista.VerificaBiometria || empregadoLista.DuplaVerificacao ? "" : empregado3.Senha) : strArray[5 + num3].Trim()) : strArray[5 + num3].Trim();
            if (empregadoLista.Cartao == 0UL)
              empregadoLista.Cartao = Convert.ToUInt64(empregadoLista.Pis);
            if (empregadoLista.CartaoBarras == 0UL)
              empregadoLista.CartaoBarras = Convert.ToUInt64(empregadoLista.Pis);
            if (empregadoLista.CartaoProx == 0UL)
              empregadoLista.CartaoProx = Convert.ToUInt64(empregadoLista.Pis);
            empregadoLista.EmpregadorId = empregadorId;
            RespostaValidacao respostaValidacao2 = this.ValidaDadosLinhaImportacao(empregadoLista, repPlus);
            if (!respostaValidacao2.Validado)
            {
              respostaImportacao.Status = TipoResposta.ERRO_EM_UM_CAMPO;
              respostaImportacao.LinhaComErro = num1;
              respostaImportacao.CampoComErro = respostaValidacao2.CampoComErro;
              return respostaImportacao;
            }
            int index1 = listaEmpregados1.FindIndex((Predicate<Empregado>) (empregadoRepetido => empregadoRepetido.Pis == empregadoLista.Pis));
            if (index1 >= 0)
            {
              respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NO_ARQUIVO;
              respostaImportacao.LinhaComErro = num1;
              respostaImportacao.LinhaRepetida = index1 + 1;
              respostaImportacao.CampoComErro = TipoCampoLinha.PIS;
              return respostaImportacao;
            }
            if (repPlus)
            {
              int index2 = listaEmpregados1.FindIndex((Predicate<Empregado>) (empregadoRepetido => (long) empregadoRepetido.CartaoBarras == (long) empregadoLista.CartaoBarras && empregadoLista.CartaoBarras != 0UL));
              if (index2 >= 0)
              {
                respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NO_ARQUIVO;
                respostaImportacao.LinhaComErro = num1;
                respostaImportacao.LinhaRepetida = index2 + 1;
                respostaImportacao.CampoComErro = TipoCampoLinha.CARTAO;
                return respostaImportacao;
              }
              int index3 = listaEmpregados1.FindIndex((Predicate<Empregado>) (empregadoRepetido => (long) empregadoRepetido.CartaoProx == (long) empregadoLista.CartaoProx && empregadoLista.CartaoProx != 0UL));
              if (index3 >= 0)
              {
                respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NO_ARQUIVO;
                respostaImportacao.LinhaComErro = num1;
                respostaImportacao.LinhaRepetida = index3 + 1;
                respostaImportacao.CampoComErro = TipoCampoLinha.CARTAOPROX;
                return respostaImportacao;
              }
              int index4 = listaEmpregados1.FindIndex((Predicate<Empregado>) (empregadoRepetido => (long) empregadoRepetido.Teclado == (long) empregadoLista.Teclado && empregadoLista.Teclado != 0UL));
              if (index4 >= 0)
              {
                respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NO_ARQUIVO;
                respostaImportacao.LinhaComErro = num1;
                respostaImportacao.LinhaRepetida = index4 + 1;
                respostaImportacao.CampoComErro = TipoCampoLinha.TECLADO;
                return respostaImportacao;
              }
            }
            else
            {
              int index5 = listaEmpregados1.FindIndex((Predicate<Empregado>) (empregadoRepetido => (long) empregadoRepetido.Cartao == (long) empregadoLista.Cartao));
              if (index5 >= 0)
              {
                respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NO_ARQUIVO;
                respostaImportacao.LinhaComErro = num1;
                respostaImportacao.LinhaRepetida = index5 + 1;
                respostaImportacao.CampoComErro = TipoCampoLinha.CARTAO;
                return respostaImportacao;
              }
            }
            empregadoLista.AcaoImportacao = 0;
            RespostaValidacao respostaValidacao3 = this.TestaValoresRepetidosNaBase(empregadoLista, cartaoAntigo);
            if (!respostaValidacao3.Validado)
            {
              if (!repPlus)
              {
                if (this.ExisteCartao(empregadoLista) && respostaValidacao3.CampoComErro == TipoCampoLinha.CARTAO)
                {
                  respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NA_BASE;
                  respostaImportacao.LinhaComErro = num1;
                  respostaImportacao.CampoComErro = respostaValidacao3.CampoComErro;
                  return respostaImportacao;
                }
                if (strArray.Length > 6)
                {
                  if (strArray[6].Trim().ToUpper() == "E")
                    empregadoLista.AcaoImportacao = 2;
                  else if (this.VerificaSeAtualizaEmpregadosNaBase(empregadoLista))
                  {
                    if (!this.ExisteCartoesEdicao(empregadoLista))
                    {
                      empregadoLista.AcaoImportacao = 1;
                    }
                    else
                    {
                      respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NA_BASE;
                      respostaImportacao.LinhaComErro = num1;
                      respostaImportacao.CampoComErro = TipoCampoLinha.CARTAO;
                      return respostaImportacao;
                    }
                  }
                  else
                    empregadoLista.AcaoImportacao = 3;
                }
                else if (this.VerificaSeAtualizaEmpregadosNaBase(empregadoLista))
                {
                  if (!this.ExisteCartoesEdicao(empregadoLista))
                  {
                    empregadoLista.AcaoImportacao = 1;
                  }
                  else
                  {
                    respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NA_BASE;
                    respostaImportacao.LinhaComErro = num1;
                    respostaImportacao.CampoComErro = TipoCampoLinha.CARTAO;
                    return respostaImportacao;
                  }
                }
                else
                  empregadoLista.AcaoImportacao = 3;
              }
              else
              {
                if (this.ExisteCartao(empregadoLista) && respostaValidacao3.CampoComErro == TipoCampoLinha.CARTAO)
                {
                  respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NA_BASE;
                  respostaImportacao.LinhaComErro = num1;
                  respostaImportacao.CampoComErro = respostaValidacao3.CampoComErro;
                  return respostaImportacao;
                }
                if (this.ExisteCartaoBarras(empregadoLista) && respostaValidacao3.CampoComErro == (TipoCampoLinha) (3 + num3))
                {
                  respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NA_BASE;
                  respostaImportacao.LinhaComErro = num1;
                  respostaImportacao.CampoComErro = respostaValidacao3.CampoComErro;
                  return respostaImportacao;
                }
                if (this.ExisteCartaoProx(empregadoLista) && respostaValidacao3.CampoComErro == (TipoCampoLinha) (3 + num3))
                {
                  respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NA_BASE;
                  respostaImportacao.LinhaComErro = num1;
                  respostaImportacao.CampoComErro = respostaValidacao3.CampoComErro;
                  return respostaImportacao;
                }
                if (this.ExisteTeclado(empregadoLista) && respostaValidacao3.CampoComErro == (TipoCampoLinha) (3 + num3))
                {
                  respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NA_BASE;
                  respostaImportacao.LinhaComErro = num1;
                  respostaImportacao.CampoComErro = respostaValidacao3.CampoComErro;
                  return respostaImportacao;
                }
                if (num3 == 2)
                {
                  if (strArray.Length > 8)
                  {
                    if (strArray[6 + num3].Trim().ToUpper() == "E")
                      empregadoLista.AcaoImportacao = 2;
                    else if (this.VerificaSeAtualizaEmpregadosNaBaseRepPlus(empregadoLista))
                    {
                      if (!this.ExisteCartoesEdicao(empregadoLista))
                      {
                        empregadoLista.AcaoImportacao = 1;
                      }
                      else
                      {
                        respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NA_BASE;
                        respostaImportacao.LinhaComErro = num1;
                        respostaImportacao.CampoComErro = TipoCampoLinha.CARTAO;
                        return respostaImportacao;
                      }
                    }
                    else
                      empregadoLista.AcaoImportacao = 3;
                  }
                  else if (this.VerificaSeAtualizaEmpregadosNaBaseRepPlus(empregadoLista))
                  {
                    if (!this.ExisteCartoesEdicao(empregadoLista))
                    {
                      empregadoLista.AcaoImportacao = 1;
                    }
                    else
                    {
                      respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NA_BASE;
                      respostaImportacao.LinhaComErro = num1;
                      respostaImportacao.CampoComErro = TipoCampoLinha.CARTAO;
                      return respostaImportacao;
                    }
                  }
                  else
                    empregadoLista.AcaoImportacao = 3;
                }
                else if (strArray.Length > 9)
                {
                  if (strArray[6 + num3].Trim().ToUpper() == "E")
                    empregadoLista.AcaoImportacao = 2;
                  else if (this.VerificaSeAtualizaEmpregadosNaBaseRepPlus(empregadoLista))
                  {
                    if (!this.ExisteCartoesEdicao(empregadoLista))
                    {
                      empregadoLista.AcaoImportacao = 1;
                    }
                    else
                    {
                      respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NA_BASE;
                      respostaImportacao.LinhaComErro = num1;
                      respostaImportacao.CampoComErro = TipoCampoLinha.CARTAO;
                      return respostaImportacao;
                    }
                  }
                  else
                    empregadoLista.AcaoImportacao = 3;
                }
                else if (this.VerificaSeAtualizaEmpregadosNaBaseRepPlus(empregadoLista))
                {
                  if (!this.ExisteCartoesEdicao(empregadoLista))
                  {
                    empregadoLista.AcaoImportacao = 1;
                  }
                  else
                  {
                    respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NA_BASE;
                    respostaImportacao.LinhaComErro = num1;
                    respostaImportacao.CampoComErro = TipoCampoLinha.CARTAO;
                    return respostaImportacao;
                  }
                }
                else
                  empregadoLista.AcaoImportacao = 3;
              }
              if (respostaValidacao3.CampoComErro == (TipoCampoLinha) (3 + num3))
              {
                respostaImportacao.Status = TipoResposta.VALOR_REPETIDO_NA_BASE;
                respostaImportacao.LinhaComErro = num1;
                respostaImportacao.CampoComErro = respostaValidacao3.CampoComErro;
                return respostaImportacao;
              }
            }
            else if (!repPlus)
            {
              if (strArray.Length > 6 && strArray[6].Trim().ToUpper() == "E")
                continue;
            }
            else if (num3 == 3)
            {
              if (strArray.Length > 9 && strArray[6 + num3].Trim().ToUpper() == "E")
                continue;
            }
            else if (strArray.Length > 8 && strArray[6 + num3].Trim().ToUpper() == "E")
              continue;
            if (Convert.ToInt64(empregadoLista.Pis) == 0L || empregadoLista.Pis.Trim().Equals(""))
              respostaImportacao.LstPisInvalidos.Add(str);
            else if (RegistrySingleton.GetInstance().VALIDA_PIS && !Ferramentas.ValidaPIS(empregadoLista.Pis.Substring(1, 11)) && Convert.ToInt64(empregadoLista.Pis).ToString().Length != 12)
            {
              respostaImportacao.LstPisInvalidos.Add(str);
            }
            else
            {
              if (empregadoLista.AcaoImportacao == 0)
              {
                empregadoLista.Processado = false;
                listaEmpregados1.Add(empregadoLista);
              }
              if (empregadoLista.AcaoImportacao == 1)
              {
                empregadoLista.Processado = false;
                listaEmpregados2.Add(empregadoLista);
              }
              if (empregadoLista.AcaoImportacao == 2)
                listaEmpregados3.Add(empregadoLista);
              if (empregadoLista.AcaoImportacao == 0)
                ++num2;
            }
          }
        }
        Empregado empregado = new Empregado();
        empregado.OnNotificarProgressBar += new EventHandler<NotificarProgressBarEventArgs>(this.empregado_OnNotificarProgressBar);
        respostaImportacao.NumeroEmpregadosImportados = empregado.InserirListaEmpregados(listaEmpregados1);
        respostaImportacao.NumeroEmpregadosAlterados = empregado.AlterarListaEmpregados(listaEmpregados2);
        respostaImportacao.NumeroEmpregadosExcluidos = empregado.ExcluirListaEmpregados(listaEmpregados3);
        respostaImportacao.Status = TipoResposta.SUCESSO;
        return respostaImportacao;
      }
      catch (Exception ex)
      {
        respostaImportacao.Status = TipoResposta.ERRO_DE_EXCECAO;
        respostaImportacao.LinhaComErro = num1;
        return respostaImportacao;
      }
    }

    private bool ExisteTeclado(Empregado empregadoLista)
    {
      try
      {
        if (new Empregado().VerificarSeExisteTecladoPorEmpregadorRepPlus(empregadoLista) > 0)
          return true;
      }
      catch
      {
        return true;
      }
      return false;
    }

    private bool ExisteCartaoProx(Empregado empregadoLista)
    {
      try
      {
        if (new Empregado().VerificarSeExisteCartaoProxPorEmpregadorRepPlus(empregadoLista) > 0)
          return true;
      }
      catch (AppTopdataException ex)
      {
        return true;
      }
      catch (Exception ex)
      {
        return true;
      }
      return false;
    }

    private bool ExisteCartaoBarras(Empregado empregadoLista)
    {
      try
      {
        if (new Empregado().VerificarSeExisteCartaoBarrasPorEmpregadorRepPlus(empregadoLista) > 0)
          return true;
      }
      catch (AppTopdataException ex)
      {
        return true;
      }
      catch (Exception ex)
      {
        return true;
      }
      return false;
    }

    private bool ExisteCartao(Empregado empregadoLista)
    {
      try
      {
        if (new Empregado().VerificarSeExisteCartaoPorEmpregador(empregadoLista) > 0)
          return true;
      }
      catch (AppTopdataException ex)
      {
        return true;
      }
      catch (Exception ex)
      {
        return true;
      }
      return false;
    }

    private bool ExisteCartoesEdicao(Empregado empregadoLista)
    {
      try
      {
        foreach (Empregado empregado in new Empregado().PesquisarListaEmpregadosPorEmpregador(empregadoLista.EmpregadorId))
        {
          if (!(empregado.Pis == empregadoLista.Pis) && ((long) empregado.Cartao == (long) empregadoLista.Cartao && empregado.Cartao != 0UL || (long) empregado.CartaoBarras == (long) empregadoLista.CartaoBarras && empregado.CartaoBarras != 0UL || (long) empregado.CartaoProx == (long) empregadoLista.CartaoProx && empregado.CartaoProx != 0UL))
            return true;
        }
        return false;
      }
      catch (AppTopdataException ex)
      {
        return true;
      }
      catch (Exception ex)
      {
        return true;
      }
    }

    private void empregado_OnNotificarProgressBar(object sender, NotificarProgressBarEventArgs e)
    {
      if (this.OnNotificarProgressBar == null)
        return;
      this.OnNotificarProgressBar((object) this.OnNotificarProgressBar, e);
    }

    private RespostaValidacao ValidaDadosLinhaImportacao(
      Empregado empregadoLista,
      bool repPlus)
    {
      ulong result1 = 0;
      int result2 = 0;
      long result3 = 0;
      RespostaValidacao respostaValidacao = new RespostaValidacao();
      respostaValidacao.Validado = false;
      if (empregadoLista.Nome.Length > 52 || empregadoLista.Nome == "")
      {
        respostaValidacao.CampoComErro = TipoCampoLinha.NOME;
        return respostaValidacao;
      }
      for (int startIndex = 0; startIndex < empregadoLista.Nome.Length; ++startIndex)
      {
        if (!ImportaEmpregados.ValidaCaracteresInvalidos(empregadoLista.Nome.Substring(startIndex, 1)))
        {
          respostaValidacao.CampoComErro = TipoCampoLinha.NOME;
          return respostaValidacao;
        }
      }
      if (empregadoLista.NomeExibicao.Length > 16)
      {
        respostaValidacao.CampoComErro = TipoCampoLinha.NOME_EXIBICAO;
        return respostaValidacao;
      }
      for (int startIndex = 0; startIndex < empregadoLista.NomeExibicao.Length; ++startIndex)
      {
        if (!ImportaEmpregados.ValidaCaracteresInvalidos(empregadoLista.NomeExibicao.Substring(startIndex, 1)))
        {
          respostaValidacao.CampoComErro = TipoCampoLinha.NOME_EXIBICAO;
          return respostaValidacao;
        }
      }
      bool flag1 = ulong.TryParse(empregadoLista.Pis, out result1);
      if (empregadoLista.Pis.Length > 12 || !flag1)
      {
        respostaValidacao.CampoComErro = TipoCampoLinha.PIS;
        return respostaValidacao;
      }
      bool flag2 = int.TryParse(empregadoLista.Senha, out result2);
      if (empregadoLista.Senha.Length > 4 || empregadoLista.Senha != "" && !flag2 || empregadoLista.Senha.Length > 0 && empregadoLista.Senha.Length < 4)
      {
        respostaValidacao.CampoComErro = TipoCampoLinha.TECLADO;
        return respostaValidacao;
      }
      if (empregadoLista.VerificaBiometria && empregadoLista.Senha.Length > 0)
      {
        respostaValidacao.CampoComErro = TipoCampoLinha.CARTAOPROX;
        return respostaValidacao;
      }
      bool flag3 = long.TryParse(empregadoLista.Cartao.ToString(), out result3);
      if (empregadoLista.Cartao.ToString().Length > 16 || empregadoLista.Cartao.ToString().Length < 1 || !flag3)
      {
        respostaValidacao.CampoComErro = TipoCampoLinha.CARTAO;
        return respostaValidacao;
      }
      if (repPlus)
      {
        bool flag4 = long.TryParse(empregadoLista.CartaoProx.ToString(), out result3);
        if (empregadoLista.CartaoProx.ToString().Length > 16 || empregadoLista.CartaoProx.ToString().Length < 1 || !flag4)
        {
          respostaValidacao.CampoComErro = TipoCampoLinha.CARTAOPROX;
          return respostaValidacao;
        }
        bool flag5 = long.TryParse(empregadoLista.CartaoBarras.ToString(), out result3);
        if (empregadoLista.CartaoBarras.ToString().Length > 16 || empregadoLista.CartaoBarras.ToString().Length < 1 || !flag5)
        {
          respostaValidacao.CampoComErro = TipoCampoLinha.CARTAO;
          return respostaValidacao;
        }
        bool flag6 = long.TryParse(empregadoLista.Teclado.ToString(), out result3);
        if (empregadoLista.Teclado.ToString().Length > 16 || empregadoLista.Teclado.ToString().Length < 1 || !flag6)
        {
          respostaValidacao.CampoComErro = TipoCampoLinha.TECLADO;
          return respostaValidacao;
        }
      }
      respostaValidacao.Validado = true;
      return respostaValidacao;
    }

    private bool VerificaSeAtualizaEmpregadosNaBase(Empregado empregadoLista)
    {
      Empregado empregado = new Empregado().PesquisarEmpregadosPorPis(empregadoLista);
      bool flag;
      if ((long) empregado.Cartao != (long) empregadoLista.Cartao)
        flag = true;
      else if (empregado.Nome != empregadoLista.Nome)
        flag = true;
      else
        flag = empregado.NomeExibicao != empregadoLista.NomeExibicao.PadRight(12, ' ').Substring(0, 12).TrimEnd(' ') || empregado.Senha != empregadoLista.Senha || empregado.VerificaBiometria != empregadoLista.VerificaBiometria;
      return flag;
    }

    private bool VerificaSeAtualizaEmpregadosNaBaseRepPlus(Empregado empregadoLista)
    {
      Empregado empregado = new Empregado().PesquisarEmpregadosPorPis(empregadoLista);
      bool flag;
      if ((long) empregado.Cartao != (long) empregadoLista.Cartao)
        flag = true;
      else if (empregado.Nome != empregadoLista.Nome)
        flag = true;
      else
        flag = empregado.NomeExibicao != empregadoLista.NomeExibicao.PadRight(12, ' ').Substring(0, 12).TrimEnd(' ') || empregado.Senha != empregadoLista.Senha || empregado.VerificaBiometria != empregadoLista.VerificaBiometria || empregado.DuplaVerificacao != empregadoLista.DuplaVerificacao || (long) empregado.CartaoProx != (long) empregadoLista.CartaoProx || (long) empregado.Teclado != (long) empregadoLista.Teclado;
      return flag;
    }

    private RespostaValidacao TestaValoresRepetidosNaBase(
      Empregado empregadoLista,
      bool cartaoAntigo)
    {
      RespostaValidacao respostaValidacao = new RespostaValidacao();
      respostaValidacao.Validado = false;
      if (this._lstEmpregadosBase.FindIndex((Predicate<Empregado>) (empregadoRepetido => empregadoRepetido.Pis == empregadoLista.Pis)) >= 0)
      {
        respostaValidacao.CampoComErro = TipoCampoLinha.PIS;
        return respostaValidacao;
      }
      if (cartaoAntigo && this._lstEmpregadosBase.FindIndex((Predicate<Empregado>) (empregadoRepetido => (long) empregadoRepetido.Cartao == (long) empregadoLista.Cartao)) >= 0)
      {
        respostaValidacao.CampoComErro = TipoCampoLinha.CARTAO;
        return respostaValidacao;
      }
      if (this._lstEmpregadosBase.FindIndex((Predicate<Empregado>) (empregadoRepetido => (long) empregadoRepetido.CartaoBarras == (long) empregadoLista.CartaoBarras)) >= 0 && empregadoLista.CartaoBarras != 0UL)
      {
        respostaValidacao.CampoComErro = TipoCampoLinha.CARTAO;
        return respostaValidacao;
      }
      if (this._lstEmpregadosBase.FindIndex((Predicate<Empregado>) (empregadoRepetido => (long) empregadoRepetido.CartaoProx == (long) empregadoLista.CartaoProx)) >= 0 && empregadoLista.CartaoProx != 0UL)
      {
        respostaValidacao.CampoComErro = TipoCampoLinha.CARTAOPROX;
        return respostaValidacao;
      }
      if (this._lstEmpregadosBase.FindIndex((Predicate<Empregado>) (empregadoRepetido => (long) empregadoRepetido.Teclado == (long) empregadoLista.Teclado)) >= 0 && empregadoLista.Teclado != 0UL)
      {
        respostaValidacao.CampoComErro = TipoCampoLinha.TECLADO;
        return respostaValidacao;
      }
      respostaValidacao.Validado = true;
      return respostaValidacao;
    }

    public static bool ValidaCaracteresInvalidos(string caracter)
    {
      int utf32 = char.ConvertToUtf32(caracter, 0);
      return utf32 >= 44 && (utf32 <= 57 || utf32 >= 65) && (utf32 <= 90 || utf32 >= 97) && (utf32 <= 122 || utf32 >= 192) && (utf32 <= 207 || utf32 >= 210) && utf32 != 241 && utf32 != 253 && utf32 != (int) byte.MaxValue || utf32 == 32 || utf32 == 8;
    }

    private void ConvertToWiegandFC(Empregado empFC)
    {
      try
      {
        string str = empFC.CartaoProx.ToString().PadLeft(8, '0');
        string s = Convert.ToInt16(str.Substring(0, 3)).ToString("X") + Convert.ToInt64(str.Substring(3)).ToString("X");
        empFC.CartaoProx = ulong.Parse(s, NumberStyles.HexNumber);
      }
      catch
      {
      }
    }
  }
}
