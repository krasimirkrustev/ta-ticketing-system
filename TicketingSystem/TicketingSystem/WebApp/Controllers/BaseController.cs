using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected IUowData uow;

        public BaseController()
        {
            this.uow = new UowData();
        }

        public BaseController(IUowData uow)
        {
            this.uow = uow;
        }
	}
}