using LO30.Data;
using LO30.Data.Contexts;
using LO30.Data.Models;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LO30.Controllers
{
  public class AdminSettingsController : Controller
  {
    public AdminSettingsController()
    {
    }

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }

    [Authorize]
    [HttpPost]
    public ActionResult Create(Setting settingToCreate)
    {
      return View();

      //try
      //{
      //  if (!ModelState.IsValid) return View();

      //  _lo30ContextService.SaveOrUpdateSetting(settingToCreate);

      //  return RedirectToAction("List");
      //}
      //catch (Exception ex)
      //{
      //  ViewBag.ErrorMessage = "Unable to perform action.  Exception:" + ex.Message;
      //  return View(settingToCreate);
      //}
    }

    [Authorize]
    public ActionResult Delete(int id, bool? exception, string exceptionMessage)
    {
      if (exception.HasValue)
      {
        ViewBag.ErrorMessage = "Unable to perform action.  Exception:" + exceptionMessage;
      }

      var setting = new Setting();
      using (var context = new LO30Context())
      {
        setting = context.Settings.Where(x => x.SettingId == id).FirstOrDefault();
      }
      return View(setting);
    }

    [Authorize]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      return View();

      //try
      //{
      //  var deletedSetting = _repo.DeleteSettingBySettingId(id);

      //  return RedirectToAction("List");
      //}
      //catch (Exception ex)
      //{
      //  return RedirectToAction("Delete",
      //     new System.Web.Routing.RouteValueDictionary {
      //                                { "id", id },
      //                                { "exception", true },
      //                                { "exceptionMessage", ex.Message}
      //                            });
      //}
    }

    [Authorize]
    public ActionResult Details(int id)
    {
      var setting = new Setting();
      using (var context = new LO30Context())
      {
        setting = context.Settings.Where(x => x.SettingId == id).FirstOrDefault();
      }
      return View(setting);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      var setting = new Setting();
      using (var context = new LO30Context())
      {
        setting = context.Settings.Where(x => x.SettingId == id).FirstOrDefault();
      }
      return View(setting);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Edit(Setting settingToEdit)
    {
      return View();

      //try
      //{
      //  var originalSetting = new Setting();
      //  using (var context = new LO30Context())
      //  {
      //    originalSetting = context.Settings.Where(x => x.SettingId == id).FirstOrDefault();
      //  }

      //  if (!ModelState.IsValid) return View(originalSetting);

      //  _lo30ContextService.SaveOrUpdateSetting(settingToEdit);

      //  return RedirectToAction("List");
      //}
      //catch (Exception ex)
      //{
      //  ViewBag.ErrorMessage = "Unable to perform action.  Exception:" + ex.Message;
      //  return View(settingToEdit);
      //}

    }

    [Authorize]
    public ActionResult List()
    {
      var settings = new List<Setting>();
      using (var context = new LO30Context())
      {
        settings = context.Settings.ToList();
      }
      return View(settings);
    }

  }
}
