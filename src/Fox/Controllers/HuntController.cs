using System.Web.Http;
using AllowAnonymousAttribute = System.Web.Http.AllowAnonymousAttribute;

namespace Puma.Prey.Fox.Controllers
{
    public class HuntController : ApiController
    {
        [AllowAnonymous]
        /*
        public IEnumerable<Hunt> Get()
        {
            var hunts = new List<Hunt>();

            using (var context = new RabbitDBContext())
            {
                var q = from c in context.Hunts
                        select c;
                hunts = q.ToList();
            }

            return hunts;
        }
        */

        // GET api/hunt/5
        //[AllowAnonymous]
        /*
        public Hunt Get(int id)
        {
            Hunt hunt = null;

            using (var context = new RabbitDBContext())
            {
                var q = from c in context.Hunts
                        where c.Id == id
                        select c;
                hunt = q.FirstOrDefault();
            }

            return hunt;
        }

        // POST api/hunt
        public void Post([FromBody]string value)
        {
        }
        */
        // PUT api/hunt/5
        public void Put(int id, [FromBody] string value)
        {
        }
        /*
        // DELETE api/hunt/5
        public void Delete(string id)
        {
            using (var context = new RabbitDBContext())
            {
                string q = string.Format("DELETE FROM Hunt WHERE Id = {0}", id);
                context.Database.ExecuteSqlCommand(q);
            }
        }*/
    }
}
