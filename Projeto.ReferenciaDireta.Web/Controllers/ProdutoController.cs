using MySql.Data.MySqlClient;
using Projeto.ReferenciaDireta.Web.Models;
using System;
using System.Net;
using System.Web.Mvc;

namespace Projeto.ReferenciaDireta.Web.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session["Usuario"] == null)
                return RedirectToAction("Logar", "Usuario");


            Produto produto = new Produto();

            var conexao = new MySqlConnection("Database=referenciaDireta;Data Source=localhost;User Id=root;Password=;");
            conexao.Open();
            var sql = "SELECT * FROM Produtos WHERE codigo = @codigo";
            var cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@codigo", id);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                produto.ProdutoId = Convert.ToInt32(reader["codigo"]);
                produto.Nome = reader["nome"].ToString();
                produto.Preco = Convert.ToDecimal(reader["preco"]);
                produto.Imagem = reader["imagem"].ToString();
            }

            reader.Close();
            conexao.Close();
            conexao.Dispose();

            if (produto.ProdutoId <= 0)
            {
                return HttpNotFound();
            }
            return View(produto);
        }
    }
}