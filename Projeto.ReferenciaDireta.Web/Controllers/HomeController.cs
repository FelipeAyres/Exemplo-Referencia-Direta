using MySql.Data.MySqlClient;
using Projeto.ReferenciaDireta.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace Projeto.ReferenciaDireta.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Logar", "Usuario");
            }

            Usuario usuario = new Usuario();
            usuario = Session["Usuario"] as Usuario;
            ViewBag.NomeUsuario = usuario.Nome;

            
            Produto produto;
            List<Produto> produtos = new List<Produto>();

            var conexao = new MySqlConnection("Database=referenciaDireta;Data Source=localhost;User Id=root;Password=;");
            conexao.Open();
            var sql = "SELECT * FROM produtos";
            var cmd = new MySqlCommand(sql, conexao);

            DataSet ds = new DataSet();
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conexao);

            adapter.Fill(ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                produto = new Produto();
                produto.ProdutoId = Convert.ToInt32(dr["codigo"]);
                produto.Nome = dr["nome"].ToString();
                produto.Preco = Convert.ToDecimal(dr["preco"]);
                produto.Imagem = dr["imagem"].ToString();
                produtos.Add(produto);
            }           

            conexao.Close();
            conexao.Dispose();

            return View(produtos);                        
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Logar", "Usuario");
        }        
        


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}