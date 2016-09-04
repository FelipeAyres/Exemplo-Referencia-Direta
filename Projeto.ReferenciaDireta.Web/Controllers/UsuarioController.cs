using MySql.Data.MySqlClient;
using Projeto.ReferenciaDireta.Web.Models;
using System;
using System.Net;
using System.Web.Mvc;

namespace Projeto.ReferenciaDireta.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario/Logar
        public ActionResult Logar()
        {
            return View();
        }

        // POST: Usuario/Logar
        [HttpPost]
        public ActionResult Logar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var conexao = new MySqlConnection("Database=referenciaDireta;Data Source=localhost;User Id=root;Password=;");
                conexao.Open();
                var sql = "SELECT * FROM Usuarios WHERE username = @Usuario and senha = @Senha ";
                var cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@Usuario", usuario.UserName);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuario.UsuarioId = Convert.ToInt32(reader["codigo"]);
                    usuario.Nome = reader["nome"].ToString();
                    usuario.Cpf = reader["cpf"].ToString();
                }

                reader.Close();
                conexao.Close();
                conexao.Dispose();

                if (usuario.UsuarioId > 0)
                {
                    Session["Usuario"] = usuario;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.MsgErro = "O USUÁRIO NÃO EXISTE";
                    return View();
                }
            }
            return View();
        }

        // GET: /Usuario/Perfil/5
        [HttpGet]
        public ActionResult Perfil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session["Usuario"] == null)
                return RedirectToAction("Logar", "Usuario");

            Usuario usuario = new Usuario();

            var conexao = new MySqlConnection("Database=referenciaDireta;Data Source=localhost;User Id=root;Password=;");
            conexao.Open();

            //COMENTAR ESSA LINHA
            var sql = "SELECT * FROM Usuarios WHERE codigo = @codigo";
            //DESCOMENTAR ESSA LINHA
            //var sql = "SELECT * FROM Usuarios WHERE codigo = @codigo and codigo = @usuario";

            var cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@codigo", id);
            //DESCOMENTAR ESSA LINHA
            //cmd.Parameters.AddWithValue("@usuario", ((Usuario)Session["Usuario"]).UsuarioId);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                usuario.UsuarioId = Convert.ToInt32(reader["codigo"]);
                usuario.Nome = reader["nome"].ToString();
                usuario.UserName = reader["username"].ToString();
                usuario.Senha = reader["senha"].ToString();
                usuario.Cpf = reader["cpf"].ToString();
            }


            reader.Close();
            conexao.Close();
            conexao.Dispose();

            if (usuario.UsuarioId <= 0)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Perfil(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var conexao = new MySqlConnection("Database=referenciaDireta;Data Source=localhost;User Id=root;Password=;");
                conexao.Open();
                var sql = "UPDATE Usuarios SET nome = @nome, username = @username, senha = @senha, cpf = @cpf WHERE codigo = @codigo";
                var cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@codigo", usuario.UsuarioId);
                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@username", usuario.UserName);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@cpf", usuario.Cpf);

                cmd.ExecuteNonQuery();
                conexao.Close();
                conexao.Dispose();

                return RedirectToAction("Index", "Home");
            }
            return View(usuario);
        }


    }
}