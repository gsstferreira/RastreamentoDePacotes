using Common.Controllers;
using Common.Models;
using System;
using System.Web.Mvc;

namespace API.Controllers
{
    public class UsuarioController : BaseApiController
    {

        public UsuarioController() : base() {}

        [HttpGet]
        public string DadosDoUsuario()
        {
            var resp = new RespostaHttp();

            try
            {
                resp.Ok = true;
                resp.Mensagem = _user;
            }
            catch(Exception)
            {
                resp.Ok = false;
                resp.Mensagem = "Ocorreu um erro ao processar a requisição. (500)";
            }

            return Serialize(resp);
        }

        [HttpPost]
        [AllowAnonymous]
        public string CadastrarUsuario()
        {
            var resp = new RespostaHttp();

            try
            {
                string nome = _requestBody.GetValueAs<string>("NomeUsuario");
                string senhaDec = _requestBody.GetValueAs<string>("SenhaUsuario");
                string email = _requestBody.GetValueAs<string>("EmailUsuario");

                var oldUser = _usuarioService.ObterPorEmail(email);

                if(oldUser == null)
                {
                    var usuario = new Usuario
                    {
                        DataCadastro = DateTime.UtcNow,
                        Email = email,
                        Nome = nome,
                    };

                    usuario.CriarSenha(senhaDec);

                    var sucesso = _usuarioService.SalvarUsuario(usuario);

                    if (sucesso)
                    {
                        resp.Ok = true;
                        resp.Mensagem = usuario.UsuarioId;
                    }
                    else
                    {
                        resp.Ok = false;
                        resp.Mensagem = "Não foi possível realizar o cadastro do usuário.";
                    }
                }
                else
                {
                    resp.Ok = false;
                    resp.Mensagem = "Esse email já está vinculado a outra conta.";
                }
            }
            catch(Exception)
            {
                resp.Ok = false;
                resp.Mensagem = "Ocorreu um erro ao processar a requisição. (500)";
            }
            return Serialize(resp);
        }

        [HttpPost]
        [AllowAnonymous]
        public string Login()
        {
            var resp = new RespostaHttp();

            try
            {
                string email = _requestBody.GetValueAs<string>("EmailUsuario");
                string senha = _requestBody.GetValueAs<string>("SenhaUsuario");

                var user = _usuarioService.ObterPorEmail(email);

                if(user != null)
                {
                    if(user.VerificarSenha(senha))
                    {
                        resp.Ok = true;
                        resp.Mensagem = user.UsuarioId;
                    }
                    else
                    {
                        resp.Ok = false;
                        resp.Mensagem = "Senha incorreta."; 
                    }
                }
                else
                {
                    resp.Ok = false;
                    resp.Mensagem = "Esse email não está cadastrado.";
                }
            }
            catch(Exception)
            {
                resp.Ok = false;
                resp.Mensagem = "Ocorreu um erro ao processar a requisição. (500)";
            }

            return Serialize(resp);
        }

        [HttpPost]
        public string AlterarSenha()
        {
            var resp = new RespostaHttp();

            try
            {
                string senhaAntiga = _requestBody.GetValueAs<string>("SenhaAntiga");
                string senhaNova = _requestBody.GetValueAs<string>("SenhaNova");

                if(_user.VerificarSenha(senhaAntiga))
                {
                    _user.CriarSenha(senhaNova);
                    _usuarioService.SalvarUsuario(_user);

                    resp.Ok = true;
                    resp.Mensagem = "Senha alterada com sucesso.";

                }
                else
                {
                    resp.Ok = false;
                    resp.Mensagem = "A senha atual incorreta.";
                }
            }
            catch (Exception)
            {
                resp.Ok = false;
                resp.Mensagem = "Ocorreu um erro ao processar a requisição. (500)";
            }

            return Serialize(resp);
        }
    }
}