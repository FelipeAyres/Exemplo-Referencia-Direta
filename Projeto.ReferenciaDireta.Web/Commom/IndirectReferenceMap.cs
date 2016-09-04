using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.ReferenciaDireta.Web.Commom
{
    public static class IndirectReferenceMap
    {
        public static int GetDirectReference(Guid indirectReference)
        {
            var map = (Dictionary<Guid, int>)HttpContext.Current.Session["IndirectMap"];
            return map[indirectReference];
        }


        public static Guid GetIndirectReference(int directReference)
        {
            var map = (Dictionary<int, Guid>)HttpContext.Current.Session["DirectMap"];
            return map == null ?
              AddDirectReference(directReference)
              : map[directReference];
        }


        private static Guid AddDirectReference(int directReference)
        {
            var indirectReference = Guid.NewGuid();
            HttpContext.Current.Session["DirectMap"] = new Dictionary<int, Guid> { { directReference, indirectReference } };
            HttpContext.Current.Session["IndirectMap"] = new Dictionary<Guid, int> { { indirectReference, directReference } };
            return indirectReference;
        }
    }
}