using Application.Services.Abstract;
using Domain.Dtos.Category;
using Domain.Dtos.PasswordCollection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IPasswordCollectionService _passwordCollectionService;
        private readonly ICategoryService _categoryService;

        public AdminController(IPasswordCollectionService passwordCollectionService, ICategoryService categoryService)
        {
            _passwordCollectionService = passwordCollectionService;
            _categoryService = categoryService;
        }

        #region Password Collection

        [HttpGet]
        public async Task<IActionResult> PasswordList()
        {
            var accountId = Convert.ToInt64(User.FindFirstValue(UIHelper.UserClaimsKey));
            var passwordList = await _passwordCollectionService.PasswordList(accountId); 
            ViewBag.Message = TempData["Message"];
            return View(passwordList);
        }

        [HttpGet]
        public async Task<IActionResult> PasswordCollectionModify(long? id)
        {
            PasswordCollectionDto passwordCollection = new();
            var accountId = Convert.ToInt64(User.FindFirstValue(UIHelper.UserClaimsKey));
            if (id != null && id > 0)
            {
                passwordCollection = await _passwordCollectionService.GetPasswordCollection(accountId, id);
            }
            var categories = await _categoryService.CategoryList(accountId);
            ViewBag.Categories = (from x in categories
                                  select new SelectListItem
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Name
                                  }
                                  ).ToList();
            return View(passwordCollection);
        }


        [HttpPost]
        public async Task<IActionResult> PasswordCollectionSave([FromForm] PasswordCollectionDto model)
        {
            string message;
            if (ModelState.IsValid)
            {
                var accountId = Convert.ToInt64(User.FindFirstValue(UIHelper.UserClaimsKey));
                var saveResult = await _passwordCollectionService.PasswordCollectionAddOrUpdate(accountId, model);
                if (saveResult != null && model.Id == 0)
                {
                    message = "Kayıt Başarılı Bir Şekilde Eklenmiştir.";
                }
                else if (saveResult != null && model.Id > 0)
                {
                    message = "Kayıt Başarılı Bir Şekilde Güncellenmiştir.";
                }
                else
                {
                    message = "Kayıt Eklenemedi! Bir sorunla karşılaştık";
                }
            }
            else
            {
                message = "Bilgiler doğru formatta girilmemiş.";
            }

            TempData["Message"] = message;
            return RedirectToAction("PasswordList", "Admin");
        }

        [HttpDelete]
        public async Task<JsonResult> DeletePasswordCollection(long id)
        {
            var accountId = Convert.ToInt64(User.FindFirstValue(UIHelper.UserClaimsKey));
            var deleteResult = await _passwordCollectionService.DeletePasswordCollection(accountId, id);
            return Json(deleteResult);
        }

        #endregion


        #region Category
        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var accountId = Convert.ToInt64(User.FindFirstValue(UIHelper.UserClaimsKey));
            var categoryList = await _categoryService.CategoryList(accountId);
            ViewBag.Message = TempData["Message"];
            return View(categoryList);
        }


        [HttpGet]
        public async Task<IActionResult> CategoryModify(long? id)
        {
            CategoryDto category = new();
            if (id != null && id > 0)
            {
                var accountId = Convert.ToInt64(User.FindFirstValue(UIHelper.UserClaimsKey));
                category = await _categoryService.GetCategory(accountId, id);
            }

            return View(category);
        }


        [HttpPost]
        public async Task<IActionResult> CategorySave([FromForm] CategoryDto model)
        {
            string message;
            if (ModelState.IsValid)
            {
                var accountId = Convert.ToInt64(User.FindFirstValue(UIHelper.UserClaimsKey));
                var saveResult = await _categoryService.CategoryAddOrUpdate(accountId, model);
                if (saveResult != null && model.Id == 0)
                {
                    message = "Kayıt Başarılı Bir Şekilde Eklenmiştir.";
                }
                else if (saveResult != null && model.Id > 0)
                {
                    message = "Kayıt Başarılı Bir Şekilde Güncellenmiştir.";
                }
                else
                {
                    message = "Kayıt Eklenemedi! Bir sorunla karşılaştık";
                }
            }
            else
            {
                message = "Bilgiler doğru formatta girilmemiş.";
            }

            TempData["Message"] = message;
            return RedirectToAction("CategoryList", "Admin");
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteCategory(long id)
        {
            var accountId = Convert.ToInt64(User.FindFirstValue(UIHelper.UserClaimsKey));
            var deleteResult = await _categoryService.DeleteCategory(accountId, id);
            return Json(deleteResult);
        }

        #endregion
    }
}
