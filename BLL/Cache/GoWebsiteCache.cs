using Common.Cache;
using Entities;
using Entities.Settings;
using Entities.System;
using Entities.Website;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CreationConverter = Entities.Documents.CreationConverter;
using DocumentType = Entities.System.DocumentType;


namespace BLL.Cache
{
    public class GoWebsiteCache: CacheBase
    {
        private readonly bool _loadImages;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public GoWebsiteCache(bool showDeleted = false, bool loadImages = true) : base(
            ApplicationSettingManager.SQLConnectionString, showDeleted)
        {
            _loadImages = loadImages;
        }



        #region GoWebSite

        public async Task<List<WebsitePage>> GetWebsitePages(bool showIsDeleted = false)
        {
            var result = await GetDocuments<WebsitePage>(documentType: DocumentType.WebsitePage);
            return result.Where(x => x.Document.IsDeleted == showIsDeleted).ToList();
        }


        public async Task<List<CaseStudyCard>> GetCaseStudyCard(bool showIsDeleted = false)
        {
            var result = await GetDocuments<CaseStudyCard>(documentType: DocumentType.CaseStudyCard);
            return result.Where(x => x.Document.IsDeleted == showIsDeleted).ToList();
        }


        public async Task<List<ColsData>> GetColsData(bool showIsDeleted = false)
        {
            var result = await GetDocuments<ColsData>(documentType: DocumentType.ColsData);
            return result.Where(x => x.Document.IsDeleted == showIsDeleted).ToList();
        }

        //public async Task<List<Section>> GetSections(bool showIsDeleted = false)
        //{
        //    var result = await GetDocuments<Section>(documentType: DocumentType.Section);
        //    return result.Where(x => x.Document.IsDeleted == showIsDeleted).ToList();
        //}


        #endregion


        public async Task<List<Image>> GetImages(Guid? parentImageId = null,
            Guid? documentId = null,
            Theme? theme = null,
            ImageType? imageType = null)
        {
            if (!_loadImages)
                return new List<Image>();
            try
            {
                var images = await GetListAsync<Image>(name: ClassLookup.Image);
                lock (Log)
                {
                    //var parentImages = images.Where(x => x.InternalStatus == BaseEntity.EntityStatus.New).Where(i => !i.ParentImageId.HasValue).ToList();
                    var childImages = images.Where(i => i.ParentImageId.HasValue).ToList();
                    var parentImages = images.Where(i => !i.ParentImageId.HasValue).ToList();

                    foreach (var image in images.Where(x => x.InternalStatus == BaseEntity.EntityStatus.New))
                    {
                        //if(image.Images == null)
                        //    image.Images = new List<Image>();
                        //this is a child image
                        if (image.ParentImageId.HasValue)
                        {
                            var parentImage = parentImages.FirstOrDefault(x => x.Id == image.ParentImageId.Value);
                            if (parentImage != null && parentImage.Images.All(x => x.Id != image.Id))
                                parentImage.Images.Add(item: image);
                        }
                        else
                        {
                            //this is a parent image
                            image.Images = childImages.Where(x => x.ParentImageId.HasValue && x.ParentImageId.Value == image.Id).ToList();
                        }

                        image.InternalStatus = BaseEntity.EntityStatus.Created;
                    }

                    //foreach (var parentImges in parentImages)
                    //{
                    //    parentImges.Images = images.Where(x => x.ParentImageId.HasValue && x.ParentImageId.Value == parentImges.Id).ToList();
                    //    parentImges.InternalStatus = BaseEntity.EntityStatus.Created;
                    //}
                    //if a new child image addedd we n
                    //var childImages = images.Where(x => x.InternalStatus == BaseEntity.EntityStatus.New).Where(i => !i.ParentImageId.HasValue).ToList();
                    //foreach (var childImage in childImages)
                    //{
                    //    var parentImage = images.FirstOrDefault(x => x.Id == childImage.ParentImageId);
                    //    parentImage?.Images.Add(childImage);

                    //}
                }



                if (theme.HasValue)
                    images = images.Where(i => i.ThemeId == (int)theme).ToList();
                if (imageType.HasValue)
                    images = images.Where(i => i.ImageTypeId == (int)imageType).ToList();

                //var retVal = new List<Image>();
                if (parentImageId.HasValue)
                {
                    images = images.Where(i => i.ParentImageId == parentImageId).ToList();
                    return images;
                }

                if (documentId.HasValue)
                    images = images.Where(i => i.DocumentId == documentId).ToList();
                return images;
            }
            catch (Exception ex)
            {
                Log.Error("Failed to load images", ex);
                throw;
            }
        }



     

        #region Private Methods

        private async Task<IEnumerable<T>> GetDocuments<T>(DocumentType documentType, bool loadImags = true)
            where T : IDocument
        {
            var items = await GetDocuments(documentType);
            List<Image> images = null;
            if (loadImags)
            {
                images = await GetImages();
            }

            var typedItems = items.Select(x =>
            {
                try
                {
                    T docObje = x.Deserialize<T>(new CreationConverter());
                    docObje.Document = x;
                    if (loadImags)
                    {
                        x.Images = images.Where(i => i.DocumentId == x.Id).ToList();
                    }

                    return docObje;
                }
                catch (Exception)
                {
                    return default(T);
                }
            });
            return typedItems;
        }

        public async Task<T> GetDocument<T>(Guid id)
            where T : IDocument
        {
            var document = await GetDocument(id);

            var docObje = document.Deserialize<T>(new CreationConverter());
            docObje.Document = document;
            return docObje;
        }

        public async Task<Document> GetDocument(Guid id)
        {
            var items = await GetDocuments();
            var document = items.FirstOrDefault(x => x.Id.Equals(g: id));

            return document;
        }

        public async Task<IEnumerable<Document>> GetDocuments(DocumentType documentType = null)
        {
            var items = Get<Document>(name: ClassLookup.Document);
            if (documentType != null)
                items = items.Where(x => x.DocumentTypeId == documentType);

            return items;
        }

        


        #endregion

    }

    public static class ClassLookup
    {
        public static string ApiLog = "ApiLog";
        public static string AppVersion = "AppVersion";
        public static string Capability = "Capability";
        public static string CapabilityTyp = "CapabilityTyp";
        public static string Country = "Country";
        public static string Currency = "Currency";
        public static string DocumentLead = "DocumentLead";
        public static string Document = "Document";
        public static string Image = "Image";
        public static string Language = "Language";
        public static string NotificationsType = "NotificationsType";
        public static string OperatingSystemVersion = "OperatingSystemVersion";
        public static string User = "User";


    }
}
