using System;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Common;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using Connect.FlickrGallery.Core.Models.Photographers;

namespace Connect.FlickrGallery.Core.Repositories
{

	public class PhotographerRepository : ServiceLocator<IPhotographerRepository, PhotographerRepository>, IPhotographerRepository
 {
        protected override Func<IPhotographerRepository> GetFactory()
        {
            return () => new PhotographerRepository();
        }
        public IEnumerable<Photographer> GetPhotographers(int moduleId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<Photographer>();
                return rep.Get(moduleId);
            }
        }
        public Dictionary<string, Photographer> GetPhotographersDictionary(int moduleId)
        {
            return GetPhotographers(moduleId).ToDictionary(p => p.FlickrId, p => p);
        }
        public Photographer GetPhotographer(int moduleId, int photographerId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<Photographer>();
                return rep.GetById(photographerId, moduleId);
            }
        }
        public int AddPhotographer(ref PhotographerBase photographer)
        {
            Requires.NotNull(photographer);
            Requires.PropertyNotNegative(photographer, "ModuleId");
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<PhotographerBase>();
                rep.Insert(photographer);
            }
            return photographer.PhotographerId;
        }
        public void DeletePhotographer(PhotographerBase photographer)
        {
            Requires.NotNull(photographer);
            Requires.PropertyNotNegative(photographer, "PhotographerId");
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<PhotographerBase>();
                rep.Delete(photographer);
            }
        }
        public void DeletePhotographer(int moduleId, int photographerId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<PhotographerBase>();
                rep.Delete("WHERE ModuleId = @0 AND PhotographerId = @1", moduleId, photographerId);
            }
        }
        public void UpdatePhotographer(PhotographerBase photographer)
        {
            Requires.NotNull(photographer);
            Requires.PropertyNotNegative(photographer, "PhotographerId");
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<PhotographerBase>();
                rep.Update(photographer);
            }
        } 
 }

    public interface IPhotographerRepository
    {
        IEnumerable<Photographer> GetPhotographers(int moduleId);
        Dictionary<string, Photographer> GetPhotographersDictionary(int moduleId);
        Photographer GetPhotographer(int moduleId, int photographerId);
        int AddPhotographer(ref PhotographerBase photographer);
        void DeletePhotographer(PhotographerBase photographer);
        void DeletePhotographer(int moduleId, int photographerId);
        void UpdatePhotographer(PhotographerBase photographer);
    }
}

