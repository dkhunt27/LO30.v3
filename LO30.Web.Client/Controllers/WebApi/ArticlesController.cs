﻿using LO30.Data;
using LO30.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LO30.Controllers
{
  public class ArticlesController : ApiController
  {
    
    public ArticlesController()
    {
      
    }

    //public List<Article> Get()
    //{
    //  List<Article> results = _repo.GetArticles();

    //  //var articles = results.OrderByDescending(t => t.Created)
    //  //                    .Take(25)
    //  //                    .ToList();
    //  var articles = results.ToList();

    //  return articles;
    //}

    //public HttpResponseMessage Get(int id)
    //{
    //  List<Article> results = _repo.GetArticles();

    //  var article = results.Where(t => t.ArticleId == id).FirstOrDefault();
      
    //  if (article != null)
    //  {
    //    return Request.CreateResponse(HttpStatusCode.OK, article);
    //  }
    //  else
    //  {
    //    return Request.CreateResponse(HttpStatusCode.NotFound);
    //  }
    //}

    //public HttpResponseMessage Post([FromBody]Article newArticle)
    //{
    //  if (newArticle.Created == default(DateTime))
    //  {
    //    newArticle.Created = DateTime.UtcNow;
    //  }

    //  if (_repo.AddArticle(newArticle) && _repo.Save())
    //  {
    //    return Request.CreateResponse(HttpStatusCode.Created, newArticle);
    //  }
    //  else
    //  {
    //    return Request.CreateResponse(HttpStatusCode.BadRequest);
    //  }
    //}
  }
}
